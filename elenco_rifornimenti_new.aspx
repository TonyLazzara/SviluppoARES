<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="elenco_rifornimenti.aspx.vb" Inherits="elenco_rifornimenti" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 
    <style type="text/css">
        .style1
        {
            width: 222px;
        }
        .style2
        {
            width: 172px;
        }
        .style3
        {
            width: 153px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label2" runat="server" Text="Elenco Rifornimenti" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
    </table>
   <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">   
        <tr>
           <td align="left" class="style3" >
               <asp:Label ID="LblStazione" runat="server" CssClass="testo_bold" Text="Stazione"></asp:Label>
          </td>  
           <td align="left" >
              <asp:Label ID="LblMarca0" runat="server" Text="Fornitore" CssClass="testo_bold"></asp:Label>
            </td>  
          <td align="center" style="width:240px;">
              <asp:Label ID="TxtData" runat="server" Text="Data Rifornimento" CssClass="testo_bold"></asp:Label>
           </td>  
          <td align="left" class="style37">
              <asp:Label ID="LblTarga" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
           </td>
          <!--
          <td align="left" class="style46">
              <asp:Label ID="LblTelaio" runat="server" Text="Telaio" CssClass="testo_bold"></asp:Label>
          </td>
          -->
          <td align="left" class="style50">
              <asp:Label ID="LblMarca" runat="server" Text="Marca" CssClass="testo_bold"></asp:Label>
          </td>  
          <td align="left" class="style55">
              <asp:Label ID="LblModello" runat="server" Text="Modello" CssClass="testo_bold"></asp:Label>
          </td>       
        </tr>
        <tr>
          <td class="style3">
            <asp:DropDownList ID="dropCercaStazione" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlStazioni" DataTextField="stazione" style="margin-left: 0px"
                   DataValueField="id" AutoPostBack="True">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
          </td>
          <td>
            <asp:DropDownList ID="DDLFornitore" runat="server" 
                  DataSourceID="sqlFornitori" DataTextField="descrizione" DataValueField="id"
                   AppendDataBoundItems="True" >
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>    
            </td>
          <td align="left" style="width:240px;">
              <a onclick="Calendar.show(document.getElementById('<%=TxtDataDal.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="TxtDataDal" runat="server" Width="85px"  placeholder="Dal"></asp:TextBox></a>
                  <%--  <asp:CalendarExtender ID="TxtDataDal_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDataDal">
                </asp:CalendarExtender>--%>
               /<a onclick="Calendar.show(document.getElementById('<%=TxtDataAl.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="TxtDataAl" runat="server" Width="85px"  placeholder="Al"></asp:TextBox></a>

            <%--   <asp:CalendarExtender ID="TxtDataAl_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDataAl">
                </asp:CalendarExtender>--%>

          </td>         
          <td align="left" class="style4">
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="85px"></asp:TextBox>
          </td>
          <!--
          <td align="left" class="style46">            
               <asp:TextBox ID="txtCercaTelaio" runat="server" Width="136px"></asp:TextBox>            
          </td>
          -->
          <td align="left" class="style50">
            
               <asp:DropDownList ID="dropCercaMarca" runat="server"
                   DataSourceID="SqlCercaMarca" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>            
            </td>
             <td align="left" class="style55">            
               <asp:DropDownList ID="dropCercaModello" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlCercaModello" DataTextField="descrizione" style="margin-left: 0px"
                   DataValueField="id_modello">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
            </td>
        </tr>
        <tr>         
          <td align="left" class="style54" colspan="3">
               <asp:Label ID="lblStato" runat="server" CssClass="testo_bold" Text="Stato veicolo"></asp:Label>
          </td>          
          <td align="left" class="style2" colspan="3">
               &nbsp;
         </td>
        </tr>
        <tr>                   
          <td align="left" class="style54" colspan="3">
               <asp:DropDownList ID="dropStatoVendita" runat="server" AppendDataBoundItems="True" 
                   style="margin-left: 0px">
                   <asp:ListItem Selected="False" Value="0">Qualsiasi</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">Da rifornire</asp:ListItem>                    
                   <asp:ListItem Selected="False" Value="2">In rifornimento</asp:ListItem>
                   <asp:ListItem Selected="False" Value="3">Riforniti</asp:ListItem> 
                   <asp:ListItem Selected="False" Value="4">Riforniti - Importo da definire</asp:ListItem>                      
               </asp:DropDownList>
               <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" 
                   style="height: 26px" UseSubmitBehavior="False" />
               <asp:Button ID="btnPulisciCampi" runat="server" Text="Azzera Campi" Visible="True" style="height: 26px" UseSubmitBehavior="False" />
          
               <asp:Button ID="btnCreaRifornimento" runat="server" Text="Crea Rifornimento" Visible="True" style="height: 26px" UseSubmitBehavior="False" />
          
          </td>   
          <td align="left" colspan="3">                                                                     
               <asp:Button ID="btnStampa" runat="server" Text="Report" 
                   style="height: 26px" UseSubmitBehavior="False" /> 
               <asp:Button ID="BtnStampaCarburante" runat="server" Text="Scheda Carburante" 
                   style="height: 26px" Visible="False" UseSubmitBehavior="False" ToolTip="Specificare fornitore e data di rifornimento per stampare la scheda." />
            
            
            
            <asp:Label ID="LblCaricaElenco" runat="server" Text="CaricaFile" Visible="False"></asp:Label>  
            <asp:Label ID="lblNumRisultati" runat="server" Visible="False"></asp:Label>                               
          </td>       
        </tr>        
      </table>    

         <% 
             If LblCaricaElenco.Text = "CaricaFile" Then
        %>
        <table border="0" cellpadding="0" cellspacing="0" width="1024px">             
        <tr>
          <td colspan="2">
              
              <asp:ListView ID="listRifornimenti" runat="server" DataKeyNames="id" DataSourceID="SqlRifornimenti">
                   <ItemTemplate>
                      <tr style="background-color:#FFFFFF;color: #000000;" >
                          <td>
                            <asp:Label ID="anno_rifornimento" runat="server" Text='<%# Eval("anno_rifornimento") %>' Visible="false" />
                            <asp:Label ID="num_rifornimento" runat="server" Text='<%# Eval("num_rifornimento") %>' Visible="false" />
                            <asp:Label ID="num_rif" runat="server" CssClass="testo" />
                          </td>
                          <td>
                              <asp:Label ID="lblFornitore" runat="server" Text='<%# Eval("fornitore") %>'    />                              
                          </td> 
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>'   />                              
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("descrizione") %>'    />                              
                          </td>                                                    
                          <td>
                              <asp:Label ID="alimentazioneLabel" runat="server" Text='<%# Eval("alimentazione") %>'   />                                                            
                          </td>
                          <td>
                              <asp:Label ID="dataRifLabel" runat="server" Text='<%# Eval("data_uscita_parco") %>'   />
                          </td>
                           <td>
                               <asp:Label ID="serbatoioLabel" runat="server" Text='<%# Eval("serbatoio") %>'   />                                                            
                               <asp:Label ID="Label118" runat="server" Text='/'    />
                               <asp:Label ID="capicitaserbatoioLabel" runat="server" Text='<%# Eval("capacita_serbatoio") %>'   /> 
                              
                          </td>                              
                          <td>
                              <asp:Label ID="kmOutLabel" runat="server" Text='<%# Eval("km_out") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("data_rientro_parco") %>'   />
                          </td>                        
                          <td>
                              <asp:Label ID="kmInLabel" runat="server" Text='<%# Eval("km_in") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="importoLabel" runat="server" Text='<%# Eval("importo_rifornimento") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="disponibile_nolo" runat="server" Text='<%# Eval("disponibile_nolo") %>'   />
                          </td>                                           
                          <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                          </td>  
                          <td align="center">
                              <asp:ImageButton ID="btnCancella" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il veicolo?'));" ToolTip="Elimina"  />
                          </td>
                      </tr>
                  </ItemTemplate>
                   <AlternatingItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td>
                            <asp:Label ID="anno_rifornimento" runat="server" Text='<%# Eval("anno_rifornimento") %>' Visible="false" />
                            <asp:Label ID="num_rifornimento" runat="server" Text='<%# Eval("num_rifornimento") %>' Visible="false" />
                            <asp:Label ID="num_rif" runat="server" CssClass="testo" />
                          </td>
                          <td>
                              <asp:Label ID="lblFornitore" runat="server" Text='<%# Eval("fornitore") %>'    />                              
                          </td>
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>'   />                              
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("descrizione") %>'    />                              
                          </td>                                                    
                          <td>
                              <asp:Label ID="alimentazioneLabel" runat="server" Text='<%# Eval("alimentazione") %>'   />                                                            
                          </td>
                          <td>
                              <asp:Label ID="dataRifLabel" runat="server" Text='<%# Eval("data_uscita_parco") %>'   />
                          </td>
                          <td>
                               <asp:Label ID="serbatoioLabel" runat="server" Text='<%# Eval("serbatoio") %>'   />                                                            
                               <asp:Label ID="Label118" runat="server" Text='/'    />
                               <asp:Label ID="capicitaserbatoioLabel" runat="server" Text='<%# Eval("capacita_serbatoio") %>'   /> 
                              
                          </td>                          
                          <td>
                              <asp:Label ID="kmOutLabel" runat="server" Text='<%# Eval("km_out") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("data_rientro_parco") %>'   />
                          </td>                        
                          <td>
                              <asp:Label ID="kmInLabel" runat="server" Text='<%# Eval("km_in") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="importoLabel" runat="server" Text='<%# Eval("importo_rifornimento") %>'   />
                          </td>  
                          <td>
                              <asp:Label ID="disponibile_nolo" runat="server" Text='<%# Eval("disponibile_nolo") %>'   />
                          </td>                                           
                          <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="btnCancella" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il veicolo?'));" ToolTip="Elimina"  />
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                   <EmptyDataTemplate>
                      <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                          <tr>
                              <td class="testo_bold_nero">
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                   <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%"  class="testo_piccolo"
                                      style="border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;" >
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF;background-color:#19191b;line-height:18px;">
                                          <th id="Th12" runat="server"  style="width:50px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton31" runat="server" CommandName="order_by_num_rif" CssClass="testo_titolo">Num.</asp:LinkButton>
                                          </th>
                                          <th id="Th9" runat="server" style="width:110px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_fornitore" CssClass="testo_titolo">Fornitore</asp:LinkButton>
                                          </th>
                                          <th id="Th4" runat="server" style="width:90px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_Targa" CssClass="testo_titolo">Targa</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server"  style="width:140px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_Modello" CssClass="testo_titolo">Modello</asp:LinkButton>
                                          </th>                                                                                    
                                          <th id="Th10" runat="server" align="left">
                                              
                                              <asp:Label ID="LblAlimentazione" runat="server" Text="Alim." CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th1" runat="server"  style="width:160px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_Data" CssClass="testo_titolo">Data/Ora Out</asp:LinkButton>
                                          </th>
                                          <th id="Th8" runat="server"   style="width:60px;text-align:left;">
                                              <asp:Label ID="LblSerbatoio" runat="server" Text="Serb." CssClass="testo_titolo"></asp:Label>
                                          </th>                                          
                                          <th id="Th3" runat="server" align="left">                                             
                                              <asp:Label ID="LblKmOut" runat="server" Text="Km Out" CssClass="testo_titolo"></asp:Label>
                                          </th>         
                                          <th id="Th7" runat="server"  style="width:160px;text-align:left;">
                                              <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_DataIn" CssClass="testo_titolo">Data/Ora In</asp:LinkButton>
                                          </th>                                 
                                          <th id="Th5" runat="server" align="left">
                                             
                                             <asp:Label ID="LblKmIn" runat="server" Text="Km In" CssClass="testo_titolo"></asp:Label>
                                          </th>   
                                          <th id="Th2" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_Importo" CssClass="testo_titolo">Importo</asp:LinkButton>
                                          </th> 
                                           <th id="Th11" runat="server" align="left">
                                              <asp:Label ID="LblRiferimento" runat="server" Text="Dis.N" CssClass="testo_titolo"></asp:Label>
                                          </th> 
                                          <th></th>                 
                                          <th></th>                      
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server" >
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
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
    <% End If%>

        <asp:SqlDataSource ID="SqlRifornimenti" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
            SelectCommand="SELECT rifornimenti.id, anno_rifornimento, num_rifornimento, stazioni.nome_stazione, veicoli.targa, MODELLI.descrizione, MODELLI.capacita_serbatoio, alimentazione.descrizione AS Alimentazione, 
                                rifornimenti.data_uscita_parco, rifornimenti.serbatoio, rifornimenti.km_out, rifornimenti.data_rientro_parco, rifornimenti.km_in, rifornimenti.data_rifornimento, 
                                rifornimenti.importo_rifornimento, veicoli.disponibile_nolo
                          FROM  rifornimenti WITH(NOLOCK) INNER JOIN
                                veicoli WITH(NOLOCK) ON rifornimenti.id_veicolo = veicoli.id INNER JOIN
                                MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN
                                alimentazione WITH(NOLOCK) ON MODELLI.TipoCarburante = alimentazione.id INNER JOIN
                                stazioni WITH(NOLOCK) ON veicoli.id_stazione = stazioni.id WITH(NOLOCK)
                          WHERE (1 = 1) AND rifornimenti.data_uscita_parco is null AND veicoli.venduta='0' ">
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="SqlStazioni" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice">
          </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="SqlCercaMarca" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT [id], [descrizione] FROM marche WITH(NOLOCK) ORDER BY [descrizione]">
          </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="SqlCercaModello" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT id_modello, descrizione FROM modelli WITH(NOLOCK) WHERE (id_CasaAutomobilistica = @id_marca) ORDER BY descrizione">
            <SelectParameters>
                <asp:ControlParameter ControlID="dropCercaMarca" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlFornitori" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
       SelectCommand="SELECT id, descrizione FROM alimentazione_fornitori_x_stazione WHERE id_stazione=@id_stazione ">
         <SelectParameters>
                    <asp:ControlParameter ControlID="dropCercaStazione" Name="id_stazione" PropertyName="SelectedValue" Type="Int32" />
         </SelectParameters>
    </asp:SqlDataSource>
    
        <asp:Label ID="lblSql" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblWhere" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>
        
        <asp:Label ID="livello_accesso" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="livello_accesso_admin" runat="server" Visible="False"></asp:Label>
        
    <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
</asp:Content>

