<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="stop_sell.aspx.vb" Inherits="stop_sell" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 
    <style type="text/css">
        .style1
        {        	  
            width: 146px;
        }
        .style2
        {
            width: 10%;
        }
        .style3
        {
        }
        .style4
        {
            width: 164px;
        }
        .style5
        {
            width: 1024px;
        }
        .style6
        {
            width: 107px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<table width="1024px" cellpadding="0" cellspacing="0">
  <tr>
    <td style="color: #FFFFFF;background-color:#444;" 
                 class="style1">
      <b>&nbsp;Gestione Stop Sale</b>
      <asp:Label ID="lblNumero" runat="server" Text="" CssClass=" testo_titolo"></asp:Label>
    </td>
  </tr>
</table>
<div id="cercaStopSell" visible="true" runat="server">
<table width="1024px" cellpadding="0" cellspacing="2" style="border:4px solid #444">
  <tr>
    <td class="style5">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td class="style6">
          <asp:Label ID="lblNum" runat="server" Text="Stop Sale Num." CssClass="testo_bold"></asp:Label>
            <br />
          <asp:TextBox ID="txtNumStopSale" runat="server" Width="64px"></asp:TextBox>
        </td>
        <td class="style4">
          <asp:Label ID="lblDescrizione" runat="server" Text="Descrizione" CssClass="testo_bold"></asp:Label><br />
          <asp:TextBox ID="txtCercaStopSell" runat="server" Width="150px"></asp:TextBox>
        </td>
        <td>
          <asp:Label ID="lblData" runat="server" Text="Data" CssClass="testo_bold"></asp:Label><br />
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaPerData.ClientID%>'), '%d/%m/%Y', false)">
          <asp:TextBox ID="txtCercaPerData" runat="server" Width="80px"></asp:TextBox></a>
                  &nbsp;<asp:Button ID="btnCercaStopSall" runat="server" Text="Cerca" 
                style="margin-left: 0px" ValidationGroup="cerca_lista" />
            
          <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" 
           Format="dd/MM/yyyy" TargetControlID="txtCercaPerData">
      </asp:CalendarExtender>--%>
      <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
           Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
           TargetControlID="txtCercaPerData">
      </asp:MaskedEditExtender>
        </td>
      </tr>
    </table>
    </td>
    <td align="right">
        <asp:Button ID="btnNuovoStopSall" runat="server" Text="Nuovo Stop Sale" />
    </td>
  </tr>
</table>
<table width="1024px" >
  <tr>
    <td class="style5">
      &nbsp;
    </td>
    <td>
        &nbsp;</td></tr><tr>
    <td colspan="2" style="width:100%;">
      
        <asp:ListView ID="ListStopSall" runat="server" DataKeyNames="id" OnPagePropertiesChanging="ListStopSall_PagePropertiesChanging"
            DataSourceID="SqlStopSall" Visible="false" >
            <ItemTemplate>
                <tr style="background-color:#DCDCDC;color: #000000;" class="testo_bold">
                    <td>
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="True" />
                    </td>
                    <td>
                        <asp:Label ID="da_dataLabel" runat="server" Text='<%# Eval("da_data") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="a_dataLabel" runat="server" Text='<%# Eval("a_data") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="descrizioneLabel" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="lblDataEfficacia" runat="server" Text='<%# Eval("data_efficacia") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/lente.png" CommandName="modificaStopSell" ToolTip="Modifica" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="eliminaStopSell" ToolTip="Elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare lo stop sell?'));" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style=""  class="testo_bold">
                    <td>
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="True" />
                    </td>
                    <td>
                        <asp:Label ID="da_dataLabel" runat="server" Text='<%# Eval("da_data") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="a_dataLabel" runat="server" Text='<%# Eval("a_data") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="descrizioneLabel" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:Label ID="lblDataEfficacia" runat="server" Text='<%# Eval("data_efficacia") %>' CssClass="testo" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/lente.png" CommandName="modificaStopSell" ToolTip="Modifica" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="eliminaStopSell" ToolTip="Elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare lo stop sell?'));" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>
                            Non è stato restituito alcun dato.</td></tr></table></EmptyDataTemplate><LayoutTemplate>
                <table runat="server" width="100%">
                    <tr runat="server">
                        <td runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                    <th id="Th1" runat="server" align="left">
                                        <asp:LinkButton ID="lnkBtnNumStopSale" runat="server" CommandName="order_by_NumStopSale" CssClass="testo" Style="color:White !important;">Stop Sale Num.</asp:LinkButton>
                                        <%--<asp:Label ID="Label6" runat="server" Text="Stop Sale Num." CssClass="testo_bold" ForeColor="White"></asp:Label> --%>
                                    </th>
                                    <th runat="server" align="left">
                                        <%--<asp:Label ID="Label1" runat="server" Text="Da data" CssClass="testo_bold" ForeColor="White"></asp:Label> --%>
                                        <asp:LinkButton ID="linBtnDataDa" runat="server" CommandName="order_by_DataDa" CssClass="testo" Style="color:White !important;">Da data</asp:LinkButton>
                                    </th>
                                    <th runat="server" align="left">
                                        <%--<asp:Label ID="Label2" runat="server" Text="A data" CssClass="testo_bold" ForeColor="White"></asp:Label> --%>
                                        <asp:LinkButton ID="linBtnDataA" runat="server" CommandName="order_by_DataA" CssClass="testo" Style="color:White !important;">A data</asp:LinkButton>
                                    </th>
                                    <th runat="server" align="left">
                                        <%--<asp:Label ID="Label3" runat="server" Text="Descrizione" CssClass="testo_bold" ForeColor="White"></asp:Label>--%> 
                                        <asp:LinkButton ID="linBtnDescrizione" runat="server" CommandName="order_by_Descrizione" CssClass="testo" Style="color:White !important;">Descrizione</asp:LinkButton>
                                    </th>
                                    <th runat="server" align="left">
                                        <%--<asp:Label ID="Label4" runat="server" Text="Efficacia" CssClass="testo_bold" ForeColor="White"></asp:Label>--%>                                       
                                        <asp:LinkButton ID="linBtnEfficacia" runat="server" CommandName="order_by_Efficacia" CssClass="testo" Style="color:White !important;">Efficacia</asp:LinkButton>
                                    </th>
                                    <th runat="server">
                                    </th>
                                    <th runat="server">
                                    </th>
                                </tr>
                                <tr ID="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="20" >
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                        ShowLastPageButton="True" />
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
<div id="nuovoStopSell" visible="false" runat="server" width="1024px">
<table width="1024px" cellpadding="0" cellspacing="2" style="border:4px solid #444">
  <tr>
    <td>
      <asp:Label ID="lblDescrizioneNew" runat="server" Text="Descrizione:" CssClass="testo_bold"></asp:Label> <asp:TextBox ID="txtDescrizione" runat="server"></asp:TextBox>&nbsp;&nbsp;
      <asp:Label ID="lblStopSaleDa" runat="server" Text="Stop Sale da:" CssClass="testo_bold"></asp:Label>
        
        <a onclick="Calendar.show(document.getElementById('<%=stopDa.ClientID%>'), '%d/%m/%Y', false)">
            &nbsp;<asp:TextBox ID="stopDa" runat="server" Width="70px"></asp:TextBox>
            </a>

        <%--<asp:CalendarExtender ID="stopDa_CalendarExtender" runat="server" 
           Format="dd/MM/yyyy" TargetControlID="stopDa">
      </asp:CalendarExtender>--%>
      <asp:MaskedEditExtender ID="stopaDa_MaskedEditExtender" runat="server" 
           Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
           TargetControlID="stopDa">
      </asp:MaskedEditExtender>
      &nbsp;&nbsp;
      <asp:Label ID="lblStopSaleA" runat="server" Text="Stop Sale a:" CssClass="testo_bold"></asp:Label>

      &nbsp;
        <a onclick="Calendar.show(document.getElementById('<%=stopA.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox ID="stopA" runat="server" Width="70px"></asp:TextBox>
            </a>

       <%-- <asp:CalendarExtender ID="stopA_CalendarExtender" runat="server" 
           Format="dd/MM/yyyy" TargetControlID="stopA">
      </asp:CalendarExtender>--%>
      <asp:MaskedEditExtender ID="stopaA_MaskedEditExtender" runat="server" 
           Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
           TargetControlID="stopA">
      </asp:MaskedEditExtender>
      
    
    &nbsp;
      <asp:Label ID="lbl1" runat="server" Text="Data Efficacia:" CssClass="testo_bold"></asp:Label>
    &nbsp;
         <a onclick="Calendar.show(document.getElementById('<%=dataEfficacia.ClientID%>'), '%d/%m/%Y', false)">
            <asp:TextBox ID="dataEfficacia" runat="server" Width="70px"></asp:TextBox>
         </a>

      <%--  <asp:CalendarExtender ID="dataEfficacia_CalendarExtender" runat="server" 
           Format="dd/MM/yyyy" TargetControlID="dataEfficacia">
      </asp:CalendarExtender>--%>
      <asp:MaskedEditExtender ID="dataEfficacia_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
           TargetControlID="dataEfficacia">
      </asp:MaskedEditExtender>
      
    
    &nbsp;
                 <asp:DropDownList ID="dropOre" runat="server">
                     <asp:ListItem Selected="True" Value="00">00</asp:ListItem>
                     <asp:ListItem Value="01">01</asp:ListItem>
                     <asp:ListItem Value="02">02</asp:ListItem>
                     <asp:ListItem Value="03">03</asp:ListItem>
                     <asp:ListItem Value="04">04</asp:ListItem>
                     <asp:ListItem Value="05">05</asp:ListItem>
                     <asp:ListItem Value="06">06</asp:ListItem>
                     <asp:ListItem Value="07">07</asp:ListItem>
                     <asp:ListItem Value="08">08</asp:ListItem>
                     <asp:ListItem Value="09">09</asp:ListItem>
                     <asp:ListItem>10</asp:ListItem>
                     <asp:ListItem>11</asp:ListItem>
                     <asp:ListItem>12</asp:ListItem>
                     <asp:ListItem>13</asp:ListItem>
                     <asp:ListItem>14</asp:ListItem>
                     <asp:ListItem>15</asp:ListItem>
                     <asp:ListItem>16</asp:ListItem>
                     <asp:ListItem>17</asp:ListItem>
                     <asp:ListItem>18</asp:ListItem>
                     <asp:ListItem>19</asp:ListItem>
                     <asp:ListItem>20</asp:ListItem>
                     <asp:ListItem>21</asp:ListItem>
                     <asp:ListItem>22</asp:ListItem>
                     <asp:ListItem>23</asp:ListItem>
       </asp:DropDownList>
      
    
    &nbsp;<asp:DropDownList ID="dropMinuti" runat="server">
                     <asp:ListItem Selected="True" Value="00">00</asp:ListItem>
                     <asp:ListItem>00</asp:ListItem>
                     <asp:ListItem>05</asp:ListItem>
                     <asp:ListItem>10</asp:ListItem>
                     <asp:ListItem>15</asp:ListItem>
                     <asp:ListItem>20</asp:ListItem>
                     <asp:ListItem>25</asp:ListItem>
                     <asp:ListItem>30</asp:ListItem>
                     <asp:ListItem>35</asp:ListItem>
                     <asp:ListItem>40</asp:ListItem>
                     <asp:ListItem>45</asp:ListItem>
                     <asp:ListItem>50</asp:ListItem>
                     <asp:ListItem>55</asp:ListItem>
                     <asp:ListItem>59</asp:ListItem>
                 </asp:DropDownList>
      
    
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
          <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td colspan="2">
                  <asp:Label ID="lblStazioniStopSale" runat="server" Text="Stazioni in Stop Sale" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="right" colspan="2">
            
                <asp:RadioButtonList ID="radioSel1" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1"><span class="testo_bold">Tutti</span></asp:ListItem><asp:ListItem Value="0"><span class="testo_bold">Seleziona</span></asp:ListItem></asp:RadioButtonList></td></tr></table></td></tr><tr>
          <td>
          <table width="100%">
            <tr>
              <td valign="top" width="45%">
                <asp:ListBox ID="listStazioni" runat="server" DataSourceID="sqlStazioni" 
                    DataTextField="id_nome_stazione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />
                      
              </td>
              <td valign="top" align="center" width="10%">
                      <asp:Button ID="PassaUnoStazioni" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoStazioni" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiStazioni" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiStazioni" runat="server" Text="<<" />
                      </td>
              <td valign="top" width="45%">
                <asp:ListBox ID="listStazioniSelezionate" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />
              </td>
            </tr>
          </table>
          </td>
        </tr>
        <tr>
          <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td>
                  <asp:Label ID="lblGruppiStopSale" runat="server" Text="Gruppi in Stop Sale" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="right">
            
                <asp:RadioButtonList ID="radioSel2" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1"><span class="testo_bold">Tutti</span></asp:ListItem><asp:ListItem Value="0"><span class="testo_bold">Seleziona</span></asp:ListItem></asp:RadioButtonList>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            
          <table width="100%">
            <tr>
              <td valign="top" width="45%">
                <asp:ListBox ID="listGruppi" runat="server" DataSourceID="sqlGruppi" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br /> 
                      
              </td>
              <td valign="top" align="center" width="10%">
                      <asp:Button ID="PassaUnoGruppo" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoGruppo" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiGruppo" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiGruppo" runat="server" Text="<<" />
                      </td>
              <td valign="top" width="45%">
                <asp:ListBox ID="listGruppiSelezionati" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />
              </td>
            </tr>
          </table>
          </td>
        </tr>
        <tr>
          <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td>
                  <asp:Label ID="lblFontiStopSale" runat="server" Text="Fonti in Stop Sale" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="right">
            
                <asp:RadioButtonList ID="radioSel3" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1"><span class="testo_bold">Tutti</span></asp:ListItem><asp:ListItem Value="0"><span class="testo_bold">Seleziona</span></asp:ListItem></asp:RadioButtonList></td></tr></table></td></tr><tr>
          <td>
            
          <table width="100%">
            <tr>
              <td valign="top" width="45%">
                <asp:ListBox ID="listFonti" runat="server" DataSourceID="sqlFonti" 
                    DataTextField="descrizione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />
                      
              </td>
              <td valign="top" align="center" width="10%">
                      <asp:Button ID="PassaUnoFonte" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoFonte" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiFonte" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiFonte" runat="server" Text="<<" />
                      </td>
              <td valign="top" width="45%">
                <asp:ListBox ID="listFontiSelezionate" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />
              </td>
            </tr>
            <tr>
              <td valign="top">
                  &nbsp;</td><td valign="top" align="center" class="style2">
                      &nbsp;</td><td valign="top">
                  &nbsp;</td></tr>
            <tr>
              <td valign="top">
                  <asp:Label ID="LblValidita" runat="server" Text="Validità" CssClass="testo_bold"></asp:Label><br />
                  <asp:TextBox ID="TxtValidita" runat="server" Width="400px"></asp:TextBox>                
              </td>
              <td valign="top" align="center" class="style2">
                      &nbsp;
              </td>
              <td valign="top">
                  &nbsp;
              </td>
            </tr>
            <tr>
              <td valign="top" align="center" colspan="3">
                  <asp:Button ID="btnSalvaStopSell" runat="server" Text="Salva Stop Sale" ValidationGroup="invia_lista" />&nbsp;
                  <asp:Button ID="btnTorna" runat="server" Text="Annulla" />
              </td>
            </tr>
          </table>        
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<table>
  <tr>
    <td class="style3" width="1024px" colspan="2">
      <asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (STR(codice) + ' - ' + nome_stazione) As id_nome_stazione FROM [stazioni] WHERE attiva='1' ORDER BY codice">
      </asp:SqlDataSource>
      <asp:SqlDataSource ID="sqlGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_gruppo, cod_gruppo, attivo FROM gruppi WHERE attivo='1' ORDER BY cod_gruppo">
      </asp:SqlDataSource>
      <asp:SqlDataSource ID="sqlFonti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [clienti_tipologia] ORDER BY descrizione">
      </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlStopSall" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT CONVERT(char(10), da_data, 103) AS da_data, CONVERT(char(10), a_data, 103) AS a_data,da_data as dd, descrizione, id, data_efficacia FROM stop_sell order by dd">
        </asp:SqlDataSource>
    </td>
  </tr>
</table>
</div>
<asp:Label ID="lblNoOrderBY" runat="server" Visible="false"></asp:Label>
<asp:Label ID="idStopSell" runat="server" visible="false"></asp:Label><asp:Label ID="query" runat="server" Visible="false"></asp:Label><asp:CompareValidator ID="CompareValidator2" runat="server" 
     ControlToValidate="txtCercaPerData" 
     Font-Size="0pt"  Operator="DataTypeCheck"
     ErrorMessage="Specificare un valore corretto come criterio di ricerca." Type="Date" ValidationGroup="cerca_lista"> </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
     ControlToValidate="txtDescrizione" ErrorMessage="Specificare una descrizione" 
     Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
     ControlToValidate="stopDa" ErrorMessage="Specificare una data di inizio Stop Sale" 
     Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator14" runat="server" 
     ControlToValidate="stopDa" 
     Font-Size="0pt"  Operator="DataTypeCheck"
     ErrorMessage="Specificare un valore corretto per la data di inizio Stop Sale." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" 
     ControlToValidate="stopA" 
     Font-Size="0pt"  Operator="DataTypeCheck"
     ErrorMessage="Specificare un valore corretto per la data di fine Stop Sale." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
     ControlToValidate="stopA" ErrorMessage="Specificare una data di fine Stop Sale" 
     Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
<asp:CompareValidator id="CompareValidator4" runat="server"
     ControlToValidate="stopA"
     ControlToCompare="stopDa"
     Type= "Date"
     Operator = "GreaterThanEqual"
     ErrorMessage = "Attenzione : la data finale di Stop Sale è precedente alla data iniziale."
     ValidationGroup="invia_lista"
     Font-Size="0pt"> </asp:CompareValidator>
     
<%--<asp:CompareValidator id="CompareValidator5" runat="server"
     ControlToValidate="stopDa"
     ControlToCompare="txtDataOdierna"
     Type= "Date"
     Operator = "GreaterThanEqual"
     ErrorMessage = "Attenzione : la data di inizio Stop Sale non può essere precedente alla data odierna"
     ValidationGroup="invia_lista"
     Font-Size="0pt"> </asp:CompareValidator>--%>

<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
     ControlToValidate="dataEfficacia" ErrorMessage="Specificare una data di efficacia" 
     Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator3" runat="server" 
     ControlToValidate="dataEfficacia" 
     Font-Size="0pt"  Operator="DataTypeCheck"
     ErrorMessage="Specificare un valore corretto della Data di efficacia." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>

<%--<asp:CompareValidator id="CompareValidator6" runat="server"
     ControlToValidate="dataEfficacia"
     ControlToCompare="txtDataOdierna"
     Type= "Date"
     Operator = "GreaterThanEqual"
     ErrorMessage = "Attenzione : la data di efficacia deve essere uguale o successiva alla data odierna."
     ValidationGroup="invia_lista"
     Font-Size="0pt"> </asp:CompareValidator>     --%>
     
    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
     DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
     ValidationGroup="invia_lista" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" 
     DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
     ValidationGroup="cerca_lista" />

    <asp:Label ID="permesso_accesso" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>