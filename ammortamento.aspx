<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="ammortamento.aspx.vb" Inherits="fondo_ammortamento" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .style2
        {
            width: 131px;
        }
        .style4
        {
        }
        .style7
        {
            text-align: right;
            color: #000000;
            font-family: Verdana;
            font-size: x-small;
            }
                
        .style10
        {
            width: 65px;
        }
                
        .style11
        {
            width: 61px;
        }
                
        .style12
        {
            text-align: right;
            color: #000000;
            font-family: Verdana;
            font-size: x-small;
            }
                
        .style13
        {
            text-align: right;
            color: #000000;
            font-family: Verdana;
            font-size: x-small;
            width: 121px;
        }
                
        .style14
        {
            text-align: right;
            color: #000000;
            font-family: Verdana;
            font-size: x-small;
            width: 83px;
        }
                
        .style15
        {
            width: 83px;
        }
        .style16
        {
            width: 129px;
        }
        .style17
        {
            text-align: right;
            color: #000000;
            font-family: Verdana;
            font-size: x-small;
            width: 113px;
        }
                
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Fondo di ammortamento</b>
           </td>
         </tr>
</table>

   <table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
     <tr>
       <td class="style13" style="padding: 1px">
           <asp:Label ID="lblDaAmmortizzareAl" runat="server" Text="Ammort. al:" CssClass="testo_bold"></asp:Label>
       </td>
       <td class="style2" style="padding: 1px">
           <a onclick="Calendar.show(document.getElementById('<%=txtDataAmmortamento.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtDataAmmortamento" runat="server" Width="70px"></asp:TextBox></a>
             <%--  <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataAmmortamento">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataAmmortamento">
                </ajaxtoolkit:MaskedEditExtender>    
       </td>
       <td class="style14" style="padding: 1px">
         <asp:Label ID="lblTipoAmmortamento" runat="server" Text="Tipo amm.:" CssClass="testo_bold"></asp:Label>
       </td>
       <td class="style16" style="padding: 1px">

           <asp:DropDownList ID="dropTipoAmmortamento" runat="server" AutoPostBack="True">
               <asp:ListItem Value="0">Normale</asp:ListItem>
               <asp:ListItem Value="1">Decelerato</asp:ListItem>
               <asp:ListItem Value="2">Accelerato</asp:ListItem>
               <asp:ListItem Selected="True" Value="3">Fisso</asp:ListItem>
           </asp:DropDownList>
       </td>
       <td class="style17" style="padding: 1px">
         <b>Valore:</b>
       </td>
       <td style="padding: 1px">
           <asp:TextBox ID="txtValore" runat="server" Width="43px"></asp:TextBox>
       </td>
     </tr>
     <tr>
       <td class="style13" style="padding: 1px">
         <asp:Label ID="lblDataAcquistoDa" runat="server" Text="Data acquisto da:" CssClass="testo_bold"></asp:Label>&nbsp;
       </td>
       <td class="style2" style="padding: 1px">
           <a onclick="Calendar.show(document.getElementById('<%=acquistoDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="acquistoDa" runat="server" Width="70px"></asp:TextBox></a>
           <%--    <ajaxtoolkit:CalendarExtender ID="txtDataAmmortamento0_CalendarExtender" 
                   runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="acquistoDa">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="txtDataAmmortamento0_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="acquistoDa">
                </ajaxtoolkit:MaskedEditExtender>    
         </td>
       <td align="left" class="style14">
               <asp:Label ID="lblDataAcquistoA" runat="server" Text="a" CssClass="testo_bold"></asp:Label>
         </td>
       <td class="style16" style="padding: 1px">
           <a onclick="Calendar.show(document.getElementById('<%=acquistoA.ClientID%>'), '%d/%m/%Y', false)"> 
           <asp:TextBox ID="acquistoA" runat="server" Width="70px"></asp:TextBox></a>
               <%--<ajaxtoolkit:CalendarExtender ID="txtFatturaDa0_CalendarExtender" 
                   runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="acquistoA">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="txtFatturaDa0_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="acquistoA">
                </ajaxtoolkit:MaskedEditExtender>    
         </td>
       <td class="style17" style="padding: 1px">

         <asp:Label ID="lblDataImmatrDa" runat="server" Text="Data immatr. da:" CssClass="testo_bold"></asp:Label>
       </td>
       <td style="padding: 1px" class="style10">
            <a onclick="Calendar.show(document.getElementById('<%=immDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="immDa" runat="server" Width="70px"></asp:TextBox></a>
              <%-- <ajaxtoolkit:CalendarExtender ID="acquistoDa0_CalendarExtender" 
                   runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="immDa">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="acquistoDa0_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="immDa">
                </ajaxtoolkit:MaskedEditExtender>    
         </td>
       <td style="padding: 1px" class="style7">
         <asp:Label ID="lblDataImmatrA" runat="server" Text="a" CssClass="testo_bold"></asp:Label>
       </td>
       <td style="padding: 1px" class="style11">
           <a onclick="Calendar.show(document.getElementById('<%=immA.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="immA" runat="server" Width="70px"></asp:TextBox></a>
               <%--<ajaxtoolkit:CalendarExtender ID="acquistoDa1_CalendarExtender" 
                   runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="immA">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="acquistoDa1_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="immA">
                </ajaxtoolkit:MaskedEditExtender>    
       </td>
       <td style="padding: 1px" class="style12">
         <%--<asp:Label ID="lblLeasing" runat="server" Text="Leasing" CssClass="testo_bold"></asp:Label>--%>
       </td>
       <td style="padding: 1px">
               <%--<asp:DropDownList ID="dropLeasing" runat="server" AppendDataBoundItems="True" >
                   <asp:ListItem Selected="True" Value="-1">Tutte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Si</asp:ListItem>
                   <asp:ListItem Selected="False" Value="0">No</asp:ListItem>
               </asp:DropDownList>--%>
         </td>
     </tr>
     <tr>
       <td class="style13" style="padding: 1px">
         <asp:Label ID="lblProprietario" runat="server" Text="Proprietario:" 
               CssClass="testo_bold"></asp:Label>
       </td>
       <td class="style2" style="padding: 1px">
            
               <asp:DropDownList ID="dropCercaProprietario" runat="server"
                   DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
         </td>
       <td align="right" style="padding: 1px" class="style15">
            
         <asp:Label ID="lblDataImmatrDa0" runat="server" Text="Marca:" 
               CssClass="testo_bold"></asp:Label>
            
         </td>
       <td class="style4" style="padding: 1px" colspan="2">
           <asp:DropDownList ID="dropCercaMarca" runat="server" 
               AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlCercaMarca" 
               DataTextField="descrizione" DataValueField="id" 
               style="margin-left: 0px; height: 22px;">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
           </asp:DropDownList>
         </td>
       <td style="padding: 1px">
         <asp:Label ID="lblDataImmatrDa1" runat="server" Text="Modello:" 
               CssClass="testo_bold"></asp:Label>
         </td>
       <td style="padding: 1px" colspan="4">
               <asp:DropDownList ID="dropCercaModello" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlCercaModello" DataTextField="descrizione" style="margin-left: 0px"
                   DataValueField="id_modello">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
       </td>
     </tr>
   </table>
   
   <table border="0" cellpadding="1" cellspacing="1" width="1024px" >
      <tr>
        <td align="center">
              <asp:Button ID="btnRicerca" runat="server" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" 
                  Text="Ricerca" />
                             &nbsp;
             <asp:Button ID="btnAggiorna" runat="server" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" 
                  Text="Aggiorna Fondo"  OnClientClick="javascript: return(window.confirm ('Il calcolo del fondo di ammortamento per il periodo selezionato è irreversibile e verrà applicato a tutti i veicoli. \n\nSei sicuro di voler procedere?'));" />       
        
        &nbsp;<asp:Button ID="btnReport" runat="server" Text="Report" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />

        </td>
      </tr>
      <tr runat="server" id="RigaTotaleRicerca" visible="false">
        <td align="left" style=" font-size:14px;">
         TOTALE ACQUISTI <b><asp:Label ID="totaleAcquisti" runat="server" Text="0" ></asp:Label></b>
        &nbsp;&nbsp; -&nbsp;&nbsp;
        TOTALE FONDO AMM. <b><asp:Label ID="totaleFondoAttuale" runat="server" Text="0" ></asp:Label></b>
         &nbsp;&nbsp; -&nbsp;&nbsp;
        TOTALE FONDO AMM. PREVISTO <b><asp:Label ID="totaleFondoPrevisto" runat="server" Text="0" ></asp:Label></b>
         &nbsp;&nbsp; -&nbsp;&nbsp;
        TOTALE AMM. PREVISTO <b><asp:Label ID="totaleAmmPrevisto" runat="server" Text="0" ></asp:Label></b>
        </td>
      </tr>
      <tr>
        <td align="left">
        
              <asp:ListView ID="listAutoAmmortamento" runat="server" DataKeyNames="id" DataSourceID="sqlVeicoliAmmortamento" Visible="false">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000; font-size:small">
                          <td valign="top" width="20%">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="leasing" runat="server" Text='<%# Eval("leasing") %>' Visible="false" />
                              <asp:Label ID="data_ammortamentoLabel" runat="server" Text='<%# Eval("data_ammortamento") %>' Visible="false" />
                              <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "/parcoVeicoli.aspx?veicolo=" & Eval("id") %>'><asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' /></asp:HyperLink><br /><asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <td valign="top" align="left">
                              <br />
                              <%--<asp:CheckBox ID="chkLeasing" runat="server" Checked="false" Enabled="false" />--%>
                          </td>
                          <td valign="top">
                             <b>N:</b> <asp:Label ID="lblNFattAcq" runat="server"  />
                             <br />
                             <b>D:</b> <asp:Label ID="dataFattAcq" runat="server"  />
                          </td>
                          <td>
                            <b>Tot.Fatt.Acq.</b> <asp:Label ID="lblTotFatt" runat="server"></asp:Label><br /><b>Tot.N.C..Acq.</b> <asp:Label ID="lblTotNC" runat="server"></asp:Label><br /><b>Tot.Acquisto.</b> <asp:Label ID="lblTotAcq" runat="server"></asp:Label></td><td valign="top">
                              <asp:Label ID="data_immatricolazioneLabel" runat="server" Text='<%# Eval("data_immatricolazione") %>' />
                          </td>
                          <td valign="top">
                              <asp:Label ID="fondo_ammortamentoLabel" runat="server" Text='<%# Eval("fondo_ammortamento") %>' />
                          </td>
                          <td valign="top">
                              <asp:Label ID="fondoPrevisto" runat="server" />
                          </td>
                          <td valign="top">
                              <asp:Label ID="ammPrevisto" runat="server" />
                          </td>
                          <td valign="top" align="center">
                              <asp:CheckBox ID="chkAdeguato" runat="server" Checked="false" Enabled="false" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="font-size:small">
                          <td valign="top">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="leasing" runat="server" Text='<%# Eval("leasing") %>' Visible="false" />
                              <asp:Label ID="data_ammortamentoLabel" runat="server" Text='<%# Eval("data_ammortamento") %>' Visible="false" />
                              <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "/parcoVeicoli.aspx?veicolo=" & Eval("id") %>'><asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' /></asp:HyperLink><br /><asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <td valign="top" align="left">
                              <br />
                             <%-- <asp:CheckBox ID="chkLeasing" runat="server" Checked="false" Enabled="false" />--%>
                          </td>
                          <td valign="top">
                             <b>N:</b> <asp:Label ID="lblNFattAcq" runat="server"  />
                             <br />
                             <b>D:</b> <asp:Label ID="dataFattAcq" runat="server"  />
                          </td>
                          <td>
                            <b>Tot.Fatt.Acq.</b> <asp:Label ID="lblTotFatt" runat="server"></asp:Label><br /><b>Tot.N.C. Acq.</b> <asp:Label ID="lblTotNC" runat="server"></asp:Label><br /><b>Tot.Acquisto.</b> <asp:Label ID="lblTotAcq" runat="server"></asp:Label></td><td valign="top">
                              <asp:Label ID="data_immatricolazioneLabel" runat="server" Text='<%# Eval("data_immatricolazione") %>' />
                          </td>
                          <td valign="top">
                              <asp:Label ID="fondo_ammortamentoLabel" runat="server" Text='<%# Eval("fondo_ammortamento") %>' />
                          </td>
                          <td valign="top">
                              <asp:Label ID="fondoPrevisto" runat="server" />
                          </td>
                          <td valign="top">
                              <asp:Label ID="ammPrevisto" runat="server"  />
                          </td>
                          <td valign="top" align="center">
                              <asp:CheckBox ID="chkAdeguato" runat="server" Checked="false" Enabled="false" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="0" width="100%" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                      <tr runat="server" style="color: #FFFFFF; font-size:x-small" bgcolor="#19191b">
                                          <th runat="server" align="left">
                                              Targa/Modello</th><th ></th>
                                         <th id="Th3" runat="server" align="left">
                                              Dati Prima<br /> Fatt. Acquisto </th><th id="Th4" runat="server" align="left">
                                         </th>
                                          <th runat="server" align="left">
                                              Data Imm.</th><th runat="server" align="left">
                                              Fondo<br />Amm. Attuale </th><th id="Th1" runat="server" align="left">
                                              Fondo<br />Amm. Previsto </th><th id="Th2" runat="server" align="left">
                                             Amm. Previsto </th><th id="Th5" runat="server" align="left">
                                             Adeguato </th></tr><tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr runat="server">
                              <td runat="server" style="">
                                 <asp:DataPager ID="DataPager1" runat="server" PageSize="5000">
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
   
   
    <%--<ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>--%>

    <asp:SqlDataSource ID="sqlVeicoliAmmortamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT veicoli.id, veicoli.targa, (marche.descrizione + ' ' + modelli.descrizione) As modello, CONVERT(CHAR(10),veicoli.data_immatricolazione,105) As data_immatricolazione, veicoli.fondo_ammortamento, veicoli.data_ammortamento, proprietari_veicoli.descrizione As leasing FROM veicoli WITH(NOLOCK) LEFT JOIN proprietari_veicoli WITH(NOLOCK) On veicoli.id_proprietario=proprietari_veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello LEFT JOIN marche WITH(NOLOCK) ON modelli.id_CasaAutomobilistica=marche.id WHERE veicoli.id>0 "></asp:SqlDataSource>
    
      
    <asp:SqlDataSource ID="sqlProprietariVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [descrizione], [id] FROM proprietari_veicoli WITH(NOLOCK) ORDER BY [descrizione]"></asp:SqlDataSource>
    
      
    <asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaMarca" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM marche WITH(NOLOCK) ORDER BY [descrizione]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaModello" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_modello, descrizione FROM modelli WITH(NOLOCK) WHERE (id_CasaAutomobilistica = @id_marca) ORDER BY descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropCercaMarca" Name="id_marca" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
      
    <asp:Label ID="txtQuery" runat="server" Visible="false"></asp:Label><asp:Label ID="txtPrimaQuery" runat="server" Visible="false"></asp:Label><br />
    
      
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                   ControlToValidate="txtDataAmmortamento" ErrorMessage="Specificare la data di ammortamento." 
                   Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator14" runat="server" 
                    ControlToValidate="txtDataAmmortamento" 
                    Font-Size="0pt"  Operator="DataTypeCheck"
                    ErrorMessage="Specificare una data di ammortamento corretta." Type="Date" ValidationGroup="invia"> </asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="txtValore" ErrorMessage="Specificare il valore dell'ammortamento." 
                   Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtValore" 
        ErrorMessage="Specificare un valore corretto per il campo Percentuale valore ammortamento." Font-Size="0pt" 
        Type="Double" ValidationGroup="invia" 
        MinumumValue="0" MaximumValue="100" > </asp:RangeValidator><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
         DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
         ValidationGroup="invia" />
</asp:Content>

