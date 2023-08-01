<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gestione_checkin.ascx.vb" Inherits="gestione_danni_gestione_checkin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="edit_danno.ascx" TagName="edit_danno" TagPrefix="uc1" %>
<%@ Register Src="DettagliPagamento.ascx" TagName="DettagliPagamento" TagPrefix="uc1" %>
<%@ Register Src="gestione_note.ascx" TagName="gestione_note" TagPrefix="uc1" %>

<link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
<script type="text/javascript"  src="/lytebox.js"></script>


<style type="text/css">
    .style10
    {
        width: 178px;
    }
    .style11
    {
        width: 172px;
    }
    .style13
    {
        width: 80px;
    }
    .style14
    {
        width: 260px;
    }
    .style15
    {
        width: 80px;
    }
</style>


<div id="div_ricerca" runat="server" visible="false">

<ajaxtoolkit:TabContainer ID="tabPanelImport" runat="server" ActiveTabIndex="0" 
        Width="100%" Visible="true">

  <ajaxtoolkit:TabPanel ID="TabFiltroRicerca" runat="server" HeaderText="Ricerca per targa">
    <HeaderTemplate>Ricerca per targa</HeaderTemplate>
    <ContentTemplate>

<div id="div_ricerca_danno_targa" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Ricerca danno per targa</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_targa" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Stato:" CssClass="testo_bold"></asp:Label>
                <asp:DropDownList ID="DropDownList_stato_danno" runat="server">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                    <asp:ListItem Value="1">Aperto</asp:ListItem>
                    <asp:ListItem Value="2">Chiuso</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:60%;">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="bt_cerca_targa" runat="server" Text="Cerca" ValidationGroup="CercaTarga"/>
            </td>
        </tr>
    </table>

<asp:ValidationSummary ID="ValidationSummary2" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="CercaTarga" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="tx_targa" ErrorMessage="Specificare una targa valida." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="CercaTarga"></asp:RequiredFieldValidator> 
</div>

    </ContentTemplate>
  </ajaxtoolkit:TabPanel>

  <ajaxtoolkit:TabPanel ID="tabImportXml" runat="server" HeaderText="Ricerca per stazione">
    <HeaderTemplate>Ricerca per stazione</HeaderTemplate>
    <ContentTemplate>

<div id="div_ricerca_danno_stazione" runat="server" >
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Ricerca auto con danni</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="lblCercaDitta" runat="server" Text="Stazioni:" CssClass="testo_bold"></asp:Label>&nbsp;
                <asp:DropDownList ID="DropDownStazioni" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStazioni" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Data da:" CssClass="testo_bold"></asp:Label>&nbsp;
                 <a onclick="Calendar.show(document.getElementById('<%=tx_DataDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox ID="tx_DataDa" runat="server" Width="70px"></asp:TextBox>
                     </a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_DataDa" ID="CalendarExtender1">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="tx_DataDa" ID="MaskedEditExtender1">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Data a:" CssClass="testo_bold"></asp:Label>&nbsp;
                <a onclick="Calendar.show(document.getElementById('<%=tx_DataA.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox ID="tx_DataA" runat="server" Width="70px"></asp:TextBox>
                    </a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_DataA" ID="CalendarExtender2">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="tx_DataA" ID="MaskedEditExtender2">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Stato:" CssClass="testo_bold"></asp:Label>
                <asp:DropDownList ID="DropDown_stato_danno" runat="server">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                    <asp:ListItem Value="1">Aperto</asp:ListItem>
                    <asp:ListItem Value="2">Chiuso</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="bt_cerca_veicoli" runat="server" Text="Cerca" />
            </td>
        </tr>
    </table>
</div>

<div id="div_elenco_auto" runat="server">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table6">
    <tr>
      <td>
          <asp:ListView ID="listViewElencoAuto" runat="server" DataSourceID="sqlElencoAuto">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_num" runat="server" Text='<%# Eval("num") %>' Visible="false" />
                          <asp:Label ID="lb_id_stazione" runat="server" Text='<%# Eval("id_stazione") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_modello" runat="server" Text='<%# Eval("modello") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_num" runat="server" Text='<%# Eval("num") %>' Visible="false" />
                          <asp:Label ID="lb_id_stazione" runat="server" Text='<%# Eval("id_stazione") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_modello" runat="server" Text='<%# Eval("modello") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Per i filtri applicati non è stata trovata nessuna voce.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th1" runat="server">
                                          Stazione</th>
                                      <th id="Th2" runat="server">
                                          Targa</th>
                                      <th id="Th3" runat="server">
                                          Modello</th>
                                      <th id="Th5" runat="server">
                                          </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="20" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
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
  <br />
</div>

    </ContentTemplate>
  </ajaxtoolkit:TabPanel>
      
</ajaxtoolkit:TabContainer>

</div>

<div runat="server" id="div_targa" visible="false">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td  align="left" style="color: #FFFFFF;background-color:#444;"> 
               
            <div id="div_num_documento" runat="server" visible="false" >
                &nbsp;<b>Numero Documento: </b>
                <asp:Label ID="lb_num_documento1" runat="server" Text="" ></asp:Label>
                <asp:LinkButton ID="lb_num_documento" runat="server" CssClass="testo_bold" style="color: #FFFFFF" Text="" />
                &nbsp;
            </div>
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Stazione: </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>    
            <asp:Label ID="IDdelVeicolo" runat="server" Text="ID" Visible="False" ></asp:Label>  
        </td>
    </tr>
</table>
</div>

<div id="div_elenco_danni" runat="server" visible="false" style="background-color:#c8c8c6 !Important;">
<table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
<tr>
    <td>
        <asp:Label ID="Label7" runat="server" Text="Stato Del Veicolo" ></asp:Label>
        <asp:Label ID="AutoInOdl" runat="server" Text="Veicolo in ODL. Il Check In dovrà essere fatto alla chiusura del ODL." Font-Bold="True" ForeColor="Red" Visible="False" ></asp:Label>
    </td>
    <td align="right">
        <asp:Button ID="bt_stampa_atto_notorio" runat="server" Text="Stampa Atto Notorio" />
        <asp:Button ID="bt_stampa_check_out" runat="server" Text="Stampa Check Out" />
        <asp:Button ID="bt_stampa_check_in" runat="server" Text="Stampa Check In" visible="false"/>
    </td>
</tr>
<tr>
    <td colspan="2">
  <ajaxtoolkit:TabContainer ID="tab_mappe" runat="server" ActiveTabIndex="0" 
            Width="100%">

      <ajaxtoolkit:TabPanel ID="tab_fronte" runat="server" HeaderText="Vista Fronte">
            <HeaderTemplate>Carrozzeria</HeaderTemplate>
            <ContentTemplate>
            
