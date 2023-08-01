<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_multe.aspx.vb" Inherits="gestione_multe" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>
<%@ Register Src="gestione_Multe/FattureMulte.ascx" TagName="FattureMulte" TagPrefix="uc1" %>
<%@ Register Src="gestione_Multe/Enti.ascx" TagName="Enti" TagPrefix="uc1" %>
<%@ Register Src="gestione_Multe/ArticoliCDS.ascx" TagName="ArticoliCDS" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
    <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 


    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />   
    <style type="text/css">
    .style5
    {
        width: 131px;
    }
    .style3
    {
        width: 106px;
    }
     .style33
     {
         width: 182px;
     }
     .style34
     {
         width: 169px;
     }
     .style36
     {
         height: 11px;
         width: 179px;
     }
     .style37
     {
         height: 11px;
         width: 89px;
     }
     .style38
     {
         height: 11px;
         width: 76px;
     }
     .style39
     {
         height: 11px;
         width: 74px;
     }
     .style40
     {
         height: 11px;
         width: 49px;
     }
     .style41
     {
         height: 11px;
         width: 14px;
     }

    </style>
    <script type="text/javascript" src="gestione_multe/InputImportoRidotto.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<div id="div_container" runat="server"> 


<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>--%>
<div>
    <table border="0" cellpadding="1" cellspacing="1" width="100%" >
             <tr>
               <td class="style3">
                   <asp:Label ID="lblAnno" runat="server" Text="Anno"></asp:Label>
               </td>
               <td><asp:Label ID="lblProt" runat="server" Text="Prot."></asp:Label></td>
               <td><asp:Label ID="lblDataIns" runat="server" Text="Data inserimento"></asp:Label></td>
               <td><asp:Label ID="lblProvenienza" runat="server" Text="Provenienza"></asp:Label></td>
               <td><asp:Label ID="lblStato" runat="server" Text="Stato"></asp:Label></td>
               <td><asp:Label ID="label1" runat="server" Text="ID"></asp:Label></td>
               <td>
                   <asp:Button ID="btnNuovo" runat="server" Text="Nuovo" 
                    Font-Bold="True" Font-Size="Small" ForeColor="White" />
               </td>
               <td>
                   <asp:Button ID="btnSalvaModifiche" runat="server" Text="Salva modifiche" 
                    Font-Bold="True" Font-Size="Small" ForeColor="White" />
               </td>
               <td>
                   <asp:Button ID="btnElimina" runat="server" Text="Elimina" 
                    Font-Bold="True" Font-Size="Small" ForeColor="White" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnElimina" 
							ConfirmText="Confermi l'eminazione della multa?" >
                    </asp:ConfirmButtonExtender>							
               </td>
               <td>
                   <asp:Button ID="btnChiudi" runat="server" Text="Chiudi" 
                    Font-Bold="True" Font-Size="Small" ForeColor="White" />
               </td>
             </tr>
            <tr>
               <td><asp:TextBox ID="txtAnno" runat="server" Width="80px" Enabled="False"></asp:TextBox></td>
               <td><asp:TextBox ID="txtProt" runat="server" Width="80px" Enabled="False"></asp:TextBox></td>
               <td><asp:TextBox ID="txtDataInser" runat="server" Width="80px" Enabled="False" 
                       Height="22px"></asp:TextBox></td>
               <td>
                    <asp:DropDownList ID="DropProvenienza" runat="server" Height="20px" Width="120px" AutoPostBack="False" 
                        DataSourceID="sqlProvenienza" DataTextField="Provenienza" DataValueField="ID" 
                        style="margin-left: 0px" AppendDataBoundItems="True">
                   </asp:DropDownList>
               </td>
               <td>
                   <asp:DropDownList ID="DropStato" runat="server">
                    <asp:ListItem Value="true">Aperto</asp:ListItem>
                    <asp:ListItem Value="false">Chiuso</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td><asp:Label ID="lblID" runat="server" Width="50px"></asp:Label></td>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
            </tr>
    </table>    
   
    
 <div style="float:left; width:80%" id="div_dropenti">
 
     <asp:Panel ID="PanelDatiVerbale" runat="server" >
    <table width="100%" >
        <tr>
            <td align="left" style="color: #FFFFFF" bgcolor="#369061">
                <b>Dati del verbale</b>
            </td>
        </tr>
    </table>
    <table style="float:left;font-family:Arial, Helvetica, sans-serif;font-size:12px;" width="40%"  id="table_dropenti" runat="server">
        <tr>
            <td>Ente:</td>
            <td>
            <asp:DropDownList ID="DropEnti" runat="server" Width="180px" AutoPostBack="true" 
                        DataSourceID="sqlEnti" DataTextField="Ente" DataValueField="ID" 
                        style="margin-left: 0px" AppendDataBoundItems="True">
                <asp:ListItem Value="">-seleziona-</asp:ListItem> 
                   </asp:DropDownList>
                   <asp:ImageButton ID="ImageButtonNewEnte" runat="server" 
                            ImageUrl="/images/aggiorna.png" style="height: 16px; " />
            </td>
        </tr>
         <tr>
            <td>Indirizzo:</td>
            <td><asp:TextBox ID="txtEnteIndir" runat="server" Width="195px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Comune:</td>
            <td>
                <asp:TextBox ID="txtEnteComune" runat="server"></asp:TextBox>
                <asp:Button ID="btnCercaComuni" Text="..." runat="server" Width="30px" />
                
                <asp:ListBox ID="ListComuni" runat="server" Width="200px" AutoPostBack="true"
                    DataTextField="comune" DataValueField="id">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>C.A.P. e Prov.:</td>
            <td>
                <asp:TextBox ID="txtCap" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                <asp:TextBox ID="txtProv" runat="server" Width="50px" MaxLength="2"></asp:TextBox>
            </td>
        </tr>
      
        

    </table>
    <table style="float:left;font-family:Arial, Helvetica, sans-serif;font-size:12px;" width="30%">
        <tr>
            <td>Num. Verbale:</td>
            <td><asp:TextBox ID="txtVerbale" runat="server" Width="100px" MaxLength="30"></asp:TextBox></td>
        </tr>
         <tr>
            <td>Data notifica:</td>
            <td>
                <a onclick="Calendar.show(document.getElementById('<%=txtNotifica.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="txtNotifica" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
                <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtNotifica" ID="CalendarExtender1">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtNotifica" ID="MaskedEditExtender1">
                </asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Articolo CDS:</td>
            <td>
                <asp:DropDownList ID="DropArtCDS" runat="server" Width="85px" AutoPostBack="False" 
                    DataSourceID="SqlArtCDS" DataTextField="CDS" DataValueField="idCDS" 
                    style="margin-left: 0px" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
                <asp:ImageButton ID="ImageButtonNewArtCDS" runat="server" 
                            ImageUrl="/images/aggiorna.png" style="height: 16px; " />
            </td>
            </tr>
        <tr>
            <td>Importo:</td>
            <td>
                <asp:TextBox ID="txtImporto" runat="server"  onKeyPress="return filterInput(event)" Width="100px" MaxLength="9"></asp:TextBox>
            </td>
        </tr>
       
         



    </table>
        
        <table style="float:left;font-family:Arial, Helvetica, sans-serif;font-size:12px;" width="25%">
        <tr>


            <td>Targa:</td><td><asp:TextBox ID="txtTarga" runat="server" Width="100px" 
                MaxLength="9"></asp:TextBox></td></tr><tr>
            <td>Data infrazione:</td>
            <td>
                 <a onclick="Calendar.show(document.getElementById('<%=txtDataInfrazione.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="txtDataInfrazione" runat="server" Width="100px" MaxLength="10"></asp:TextBox></a>
             <%--   <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataInfrazione" ID="CalendarExtender2">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataInfrazione" ID="MaskedEditExtender2">
                </asp:MaskedEditExtender>
            </td></tr><tr>
            <td>Ora infrazione:</td><td>
            <asp:TextBox ID="txtOraInfrazione" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                <asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtOraInfrazione" ID="MaskedEditExtender3">
                </asp:MaskedEditExtender>
            </td></tr></table>

   <table width="100%;" style="border:solid 0px;font-family:Arial, Helvetica, sans-serif;margin-top:20px;font-size:12px;">     
       <tr>

           <td>
            
               <table width="320px;" style="border:solid 0px;font-family:Arial, Helvetica, sans-serif;margin-top:0px;font-size:12px;">
              <tr>
                     <td style="width:84px;">Tel.
                
                
            </td>
            <td >
                <asp:TextBox ID="txt_prot_enti_tel" runat="server"  Width="300px" ></asp:TextBox>        
            </td>           
            
             </tr>

              <tr>
                    <td>Email:</td>
                    <td><asp:TextBox ID="txt_prot_enti_email" runat="server" Width="300px"  ></asp:TextBox>       </td>
              </tr>
                   
               <tr>
                    <td>PEC:</td>
                    <td><asp:TextBox ID="txt_prot_enti_emailpec" runat="server"  Width="300px" ></asp:TextBox> </td>
              </tr>

                <tr>
                    <td>Note Ente:</td>
                    <td><asp:TextBox ID="txt_enti_notes" runat="server"  Width="300px"  TextMode="MultiLine" Rows="5"></asp:TextBox>  </td>
              </tr>


        </table>
           </td>
     <td style="width:20px;">

     </td>
     <td>
        <table width="100%;" style="border:solid 0px;font-family:Arial, Helvetica, sans-serif;margin-top:0px;font-size:12px;">
            <tr style="vertical-align:top;">            
            <td> 
                Note Prot:                          
            </td>            
            </tr>  
        <tr>
            <td>
                 <asp:TextBox ID="txt_prot_enti_notes" runat="server"  Width="400px"  TextMode="MultiLine" Rows="9" ></asp:TextBox>      
            </td>
        </tr>

         </table> 

     </td>
       
        
     </tr>
 </table> 

         <table width="100%">
        <tr>
            <td>&nbsp;</td></tr><tr>
            <td style="width:48%">
                <asp:Button ID="btnAssegnaProtVuoto" runat="server" Text="Assegna Prot.Vuoto" 
                Font-Bold="True" Font-Size="X-Small" ForeColor="White" style="height: 20px"/>
            </td>
            <td style="width:20%" align="right">
                <asp:Button ID="btnRicercaMovim" runat="server" Text="Ricerca movim. auto" 
                Font-Bold="True" Font-Size="Small" ForeColor="White" style="height: 22px" />
            </td>
            <td style="width:5%">
                <asp:Button ID="btnAssegnaProt" runat="server" Text="Assegna Prot." 
                Font-Bold="True" Font-Size="Small" ForeColor="White" style="height: 22px" ValidationGroup="DatiVerbale" />
            </td>
            <td style="width:20%" align="right">
                <asp:Button ID="btnRicercaTuttiMov" runat="server" Text="Ricerca tutti i mov.auto" 
                Font-Bold="True" Font-Size="Small" ForeColor="White" style="height: 22px" />
            </td>
            <td style="width:5%">
            &nbsp;
            </td>
        </tr>

   
    </table>

    </asp:Panel>
    </div>
    <asp:Panel ID="PanelCasistiche" runat="server" >
        <table style="padding-top:2px;font-family:Arial, Helvetica, sans-serif;font-size:12px;" border="0" cellpadding="0" cellspacing="0" width="20%" >
            <tr>
                <td colspan="2" align="center" style="color: #FFFFFF; font-size:large" bgcolor="#369061">
                     <b>Azioni previste</b>
                </td>
            </tr>
        </table>
        <table id="TableCasistiche" runat="server" border="0" cellpadding="0" cellspacing="0" width="20%" visible="true" 
            style="font-family:Arial, Helvetica, sans-serif;font-size:12px;" >
            <tr>
                <td>Rinotifica:</td><td>
                    <asp:CheckBox ID="ChkRinotifica" runat="server" Text="" Enabled="false"/></td>
            </tr>
            <tr>
                <td>Add.Serv.Fee:</td><td>
                    <asp:CheckBox ID="ChkAddServFee" runat="server" Text="" Enabled="false"/></td>
            </tr>
            <tr>
                <td>Add.Verbale:</td><td>
                    <asp:CheckBox ID="ChkAddVerbale" runat="server" Text="" Enabled="false"/></td>
            </tr>
            <tr>
                <td>Incasso c/c:</td><td>
                    <asp:CheckBox ID="ChkIncCartaCred" runat="server" Text="" Enabled="false"/></td>
            </tr>
            <tr>
                <td>Fattura:</td><td>
                    <asp:CheckBox ID="ChkFattura" runat="server" Text="" Enabled="false"/></td>
            </tr>
            <tr>
                <td>Ricorso:</td><td>
                    <asp:CheckBox ID="ChkRicorso" runat="server" Text="" Enabled="false"/></td>
            </tr>
            </table>
    </asp:Panel>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<hr style="clear:both; color:#369061;" />
