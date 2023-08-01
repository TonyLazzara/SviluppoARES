<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RicercaMulte.ascx.vb" Inherits="gestione_multe_RicercaMulte" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link rel="StyleSheet" type="text/css" href="../css/style.css" /> 
<style type="text/css">
    .style1
    {
        width: 349px;
    }
    .style2
    {
        width: 92px;
    }
    .style3
    {
        width: 93px;
    }
    .style4
    {
        height: 11px;
    }
</style>
<table border="0" cellpadding="0" cellspacing="0" width="1024px" runat="server" id="tab_titolo">
  <tr>
    <td align="left" style="color: #FFFFFF" bgcolor="#2E6D54" class="testo_bold_bianco" >
      <b>&nbsp;Ricerca Multe</b>
    </td>
  </tr>
</table>

<div id="CercaMulte" runat="server" visible="true">
<table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #2E6D54;" 
    border="0" runat="server" id="table1" class="testo_bold_nero">
  <tr align="center">
    <td style="width:5%">
    &nbsp;</td>
    <td style="width:7%">
      <asp:Label ID="lblID" runat="server" Text="ID"></asp:Label>
    </td>
    <td style="width:8%">
      <asp:Label ID="lblProt" runat="server" Text="Num.Prot."></asp:Label>
    </td>
    <td style="width:7%">
        <asp:Label ID="lblAnno" runat="server" Text="Anno"></asp:Label>
    </td>
    <td style="width:14%">
        <asp:Label ID="lblDataIns" runat="server" Text="Data Inserimento"></asp:Label>
    </td>
    <td style="width:14%">
        <asp:Label ID="lblNumVerb" runat="server" Text="Numero Verbale"></asp:Label>
    </td>
    <td style="width:14%">
        <asp:Label ID="lblDataNotifica" runat="server" Text="Data Notifica"></asp:Label>
    </td>
    <td style="width:9%">
        <asp:Label ID="lblTarga" runat="server" Text="Targa"></asp:Label>
    </td>
    <td style="width:14%">
        <asp:Label ID="lblDataInfraz" runat="server" Text="Data Infrazione"></asp:Label>
    </td>
    <td style="width:8%">
        <asp:Label ID="lblOraInfraz" runat="server" Text="Ora"></asp:Label>
    </td>
  </tr>
  <tr align="center">
    <td>DA:</td>
    <td>
      <asp:TextBox ID="txtDaID" runat="server" Width="50px" MaxLength="7" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
      <asp:TextBox ID="txtDaProt" runat="server" Width="50px" MaxLength="7" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
      <asp:TextBox ID="txtDaAnno" runat="server" Width="40px" MaxLength="4" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
        <a onclick="Calendar.show(document.getElementById('<%=txtDaDataInser.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtDaDataInser" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
       <%-- <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaDataInser" ID="CalendarExtender5">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaDataInser" ID="MaskedEditExtender7">
        </asp:MaskedEditExtender>
    </td>
    <td>
      <asp:TextBox ID="txtDaNumVerb" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
    </td>
    <td>
        <a onclick="Calendar.show(document.getElementById('<%=txtDaDataNotifica.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtDaDataNotifica" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
       <%-- <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaDataNotifica" ID="CalendarExtender2">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaDataNotifica" ID="MaskedEditExtender2">
        </asp:MaskedEditExtender>
    </td>
    <td>
      <asp:TextBox ID="txtDaTarga" runat="server" Width="80px" MaxLength="9"></asp:TextBox>
    </td>
    <td>
        <a onclick="Calendar.show(document.getElementById('<%=txtDaDataInfraz.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtDaDataInfraz" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
       <%-- <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaDataInfraz" ID="CalendarExtender1">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaDataInfraz" ID="MaskedEditExtender1">
        </asp:MaskedEditExtender>
    </td>
    <td>
      <asp:TextBox ID="txtDaOraInfraz" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
        <asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaOraInfraz" ID="MaskedEditExtender3">
        </asp:MaskedEditExtender>
    </td>
    </tr>
    <tr align="center">
      <td>A:</td>
    <td>
      <asp:TextBox ID="txtAID" runat="server" Width="50px" MaxLength="7" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
      <asp:TextBox ID="txtAProt" runat="server" Width="50px" MaxLength="7" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
      <asp:TextBox ID="txtAAnno" runat="server" Width="40px" MaxLength="4" onKeyPress="return filterInput(event)"></asp:TextBox>
    </td>
    <td>
          <a onclick="Calendar.show(document.getElementById('<%=txtADataInser.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtADataInser" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
       <%-- <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtADataInser" ID="CalendarExtender6">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtADataInser" ID="MaskedEditExtender8">
        </asp:MaskedEditExtender>
    </td>    
    <td>
      <asp:TextBox ID="txtANumVerb" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
    </td>
    <td>
         <a onclick="Calendar.show(document.getElementById('<%=txtADataNotifica.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtADataNotifica" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
        <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtADataNotifica" ID="CalendarExtender3">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtADataNotifica" ID="MaskedEditExtender5">
        </asp:MaskedEditExtender>
    </td>
    <td>
      <asp:TextBox ID="txtATarga" runat="server" Width="80px" MaxLength="9"></asp:TextBox>
    </td>
    <td>
        <a onclick="Calendar.show(document.getElementById('<%=txtADataInfraz.ClientID%>'), '%d/%m/%Y', false)">
      <asp:TextBox ID="txtADataInfraz" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
       <%-- <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtADataInfraz" ID="CalendarExtender4">
        </asp:CalendarExtender>--%>
        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtADataInfraz" ID="MaskedEditExtender6">
        </asp:MaskedEditExtender>
    </td>
    <td>
      <asp:TextBox ID="txtAOraInfraz" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
        <asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" 
            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
            CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAOraInfraz" ID="MaskedEditExtender4">
        </asp:MaskedEditExtender>
    </td>
  </tr>
  <tr>
    <td colspan="10">
        <hr style="clear:both; color:#369061;" />
        <table>
          <tr> 
            <td align="center">
              <asp:Label ID="lblStatoMulta" runat="server" Text="Stato:"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblCasistiche" runat="server" Text="Casistica:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblIncassate" runat="server" Text="Incassate:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblMancaInc" runat="server" Text="Ente"></asp:Label> <%--Text="Manc.inc:" modifica 13.05.2022--%>
            </td>
            <td align="center">
              <asp:Label ID="lblFatturate" runat="server" Text="Fatturate:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblRicorsoFatto" runat="server" Text="Ricorso:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblComunicFatta" runat="server" Text="Comun.cliente:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblFaxFatto" runat="server" Text="Provenienza:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblConFattureLoc" runat="server" Text="Con fattura:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblRimborsate" runat="server" Text="Rimborsate:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblPagate" runat="server" Text="Pagate:"></asp:Label>
            </td>
            <td align="center">
              <asp:Label ID="lblOperatori" runat="server" Text="Operatori:"></asp:Label>
            </td>
          </tr>
          <tr> 
            <td>
                   <asp:DropDownList ID="DropStatoMulta" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Aperte</asp:ListItem>
                    <asp:ListItem Value="0">Chiuse</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                    <asp:DropDownList ID="DropCasistiche" runat="server" AppendDataBoundItems="True"
                          DataSourceID="sqlCasistiche" DataTextField="Casistica" DataValueField="ID" Width="80px">
                            <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropIncassate" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Inc.</asp:ListItem>
                    <asp:ListItem Value="0">Non inc.</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                    <asp:DropDownList ID="DropTipoMancInc" runat="server" AppendDataBoundItems="True"
                          DataSourceID="SqlTipoMancIncMulte" DataTextField="DescrMancIncasso" DataValueField="Id" Width="80px">
                            <asp:ListItem Selected="True" Value="0">Selez.</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropFatturate" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Fatt.</asp:ListItem>
                    <asp:ListItem Value="0">Non fatt.</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropRicorso" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Fatto</asp:ListItem>
                    <asp:ListItem Value="0">Non fatto</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropComunCliente" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Fatta</asp:ListItem>
                    <asp:ListItem Value="0">Non fatta</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropFaxFatto" runat="server" Visible="false">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Fatto</asp:ListItem>
                    <asp:ListItem Value="0">Non fatto</asp:ListItem>
                   </asp:DropDownList>

                 <asp:DropDownList ID="ddl_provenienza" runat="server" AppendDataBoundItems="True"
                          DataSourceID="SqlProvenienza" DataTextField="provenienza" DataValueField="Id" Width="80px">
                            <asp:ListItem Selected="True" Value="0">Selez.</asp:ListItem>
                    </asp:DropDownList>




            </td>
            <td>
                   <asp:DropDownList ID="DropFattLocat" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Con fatt.</asp:ListItem>
                    <asp:ListItem Value="0">Senza fatt.</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropRimborsate" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Rimb.</asp:ListItem>
                    <asp:ListItem Value="0">Non rimb.</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                   <asp:DropDownList ID="DropPagate" runat="server">
                    <asp:ListItem Value="-1" Selected="True">Tutte</asp:ListItem>
                    <asp:ListItem Value="1">Pagate</asp:ListItem>
                    <asp:ListItem Value="0">Non pag.</asp:ListItem>
                   </asp:DropDownList>
            </td>
            <td>
                    <asp:DropDownList ID="DropOperatori" runat="server" AppendDataBoundItems="True"
                          DataSourceID="sqlOperatori" DataTextField="username" DataValueField="id" Width="70px">
                            <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                    </asp:DropDownList>
            </td>
          </tr>
        </table>
  </td>
  </tr>
  <tr>
    <td colspan="10">
        <table>
          <tr> 
            <td style="width:15%">
                <asp:Image ID="ImageReports" runat="server" ImageUrl="~/images/print.ico" 
                    Height="25px" Width="25px" />
                <asp:LinkButton ID="linkReportStandard" runat="server" Text="Report Standard" ForeColor="#2E6D54"></asp:LinkButton>
            </td>
            <td style="width:15%">
                <asp:Image ID="ImageReports2" runat="server" ImageUrl="~/images/print.ico" 
                    Height="25px" Width="25px" />
                <asp:LinkButton ID="LinkReportAltriDati" runat="server" Text="Report altri dati" ForeColor="#2E6D54"></asp:LinkButton>
            </td>
            <td style="width:5%">&nbsp;</td>
            <td style="width:10%">
              <asp:Button ID="btnCerca" runat="server" Text="Cerca" style="height: 26px" />
            </td>
            <td style="width:20%">
                <asp:Button ID="btnRipulisciCampi" runat="server" Text="Ripulisci campi" />
            </td>
            <td style="width:25%">
                <asp:Button ID="btnGenProtVuoti" runat="server" Text="Genera Prot.vuoti" />
                <asp:TextBox ID="txtNumProt" runat="server" Width="20px" MaxLength="2" onKeyPress="return filterInput(event)"></asp:TextBox>
            </td>
            <td align="right" style="width:10%">
              <asp:Button ID="btnNuovaMulta" runat="server" Text="Nuova multa" CommandName="Nuovamulta"/>
            </td>
          </tr>
        </table>
  </td>
  </tr>
 </table>
