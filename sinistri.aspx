<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="sinistri.aspx.vb" Inherits="sinistri" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="gestione_danni/sinistro_gestito_da.ascx" TagName="sinistro_gestito_da" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/sinistro_tipologia.ascx" TagName="sinistro_tipologia" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />



    <style type="text/css">
        .style2
        {
        }
        .style4
        {
            width: 104px;
        }
        .style5
        {
            width: 203px;
        }
        .style6
        {
        }
        .style7
        {
        }
        .style8
        {
            width: 213px;
        }
        .style9
        {
            width: 195px;
        }
        .style10
        {
            width: 66px;
        }
        .style11
        {
            width: 168px;
        }
        .style13
        {
            width: 135px;
        }
        .style14
        {
        }
        .style15
        {
        }
        .style17
        {
        }
        .style18
        {
            width: 139px;
        }
        .style20
        {
            width: 132px;
            height: 21px;
        }
        .style22
        {
            width: 141px;
            height: 21px;
        }
        .style23
        {
            width: 45px;
            height: 21px;
        }
        .style24
        {
            height: 21px;
            width: 149px;
        }
        .style25
        {
            width: 149px;
        }
        .style26
        {
            height: 21px;
            }
        .style27
        {
            width: 154px;
        }
        .style28
        {
        }
        .style29
        {
            width: 139px;
            height: 21px;
        }
        .style30
        {
            height: 21px;
            width: 174px;
        }
        .style32
        {
            width: 148px;
        }
        .style33
        {
            width: 123px;
        }
        .style35
        {
            width: 30px;
        }
        .style36
        {
            width: 162px;
        }
        .style38
        {
            width: 25px;
        }
        .style39
        {
            width: 144px;
        }
        .style40
        {
            width: 145px;
        }
        .style41
        {
            width: 126px;
        }
        .style43
        {
            width: 50px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Gestione Sinistri" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
    </table>
  <div runat="server" id="div_pagina">
  <div runat="server" id="div_ricerca" visible="false">
    <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
             <td class="style23">
                 <asp:Label ID="Label41" runat="server" Text="Anno" CssClass="testo_bold"></asp:Label>
             </td>
             <td class="style20">
                 <asp:Label ID="Label42" runat="server" Text="Prot. Interno" CssClass="testo_bold"></asp:Label>
             </td>
             <td class="style29">
                 <asp:Label ID="Label43" runat="server" Text="Num. Sinistro" CssClass="testo_bold"></asp:Label>
             </td>
             <td class="style22">
                 <asp:Label ID="Label44" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
              </td>
              <td class="style27">
              
              <asp:Label ID="LblProprietario" runat="server" Text="Proprietario" CssClass="testo_bold"></asp:Label>
              
              </td>
              <td class="style30">
                 <asp:Label ID="Label59" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
                 </td>
             <td class="style24">
                 <asp:Label ID="Label58" runat="server" Text="Tipo Sinistro" 
                     CssClass="testo_bold"></asp:Label>
                 </td>
          </tr>
          <tr>
             <td class="style6">
                 <asp:DropDownList ID="dropCercaAnno" runat="server" AppendDataBoundItems="True">
                     <asp:ListItem Value="0">...</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td class="style15">
                 <asp:TextBox ID="txtCercaProtocollo" runat="server" Width="60px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
              </td>
             <td class="style18">
             
                 <asp:TextBox ID="txtCercaNumeroSinistro" runat="server" Width="60px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
             
              </td>
             <td class="style28">
                 <asp:TextBox ID="txtCercaTarga" runat="server" Width="80px" MaxLength="20"></asp:TextBox>
              </td>
              <td class="style27">
              
               <asp:DropDownList ID="dropCercaProprietario" runat="server"
                   DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
              </td>
              <td class="style30">
                <asp:DropDownList ID="dropCercaStato" runat="server" AppendDataBoundItems="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                   <asp:ListItem Value="4">Attivo</asp:ListItem>
                   <asp:ListItem Value="1">Attivo non definito</asp:ListItem>
                   <asp:ListItem Value="2">Attivo definito</asp:ListItem>
                   <asp:ListItem Value="3">Passivo</asp:ListItem>
                </asp:DropDownList>
              </td>
             <td class="style25">
                <asp:DropDownList ID="dropCercaTipologia" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTipologiaSinistro" DataTextField="descrizione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td class="style15">
                 <asp:Label ID="Label60" runat="server" Text="Gestita Da" CssClass="testo_bold"></asp:Label>
              </td>
             <td class="style18">
             
                 <asp:Label ID="Label61" runat="server" Text="Da Data Sinistro"  CssClass="testo_bold"></asp:Label>
              </td>
             <td class="style28">
               <asp:Label ID="Label54" runat="server" Text="A Data Sinistro"  CssClass="testo_bold"></asp:Label>
             </td>
              <td class="style27">
              
                 <asp:Label ID="Label62" runat="server" Text="Da Data Definizione"  CssClass="testo_bold"></asp:Label>
              
              </td>
              <td class="style30">
             
               <asp:Label ID="Label63" runat="server" Text="A Data Definizione"  CssClass="testo_bold"></asp:Label>
              </td>
             <td class="style25">
                 &nbsp;</td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td class="style15">
               
                <asp:DropDownList ID="dropCercaGestitaDa" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGestitoDa" DataTextField="descrizione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
               
               </td>
             <td class="style18">
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaDaDataSinistro.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDaDataSinistro"></asp:TextBox>
                   </a>
           <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtCercaDaDataSinistro" ID="CalendarExtender2">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDaDataSinistro" ID="MaskedEditExtender1">
            </ajaxtoolkit:MaskedEditExtender>
              </td>
             <td class="style28">
                  <a onclick="Calendar.show(document.getElementById('<%=txtCercaADataSinistro.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtCercaADataSinistro"></asp:TextBox>
                      </a>
   <%--         <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                     TargetControlID="txtCercaADataSinistro" ID="txtDaData0_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                     CultureDatePlaceholder="" CultureTimePlaceholder="" 
                     CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                     CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                     CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaADataSinistro" 
                     ID="txtDaData0_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
              </td>
              <td class="style27">
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaDaDataDefinizione.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDaDataDefinizione"></asp:TextBox>
                   </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                      TargetControlID="txtCercaDaDataDefinizione" 
                      ID="txtCercaDaDataDefinizione_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                      CultureDatePlaceholder="" CultureTimePlaceholder="" 
                      CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                      CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                      CultureAMPMPlaceholder="" Enabled="True" 
                      TargetControlID="txtCercaDaDataDefinizione" 
                      ID="txtCercaDaDataDefinizione_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
              </td>
              <td class="style30">
             <a onclick="Calendar.show(document.getElementById('<%=txtCercaADataDefinizione.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtCercaADataDefinizione"></asp:TextBox>
                 </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                     TargetControlID="txtCercaADataDefinizione" 
                     ID="txtCercaADataDefinizione_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                     CultureDatePlaceholder="" CultureTimePlaceholder="" 
                     CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                     CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                     CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaADataDefinizione" 
                     ID="txtCercaADataDefinizione_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
              </td>
             <td class="style25">
                 &nbsp;</td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td class="style15" colspan="2">
               
                 <asp:Label ID="Label64" runat="server" Text="Conducente" CssClass="testo_bold"></asp:Label>
               
               </td>
             <td class="style17" colspan="2">
               
                 <asp:Label ID="Label65" runat="server" Text="Controparte" CssClass="testo_bold"></asp:Label>
               
               </td>
              <td class="style26" colspan="2">
             
                 <asp:Label ID="Label66" runat="server" Text="Compagnia Controparte" CssClass="testo_bold"></asp:Label>
               
               </td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td colspan="2">
               
                 <asp:TextBox ID="txtCercaConducente" runat="server" Width="272px"></asp:TextBox>
              </td>
             <td class="style17" colspan="2">
               
                 <asp:TextBox ID="txtCercaControparte" runat="server" Width="272px"></asp:TextBox>
              </td>
              <td class="style26" colspan="2">
             
                 <asp:TextBox ID="txtCercaCompagniaControparte" runat="server" Width="210px"></asp:TextBox>
              </td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td class="style15">
                 
                 <asp:Label ID="Label67" runat="server" Text="Contratto" CssClass="testo_bold"></asp:Label>
                 
              </td>
             <td class="style18">
             
                 
                 <asp:Label ID="Label68" runat="server" Text="RDS" CssClass="testo_bold"></asp:Label>
                 
                 
              </td>
             <td class="style28" colspan="2">
              
              <asp:Label ID="lblStazione" runat="server" Text="Stazione" CssClass="testo_bold"></asp:Label>
              
             </td>
              <td class="style30">
             
                 
                 <asp:Label ID="Label104" runat="server" Text="Num. Sinistro Compagnia" 
                      CssClass="testo_bold"></asp:Label>
               
                 
              </td>
             <td class="style25">

              </td>
          </tr>
          <tr>
             <td class="style6">
                 &nbsp;</td>
             <td class="style15">
                 
                 <asp:TextBox ID="txtCercaContratto" runat="server" Width="130px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
             
              </td>
             <td class="style18">
             
                 
                 <asp:TextBox ID="txtCercaRds" runat="server" Width="130px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
             
                 
              </td>
             <td class="style28" colspan="2">
              
            <asp:DropDownList ID="dropCercaStazione" runat="server" 
                AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                DataValueField="id">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            </asp:DropDownList>
              </td>
              <td class="style30">
             
                 
                 <asp:TextBox ID="txtCercaNumSinistroCompagnia" runat="server" Width="160px" 
                      MaxLength="30"></asp:TextBox>
              </td>
             <td class="style25">

                 &nbsp;</td>
          </tr>
          <tr>
             <td>
               
                 <asp:Label ID="LblReport" runat="server" CssClass="testo_bold" Text="Report:"></asp:Label>
               
             </td>
             <td colspan="6" >
            
               <asp:DropDownList ID="dropReport" runat="server" AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Lista sinistri attivi X tipologia</asp:ListItem>
                   <asp:ListItem  Value="1">Lista sinistri passivi X tipologia</asp:ListItem>
               </asp:DropDownList>
            
               &nbsp;<asp:Button ID="btnReport" runat="server" Text="Stampa Report" />
            
               </td>
          </tr>
          <tr>
             <td class="style14" colspan="7" align="center">
                 <asp:Button ID="btnCerca" runat="server" Text="Cerca" />
              &nbsp;
                 <asp:Button ID="btnAzzera" runat="server" Text="Azzera campi" />
              </td>
          </tr>
     </table>
     <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
       <tr>
         <td>
         
             <asp:ListView ID="listSinistri" runat="server" DataKeyNames="id" DataSourceID="sqlSinistri" >
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="annoLabel" runat="server" Text='<%# Eval("anno") %>' CssClass="testo" />
                          </td>
                          <td align="center">
                              <asp:Label ID="protocolloLabel" runat="server" Text='<%# Eval("numero_protocollo_interno") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                        <%--  <td align="center">
                              <asp:Label ID="numeroLabel" runat="server" Text='<%# Eval("numero") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>--%>
                          <td align="left">
                              <asp:Label ID="statoLabel" runat="server" Text='<%# Eval("stato") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="tipologiaLabel" runat="server" Text='<%# Eval("tipologia") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario_veicolo") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="dataEventoLabel" runat="server" Text='<%# Eval("data_evento") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="guidatoreLabel" runat="server" Text='<%# Eval("guidatore") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="controparteLabel" runat="server" Text='<%# Eval("controparte") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
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
                              <asp:Label ID="annoLabel" runat="server" Text='<%# Eval("anno") %>' CssClass="testo" />
                          </td>
                          <td align="center">
                              <asp:Label ID="protocolloLabel" runat="server" Text='<%# Eval("numero_protocollo_interno") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                         <%-- <td align="center">
                              <asp:Label ID="numeroLabel" runat="server" Text='<%# Eval("numero") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>--%>
                          <td align="left">
                              <asp:Label ID="statoLabel" runat="server" Text='<%# Eval("stato") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="tipologiaLabel" runat="server" Text='<%# Eval("tipologia") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario_veicolo") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="dataEventoLabel" runat="server" Text='<%# Eval("data_evento") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="guidatoreLabel" runat="server" Text='<%# Eval("guidatore") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="controparteLabel" runat="server" Text='<%# Eval("controparte") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
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
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                 
                      <table id="Table2" runat="server" width="100%" >
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_anno" CssClass="testo_titolo">Anno</asp:LinkButton>
                                          </th>
                                          <th id="Th1" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_num_int" CssClass="testo_titolo">N.In.</asp:LinkButton>
                                          </th>
                                        <%--  <th id="Th2" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_numero" CssClass="testo_titolo">Num.</asp:LinkButton>
                                          </th>--%>
                                          <th id="Th3" runat="server" align="left"  style="width:120px;">
                                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_stato" CssClass="testo_titolo">Stato</asp:LinkButton>
                                          </th>
                                          <th id="Th4" runat="server" align="left" style="width:115px;">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_tipologia" CssClass="testo_titolo">Tipologia</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server" align="left" style="width:85px;">
                                             <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_targa" CssClass="testo_titolo">Targa</asp:LinkButton>
                                          </th>
                                          <th id="Th7" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_proprietario" CssClass="testo_titolo">Propr.</asp:LinkButton>
                                          </th>
                                          <th id="Th8" runat="server" align="left" style="width:80px;">
                                             <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_data_evento" CssClass="testo_titolo">Data Ev.</asp:LinkButton>
                                          </th>
                                          <th id="Th9" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_guidatore" CssClass="testo_titolo">Guidatore</asp:LinkButton>
                                          </th>
                                          <th id="Th10" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_controparte" CssClass="testo_titolo">Controparte</asp:LinkButton>
                                          </th>
                                          <th></th>
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
  <div runat="server" id="div_dettaglio" visible="false">
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">
          <tr>
             <td>
                 <asp:Label ID="lblTipoSinistro" runat="server" Text="Label" CssClass="testo_bold"  Font-Size="24px" ></asp:Label>
                 &nbsp;
                 &nbsp;
                 </td>
              <td align="right">
                 <asp:Label ID="lblProprietarioVeicolo" runat="server" Text="Propr" CssClass="testo_bold"  Font-Size="20px" ></asp:Label>
                 &nbsp;&nbsp;&nbsp;
              </td>
          </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" Text="Anno" CssClass="testo_bold"></asp:Label>
             </td>
             <td class="style11">
                 <asp:Label ID="Label3" runat="server" Text="Protocollo Interno" CssClass="testo_bold"></asp:Label>
             </td>
             <td class="style13">
                 <asp:Label ID="Label40" runat="server" Text="Numero Sinistro" CssClass="testo_bold"></asp:Label>
             </td>
             <td colspan="2">
                 <asp:Label ID="Label105" runat="server" Text="Numero Sinistro Compagnia" 
                     CssClass="testo_bold"></asp:Label>
              </td>
             <td>
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
          </tr>
          <tr>
             <td>
                 <asp:TextBox ID="txtAnno" runat="server" Width="66px" ReadOnly="true"></asp:TextBox>
             </td>
             <td class="style11">
                 <asp:TextBox ID="txtProtocollo" runat="server" Width="60px" ReadOnly="true"></asp:TextBox>
              </td>
             <td class="style13">
             
                 <asp:TextBox ID="txtNumeroSinistro" runat="server" Width="60px" ReadOnly="true"></asp:TextBox>
             
             </td>
             <td colspan="2">
             
                 
                 <asp:TextBox ID="txtNumSinistroCompagnia" runat="server" Width="160px" MaxLength="30"></asp:TextBox>
              </td>
             <td>
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td class="style11">
                 <asp:Label ID="Label4" runat="server" Text="Tipologia" CssClass="testo_bold"></asp:Label>
              &nbsp;<asp:ImageButton ID="btnVediTipologia" runat="server" 
                     ImageUrl="/images/aggiorna.png" />
              </td>
             <td class="style13">
             
                 <asp:Label ID="Label5" runat="server" Text="Data Sinistro" CssClass="testo_bold"></asp:Label>
             
             </td>
             <td>
                 <asp:Label ID="Label6" runat="server" Text="Data Apertura Sin." CssClass="testo_bold"></asp:Label>
             </td>
             <td>
                 <asp:Label ID="Label12" runat="server" Text="RDS N." CssClass="testo_bold"></asp:Label>
             </td>
             <td>
                 <asp:Label ID="Label13" runat="server" Text="CONTRATTO N." 
                     CssClass="testo_bold"></asp:Label>
              </td>
             <td>
                 &nbsp;</td>
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td class="style11">
                <asp:DropDownList ID="dropTipologia" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTipologiaSinistro" DataTextField="descrizione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
              </td>
             <td class="style13">
                 <a onclick="Calendar.show(document.getElementById('<%=txtDataEvento.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtDataEvento"></asp:TextBox>
                     </a>
                <%--<ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataEvento" ID="txtDataEvento_CalendarExtender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataEvento" 
                          ID="txtDataEvento_MaskedEditExtender">
                </ajaxtoolkit:maskededitextender>
             </td>
             <td>
                 <asp:TextBox ID="txtDataAperturaSinistro" runat="server" Width="66px" ReadOnly="true"></asp:TextBox>
              </td>
             <td>
                 <asp:TextBox ID="txtNumeroRDS" runat="server" Width="100px" 
                     ReadOnly="true"></asp:TextBox>
              </td>
             <td>
                 <asp:TextBox ID="txtNumContratto" runat="server" Width="100px" 
                     ReadOnly="true"></asp:TextBox>
              </td>
             <td>
                 &nbsp;</td>
          </tr>
          <tr>
             <td>
                <asp:Label ID="Label7" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
             </td>
             <td colspan="2">
                <asp:Label ID="Label8" runat="server" Text="Modello" CssClass="testo_bold"></asp:Label>
             </td>
             <td>
                 &nbsp;</td>
             <td colspan="3">
                <asp:Label ID="Label11" runat="server" Text="Stazione" CssClass="testo_bold"></asp:Label>
              </td>       
          </tr>
          <tr>
             <td>
                 <asp:TextBox ID="txtTarga" runat="server" Width="80px" ReadOnly="true" 
                     MaxLength="20"></asp:TextBox>
             </td>
             <td colspan="3">
                 <asp:TextBox ID="txtModello" runat="server" Width="400px" ReadOnly="true" 
                     MaxLength="100"></asp:TextBox>
              </td>
             <td colspan="3">
                 <asp:TextBox ID="txtStazione" runat="server" Width="400px" ReadOnly="true" 
                     MaxLength="50"></asp:TextBox>
              </td>    
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td colspan="3">
                <asp:Label ID="Label9" runat="server" Text="Nostro Guidatore" CssClass="testo_bold"></asp:Label>
              </td>
             <td colspan="3">
                <asp:Label ID="Label10" runat="server" Text="Controparte" CssClass="testo_bold"></asp:Label>
              </td>    
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td colspan="3">
                 <asp:TextBox ID="txtGuidatore" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
              </td>
             <td colspan="3">
                 <asp:TextBox ID="txtControparte" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
              </td>    
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td colspan="3">
                <asp:Label ID="Label14" runat="server" Text="Compagnia di controparte" 
                     CssClass="testo_bold"></asp:Label>
             </td>
             <td>
                <asp:Label ID="Label15" runat="server" Text="Franchigia Compagnia" CssClass="testo_bold"></asp:Label>
             </td>
             <td>
                <asp:Label ID="Label21" runat="server" Text="Pagata Il" 
                     CssClass="testo_bold"></asp:Label>
              </td>
             <td>
                <asp:Label ID="Label20" runat="server" Text="Franchigie Contestate" CssClass="testo_bold"></asp:Label>
              </td>    
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td colspan="3">
                 <asp:TextBox ID="txtCompagniaControparte" runat="server" Width="400px" 
                     MaxLength="50"></asp:TextBox>
              </td>
             <td>
                 <asp:TextBox ID="txtFranchigiaCompagnia" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
             </td>
             <td>
                 <a onclick="Calendar.show(document.getElementById('<%=txtFranchigiaCompagniaPagataIl.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtFranchigiaCompagniaPagataIl"></asp:TextBox></a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtFranchigiaCompagniaPagataIl" ID="txtFranchigiaCompagniaPagataIl_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtFranchigiaCompagniaPagataIl" 
                          ID="txtFranchigiaCompagniaPagataIl_maskededitextender">
                </ajaxtoolkit:maskededitextender>
              </td>
             <td>
                 <asp:TextBox ID="txtFranchigieContestate" runat="server" Width="66px" 
                     onKeyPress="return filterInputDouble(event)"></asp:TextBox>
              </td>     
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td class="style11">
                <asp:Label ID="Label16" runat="server" Text="Addebito Applicato" CssClass="testo_bold"></asp:Label>
              </td>
             <td class="style13">
                <asp:Label ID="Label17" runat="server" Text="Data Rimborso" 
                     CssClass="testo_bold"></asp:Label>
              </td>
             <td>
                <asp:Label ID="Label18" runat="server" Text="Imp.Rimborso" 
                     CssClass="testo_bold"></asp:Label>
              </td>
             <td>
                <asp:Label ID="Label22" runat="server" Text="SKO" CssClass="testo_bold"></asp:Label>
              </td>    
             <td>
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
          </tr>
          <tr>
             <td>
                 &nbsp;</td>
             <td class="style11">
                 <asp:TextBox ID="txtAddebitoApplicato" runat="server" Width="66px" 
                     onKeyPress="return filterInputDouble(event)"></asp:TextBox>
              </td>
             <td class="style13">
                 <a onclick="Calendar.show(document.getElementById('<%=txtDataRimborso.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtDataRimborso"></asp:TextBox></a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataRimborso" ID="txtDataRimborso_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataRimborso" 
                          ID="txtDataRimborso_maskededitextender">
                </ajaxtoolkit:maskededitextender>
              </td>
             <td>
                 <asp:TextBox ID="txtImportoRimborso" runat="server" Width="66px" 
                     onKeyPress="return filterInputDouble(event)"></asp:TextBox>
              </td>
             <td>
                 <asp:TextBox ID="txtSko" runat="server" Width="66px" 
                     onKeyPress="return filterInputDouble(event)"></asp:TextBox>
              </td>    
             <td>
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
          </tr>
       </table>
       <div runat="server" id="div_importi_sinistro_attivo" visible="false">
         <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
           <tr> 
             <td>
                 <asp:Label ID="lblTipoSinistro0" runat="server" Text="DATI LEGALE" CssClass="testo_bold"  Font-Size="24px" ></asp:Label>
             </td>
           </tr>
          </table>
         <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
            <tr> 
               <td class="style36">
                <asp:Label ID="Label23" runat="server" Text="Gestita Da" 
                       CssClass="testo_bold"></asp:Label>
               &nbsp;<asp:ImageButton ID="btnVediGestitaDa" runat="server" ImageUrl="/images/aggiorna.png" />
               </td>
               <td class="style33">
               
                <asp:DropDownList ID="dropGestitoDa" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGestitoDa" DataTextField="descrizione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
               
               </td>
               <td class="style4">
               
                <asp:Label ID="Label24" runat="server" Text="In Data" 
                       CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style5">
                <asp:TextBox runat="server" Width="70px" ID="txtGestitaDaInData" ReadOnly="true"></asp:TextBox>
                <%--<asp:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtGestitaDaInData" ID="txtGestitaDaInData_calendarextender">
                </asp:calendarextender>--%>

                 <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtGestitaDaInData" 
                          ID="txtGestitaDaInData_maskededitextender">
                </ajaxtoolkit:maskededitextender>
               </td>
               <td class="style2" colspan="2" align="center">
               
                <asp:Label ID="Label70" runat="server" Text="PRESUNTO" CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style35">
                   &nbsp;</td>
               <td align="center">
               
                <asp:Label ID="Label71" runat="server" Text="PAGATO" CssClass="testo_bold"></asp:Label>
               
                </td>
               <td>
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label25" runat="server" Text="Data Definizione" CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               
                 <asp:TextBox ID="txtDataDefinizione" runat="server" Width="66px" ReadOnly="true"></asp:TextBox>
                </td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
               </td>
               <td class="style32">
               
                <asp:Label ID="Label26" runat="server" Text="Sorte" CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style10">
               
                 <asp:TextBox ID="txtSorte" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style35">
               
                   &nbsp;</td>
               <td>
                 <asp:TextBox ID="txtSortePagata" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
                </td>
               <td>
               
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label81" runat="server" Text="Anticipo spese il" 
                       CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               <a onclick="Calendar.show(document.getElementById('<%=txtAnticipoSpeseIl.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtAnticipoSpeseIl"></asp:TextBox></a>

                <%--<ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtAnticipoSpeseIl" ID="txtAnticipoSpeseIl_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                
                   <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAnticipoSpeseIl" 
                          ID="txtAnticipoSpeseIl_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td class="style4">
               
                <asp:Label ID="Label82" runat="server" Text="Per importo"  CssClass="testo_bold"></asp:Label>
               
                </td>
               <td class="style5">
               
                 <asp:TextBox ID="txtAnticipoPerImporto" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
                <asp:Label ID="Label83" runat="server" Text="Su tot."  CssClass="testo_bold"></asp:Label>
               
                 <asp:TextBox ID="txtAnticipoSuImporto" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style32">
               
                <asp:Label ID="Label28" runat="server" Text="Svalutazione" CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style10">
               
                 <asp:TextBox ID="txtSvalutazione" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style35">
                   &nbsp;</td>
               <td>
               
                 <asp:TextBox ID="txtSvalutazionePagata" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
                </td>
               <td>
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label84" runat="server" Text="Rimborso spese il" CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               <a onclick="Calendar.show(document.getElementById('<%=txtRimborsoSpeseIl.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtRimborsoSpeseIl"></asp:TextBox></a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtRimborsoSpeseIl" 
                       ID="txtRimborsoSpeseIl_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRimborsoSpeseIl" 
                          ID="txtRimborsoSpeseIl_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td class="style4">
               
                <asp:Label ID="Label85" runat="server" Text="Per importo"  CssClass="testo_bold"></asp:Label>
               
                </td>
               <td class="style5">
                 <asp:TextBox ID="txtRimborsoSpeseImporto" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               </td>
               <td class="style32">        
               
                <asp:Label ID="Label29" runat="server" Text="Fermo Macchina" CssClass="testo_bold"></asp:Label>
               
                </td>
               <td class="style10">
                 <asp:TextBox ID="txtFermoMacchina" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td class="style35">
                   &nbsp;</td>
               <td>
               
                 <asp:TextBox ID="txtFermoMacchinaPagata" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
                </td>
               <td>
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label86" runat="server" Text="Atto citazione inviato il" 
                       CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style33">
               <a onclick="Calendar.show(document.getElementById('<%=txtAttoCitazioneInviatoIl.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtAttoCitazioneInviatoIl"></asp:TextBox></a>
            <%--    <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtAttoCitazioneInviatoIl" 
                       ID="txtAttoCitazioneInviatoIl_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAttoCitazioneInviatoIl" 
                          ID="txtAttoCitazioneInviatoIl_maskededitextender">
                </ajaxtoolkit:maskededitextender>
               
               </td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
                &nbsp;</td>
               <td class="style32">
               
                <asp:Label ID="Label30" runat="server" Text="Totale" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10" style="border-top-style: solid; border-top-width: thin; border-top-color: #000000" >    
                 <asp:TextBox ID="txtTotale" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td class="style35">    
                   &nbsp;</td>
               <td>    
               
                 <asp:TextBox ID="txtTotalePagata" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
                </td>
               <td>    
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
                   &nbsp;</td>
               <td class="style32">
               
                <asp:Label ID="Label72" runat="server" Text="Competenze AEC" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">
                 <asp:TextBox ID="txtCompetenzeAEC" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td class="style35">
                   &nbsp;</td>
               <td>
                 <asp:TextBox ID="txtCompetenzeAECPagato" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td>
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
                   &nbsp;</td>
               <td class="style32">
               
                <asp:Label ID="Label73" runat="server" Text="Totale Parziale" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">
                 <asp:TextBox ID="txtTotaleParziale" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td class="style35">
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                <asp:Label ID="Label31" runat="server" Text="Compenso SRC" CssClass="testo_bold" Visible="false"></asp:Label>
               
                </td>
               <td class="style5">
                 <asp:TextBox ID="txtCompensoSBC" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)" ReadOnly="true" Visible="false"></asp:TextBox>            
                </td>
               <td class="style32">
               
                <asp:Label ID="Label74" runat="server" Text="Spese AEC Perizie" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtSpesePerizie" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label37" runat="server" Text="Acconto. Liquid. Sinistro" 
                       CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               
                 <asp:TextBox ID="txtAccontoLiquidazioneSinistro" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
                </td>
               <td class="style4">
               
                <asp:Label ID="Label32" runat="server" Text="Spese" CssClass="testo_bold"></asp:Label> 
               
                </td>
               <td class="style5">
               
                 <asp:TextBox ID="txtSpese" runat="server" Width="66px"  onKeyPress="return filterInputDouble(event)" ReadOnly="true"></asp:TextBox>            
               
                </td>
               <td class="style32">
               
                <asp:Label ID="Label75" runat="server" Text="Spese AEC Fotografie" 
                       CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtSpeseFotografie" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label36" runat="server" Text="Rich. Liquid. Danni" CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               
                 <asp:TextBox ID="txtRichiestaLiquidazioneDanni" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
                </td>
               <td class="style4">
               
                <asp:Label ID="Label33" runat="server" Text="Onorario Avv." CssClass="testo_bold"></asp:Label>
               
                </td>
               <td class="style5">
                 <asp:TextBox ID="txtOnorarioAvvocato" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>            
                </td>
               <td class="style32">
               
                <asp:Label ID="Label76" runat="server" Text="Spese AEC Post." 
                       CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtSpesePostali" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                <asp:Label ID="Label35" runat="server" Text="DIFFERENZA" CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style33">
               
                 <asp:TextBox ID="txtDifferenza" runat="server" Width="66px" Readonly="true" onKeyPress="return filterInputDoubleSegno(event)"></asp:TextBox>
                </td>
               <td class="style4">
               
                <asp:Label ID="Label34" runat="server" Text="LIQUIDATO" CssClass="testo_bold"></asp:Label>
               
                </td>
               <td class="style5">
                 <asp:TextBox ID="txtLiquidato" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>            
                   <asp:Button ID="btnCalcola" runat="server" Text="Calcola" />
                </td>
               <td class="style32">
               
                <asp:Label ID="Label77" runat="server" Text="Spese AEC Visure" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtSpeseVisure" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                <asp:Label ID="Label27" runat="server" Text="% Concorsuale" 
                       CssClass="testo_bold"></asp:Label>
                </td>
               <td class="style5">
               
                 <asp:TextBox ID="txtPercentualeConcorsuale" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
                </td>
               <td class="style32">
               
                <asp:Label ID="Label78" runat="server" Text="Spese AEC Compensi" CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtSpeseCompensi" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
                   &nbsp;</td>
               <td class="style32">
               
                <asp:Label ID="Label79" runat="server" Text="Totale Spese AEC" 
                       CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtTotaleSpeseAEC" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            <tr> 
               <td class="style36">
               
                   &nbsp;</td>
               <td class="style33">
               
                   &nbsp;</td>
               <td class="style4">
               
                   &nbsp;</td>
               <td class="style5">
                   &nbsp;</td>
               <td class="style32">
               
                <asp:Label ID="Label80" runat="server" Text="Totale" 
                       CssClass="testo_bold"></asp:Label> 
                </td>
               <td class="style10">   
                 <asp:TextBox ID="txtTotaleGlobale" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right" class="style35">   
                   &nbsp;</td>
               <td>   
                 <asp:TextBox ID="txtTotaleGlobalePagato" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)" style="margin-left: 0px"></asp:TextBox>       
                </td>
               <td align="right">   
                   &nbsp;</td>
            </tr>
            
         </table>
       
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
            <td class="style6" colspan="8">
                <asp:Label ID="Label45" runat="server" Text="FATTURE" CssClass="testo_bold" Font-Size="20px"></asp:Label>
            </td>
          </tr>
       </table>
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr> 
               <td class="style39" style="background-color: #FFFFFF">
               
                <asp:Label ID="Label87" runat="server" Text="Fattura AEC a SRC" CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style38">   
                   &nbsp;</td>
               <td class="style40">
                <asp:Label ID="Label88" runat="server" Text="Importo" CssClass="testo_bold"></asp:Label>
               </td>
               <td>
               
                 <asp:TextBox ID="txtImportoFatturaSBC" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style41">
               
                <asp:Label ID="Label89" runat="server" Text="Data" CssClass="testo_bold"></asp:Label>
                </td>
               <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtFatturaSBCData.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtFatturaSBCData"></asp:TextBox></a>
                <%--<ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtFatturaSBCData" 
                       ID="txtFatturaSBCData_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtFatturaSBCData" 
                          ID="txtFatturaSBCData_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td>   
               
                <asp:Label ID="Label90" runat="server" Text="Numero" CssClass="testo_bold"></asp:Label>
                </td>
               <td>   
             
                 <asp:TextBox ID="txtFatturaSbcNumero" runat="server" Width="100px" 
                       MaxLength="20" ></asp:TextBox>
             
               </td>
               
            </tr>
          <tr> 
               <td class="style39" style="background-color: #FFFFFF">
               
                 <asp:Label ID="Label91" runat="server" Text="Fattura AEC a AVV" CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style38">   
                   &nbsp;</td>
               <td class="style40">
                 <asp:Label ID="Label92" runat="server" Text="Importo" CssClass="testo_bold"></asp:Label>
               </td>
               <td>
               
                 <asp:TextBox ID="txtImportoFatturaAvv" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style41">
               
                <asp:Label ID="Label93" runat="server" Text="Data" CssClass="testo_bold"></asp:Label>
                </td>
               <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtFatturaAvvData.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtFatturaAvvData"></asp:TextBox></a>
                <%--<ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtFatturaAvvData" 
                       ID="txtFatturaAvvData_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtFatturaAvvData" 
                          ID="txtFatturaAvvData_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td>                  
                <asp:Label ID="Label94" runat="server" Text="Numero" CssClass="testo_bold"></asp:Label>
                </td>
               <td>   
             
                 <asp:TextBox ID="txtFatturaAvvNumero" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
             
               </td>
               
            </tr>
          <tr> 
               <td class="style39" style="background-color: #FFFFFF">
               
                 <asp:Label ID="Label95" runat="server" Text="Fattura Onorario Avv." 
                       CssClass="testo_bold"></asp:Label>
               
               </td>
               <td class="style38">   
                   &nbsp;</td>
               <td class="style40">
                 <asp:Label ID="Label96" runat="server" Text="Importo Lordo" CssClass="testo_bold"></asp:Label>
               </td>
               <td>
               
                 <asp:TextBox ID="txtImportoFatturaOnorarioAvvocato" runat="server" Width="66px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style41">
               
                <asp:Label ID="Label97" runat="server" Text="Data" CssClass="testo_bold"></asp:Label>
                </td>
               <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtDataFatturaOnorarioAvvocato.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtDataFatturaOnorarioAvvocato"></asp:TextBox></a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataFatturaOnorarioAvvocato" 
                       ID="txtDataFatturaOnorarioAvvocato_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFatturaOnorarioAvvocato" 
                          ID="txtDataFatturaOnorarioAvvocato_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td>   
               
                <asp:Label ID="Label98" runat="server" Text="Numero" CssClass="testo_bold"></asp:Label>
                </td>
               <td>   
             
                 <asp:TextBox ID="txtNumeroFatturaOnorarioAvvocato" runat="server" Width="100px" 
                       MaxLength="20" ></asp:TextBox>
             
               </td>
               
            </tr>
          <tr> 
               <td class="style39">
               
                   &nbsp;</td>
               <td class="style38">   
                   &nbsp;</td>
               <td class="style40">
                 <asp:Label ID="Label99" runat="server" Text="Importo Netto" CssClass="testo_bold"></asp:Label>
               </td>
               <td>
               
                 <asp:TextBox ID="txtImportoFatturaOnorarioAvvocatoNetto" runat="server" Width="66px" 
                       onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               
               </td>
               <td class="style41">
               
                <asp:Label ID="Label100" runat="server" Text="Pagato in data" CssClass="testo_bold"></asp:Label>
                </td>
               <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtDataPagamentoOnorarioNetto.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtDataPagamentoOnorarioNetto"></asp:TextBox></a>
                <%--<ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataPagamentoOnorarioNetto" 
                       ID="txtDataPagamentoOnorarioNetto_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataPagamentoOnorarioNetto" 
                          ID="txtDataPagamentoOnorarioNetto_maskededitextender">
                </ajaxtoolkit:maskededitextender>
                </td>
               <td>   
               
                   &nbsp;</td>
               <td>   
             
                   &nbsp;</td>
               
            </tr>
       </table>
      </div>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
            <td class="style43">
                <asp:Label ID="Label38" runat="server" Text="Note" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="bottom">
            
                <asp:TextBox ID="txtNote" runat="server" Height="62px" TextMode="MultiLine" 
                    Width="884px"></asp:TextBox>
            
                &nbsp;<asp:Button ID="btnSalvaNota" runat="server" Text="Salva" Visible="false" />
            
            </td>
          </tr> 
          <tr>
            <td colspan="2">
            <asp:ListView ID="listNote" runat="server" DataKeyNames="id" DataSourceID="sqlNote">
               <ItemTemplate>
                   <tr style="background-color:#DCDCDC;color: #000000;">
                       <td align="left">
                           <asp:Label ID="operatore" runat="server" CssClass="testo" Text='<%# Eval("operatore") %>'/>
                       </td>
                       <td align="left">
                           <asp:Label ID="data" runat="server" CssClass="testo" Text='<%# Eval("data") %>' />
                       </td>
                       <td align="left">
                           <asp:Label ID="descrizone" runat="server" CssClass="testo" Text='<%# Eval("descrizione") %>'/>
                       </td>
                   </tr>
               </ItemTemplate>
               <AlternatingItemTemplate>
                   <tr style="">
                       <td align="left">
                           <asp:Label ID="operatore" runat="server" CssClass="testo" Text='<%# Eval("operatore") %>' />
                       </td>
                       <td align="left">
                           <asp:Label ID="data" runat="server" CssClass="testo" Text='<%# Eval("data") %>' />
                       </td>
                       <td align="left">
                           <asp:Label ID="descrizone" runat="server" CssClass="testo" Text='<%# Eval("descrizione") %>'/>
                       </td>
                   </tr>
               </AlternatingItemTemplate>
               <EmptyDataTemplate>
                   <table ID="Table3" runat="server" style="">
                       <tr>
                           <td>
                           </td>
                       </tr>
                   </table>
               </EmptyDataTemplate>
               <LayoutTemplate>
                   <table ID="Table4" runat="server" width="100%">
                       <tr ID="Tr4" runat="server">
                           <td ID="Td3" runat="server">
                               <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                                   width="100%">
                                   <tr ID="Tr5" runat="server" bgcolor="#19191b" style="color: #FFFFFF">
                                       <th ID="Th11" runat="server" align="left">
                                           <asp:Label ID="Label101" runat="server" CssClass="testo_titolo" Text="Operatore"></asp:Label>
                                       </th>
                                       <th ID="Th12" runat="server" align="left">
                                           <asp:Label ID="Label102" runat="server" CssClass="testo_titolo" Text="Data"></asp:Label>
                                       </th>
                                       <th ID="Th13" runat="server" align="left" width="70%">
                                           <asp:Label ID="Label103" runat="server" CssClass="testo_titolo" Text=""></asp:Label>
                                       </th>
                                   </tr>
                                   <tr ID="itemPlaceholder" runat="server">
                                   </tr>
                               </table>
                           </td>
                       </tr>
                       <tr ID="Tr6" runat="server">
                           <td ID="Td4" runat="server" style="">
                           </td>
                       </tr>
                   </table>
               </LayoutTemplate>
           </asp:ListView>
              
            </td>
          </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
            <td class="style9">
                <asp:Label ID="Label19" runat="server" Text="Data Rimborso a Cliente SRC" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
            <a onclick="Calendar.show(document.getElementById('<%=txtDataRimborsoClienteSBC.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtDataRimborsoClienteSBC"></asp:TextBox></a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtDataRimborsoClienteSBC" 
                    ID="txtDataRimborsoClienteSBC_calendarextender">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataRimborsoClienteSBC" 
                          ID="txtDataRimborsoClienteSBC_maskededitextender">
                </ajaxtoolkit:maskededitextender>
            
            </td>
            <td class="style8">
                <asp:Label ID="Label39" runat="server" Text="Importo Rimborso a Cliente SRC" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
            
                 <asp:TextBox ID="txtImportoRimborsoAClienteSbc" runat="server" Width="66px" 
                    onKeyPress="return filterInputDouble(event)"></asp:TextBox>
            
            </td>
          </tr>
          <tr>
            <td class="style7" colspan="4" align="center">
                <asp:Button ID="btnSalva" runat="server" Text="Salva" style="height: 26px" />
&nbsp;<asp:Button ID="btnStampaSinistro" runat="server" Text="Stampa sinistro" Visible="false" />
&nbsp;<asp:Button ID="btnStampaDatiLegale" runat="server" Text="Stampa Dati Legale" Visible="false" />
&nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Chiudi" />
              </td>
          </tr>
       </table>
    </div>
  </div>
    <div id="div_gestitoDa" runat="server" Visible="False">
        <uc1:sinistro_gestito_da id="gestito_da" runat="server" />       
    </div>
    
    <div id="div_tipologia" runat="server" Visible="False">
        <uc1:sinistro_tipologia id="tipologia" runat="server" />        
    </div>
  
  <asp:SqlDataSource ID="sqlSinistri" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT sinistri.id, anno, numero_protocollo_interno, numero, targa, proprietario_veicolo, CONVERT(Char(10), data_evento, 103) As data_evento, guidatore, controparte, sinistri_stato.descrizione As stato, sinistri_tipologia.descrizione As tipologia FROM sinistri WITH(NOLOCK) INNER JOIN sinistri_stato WITH(NOLOCK) ON sinistri.id_stato=sinistri_stato.id INNER JOIN sinistri_tipologia WITH(NOLOCK) ON sinistri.id_tipologia=sinistri_tipologia.id WHERE attivo='1'"></asp:SqlDataSource>
 
  <asp:SqlDataSource ID="sqlTipologiaSinistro" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrizione FROM sinistri_tipologia WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>
    
  <asp:SqlDataSource ID="sqlGestitoDa" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrizione FROM sinistri_gestito_da WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>
    
  <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) ORDER BY nome_stazione">
  </asp:SqlDataSource>
  
  <asp:SqlDataSource ID="sqlNote" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT sinistri_note.id, sinistri_note.descrizione, sinistri_note.data, (operatori.cognome + ' ' + operatori.nome) As operatore  FROM sinistri_note WITH(NOLOCK) INNER JOIN operatori WITH(NOLOCK) ON sinistri_note.id_operatore=operatori.id WHERE anno=@anno AND protocollo=@protocollo ORDER BY id DESC">
    <SelectParameters>
            <asp:ControlParameter ControlID="txtAnno" Name="anno" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="txtProtocollo" Name="protocollo" 
                PropertyName="Text" Type="Int32" />
       </SelectParameters>      
  </asp:SqlDataSource>
    
    <asp:Label ID="stato" runat="server" Visible="false"></asp:Label>  
    <asp:Label ID="id_sinistro" runat="server" Visible="false"></asp:Label>  
    <asp:Label ID="id_rds" runat="server" Visible="false"></asp:Label>  
    <asp:Label ID="id_contratto" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_veicolo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione" runat="server" Visible="false"></asp:Label> 
    <asp:Label ID="id_proprietario_veicolo" runat="server" Visible="false"></asp:Label> 
    <asp:Label ID="iva_attuale" runat="server" Visible="false"></asp:Label>   
    <asp:Label ID="query_cerca" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>   
    <asp:Label ID="permesso_accesso" runat="server" visible="false"></asp:Label>
    


    </asp:Content>
  



