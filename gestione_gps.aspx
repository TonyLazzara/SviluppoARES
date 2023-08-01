<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_gps.aspx.vb" Inherits="gestione_gps" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" /> 
    <style type="text/css">
        .style1
        {
            width: 108px;
        }
        .style2
        {
        }
        .style4
        {
        }
        .style8
        {
            width: 135px;
        }
        .style12
        {
            width: 126px;
        }
        .style15
        {
            width: 114px;
        }
        .style19
        {
            width: 110px;
        }
        .style20
        {
            width: 106px;
        }
        .style21
        {
            width: 117px;
        }
        .style22
        {
            width: 90px;
        }
        .style23
        {
            width: 134px;
        }
          tr{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" runat="server" id="tab_titolo">
  <tr>
    <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
      <b>&nbsp;GPS</b>
    </td>
  </tr>
</table>

<div id="cerca_gps" runat="server" visible="true">
 <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0" runat="server" id="table1">
  <tr>
    <td class="style23">
      <asp:Label ID="lblCercaGps" runat="server" Text="Codice:" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style22">
      <asp:TextBox ID="txtCercaCodice" runat="server" Width="70px"></asp:TextBox>
    </td>
    <td class="style15">
      <asp:Label ID="lblCercaSeriale" runat="server" Text="Seriale:" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style21">
      <asp:TextBox ID="txtCercaSeriale" runat="server" Width="100px"></asp:TextBox>
    </td>
    <td class="style19">
      <asp:Label ID="lblCercaStazione" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style8">
      <asp:DropDownList ID="dropCercaStazione" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Tutte...</asp:ListItem>
      </asp:DropDownList>
    </td>
    <td class="style20">
      <asp:Label ID="lblCercaStato" runat="server" Text="Stato:" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style12">
        <asp:DropDownList ID="dropCercaStato" runat="server" AppendDataBoundItems="True" DataSourceID="sqlStatus" DataTextField="descrizione" 
            DataValueField="id">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
      
        </asp:DropDownList>      
    </td>
  </tr>
  <tr>
    <td class="style23">
      <asp:Label ID="lblDataIngresso" runat="server" Text="Da Data Ingresso:"  CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style22">

          <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataIngressoDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataIngressoDa"></asp:TextBox>
              </a>


               <%-- <asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaDataIngressoDa" 
                    ID="txtCercaDataIngressoDa_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataIngressoDa" 
                          ID="txtCercaDataIngressoDa_maskededitextender">
                </asp:maskededitextender>

     </td>
    <td class="style15">
      <asp:Label ID="lblDataIngresso1" runat="server" Text="A Data Ingresso:"  
            CssClass="testo_bold"></asp:Label>
      </td>
    <td class="style21">
         <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataIngressoA.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataIngressoA"></asp:TextBox>
             </a>

                <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaDataIngressoA" 
                    ID="txtCercaDataIngressoA_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataIngressoA" 
                          ID="txtCercaDataIngressoA_maskededitextender">
                </asp:maskededitextender>
    </td>
    <td class="style19">
      <asp:Label ID="lblDataUscita0" runat="server" Text="Da Data Uscita:"  
            CssClass="testo_bold"></asp:Label>
      </td>
    <td class="style8">
        <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataUscitaDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataUscitaDa"></asp:TextBox>
            </a>
                <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaDataUscitaDa" 
                    ID="txtCercaDataUscitaDa_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataUscitaDa" 
                          ID="txtCercaDataUscitaDa_maskededitextender">
                </asp:maskededitextender>
    </td>
    <td class="style20">
      <asp:Label ID="lblDataUscita1" runat="server" Text="A Data Uscita:"  
            CssClass="testo_bold"></asp:Label>
      </td>
    <td class="style12">
        <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataUscitaA.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataUscitaA"></asp:TextBox>
            </a>
               <%-- <asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaDataUscitaA" 
                    ID="txtCercaDataUscitaA_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataUscitaA" 
                          ID="txtCercaDataUscitaA_maskededitextender">
                </asp:maskededitextender>
    </td>
  </tr>
  <tr>
    <td class="style23">
      <asp:Label ID="lblCercaProprietario0" runat="server" Text="Proprietario:" 
            CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style4" colspan="2">
        <asp:DropDownList ID="dropCercaProprietario" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlProprietari" DataTextField="descrizione" 
            DataValueField="id">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
      
        </asp:DropDownList>      
    </td>
    <td class="style21">
        &nbsp;</td>
    <td class="style19">
        &nbsp;</td>
    <td class="style8">
        &nbsp;</td>
    <td class="style20">
        &nbsp;</td>
    <td class="style12">
        &nbsp;</td>
  </tr>
  <tr>
    <td class="style2" colspan="8" align="center">
    
        <asp:Button ID="btnCerca" runat="server" Text="Cerca" UseSubmitBehavior="False" />
&nbsp;<asp:Button ID="btnNuovo" runat="server" Text="Nuovo GPS" UseSubmitBehavior="False" />
    
    </td>
  </tr>
</table>
</div>

<div id="risultati" runat="server" visible="false">
<table cellpadding="0" cellspacing="2" width="1024px" border="0" runat="server" id="table2">
  <tr>
    <td>
      
        <asp:ListView ID="listGPS" runat="server" DataSourceID="sqlGPS" DataKeyNames="id">
            <ItemTemplate>
                <tr style="background-color:#DCDCDC; color: #000000;">
                    <td>
                        <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                        <asp:Label ID="codice" runat="server" Text='<%# Eval("codice") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="seriale" runat="server" Text='<%# Eval("seriale") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_stazione_attuale" runat="server" Text='<%# Eval("id_stazione_attuale") %>' visible="false"/>
                        <asp:Label ID="stazione" runat="server" Text='<%# Eval("stazione") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_proprietario" runat="server" Text='<%# Eval("id_proprietario") %>' visible="false"/>
                        <asp:Label ID="proprietario" runat="server" Text='<%# Eval("proprietario") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_gps_status" runat="server" Text='<%# Eval("id_gps_status") %>' visible="false"/>
                        <asp:Label ID="gps_status" runat="server" Text='<%# Eval("gps_status") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="data_ingresso_in_parco" runat="server" Text='<%# Eval("data_ingresso_in_parco") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="data_uscita_parco" runat="server" Text='<%# Eval("data_uscita_parco") %>' CssClass="testo" />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="modifica"/>
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina"/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#DCDCDC; color: #000000;">
                    <td>
                        <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                        <asp:Label ID="codice" runat="server" Text='<%# Eval("codice") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="seriale" runat="server" Text='<%# Eval("seriale") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_stazione_attuale" runat="server" Text='<%# Eval("id_stazione_attuale") %>' visible="false"/>
                        <asp:Label ID="stazione" runat="server" Text='<%# Eval("stazione") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_proprietario" runat="server" Text='<%# Eval("id_proprietario") %>' visible="false"/>
                        <asp:Label ID="proprietario" runat="server" Text='<%# Eval("proprietario") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="id_gps_status" runat="server" Text='<%# Eval("id_gps_status") %>' visible="false"/>
                        <asp:Label ID="gps_status" runat="server" Text='<%# Eval("gps_status") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="data_ingresso_in_parco" runat="server" Text='<%# Eval("data_ingresso_in_parco") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="data_uscita_parco" runat="server" Text='<%# Eval("data_uscita_parco") %>' CssClass="testo" />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="modifica"/>
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina"/>
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
                <table id="Table2" runat="server" width="100%">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                    <th id="Th1" runat="server" align="left">
                                        <asp:Label ID="gps_status" runat="server" Text='Codice' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th2" runat="server" align="left">
                                        <asp:Label ID="Label1" runat="server" Text='Seriale' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th3" runat="server" align="left">
                                        <asp:Label ID="Label2" runat="server" Text='Stazione' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th7" runat="server" align="left">
                                        <asp:Label ID="Label3" runat="server" Text='Proprietario' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th4" runat="server" align="left">
                                        <asp:Label ID="Label4" runat="server" Text='Stato' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th8" runat="server" align="left">
                                        <asp:Label ID="Label5" runat="server" Text='Data Ingresso' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th9" runat="server" align="left">
                                        <asp:Label ID="Label6" runat="server" Text='Data Uscita' CssClass="testo_titolo" />
                                    </th>
                                    <th id="Th5" runat="server" align="left">
                                    </th>
                                    <th id="Th6" runat="server" align="left">
                                    </th>
                                </tr>
                                <tr ID="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td2" runat="server" style="">
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
      
    </td>
  </tr>
</table>
<br />
</div>

<div id="nuovo_gps" runat="server" visible="false">
  <table cellpadding="0" cellspacing="2" width="1024px" border="0" runat="server" id="table3">
       <tr>
          <td>
            <asp:Label ID="lblCodice" runat="server" Text="Codice:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtCodice" runat="server" MaxLength="50" Width="120px"></asp:TextBox>
            <asp:Label ID="id_modifica" runat="server" Visible="false"></asp:Label>
          </td>
          <td>
            <asp:Label ID="lblSeriale" runat="server" Text="Seriale:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtSeriale" runat="server" MaxLength="50"></asp:TextBox>
          </td>
        </tr>
       <tr>
          <td>
            <asp:Label ID="lblStazione" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
      <asp:DropDownList ID="dropStazione" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>

      <asp:DropDownList ID="dropOldStazione" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id" Visible="false">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>
           </td>
          <td>
            <asp:Label ID="lblProprietario" runat="server" Text="Proprietario:" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
              <asp:DropDownList ID="dropProprietario" runat="server" AppendDataBoundItems="True" DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>
           </td>
        </tr>
       <tr>
          <td>
            <asp:Label ID="lblDataImmissione" runat="server" Text="Data Immissione in Parco:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtDataImmissione" runat="server" MaxLength="10" Width="75px" ReadOnly="True"></asp:TextBox>
           </td>
          <td>
              <asp:Label ID="lblDataUscita" runat="server" Text="Data Uscita dal Parco:" CssClass="testo_bold"></asp:Label></td>
          <td>
            <asp:TextBox ID="txtDataUscita" runat="server" MaxLength="10" Width="75px" ReadOnly="True"></asp:TextBox>
           </td>
        </tr>
         <tr>
          <td colspan="4" align="center">
            <asp:Button ID="btnDismissGps" runat="server" Text="Dismissione GPS" 
                  ValidationGroup="invia" UseSubmitBehavior="False" />
            <asp:Button ID="btnSalva" runat="server" Text="Salva GPS" ValidationGroup="invia" UseSubmitBehavior="False" />&nbsp;
            <asp:Button ID="btnAnnulla" runat="server" Text="Chiudi" UseSubmitBehavior="False" />
          </td>
        </tr>
  </table>
</div>
    
<asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice">
</asp:SqlDataSource>


<asp:SqlDataSource ID="sqlStatus" runat="server" 
   ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrizione FROM gps_status WITH(NOLOCK) WHERE id=1 or id=2 or id=7 ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlGPS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gps.id, gps.codice, gps.seriale, gps.id_stazione_attuale,  (stazioni.codice + ' ' + stazioni.nome_stazione) As stazione,
                              gps.id_proprietario, proprietari_veicoli.descrizione As proprietario, gps.id_gps_status, gps_status.descrizione As gps_status 
                       FROM gps WITH(NOLOCK) LEFT JOIN stazioni ON gps.id_stazione_attuale=stazioni.id 
                       INNER JOIN proprietari_veicoli WITH(NOLOCK) ON gps.id_proprietario=proprietari_veicoli.id 
                       INNER JOIN gps_status WITH(NOLOCK) ON gps.id_gps_status=gps_status.id ">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>

<asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
<asp:Label ID="livello_permesso" runat="server" Visible="false"></asp:Label>
<asp:Label ID="livello_permesso_dismissione" runat="server" Visible="false"></asp:Label>

</asp:Content>

