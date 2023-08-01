<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RiepilogoIncassi.ascx.vb" Inherits="gestione_multe_RiepilogoIncassi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<div id="DivRiepilogoIncassi" runat="server" style="background-position:center">
<table border="0" cellpadding="0" cellspacing="0" width="1024px" runat="server" id="tab_titolo">
  <tr>
    <td align="left" style="color: #FFFFFF" bgcolor="#2E6D54" class="style1">
      <b>&nbsp;Riepilogo incasso POS</b>
    </td>
  </tr>
</table>
<table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #2E6D54;" border="0" runat="server" id="table1">
    <tr align="center">
        <td align="right" style="width:10%">
            <asp:Label ID="lblControCassa" runat="server" Text="Cassa POS: "></asp:Label>
        </td>
        <td style="width:10%">
            <asp:DropDownList ID="DropControCassa" runat="server" AutoPostBack="false" 
                DataSourceID="sqlControCassa" DataTextField="Contro_cassa" DataValueField="cassa" 
                style="margin-left: 0px" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" style="width:10%">
            <asp:Label ID="lblDaDataRiepilogo" runat="server" Text="Da data: "></asp:Label>
        </td>
        <td align="left" style="width:10%">
            <asp:TextBox ID="txtDaDataRiepilogo" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaDataRiepilogo" ID="CalendarExtender1">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaDataRiepilogo" ID="MaskedEditExtender1">
                </asp:MaskedEditExtender>
        </td>
        <td align="right" style="width:10%">
            <asp:Label ID="lblADataRiepilogo" runat="server" Text="A data: "></asp:Label>
        </td>
        <td align="left" style="width:10%">
            <asp:TextBox ID="txtADataRiepilogo" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtADataRiepilogo" ID="CalendarExtender2">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtADataRiepilogo" ID="MaskedEditExtender2">
                </asp:MaskedEditExtender>
        </td>        
        <td style="width:40%">
        </td>
   </tr>
   <tr>
    <td colspan="6">
        &nbsp;
    </td>
   </tr>
   <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnStampaReport" runat="server" Text="Stampa Report" ValidationGroup="DataIncassi" /> 
        </td>
        <td align="center">
            <asp:Button ID="btnCalcolaTotale" runat="server" Text="Calcola Totale" ValidationGroup="DataIncassi"/>
        </td>
        <td>
            <asp:Label ID="lblTotaleInc" runat="server" Font-Size="Medium" Text=""></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td align="right">
            <asp:Button ID="btnChiudi" runat="server" Text="Chiudi" />
        </td>
   </tr>
</table>
</div>
<asp:Label ID="lblIdStazione" runat="server" Visible="false"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
    ControlToValidate="txtDaDataRiepilogo" ErrorMessage="Nessuna data inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DataIncassi"></asp:RequiredFieldValidator> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="txtADataRiepilogo" ErrorMessage="Nessuna data inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DataIncassi"></asp:RequiredFieldValidator>     
<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" HeaderText="ERRORI RISCONTRATI:"
    ValidationGroup="DataIncassi" />
<asp:SqlDataSource ID="sqlControCassa" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [cassa], [Contro_cassa] FROM [POS] WITH(NOLOCK) WHERE ID_stazione = @Staz">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblIdStazione" Name="Staz" 
            PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>
    