<div id="div_img_fronte" runat="server" style="position:relative;" >
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table4">
  <tr runat="server">
    <td id="Td1" runat="server" valign="top">
        
        <asp:Image ID="img_fronte" runat="server"  ImageUrl="~/images/SchemaAuto.gif" style="position:relative;" />

    </td>
    <td id="Td9" style="vertical-align:top;" runat="server">
        <asp:ListView ID="lv_elenco_danni_F" runat="server" 
            DataSourceID="sqlDanniMappati_F" DataKeyNames="id_danno">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="Label5" runat="server" Text='<%# Eval("indice") %>'/>
                         <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                         <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                       </td>
                       <td>
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <!-- Tony 16/06/2022 -->
                      <td id="Td15" align="center" width="40px" runat="server" visible="True">
                          <asp:ImageButton ID="imgBtnFoto" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lentefoto"/>
                      </td>
                      <!-- FINE -->                      
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="Label5" runat="server" Text='<%# Eval("indice") %>'/>
                         <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                         <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                       </td>
                       <td>
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <!-- Tony 16/06/2022 -->
                      <td id="Td15" align="center" width="40px" runat="server" visible="True">
                          <asp:ImageButton ID="imgBtnFoto" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lentefoto"/>
                      </td>
                      <!-- FINE -->
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun danno aperto sulla vista fronte.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th7" runat="server">
                                          N°</th>
                                      <th id="Th1" runat="server">
                                          Posizione</th>
                                      <th id="Th2" runat="server">
                                          Danno</th>
                                      <th id="Th3" runat="server">
                                          Entità</th>                                      
                                      <th id="Th9" runat="server">
                                          Foto</th>                                       
                                      <th id="th_lente" runat="server">
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
</div>

            </ContentTemplate>
        </ajaxtoolkit:TabPanel>

      <ajaxtoolkit:TabPanel ID="tab_retro" runat="server" HeaderText="Vista Retro" Visible="false" >
            <HeaderTemplate>Vista Retro</HeaderTemplate>
            <ContentTemplate>

<div id="div_img_retro" runat="server" style="position:relative;" visible="false">
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table9">
  <tr>
    <td valign="top">
        
        <asp:Image ID="img_retro" runat="server"  ImageUrl="~/images/SchemaAuto.gif" style="position:relative;" />
        
    </td>
    <td style="vertical-align:top;">
        <asp:ListView ID="lv_elenco_danni_R" runat="server" DataSourceID="sqlDanniMappati_R" DataKeyNames="id_danno">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="Label5" runat="server" Text='<%# Eval("indice") %>'/>
                         <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                         <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                       </td>
                       <td>
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="Label5" runat="server" Text='<%# Eval("indice") %>'/>
                         <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                         <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                       </td>
                       <td>
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun danno aperto sulla vista retro.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th7" runat="server">
                                          N°</th>
                                      <th id="Th1" runat="server">
                                          Posizione</th>
                                      <th id="Th2" runat="server">
                                          Danno</th>
                                      <th id="Th3" runat="server">
                                          Entità</th>
                                      <th id="th_lente" runat="server">
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
</div>

            </ContentTemplate>
      </ajaxtoolkit:TabPanel>

      <ajaxtoolkit:TabPanel ID="tab_accessori" runat="server" HeaderText="Accessori/Dotazioni">
            <HeaderTemplate>Accessori/Dotazioni</HeaderTemplate>
            <ContentTemplate>

<div id="div_accessori" runat="server" style="position:relative;" >
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">
  <tr>
    <td>
        <b>Dotazioni</b>
    </td>
  </tr>
  <tr>
    <td style="vertical-align:top;">
        <asp:ListView ID="ListView_dotazioni" runat="server" DataSourceID="sql_dotazioni" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_descrizione_ing" runat="server" Text='<%# Eval("descrizione_ing") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                          <asp:Label ID="lb_des_assente" runat="server" Text='' />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_descrizione_ing" runat="server" Text='<%# Eval("descrizione_ing") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                          <asp:Label ID="lb_des_assente" runat="server" Text='' />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessuna dotazione censita per il veicolo.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Dotazioni</th>
                                      <th>
                                          Accessories</th>
                                      <th>
                                          Assente</th>
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
  <tr>
    <td>
        <b>Accessori</b>
    </td>
  </tr>
  <tr>
    <td style="vertical-align:top;">
        <asp:ListView ID="ListView_accessori" runat="server" DataSourceID="sql_acessori" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                          <asp:Label ID="lb_des_assente" runat="server" Text='' />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                          <asp:Label ID="lb_des_assente" runat="server" Text='' />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun accessorio selezionato.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Dotazioni</th>
                                      <th>
                                          Assente</th>
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
</div>

            </ContentTemplate>
      </ajaxtoolkit:TabPanel>

      <ajaxtoolkit:TabPanel ID="tab_meccanici" runat="server" HeaderText="Danni Meccanici/Elettrici">
            <HeaderTemplate>Danni Meccanici/Elettrici/Altro</HeaderTemplate>
            <ContentTemplate>

<div id="div_meccanici_elettrici" runat="server" style="position:relative;" >
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table11">
  <tr runat="server">
    <td style="vertical-align:top;" runat="server">
        <asp:ListView ID="ListView_meccanici_elettrici" runat="server" 
            DataSourceID="sql_meccanici_elettrici" DataKeyNames="id_danno" 
            EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="Label19" runat="server" Text='Guasto' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="Label19" runat="server" Text='Guasto' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente_storico.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun danno meccanio/elettrico.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th7" runat="server">
                                          Tipo</th>
                                      <th id="Th1" runat="server">
                                          Descrizione</th>
                                      <th id="Th8" runat="server">
                                          Danno</th>
                                      <th id="th_lente" runat="server">
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
</div>

            </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      
</ajaxtoolkit:TabContainer>
    </td>
</tr>
<tr>
    <td colspan="2">

<div id="div_nuovo_checkin" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
       <tr>
        <td style="width:20%">
            <asp:Button ID="bt_furto" runat="server" Text="Furto Veicolo" visible="false"/>
        </td>
        <td align="center">
            <asp:Button ID="bt_nessun_danno" runat="server" Text="Nessun Nuovo Danno" Visible="false"/>
            <asp:Button ID="bt_nuovo_evento_danno" runat="server" Text="Nuovo Danno" />
            <asp:Button ID="bt_chiudi_chekin" runat="server" Text="Chiudi"/>
        </td>
        <td  style="width:20%">
            &nbsp;</td>
       </tr>
    </table>
</div>    

    </td>
</tr>
</table>
</div>


<div id="div_edit_evento" runat="server" visible="false" style="background-color:#c8c8c6 !Important;">
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b><asp:Label ID="lb_intestazione_evento" runat="server" Text="Nuovo Evento"></asp:Label></b> 
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
      <tr>
        <td>
            <asp:Label ID="Label14" runat="server" Text="Evento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="DropDownTipoEventoAperturaDanno" runat="server"  AppendDataBoundItems="True" 
                DataSourceID="sql_tipo_documento_apertura_danno" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Nota:" CssClass="testo_bold"></asp:Label>
        </td>
        <td rowspan="2" style="vertical-align:top;">
            <asp:TextBox ID="tx_nota_evento" runat="server" Width="340px" Height="65px" TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Data:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <a onclick="Calendar.show(document.getElementById('<%=tx_data_evento.ClientID%>'), '%d/%m/%Y', false)">
            <asp:TextBox ID="tx_data_evento" runat="server" Width="70px" ></asp:TextBox>
                </a>
            <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_DataDa" ID="CalendarExtender1">
                </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="tx_data_evento" ID="MaskedEditExtender3">
            </ajaxtoolkit:MaskedEditExtender>
        </td>
        <td>&nbsp;
        </td>
       </tr>
       <tr id="riga_per_furto" runat="server" visible="false">
        <td>
            <asp:Label ID="Label43" runat="server" Text="Data Denuncia Furto:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_data_denuncia_furto" runat="server" Width="70px" ></asp:TextBox>
            <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_denuncia_furto" ID="CalendarExtender5">
            </ajaxtoolkit:CalendarExtender>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date"  TargetControlID="tx_data_denuncia_furto" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender5">
            </ajaxtoolkit:MaskedEditExtender>
            &nbsp;
            <asp:Label ID="Label44" runat="server" Text="Ora:" CssClass="testo_bold"></asp:Label>&nbsp;
            <asp:TextBox ID="tx_ora_denuncia_furto" runat="server" Width="40px" ></asp:TextBox>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" 
                              TargetControlID="tx_ora_denuncia_furto"
                              Mask="99:99"
                              MessageValidatorTip="true"
                              ClearMaskOnLostFocus="true"
                              OnFocusCssClass="MaskedEditFocus"
                              OnInvalidCssClass="MaskedEditError"
                              MaskType="Time"
                              CultureName="en-US">
            </ajaxToolkit:MaskedEditExtender>
        </td>
        <td colspan="2">&nbsp;
        </td>
       </tr>
       <tr>
        <td colspan="4">
