<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="movimenti_veicoli.aspx.vb" Inherits="movimenti_veicoli" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .toolheader {
        background-color:#19191b;
        color:#FFFFFF;
        font-weight:bold;
        width:400px;
        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
        .toolbody {
        width:400px;
        background-color:#FFFFFF;
        filter:alpha(opacity=90);
	        -moz-opacity:.90;
	        opacity:.90;
	        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
        .style3
        {
            width: 132px;
        }
        .style4
        {
            width: 180px;
        }
        .style5
        {
            width: 138px;
        }
        .style6
        {
        }
        .style7
        {
            width: 36px;
        }
        .style8
        {
            width: 242px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Movimenti Veicoli" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
  </table>
  <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">
    <tr>
      <td class="style8">
        <asp:Label ID="lblStazioneUscita" runat="server" Text="Stazione Uscita" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style5">
        <asp:Label ID="lblDataDa" runat="server" Text="Da Data Uscita" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style3">
        <asp:Label ID="ldlDataA" runat="server" Text="A Data Uscita" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style4">
              <asp:Label ID="LblMarca" runat="server" Text="Marca" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style6" colspan="3">
        <asp:Label ID="lblModello" runat="server" Text="Modello" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
      
        <asp:Label ID="lblVeicoloGps" runat="server" Text="Veicolo/GPS" CssClass="testo_bold"></asp:Label>
      
      </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:DropDownList ID="cercaStazioneUscita" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
      </td>
      <td class="style5">
           <a onclick="Calendar.show(document.getElementById('<%=txtCercaUscitaDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaUscitaDa"></asp:TextBox>
               </a>
                <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaUscitaDa" ID="txtCercaUscitaDa_CalendarExtender">
                </asp:calendarextender>--%>

                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaUscitaDa" 
                          ID="txtCercaUscitaDa_MaskedEditExtender">
                </asp:maskededitextender>
      </td>
      <td class="style3">
          <a onclick="Calendar.show(document.getElementById('<%=txtCercaUscitaA.ClientID%>'), '%d/%m/%Y', false)">
                 <asp:TextBox runat="server" Width="70px" ID="txtCercaUscitaA"></asp:TextBox>
              </a>
                <%-- <asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaUscitaA" 
                          ID="txtCercaUscitaA_CalendarExtender">
                 </asp:calendarextender>--%>
                 <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaUscitaA" 
                          ID="txtCercaUscitaA_MaskedEditExtender">
                 </asp:maskededitextender>
      </td>
      <td class="style4">
            
               <asp:DropDownList ID="dropCercaMarca" runat="server"
                   DataSourceID="SqlCercaMarca" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
      </td>
      <td class="style6" colspan="3">
               <asp:DropDownList ID="dropCercaModello" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlCercaModello" DataTextField="descrizione" style="margin-left: 0px"
                   DataValueField="id_modello">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
        </td>
        <td>
      
               <asp:DropDownList ID="dropCercaVeicoloGps" runat="server" style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" >
                   <asp:ListItem Selected="True" Value="0">Veicolo</asp:ListItem>
                   <asp:ListItem Value="1">GPS</asp:ListItem>
               </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:Label ID="lblStazioneUscita0" runat="server" Text="Stazione Presunto Rientro" 
              CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style5">
        <asp:Label ID="lblDataDa1" runat="server" Text="Da Data Pr. Rientro" CssClass="testo_bold"></asp:Label>
        </td>
      <td class="style3">
        <asp:Label ID="lblDataDa2" runat="server" Text="A Data Pr. Rientro" 
                     CssClass="testo_bold"></asp:Label>
        </td>
      <td class="style4">
            
        <asp:Label ID="lblDataDa3" runat="server" Text="Auro Rientrata" 
                     CssClass="testo_bold"></asp:Label>
        </td>
      <td class="style6" colspan="3">
               &nbsp;</td>
      <td>
      
      </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:DropDownList ID="cercaStazionePresuntoRientro" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
        </td>
      <td class="style5">
          <a onclick="Calendar.show(document.getElementById('<%=txtCercaRientroPrDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaRientroPrDa"></asp:TextBox>
              </a>
              <%--  <asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaRientroPrDa" 
                    ID="txtCercaRientroPrDa_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRientroPrDa" 
                          ID="txtCercaRientroPrDa_maskededitextender">
                </asp:maskededitextender>
        </td>
      <td class="style3">
          <a onclick="Calendar.show(document.getElementById('<%=txtCercaRientroPrA.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaRientroPrA"></asp:TextBox>
              </a>
                <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaRientroPrA" 
                     ID="txtCercaRientroPrA_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRientroPrA" 
                          ID="txtCercaRientroPrA_maskededitextender">
                </asp:maskededitextender>
        </td>
      <td class="style4">
            
               <asp:DropDownList ID="dropAutoRientrata" runat="server" 
                   AppendDataBoundItems="True">
                   <asp:ListItem>...</asp:ListItem>
                   <asp:ListItem>Si</asp:ListItem>
                   <asp:ListItem>No</asp:ListItem>
               </asp:DropDownList>
        </td>
      <td class="style6" colspan="3">
               &nbsp;</td>
      <td>
      
      </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:Label ID="lblStazioneRientro" runat="server" Text="Stazione di Rientro" 
              CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style5">
        <asp:Label ID="lblDataDa0" runat="server" Text="Da Data Rientro" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style3">
        <asp:Label ID="ldlDataA0" runat="server" Text="A Data Rientro" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style4">
            
        <asp:Label ID="lblRiferimento" runat="server" Text="Riferimento" CssClass="testo_bold"></asp:Label>
        </td>
      <td class="style6" colspan="3">
        <asp:Label ID="LblTarga" runat="server" Text="Targa/Cod.Gps" CssClass="testo_bold"></asp:Label>
        </td>
      <td>
      
      </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:DropDownList ID="cercaStazioneRientro" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
      </td>
      <td class="style5">
           <a onclick="Calendar.show(document.getElementById('<%=txtCercaRientroDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaRientroDa"></asp:TextBox>
               </a>
              <%--  <asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaRientroDa" 
                    ID="txtCercaRientroDa_calendarextender">
                </asp:calendarextender>--%>
                <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRientroDa" 
                          ID="txtCercaRientroDa_maskededitextender">
                </asp:maskededitextender>
        </td>
      <td class="style3">
          <a onclick="Calendar.show(document.getElementById('<%=txtCercaRientroA.ClientID%>'), '%d/%m/%Y', false)">
                 <asp:TextBox runat="server" Width="70px" ID="txtCercaRientroA"></asp:TextBox>
              </a>
                 <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaRientroA" 
                          ID="txtCercaRientroA_calendarextender">
                 </asp:calendarextender>--%>
                 <asp:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRientroA" 
                          ID="txtCercaRientroA_maskededitextender">
                 </asp:maskededitextender>
        </td>
      <td class="style4">
            
          <asp:TextBox ID="txtNumeroRifemento" runat="server"></asp:TextBox>
        </td>
      <td class="style6" colspan="3">
          <asp:TextBox ID="txtTarga" runat="server" Width="84px"></asp:TextBox>
        </td>
      <td>
      
      </td>
    </tr>
    <tr>
      <td class="style8">
        <asp:Label ID="lblTipoMovimento" runat="server" Text="Tipo Movimento" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style5">
          &nbsp;</td>
      <td class="style3">
          &nbsp;</td>
      <td></td>
      <td class="style7" colspan="3">
        
      </td>
      <td>
      
      </td>
    </tr>
    <tr>
      <td colspan="8">
          <asp:CheckBoxList ID="tipoMovimenti" runat="server" 
              DataSourceID="sqlTipologieMovimenti" DataTextField="descrzione" 
              DataValueField="id" RepeatColumns="6" RepeatDirection="Horizontal">
          </asp:CheckBoxList>
      </td>
    </tr>
    <tr>
      <td colspan="5">
               <asp:Label ID="LblReport" runat="server" Text="Report:" CssClass="testo_bold"></asp:Label>&nbsp;       
          <asp:DropDownList ID="dropReport" runat="server" AppendDataBoundItems="True" 
                   style="margin-left: 0px" AutoPostBack="True">
              <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              <asp:ListItem Selected="False" Value="1">Lista Movimenti Differenza KM</asp:ListItem>
              <asp:ListItem Selected="False" Value="2">Lista Movimenti Differenza Litri</asp:ListItem>
              <asp:ListItem Selected="False" Value="3">Scadenziario</asp:ListItem>
          </asp:DropDownList>&nbsp;
          <asp:Label ID="lblPasso" runat="server" Text="Differenza Minima:" CssClass="testo_bold" Visible="false"></asp:Label>
               &nbsp;<asp:TextBox ID="txtPasso" runat="server" Width="40px" Visible="false"></asp:TextBox>
               &nbsp;
          <asp:Button ID="btnReport" runat="server" Text="Stampa Report" />
          &nbsp;
          <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" />
      </td>
      <td valign="top" class="style6">
          &nbsp;</td>
      <td valign="top" align="right">
          <asp:Button ID="btnScadenziario" runat="server" Text="Scadenziario" />
          </td>
      <td>
      
      </td>
    </tr>
  </table>
  <br />
  <table border="0" cellpadding="0" cellspacing="0" width="1024px">
   <tr>
     <td>
       <asp:ListView ID="listMovimentiTarga" runat="server" DataKeyNames="id" DataSourceID="sqlMovimentiTarga" Visible="false">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_riferimento" runat="server" Text='<%#GetLinkRA(Eval("num_riferimento"), Eval("tipo_movimento")) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tipo_movimento" runat="server" Text='<%# Eval("tipo_movimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                             <asp:Label ID="gps" runat="server" Text='<%# Eval("gps") %>' CssClass="testo_piccolo" />
                             <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("modello") %>' controltovalidate="targa" header="Dettagli Auto" CssHeader="toolheader"  CssBody="toolbody"   />
                             <boxover:BoxOver ID="BoxOver2" runat="server" body='<%# Eval("modello") %>' controltovalidate="gps" header="GPS" CssHeader="toolheader"  CssBody="toolbody"   />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_uscita" runat="server" Text='<%# Eval("stazione_uscita") %>' CssClass="testo_piccolo" Font-Bold="true"  />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_uscita" runat="server" Text='<%# Left(Eval("ora_uscita") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_presunto_rientro" runat="server" Text='<%# Eval("stazione_presunto_rientro") %>' CssClass="testo_piccolo" Font-Bold="true"  />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" /> 
                          </td>
                          <td align="left">
                             <asp:Label ID="ora_presunto_rientro" runat="server" Text='<%# Left(Eval("ora_presunto_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_rientro" runat="server" Text='<%# Eval("stazione_rientro") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_rientro" runat="server" Text='<%# Left(Eval("ora_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_rientro" runat="server" Text='<%# Eval("serbatoio_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_riferimento" runat="server" Text='<%# GetLinkRA(Eval("num_riferimento"), Eval("tipo_movimento")) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tipo_movimento" runat="server" Text='<%# Eval("tipo_movimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                             <asp:Label ID="gps" runat="server" Text='<%# Eval("gps") %>' CssClass="testo_piccolo" />
                             <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("modello") %>' controltovalidate="targa" header="Dettagli Auto" CssHeader="toolheader"  CssBody="toolbody"   />
                             <boxover:BoxOver ID="BoxOver2" runat="server" body='<%# Eval("modello") %>' controltovalidate="gps" header="GPS" CssHeader="toolheader"  CssBody="toolbody"   />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_uscita" runat="server" Text='<%# Eval("stazione_uscita") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_uscita" runat="server" Text='<%# Left(Eval("ora_uscita") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_presunto_rientro" runat="server" Text='<%# Eval("stazione_presunto_rientro") %>' CssClass="testo_piccolo" Font-Bold="true"  />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" /> 
                          </td>
                          <td align="left">
                             <asp:Label ID="ora_presunto_rientro" runat="server" Text='<%# Left(Eval("ora_presunto_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_rientro" runat="server" Text='<%# Eval("stazione_rientro") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_rientro" runat="server" Text='<%# Left(Eval("ora_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_rientro" runat="server" Text='<%# Eval("serbatoio_rientro") %>' CssClass="testo_piccolo" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th1" runat="server" align="left">
                                             <asp:Label ID="Label22" runat="server" Text="Rif." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th2" runat="server" align="left">
                                             <asp:Label ID="Label4" runat="server" Text="Tipo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th7" runat="server" align="left">
                                             <asp:Label ID="Label8" runat="server" Text="Veicolo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th6" runat="server" align="left">
                                              <asp:Label ID="Label6" runat="server" Text="Staz.Out" CssClass="testo_titolo_piccolo" Font-Bold="true"></asp:Label>
                                          </th>
                                          <th id="Th3" runat="server" align="left">
                                              <asp:Label ID="Label5" runat="server" Text="Data Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th4" runat="server" align="left">
                                              <asp:Label ID="Label7" runat="server" Text="Ora" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                              <asp:Label ID="Label9" runat="server" Text="Km Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th>
                                              <asp:Label ID="Label10" runat="server" Text="Lt Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th12" runat="server" align="left">
                                              <asp:Label ID="Label14" runat="server" Text="Staz.Pr." CssClass="testo_titolo_piccolo" Font-Bold="true"></asp:Label>
                                          </th>
                                          <th id="Th13" runat="server" align="left">
                                              <asp:Label ID="Label15" runat="server" Text="Data Pr." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th14" runat="server" align="left">
                                              <asp:Label ID="Label16" runat="server" Text="Ora" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th8" runat="server" align="left">
                                              <asp:Label ID="Label1" runat="server" Text="Staz.In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th9" runat="server" align="left">
                                              <asp:Label ID="Label3" runat="server" Text="Data In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th10" runat="server" align="left">
                                              <asp:Label ID="Label11" runat="server" Text="Ora" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th11" runat="server" align="left">
                                              <asp:Label ID="Label12" runat="server" Text="Km In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th>
                                              <asp:Label ID="Label13" runat="server" Text="Lt In" CssClass="testo_titolo_piccolo"></asp:Label>
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
  
  <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WHERE attiva='1' ORDER BY codice"></asp:SqlDataSource>
  <asp:SqlDataSource ID="sqlTipologieMovimenti" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrzione FROM tipologia_movimenti ORDER BY id"></asp:SqlDataSource>
  <asp:SqlDataSource ID="sqlMovimentiTarga" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT movimenti_targa.id, movimenti_targa.num_riferimento, veicoli.targa,modelli.descrizione As modello, CONVERT(Char(10), movimenti_targa.data_uscita, 103) As data_uscita,CONVERT(Char(10), movimenti_targa.data_rientro, 103) As data_rientro,CONVERT(Char(8), movimenti_targa.data_uscita, 108) As ora_uscita, tipologia_movimenti.descrzione As tipo_movimento,CONVERT(Char(8), movimenti_targa.data_rientro, 108) As ora_rientro,(stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro,km_uscita, km_rientro, serbatoio_uscita, serbatoio_rientro FROM movimenti_targa INNER JOIN veicoli ON movimenti_targa.id_veicolo=veicoli.id LEFT JOIN stazioni As stazioni1 ON movimenti_targa.id_stazione_uscita=stazioni.id LEFT JOIN stazioni As stazioni2 ON movimenti_targa.id_stazione_rientro=stazioni.id INNER JOIN tipologia_movimenti ON movimenti_targa.id_tipo_movimento=tipologia_movimenti.id INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE movimenti_targa.id>0 ORDER BY movimenti_targa.id DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaModello" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_modello, descrizione FROM modelli WHERE (id_CasaAutomobilistica = @id_marca) ORDER BY descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropCercaMarca" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaMarca" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [marche] ORDER BY [descrizione]"></asp:SqlDataSource>
    
    <asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
    </asp:Content>
 