<div id="pannelloMoviAuto" runat="server">
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <div id="visualizza_movimAuto"  runat="server" style="width:1024px; font-size:small;" visible="true">
            <asp:ListView ID="ListViewMovAuto" runat="server" DataSourceID="sqlMovAuto" DataKeyNames="id">
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC; color: #000000;">
                        <td>
                            <asp:Label ID="lblIdMovAuto" runat="server" Width="60px"  Text= '<%# Eval("id") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescrzMov" runat="server" Width="90px" ToolTip='<%# Eval("id_tipo_movimento") %>' 
                                Text='<%# Eval("descrzione") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="NumRiferimento" runat="server" ForeColor="Green" AlternateText='<%# Eval("num_riferimento") %>'  CommandName="VisualizzaDocum" />
                        </td>
                        <td>
                            <asp:Label ID="lblTarga" runat="server" Width="90px" Text='<%# Eval("targa") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazUscita" runat="server" Width="150px" Text='<%# Eval("StazUscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazRientro" runat="server" Width="150px" Text='<%# Eval("StazRientro") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataUscita" runat="server" Width="160px" Text='<%# Eval("data_uscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataRientro" runat="server" Width="160px" Text='<%# Eval("data_rientro") %>'></asp:Label>
                        </td>
                        <td align="center" width="40px">
                            <asp:ImageButton ID="SelezMovAuto" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMovAuto"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label ID="lblIdMovAuto" runat="server" Width="60px" Text='<%# Eval("id") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescrzMov" runat="server" Width="90px" ToolTip='<%# Eval("id_tipo_movimento") %>' 
                                Text='<%# Eval("descrzione") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="NumRiferimento" runat="server" ForeColor="Green" AlternateText='<%# Eval("num_riferimento") %>'  CommandName="VisualizzaDocum" />
                        </td>
                        <td>
                            <asp:Label ID="lblTarga" runat="server" Width="90px" Text='<%# Eval("targa") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazUscita" runat="server" Width="150px" Text='<%# Eval("StazUscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazRientro" runat="server" Width="150px" Text='<%# Eval("StazRientro") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataUscita" runat="server" Width="160px" Text='<%# Eval("data_uscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataRientro" runat="server" Width="160px" Text='<%# Eval("data_rientro") %>'></asp:Label>
                        </td>
                        <td align="center" width="40px">
                            <asp:ImageButton ID="SelezMovAuto" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMovAuto"/>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                <table id="Table1" runat="server" style="">
                    <tr>
                        <td>
                            Non vi sono movimenti con la targa selezionata.
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
                                                Tipo Movim.</th>
                                            <th id="Th3" runat="server">
                                                Riferimento</th>
                                            <th id="Th4" runat="server">
                                                Targa</th>
                                            <th id="Th5" runat="server">
                                                Staz.Uscita</th>
                                            <th id="Th6" runat="server">
                                                Staz.Rientro</th>
                                            <th id="Th7" runat="server">
                                                Data Uscita</th>
                                            <th id="Th8" runat="server">
                                                Data Rientro</th>
                                            <th id="Th9" runat="server">
                                                </th>
                                    </tr>
                                    <tr ID="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td2" runat="server" style="">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnAnnullaListaMovAuto" runat="server" Text="Annulla" OnClick="AnnullaListMovAuto" />
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label ID="lblIdMovAuto" runat="server" Width="60px" Text='<%# Eval("id") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescrzMov" runat="server" Width="90px" ToolTip='<%# Eval("id_tipo_movimento") %>' 
                                Text='<%# Eval("descrzione") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="NumRiferimento" runat="server" ForeColor="Green" AlternateText='<%# Eval("num_riferimento") %>'  CommandName="VisualizzaDocum" />
                        </td>
                        <td>
                            <asp:Label ID="lblTarga" runat="server" Width="90px" Text='<%# Eval("targa") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazUscita" runat="server" Width="150px" Text='<%# Eval("StazUscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStazRientro" runat="server" Width="150px" Text='<%# Eval("StazRientro") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataUscita" runat="server" Width="160px" Text='<%# Eval("data_uscita") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDataRientro" runat="server" Width="160px" Text='<%# Eval("data_rientro") %>'></asp:Label>
                        </td>
                        <td align="center" width="40px">
                            <asp:ImageButton ID="SelezMovAuto" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezMovAuto"/>
                        </td>                        
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
</div>
<div id="VisualConducenti" runat="server">
    <uc1:anagrafica_conducenti ID="anagrafica_conducenti1" runat="server" Visible="false" />    
</div>
<div runat="server" id="tab_incassi" visible="false" >
    <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
</div>
<div runat="server" id="VisualFatture" visible="false">
    <uc1:FattureMulte ID="FattureMulte1" runat="server" />
</div>
<div runat="server" id="VisulEnti" visible="false">
    <uc1:Enti ID="Enti1" runat="server" />
</div>
<div runat="server" id="VisualArticoliCDS" visible="false">
    <uc1:ArticoliCDS ID="ArticoliCDS1" runat="server" />
</div>
<div id="VisualizzaDitte" runat="server">
    <uc1:anagrafica_ditte ID="anagrafica_ditte1" runat="server" Visible="false" />    