<div id="div_elenco_edit_evento" runat="server" visible="false">
<ajaxtoolkit:TabContainer ID="tabPanelEvento" runat="server" ActiveTabIndex="0" 
        Width="100%">

<ajaxtoolkit:TabPanel ID="Tab_evento" runat="server" HeaderText="Elenco Danni">
    <HeaderTemplate>Elenco Danni</HeaderTemplate>
    <ContentTemplate>

 <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table3">
    <tr runat="server" id="riga_da_addebitare">
       <td>
       
           <asp:Button ID="bt_da_addebitare_F" runat="server" Text="Tutti Da Addebitare" />
           <asp:Button ID="bt_da_non_addebitare_F" runat="server" Text="Tutti Da Non Addebitare" />
           &nbsp;
           <asp:Label ID="Label54" runat="server" Text="Motivo Non Addebito:" CssClass="testo_bold"></asp:Label>
           <asp:DropDownList ID="DropDownNonAddebito_F" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                </asp:DropDownList>
       </td>
    </tr>
    <tr runat="server">
      <td runat="server">
          <asp:ListView ID="listViewElencoDanniPerEvento" runat="server" 
              DataSourceID="sqlElencoDanniPerEvento" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />                          
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                      <td id="Td3" runat="server" visible='<%# lb_th_da_addebitare.Text %>'>
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <%  
                          'RDS (Nuovo-Modifiche-Eliminazione)
                          If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "168") = 3 Then%>
                            <td id="Td16" align="center" width="40px" runat="server" visible="true">                          
                                <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il record?'));" />                                                        
                            </td>
                      <%  Else
                      %>    
                            <td id="Td18" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>                          
                                &nbsp;
                            </td>
                      <% end if %>

                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                      <td id="Td3" runat="server" visible='<%# lb_th_da_addebitare.Text %>'>
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <%  
                          'RDS (Nuovo-Modifiche-Eliminazione)
                          If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "168") = 3 Then%>
                            <td id="Td16" align="center" width="40px" runat="server" visible="true">                          
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il record?'));" />                                                        
                            </td>
                      <%  Else
                      %>    
                            <td id="Td18" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>                          
                                &nbsp;
                            </td>
                      <% end if %>                      
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Nessun danno salvato per questo evento.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th4" runat="server">
                                          Tipo</th>
                                      <th id="Th1" runat="server">
                                          Posizione</th>
                                      <th id="Th2" runat="server">
                                          Danno</th>
                                      <th id="Th3" runat="server">
                                          Entità</th>
                                      <th id="Th6" runat="server">
                                          Riparato</th>
                                      <th id="th_da_addebitare" runat="server">
                                          Da Addebitare</th>
                                      <th id="th_lente" runat="server">
                                          </th>
                                    <th id="th_elimina" runat="server">
                                          </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="20" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
                                  </Fields>
                              </asp:DataPager>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
      </td>
    </tr>
    <tr runat="server">
        <td align="center" runat="server">
            <asp:Button ID="bt_nuovo_danno" runat="server" Text="Nuovo Danno" />
            <asp:Button ID="bt_addebita_furto" runat="server" Text="Da Addebitare" 
                Visible="False" />
            <asp:Button ID="bt_non_addebita_furto" runat="server" Text="Da Non Addebitare" 
                Visible="False" />
            <asp:Button ID="bt_rientro_veicolo_rubato" runat="server" 
                Text="Rientro Veicolo Rubato" Visible="False" />
            <asp:Button ID="bt_pagamento_da_furto" runat="server" Text="Pagamento" 
                Visible="False" />
            <asp:Button ID="bt_salva_furto" runat="server" Text="Salva Furto" 
                Visible="False" ValidationGroup="SalvaFurto" />
            <asp:Button ID="bt_chiudi_senza_furto" runat="server" Text="Chiudi" 
                Visible="False" />           
        </td>
    </tr>
  </table>	
	</ContentTemplate>
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="Tab_documenti" runat="server" HeaderText="Documenti">
    <HeaderTemplate>Documenti</HeaderTemplate>
    <ContentTemplate>

   <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table7">
    <tr id="Tr4" runat="server">
      <td id="Td3" colspan="2" runat="server">
         <asp:ListView ID="listViewDocumenti" runat="server" 
              DataSourceID="sqlDocumenti" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td> 
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <%--<a runat="server" id="doc_img" href="/images/DocDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni]'><img src="/images/lente.png" style="width: 16px" /></a>--%>
                        <a id="doc_altro" href="/images/DocDanni/<%# Eval("riferimento_foto") %>" target="_blank" ><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <%--<a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni]'><img src="/images/lente.png" style="width: 16px" /></a>--%>
                        <a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" target="_blank" ><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun Documento Allegato.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th5" runat="server">
                                          Documento</th>
                                      <th id="Th2" runat="server">
                                          Nome</th>
                                      <th id="th_lente" runat="server">
                                          </th>
                                      <th id="th_elimina" runat="server">
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
    <tr id="Tr5" runat="server">
        <td id="Td4" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Allega Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td5" runat="server" >
            <asp:FileUpload ID="FileUpload1" size="45" runat="server" />
        </td>
    </tr>
    <tr id="Tr6" runat="server">
        <td id="Td6" runat="server">
            <asp:Label ID="Label3" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td7" runat="server" >
            <asp:DropDownList ID="DropDownTipoDocumentoImg" runat="server" AppendDataBoundItems="True" 
                      DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnInviaFile" runat="server" Text="Salvataggio" ValidationGroup="Upload" />
        </td>
    </tr>
   </table> 	

<asp:ValidationSummary ID="ValidationSummary4" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Upload" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
    ControlToValidate="FileUpload1" ErrorMessage="Nessuna immagine selezionata." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator8" runat="server" 
    ControlToValidate="DropDownTipoDocumentoImg" ErrorMessage="Specificare il tipo del documento."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Upload" ></asp:CompareValidator>

	</ContentTemplate>
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="Tab_fermo_tecnico" runat="server" HeaderText="In fermo tecnico presso" Visible="false">
    <HeaderTemplate>In fermo tecnico presso</HeaderTemplate>
    <ContentTemplate>

   <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table5">
    <tr id="Tr8" runat="server">
      <td id="Td10" colspan="2" runat="server">
         <asp:ListView ID="listView1" runat="server" 
              DataSourceID="sqlDocumenti" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td> 
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni]'><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni]'><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun Documento Allegato.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th5" runat="server">
                                          Documento</th>
                                      <th id="Th2" runat="server">
                                          Nome</th>
                                      <th id="th_lente" runat="server">
                                          </th>
                                      <th id="th_elimina" runat="server">
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
    <tr id="Tr9" runat="server">
        <td id="Td11" runat="server">
            <asp:Label ID="Label40" runat="server" Text="Allega Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td12" runat="server" >
            <asp:FileUpload ID="FileUpload2" size="45" runat="server" />
        </td>
    </tr>
    <tr id="Tr10" runat="server">
        <td id="Td13" runat="server">
            <asp:Label ID="Label41" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td14" runat="server" >
            <asp:DropDownList ID="DropDown_fermo_tecnico" runat="server" AppendDataBoundItems="True" 
                      DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="Salvataggio" ValidationGroup="Upload" />
        </td>
    </tr>
   </table> 	

