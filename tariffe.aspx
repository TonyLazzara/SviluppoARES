<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tariffe.aspx.vb" Inherits="tariffe" title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
    {
        }
    input[type=submit]
       {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style5
        {
            width: 193px;
        }
        .style6
        {
            width: 120px;
        }
        .style9
        {
            width: 145px;
        }
        .style11
        {
            width: 144px;
        }
        .style13
        {
            width: 304px;
        }
        .style15
        {
            width: 22px;
        }
        .style17
        {
            height: 27px;
        }
        .style18
        {
            width: 50%;
            height: 27px;
        }
        .style19
        {
            width: 14%;
        }
        .style21
        {
            width: 1024px;
        }
        .style22
        {
            width: 112px;
        }
        .style25
        {
            width: 147px;
        }
        .style26
        {
            width: 123px;
        }
        .style27
        {
        }
        .style28
        {
            width: 130px;
        }
        .style30
        {
            width: 322px;
        }
        .style31
        {
            width: 30%;
        }
        .style32
        {
            width: 38%;
        }
        .style33
        {
            width: 478px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Tariffe</b>
           </td>
         </tr>
    </table>
    <div id="tab_cerca" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" runat="server" style="border:4px solid #444">
    <tr>
      <td class="style5">
        <asp:Label ID="Label18" runat="server" Text="Nome Tariffa" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style6">
        <asp:Label ID="Label46" runat="server" Text="Codice" CssClass="testo_bold"></asp:Label>
        </td>
      <td class="style28">
        <asp:Label ID="Label54" runat="server" Text="Vendibile in data" CssClass="testo_bold" Visible="false"></asp:Label>
      </td>
      <td>
         <asp:Label ID="Label55" runat="server" Text="Pickup in data" CssClass="testo_bold" Visible="false"></asp:Label>
      </td>
      <td align="left">

            &nbsp;</td>
      <td>
      </td>
    </tr>
     <tr>
     <td class="style5">
        
         <asp:TextBox ID="txtCodice" runat="server" Width="180px"></asp:TextBox>
        
      </td>
      <td class="style6">
 
         <asp:TextBox ID="txtCodiceBreve" runat="server" Width="100px"></asp:TextBox>
        
         </td>
      <td class="style28">
          <a onclick="Calendar.show(document.getElementById('<%=cercaVendibilita.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="cercaVendibilita" runat="server" Width="70px" Visible="false"></asp:TextBox>
              </a>
              <%-- <asp:CalendarExtender ID="cercaVendibilita_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="cercaVendibilita">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="cercaVendibilita_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="cercaVendibilita">
               </asp:MaskedEditExtender>
       
      </td>
      <td align="left">
          <a onclick="Calendar.show(document.getElementById('<%=cercaPickup.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="cercaPickup" runat="server" Width="70px" Visible="false"></asp:TextBox>
              </a>
       <%--        <asp:CalendarExtender ID="cercaPickup_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="cercaPickup">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="cercaPickup_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="cercaPickup">
               </asp:MaskedEditExtender>
           </td>
      <td align="left">

            <asp:Button ID="btnCerca" runat="server" Text="Cerca" UseSubmitBehavior="False" />  
       
         </td>
      <td align="right">

            <asp:Button ID="btnNuovo" runat="server" Text="Nuova tariffa" UseSubmitBehavior="False" />  
       
      </td>
    </tr>
  </table>
  <br />
  <table width="1024px">
    <tr> 
      <td colspan="4" align="center">
       
          &nbsp;&nbsp;
          <br />
       
          <asp:ListView ID="listTariffe" runat="server" DataKeyNames="id" DataSourceID="sqlTariffe">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC;color: #000000;">
                      <td align="left">
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="true" CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="CODTAR" runat="server" Text='<%# Eval("codtar") %>' CssClass="testo" />
                            
                          <asp:Label ID="tariffa_broker" runat="server" Text='<%# Eval("is_broker_prepaid") %>' Visible="false" />
                          <asp:Label ID="max_sconto" runat="server" Text='<%# Eval("max_sconto") %>' Visible="false" />
                          <asp:Label ID="max_sconto_rack" runat="server" Text='<%# Eval("max_sconto_rack") %>' Visible="false" />
                          <asp:Label ID="IsWeb" runat="server" Text='<%# Eval("is_web") %>' Visible="false" />
                          <asp:Label ID="IsWebPrepagata" runat="server" Text='<%# Eval("is_web_prepagato") %>' Visible="false" />
                          <asp:Label ID="codicepromozionale" runat="server" Text='<%# Eval("codicepromozionale") %>' Visible="false" />
                      </td>
                      <%--<td align="left">
                          <asp:Label ID="Label51" runat="server" Text='<%# Eval("vendibilita_da") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label50" runat="server" Text='<%# Eval("vendibilita_a") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label52" runat="server" Text='<%# Eval("pickup_da") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label53" runat="server" Text='<%# Eval("pickup_a") %>' CssClass="testo" />
                      </td>--%>
                      <td align="center">
                         <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                      </td>
                      <td align="center">  
                        <asp:Button ID="btnFonti" runat="server" Text="Fonti/Stazioni" CommandName="fonti_stazioni" />     
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                       <td align="left">
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="true" CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="CODTAR" runat="server" Text='<%# Eval("codtar") %>' CssClass="testo" />
                              
                          <asp:Label ID="tariffa_broker" runat="server" Text='<%# Eval("is_broker_prepaid") %>' Visible="false" />
                          <asp:Label ID="max_sconto" runat="server" Text='<%# Eval("max_sconto") %>' Visible="false" />
                          <asp:Label ID="max_sconto_rack" runat="server" Text='<%# Eval("max_sconto_rack") %>' Visible="false" />
                           <asp:Label ID="IsWeb" runat="server" Text='<%# Eval("is_web") %>' Visible="false" />
                          <asp:Label ID="IsWebPrepagata" runat="server" Text='<%# Eval("is_web_prepagato") %>' Visible="false" />
                          <asp:Label ID="codicepromozionale" runat="server" Text='<%# Eval("codicepromozionale") %>' Visible="false" />
                      </td>
                      <%--<td align="left">
                          <asp:Label ID="Label51" runat="server" Text='<%# Eval("vendibilita_da") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label50" runat="server" Text='<%# Eval("vendibilita_a") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label52" runat="server" Text='<%# Eval("pickup_da") %>' CssClass="testo" />
                      </td>
                      <td align="left">
                          <asp:Label ID="Label53" runat="server" Text='<%# Eval("pickup_a") %>' CssClass="testo" />
                      </td>--%>
                      <td align="center">
                         <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                      </td>
                      <td align="center">  
                        <asp:Button ID="btnFonti" runat="server" Text="Fonti/Stazioni" CommandName="fonti_stazioni" />     
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table runat="server" style="">
                      <tr>
                          <td>
                              Nessuna tariffa salvata.</td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table runat="server" width="100%">
                      <tr runat="server">
                          <td runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%"
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;" >
                                  <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                      <th id="Th13" runat="server" align="left">
         
                                      </th>
                                      <th id="Th1" runat="server" align="left">
                                          <asp:Label ID="Label24" runat="server" Text="Nome Tariffa" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>
                                      <th id="Th5" runat="server" align="left">
                                          <asp:Label ID="Label32" runat="server" Text="Codice" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>
                                      <%--<th id="Th9" runat="server" align="left">
                                          <asp:Label ID="Label36" runat="server" Text="Valida Da" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>
                                      <th id="Th10" runat="server" align="left">
                                          <asp:Label ID="Label47" runat="server" Text="Valida A" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>
                                      <th id="Th11" runat="server" align="left">
                                          <asp:Label ID="Label48" runat="server" Text="Pickup Da" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>
                                      <th id="Th12" runat="server" align="left">
                                          <asp:Label ID="Label49" runat="server" Text="Pickup A" CssClass="testo" ForeColor="White"></asp:Label>
                                      </th>--%>
                                      <th id="Th3" runat="server">
                                      </th>
                                      <th id="Th4" runat="server">
                                      </th>
                                      <th id="Th2" runat="server">
                                      </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr runat="server">
                          <td runat="server" style="">
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
       
          <br />
      </td>
    </tr>
  </table>
  </div>
  <asp:Panel ID="tab_fonti_stazioni" runat="server" Visible="false">
     
      <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
        <tr>
            <td>
              <asp:Label ID="Label19" runat="server" Text="Tipologia di clienti a cui la tariffa è applicabile:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="right">
            
                <asp:RadioButtonList ID="radioSel1" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Tutti</asp:ListItem>
                    <asp:ListItem Value="0">Seleziona</asp:ListItem>
                </asp:RadioButtonList>
            
            </td>
        </tr>
       </table>
       <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
        <tr>
            <td valign="top" width="45%" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-left-style: solid; border-left-width: thin; border-left-color: #000000">
               <asp:Label ID="lblNonAssociati1" runat="server" Text="Non associati alla tariffa" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top" width="10%" align="center" 
                
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000">
            </td>
            <td valign="top" width="45%" align="left" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:Label ID="lblAssociati1" runat="server" Text="Associati alla tariffa" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" width="45%" 
                style="border-left-style: solid; border-left-width: thin; border-left-color: #000000; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000;">
                <asp:ListBox ID="listTipoClienti" runat="server" DataSourceID="sqlTipoClienti" 
                    DataTextField="descrizione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />&nbsp;  
                      
            </td>
            <td valign="top" width="10%" align="center" 
                
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000">
                      <asp:Button ID="PassaUnoClienti" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoClienti" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttoClienti" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttoClienti" runat="server" Text="<<" />
                      </td>
            <td valign="top" width="45%" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:ListBox ID="listTipoClientiSelezionati" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />&nbsp;
            </td>
        </tr>
        </table>
        
        <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Applicabile se il PICK UP avviene nelle seguenti stazioni:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="right">
            
                <asp:RadioButtonList ID="radioSel2" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Tutte</asp:ListItem>
                    <asp:ListItem Value="0">Seleziona</asp:ListItem>
                </asp:RadioButtonList>
            
            </td>
        </tr>
      </table>
        
      <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
        <tr>
            <td valign="top" width="45%" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-left-style: solid; border-left-width: thin; border-left-color: #000000">
               <asp:Label ID="Label1" runat="server" Text="Non associati alla tariffa" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top" width="10%" align="center" 
                
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000">
            </td>
            <td valign="top" width="45%" align="left" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:Label ID="Label2" runat="server" Text="Associati alla tariffa" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" width="45%" 
                style="border-left-style: solid; border-left-width: thin; border-left-color: #000000; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000;">
                <asp:ListBox ID="listStazioniPick" runat="server" DataSourceID="sqlStazioni" 
                    DataTextField="stazione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />&nbsp;  
                      
            </td>
            <td valign="top" width="10%" align="center" 
                
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000">
                      <asp:Button ID="PassaUnoPick" runat="server" Text=">" style="height: 26px" /><br />
                      <asp:Button ID="TornaUnoPick" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiPick" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiPick" runat="server" Text="<<" />
                      </td>
            <td valign="top" width="45%" style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:ListBox ID="listStazioniPickSelezionate" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />&nbsp;
            </td>
        </tr>
        </table>
        
        <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
 
        <tr>
            <td colspan="2">
              <asp:Label ID="Label21" runat="server" Text="Applicabile se il DROP OFF avviene nelle seguenti stazioni:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="right" colspan="2">
            
                <asp:RadioButtonList ID="radioSel3" runat="server" 
                    AppendDataBoundItems="True" RepeatDirection="Horizontal" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Tutte</asp:ListItem>
                    <asp:ListItem Value="0">Seleziona</asp:ListItem>
                </asp:RadioButtonList>
            
            </td>
        </tr>
         <tr>
            <td valign="top" width="45%" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-left-style: solid; border-left-width: thin; border-left-color: #000000">
               <asp:Label ID="Label3" runat="server" Text="Non associati alla tariffa" CssClass="testo_bold"></asp:Label>
            </td>
            <td colspan="2" valign="top" width="10%" align="center" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000">
            </td>
            <td valign="top" width="45%" align="left" 
                style="border-top-style: solid; border-top-width: thin; border-top-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:Label ID="Label4" runat="server" Text="Associati alla tariffa" Font-Bold="true" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
         <tr>
            <td valign="top" width="45%" 
                style="border-left-style: solid; border-left-width: thin; border-left-color: #000000; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000;">
                <asp:ListBox ID="listStazioniDrop" runat="server" DataSourceID="sqlStazioni" 
                    DataTextField="stazione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />&nbsp;  
                      
            </td>
            <td colspan="2" valign="top" width="10%" align="center" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000">
                      <asp:Button ID="PassaUnoDrop" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoDrop" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiDrop" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiDrop" runat="server" Text="<<" />
                      </td>
            <td valign="top" width="45%" style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000">
                <asp:ListBox ID="listStazioniDropSelezionate" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />&nbsp;
            </td>
        </tr>
      </table>

      <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
        <tr>
         <td align="center">
              <asp:Button ID="btnMemorizzaFontiStazioni" runat="server" Text="Memorizza" />
              <asp:Button ID="btnTorna" runat="server" Text="Torna alla lista" />
         </td>
        </tr>
          <tr>
              <td>
                <asp:Label ID="Label27" runat="server" Text="Regole speciali per Tipo Cliente" CssClass="testo_bold"></asp:Label>
              </td>
          </tr>
          <tr>
              <td>
                  <asp:ListView ID="listRegoleXFonti" runat="server" DataKeyNames="id" DataSourceID="sqlRegoleXFonti">
                      <ItemTemplate>
                          <tr style="background-color:#DCDCDC;color: #000000;">
                              <td align="left">
                                  <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                  <asp:Label ID="cliente" runat="server" CssClass="testo" Text='<%# Eval("cliente") %>'  />
                              </td>
                              <td align="left">
                                  <asp:TextBox ID="nome_tariffa" runat="server" Text='<%# Eval("nome_tariffa") %>' MaxLength="50" Width="500px" ></asp:TextBox>
                              </td>
                              <td align="left">
                                  <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_da").ClientID %>'), '%d/%m/%Y', false)"> 
                                  <asp:TextBox ID="valido_da" runat="server" Text='<%# Eval("valido_da") %>' Width="80px" ></asp:TextBox></a>
                                  <%--<asp:CalendarExtender ID="valido_da_CalendarExtender" runat="server" 
                                       Format="dd/MM/yyyy" TargetControlID="valido_da">
                                   </asp:CalendarExtender>--%>
                                   <asp:MaskedEditExtender ID="valido_da_MaskedEditExtender" runat="server" 
                                       Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                       TargetControlID="valido_da">
                                   </asp:MaskedEditExtender>
                              </td>
                          </tr>
                      </ItemTemplate>
                      <AlternatingItemTemplate>
                          <tr style="">
                              <td align="left">
                                  <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                  <asp:Label ID="cliente" runat="server" CssClass="testo" Text='<%# Eval("cliente") %>'  />
                              </td>
                              <td align="left">
                                  <asp:TextBox ID="nome_tariffa" runat="server" Text='<%# Eval("nome_tariffa") %>' MaxLength="50" Width="500px" ></asp:TextBox>
                              </td>
                              <td align="left">
                                  <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_da").ClientID %>'), '%d/%m/%Y', false)">
                                  <asp:TextBox ID="valido_da" runat="server" Text='<%# Eval("valido_da") %>' Width="80px" ></asp:TextBox></a>
                                 <%-- <asp:CalendarExtender ID="valido_da_CalendarExtender" runat="server" 
                                       Format="dd/MM/yyyy" TargetControlID="valido_da">
                                   </asp:CalendarExtender>--%>
                                   <asp:MaskedEditExtender ID="valido_da_MaskedEditExtender" runat="server" 
                                       Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                       TargetControlID="valido_da">
                                   </asp:MaskedEditExtender>
                              </td>
                          </tr>
                      </AlternatingItemTemplate>
                      <EmptyDataTemplate>
                          <table runat="server" style="">
                              <tr>
                                  <td>
                                      Nessuna Tipologia Cliente trovata.
                                  </td>
                              </tr>
                          </table>
                      </EmptyDataTemplate>
                      <LayoutTemplate>
                          <table runat="server" width="100%">
                              <tr runat="server">
                                  <td runat="server">
                                      <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                                          width="100%">
                                          <tr runat="server" bgcolor="#19191b" style="color: #FFFFFF">
                                              <th ID="Th6" runat="server" align="left">
                                                  <asp:Label ID="Label40" runat="server" CssClass="testo" ForeColor="White" Text="Tipo Cliente"></asp:Label>
                                              </th>
                                              <th ID="Th7" runat="server" align="left">
                                                  <asp:Label ID="Label41" runat="server" CssClass="testo" ForeColor="White" Text="Nome Tariffa"></asp:Label>
                                              </th>
                                              <th ID="Th8" runat="server" align="left">
                                                  <asp:Label ID="Label34" runat="server" CssClass="testo" ForeColor="White" Text="Tipo Cliente valido da Data"></asp:Label>
                                              </th>
                                          </tr>
                                          <tr ID="itemPlaceholder" runat="server">
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                              <tr runat="server">
                                  <td runat="server" style="">
                                  </td>
                              </tr>
                          </table>
                      </LayoutTemplate>
                  </asp:ListView>
              </td>
          </tr>
          <tr>
              <td align="center">

                  <asp:Button ID="btnMemorizzaImpostazioniFonti" runat="server" Text="Memorizza" 
                      UseSubmitBehavior="False"  />

              </td>
          </tr>
        </table>
  </asp:Panel>
    
  <asp:Panel ID="tab_vedi" runat="server" Visible="false">
   <table border="1" cellpadding="2" cellspacing="2" width="1024px" >
   <tr>
     <td class="style13">
       <asp:Label ID="Label5" runat="server" Text="Nome Tariffa" CssClass="testo_bold"></asp:Label>
       <asp:Label ID="idTariffaVisualizzata" runat="server" Text='0' Visible="false" />
     </td>
     <td class="style15">
         <asp:Label ID="Label31" runat="server" CssClass="testo_bold" Text="Codice"></asp:Label>
     </td>
     <td class="style33">
         <asp:Label ID="Label29" runat="server" CssClass="testo_bold" Text="Tariffa Broker Prepaid"></asp:Label>
     </td>
     <td class="style19">
       <asp:Label ID="Label30" runat="server" Text="Max Sconto Applicabile (Tot.)" CssClass="testo_bold"></asp:Label>
     </td> 
       <td class="style32">
           <asp:Label ID="Label39" runat="server" CssClass="testo_bold" 
               Text="Max Sconto Applicabile (Rack)"></asp:Label>
       </td>
     <td style="width:15%">
        <asp:Label ID="Label36" runat="server" CssClass="testo_bold" 
               Text="Tariffe Web"></asp:Label>
     </td>
     <td style="width:15%">
        <asp:Label ID="Label47" runat="server" CssClass="testo_bold" 
               Text="Tariffe Web Prepagata"></asp:Label>
     </td>     
   </tr>
   <tr>
     <td class="style13">
         <asp:TextBox ID="txtNomeTariffa" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
     </td>
     <td class="style15">
         <asp:TextBox ID="txtCodtar" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
     </td>
     <td class="style33">
         <asp:DropDownList ID="dropBroker" runat="server" AppendDataBoundItems="True">
             <asp:ListItem Selected="True" Value="2">...</asp:ListItem>
             <asp:ListItem Value="1">Si</asp:ListItem>
             <asp:ListItem Value="0">No</asp:ListItem>
         </asp:DropDownList>
     </td>
     <td class="style19">        
         <asp:TextBox ID="txtMaxSconto" runat="server" Width="40px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
     </td>     
       <td class="style32">
           <asp:TextBox ID="txtMaxScontoRack" runat="server" Width="40px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
       </td>
     <td style="width:15%">     
         <asp:DropDownList ID="DropDownListWeb" runat="server" AppendDataBoundItems="True">
             <asp:ListItem Selected="True" Value="2">...</asp:ListItem>
             <asp:ListItem Value="1">Si</asp:ListItem>
             <asp:ListItem Value="0">No</asp:ListItem>
         </asp:DropDownList>
     </td> 
     <td style="width:15%">     
         <asp:DropDownList ID="DropDownListWebPrepagata" runat="server" AppendDataBoundItems="True">
             <asp:ListItem Selected="True" Value="2">...</asp:ListItem>
             <asp:ListItem Value="1">Si</asp:ListItem>
             <asp:ListItem Value="0">No</asp:ListItem>
         </asp:DropDownList>
     </td>   
   </tr>
   <tr>
     <td colspan="7">
        <asp:Label ID="lblCodicePromozionale" runat="server" CssClass="testo_bold" Text="Codice Promozionale:"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtCodicePromozionale" runat="server" Width="100px" 
                    MaxLength="10" ></asp:TextBox>
     </td>
   </tr>
   
   <tr>
     <td colspan="7" align="center">
        <asp:Button ID="btnSalvaIntestazione" runat="server" 
             ValidationGroup="salva_intestazione" Text="Salva Intestazione" 
             UseSubmitBehavior="False" />
         &nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Annulla" UseSubmitBehavior="False" />
         
                  &nbsp;<asp:Button ID="btnFiltra" runat="server" Text="Filtra" UseSubmitBehavior="False" />
     </td>      
   </tr>
   
   
   <tr>
   
                 <td colspan="2">
             
                    <asp:Label ID="Label6" runat="server" CssClass="testo_bold" Text="Valore Tariffa:"></asp:Label>
                     &nbsp;
                     <asp:DropDownList ID="dropTempoKm" runat="server" AppendDataBoundItems="True" 
                         DataSourceID="sqlTempoKm" DataTextField="codice" DataValueField="id" 
                         Height="22px">
                         <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                     </asp:DropDownList>
                     <asp:DropDownList ID="dropTempoKmFiltrato" runat="server" AppendDataBoundItems="True" 
                         DataSourceID="sqlTempoKmFiltrato" DataTextField="codice" DataValueField="id" Height="22px">
                         <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td colspan="5" style="width:50%">

                           <asp:Label ID="Label7" runat="server" CssClass="testo_bold" Text="Condizioni:"></asp:Label>

                           &nbsp;
                           <asp:DropDownList ID="dropCondizioni" runat="server" 
                               AppendDataBoundItems="True" DataSourceID="sqlCondizioni" DataTextField="codice" 
                               DataValueField="id" style="height: 22px">
                               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                           </asp:DropDownList>
                           <asp:DropDownList ID="dropCondizioniFiltrato" runat="server" 
                               AppendDataBoundItems="True" DataSourceID="sqlCondizioniFiltrato" DataTextField="codice" 
                               DataValueField="id" style="height: 22px">
                               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                           </asp:DropDownList>
                 </td>
   </tr>
       <tr>
           <td colspan="2" class="style17">
           </td>
           <td colspan="5" class="style18">
               <asp:Label ID="Label23" runat="server" CssClass="testo_bold" Text="Condizione madre:"></asp:Label>
               &nbsp;<asp:DropDownList ID="dropCondizioneMadre" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlCondizioni" DataTextField="codice" 
                   DataValueField="id" style="height: 22px">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
               &nbsp;<asp:DropDownList ID="dropCondizioniMadreFiltrato" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlCondizioniFiltrato" DataTextField="codice" 
                   DataValueField="id" style="height: 22px">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td class="style17" colspan="2">
               <asp:Label ID="Label33" runat="server" CssClass="testo_bold" 
                   Text="Tariffa Rack:"></asp:Label>
               &nbsp;<asp:DropDownList ID="dropTariffaRack" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlTariffaRack" DataTextField="codice" DataValueField="id" 
                   Height="22px">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
               <%--<asp:DropDownList ID="dropTempoKmRackFiltrato" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlTariffaRack" DataTextField="codice" DataValueField="id" 
                   Height="22px">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>--%>
           </td>
           <td class="style18" colspan="5">
                &nbsp;
          </td>
       </tr>
   </table>
   <table border="1" cellpadding="2" cellspacing="2" width="1024px" class="style21" >
   <tr>
               <td class="style11">
                   <asp:Label ID="Label8" runat="server" Text="Vendibilità da" CssClass="testo_bold"></asp:Label>
               </td>
               <td class="style27">
                 <asp:Label ID="Label9" runat="server" Text="Vendibilità fino a" CssClass="testo_bold"></asp:Label>
               </td>
               <td class="style6">
                 <asp:Label ID="Label10" runat="server" Text="Pick up da" CssClass="testo_bold"></asp:Label>
               </td>
               <td class="style9">
                 <asp:Label ID="Label11" runat="server" Text="Pick up fino a" CssClass="testo_bold"></asp:Label>
               </td>
               <td class="style25">
                 <asp:Label ID="Label12" runat="server" Text="Max. Data Rilascio" CssClass="testo_bold"></asp:Label>
               </td>
               <td class="style26">
                   <asp:Label ID="Label42" runat="server" CssClass="testo_bold" 
                       Text="Min. Giorni Nolo"></asp:Label>
               </td>
               <td class="style22">
                   <asp:Label ID="Label43" runat="server" CssClass="testo_bold" 
                       Text="Max Giorni Nolo"></asp:Label>
               </td>
           </tr>
       <tr>
           <td class="style11">
               <a onclick="Calendar.show(document.getElementById('<%=vendibilitaDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="vendibilitaDa" runat="server" Width="70px"></asp:TextBox></a>
              <%-- <asp:CalendarExtender ID="vendibilitaDa_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="vendibilitaDa">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="vendibilitaDa_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="vendibilitaDa">
               </asp:MaskedEditExtender>
           </td>
           <td class="style27">
               <a onclick="Calendar.show(document.getElementById('<%=vendibilitaA.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="vendibilitaA" runat="server" Width="70px"></asp:TextBox></a>
            <%--   <asp:CalendarExtender ID="vendibilitaA_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="vendibilitaA">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="vendibilitaA_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="vendibilitaA">
               </asp:MaskedEditExtender>
           </td>
           <td class="style6">
                 <a onclick="Calendar.show(document.getElementById('<%=pickUpDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="pickUpDa" runat="server" Width="70px"></asp:TextBox></a>
               <asp:label ID="lbl_pickup_da" runat="server" Width="70px" Visible="false"></asp:label>
              <%-- <asp:CalendarExtender ID="pickUpDa_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="pickUpDa">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="pickUpDa_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="pickUpDa">
               </asp:MaskedEditExtender>
           </td>
           <td class="style9">
                 <a onclick="Calendar.show(document.getElementById('<%=pickUpA.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="pickUpA" runat="server" Width="70px"></asp:TextBox></a>
              <%-- <asp:CalendarExtender ID="pickUpA_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="pickUpA">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="pickUpA_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="pickUpA">
               </asp:MaskedEditExtender>
           </td>
           <td class="style25">
               <a onclick="Calendar.show(document.getElementById('<%=txtMaxDataRilascio.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtMaxDataRilascio" runat="server" Width="70px"></asp:TextBox></a>
             <%--  <asp:CalendarExtender ID="txtMaxDataRilascio_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="txtMaxDataRilascio">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="txtMaxDataRilascio_MaskedEditExtender" runat="server" 
                   Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="txtMaxDataRilascio">
               </asp:MaskedEditExtender>
           </td>
           <td class="style26">
               <asp:TextBox ID="txtMinGiorniNolo" runat="server" 
                   onKeyPress="return filterInputInt(event)" Width="40px"></asp:TextBox>
           </td>
           <td class="style22">
               <asp:TextBox ID="txtMaxGiorniNolo" runat="server" 
                   onKeyPress="return filterInputInt(event)" Width="40px"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td class="style11">
               <asp:Label ID="Label44" runat="server" CssClass="testo_bold" Text="Minuti di Ritardo"></asp:Label>
           </td>
           <td class="style27" colspan="2">
               <asp:Label ID="Label45" runat="server" CssClass="testo_bold" Text="Ulteriore tolleranza (Rientro Vettura RA)"></asp:Label>
           </td>
           <td class="style9">
               <asp:Label ID="Label48" runat="server" CssClass="testo_bold" Text="Max Sconto Applicabile (Tot.)"></asp:Label>
               </td>
           <td class="style25">
               <asp:Label ID="Label50" runat="server" CssClass="testo_bold" Text="ID TempoKM" ></asp:Label></td>
           <td class="style26">
               <asp:Label ID="Label51" runat="server" CssClass="testo_bold" Text="ID Tariffa" ></asp:Label></td>
           <td class="style22">
               &nbsp;</td>
       </tr>
       <tr>
           <td class="style11">
               <asp:TextBox ID="txtMinutiDiRitardo" runat="server" 
                   onKeyPress="return filterInputInt(event)" Width="40px"></asp:TextBox>
           </td>
           <td class="style27" colspan="2">
               <asp:TextBox ID="txtUlterioreTolleranza" runat="server" onKeyPress="return filterInputInt(event)" Width="40px"></asp:TextBox>
           </td>
           <td class="style9">
              <asp:TextBox ID="txt_max_sconto" runat="server" Width="40px"></asp:TextBox></td>  <%--aggiunto salvo 17.01.2023--%>
           <td class="style25">
                <asp:TextBox ID="txt_id_tempokm" runat="server" Width="60px" Enabled="false"></asp:TextBox></td>  <%--aggiunto salvo 18.01.2023--%>
           <td class="style26">
               &nbsp;
                <asp:TextBox ID="txt_id_tariffa" runat="server" Width="60px" Enabled="false"></asp:TextBox></td>  <%--aggiunto salvo 09.02.2023--%>
           </td>
           <td class="style22">
               &nbsp;</td>
       </tr>
       <tr>
           <td class="style2" colspan="6" style="text-align:left;">
            <asp:Label ID="lbl_list_contratti" runat="server" Text="" CssClass="testo_bold"></asp:Label>
               &nbsp;<br />
               
           </td>
           <td align="center" class="style2" colspan="6" style="text-align:center;" >
                <asp:Label ID="lbl_modifica_admin" runat="server" Text="Modifica Admin" CssClass="testo_bold"></asp:Label>
                    <asp:CheckBox ID="ck_modifica_admin" runat="server" />
               </td>
       </tr>
       <tr>
           <td align="center" class="style2" colspan="7">
               
               &nbsp;<asp:Button ID="btnInserisci" runat="server" Text="Inserisci" ValidationGroup="invia_lista" Width="60pt" UseSubmitBehavior="False" />
               &nbsp;<asp:Button ID="btnAnnullaModifica" runat="server" Text="Annulla Modifica" visible="false" UseSubmitBehavior="False" /> 
               <asp:Button ID="btnSalve" runat="server" ValidationGroup="salva_intestazione" Text="Salva Tariffa" UseSubmitBehavior="False" />
               &nbsp;<br />
           </td>
       </tr>
      </table>
   <table border="1" cellpadding="2" cellspacing="2" width="1024px" >
       <tr>
         <td style="background-color:White;" align="center">
           <asp:Label ID="Label13" runat="server" Text="Dettagli Tariffa" CssClass="testo_bold"></asp:Label>
         </td>      
       </tr>
       <tr>
           <td align="left" valign="top">
               <asp:DataList ID="listRigheTariffa" runat="server" DataKeyField="id" DataSourceID="sqlDettagliTariffa" Width="100%">
                   <ItemTemplate>
                     <tr>
                       <td bgcolor="#19191b" style="width:100%;">
                      <asp:Label ID="Label12" runat="server" Text="Vendibilità:" CssClass="testo_bold" ForeColor="#FFFFFF"></asp:Label>
                         <asp:Label ID="vendibilita_daLabel" runat="server" Text='<%# Eval("vendibilitaDa") %>' CssClass="testo" ForeColor="#FFFFFF" />&nbsp;-&nbsp;
                         <asp:Label ID="vendibilita_aLabel" runat="server" Text='<%# Eval("vendibilita_a") %>' CssClass="testo" ForeColor="#FFFFFF" />&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label14" runat="server" Text="PickUp:" CssClass="testo_bold" ForeColor="#FFFFFF"></asp:Label>
                         <asp:Label ID="pickup_daLabel" runat="server" Text='<%# Eval("pickup_da") %>' CssClass="testo" ForeColor="#FFFFFF" />&nbsp;-&nbsp;
                         <asp:Label ID="pickup_aLabel" runat="server" Text='<%# Eval("pickup_a") %>' CssClass="testo" ForeColor="#FFFFFF" />
                       </td>
                       <td bgcolor="#19191b" style="width:100%;"></td>
                       <td bgcolor="#19191b" style="width:100%;"></td>
                       <td bgcolor="#19191b" style="width:100%;"></td>
                     </tr>
                     <tr>
                       <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="id_tempo_kmLabel" runat="server" Text='<%# Eval("id_tempo_km") %>' Visible="false" />
                        <asp:Label ID="Label15" runat="server" Text="Valore Tariffa:" CssClass="testo_bold"></asp:Label>
                          &nbsp;<asp:Label ID="tempo_km" runat="server" Text='<%# Eval("tempo_km") %>' CssClass="testo" />
                          <asp:Label ID="lbl_id_condizione" runat="server" Text='<%# Eval("id_condizione") %>' Visible="false" />
                        <asp:Label ID="Label16" runat="server" Text="Condizione:" CssClass="testo_bold"></asp:Label>
                          &nbsp;<asp:Label ID="lblCondizione" runat="server" Text='<%# Eval("condizione") %>' CssClass="testo" /> 
                       </td>
                       <td>
                         <asp:LinkButton ID="btnStampaTariffa" runat="server" CommandName="stampa">Stampa</asp:LinkButton>
                         <%--<a target="_blank" href="GeneraPdf.aspx?pagina=orizzontale&DocPdf=/stampe/stampa_tariffa&id_cond=<%# Eval("id_condizione") %>&id_tempo_km=<%# Eval("id_tempo_km") %>&id_cond_madre=<%# Eval("id_condizione_madre") %>">Stampa</a>--%>
                       </td>
                       <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vediDettaglioTariffa" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="eliminaDettaglioTariffa"
                              OnClientClick="javascript: return(window.confirm ('Vuoi cancellare la tariffa selezionata?\n\nAttenzione: la tariffa verrà cancellata solamente se non già utilizzata.'));" />
                      </td>
                     </tr>
                     <tr>
                       <td>
                        <asp:Label ID="idTariffaMadre" runat="server" Text='<%# Eval("id_condizione_madre") %>' Visible="false" />
                        <asp:Label ID="Label28" runat="server" Text="Condizione Madre:" CssClass="testo_bold"></asp:Label>    
                        &nbsp;<asp:Label ID="Lb_condizione_madre" runat="server" Text='<%# Eval("cond_madre") %>' CssClass="testo" />
                         <asp:Label ID="Label25" runat="server" Text="Minuti di ritardo:" CssClass="testo_bold"></asp:Label>
                        &nbsp;<asp:Label ID="Lb_minuti_di_ritardo" runat="server" Text='<%# Eval("minuti_di_ritardo") %>' CssClass="testo" />
                        <asp:Label ID="Label22" runat="server" Text="Ulteriore Tolleranza:" CssClass="testo_bold"></asp:Label>
                        &nbsp;<asp:Label ID="Lb_tolleranza_rientro_nolo" runat="server" Text='<%# Eval("tolleranza_rientro_nolo") %>' CssClass="testo" />
                         <asp:Label ID="Label17" runat="server" Text="Max Data Rientro:" CssClass="testo_bold"></asp:Label>
                        &nbsp;<asp:Label ID="lblMaxData" runat="server" Text='<%# Eval("max_data_rientro") %>' CssClass="testo" />
                         <asp:Label ID="Label35" runat="server" Text="Min. Giorni Nolo:" CssClass="testo_bold"></asp:Label>
                        &nbsp;<asp:Label ID="min_giorni_nolo" runat="server" Text='<%# Eval("min_giorni_nolo") %>' CssClass="testo" />
                        <asp:Label ID="Label37" runat="server" Text="Max. Giorni Nolo:" CssClass="testo_bold"></asp:Label>
                        &nbsp;<asp:Label ID="max_giorni_nolo" runat="server" Text='<%# Eval("max_giorni_nolo") %>' CssClass="testo" />
                        &nbsp;<asp:Label ID="Label49" runat="server" Text="Max. Sconto:" CssClass="testo_bold"></asp:Label>
                          &nbsp;<asp:Label ID="max_sconto_t" runat="server" Text='<%# Eval("max_sconto") %>' CssClass="testo" />
                           
                       </td>
                       <td></td>
                         <%--<a href="WebService/">WebService/</a>--%>
                       <td></td>  
                     </tr>  
                     <tr>
                       <td>
                        <asp:Label ID="id_tariffa_rack" runat="server" Text='<%# Eval("id_tariffa_rack") %>' Visible="false" />
                        <asp:Label ID="Label38" runat="server" Text="Val. Tariffa Rack:" CssClass="testo_bold"></asp:Label>   
                        &nbsp;<asp:Label ID="Label26" runat="server" Text='<%# Eval("tariffa_rack") %>' CssClass="testo" />
                       </td>
                       <td></td>
                       <td></td>  
                     </tr>
                     
                   </ItemTemplate>
               </asp:DataList>
           </td>
          
       </tr>
</table>
</asp:Panel>
   
    <asp:Label ID="idTariffa" runat="server" visible="false"></asp:Label>
    <asp:Label ID="tariffaBroker" runat="server" visible="false"></asp:Label>
    <asp:Label ID="where_tempoKm" runat="server" visible="false"></asp:Label>
    <asp:Label ID="filtra" runat="server" visible="false"></asp:Label>
    <asp:Label ID="lblQuery" runat="server"  visible="false" ></asp:Label>

    <%--<asp:SqlDataSource ID="sqlTariffe" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT tariffe.id, tariffe.data_creazione, tariffe.codice,tariffe.CODTAR, tariffe.max_sconto, tariffe.max_sconto_rack, tariffe.is_broker_prepaid, CONVERT(Char(10),tariffe_righe.vendibilita_da,103) As vendibilita_da, CONVERT(Char(10),tariffe_righe.vendibilita_a,103) As vendibilita_a, CONVERT(Char(10),tariffe_righe.pickup_Da,103) As pickup_Da, CONVERT(Char(10),tariffe_righe.pickup_a,103) As pickup_a FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE attiva='1' ORDER BY data_creazione DESC"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="sqlTariffe" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, data_creazione, codice,CODTAR, max_sconto, max_sconto_rack, is_broker_prepaid,is_web,is_web_prepagato, codicepromozionale FROM tariffe WITH(NOLOCK) WHERE attiva='1' ORDER BY data_creazione DESC"></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="sqlCondizioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT (codice + ' - ' + convert(char(10),valido_da,103) + ' - ' + convert(char(10),valido_a,103)) As codice,id, valido_da FROM condizioni WITH(NOLOCK) WHERE attivo='1' ORDER BY codice, valido_da">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTempoKm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT (codice + ' - ' + convert(char(10),valido_da,103) + ' - ' + convert(char(10),valido_a,103)) As codice,id, valido_da FROM tempo_km WITH(NOLOCK) ORDER BY codice, valido_da">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTempoKmFiltrato" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT (codice + ' - ' + convert(char(10),valido_da,103) + ' - ' + convert(char(10),valido_a,103)) As codice,id, valido_da FROM tempo_km WITH(NOLOCK) WHERE attivo='1' AND (getDate() BETWEEN valido_da AND valido_a OR valido_da >=getDate()) ORDER BY codice, valido_da">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlCondizioniFiltrato" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT (codice + ' - ' + convert(char(10),valido_da,103) + ' - ' + convert(char(10),valido_a,103)) As codice,id, valido_da FROM condizioni WITH(NOLOCK) WHERE attivo='1' AND (getDate() BETWEEN valido_da AND valido_a OR valido_da >=getDate()) ORDER BY codice, valido_da">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTariffaRack" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE attiva='1'">
    </asp:SqlDataSource>
<%--    <asp:SqlDataSource ID="sqlTipoTariffa" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM tipo_tariffa WITH(NOLOCK) ORDER BY [descrizione]">
    </asp:SqlDataSource>--%>

    <asp:SqlDataSource ID="sqlDettagliTariffa" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" SelectCommand="SELECT tariffe_righe.max_sconto ,tariffe_righe.id, minuti_di_ritardo, tolleranza_rientro_nolo, id_condizione_madre, tariffe_righe.id_tariffa_rack, (condizione_madre.codice + ' - ' + CONVERT(char(10),condizione_madre.valido_da,103) + ' - ' + CONVERT(char(10),condizione_madre.valido_a,103)) As cond_madre, (tempo_km.codice + ' - ' + CONVERT(char(10),tempo_km.valido_da,103) + ' - ' + CONVERT(char(10),tempo_km.valido_a,103)) As tempo_km, (tariffa_rack.codice) As tariffa_rack, id_tempo_km, (condizione_figlia.codice + ' - ' + CONVERT(char(10),condizione_figlia.valido_da,103) + ' - ' + CONVERT(char(10),condizione_figlia.valido_a,103)) As condizione, id_condizione, convert(char(10),vendibilita_da,103) As vendibilitaDa, convert(char(10),vendibilita_a,103) As vendibilita_a, convert(char(10),pickup_da,103) As pickup_da, convert(char(10),pickup_a,103) As pickup_a, CONVERT(char(10),max_data_rientro,103) As max_data_rientro, min_giorni_nolo, max_giorni_nolo FROM tariffe_righe WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON tariffe_righe.id_tempo_km=tempo_km.id LEFT JOIN tariffe As tariffa_rack WITH(NOLOCK) ON tariffe_righe.id_tariffa_rack=tariffa_rack.id INNER JOIN condizioni As condizione_figlia WITH(NOLOCK) ON tariffe_righe.id_condizione=condizione_figlia.id LEFT JOIN condizioni As condizione_madre WITH(NOLOCK) ON tariffe_righe.id_condizione_madre=condizione_madre.id WHERE ([id_tariffa] = @id_tariffa) ORDER BY tariffe_righe.pickup_da DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="idTariffa" Name="id_tariffa" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
                            
    <asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [descrizione], [id] FROM clienti_tipologia WITH(NOLOCK) WHERE broker=@is_tariffa_broker OR @is_tariffa_broker='0' ORDER BY [descrizione]">
        <SelectParameters>
            <asp:ControlParameter ControlID="tariffaBroker" Name="is_tariffa_broker" PropertyName="Text" Type="String" />
        </SelectParameters>
      </asp:SqlDataSource>
                            
    <asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (STR(codice) + ' - ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) ORDER BY [codice]">
    </asp:SqlDataSource>
                           
                            
    <asp:SqlDataSource ID="sqlRegoleXFonti" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT tariffe_X_fonti.id, clienti_tipologia.descrizione As cliente , CONVERT(Char(10), tariffe_X_fonti.valido_da, 103) As valido_da, tariffe_X_fonti.nome_tariffa FROM tariffe_X_fonti WITH(NOLOCK) INNER JOIN clienti_tipologia ON tariffe_x_fonti.id_tipologia_cliente=clienti_tipologia.id WHERE tariffe_x_fonti.id_tariffa=@id_tariffa">
       <SelectParameters>
            <asp:ControlParameter ControlID="idTariffa" Name="id_tariffa"  PropertyName="Text" Type="String" />
       </SelectParameters>
    </asp:SqlDataSource>                        
                            
               <asp:CompareValidator ID="compareTempoKm" runat="server" 
                    ControlToValidate="dropTempoKm" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia_lista" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare un Tempo Km."> </asp:CompareValidator>
               <asp:CompareValidator ID="compareTempoKmFiltrato" runat="server" 
                    ControlToValidate="dropTempoKmFiltrato" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia_lista" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare un Tempo Km."> </asp:CompareValidator>
               
               <asp:CompareValidator ID="compareCondizioni" runat="server" 
                    ControlToValidate="dropCondizioni" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia_lista" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare una Condizione."> </asp:CompareValidator>
               <asp:CompareValidator ID="compareCondizioniFiltrato" runat="server" 
                    ControlToValidate="dropCondizioni" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia_lista" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare una Condizione."> </asp:CompareValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="vendibilitaDa" ErrorMessage="Specificare la data iniziale di vendibilità." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                ControlToValidate="vendibilitaDa" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data iniziale di vendibilità'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="vendibilitaA" ErrorMessage="Specificare la data finale di vendibilità." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="vendibilitaA" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data finale di vendibilità'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="txtMaxDataRilascio" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per Massima data di rilascio'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareFieldValidator2" runat="server"
                                 ControlToValidate="vendibilitaA"
                                 ControlToCompare="vendibilitaDa"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale di vendibilità è precedente alla data iniziale."
                                 ValidationGroup="invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareValidator7" runat="server"
                                 ControlToValidate="txtMaxGiorniNolo"
                                 ControlToCompare="txtMinGiorniNolo"
                                 Type= "Integer"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : il campo giorni massimi di noleggio deve essere successivo al minimo giorni di nolo."
                                 ValidationGroup="invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="pickUpDa" ErrorMessage="Specificare la data iniziale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="pickUpDa" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data iniziale di pick up'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                               ControlToValidate="pickUpA" ErrorMessage="Specificare la data finale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="pickUpA" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data finale di pick up'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareValidator4" runat="server"
                                 ControlToValidate="pickUpA"
                                 ControlToCompare="pickUpDa"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale di pick up è precedente alla data iniziale."
                                 ValidationGroup="invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>  

                            <%--salvo 08.02.2023 rimossa verifica in quanto la data di pick up deve rimanere fissa ma può essere estesa la data di drop off--%>
                            <%-- <asp:CompareValidator id="CompareValidator5" runat="server"
                                 ControlToValidate="pickUpdA"
                                 ControlToCompare="vendibilitaDa"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data iniziale di pick up non può essere precendete alla data iniziale di vendibilità."
                                 ValidationGroup="invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>   --%>
                             
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                               ControlToValidate="txtMinutiDiRitardo" ErrorMessage="Specificare i minuti di ritardo." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator> 
                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                               ControlToValidate="txtUlterioreTolleranza" ErrorMessage="Specificare i minuti di tolleranza extra (per rientro della vettura su RA)." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>  
                               
                             <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                  ControlToValidate="txtMinutiDiRitardo" 
                                  Font-Size="0pt" Operator="GreaterThanEqual" Type="Integer"
                                  ValidationGroup="invia_lista" ValueToCompare="0" ErrorMessage="Specificare un valore corretto per i Minuti di ritardo consentiti."> </asp:CompareValidator>
  
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="invia_lista" />
  
  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                               ControlToValidate="txtNomeTariffa" ErrorMessage="Specificare il nome della tariffa." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="salva_intestazione"> </asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                               ControlToValidate="txtMaxSconto" ErrorMessage="Specificare il massimo sconto applicabile sul totale." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="salva_intestazione"> </asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                               ControlToValidate="txtMaxScontoRack" ErrorMessage="Specificare il massimo sconto applicabile sulla tariffa rack." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="salva_intestazione"> </asp:RequiredFieldValidator>
  <asp:CompareValidator ID="CompareValidator10" runat="server" 
                                  ControlToValidate="txtMaxSconto" 
                                  Font-Size="0pt" Operator="GreaterThanEqual" Type="Double" 
                                  ValidationGroup="salva_intestazione" ValueToCompare="0" ErrorMessage="Specificare un valore corretto per il massimo sconto applicabile."> </asp:CompareValidator>
  <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                  ControlToValidate="txtMaxSconto" 
                                  Font-Size="0pt" Operator="LessThanEqual" Type="Double" 
                                  ValidationGroup="salva_intestazione" ValueToCompare="100" ErrorMessage="Specificare un valore corretto per il massimo sconto applicabile."> </asp:CompareValidator>
  
  
  <%--<asp:CompareValidator ID="CompareValidator15" runat="server" 
                    ControlToValidate="dropPagamento" 
                    Operator="NotEqual"  Type="Integer" 
                    ValidationGroup="salva_intestazione" ValueToCompare="2" 
                    Font-Size="0pt" ErrorMessage="Specificare il tipo di pagamento per la prenotazione."> </asp:CompareValidator>--%>
 
  <asp:CompareValidator ID="CompareValidator12" runat="server" 
                    ControlToValidate="dropBroker" 
                    Operator="NotEqual"  Type="Integer" 
                    ValidationGroup="salva_intestazione" ValueToCompare="2" 
                    Font-Size="0pt" ErrorMessage="Specificare se la tariffa è dedicata ai Broker."> </asp:CompareValidator>

 <asp:CompareValidator ID="CompareValidator8" runat="server" 
                    ControlToValidate="DropDownListWeb" 
                    Operator="NotEqual"  Type="Integer" 
                    ValidationGroup="salva_intestazione" ValueToCompare="2" 
                    Font-Size="0pt" ErrorMessage="Specificare se la tariffa è dedicata al web con pagamento al banco."> </asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator13" runat="server" 
                    ControlToValidate="DropDownListWebPrepagata" 
                    Operator="NotEqual"  Type="Integer" 
                    ValidationGroup="salva_intestazione" ValueToCompare="2" 
                    Font-Size="0pt" ErrorMessage="Specificare se la tariffa è dedicata al web con pagamento on-line."> </asp:CompareValidator>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
        ControlToValidate="txtCodtar" 
        ErrorMessage="Specificare un codice tariffa." Font-Size="0pt" 
        SetFocusOnError="True" ValidationGroup="salva_intestazione"> </asp:RequiredFieldValidator>

  
  <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="salva_intestazione" />    

</asp:Content>