</div>
<div id="div_steps" style="width:100%; clear:both;" runat="server" visible="false">
    <asp:tabcontainer 
            ID="tabPanelSteps" runat="server" ActiveTabIndex="2" 
            Width="100%">
     <asp:tabpanel ID="tabRinotifica" runat="server" 
        HeaderText="Rinotifica">
        <HeaderTemplate>Rinotifica</HeaderTemplate>
     <ContentTemplate><div id="divRinotifica" runat="server" style="min-height:500px; "><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Dati conducente</b> </td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Nominativo <asp:ImageButton ID="btnSelezionaConducente" runat="server" 
                            ImageUrl="/images/aggiorna.png" style="height: 16px; " /></td><td>Data nascita</td><td>Luogo nascita</td><td>Indirizzo</td><td>C.A.P.</td><td>Comune</td><td>Prov.</td><td>Nazione</td></tr><tr><td><asp:TextBox ID="txtConducente" runat="server" Width="220px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtDataNascitaCond" runat="server" Width="80px" Enabled="false"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataNascitaCond" ID="CalendarExtender3"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataNascitaCond" ID="MaskedEditExtender4"></asp:MaskedEditExtender></td>
                                <td><asp:TextBox ID="txtLuogoNascitaCond" runat="server" Width="150px" 
                            Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtIndirizzoCond" runat="server" Width="150px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtCapCond" runat="server" Width="40px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtComuneCond" runat="server" Width="150px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtProvCond" runat="server" Width="30px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtNazioneCond" runat="server" Width="100px" Enabled="false"></asp:TextBox></td></tr><tr><td>Numero patente</td><td>Data rilascio</td><td>Luogo rilascio</td><td>Scadenza</td><td>Categ.</td><td>Cod.Cliente</td><td>&nbsp;</td><td runat="server" visible ="false">ID cond. </td></tr><tr><td><asp:TextBox ID="txtPatente" runat="server" Width="220px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtDataRilascioPatente" runat="server" Width="80px" Enabled="false"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataRilascioPatente" ID="CalendarExtender4"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataRilascioPatente" ID="MaskedEditExtender5"></asp:MaskedEditExtender></td><td><asp:TextBox ID="txtLuogoRilascioPatente" runat="server" Width="150px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtScadPatente" runat="server" Width="150px" Enabled="false"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtScadPatente" ID="CalendarExtender5"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtScadPatente" ID="MaskedEditExtender6"></asp:MaskedEditExtender></td><td><asp:TextBox ID="txtCatPatente" runat="server" Width="40px" Enabled="false"></asp:TextBox></td><td><asp:TextBox ID="txtCodCliente" runat="server" Width="80px" Enabled="false"></asp:TextBox></td><td>&nbsp;</td><td><asp:TextBox ID="txtIDCond" runat="server" Width="80px" Enabled="false" Visible="false"></asp:TextBox></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Dati noleggio</b> </td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Contratto di noleggio</td><td>Dalla stazione</td><td>Alla stazione</td><td>Dalla data e ora</td><td>Alla data e ora</td><td>numero Carta di credito</td><td>Scadenza</td></tr><tr><td><asp:TextBox ID="txtNumContratto" runat="server" Width="120px" MaxLength="10" onKeyPress="return filterInput(event)" ></asp:TextBox><asp:ImageButton ID="ImageButtnVisualizzaRA" runat="server" 
                            ImageUrl="/images/lente.png" style="height: 16px; " /></td><td><asp:TextBox ID="txtDaStazioneNolo" runat="server" Width="120px" MaxLength="25"></asp:TextBox></td><td><asp:TextBox ID="txtAStazioneNolo" runat="server" Width="120px" MaxLength="25"></asp:TextBox></td>
                                
                               <td>
                                   <a onclick="Calendar.show(document.getElementById('<%=txtDaDataNolo.ClientID%>'), '%d/%m/%Y', false)">
                                   <asp:TextBox ID="txtDaDataNolo" runat="server" Width="90px" MaxLength="10"></asp:TextBox></a>
                                   
                                   <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaDataNolo" ID="CalendarExtender7"></asp:CalendarExtender>--%>
                                   <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaDataNolo" ID="MaskedEditExtender8"></asp:MaskedEditExtender><asp:TextBox ID="txtDaOraNolo" runat="server" Width="40px" MaxLength="5"></asp:TextBox><asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaOraNolo" ID="MaskedEditExtender7"></asp:MaskedEditExtender></td>
                                    <td>
                                        <a onclick="Calendar.show(document.getElementById('<%=txtAdataNolo.ClientID%>'), '%d/%m/%Y', false)">
                                        <asp:TextBox ID="txtAdataNolo" runat="server" Width="90px" MaxLength="10"></asp:TextBox></a>
                                        <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAdataNolo" ID="CalendarExtender8"></asp:CalendarExtender>--%>
                                        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAdataNolo" ID="MaskedEditExtender9">
                                        </asp:MaskedEditExtender>
                                                                                                         
                                       <asp:TextBox ID="txtAOraNolo" runat="server" Width="40px" MaxLength="5"></asp:TextBox>
                                       <asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAOraNolo" ID="MaskedEditExtender13"></asp:MaskedEditExtender>
                                </td>
                                
                                <td>
                                    <asp:TextBox ID="txtNumeroCartaCred" runat="server" Width="170px" 
                            MaxLength="25"></asp:TextBox></td><td><asp:TextBox ID="txtScadCartaCred" runat="server" Width="50px" MaxLength="5"></asp:TextBox><asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtScadCartaCred" ID="MaskedEditExtender10"></asp:MaskedEditExtender></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Dati vendita automezzo o altri reponsabili dell'infrazione</b> </td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Acquirente</td><td>Data Vendita</td><td>N.fatt.</td><td>Cod.cliente <asp:ImageButton ID="btnSelezDitta" runat="server" 
                            ImageUrl="/images/aggiorna.png" style="height: 16px; " /></td><td>Altro responsabile</td><td>Movimento</td><td>Data Movimento</td><td>Descrizione</td></tr><tr><td><asp:TextBox ID="txtAcquirente" runat="server" Width="160px" MaxLength="50"></asp:TextBox></td>
                                <td>
                                     <a onclick="Calendar.show(document.getElementById('<%=txtDataVendita.ClientID%>'), '%d/%m/%Y', false)">
                                    <asp:TextBox ID="txtDataVendita" runat="server" Width="90px" MaxLength="10"></asp:TextBox></a>
                                    <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataVendita" ID="CalendarExtender10"></asp:CalendarExtender>--%>
                                    <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataVendita" ID="MaskedEditExtender11">

                                    </asp:MaskedEditExtender>

                                </td><td><asp:TextBox ID="txtNumFattVendita" runat="server" Width="60px"></asp:TextBox></td><td><asp:TextBox ID="txtCodClienteVendita" runat="server" Width="90px" 
                            MaxLength="10"></asp:TextBox></td><td><asp:TextBox ID="txtAltroResponsabile" runat="server" Width="160px" 
                            MaxLength="50"></asp:TextBox></td><td><asp:TextBox ID="txtNumDocAltro" runat="server" Width="70px" MaxLength="10"></asp:TextBox></td>
                                <td>
                                    <a onclick="Calendar.show(document.getElementById('<%=txtDataDocAltro.ClientID%>'), '%d/%m/%Y', false)">
                                    <asp:TextBox ID="txtDataDocAltro" runat="server"  MaxLength="10" style="Width:110px; text-align:center;"></asp:TextBox></a>
                                    <%--<asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataDocAltro" ID="CalendarExtender11"></asp:CalendarExtender>--%>
                                    <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataDocAltro" ID="MaskedEditExtender12">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td><asp:TextBox ID="txtDescrizAltro" runat="server" Width="200px" MaxLength="60"></asp:TextBox></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Casistica</td><td>&nbsp;</td><td colspan="2">&nbsp;</td><td>Salva doc.</td><td>&nbsp;<!-- Selez.manuale doc.--></td></tr><tr><td>
                                    <asp:DropDownList ID="DropCasistiche" runat="server" AutoPostBack="True" 
                                            DataSourceID="sqlCasistiche" DataTextField="Casistica" DataValueField="ID" 
                            style="margin-left: 0px" AppendDataBoundItems="True"><asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem></asp:DropDownList></td><td><asp:Button ID="btnRicorsoPrevisto" runat="server" Text="Apri ricorso previsto" 
                        Font-Bold="True" Font-Size="Small" ForeColor="White" style="height: 22px" ValidationGroup="DatiVerbale" /></td><td><asp:Button ID="btnComunicazCliente" runat="server" Text="Apri lettera cliente"  
                        Font-Bold="True" Font-Size="Small" ForeColor="White" style="height: 22px" ValidationGroup="DatiVerbale" /></td><td></td><td align="center"><asp:CheckBox ID="CheckSalvaComeAllegato" Text="" runat="server" /></td><td><asp:Button ID="btnAlriDatiRicorso" runat="server" Text="Visualizza ulteriori dati per ricorsi" /></td></tr></table><br /><div id="DivAltriDatiRicorso" runat="server" visible="false"><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Ulteriori dati necessari per ricorsi particolari</b> </td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td colspan="6"><asp:Label ID="lblAltriDatiArt126bis" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Dati necessari per il ricorso Art.126/bis:"></asp:Label></td></tr><tr><td>Dati prec.verb.</td><td>Num. verbale</td><td>Data verbale</td><td>Ora verbale</td><td>Data notifica</td><td>Punti decurt.</td></tr><tr><td>&nbsp;&nbsp;</td><td><asp:TextBox ID="TxtNumPrecVerb" runat="server" Width="150px" MaxLength="25"></asp:TextBox></td><td><asp:TextBox ID="TxtDataPrecVerb" runat="server" Width="100px" MaxLength="10"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDataPrecVerb" ID="CalendarExtender6"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="TxtDataPrecVerb" ID="MaskedEditExtender14"></asp:MaskedEditExtender></td><td><asp:TextBox ID="TxtOraPrecVerb" runat="server" Width="40px" MaxLength="5"></asp:TextBox><asp:MaskedEditExtender runat="server" Mask="99:99" MaskType="Time" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="TxtOraPrecVerb" ID="MaskedEditExtender15"></asp:MaskedEditExtender></td><td><asp:TextBox ID="txtDataNotificaPrecVerb" runat="server" Width="100px" MaxLength="10"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataNotificaPrecVerb" ID="CalendarExtender9"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataNotificaPrecVerb" ID="MaskedEditExtender16"></asp:MaskedEditExtender></td><td><asp:TextBox ID="txtPuntiDecurt" runat="server" Width="50px" MaxLength="2"></asp:TextBox></td></tr><tr><td>Dati prec. Spediz.</td><td>Num. Raccomandata</td><td>Data spedizione</td></tr><tr><td>&nbsp;&nbsp;</td><td><asp:TextBox ID="txtNumRaccPrecVerb" runat="server" Width="150px" MaxLength="25"></asp:TextBox></td><td><asp:TextBox ID="txtDataRaccPrecVerb" runat="server" Width="100px" MaxLength="10"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataRaccPrecVerb" ID="CalendarExtender12"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataRaccPrecVerb" ID="MaskedEditExtender17"></asp:MaskedEditExtender></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td><asp:Label ID="lblVeicoloDiv" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Dati necessari per il modello veicolo diverso:"></asp:Label></td></tr><tr><td>Modello veicolo errato.</td><td>Modello veicolo corretto.</td></tr><tr><td><asp:TextBox ID="txtModelloErrato" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td><td><asp:TextBox ID="txtModelloCorretto" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td><asp:Label ID="lblFurtoAppropiazioneIndebita" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Dati necessari per il furto e appropriazione indebita:"></asp:Label></td></tr><tr><td>Luogo presentazione denuncia</td><td>Data della denuncia</td></tr><tr><td><asp:TextBox ID="txtLuogoDenuncia" runat="server" Width="800px" MaxLength="200" Text="deposta presso l'Ufficio Polizia ..." ></asp:TextBox></td><td><asp:TextBox ID="txtDataDenuncia" runat="server" Width="100px" MaxLength="10"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataDenuncia" ID="CalendarExtender19"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataDenuncia" ID="MaskedEditExtender24"></asp:MaskedEditExtender></td></tr><tr><td colspan="2">Allegati</td></tr><tr><td colspan="2"><asp:TextBox ID="txtAllegatiRicorso" runat="server" Width="500px" Height="80px" TextMode="MultiLine"></asp:TextBox></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td><asp:Label ID="lblVeicoloAltroLuogo" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Dati necessari per il veicolo che si trovava in altro luogo:"></asp:Label></td></tr><tr><td>Comune ed indirizzo dell'infrazione riportati nel verbale da contestare</td></tr><tr><td><asp:TextBox ID="txtIndirInfraz" runat="server" Width="1000px" MaxLength="100" Text="Comune... in Via..." ></asp:TextBox></td></tr><tr><td>Descrizione del testo da integrare al ricorso</td></tr><tr><td><asp:TextBox ID="txtdescrAltroLuogo" runat="server" Width="1000px" Height="100px" TextMode="MultiLine"></asp:TextBox></td></tr></table></div></div></ContentTemplate>
    </asp:tabpanel>
    <asp:tabpanel ID="TabAllegInvioDoc" runat="server" 
        HeaderText="Allegati e invio doc.">
        <HeaderTemplate>Allegati e invio doc.</HeaderTemplate><ContentTemplate>
            <div id="divAllegInvioDoc" runat="server" style="height:500px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Documenti allegati alla gestione della multa</b> </td></tr>

                </table>
                <asp:Panel ID="PanelAllegati" runat="server" ScrollBars="Auto"><div id="allegati" runat="server" visible="true" style="max-height:120px">
                    <table cellpadding="0" cellspacing="2" width="100%" style="font-size:x-small;" border="0" runat="server" id="table2">
                        <tr><td>
                            <asp:ListView ID="ListViewAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="Id">
                                <ItemTemplate>
                                    <tr style="background-color:#DCDCDC; color: #000000;">
                                    <%--<td runat="server" visible="false"></td>--%>
                                        <td><asp:Label ID="lblIdAllegato" runat="server"   Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTipo" runat="server"  Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                        <td><asp:Label ID="lblNomeFile" runat="server"  Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                        <td  style="text-align:center;"><asp:Label ID="lbl_data_creazione" runat="server"  Text='<%# Eval("datacreazione") %>'></asp:Label></td>
                                        <td  style="text-align:center;">
                                            <asp:Label ID="lbl_operatore" runat="server" Text='<%# Eval("operatore") %>'></asp:Label>
                                            <asp:Label ID="lblPercorsoFile" runat="server"  Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                        <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                        <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" 
                                           OnClientClick="return confirm('Confermi eliminazione allegato');" CommandName="EliminaAllegato"/></td>
                                        <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                    </tr>
                                </ItemTemplate>

                                <AlternatingItemTemplate>
                                    <tr style="">
                                        <%--<td></td>--%>
                                        <td><asp:Label ID="lblIdAllegato" runat="server"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTipo" runat="server"  Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                        <td><asp:Label ID="lblNomeFile" runat="server"  Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                        <td  style="text-align:center;"><asp:Label ID="lbl_data_creazione" runat="server"  Text='<%# Eval("datacreazione") %>'></asp:Label></td>
                                        <td  style="text-align:center;"><asp:Label ID="lbl_operatore" runat="server"  Text='<%# Eval("operatore") %>'></asp:Label>
                                            <asp:Label ID="lblPercorsoFile" runat="server"  Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                        <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                        <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione allegato');" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                                        <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                    </tr>           
                                </AlternatingItemTemplate>

                                <EmptyDataTemplate>
                                    <table id="Table1" runat="server" style="">
                                        <tr>
                                            <td>Non vi sono allegati collegati alla multa. </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                
                                <LayoutTemplate>
                                    <table id="Table2" runat="server" width="100%">
                                        <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server">
                                                <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                                    <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                                        <%--<th id="Th1" runat="server" visible="false">Id</th>--%>
                                                        <th id="Th2"  style="width:200px;"  runat="server">Tipo</th>
                                                        <th id="Th3" runat="server">Nome File</th>
                                                        <th id="Th11" style="text-align:center;width:130px;" runat="server">Data e Ora</th>
                                                        <th id="Th4"  style="text-align:center;width:160px;" runat="server">Operatore</th>
                                                        <th id="Th5" runat="server"></th>
                                                        <th id="Th6" runat="server"></th>
                                                        <th id="Th7" runat="server"></th>
                                                    </tr>
                                                    
                                                    <tr ID="itemPlaceholder" runat="server"></tr>
                                                </table></td></tr></table>
                                </LayoutTemplate>
                                
                                <SelectedItemTemplate>
                                    <tr style="">
                                        <%--<td ></td>--%>
                                        <td><asp:Label ID="lblIdAllegato" runat="server"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTipo" runat="server"  Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                        <td><asp:Label ID="lblNomeFile" runat="server"  Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                        <td><asp:Label ID="lbl_data_creazione" runat="server"  Text='<%# Eval("datacreazione") %>'></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_operatore" runat="server"  Text='<%# Eval("operatore") %>'></asp:Label>
                                            <asp:Label ID="lblPercorsoFile" runat="server"  Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                        <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                        <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione allegato');" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                                        <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                    </tr>
                                </SelectedItemTemplate>

                            </asp:ListView>

                            </td></tr></table>
                    </div>

                </asp:Panel><hr style="clear:both; color:#369061;" /><table><tr><td><asp:Label ID="lblRilevaAutom" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Rileva allegati:"></asp:Label></td><td colspan="2"><asp:Button ID="btnRilevaAllegAuto" runat="server" Text="Avvia ricerca" Width="100px" /></td><td>&nbsp;<asp:Button ID="btnAggiornaListaAllegati" runat="server" Text="Aggiorna Lista allegati" /></td><td><asp:Label ID="lblMessUploadFile" Text="" runat="server"></asp:Label></td></tr><tr><td><asp:Label ID="lblUploaMaunDoc" runat="server" Font-Bold="true" Font-Size="Small" 
                                ForeColor="#369061"  Text="Upload documenti:"></asp:Label></td><td>&nbsp; <asp:Label ID="lblTipoAlleg" runat="server" Text="Tipo Doc." CssClass="testo_bold"></asp:Label></td><td ><asp:DropDownList ID="DropTipoAllegato" runat="server" AppendDataBoundItems="True"
                                  DataSourceID="sqlTipoAllegato" DataTextField="TipoAllegato" DataValueField="Id"><asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem></asp:DropDownList></td><td ><asp:FileUpload ID="FileUploadAllegati" size="42" runat="server" /></td><td><asp:Button ID="btnMemorizzaAlleg" runat="server" Text="Memorizza allegato" ValidationGroup="Upload_Allegati" /></td></tr></table> 
            </ContentTemplate><Triggers>
                <asp:PostBackTrigger ControlID="btnMemorizzaAlleg" />
            </Triggers><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Invio E-Mail</b> </td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td style="width: 35%"><asp:Label ID="lblDestinatarioMail" runat="server" Text="Destinatario"></asp:Label></td><td style="width: 65%"><asp:Label ID="lblTestoMail" runat="server" Text="Testo"></asp:Label></td></tr><tr><td><asp:TextBox ID="txtDestinatarioMail" runat="server" Width="320px" ></asp:TextBox></td><td rowspan="5"><asp:TextBox ID="txtTestoMail" runat="server" Width="550px" Height="100px"  TextMode="multiline" ></asp:TextBox><asp:Button ID="btnInviaEmail" runat="server" Width="90px" Height="70px" BorderWidth="4px" 
                    ValidationGroup="InvioEmail" Text="Invia E-Mail&#10;con allegati&#10;selezionati"  /></td></tr><tr><td><asp:Label ID="lblPerConoscenzaMail" runat="server" Text="Per conoscenza"></asp:Label></td></tr><tr><td><asp:TextBox ID="txtPerConoscenzaMail" runat="server" Width="320px" ></asp:TextBox></td></tr><tr><td><asp:Label ID="lblOggettoMail" runat="server" Text="Oggetto"></asp:Label></td></tr><tr><td><asp:TextBox ID="txtOggettoMail" runat="server" Width="320px" ></asp:TextBox></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Lista Mail inviate</b> </td></tr></table>
        <asp:Panel ID="PanelListaMailInviate" runat="server" ScrollBars="Auto"><div id="ListaMailInviate" runat="server" visible="true" style="max-height:120px"><table cellpadding="0" cellspacing="2" width="100%" style="font-size:x-small;" border="0" runat="server" id="table4"><tr><td><asp:ListView ID="ListViewMailInviate" runat="server" DataSourceID="sqlMailInviate" DataKeyNames="idMail"><ItemTemplate><tr style="background-color:#DCDCDC; color: #000000;"><td align="left"><asp:Label ID="lblIdMail" runat="server" CssClass="testo" Width="20px"  Text='<%# Eval("idMail") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDataInvio" runat="server" CssClass="testo" Width="50px" Text='<%# Eval("DataInvio") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDestinatario" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Destinatario") %>'></asp:Label></td><td align="left"><asp:Label ID="lblPerConoscenza" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("PerConoscenza") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOggetto" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Oggetto") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOperatore" runat="server" CssClass="testo" Width="120px" Text='<%# Eval("username") %>'></asp:Label></td><td align="left"><a runat="server" id="vediMail" href='<%# "gestione_multe/multa_vedi_mail.aspx?Id_Mail=" & Eval("idMail") %>' rel="lyteframe" title="" rev="width: 720px; height: 720px; scrolling: yes;"><asp:Image ID="image_vediMail" runat="server" ImageUrl="images/lente.png" /></a></td></tr></ItemTemplate><AlternatingItemTemplate><tr style=""><td align="left"><asp:Label ID="lblIdMail" runat="server" CssClass="testo" Width="20px"  Text='<%# Eval("idMail") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDataInvio" runat="server" CssClass="testo" Width="50px" Text='<%# Eval("DataInvio") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDestinatario" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Destinatario") %>'></asp:Label></td><td align="left"><asp:Label ID="lblPerConoscenza" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("PerConoscenza") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOggetto" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Oggetto") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOperatore" runat="server" CssClass="testo" Width="120px" Text='<%# Eval("username") %>'></asp:Label></td><td align="left"><a runat="server" id="vediMail" href='<%# "gestione_multe/multa_vedi_mail.aspx?Id_Mail=" & Eval("idMail") %>' rel="lyteframe" title="" rev="width: 720px; height: 720px; scrolling: yes;"><asp:Image ID="image_vediMail" runat="server" ImageUrl="images/lente.png" /></a></td></tr></AlternatingItemTemplate><EmptyDataTemplate><table id="Table1" runat="server" style=""><tr><td>Non vi sono mail inviate. </td></tr></table></EmptyDataTemplate><LayoutTemplate><table id="Table2" runat="server" width="100%"><tr id="Tr1" runat="server"><td id="Td1" runat="server"><table ID="itemPlaceholderContainer" runat="server" border="1" 
                                                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%"><tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso"><th id="Th1" runat="server">Id</th><th id="Th2" runat="server">Data invio</th><th id="Th3" runat="server">Destinatario</th><th id="Th4" runat="server">Per conoscenza</th><th id="Th5" runat="server">Oggetto</th><th id="Th6" runat="server">Operatore</th><th id="Th7" runat="server"></th></tr><tr ID="itemPlaceholder" runat="server"></tr></table></td></tr></table></LayoutTemplate><SelectedItemTemplate><tr style=""><td align="left"><asp:Label ID="lblIdMail" runat="server" CssClass="testo" Width="20px"  Text='<%# Eval("idMail") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDataInvio" runat="server" CssClass="testo" Width="50px" Text='<%# Eval("DataInvio") %>'></asp:Label></td><td align="left"><asp:Label ID="lblDestinatario" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Destinatario") %>'></asp:Label></td><td align="left"><asp:Label ID="lblPerConoscenza" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("PerConoscenza") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOggetto" runat="server" CssClass="testo" Width="150px" Text='<%# Eval("Oggetto") %>'></asp:Label></td><td align="left"><asp:Label ID="lblOperatore" runat="server" CssClass="testo" Width="120px" Text='<%# Eval("username") %>'></asp:Label></td><td align="left"><a runat="server" id="vediMail" href='<%# "gestione_multe/multa_vedi_mail.aspx?Id_Mail=" & Eval("idMail") %>' rel="lyteframe" title="" rev="width: 720px; height: 720px; scrolling: yes;"><asp:Image ID="image_vediMail" runat="server" ImageUrl="images/lente.png" /></a></td></tr></SelectedItemTemplate></asp:ListView></td></tr></table></div></asp:Panel></div><asp:ValidationSummary ID="ValidationSummary4" runat="server" 
        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
        ValidationGroup="Upload_Allegati" /><asp:CompareValidator ID="CompareValidator4" runat="server" 
        ControlToValidate="DropTipoAllegato" ErrorMessage="Specificare il tipo del documento."
        Type="Integer" Operator="GreaterThan" ValueToCompare="0"
        Font-Size="0pt" ValidationGroup="Upload_Allegati" ></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
    ControlToValidate="FileUploadAllegati" ErrorMessage="Nessuna file selezionato." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload_Allegati"></asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
        ValidationGroup="InvioEmail" /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
    ControlToValidate="txtDestinatarioMail" ErrorMessage="Nessun indirizzo E-mail del destinatario indicato." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="InvioEmail"></asp:RequiredFieldValidator><asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtDestinatarioMail"
    ErrorMessage="Formato dell'E-mail non corretto." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="InvioEmail"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
    ControlToValidate="txtOggettoMail" ErrorMessage="Nessun oggetto E-mail indicato." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="InvioEmail"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
    ControlToValidate="txtTestoMail" ErrorMessage="Nessun testo E-mail indicato." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="InvioEmail"></asp:RequiredFieldValidator></ContentTemplate>
    </asp:tabpanel>
    <asp:tabpanel ID="tabAddServFee" runat="server" 
        HeaderText="Addebiti e Fatturazione">
        <HeaderTemplate>addebiti e Fatturazione</HeaderTemplate><ContentTemplate><div id="divAddebiti" runat="server" style="height:400px">
              <table cellpadding="0" cellspacing="2" width="1000px" border="0" runat="server" id="table3">
                   <tr>
                     <td>
                       <asp:Label ID="lblServFeeDaInc" runat="server" Text="Servizio Fee"></asp:Label>
                       <asp:TextBox ID="txtServFeeDaInc" runat="server" Width="80px" onKeyPress="return filterInput(event)"></asp:TextBox>
                      </td>
                      <td>
                        <asp:Label ID="lblMultaDaInc" runat="server" Text="Multa"></asp:Label>
                        <asp:TextBox ID="txtMultaDaInc" runat="server" Width="80px" onKeyPress="return filterInput(event)" ></asp:TextBox>
                      </td>
                      <td>
                        <asp:Label ID="lblAltroDaInc" runat="server" Text="Altro"></asp:Label>
                        <asp:TextBox ID="txtAltroDaInc" runat="server" Width="80px" onKeyPress="return filterInput(event)" ></asp:TextBox>
                      </td>
                      <td>
                        <asp:Label ID="lblTotDaInc" runat="server" Font-Bold="true" Text="totale"></asp:Label>
                        <asp:TextBox ID="txtTotDaInc" runat="server" Font-Bold="true" Width="100px" Enabled="false" ></asp:TextBox>
                      </td>
                      <td>
                        <asp:Button ID="btnAggiornaTotInc" runat="server" Text="Aggiorna Totale da inc." />
                      </td>
                      <td>
                        <asp:Button ID="btnIncassi" runat="server" Text="Procedi incasso" />
                      </td>
                      <td>
                        <asp:DropDownList ID="dropTest" runat="server" AppendDataBoundItems="True" visible="false">
                             <asp:ListItem Selected="True" Value="0">Test</asp:ListItem>
                             <asp:ListItem Value="1">Reale</asp:ListItem>
                        </asp:DropDownList>
                      </td>
                    </tr>
                   </table>
                   <br />
                   <div runat="server" id="tab_dettagli_pagamento" visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                        <td align="left" style="color: #FFFFFF" bgcolor="#369061">
                           <b>Dettaglio dell'operazione POS selezionata</b> 
                        </td>
                      </tr>
                     </table>
                     <br />
                     <table style="border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="100%">
                       <tr>
                        <td class="style40">
                          <asp:Label ID="funzione" runat="server" Text="Funzione" CssClass="testo_bold" />
                        </td>
                        <td class="style34">
                          <asp:Label ID="Label25" runat="server" Text="Staz." CssClass="testo_bold" />
                        </td>
                        <td class="style5">
                          <asp:Label ID="Label8" runat="server" Text="Operatore" CssClass="testo_bold" />
                        </td>
                        <td class="style38">
                          <asp:Label ID="Label34" runat="server" Text="Cassa" CssClass="testo_bold" />
                        </td>
                        <td class="style36">
                          <asp:Label ID="Label35" runat="server" Text="Carta" CssClass="testo_bold" />
                        </td>
                        <td class="style37">
                          <asp:Label ID="Label36" runat="server" Text="Intestatario" CssClass="testo_bold" />
                        </td>
                        <td class="style39">
                          <asp:Label ID="Label37" runat="server" Text="Scadenza" CssClass="testo_bold" />
                        </td>
                        <td>
                          <asp:Label ID="Label67" runat="server" Text="Data Operazione" CssClass="testo_bold" />
                        </td>
                       </tr>
                       <tr>
                        <td class="style40">
                           <asp:TextBox ID="txtPOS_Funzione" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style34">
                           <asp:TextBox ID="txtPOS_Stazione" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style5">
                           <asp:TextBox ID="txtPOS_Operatore" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style38">
                           <asp:TextBox ID="txtPOS_Cassa" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style36">
                           <asp:TextBox ID="txtPOS_Carta" runat="server" Width="140px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style37">
                           <asp:TextBox ID="txtPOS_Intestatario" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style39">
                           <asp:TextBox ID="txtPOS_Scadenza" runat="server" Width="86px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                           <asp:TextBox ID="txtPOS_DataOperazione" runat="server" Width="126px" ReadOnly="true"></asp:TextBox>
                        </td>
                       </tr>
                       <tr>
                        <td class="style40">
                          <asp:Label ID="Label66" runat="server" Text="Terminal ID." CssClass="testo_bold" />
                        </td>
                        <td class="style34">
                          <asp:Label ID="Label38" runat="server" Text="Nr. Aut." CssClass="testo_bold" visible="false"/>
                        </td>
                        <td class="style5">
                          <asp:Label ID="Label69" runat="server" Text="Nr. Preaut." CssClass="testo_bold" />
                        </td>
                        <td class="style38">
                          <asp:Label ID="Label68" runat="server" Text="Nr. Batch" CssClass="testo_bold" visible="false"/>
                        </td>
                        <td class="style36">
                          <asp:Label ID="Label70" runat="server" Text="Scadenza Preaut." CssClass="testo_bold" />
                        </td>
                        <td class="style37">
                            &nbsp; <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold" />   
                        </td>
                        <td class="style39">
                            <asp:Label ID="Label71" runat="server" Text="Acquire Id" CssClass="testo_bold" visible="false"/>
                        </td>
                        <td>                         
                            <asp:Label ID="Label73" runat="server" Text="Action Code" CssClass="testo_bold" visible="false"/>
                        </td>
                       </tr>
                       <tr>
                        <td class="style40">
                          <asp:TextBox ID="txtPOS_TerminalID" runat="server" Width="84px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style34">
                          <asp:TextBox ID="txtPOS_NrAut" runat="server" Width="65px" ReadOnly="true" visible="false"></asp:TextBox>
                        </td>
                        <td class="style5">
                          <asp:TextBox ID="txtPOS_NrPreaut" runat="server" Width="158px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style38">
                          <asp:TextBox ID="txtPOS_BATCH" runat="server" Width="52px" ReadOnly="true" visible="false"></asp:TextBox>
                        </td>
                        <td class="style36">
                          <asp:TextBox ID="txtPOS_ScadenzaPreaut" runat="server" Width="140px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style37">
                            <asp:TextBox ID="txtPOS_Stato" runat="server" Width="74px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="style39">                             
                             <asp:TextBox ID="txtPOS_AcquireID" runat="server" Width="100px" ReadOnly="true" visible="false"></asp:TextBox>
                        </td>
                        <td>
                          &nbsp;
                        </td>
                       </tr>

                       <tr>
                        <td class="style40">
                         <asp:Label ID="Label72" runat="server" Text="Transation Type" CssClass="testo_bold" visible="false"/>
                        </td>
                        <td class="style34">
                         
                        </td>
                        <td class="style5">
                          &nbsp;
                        </td>
                        <td class="style38">
                          &nbsp;
                        </td>
                        <td class="style36">
                          &nbsp;
                        </td>
                        <td class="style37">
                          &nbsp;
                        </td>
                        <td class="style39">
                          &nbsp;
                        </td>
                        <td>
                          &nbsp;
                        </td>
                       </tr>


                       <tr>
                        <td class="style40">
                          <asp:TextBox ID="txtPOS_ActionCode" runat="server" Width="65px" ReadOnly="true" visible="false"></asp:TextBox>
                        </td>
                        <td class="style34">
                             <asp:TextBox ID="txtPOS_TransationType" runat="server" Width="86px" ReadOnly="true" visible="false"></asp:TextBox>
                         
                        </td>
                        <td class="style5" >
                        </td>
                        <td class="style38">
                          &nbsp;
                        </td>
                        <td class="style36">
                          &nbsp;
                        </td>
                        <td class="style37">
                          &nbsp;
                        </td>
                        <td class="style39">
                          &nbsp;
                        </td>
                        <td>
                          &nbsp;
                        </td>
                       </tr>
                       <tr>
                        <td class="style33" colspan="8" align="center">
                          <asp:Button ID="btnChiudiDettPag" runat="server" Text="Chiudi" />
                          <asp:Button ID="btnModificaDataPagamento" runat="server" Text="Modifica Data"  />   

                          <asp:Button ID="btnEliminaPagamento" runat="server" Text="Elimina Pagamento" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler eliminare il pagamento?'));"  />
                          <asp:Button ID="btnVisualizzaCC" runat="server" Text="Visualizza Numero Carta" />
                            &nbsp;<asp:Label ID="lblPasswordCC" runat="server" Text="PASSWORD: " CssClass="testo_bold" />
                            <asp:TextBox ID="txtPasswordCC" runat="server" Width="105px" ReadOnly="false" TextMode="Password"></asp:TextBox>
                        </td>
                       </tr>
                      </table>
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td align="left" style="color: #FFFFFF" bgcolor="#369061">
                        <b>Elenco delle operazioni POS eseguite</b> 
                      </td>
                     </tr>
                   </table>
                   <asp:Panel ID="PanelPagamenti" runat="server" ScrollBars="Auto">
                    <div id="DivListPagamenti" runat="server" visible="true" style="max-height:120px">
                       <asp:ListView ID="listPagamenti" runat="server" DataKeyNames="ID_CTR" DataSourceID="sqlDettagliPagamento">
                        <ItemTemplate>
                            <tr style="background-color:#DCDCDC;color: #000000;">
                                <td align="left">
                                   <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text="multa" />
                                </td>
                                <td align="left">
                                   <asp:Label ID="lb_ID_ModPag" runat="server" Text='<%# Eval("ID_ModPag") %>' Visible="false" />
                                   <asp:Label ID="ID_TIPPAG" runat="server" Text='<%# Eval("ID_TIPPAG") %>' Visible="false" />
                                      <asp:Label ID="lb_Des_ID_ModPag" runat="server" Text='<%# Eval("Des_ID_ModPag") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                                    <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                                    <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                   <asp:Label ID="lblStato" runat="server" Visible="true" />
                                   <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                                   <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td align="left">
                                    <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text="multa" />
                                </td>
                                <td align="left">
                                   <asp:Label ID="lb_ID_ModPag" runat="server" Text='<%# Eval("ID_ModPag") %>' Visible="false" />
                                   <asp:Label ID="ID_TIPPAG" runat="server" Text='<%# Eval("ID_TIPPAG") %>' Visible="false" />
                                   <asp:Label ID="lb_Des_ID_ModPag" runat="server" Text='<%# Eval("Des_ID_ModPag") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                                    <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                                    <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                   <asp:Label ID="lblStato" runat="server" Visible="true" />
                                   <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                                   <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
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
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table id="Table2" runat="server" width="100%">
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server">
                                        <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                                  style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                            <tr id="Tr2" runat="server"  style="color: #FFFFFF" bgcolor="#19191b">
                                                <th id="Th6" runat="server" align="left">
                                                    <asp:Label ID="Label45" runat="server" Text="Fonte" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th21" runat="server" align="left">
                                                    <asp:Label ID="Label38" runat="server" Text="Tipo" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th1" runat="server" align="left">
                                                    <asp:Label ID="Label22" runat="server" Text="Modalità" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th2" runat="server" align="left">
                                                    <asp:Label ID="Label18" runat="server" Text="Stato" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th3" runat="server" align="left">
                                                    <asp:Label ID="Label11" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th4" runat="server" align="left">
                                                    <asp:Label ID="Label16" runat="server" Text="Importo" CssClass="testo_titolo"></asp:Label>
                                                </th>
                                                <th id="Th5" runat="server">
                                      
                                                </th>
                                            </tr>
                                            <tr ID="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server">
                                    <td id="Td2" runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
            
                    </asp:ListView>


                      <%--<asp:ListView ID="listPagamenti" runat="server" DataKeyNames="ID_CTR" DataSourceID="sqlDettagliPagamento">
                        <ItemTemplate>
                          <tr style="background-color:#DCDCDC;color: #000000;">
                           <td align="left">
                             <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text="multa" />
                           </td>
                           <td align="left">
                             <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                             <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                             <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="lblStato" runat="server" Visible="true" />
                             <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                             <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                           </td>
                           <td align="left">
                             <asp:Label ID="lblEsitoTransazione" runat="server" Text='<%# Eval("ESITO_TRANSAZIONE") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                           </td>
                          </tr>
                         </ItemTemplate>
                         <AlternatingItemTemplate>
                          <tr style="">
                           <td align="left">
                             <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text="multa" />
                           </td>
                           <td align="left">
                             <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                             <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                             <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="lblStato" runat="server" Visible="true" />
                             <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                             <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                           </td>
                           <td align="left">
                             <asp:Label ID="lblEsitoTransazione" runat="server" Text='<%# Eval("ESITO_TRANSAZIONE") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                           </td>
                           <td align="left">
                             <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
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
                              </td>
                             </tr>
                           </table>
                         </EmptyDataTemplate>
                         <LayoutTemplate>
                           <table id="Table2" runat="server" width="100%">
                             <tr id="Tr1" runat="server"><td id="Td1" runat="server">
                               <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                 <tr id="Tr2" runat="server"  style="color: #FFFFFF" bgcolor="#19191b">
                                  <th id="Th6" runat="server" align="left">
                                    <asp:Label ID="Label45" runat="server" Text="Fonte" CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th1" runat="server" align="left">
                                     <asp:Label ID="Label22" runat="server" Text="Tipo Op." CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th2" runat="server" align="left">
                                     <asp:Label ID="Label18" runat="server" Text="Stato" CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th10" runat="server" align="left">
                                     <asp:Label ID="Label2" runat="server" Text="Esito" CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th3" runat="server" align="left">
                                     <asp:Label ID="Label11" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th4" runat="server" align="left">
                                     <asp:Label ID="Label16" runat="server" Text="Importo" CssClass="testo_titolo"></asp:Label>
                                   </th>
                                   <th id="Th5" runat="server">
                                   </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server"></tr></table></td></tr><tr id="Tr3" runat="server"><td id="Td2" runat="server" style=""></td></tr></table></LayoutTemplate>
                               </asp:ListView>--%>
                               
                               
                               
                               
                               <!-- Elenco Fatture Emesse -->
                               <asp:Label ID="livello_accesso_dettaglio_pos" runat="server" Visible="false"></asp:Label><asp:Label ID="livello_accesso_gestMulte" runat="server" Visible="false"></asp:Label></div></asp:Panel><br />
                               <div id="divFatturazione" runat="server" style="height:200px">
                               <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Elenco Fatture Emesse</b> </td>
                                </tr>
                               </table>
                               <div id="DivFattureEmesse" runat="server" visible="true" style="max-height:120px">
                                    <asp:ListView ID="ListViewFattureEmesse" runat="server" DataKeyNames="id" DataSourceID="SqlFattureEmesse">
                                        <ItemTemplate>
                                            <tr style="background-color:#DCDCDC;color: #000000;">
                                                <td>
                                                    <asp:Label ID="ID_Fattura" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                                    <asp:Label ID="lbl_codice_fattura" runat="server" Text='<%# Eval("codice_fattura") %>' CssClass="testo" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_data_fattura" runat="server" Text='<%# Eval("data_fattura") %>' />
                                                 </td>
                                                 <td align="left">
                                                    <asp:Label ID="lbl_id_ditta" runat="server" Text='<%# Eval("id_ditta") %>' CssClass="testo" />
                                                 </td>
                                                 <td align="left">
                                                    <asp:Label ID="lbl_Intestazione" runat="server" Text='<%# Eval("Intestazione") %>' CssClass="testo" />
                                                 </td>
                                                 <td>
                                                    <asp:Label ID="lbl_TotaleImponibile" runat="server" Text='<%# Eval("TotaleImponibile") %>' Visible="false" CssClass="testo" />
                                                    <asp:Label ID="lbl_TotaleIVA" runat="server" Text='<%# Eval("TotaleIVA") %>' Visible="false" CssClass="testo" />
                                                    <asp:Label ID="lbl_TotaleFattura" runat="server" Text='<%# Eval("TotaleImponibile") + Eval("TotaleIVA")  %>' CssClass="testo" />
                                                 </td>
                                                 <td>
                                                    <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="EditFatture"/>                                                    
                                                 </td>
                                                 <td>
                                                    <asp:ImageButton ID="imgBtnStampaFattura" runat="server" ImageUrl="/images/pdf.png" style="width: 16px" CommandName="StampaFatture"/>
                                                 </td>
                                             </tr>
                                         </ItemTemplate>
                                         <AlternatingItemTemplate>
                                            <tr style="">
                                                <td>
                                                    <asp:Label ID="ID_Fattura" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                                    <asp:Label ID="lbl_codice_fattura" runat="server" Text='<%# Eval("codice_fattura") %>' CssClass="testo" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_data_fattura" runat="server" Text='<%# Eval("data_fattura") %>' />
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_id_ditta" runat="server" Text='<%# Eval("id_ditta") %>' CssClass="testo" />
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_Intestazione" runat="server" Text='<%# Eval("Intestazione") %>' CssClass="testo" />
                                                 </td>
                                                 <td>
                                                    <asp:Label ID="lbl_TotaleImponibile" runat="server" Text='<%# Eval("TotaleImponibile") %>' Visible="false" CssClass="testo" />
                                                    <asp:Label ID="lbl_TotaleIVA" runat="server" Text='<%# Eval("TotaleIVA") %>' Visible="false" CssClass="testo" />
                                                    <asp:Label ID="lbl_TotaleFattura" runat="server" Text='<%# Eval("TotaleImponibile") + Eval("TotaleIVA")  %>' CssClass="testo" />
                                                 </td>
                                                 <td>
                                                    <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="EditFatture"/>
                                                 </td>
                                                 <td>
                                                    <asp:ImageButton ID="imgBtnStampaFattura" runat="server" ImageUrl="/images/pdf.png" style="width: 16px" CommandName="StampaFatture"/>
                                                 </td>
                                             </tr>
                                          </AlternatingItemTemplate>
                                          <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style=""><tr><td></td></tr>
                                            </table>
                                          </EmptyDataTemplate>
                                          <LayoutTemplate>
                                            <table id="Table2" runat="server" width="100%">
                                                <tr id="Tr1" runat="server">
                                                    <td id="Td1" runat="server">
                                                        <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="Tr2" runat="server"  style="color: #FFFFFF" bgcolor="#19191b">
                                                                <th id="Th1" runat="server" align="left">
                                                                    <asp:Label ID="Label45" runat="server" Text="N.Fattura" CssClass="testo_titolo"></asp:Label>
                                                                </th>
                                                                <th id="Th2" runat="server" align="left">
                                                                    <asp:Label ID="Label22" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label>
                                                                </th>
                                                                <th id="Th3" runat="server" align="left">
                                                                    <asp:Label ID="Label18" runat="server" Text="Cod.Ditta" CssClass="testo_titolo"></asp:Label>
                                                                </th>
                                                                <th id="Th4" runat="server" align="left">
                                                                    <asp:Label ID="Label2" runat="server" Text="Intestazione" CssClass="testo_titolo"></asp:Label>
                                                                </th>
                                                                <th id="Th5" runat="server" align="left">
                                                                    <asp:Label ID="Label11" runat="server" Text="Totale Fattura" CssClass="testo_titolo"></asp:Label>
                                                                </th>
                                                                <th id="Th6" runat="server">
                                                                </th>
                                                                <th id="Th10" runat="server">
                                                                </th>
                                                            </tr>
                                                            <tr ID="itemPlaceholder" runat="server">
                                                            </tr>
                                                       </table>
                                                     </td>
                                                   </tr>
                                                   <tr id="Tr3" runat="server">
                                                        <td id="Td2" runat="server" style="">
                                                        </td>
                                                   </tr>
                                                 </table>
                                              </LayoutTemplate>
                                            </asp:ListView>
                                           </div>
                                           <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="center"><asp:Button ID="btnNuovaFattura" runat="server"  Text="Nuova Fattura" /></td></tr></table></div></div></ContentTemplate>
    </asp:tabpanel>
    <asp:tabpanel ID="tabNoteStatus" runat="server" 
        HeaderText="Note e Stato delle operazioni eseguite" Visible="false">
        <HeaderTemplate>Note e Stato delle operazioni eseguite</HeaderTemplate>
        <ContentTemplate><div id="divNoteStatus" runat="server" style="height:400px"><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Note</b> </td></tr></table><asp:TextBox ID="txtNote" runat="server" Width="1000px" Height="70px" TextMode="MultiLine"></asp:TextBox><br /><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td align="left" style="color: #FFFFFF" bgcolor="#369061"><b>Status e riepilogo delle operazioni eseguite</b> </td></tr></table><br /><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Ricorso/Rinotifica </td><td align="left"><asp:CheckBox ID="chkRicorsoYesNo" runat="server" /></td><td align="right">Spedito con racc. n.: </td><td><asp:TextBox ID="txtNumRaccomandata" runat="server" Width="120px" MaxLength="20" onKeyPress="return filterInput(event)"></asp:TextBox></td><td align="right">del: </td><td><asp:TextBox ID="txtDataRacc" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataRacc" ID="CalendarExtender16"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataRacc" ID="MaskedEditExtender21"></asp:MaskedEditExtender></td><td align="right">Inoltrato a mezzo fax in data: </td><td><asp:TextBox ID="txtDataInoltroFax" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataInoltroFax" ID="CalendarExtender13"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataInoltroFax" ID="MaskedEditExtender18"></asp:MaskedEditExtender></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr><tr><td>Comunicaz. cliente </td><td align="left"><asp:CheckBox ID="chkComunizYesNo" runat="server" /></td><td colspan="4">&nbsp;&nbsp;</td><td align="right">Inoltrato a mezzo e-mail in data: </td><td><asp:TextBox ID="txtDataInoltroMail" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataInoltroMail" ID="CalendarExtender14"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataInoltroMail" ID="MaskedEditExtender19"></asp:MaskedEditExtender></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr><tr><td>Incassato </td><td align="left"><asp:CheckBox ID="chkIncassatoYesNo" runat="server" /></td><td align="right">Tentativi: </td><td><asp:TextBox ID="txtTentativiInc" runat="server" MaxLength="2" Width="20px" onKeyPress="return filterInput(event)"></asp:TextBox></td><td colspan="2" align="right">Motivaz. mancato inc.: </td><td colspan="2"><asp:DropDownList ID="DropTipoMancInc" runat="server" AppendDataBoundItems="True"
                          DataSourceID="SqlTipoMancIncMulte" DataTextField="DescrMancIncasso" DataValueField="Id" Width="120px"><asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem></asp:DropDownList></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr><tr><td>Rimborsato </td><td align="left"><asp:CheckBox ID="chkRimborsatoYesNo" runat="server" /></td><td align="right">il: </td><td><asp:TextBox ID="txtRimborsatoData" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtRimborsatoData" ID="CalendarExtender17"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRimborsatoData" ID="MaskedEditExtender22"></asp:MaskedEditExtender></td><td align="right">importo: </td><td><asp:TextBox ID="txtRimborsatoImporto" runat="server" Width="100px" MaxLength="8" onKeyPress="return filterInput(event)"></asp:TextBox></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr><tr><td>Pagato </td><td align="left"><asp:CheckBox ID="ChkPagatoYesNo" runat="server" /></td><td align="right">il: </td><td><asp:TextBox ID="txtPagatoData" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtPagatoData" ID="CalendarExtender18"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtPagatoData" ID="MaskedEditExtender23"></asp:MaskedEditExtender></td><td align="right">importo: </td><td><asp:TextBox ID="txtPagatoImporto" runat="server" Width="100px" MaxLength="8" onKeyPress="return filterInput(event)"></asp:TextBox></td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr><tr><td>Fatturato </td><td align="left"><asp:CheckBox ID="chkFatturatoYesNo" runat="server" /></td><td colspan="6">&nbsp;&nbsp;</td></tr><tr><td colspan="8"><hr style="clear:both; color:#369061;" /></td></tr></table><table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>Terzi Locatori (es. Leasys) </td><td><asp:DropDownList ID="DropLocatori" runat="server" AppendDataBoundItems="True"
                          DataSourceID="sqlLocatori" DataTextField="Locatore" DataValueField="Id" Width="120px"><asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem></asp:DropDownList></td><td align="right">Numero fattura: </td><td><asp:TextBox ID="txtNumFattTerziLoc" runat="server" MaxLength="10" Width="80px" ></asp:TextBox></td><td colspan="2" align="right">Data fattura: </td><td colspan="2"><asp:TextBox ID="txtDataFattTerziLoc" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataFattTerziLoc" ID="CalendarExtender15"></asp:CalendarExtender><asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFattTerziLoc" ID="MaskedEditExtender20"></asp:MaskedEditExtender></td></tr></table></div></ContentTemplate>
    </asp:tabpanel>
    </asp:tabcontainer>
