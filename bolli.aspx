<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="bolli.aspx.vb" Inherits="bolli" title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .style1
        {
        color: #000000;
        font-weight: bold;
        height: 14px;
        text-align: left;
    }
        .style2
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 126px;
            text-align: left;
        }
        .style3
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 101px;
            text-align: left;
        }
        .style4
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 77px;
            text-align: left;
        }
        .style6
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 59px;
            text-align: left;
        }
        .style7
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 128px;
            text-align: left;
        }
        .style8
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 49px;
            text-align: left;
        }
        
        .style9
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 184px;
            text-align: left;
        }
        .style10
        {
            color: #000000;
            font-weight: bold;
            height: 14px;
            width: 201px;
            text-align: left;
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">

        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox= (spanChk.type=="checkbox") ? spanChk : spanChk.children.item[0];
            xState=theBox.checked;
            elm=theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
            }
        }

        function is_int(value) {
            if (value.length <= 0) {
                return false;
            }
            for (i = 0; i < value.length; i++) {
                if ((value.charAt(i) < '0') || (value.charAt(i) > '9')) return false
            }
            return true;
        }

        function SincronizzaDataAtto(Drop) {
            //alert("Drop " + Drop.value);
            var txtAnno = document.getElementById('<%= TxtAnno.UniqueID.replace("$","_") %>');
            var TxtDataAtto = document.getElementById('<%= TxtDataAtto.UniqueID.replace("$","_") %>');
            
            if (TxtDataAtto == null) {
                return;
            }
            if (txtAnno == null) {
                TxtDataAtto.value = "";
                return;
            }
            if (!is_int(txtAnno.value)) {
                TxtDataAtto.value = "";
                return;
            }
            if (txtAnno.value < 1900) {
                TxtDataAtto.value = "";
                return;
            }
            var Mese = Drop.value
            if (Mese < 10) {
                Mese = "0" + Drop.value
            }

            TxtDataAtto.value = "01/" + Mese + "/" + txtAnno.value;
        }

        function confermaStampa() {
            var Valore = <%= TotaleRecord() %>;
            if (Valore >= 1000) {
                var Messaggio = "Il numero dei record filtrato è molto elevato (" + Valore + ").\nIl tempo di elaborazione potrebbe essere lungo.\n\nSei sicuro di voler procedere?";
                return(window.confirm (Messaggio));
            }
            return 1;
        }

    </script>
    <%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
 <div id="DivRicerca" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Bolli</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
        <tr>
            <td>   
                <asp:Label ID="lblDaPagareEntro" runat="server" Text="Da Pagare Entro:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td>          
                    <asp:DropDownList ID="DropDownListMese" runat="server" onChange="javascript:SincronizzaDataAtto(this);">
                        <asp:ListItem Value="1">Gennaio</asp:ListItem>
                        <asp:ListItem Value="2">Febbraio</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Aprile</asp:ListItem>
                        <asp:ListItem Value="5">Maggio</asp:ListItem>
                        <asp:ListItem Value="6">Giugno</asp:ListItem>
                        <asp:ListItem Value="7">Luglio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Settembre</asp:ListItem>
                        <asp:ListItem Value="10">Ottobre</asp:ListItem>
                        <asp:ListItem Value="11">Novembre</asp:ListItem>
                        <asp:ListItem Value="12">Dicembre</asp:ListItem>
                    </asp:DropDownList>                       
            </td>
            <td> 
                <asp:Label ID="lblDellAnno" runat="server" Text="Dell'Anno:" CssClass="testo_bold"></asp:Label>               
            </td>
            <td>
                <asp:TextBox ID="TxtAnno" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
            </td>
            <td>   
                <asp:Label ID="lblModello" runat="server" Text="Modello:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td>
                <asp:DropDownList ID="DropDownListModello" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSourceModelli" DataTextField="descrizione" 
                    DataValueField="id_modello" style="margin-left: 0px">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td> 
                <asp:Label ID="lblTarga" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>               
            </td>
            <td>
                <asp:TextBox ID="TxtTarga" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>    
            <asp:Label ID="lblDataAttoDopoIl" runat="server" Text="Data atto maggiore o uguale di:" CssClass="testo_bold"></asp:Label>          
        </td>
        <td>
            <a onclick="Calendar.show(document.getElementById('<%=TxtDataAtto.ClientID%>'), '%d/%m/%Y', false)"> 
            <asp:TextBox ID="TxtDataAtto" runat="server" Width="70px"></asp:TextBox>
                </a>
        <%--    <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtDataAtto">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                TargetControlID="TxtDataAtto">
            </ajaxtoolkit:MaskedEditExtender>  
        </td>
          <td>  
             <asp:Label ID="lblLeasing" runat="server" Text="Leasing:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td>           
              <asp:DropDownList ID="DropDownListLeasing" runat="server" >
                  <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                  <asp:ListItem Value="1">Non in Leasing</asp:ListItem>
                  <asp:ListItem Value="2">In Leasing</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td> 
              <asp:Label ID="lblProprietario" runat="server" Text="Proprietario:" CssClass="testo_bold"></asp:Label>                         
          </td>
          <td>             
              <asp:DropDownList ID="DropDownProprietario" runat="server" 
                  AppendDataBoundItems="True" DataSourceID="SqlDataSourceProprietario" 
                  DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>
          </td> 
          <td>
            <asp:Label ID="Label1" runat="server" Text="Stampa:" CssClass="testo_bold"></asp:Label>
          </td> 
          <td>
            <asp:DropDownList ID="DropDownTipoStampa" runat="server" >
                <asp:ListItem Value="1" Selected="True">Num. globale</asp:ListItem>
                <asp:ListItem Value="0">Num. per modello</asp:ListItem>
            </asp:DropDownList>
          </td>         
        </tr>
        <tr>
          <td colspan="8" align="center">
            <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" ValidationGroup="cerca" /> 
            <asp:Button ID="btnAzzeraScadenzaBollo" runat="server" Text="Azzera Scadenza Bollo" Visible="false" OnClientClick="javascript: return(window.confirm ('L\'azzeramento delle scadenze Bollo comporterà la cancellazione della data di scadenza bollo per tutti i veicoli selezionati dall\'attuale filtro di ricerca. \n\nSei sicuro di voler procedere?'));" />                      
          </td>
        </tr>
    </table>
    <br />