<asp:ValidationSummary ID="ValidationSummary6" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Upload" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
    ControlToValidate="FileUpload2" ErrorMessage="Nessuna immagine selezionata." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator4" runat="server" 
    ControlToValidate="DropDown_fermo_tecnico" ErrorMessage="Specificare il tipo del documento."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Upload" ></asp:CompareValidator>

	</ContentTemplate>
</ajaxtoolkit:TabPanel>
      
</ajaxtoolkit:TabContainer>

</div>

        </td>
       </tr>
       <%--<tr>
            <td colspan="4">
                <div id="div_bt_nuovo_danno" runat="server" visible="false" style="text-align: center;">
                    <asp:Button ID="btnNuovoDanno" runat="server" Text="Nuovo Danno"  Visible="True" />
                </div>
            </td>
       </tr>--%>
    </table>
</div>



<div id="div_edit_danno" runat="server" style="background-color:#c8c8c6 !Important;">
<br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b><asp:Label ID="Label39" runat="server" Text="Dettaglio Danno"></asp:Label></b> 
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
      <tr>
        <td>
<uc1:edit_danno id="edit_danno" runat="server" />
        </td>
      </tr>
    </table>
</div>

<div id="div_salva_checkin" runat="server" visible="false" style="background-color:#c8c8c6 !Important;">
<script  type="text/javascript">
    function NoReadyToGo() {
        var rb_ready_to_go_1 = document.getElementById('<%= rb_ready_to_go.UniqueID.replace("$","_") %>_1');
        var ck_rifornire = document.getElementById('<%= ck_rifornire.UniqueID.replace("$","_") %>');
        var ck_lavare = document.getElementById('<%= ck_lavare.UniqueID.replace("$","_") %>');
        var ck_fermo_tecnico = document.getElementById('<%= ck_fermo_tecnico.UniqueID.replace("$","_") %>');
        var ck_furto = document.getElementById('<%= ck_furto.UniqueID.Replace("$", "_") %>');

        if (ck_rifornire.checked) {
            rb_ready_to_go_1.checked = true;
        }
        if (ck_lavare.checked) {
            rb_ready_to_go_1.checked = true;
        }
        if (ck_fermo_tecnico.checked) {
            rb_ready_to_go_1.checked = true;
        }
        if (ck_furto.checked) {
            rb_ready_to_go_1.checked = false;
        }
        return false;
    }
</script>
<br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b>Check In</b> 
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444;border-bottom:none;" runat="server" id="tab_checkin">
      <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Ready To Go:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
          <asp:RadioButtonList ID="rb_ready_to_go" runat="server" 
                RepeatDirection="Horizontal" CssClass="testo_bold">
                <asp:ListItem Value="1">Si</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:RadioButtonList>  <asp:Label ID="lbl_status_veicolo" runat="server" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label13" runat="server" Text="Km Rientro:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_km_rientro" runat="server" Width="70px"></asp:TextBox>
            &nbsp;<asp:Label ID="lb_km_uscita_memo" runat="server" Text='' CssClass="testo_bold"></asp:Label>
        </td>
       </tr>
       <tr>
        <td>
            <asp:Label ID="Label16" runat="server" Text="Non noleggiabile perché da lavare:" CssClass="testo_bold" Visible="false" ></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ck_lavare" runat="server" Visible="false"  />
        </td>
        <td>
            <asp:Label ID="Label15" runat="server" Text="Serbatoio Rientro:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="DropDownSerbatoioRientro" runat="server">
                <asp:ListItem Value="-1" Selected="True">Seleziona...</asp:ListItem>
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="1">1/8</asp:ListItem>
                <asp:ListItem Value="2">2/8</asp:ListItem>
                <asp:ListItem Value="3">3/8</asp:ListItem>
                <asp:ListItem Value="4">4/8</asp:ListItem>
                <asp:ListItem Value="5">5/8</asp:ListItem>
                <asp:ListItem Value="6">6/8</asp:ListItem>
                <asp:ListItem Value="7">7/8</asp:ListItem>
                <asp:ListItem Value="8">8/8</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lb_serbatoio_memo" runat="server" Text='' CssClass="testo_bold"></asp:Label>
        </td>
       </tr>
       <tr runat="server" visible="false">
        <td>
            <asp:Label ID="Label17" runat="server" Text="Non noleggiabile perché da rifornire:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ck_rifornire" runat="server" onclick="javascript:NoReadyToGo();"/>
        </td>
        <td colspan="2">
        </td>
       </tr>
       <tr>
        <td>
            <asp:Label ID="Label38" runat="server" Text="Non noleggiabile per fermo tecnico:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ck_fermo_tecnico" runat="server" onclick="javascript:NoReadyToGo();" />
        </td>
        <td colspan="2">
        </td>
       </tr>
       <tr>
        <td>
            <asp:Label ID="Label53" runat="server" 
                Text="Non noleggiabile per vendita/Buy Back:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ck_vendita_buy_back" runat="server" onclick="javascript:NoReadyToGo();" />
           </td>
        <td colspan="2">
            &nbsp;</td>
       </tr>
        <tr>
        <td>
            <asp:Label ID="Label55" runat="server" 
                Text="Non noleggiabile per furto:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="ck_furto" runat="server" onclick="javascript:NoReadyToGo();" />
           </td>
        <td colspan="2">
            &nbsp;</td>
       </tr>

       <tr>
        <td style="vertical-align:top">
            <asp:Label ID="Label18" runat="server" Text="Note:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_altro" runat="server"  Width="240px" Height="65px" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
       </tr>
       </table>
       <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444;border-top:none;">
       <tr>
        <td colspan="4" align="center">
            <asp:Button ID="bt_pagamento" runat="server" Text="Pagamento" Visible="false"/>
            <asp:Button ID="bt_salva_checkin" runat="server" Text="Salva Check In" ValidationGroup="SalvaCheckIn" OnClientClick="javascript: return(window.confirm ('CONTROLLARE I KM DI RIENTRO: Sei sicuro di voler effettuare il Check-In con i dati inseriti?'));" />
            <asp:Button ID="bt_chiudi" runat="server" Text="Chiudi"/>
            <asp:Label ID="lblAux" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
       </tr>
    </table>
    <div runat="server" id="manutenzione_ordinaria" visible= "false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b>Informazioni manutenzione ordinaria</b> 
            </td>
        </tr>
    </table>
    <table border="1" cellpadding="2" cellspacing="2" width="100%" style="border:4px solid #444">
      <tr>
        <td class="style10">
          
            <asp:Label ID="Label45" runat="server" Text="Tagliando (data):" 
                CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style13">
          
            <asp:Label ID="lb_tagliando_data" runat="server" Text='' CssClass="testo"></asp:Label>
          
        </td>
        <td class="style11">
          
            <asp:Label ID="Label49" runat="server" Text="Tagliando (Km):" 
                CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style15">
          
            <asp:Label ID="lb_tagliando_km" runat="server" Text='' CssClass="testo"></asp:Label>
          
        </td>
        <td class="style14">
          
            <asp:Label ID="Label50" runat="server" Text="Ultimo Tagliando Effettuato (Data/Km):" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td>
          
            <asp:Label ID="lb_ultimo_tagliando" runat="server" Text='' CssClass="testo"></asp:Label>
          
          </td>
      </tr>
      <tr>
        <td class="style10">
          
            <asp:Label ID="Label46" runat="server" Text="Sostituzione gomme (Data):" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style13">
            <asp:Label ID="lb_sostituzione_gomme_data" runat="server" Text='' CssClass="testo"></asp:Label>
        </td>
        <td class="style11">
          
            <asp:Label ID="Label51" runat="server" Text="Sostituzione gomme (Km):" 
                CssClass="testo_bold"></asp:Label>
          
          </td>
        <td class="style15">
            <asp:Label ID="lb_sostituzione_gomme_km" runat="server" Text='' CssClass="testo"></asp:Label>
          </td>
        <td class="style14">
          
            <asp:Label ID="Label52" runat="server" 
                Text="Ultima sostituzione gomme (data/km):" CssClass="testo_bold"></asp:Label>
          
          </td>
        <td>
          
            <asp:Label ID="lb_ultima_sostituzione" runat="server" Text='' CssClass="testo"></asp:Label>
          
          </td>
      </tr>
      <tr>
        <td class="style10">
          
            <asp:Label ID="Label47" runat="server" Text="Vendita (Km):" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style13">
             <asp:Label ID="lb_vendita" runat="server" Text='' CssClass="testo"></asp:Label>  
        </td>
        <td class="style11">
             &nbsp;</td>
        <td class="style15">
             &nbsp;</td>
        <td class="style14">
             &nbsp;</td>
        <td>
             &nbsp;</td>
      </tr>
      <tr>
        <td class="style10">
          
            <asp:Label ID="Label48" runat="server" Text="Buy Back (Data):" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style13">
            <asp:Label ID="lb_buy_back" runat="server" Text='' CssClass="testo"></asp:Label>  
        </td>
        <td class="style11">
            &nbsp;</td>
        <td class="style15">
            &nbsp;</td>
        <td class="style14">
            &nbsp;</td>
        <td>
            &nbsp;</td>
      </tr>
     </table>
    </div>