</div>

<asp:Label ID="livello_accesso_eliminare_pagamenti" runat="server" Text='' Visible="false"></asp:Label>
<asp:Label ID="idPagamentoExtra" runat="server" Text='' Visible="false"></asp:Label>

<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" HeaderText="ERRORI RISCONTRATI:"
    ValidationGroup="DatiVerbale" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="txtEnteComune" ErrorMessage="Nessun Comune inserito." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="txtCap" ErrorMessage="Nessuna CAP inserito." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
    ControlToValidate="txtProv" ErrorMessage="Nessuna Provincia inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
    ControlToValidate="txtVerbale" ErrorMessage="Nessuna num.verbale inserito." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
    ControlToValidate="txtNotifica" ErrorMessage="Nessuna data notifica inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 
    
<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropArtCDS" ErrorMessage="Nessun articolo CDS selezionato."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="DatiVerbale" ></asp:CompareValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
    ControlToValidate="txtTarga" ErrorMessage="Nessuna targa inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
    ControlToValidate="txtDataInfrazione" ErrorMessage="Nessuna data dell'infrazione inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
    ControlToValidate="txtOraInfrazione" ErrorMessage="Nessuna ora dell'infrazione inserita." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale"></asp:RequiredFieldValidator> 

<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtOraInfrazione"
    ErrorMessage="Formato dell'ora non corretto." ValidationExpression="(2[0-3]|[01]\d|\d).[0-5]\d"
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="DatiVerbale">
    </asp:RegularExpressionValidator>