</div> 


<div id="DivElenco" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
      <tr>
        <td> 
            <div style="height:440px; overflow-y: scroll;">
                                                       
            <asp:ListView ID="ListBolli" runat="server" DataSourceID="SqlDataSourceElencoBolli" DataKeyNames="id" >
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC;color: #000000;">
                        <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            <asp:Label ID="selezionatoLabel" runat="server" Text='<%# Eval("selezionato") %>' visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                            <asp:Label ID="id_veicoloLabel" runat="server" Text='<%# Eval("id") %>' visible="false" />
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                        </td>
                        <td>
                            <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                        </td>
                        <td>
                            <asp:Label ID="data_immatricolazioneLabel" runat="server" Text='<%# Eval("data_immatricolazione")  %>' />
                        </td>
                        <td>
                            <asp:Label ID="EuroLabel" runat="server" Text='<%# Eval("Euro") %>' />
                        </td>
                        <td>
                            <asp:Label ID="KWLabel" runat="server" Text='<%# Eval("KW") %>' />
                        </td>
                        <td>
                            <asp:Label ID="data_scadenza_bolloLabel" runat="server" Text='<%# Eval("data_scadenza_bollo") %>' visible="false"/>
                            <asp:Label ID="mese_scadenza_bolloLabel" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:Label ID="data_atto_venditaLabel" runat="server" Text='<%# Eval("data_atto_vendita") %>' />                            
                        </td>
                        <td>
                            <!-- asp : TextBox ID="MesiDaPagare" r_unat="server" MaxLength="2" Width="40px" Text=''></asp:TextBox -->
                            <asp:Label ID="mesi_bolloLabel" runat="server" Text='<%# Eval("mesi_bollo") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownListMeseDaPagare" runat="server" >
                                
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="importoLabel" runat="server" Text='<%# Eval("importo") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="">
                        <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            <asp:Label ID="selezionatoLabel" runat="server" Text='<%# Eval("selezionato") %>' visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                            <asp:Label ID="id_veicoloLabel" runat="server" Text='<%# Eval("id") %>' visible="false" />
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                        </td>
                        <td>
                            <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                        </td>
                        <td>
                            <asp:Label ID="data_immatricolazioneLabel" runat="server" Text='<%# Eval("data_immatricolazione")  %>' />
                        </td>
                        <td>
                            <asp:Label ID="EuroLabel" runat="server" Text='<%# Eval("Euro") %>' />
                        </td>
                        <td>
                            <asp:Label ID="KWLabel" runat="server" Text='<%# Eval("KW") %>' />
                        </td>
                        <td>
                            <asp:Label ID="data_scadenza_bolloLabel" runat="server" Text='<%# Eval("data_scadenza_bollo") %>' visible="false"/>
                            <asp:Label ID="mese_scadenza_bolloLabel" runat="server" Text="" />
                        </td>
                        <td>                            
                            <asp:Label ID="data_atto_venditaLabel" runat="server" Text='<%# Eval("data_atto_vendita") %>' />                            
                        </td>
                        <td>
                            <asp:Label ID="mesi_bolloLabel" runat="server" Text='<%# Eval("mesi_bollo") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownListMeseDaPagare" runat="server" >
                                
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="importoLabel" runat="server" Text='<%# Eval("importo") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                      <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.
                              </td>
                          </tr>
                      </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table runat="server" width="100%">
                        <tr runat="server">
                            <td runat="server">
                                <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                        <th id="Th1" runat="server">
                                            <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" />
                                            Tutti</th>
                                        <th runat="server">
                                            Targa</th>
                                        <th id="Th_lente" runat="server">
                                          </th>
                                        <th runat="server">
                                            Modello</th>
                                        <th runat="server">
                                            Data Immatric.</th>
                                        <th runat="server">
                                            Euro</th>
                                        <th runat="server">
                                            KW</th>
                                        <th runat="server">
                                            Mese a Scadere Bollo</th>
                                        <th runat="server">
                                            Data Atto</th>
                                        <th id="Th2" runat="server">
                                            Mesi</th>
                                        <th id="Th3" runat="server">
                                            Da Pagare</th>
                                    </tr>
                                    <tr ID="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" PageSize="150" runat="server"  >
                                      <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"   />

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
            </div>  
        </td>
      </tr>
    </table>