</div>

<div id="div_gestione_rds" runat="server" visible="false" style="background-color:#c8c8c6 !Important;">


<br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b>RDS - <asp:Label ID="lb_stato_rds" runat="server" Text=''></asp:Label>
                &nbsp;<asp:Label ID="lb_num_rds" runat="server" Text=''></asp:Label>
                </b> 
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
       <tr>
        <td>
            
<div id="div1" runat="server">
<ajaxtoolkit:TabContainer ID="Tab_RDS" runat="server" ActiveTabIndex="0" 
        Width="100%">

<ajaxtoolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Gestione RDS">
    <HeaderTemplate>Gestione RDS</HeaderTemplate>
    <ContentTemplate>
    <script type="text/javascript" language="javascript">
        function apri_link(nome_link) {
            var mylink = document.getElementsByTagName(nome_link);
            if (mylink == null) {
                alert("link non trovato");
                return false;
            }
            mylink.click();
            alert("ok");
            return false;
        }
    </script>
    
<script type="text/javascript" language="javascript">

    function InitDivVisibile(valore) {
        var div_importo = document.getElementById('div_importo');
        if(div_importo == null)
            return false;
        var div_attesa = document.getElementById('div_attesa');
        if(div_attesa == null)
            return false;
        var div_non_addebito= document.getElementById('div_non_addebito');
        if(div_non_addebito == null)
            return false;

        if (valore == 2) {
            div_importo.style.display = 'none';
            div_attesa.style.display = 'none';
            div_non_addebito.style.display = '';
        } else if (valore == 3) {
            div_importo.style.display = 'none';
            div_attesa.style.display = '';
            div_non_addebito.style.display = 'none';
        } else {
            div_importo.style.display = '';
            div_attesa.style.display = 'none';
            div_non_addebito.style.display = 'none';
        }
        return false;
    }

    function OnSelectedIndexChange(Combo) {
        var valore = Combo.options[Combo.selectedIndex].value;
        InitDivVisibile(valore);

        return false;
    }

    function ModificaTotale() {
        var Combo = document.getElementById('<%= Drop_aliquote_iva.UniqueID.replace("$","_") %>');
        OnChangeIVA(Combo);
        return false;
    }

    function ArrotondaDueCifre(Valore) {
        return Math.round(Valore * 100) / 100;
    }

    function OnChangeIVA(Combo) {
        var valore = Combo.options[Combo.selectedIndex].value;
        var iva = 0;
        var MatriceIVA = new Array();
        MatriceIVA = <%= getMatriceIVA() %>;
        for(var i = 0; i < MatriceIVA.length; i++) {
            if(MatriceIVA[i][0] == valore) {
                iva = MatriceIVA[i][1];
                break;
            }
        }
        var tx_stima_rds = document.getElementById('<%= tx_stima_rds.UniqueID.replace("$","_") %>');
        var tx_spese_postali = document.getElementById('<%= tx_spese_postali.UniqueID.replace("$","_") %>');
        var tx_importo_totale = document.getElementById('<%= tx_importo_totale.UniqueID.replace("$","_") %>');
        if(tx_stima_rds == null) {
            alert("tx_stima_rds non trovato.");
            return false;
        }
        if(tx_spese_postali == null) {
            alert("tx_spese_postali non trovato.");
            return false;
        }
        if(tx_importo_totale == null) {
            alert("tx_importo_totale non trovato.");
            return false;
        }
        var stima = 0;
        if(tx_stima_rds.value != '') {
            stima = eval(tx_stima_rds.value.replace(',','.'));
        }
        var spese_postali = 0;
        if(tx_spese_postali.value != '') {
            spese_postali = eval(tx_spese_postali.value.replace(',','.'));
        }

        var stima_piu_iva = stima * (1 + iva / 100);

        var totale = stima_piu_iva + spese_postali;

        var str_valore = ArrotondaDueCifre(totale).toFixed(2) + '';

        tx_stima_rds.value = (ArrotondaDueCifre(stima).toFixed(2) + '').replace('.',',');
        tx_spese_postali.value = (ArrotondaDueCifre(spese_postali).toFixed(2) + '').replace('.',',');
        tx_importo_totale.value = str_valore.replace('.',',');

        return false;
    }

 </script>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" >
        <tr>
            <td style="width:112px">
                <asp:Label ID="lb_per_operazione" runat="server" Text="Operazione:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="DropOperazione" runat="server">
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                    <asp:ListItem Value="2">Da non addebitare</asp:ListItem>
                    <asp:ListItem Value="3">In attesa</asp:ListItem>
                    <asp:ListItem Value="4">All'attenzione</asp:ListItem>
                    <asp:ListItem Value="5">Da addebitare</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:CheckBox ID="chkDaRiparare" runat="server" Text="Da Riparare"></asp:CheckBox>
                <asp:Label ID="lb_operazione" runat="server" CssClass="testo_bold" 
                    Visible="False"></asp:Label>
            </td>
            <td align="right" style="width:50%">
                <a id="A2" href="/gestione_danni/SelezionaLetteraRDS.aspx?id_evento=<%= lb_id_evento.text %>" rel="lyteframe" 
                title="Stampa_Lettera" rev="width: 400px; height: 400px; scrolling: no;">
                <asp:Button ID="bt_stampa_lettera" runat="server" Text="Stampa Lettera" OnClientClick="javascript:return false;"/></a>
                
                <asp:Button ID="bt_stampa_RDS" runat="server" Text="Stampa RDS" />
            </td>
         </tr>
         <tr>
            <td colspan="3">
                <div id="div_non_addebito">
                <table border="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="Motivo Non Addebito:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownNonAddebito" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                </div>
                <div id="div_importo">
                <table border="0" cellpadding="0">
                    <tr runat="server" visible="False">
                        <td runat="server">
                            <asp:Label ID="Label22" runat="server" Text="Data incidente:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td runat="server">
                            <asp:TextBox ID="tx_data_incidente" runat="server" Width="70px"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_incidente" ID="CalendarExtender4"></ajaxtoolkit:CalendarExtender>
                            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date"  TargetControlID="tx_data_incidente" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender4"></ajaxtoolkit:MaskedEditExtender>&nbsp;&nbsp; 
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label27" runat="server" Text="Luogo incidente:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td runat="server">
                            <asp:TextBox ID="tx_luogo_incidente" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text="Importo:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tx_stima_rds" runat="server" Width="70px" onKeyPress="javascript:return filterInputDouble(event)" onchange="javascript:ModificaTotale(); return false;" ></asp:TextBox>&nbsp; &nbsp; 
                            <asp:Label ID="Label34" runat="server" Text="Seleziona IVA:" CssClass="testo_bold"></asp:Label>&nbsp; 
                            <asp:DropDownList ID="Drop_aliquote_iva" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_aliquote_iva" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList> &nbsp; &nbsp;
                            <asp:Label ID="Label24" runat="server" Text="Spese Apertura pratica Sinistro:" CssClass="testo_bold"></asp:Label>&nbsp; 
                            <asp:TextBox ID="tx_spese_postali" runat="server" Width="70px" onKeyPress="javascript:return filterInputDouble(event)" onchange="javascript:ModificaTotale(); return false;" ></asp:TextBox>&nbsp; &nbsp; 
                            <asp:Label ID="Label35" runat="server" Text="Totale:" CssClass="testo_bold"></asp:Label>&nbsp;
                            <asp:TextBox ID="tx_importo_totale" runat="server" Width="70px" 
                                Enabled="False"  ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label42" runat="server" Text="Data Perizia:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td colspan="3">
                            <a onclick="Calendar.show(document.getElementById('<%=tx_data_perizia.ClientID%>'), '%d/%m/%Y', false)">
                            <asp:TextBox ID="tx_data_perizia" runat="server" Width="70px"></asp:TextBox>
                                </a>
                    <%--        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_perizia" ID="CalendarExtender6"></ajaxtoolkit:CalendarExtender>
                   --%>        
                            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date"  TargetControlID="tx_data_perizia" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender7"></ajaxtoolkit:MaskedEditExtender>&nbsp;&nbsp; 
                            <asp:CheckBox ID="ck_perizia" runat="server" />
                            <asp:Label ID="Label37" runat="server" Text="Perizia effettuata" CssClass="testo_bold"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label36" runat="server" Text="Giorni Fermo Tecnico:" CssClass="testo_bold"></asp:Label>&nbsp; 
                            <asp:TextBox ID="tx_giorni_fermo_tecnico" runat="server" Width="70px" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>&nbsp; &nbsp; 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label28" runat="server" Text="Documentazione:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="ck_CID" runat="server" />
                            <asp:Label ID="Label30" runat="server" Text="CID" CssClass="testo_bold"></asp:Label>&nbsp; &nbsp; 
                            <asp:CheckBox ID="ck_denuncia" runat="server" />
                            <asp:Label ID="Label29" runat="server" Text="Denuncia o altro attestato" CssClass="testo_bold"></asp:Label>&nbsp; &nbsp; 
                            <asp:CheckBox ID="ck_fotocopia_doc" runat="server" />
                            <asp:Label ID="Label31" runat="server" Text="Fotocopia documenti" CssClass="testo_bold"></asp:Label>&nbsp; &nbsp; 
                            <asp:CheckBox ID="ck_preventivo" runat="server" />
                            <asp:Label ID="Label32" runat="server" Text="Preventivo" CssClass="testo_bold"></asp:Label>&nbsp; &nbsp; 
                            <asp:TextBox ID="tx_num_fotografie" runat="server" Width="30px" MaxLength="2"></asp:TextBox>&nbsp; 
                            <asp:Label ID="Label33" runat="server" Text="Fotografie" CssClass="testo_bold"></asp:Label>&nbsp; &nbsp; 
                        </td>
                    </tr>
                </table>
                </div>
                <div id="div_attesa">
                <table border="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text="In attesa:" CssClass="testo_bold"></asp:Label>&nbsp; 
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="Per ufficio manutenzione" CssClass="testo_bold"></asp:Label>&nbsp; 
                            <asp:DropDownList ID="Drop_manutenzione" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_manutenzione" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;
                            <asp:Label ID="Label21" runat="server" Text="Per documentazione" CssClass="testo_bold"></asp:Label>
                            <asp:DropDownList ID="Drop_documenti" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_documenti" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                </div>
                <div id="div_pulsanti_sinistro" runat="server" visible="False">
                    <div id="div_nuovo_sinistro" runat="server">
                         <table border="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Button ID="bt_sinistro_attivo" runat="server" Text="Sinistro Attivo"/>
                                    <asp:Button ID="bt_sinistro_passivo" runat="server" Text="Sinistro Passivo"/>
                                    
                                </td>
                            </tr>
                        </table>   
                    </div>
                    <div id="div_gestione_sinistro" runat="server">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td rowspan="2" style="vertical-align:top">
                                    <asp:Button ID="bt_gestione_sinistri" runat="server" Text="Gestione Sinistri"/>&nbsp;&nbsp;
                                </td>
                                <td>
                                    Sinistro dell'Anno:
                                    <asp:Label ID="lb_anno" runat="server" CssClass="testo_bold" />&nbsp; 
                                    Protocollo Interno:
                                    <asp:Label ID="lb_numero_protocollo_interno" runat="server" CssClass="testo_bold" />&nbsp;
                                    Numero Sinistro:
                                    <asp:Label ID="lb_numero_pratica" runat="server" CssClass="testo_bold" />&nbsp;
                                    Proprietario:
                                    <asp:Label ID="lb_proprietario_veicolo" runat="server" CssClass="testo_bold" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Stato:
                                    <asp:Label ID="lb_stato_sinistro" runat="server" CssClass="testo_bold" />&nbsp; 
                                    Tipologia:
                                    <asp:Label ID="lb_tipologia_sinistro" runat="server" CssClass="testo_bold" />&nbsp;
                                </td>
                            </tr>

                        </table>
                    </div>
                
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="bt_salva_rds" runat="server" Text="Salva" 
                    ValidationGroup="SalvaDaLavorare" UseSubmitBehavior="False"/>
                <asp:Button ID="bt_chiudi_rds" runat="server" Text="Chiudi" 
                    EnableViewState="False"/>
            </td>
        </tr>
    </table>
    </ContentTemplate>
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Storico variazioni">
    <HeaderTemplate>Storico variazioni</HeaderTemplate>
    <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table8">
    <tr id="Tr7" runat="server"><td id="Td8" runat="server">
        <asp:ListView ID="listViewStoricoVariazioni" runat="server" 
              DataSourceID="sqlStoricoRDS" DataKeyNames="id" EnableModelValidation="True">
        <ItemTemplate>
        <tr style="background-color:#DCDCDC; color: #000000;">
            <td>
                <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id") %>' Visible="false"/>
                <asp:Label ID="lb_data_creazione" runat="server" Text='<%# Eval("data_creazione") %>'/>
            </td>
            <td>
                <asp:Label ID="lb_id_stato_precedente" runat="server" Text='<%# Eval("id_stato_precedente") %>' Visible="false"/>
                <asp:Label ID="lb_stato_old" runat="server" Text='<%# Eval("stato_old") %>' />
            </td>
            <td>
                <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("id_stato") %>' Visible="false"/>
                <asp:Label ID="lb_stato_new" runat="server" Text='<%# Eval("stato_new") %>' />
            </td>
            <td>
                <asp:Label ID="lb_importo" runat="server" Text='<%# Eval("importo") %>' />
            </td>
            <td>
                <asp:Label ID="lb_des_iva" runat="server" Text='<%# Eval("des_iva") %>' />
            </td>
            <td>
                <asp:Label ID="lb_spese_postali" runat="server" Text='<%# Eval("spese_postali") %>' />
            </td>
            <td>
                <asp:Label ID="lb_incasso" runat="server" Text='<%# Eval("incasso") %>' />
            </td>
            <td>
                <asp:Label ID="lb_att_manutenzione" runat="server" Text='<%# Eval("att_manutenzione") %>' Visible="false" />
                <asp:Label ID="lb_des_att_manutenzione" runat="server" Text='<%# Eval("des_att_manutenzione") %>' />
            </td>
            <td>
                <asp:Label ID="lb_att_documentazione" runat="server" Text='<%# Eval("att_documentazione") %>' Visible="false" />
                <asp:Label ID="lb_des_att_documentazione" runat="server" Text='<%# Eval("des_att_documentazione") %>' />
            </td>
            <td>
                <asp:Label ID="lb_operatore" runat="server" Text='<%# Eval("operatore") %>' />
            </td>
        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr style="">
            <td>
                <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id") %>' Visible="false"/>
                <asp:Label ID="lb_data_creazione" runat="server" Text='<%# Eval("data_creazione") %>'/>
            </td>
            <td>
                <asp:Label ID="lb_id_stato_precedente" runat="server" Text='<%# Eval("id_stato_precedente") %>' Visible="false"/>
                <asp:Label ID="lb_stato_old" runat="server" Text='<%# Eval("stato_old") %>' />
            </td>
            <td>
                <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("id_stato") %>' Visible="false"/>
                <asp:Label ID="lb_stato_new" runat="server" Text='<%# Eval("stato_new") %>' />
            </td>
            <td>
                <asp:Label ID="lb_importo" runat="server" Text='<%# Eval("importo") %>' />
            </td>
            <td>
                <asp:Label ID="lb_des_iva" runat="server" Text='<%# Eval("des_iva") %>' />
            </td>
            <td>
                <asp:Label ID="lb_spese_postali" runat="server" Text='<%# Eval("spese_postali") %>' />
            </td>
            <td>
                <asp:Label ID="lb_incasso" runat="server" Text='<%# Eval("incasso") %>' />
            </td>
            <td>
                <asp:Label ID="lb_att_manutenzione" runat="server" Text='<%# Eval("att_manutenzione") %>' Visible="false" />
                <asp:Label ID="lb_des_att_manutenzione" runat="server" Text='<%# Eval("des_att_manutenzione") %>' />
            </td>
            <td>
                <asp:Label ID="lb_att_documentazione" runat="server" Text='<%# Eval("att_documentazione") %>' Visible="false" />
                <asp:Label ID="lb_des_att_documentazione" runat="server" Text='<%# Eval("des_att_documentazione") %>' />
            </td>
            <td>
                <asp:Label ID="lb_operatore" runat="server" Text='<%# Eval("operatore") %>' />
            </td>
        </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr style=""><td>Nessuna variazione presente. </td></tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table id="Table1" runat="server" width="100%"><tr id="Tr1" runat="server">
            <td id="Td1" runat="server">
                <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                    <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                        <th >Data</th>
                        <th >Stato Prec.</th>
                        <th >Stato Nuovo</th>
                        <th >Importo</th>
                        <th >IVA</th>
                        <th >Sp. Post.</th>
                        <th >Incasso</th>
                        <th >Man</th>
                        <th >Doc</th>
                        <th >Operatore</th>
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
    </ContentTemplate>
    </ajaxtoolkit:TabPanel>