<asp:SqlDataSource ID="sqlProvenienza" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [ID],[Provenienza] FROM [multe_provenienza] WITH(NOLOCK) ORDER BY ID">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlArtCDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [idCDS],[CDS] FROM [multe_articoliCDS] WITH(NOLOCK) ORDER BY CDS">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlEnti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [ID],[Ente] FROM [multe_enti] WITH(NOLOCK) ORDER BY ENTE">
</asp:SqlDataSource>    

<asp:SqlDataSource ID="sqlCasistiche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [ID],[Casistica] FROM [multe_casistiche] WITH(NOLOCK) LEFT JOIN multe_ModelloRicorsi ON multe_casistiche.modelloID=multe_ModelloRicorsi.idModello WHERE multe_ModelloRicorsi.idTipoAllegato<>0  ORDER BY casistica">
</asp:SqlDataSource>    

<asp:SqlDataSource ID="sqlMovAuto" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [id], [num_riferimento] FROM [movimenti_targa] WITH(NOLOCK) WHERE ID=0">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlTipoAllegato" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [Id], [TipoAllegato] FROM [multe_TipoAllegato] WITH(NOLOCK) order by TipoAllegato">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlAllegati" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT multe_Allegati.Id, rtrim(multe_TipoAllegato.TipoAllegato) as TipoAllegato, multe_Allegati.NomeFile, multe_Allegati.PercorsoFile, 
    operatori.cognome + ' ' + operatori.nome AS Operatore, dataCreazione FROM multe_Allegati WITH (NOLOCK) INNER JOIN multe_TipoAllegato WITH (NOLOCK) 
    ON multe_Allegati.IdTipoDocumento = multe_TipoAllegato.Id LEFT OUTER JOIN  operatori ON multe_Allegati.id_operatore = operatori.id 
    WHERE (multe_Allegati.IdMulta > 0) order by dataCreazione DESC">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT MOD_PAG.ID_ModPag, MOD_PAG.Descrizione Des_ID_ModPag, ID_TIPPAG, POS_Funzioni.funzione, PAGAMENTI_EXTRA.id_pos_funzioni_ares, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.operazione_stornata, PAGAMENTI_EXTRA.ESITO_TRANSAZIONE FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag = MOD_PAG.ID_ModPag WHERE (N_MULTA_RIF = @N_MULTA_RIF)">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblID" Name="N_MULTA_RIF" 
            PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>