</div>

<div id="DivPulsanti" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
      <tr>
      <td align="right" style="width: 25%;">
            &nbsp;
        </td>
        <td align="center" style="width: 50%;">
            <asp:Button ID="btnSalva" runat="server" Text="Salva" />
            <asp:Button ID="btnConferma" runat="server" Text="Stampa" OnClientClick="javascript: return(confermaStampa());" />
            <asp:Button ID="btnChiudi" runat="server" Text="Chiudi" />
        </td>
        <td align="right" style="width: 25%;">
            <asp:Label  runat="server" ID="ImportoTotaleLabel" Text="" />
        </td>
      </tr>
    </table>
</div>

    <asp:Label ID="lb_id_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Mese_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Anno_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Proprietario_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Modello_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Leasing_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_DataAtto_filtro_bollo" runat="server" Text="" Visible="false" />
    <asp:Label ID="lb_Targa_filtro_bollo" runat="server" Text="" Visible="false" />

    <asp:Label ID="lb_SqlDataSourceElencoBolli" runat="server" Text="" Visible="false" />

    <asp:SqlDataSource ID="SqlDataSourceModelli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_modello, descrizione FROM modelli WITH(NOLOCK) order by descrizione">
    </asp:SqlDataSource>    
    
               
   <asp:SqlDataSource ID="SqlDataSourceProprietario" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"                 
        SelectCommand="SELECT [id], [descrizione] FROM proprietari_veicoli WITH(NOLOCK) ORDER BY [descrizione]">
   </asp:SqlDataSource>
                
    <asp:SqlDataSource ID="SqlDataSourceElencoBolli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="" >
    </asp:SqlDataSource>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="cerca" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="TxtAnno" ErrorMessage="Specificare l'anno di elaborazione." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"></asp:RequiredFieldValidator>

<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="TxtAnno" ErrorMessage="Specificare un anno valido di elaborazione."
    Type="Integer" Operator="GreaterThan" ValueToCompare="1900"
    Font-Size="0pt" ValidationGroup="cerca" > </asp:CompareValidator>
   

</asp:Content>



