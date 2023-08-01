<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestioneAssicurazioni.aspx.vb" Inherits="gestioneAssicurazioni" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
    <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
    
    <style>
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
     </style>
    <style type="text/css">

        .style37
        {
            font-family: Courier New;
            font-size: small;
            color: #000000;
            }
        .style50
        {
        width: 120px;
    }
        .style53
        {
            width: 97px;
        }
        .style28
        {
            width: 96px;
        }
        .style4
        {
            color: #000000;
            font-weight: bold;
            }
        .style49
        {
            width: 120px;
            height: 25px;
        }
        .style33
        {
            width: 126px;
            height: 25px;
        }
        .style8
        {
            width: 72px;
            height: 19px;
        }
        .style9
        {
            height: 19px;
        }
        .style54
        {
            width: 106px;
        }
        .style55
        {
            width: 106px;
            height: 25px;
        }
        .style57
        {
            color: #000000;
            font-weight: bold;
            width: 72px;
        }
        .style58
        {
            width: 72px;
        }
        .style59
        {
            width: 126px;
        }
        .style60
    {
        width: 132px;
    }
    .style61
    {
        width: 132px;
        height: 25px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" 
                 class="style1">
             <b>Gestione Assicurazioni</b>
           </td>
         </tr>
</table>
<table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">
        <tr>
          <td align="left" class="style37" colspan="2">
              <asp:Label ID="LblTarga0" runat="server" Text="Compagnia" CssClass="testo_bold"></asp:Label>
           </td>
          <td align="left" class="style50">
              <asp:Label ID="LblTarga" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
            </td>
          <td align="left" class="style60">
              <asp:Label ID="LblProprietario" runat="server" Text="Proprietario" CssClass="testo_bold"></asp:Label>
            </td>
          <td align="left" class="style59">
               &nbsp;<asp:Label ID="LblProprietario0" runat="server" Text="Venduta A" 
                   CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style53">
               &nbsp;<asp:Label ID="LblProprietario1" runat="server" Text="Vendita N." 
                   CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style28">
               &nbsp;<asp:Label ID="LblProprietario2" runat="server" Text="Vendita Del" 
                   CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style2">
               &nbsp;<asp:Label ID="LblProprietario3" runat="server" Text="Stato" 
                   CssClass="testo_bold"></asp:Label>
         </td>
        </tr>
        <tr>
          <td align="left" class="style4" colspan="2">
               <asp:DropDownList ID="dropCompagnia" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlCompagnieAssicurative" DataTextField="compagnia" 
                   DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona</asp:ListItem>
               </asp:DropDownList>
            </td>
          <td align="left" class="style50">
            
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="85px"></asp:TextBox>
            </td>
          <td align="left" class="style60">            
               <asp:DropDownList ID="dropProprietario" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            </td>
          <td align="left" class="style59">
               <asp:DropDownList ID="dropVenditaA" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlVenditaA" 
                   DataTextField="descrizione" DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
          </td>
          <td align="left" class="style53">
            
               <asp:TextBox ID="txtVenditaNumero" runat="server" Width="85px"></asp:TextBox>
            </td>
          <td align="left" class="style28">
             <a onclick="Calendar.show(document.getElementById('<%=txtVenditaDel.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtVenditaDel" runat="server" Width="70px"></asp:TextBox>
                 </a>

            <%--   <asp:CalendarExtender ID="txtVenditaDel_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtVenditaDel">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtVenditaDel_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtVenditaDel">
                </asp:MaskedEditExtender>    
                         
           
           
            </td>
          <td align="left" class="style2">
              <asp:DropDownList ID="dropStato" runat="server" AppendDataBoundItems="True">
                  <asp:ListItem Value="0">Tutte</asp:ListItem>
                  <asp:ListItem Value="1">Attive</asp:ListItem>
                  <asp:ListItem Value="2">Escluse</asp:ListItem>
              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td align="left" class="style58" >
              </td>
          <td align="left" class="style54" >
              <asp:Label ID="LblBolloVendita" runat="server" Text="N.Ordine" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style50" >
              <asp:Label ID="LblAttoVendita" runat="server" Text="Data Inclusione" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style60" >
              <asp:Label ID="LblFatturaVendita" runat="server" Text="Data Esclusione:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style59" >
               &nbsp;</td>
          <td align="left" class="style53" >
              &nbsp;</td>
          <td align="left" class="style28"  >
              &nbsp;</td>
          <td align="left">
              &nbsp;
          </td>
        </tr>
        <tr>
          <td align="left" class="style58">
              <asp:Label ID="LblDaData" runat="server" Text="Da:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style55">                              
               <asp:TextBox ID="txtOrdineDa" runat="server" Width="70px"></asp:TextBox>
          </td>
          <td align="left" class="style49">
               <a onclick="Calendar.show(document.getElementById('<%=txtDataInclusioneDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtDataInclusioneDa" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%-- <asp:CalendarExtender ID="txtDataInclusioneDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataInclusioneDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataInclusioneDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataInclusioneDa">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style61">
              <a onclick="Calendar.show(document.getElementById('<%=txtDataEsclusioneDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtDataEsclusioneDa" runat="server" Width="70px"></asp:TextBox>
                </a>
              <%-- <asp:CalendarExtender ID="txtDataEsclusioneDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataEsclusioneDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataEsclusioneDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataEsclusioneDa">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left" class="style33">

                       
           
               &nbsp;</td>
          <td align="left" class="style53">

                       
           
               &nbsp;</td>
          <td align="left" class="style28" >

                       
           
               &nbsp;</td>
          <td align="left" >
              </td>
        </tr>
        <tr>
          <td align="left" class="style57">
              <asp:Label ID="LblAData" runat="server" Text="A:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style54">
               <asp:TextBox ID="txtOrdineA" runat="server" Width="70px"></asp:TextBox>           
          </td>
          <td align="left" class="style50">

          <a onclick="Calendar.show(document.getElementById('<%=txtDataInclusioneA.ClientID%>'), '%d/%m/%Y', false)">            
               <asp:TextBox ID="txtDataInclusioneA" runat="server" Width="70px"></asp:TextBox>
              </a>

      <%--         <asp:CalendarExtender ID="txtDataInclusioneA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataInclusioneA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataInclusioneA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataInclusioneA">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style60">

                       
            <a onclick="Calendar.show(document.getElementById('<%=txtDataEsclusioneA.ClientID%>'), '%d/%m/%Y', false)">      
               <asp:TextBox ID="txtDataEsclusioneA" runat="server" Width="70px"></asp:TextBox>
                </a>
             <%--  <asp:CalendarExtender ID="txtDataEsclusioneA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataEsclusioneA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataEsclusioneA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataEsclusioneA">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style59">
              
           
           
               &nbsp;</td>
          <td align="left" class="style53">

                       
           
               &nbsp;</td>
          <td align="left" class="style28">

                       
           
               &nbsp;</td>
          <td align="left" class="style2">
          </td>
        </tr>        
        <tr>
          <td align="left" class="style8">              
               <asp:Label ID="LblReport" runat="server" Text="Report::" CssClass="testo_bold"></asp:Label>               
          </td>
          <td align="left" class="style9" colspan="5">
            
               <asp:DropDownList ID="dropReport" runat="server" AppendDataBoundItems="True" 
                   AutoPostBack="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Lista assicurazioni</asp:ListItem>
                   <%--<asp:ListItem Selected="False" Value="1">Lista con data esclusione</asp:ListItem>--%>
               </asp:DropDownList>
            
                       
                 <a onclick="Calendar.show(document.getElementById('<%=txtDataEsclusioneRiferimento.ClientID%>'), '%d/%m/%Y', false)">  
               <asp:TextBox ID="txtDataEsclusioneRiferimento" runat="server" Width="70px" Visible="false"></asp:TextBox>
                     </a>

             <%--  <asp:CalendarExtender ID="txtDataEsclusioneRiferimento_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataEsclusioneRiferimento">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataEsclusioneRiferimento_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataEsclusioneRiferimento">
                </asp:MaskedEditExtender>    
                         
           
           
               <asp:Button ID="btnReport" runat="server" Text="Stampa Report" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />
               &nbsp;
               <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />
               &nbsp;
              <asp:Button ID="btncancellacampi" runat="server" Text="Cancella Campi" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" OnClick="btncancellacampi_Click"/>

              <asp:Label ID="LblCaricaElenco" runat="server" Text="Label" Visible="False"></asp:Label>


          </td>
          <td colspan="2">

                 <asp:Button ID="btnImportAssicurazioniVeicoli" runat="server" Text="Import assicurazioni veicoli da File" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />
                         

          </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="1024px">             
        <tr>
           <td>
               <asp:ListView ID="listRigheAssicurazioni" runat="server" DataKeyNames="id" DataSourceID="sqlRigheAssicurazioni">
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;">
                           <td align="left">
                               <asp:Label ID="inclusa_da" runat="server"  Text='<%# Eval("inclusa_da") %>' Visible="false" />
                               <asp:Label ID="esclusa_da" runat="server" Text='<%# Eval("esclusa_da") %>' visible="false" />
                               <asp:Label ID="inclusa_il" runat="server" Text='<%# Eval("inclusa_il") %>' Visible="false" />
                               <asp:Label ID="esclusa_il" runat="server" Text='<%# Eval("esclusa_il") %>' Visible ="false" />
                               <asp:Label ID="valore_moltiplicativo_tassa" runat="server" Text='<%# Eval("valore_moltiplicativo_tassa") %>' Visible ="false" />
                               <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="id_compagnia" runat="server" Text='<%# Eval("id_compagnia") %>' Visible="false" />
                               <asp:Label ID="giorni_annuali_x_calcolo" runat="server" Text='<%# Eval("giorni_annuali_x_calcolo") %>' Visible="false" />
                               <asp:Label ID="cavalli" runat="server" Text='<%# Eval("cavalli") %>' Visible="false" />
                               <asp:Label ID="percentuale_IF_x_1000" runat="server" Text='<%# Eval("percentuale_IF_x_1000") %>' Visible="false" />
                               
                              <a href='<%# "parco_veicoli/vedi_riga_assicurazione.aspx?veicolo=" & Eval("id_parco") & "&riga=" & Eval("id")%>' runat="server" id="vediPrimoGuidatore" rel="lyteframe" title="" rev="width: 1000px; height: 200px; scrolling: no;"><asp:Label ID="ordine" runat="server" Text='<%# Eval("ordine") %>' CssClass="testo_bold" /></a>
                           </td>
                           <td align="left">
                              <asp:Label ID="id_veicolo" runat="server"  Text='<%# Eval("id_veicolo") %>' Visible="false" />
                              <asp:Label ID="Label6" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <a href='<%# "tabelle_auto/vedi_compagnia_assicurativa.aspx?riga=" & Eval("id_compagnia")%>' runat="server" id="A1" rel="lyteframe" title="" rev="width: 1000px; height: 480px; scrolling: yes;"><asp:Label ID="compagnia" runat="server" Text='<%# Eval("compagnia") %>' CssClass="testo_bold" /></a>
                           </td>
                           <td align="left">
                               <asp:Label ID="data_inclusione" runat="server" Text='<%# Eval("data_inclusione").ToString.Replace(" 00:00:00","").Replace(" 0.00.00","") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                               <asp:Label ID="data_esclusione" runat="server" Text='<%# Eval("data_esclusione").ToString.Replace(" 00:00:00","").Replace(" 0.00.00","") %>' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="tot_giorni" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="valore_I_F" runat="server"  Text='<%# FormatNumber(Eval("valore_I_F"),2,,,TriState.False) %>' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_rca" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_I_F" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_assicurazione" runat="server"  Text='' CssClass="testo" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr style="">
                           <td align="left">
                               <asp:Label ID="inclusa_da" runat="server"  Text='<%# Eval("inclusa_da") %>' Visible="false" />
                               <asp:Label ID="esclusa_da" runat="server" Text='<%# Eval("esclusa_da") %>' visible="false" />
                               <asp:Label ID="inclusa_il" runat="server" Text='<%# Eval("inclusa_il") %>' Visible="false" />
                               <asp:Label ID="esclusa_il" runat="server" Text='<%# Eval("esclusa_il") %>' Visible ="false" />
                               <asp:Label ID="valore_moltiplicativo_tassa" runat="server" Text='<%# Eval("valore_moltiplicativo_tassa") %>' Visible ="false" />
                               <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="id_compagnia" runat="server" Text='<%# Eval("id_compagnia") %>' Visible="false" />
                               <asp:Label ID="cavalli" runat="server" Text='<%# Eval("cavalli") %>' Visible="false" />
                               <asp:Label ID="percentuale_IF_x_1000" runat="server" Text='<%# Eval("percentuale_IF_x_1000") %>' Visible="false" />
                               <asp:Label ID="giorni_annuali_x_calcolo" runat="server" Text='<%# Eval("giorni_annuali_x_calcolo") %>' Visible="false" />
                               <a href='<%# "parco_veicoli/vedi_riga_assicurazione.aspx?veicolo=" & Eval("id_parco") & "&riga=" & Eval("id")%>' runat="server" id="vediPrimoGuidatore" rel="lyteframe" title="" rev="width: 1000px; height: 200px; scrolling: no;"><asp:Label ID="ordine" runat="server" Text='<%# Eval("ordine") %>' CssClass="testo_bold" /></a>
                           </td>
                           <td align="left">
                              <asp:Label ID="id_veicolo" runat="server"  Text='<%# Eval("id_veicolo") %>' Visible="false" />
                              <asp:Label ID="Label6" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                              <a href='<%# "tabelle_auto/vedi_compagnia_assicurativa.aspx?riga=" & Eval("id_compagnia")%>' runat="server" id="A1" rel="lyteframe" title="" rev="width: 1000px; height: 480px; scrolling: yes;"><asp:Label ID="compagnia" runat="server" Text='<%# Eval("compagnia") %>' CssClass="testo_bold" /></a>
                           </td>
                           <td align="left">
                               <asp:Label ID="data_inclusione" runat="server" Text='<%# Eval("data_inclusione").ToString.Replace(" 00:00:00","").Replace(" 0.00.00","") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                               <asp:Label ID="data_esclusione" runat="server" Text='<%# Eval("data_esclusione").ToString.Replace(" 00:00:00","").Replace(" 0.00.00","") %>' CssClass="testo" />
                           </td>
                            <td align="right">
                               <asp:Label ID="tot_giorni" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="valore_I_F" runat="server"  Text='<%# FormatNumber(Eval("valore_I_F"),2,,,TriState.False) %>' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_rca" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_I_F" runat="server"  Text='' CssClass="testo" />
                           </td>
                           <td align="right">
                               <asp:Label ID="totale_assicurazione" runat="server"  Text='' CssClass="testo" />
                           </td>
                       </tr>
                   </AlternatingItemTemplate>
                   <EmptyDataTemplate>
                       <table runat="server" style="">
                           <tr>
                               <td>
                               </td>
                           </tr>
                       </table>
                   </EmptyDataTemplate>
                   <LayoutTemplate>
                       <table runat="server" width="100%">
                           <tr runat="server">
                               <td runat="server">
                                   <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                       <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                           <th runat="server" align="left">
                                               <asp:Label ID="Label22" runat="server" Text="Numero Ordine" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th2" runat="server" align="left">
                                               <asp:Label ID="Label5" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th1" runat="server" align="left">
                                               <asp:Label ID="Label1" runat="server" Text="Compagnia" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th runat="server" align="left">
                                               <asp:Label ID="Label2" runat="server" Text="Data Inclusione" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th runat="server" align="left">
                                               <asp:Label ID="Label3" runat="server" Text="Data Esclusione" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th4" runat="server" align="left">
                                               <asp:Label ID="Label8" runat="server" Text="Giorni" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th runat="server" align="left">
                                               <asp:Label ID="Label4" runat="server" Text="Valore assicurato" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th6" runat="server" align="left">
                                               <asp:Label ID="Label10" runat="server" Text="RCA" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th5" runat="server" align="left">
                                               <asp:Label ID="Label9" runat="server" Text="Furto e Inc." CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th3" runat="server" align="left">
                                               <asp:Label ID="Label7" runat="server" Text="Totale Assicurazione" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                       </tr>
                                       <tr ID="itemPlaceholder" runat="server">
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                           <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="100">
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
    <asp:SqlDataSource ID="sqlCompagnieAssicurative" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [compagnia] FROM compagnie_assicurative WITH(NOLOCK) ORDER BY [compagnia]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="sqlProprietari" runat="server" 
             ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
             SelectCommand="SELECT [id], [descrizione] FROM proprietari_veicoli WITH(NOLOCK) ORDER BY [descrizione]"></asp:SqlDataSource>


         <asp:SqlDataSource ID="sqlVenditaA" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [id], [descrizione] FROM vendita_a WITH(NOLOCK) ORDER BY [descrizione]">
</asp:SqlDataSource>


  <asp:SqlDataSource ID="sqlRigheAssicurazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT veicoli_assicurazioni.id_parco, veicoli.targa, veicoli.id As id_veicolo, veicoli_assicurazioni.id, veicoli_assicurazioni.id_compagnia,  compagnie_assicurative.compagnia, veicoli_assicurazioni.ordine, veicoli_assicurazioni.data_inclusione, veicoli_assicurazioni.data_esclusione, veicoli_assicurazioni.valore_I_F, (ISNULL(operatori1.cognome,'') + ' ' + ISNULL(operatori1.nome,'')) As inclusa_da, veicoli_assicurazioni.inclusa_il, (ISNULL(operatori2.cognome,'') + ' ' + ISNULL(operatori2.nome,'')) AS esclusa_da, esclusa_il FROM veicoli_assicurazioni WITH(NOLOCK) INNER JOIN operatori As operatori1 WITH(NOLOCK) ON veicoli_assicurazioni.inclusa_da=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON veicoli_assicurazioni.esclusa_da=operatori2.id INNER JOIN compagnie_assicurative WITH(NOLOCK) ON veicoli_assicurazioni.id_compagnia=compagnie_assicurative.id INNER JOIN veicoli WITH(NOLOCK) ON veicoli_assicurazioni.id_parco=veicoli.id ORDER BY data_inclusione DESC">
  </asp:SqlDataSource>
         
    <asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
        
         </asp:Content>