<asp:SqlDataSource ID="sqlMailInviate" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT multe_InvioMail.IdMulta, multe_InvioMail.idMail, multe_InvioMail.DataInvio, multe_InvioMail.Destinatario, multe_InvioMail.PerConoscenza, multe_InvioMail.Oggetto, multe_InvioMail.Testo, operatori.username FROM multe_InvioMail WITH(NOLOCK) INNER JOIN operatori WITH(NOLOCK) ON multe_InvioMail.UtenteId = operatori.id WHERE (multe_InvioMail.IdMulta = @MULTA_RIF)">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblID" Name="MULTA_RIF" 
            PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlFattureEmesse" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, codice_fattura, anno_fattura, id_riferimento, id_pagamento, data_fattura, id_ditta, Intestazione, TotaleImponibile, TotaleIVA FROM Fatture WHERE (id_riferimento = @MULTA_RIF) AND (attiva = 1) AND (tipo_fattura = 4)">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblID" Name="MULTA_RIF" 
            PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlLocatori" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [Id], [Locatore] FROM [multe_Locatori] WITH(NOLOCK)">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlTipoMancIncMulte" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT [Id], [DescrMancIncasso] FROM [multe_TipoMancIncMulte]">
</asp:SqlDataSource>

    </div>



</asp:Content>