</div>
<br />
<div id="risultati" runat="server" visible="true">
<table cellpadding="0" cellspacing="2" width="1024px" style="font-size:small;" border="0" runat="server" id="table2">
  <tr>
    <td>
        <asp:ListView ID="ListViewMulte" runat="server" DataSourceID="sqlElencoMulte" DataKeyNames="ID">
            <ItemTemplate>
                <tr style="background-color:#DCDCDC; color: #000000;">
                    <td>
                        <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumVerbale" runat="server" Width="140px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataNotifica" runat="server" Width="100px" Text='<%# IIf(Eval("DataNotifica") Is Nothing, "", Eval("DataNotifica", "{0:dd/MM/yyyy}"))  %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTarga" runat="server" Width="80px" Text='<%# Eval("Targa") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMulteAperte" runat="server" Width="100px" Checked='<%# not(Eval("StatoAperto")) %>' 
                            Text='<%# IIF (Eval("StatoAperto")= "true", "Aperta", "Chiusa") %>' />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMulta"/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumVerbale" runat="server" Width="140px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataNotifica" runat="server" Width="100px" Text='<%# IIf(Eval("DataNotifica") Is Nothing, "", Eval("DataNotifica", "{0:dd/MM/yyyy}"))  %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTarga" runat="server" Width="80px" Text='<%# Eval("Targa") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMulteAperte" runat="server" Width="100px" Checked='<%# not(Eval("StatoAperto")) %>'
                            Text='<%# IIF (Eval("StatoAperto")= "true", "Aperta", "Chiusa") %>' />

                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMulta"/>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>
                        Non vi sono movimenti con gli attuali criteri di ricerca.
                    </td>
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
                                    <th id="Th1" runat="server">
                                            Id</th>
                                        <th id="Th2" runat="server">
                                            Prot.</th>
                                        <th id="Th3" runat="server">
                                            Anno</th>
                                        <th id="Th4" runat="server">
                                            Numero Verbale</th>
                                        <th id="Th5" runat="server">
                                            Data Multa</th>
                                        <th id="Th6" runat="server">
                                            Data Notifica</th>
                                        <th id="Th7" runat="server">
                                            Importo</th>
                                        <th id="Th8" runat="server">
                                            Targa</th>
                                        <th id="Th9" runat="server">
                                            Stato Multa</th>
                                        <th id="Th10" runat="server">
                                            </th>
                                </tr>
                                <tr ID="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td2" runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                            ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                            ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumVerbale" runat="server" Width="70px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDataNotifica" runat="server" Width="140px" Text='<%# IIF (Eval("DataNotifica")is nothing, "", Eval("DataNotifica"))  %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTarga" runat="server" Width="80px" Text='<%# Eval("Targa") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMulteAperte" runat="server" Width="100px" Checked='<%# not(Eval("StatoAperto")) %>' 
                            Text='<%# IIF (Eval("StatoAperto")= "true", "Aperta", "Chiusa") %>' />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMulta"/>
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
    </td>
  </tr>
</table>
</div>
<br />
<asp:SqlDataSource ID="sqlElencoMulte" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [id], [Prot], [Anno], [NumVerbale], [DataInfrazione], [DataNotifica], [MultaImporto], [Targa], [StatoAperto] FROM [multe] WHERE ID = 0">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOperatori" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [id], [username] FROM [operatori]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlCasistiche" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [ID], [Casistica] FROM [multe_casistiche]">
</asp:SqlDataSource>
<%--<asp:SqlDataSource ID="SqlTipoMancIncMulte" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [Id], [DescrMancIncasso] FROM [multe_TipoMancIncMulte]">
</asp:SqlDataSource>--%>
<asp:SqlDataSource ID="SqlTipoMancIncMulte" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [ID],[Ente] as DescrMancIncasso FROM [Autonoleggio_SRC].[dbo].[multe_enti] order by ente">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlProvenienza" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [ID],[Provenienza] FROM [multe_provenienza] order by provenienza">
</asp:SqlDataSource>