</ajaxtoolkit:TabContainer>

</div>
        </td>
       </tr>
    </table>
    
</div>

<div runat="server" id="tab_dettagli_pagamento" visible="false" style="background-color:#c8c8c6 !Important;">
<uc1:DettagliPagamento id="DettagliPagamento" runat="server" />
</div>

<div id="div_nota" runat="server" visible="false" style="background-color:#c8c8c6 !Important;">
    <br />
    <div>
        <uc1:gestione_note id="gestione_note" runat="server" />
    </div>
</div>

<asp:Label ID="lb_stato_form" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_id_veicolo" runat="server" Text='0' Visible="true" />
<asp:Label ID="lb_stato_danno" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_flag" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_ditta" runat="server" Text='0' Visible="false" />


<asp:Label ID="lb_flag_richiede_id" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_evento" runat="server" Text='0' visible="true"></asp:Label>

<asp:Label ID="lb_id_gruppo_evento" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_immagine_default" runat="server" Text='1' visible="false"></asp:Label>

<asp:Label ID="lb_id_tipo_documento_apertura" runat="server" Text="" visible="true"></asp:Label>
<asp:Label ID="lb_id_documento_apertura" runat="server" Text="" visible="false"></asp:Label>
<asp:Label ID="lb_num_crv" runat="server" Text="" visible="false"></asp:Label>
<asp:Label ID="txtIdEventoApertura" runat="server" Text="" visible="true"></asp:Label>

