<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="esporta_dati.aspx.vb" Inherits="esporta_dati" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
            <tr>
            <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
                <asp:Label ID="Label14" runat="server" Text="Estrazione dati in formato XML" CssClass="testo_titolo" style="margin-left: 10px;"></asp:Label>
            </td>
            </tr>
        </table>
    </div>

    <div>
        <table runat="server" id="tab_dati" border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;">
       <tr style="line-height: 30px;">
          <td style="width: 160px;">
             <asp:Label ID="Label1" runat="server" Text="Tipologia Fatture" CssClass="testo_bold" style="margin-left: 9px;"></asp:Label>             
          </td>
          <td style="width: 180px;">
             <asp:Label ID="Label41" runat="server" Text="Data Emissione Fattura" CssClass="testo_bold"></asp:Label>             
          </td>
          <td style="width: 420px;">
             <asp:Label ID="Label2" runat="server" Text="Ragione Sociale/Nominativo Cliente" CssClass="testo_bold"></asp:Label>             
          </td> 
          <td>
             <asp:Label ID="Label3" runat="server" Text="Numero Fattura" CssClass="testo_bold"></asp:Label>             
          </td>          
       </tr>
       <tr style="line-height: 30px;"> 
          <td>
               <asp:RadioButton ID="rdbtnNoleggi" runat="server" Text="Noleggi" 
                   GroupName="TipologiaFatture" Font-Names="Arial" Font-Bold="True" 
                   Font-Size="Small" />
               <asp:RadioButton ID="rdbtnMulte" runat="server" Text="Multe" 
                   GroupName="TipologiaFatture" Font-Names="Arial" Font-Bold="True" 
                   Font-Size="Small" />
           </td>          
          <td>
              <a onclick="Calendar.show(document.getElementById('<%=txtDataFattura.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="140px" ID="txtDataFattura" 
                    Font-Names="Arial"></asp:TextBox></a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataFattura" ID="txtDataFattura_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFattura" 
                          ID="txtDataFattura_MaskedEditExtender">
                </ajaxtoolkit:MaskedEditExtender>

                &nbsp;
              <a onclick="Calendar.show(document.getElementById('<%=txtDataFatturaA.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="140px" ID="txtDataFatturaA" 
                    Font-Names="Arial"></asp:TextBox></a>
                <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataFatturaA" ID="txtDataFatturaA_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFatturaA" 
                          ID="txtDataFatturaA_MaskedEditExtender">
                </ajaxtoolkit:MaskedEditExtender>
           </td>   
          <td>
            <asp:DropDownList ID="dropNominativo" runat="server" 
                AppendDataBoundItems="True" DataSourceID="sqlNominativo" DataTextField="Cliente" 
                DataValueField="id_conducente">
                <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
            </asp:DropDownList>
          </td>   
          <td>
              <asp:TextBox ID="txtNumFattura" runat="server" Font-Names="arial" 
                  Font-Bold="True" Width="100px"></asp:TextBox>
          </td>          
       </tr>   
       <tr>
          <td>
                <asp:Button ID="btnCmdAvvio" runat="server" Text="Genera XML" style="margin-left: 10%;"/>
          </td>
          <td>
                <asp:Button ID="btnScaricaFile" runat="server" Text="Scarica File XML" style="margin-left: 25%;"/>                
          </td>
          <td colspan="2">
                <asp:Button ID="btnEsci" runat="server" Text="Esci" style="margin-left: 9%;"/>
          </td>
       </tr>   
     </table>
    </div>

    <asp:TextBox ID="txtNFattInviate" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtProgressivo" runat="server" Visible="False"></asp:TextBox>

     <asp:SqlDataSource ID="sqlNominativo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="select conducenti.id_conducente, (nominativo + '   -- ' + codfis) as Cliente  from conducenti order by nominativo">
     </asp:SqlDataSource>
</asp:Content>