<asp:Label ID="lb_num_prenotazione" runat="server" Text="" visible="false"></asp:Label>

<asp:Label ID="lb_evento_modificato" runat="server" Text='' visible="false"></asp:Label>

<asp:Label ID="lb_id_stato_rds" runat="server" Text='0' visible="false"></asp:Label>


<asp:Label ID="lb_th_da_addebitare" runat="server" Text='false' visible="false"></asp:Label>
<asp:Label ID="lb_th_lente" runat="server" Text='false' visible="false"></asp:Label>

<asp:Label ID="lb_th_lente_storico" runat="server" Text='true' visible="false"></asp:Label>


<asp:Label ID="livello_accesso_dettaglio_pos" runat="server" Visible="false"></asp:Label>



<asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (codice + ' - ' + nome_stazione) descrizione FROM [stazioni] WITH(NOLOCK) WHERE [attiva] = 1 ORDER BY [codice]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDanno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ed.id id_evento, ed.id_veicolo, ed.id_tipo_documento_apertura, tda.descrizione des_id_tipo_documento_apertura, ed.id_documento_apertura, 
            ed.nota, CONVERT(char(10), ed.data, 103) As data_creazione,
            d.id id_danno, d.stato, d.id_posizione_danno, pd.descrizione des_id_posizione_danno, d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno
            FROM veicoli_evento_apertura_danno ed WITH(NOLOCK)
            INNER JOIN veicoli_tipo_documento_apertura_danno tda WITH(NOLOCK) ON tda.id = ed.id_tipo_documento_apertura
            LEFT JOIN veicoli_danni d WITH(NOLOCK) ON ed.id = d.id_evento_apertura
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            WHERE ed.id_veicolo = @id_veicolo
            AND (0 = @id_flag OR d.stato = @stato_danno)
            ORDER BY ed.id DESC, d.id DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_veicolo" Name="id_veicolo" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_id_flag" Name="id_flag" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_stato_danno" Name="stato_danno" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlElencoDanniPerEvento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT d.id id_danno, d.tipo_record, d.stato, d.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno, d.da_addebitare,
            a.id id_dotazione, a.descrizione des_dotazione, ce.id id_acessori, ce.descrizione des_acessori, SUBSTRING(d.descrizione, 1, 30) + '...' descrizione_danno
            FROM veicoli_danni d WITH(NOLOCK)
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            LEFT JOIN accessori a WITH(NOLOCK) ON d.id_dotazione = a.id
            LEFT JOIN condizioni_elementi ce WITH(NOLOCK) ON d.id_acessori = ce.id
            WHERE d.id_evento_apertura = @id_evento
            ORDER BY d.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDanniMappati_F" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice,
            gd.id_danno, gd.id_evento_apertura, gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            gd.id_tipo_danno, td.descrizione des_id_tipo_danno, gd.entita_danno
            FROM veicoli_gruppo_evento ge WITH(NOLOCK) 
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record = 1 AND ge.id = @id_gruppo_evento
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno
            LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello
            INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello,@immagine_default) AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno
            ORDER BY CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_immagine_default" Name="immagine_default" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDanniMappati_R" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice,
            gd.id_danno, gd.id_evento_apertura, gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            gd.id_tipo_danno, td.descrizione des_id_tipo_danno, gd.entita_danno
            FROM veicoli_gruppo_evento ge WITH(NOLOCK) 
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record = 1 AND ge.id = @id_gruppo_evento
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno
            LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello
            INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello,@immagine_default) AND im.tipo_img = 2 AND gd.id_posizione_danno = im.id_posizione_danno
            ORDER BY CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_immagine_default" Name="immagine_default" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_meccanici_elettrici" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gd.id_danno, gd.id_evento_apertura, gd.tipo_record, SUBSTRING(gd.descrizione, 1, 40) + '...' descrizione
            FROM veicoli_gruppo_evento ge WITH(NOLOCK) 
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record IN (2, 3, 7) AND ge.id = @id_gruppo_evento
            ORDER BY gd.tipo_record, gd.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_dotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ga.id, ga.id_evento_apertura, ga.id_veicolo, ga.id_accessorio, ga.assente, a.descrizione, a.descrizione_ing
            FROM veicoli_gruppo_accessori ga WITH(NOLOCK) 
            INNER JOIN accessori a WITH(NOLOCK) ON a.id = ga.id_accessorio
            WHERE ga.id_evento_apertura = @id_gruppo_evento
            ORDER BY ga.assente, a.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_acessori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ga.id, ga.id_evento_apertura, ga.id_veicolo, ga.id_accessorio, ga.assente, ce.descrizione
            FROM veicoli_gruppo_accessori_contratto ga WITH(NOLOCK) 
            INNER JOIN condizioni_elementi ce WITH(NOLOCK) ON ga.id_accessorio = ce.id 
            WHERE ga.id_evento_apertura = @id_gruppo_evento
            ORDER BY ga.assente, ce.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:Label ID="lb_sqlElencoAuto" runat="server" Text='' Visible="false" />

<asp:SqlDataSource ID="sqlElencoAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_tipo_documento_apertura_danno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] 
            FROM [veicoli_tipo_documento_apertura_danno] WITH(NOLOCK) 
            WHERE (richiede_id = 0 OR 1 = @flag_richiede_id)
            ORDER BY [descrizione]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_flag_richiede_id" Name="flag_richiede_id" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDocumenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT vdf.*, td.descrizione des_tipo_documento
            FROM [veicoli_danni_evento_foto] vdf WITH(NOLOCK) 
            INNER JOIN veicoli_danni_img_tipo_documenti td WITH(NOLOCK) ON td.id = vdf.tipo_documento 
            WHERE vdf.id_evento = @id_evento ORDER BY td.ordine, vdf.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="sqlStoricoRDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT r.*, d1.descrizione stato_old, d2.descrizione stato_new, i.descrizione des_iva, 
            man.descrizione des_att_manutenzione, doc.descrizione des_att_documentazione,
            o.cognome + ' ' + o.nome operatore
            FROM [veicoli_stato_rds_variazione] r WITH(NOLOCK) 
            INNER JOIN operatori o WITH(NOLOCK) ON r.id_utente = o.id
            LEFT JOIN veicoli_stato_rds d1 WITH(NOLOCK) ON r.id_stato_precedente = d1.id
            LEFT JOIN veicoli_stato_rds d2 WITH(NOLOCK) ON r.id_stato = d2.id
            LEFT JOIN aliquote_iva i WITH(NOLOCK) ON r.iva = i.id 
            LEFT JOIN veicoli_tipo_attesa man WITH(NOLOCK) ON r.att_manutenzione = man.id 
            LEFT JOIN veicoli_danni_img_tipo_documenti doc WITH(NOLOCK) ON r.att_documentazione = doc.id 
            WHERE r.id_evento = @id_evento 
            ORDER BY r.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT POS_Funzioni.funzione, PAGAMENTI_EXTRA.id_pos_funzioni_ares, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.operazione_stornata, (CASE WHEN N_CONTRATTO_RIF IS NULL THEN 'PREN ' ELSE 'CNT ' END) AS provenienza FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id WHERE (N_RDS_RIF = @N_RDS_RIF)">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_num_rds" Name="N_RDS_RIF" 
                PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
        </SelectParameters>
    </asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_aliquote_iva" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM aliquote_iva">
</asp:SqlDataSource>


<asp:SqlDataSource ID="sqlTipoDocumentoImg" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_danni_img_tipo_documenti] WITH(NOLOCK) WHERE tipo = 2 ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_manutenzione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_tipo_attesa] WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_documenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_danni_img_tipo_documenti] WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_non_addebito" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_motivo_non_addebito_rds] WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="EditEvento" />

<asp:CompareValidator ID="CompareValidator3" runat="server" 
    ControlToValidate="DropDownTipoEventoAperturaDanno" ErrorMessage="Specificare l'evento che ha generato il danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="EditEvento" > </asp:CompareValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_data_evento" ErrorMessage="Specificare la data dell'evento che ha generato il danno." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="EditEvento"></asp:RequiredFieldValidator> 


<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="SalvaCheckIn" />

<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="DropDownSerbatoioRientro" ErrorMessage="Specificare la quantità di carburante presente al check in."
    Type="Integer" Operator="GreaterThan" ValueToCompare="-1"
    Font-Size="0pt" ValidationGroup="SalvaCheckIn" > </asp:CompareValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
    ControlToValidate="tx_km_rientro" ErrorMessage="Specificare i km di rientro presenti al check in." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaCheckIn" ></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidatorKmRientro" runat="server" 
    ControlToValidate="tx_km_rientro" ErrorMessage="Specificare un valore corretto per i km di rientro presenti al check in."
    Type="Integer" Operator="GreaterThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaCheckIn"></asp:CompareValidator>


<asp:ValidationSummary ID="ValidationSummary5" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="SalvaFurto" />

<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropDownTipoEventoAperturaDanno" ErrorMessage="Specificare l'evento che ha generato il danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="SalvaFurto" > </asp:CompareValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
    ControlToValidate="tx_data_evento" ErrorMessage="Specificare la data dell'evento che ha generato il danno." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaFurto"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
    ControlToValidate="tx_data_denuncia_furto" ErrorMessage="Specificare la data della denuncia del furto." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaFurto"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
    ControlToValidate="tx_ora_denuncia_furto" ErrorMessage="Specificare l'ora della denuncia del furto." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaFurto"></asp:RequiredFieldValidator>



