<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="MulteLocatori.aspx.vb" Inherits="MulteLocatori" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />
  <style type="text/css">
        .style1
        {        	  
            width: 146px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style2
      {
          width: 87px;
      }
        .button 
        {
        border:none;
        border:0px;
        margin:0px;
        padding:0px;
        background: #369061;
		}
        .button:hover {background: #444;}
        .allineatestoadx {text-align:right;}
        .centrotesto {text-align:center;}
</style> 
<script type="text/javascript" src="gestione_multe/InputImportoRidotto.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table border="0" cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td align="left" style="color: #FFFFFF; font-size:large;" bgcolor="#369061"  >
                    <b>Gestione multe di terzi Locatori</b>
                </td>
            </tr>
        </table>
        <div id="divMulteLocatori" runat="server" style="min-height:500px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblLocatore" runat="server" Text="Locatore" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNumFatturaLoc" runat="server" Text="Num. fatt. Locatore" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataFattLoc" runat="server" Text="Data fatt. Locatore" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTotFattLoc" runat="server" Text="Tot. fatt. Locatore" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSelezFile" runat="server" Text="Seleziona il file da importare" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
             </tr>
             <tr>
                <td >
                    <asp:DropDownList ID="DropLocatori" runat="server" AppendDataBoundItems="True"
                          DataSourceID="sqlLocatori" DataTextField="Locatore" DataValueField="Id" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtNumFattLoc" runat="server" Width="120px" MaxLength="9" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtDataFattLoc" runat="server" Width="120px"></asp:TextBox>
                        <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataFattLoc" ID="CalendarExtender16">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFattLoc" ID="MaskedEditExtender21">
                        </asp:MaskedEditExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtTotFattLoc" runat="server" Width="110px" MaxLength="6" onKeyPress="return filterInput(event)"></asp:TextBox>
                </td>
                <td >
                    <asp:FileUpload ID="FileUploadFattLocatori" size="42" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnImportaFattura" runat="server" Text="Importa fattura" ValidationGroup="ImportFattura" />
                </td>
            </tr>
        </table>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImportaFattura" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <asp:Label ID="lblElencoFattImportate" runat="server" Font-Bold="true" Font-Size="Large" 
                        ForeColor="#369061"  Text="Elenco fatture locatori importate"></asp:Label>
        <asp:Panel ID="PanelFattureLoc" runat="server" ScrollBars="Auto" style="max-height:300px;">
            <table cellpadding="0" cellspacing="2" width="100%" style="font-size:small;" border="0" runat="server" id="table2">
              <tr>
                <td>
                    <asp:ListView ID="ListViewFattLocatori" runat="server" DataSourceID="sqlFattureLocatori" DataKeyNames="Id">
                        <ItemTemplate>
                            <tr style="background-color:#DCDCDC; color: #000000;">
                                <td>
                                    <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_IdLocatore" runat="server" Width="20px"  Text='<%# Eval("IdLocatore") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_Locatore" runat="server" Width="100px"  Text='<%# Eval("Locatore") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_NFattura" runat="server" Width="50px" Text='<%# Eval("NFattura") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_DataFattura" runat="server" Width="100px" Text='<%# Format(Eval("DataFattura"), "dd/MM/yyyy") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_TotFattura" runat="server" Width="80px" Text='<%# Format(Eval("TotFattura"), "0.00") %>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="~/images/ingranaggi.gif" style="width: 16px" CommandName="ElaboraFattLoc"/>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="StampaFattLoc" runat="server" ImageUrl="~/images/print.ico" style="width: 16px" CommandName="StampaFattLoc"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_IdLocatore" runat="server" Width="20px"  Text='<%# Eval("IdLocatore") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_Locatore" runat="server" Width="100px"  Text='<%# Eval("Locatore") %>'></asp:Label>
                                 </td>
                                <td>
                                    <asp:Label ID="lbl_NFattura" runat="server" Width="50px" Text='<%# Eval("NFattura") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_DataFattura" runat="server" Width="100px" Text='<%# Format(Eval("DataFattura"), "dd/MM/yyyy") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_TotFattura" runat="server" Width="80px" Text='<%# Format(Eval("TotFattura"), "0.00") %>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="~/images/ingranaggi.gif" style="width: 16px" CommandName="ElaboraFattLoc"/>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="StampaFattLoc" runat="server" ImageUrl="~/images/print.ico" style="width: 16px" CommandName="StampaFattLoc"/>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>
                                    Non vi sono fatture Locatori.
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
                                                        Locatore</th>
                                                    <th id="Th2" runat="server">
                                                        Num. Fattura</th>
                                                    <th id="Th3" runat="server">
                                                        Data Fattura</th>
                                                    <th id="Th4" runat="server">
                                                        Tot. Fattura</th>
                                                    <th id="Th5" runat="server">
                                                        Elab.</th>
                                                    <th id="Th6" runat="server">
                                                        Stampa</th>
                                            </tr>
                                            <tr ID="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_IdLocatore" runat="server" Width="20px"  Text='<%# Eval("IdLocatore") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_Locatore" runat="server" Width="100px"  Text='<%# Eval("Locatore") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_NFattura" runat="server" Width="50px" Text='<%# Eval("NFattura") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_DataFattura" runat="server" Width="100px" Text='<%# Format(Eval("DataFattura"), "dd/MM/yyyy") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_TotFattura" runat="server" Width="80px" Text='<%# Format(Eval("TotFattura"), "0.00") %>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="~/images/ingranaggi.gif" style="width: 16px" CommandName="ElaboraFattLoc"/>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="StampaFattLoc" runat="server" ImageUrl="~/images/print.ico" style="width: 16px" CommandName="StampaFattLoc"/>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </td>
              </tr>
            </table>
            <table cellpadding="0" cellspacing="2" width="100%" border="0" runat="server" id="table7">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnChiudiFattLoc" runat="server" Text="Chiudi" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
        <div id="divElaboraFattLoc" runat="server" style="min-height:500px">
            <table cellpadding="0" cellspacing="2" width="100%" border="0" runat="server" id="table3">
                <tr>
                    <td align="right">
                        Dettaglio fattura Locatore n:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRifFattLoc" runat="server" Width="100px" Enabled="false" ></asp:TextBox>
                    </td>
                    <td align="right">
                        Del:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRifDataFattLoc" runat="server" Width="120px" Enabled="false" ></asp:TextBox>
                    </td>
                    <td align="right">
                        Locatore:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRifLocatore" runat="server" Width="150px" Enabled="false" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="PanelDettaglioFattLoc" runat="server" ScrollBars="Auto" style="max-height:210px;">
                <table cellpadding="0" cellspacing="2" width="100%" style="font-size:x-small;" border="0" runat="server" id="table5">
                  <tr>
                    <td>
                        <asp:ListView ID="ListViewDettFattLoc" runat="server" DataSourceID="SqlDettaglioFattLoc" DataKeyNames="Id">
                            <ItemTemplate>
                                <tr style="background-color:#DCDCDC; color: #000000;">
                                    <td>
                                        <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_IdFattLoc" runat="server" Width="20px"  Text='<%# Eval("idFatturaLoc") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Targa" runat="server" Width="60px"  Text='<%# Eval("Targa") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_DataInfrazione" runat="server" Width="80px" Text='<%# Eval("DataInfrazione") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_NumVerbale" runat="server" Width="80px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Ente" runat="server" Width="120px" Text='<%# Eval("Ente") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ComuneEnte" runat="server" Width="120px" Text='<%# Eval("ComuneEnte") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Importo" runat="server" Width="70px" Text='<%# Format(Eval("Importo"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Spese" runat="server" Width="70px" Text='<%# Format(Eval("Spese"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Totale" runat="server" Width="70px" Text='<%# Format(Eval("Totale"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_idMulta" runat="server" Width="70px" Text='<%# Eval("IdMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_ProtMulta" runat="server" Width="70px" Text='<%# Eval("ProtMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgRicercaMulta" runat="server" ImageUrl="~/images/lente.png" style="width: 16px" CommandName="RicercaMulta"/>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgCancellaProt" runat="server" ImageUrl="~/images/cancella.jpg" style="width: 16px" CommandName="CancellaProt"/>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_IdFattLoc" runat="server" Width="20px"  Text='<%# Eval("idFatturaLoc") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Targa" runat="server" Width="60px"  Text='<%# Eval("Targa") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_DataInfrazione" runat="server" Width="80px" Text='<%# Eval("DataInfrazione") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_NumVerbale" runat="server" Width="80px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Ente" runat="server" Width="120px" Text='<%# Eval("Ente") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ComuneEnte" runat="server" Width="120px" Text='<%# Eval("ComuneEnte") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Importo" runat="server" Width="70px" Text='<%# Format(Eval("Importo"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Spese" runat="server" Width="70px" Text='<%# Format(Eval("Spese"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Totale" runat="server" Width="70px" Text='<%# Format(Eval("Totale"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_idMulta" runat="server" Width="70px" Text='<%# Eval("IdMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_ProtMulta" runat="server" Width="70px" Text='<%# Eval("ProtMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgRicercaMulta" runat="server" ImageUrl="~/images/lente.png" style="width: 16px" CommandName="RicercaMulta"/>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgCancellaProt" runat="server" ImageUrl="~/images/cancella.jpg" style="width: 16px" CommandName="CancellaProt"/>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <EmptyDataTemplate>
                            <table id="Table1" runat="server" style="">
                                <tr>
                                    <td>
                                        Non vi sono fatture Locatori.
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
                                                            Targa</th>
                                                        <th id="Th2" runat="server">
                                                            Data Infraz.</th>
                                                        <th id="Th3" runat="server">
                                                            Num.Verb.</th>
                                                        <th id="Th4" runat="server">
                                                            Ente</th>
                                                        <th id="Th5" runat="server">
                                                            Comune Ente</th>
                                                        <th id="Th7" runat="server">
                                                            Importo</th>                                                                                                                                                                                    
                                                        <th id="Th8" runat="server">
                                                            Spese</th>                                                                                                                                                                                    
                                                        <th id="Th9" runat="server">
                                                            Totale</th>                                                                                                                                                                                    
                                                        <th id="Th10" runat="server">
                                                            Id multa</th>                                                                                                                                                                                    
                                                        <th id="Th11" runat="server">
                                                            Prot.multa</th>                                                                                                                                                                                    
                                                        <th id="Th12" runat="server">
                                                            Seleziona</th>
                                                        <th id="Th6" runat="server">
                                                            canc.Prot</th>                                                            
                                                </tr>
                                                <tr ID="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <SelectedItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Label ID="lbl_id" runat="server" Width="20px"  Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_IdFattLoc" runat="server" Width="20px"  Text='<%# Eval("idFatturaLoc") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Targa" runat="server" Width="60px"  Text='<%# Eval("Targa") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_DataInfrazione" runat="server" Width="80px" Text='<%# Eval("DataInfrazione") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_NumVerbale" runat="server" Width="80px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Ente" runat="server" Width="120px" Text='<%# Eval("Ente") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ComuneEnte" runat="server" Width="120px" Text='<%# Eval("ComuneEnte") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Importo" runat="server" Width="70px" Text='<%# Format(Eval("Importo"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Spese" runat="server" Width="70px" Text='<%# Format(Eval("Spese"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_Totale" runat="server" Width="70px" Text='<%# Format(Eval("Totale"), "0.00") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_idMulta" runat="server" Width="70px" Text='<%# Eval("IdMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbl_ProtMulta" runat="server" Width="70px" Text='<%# Eval("ProtMulta") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgRicercaMulta" runat="server" ImageUrl="~/images/lente.png" style="width: 16px" CommandName="RicercaMulta"/>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgCancellaProt" runat="server" ImageUrl="~/images/cancella.jpg" style="width: 16px" CommandName="CancellaProt"/>
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
                    </td>
                  </tr>
                </table>
            </asp:Panel>
            <table border="3" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtTargaTemp" runat="server" Text="" Enabled="false" Font-Bold="true" Width="61px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDataInfrazTemp" runat="server" Text="" Enabled="false" Font-Bold="true" Width="78px" Font-Size="8" Height="25px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumVerbTemp" runat="server" Text="" Enabled="false" Font-Bold="true" Width="79px" Font-Size="9" Height="25px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEnteTemp" runat="server" Text="" Enabled="false" Font-Bold="true" Width="120px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtComuneEnteTemp" runat="server" Text="" Enabled="false" Font-Bold="true" Width="120px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImportoTemp" runat="server" Text="" Enabled="false" CssClass="allineatestoadx" Font-Bold="true" Width="70px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSpeseTemp" runat="server" Text="" Enabled="false" CssClass="allineatestoadx" Font-Bold="true" Width="68px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotaleTemp" runat="server" Text="" Enabled="false" CssClass="allineatestoadx" Font-Bold="true" Width="68px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdMultaTemp" runat="server" Text="" Enabled="false" CssClass="centrotesto" Font-Bold="true" Width="68px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProMultaTemp" runat="server" Text="" Enabled="false" CssClass="centrotesto" Font-Bold="true" Width="68px" Height="25px" Font-Size="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblRigaTemp" runat="server" Text="Riga selezionata" Width="138px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />           
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td align="left" style="color: #FFFFFF;" bgcolor="#369061"  >
                    <b>Elenco multe trovate in base a riga selezionata</b>
                </td>
            </tr>
            </table>
            <asp:Panel ID="PanelMulteTovate" runat="server" ScrollBars="Auto" style="max-height:160px;">
            <table cellpadding="0" cellspacing="2" width="100%" style="font-size:small;" border="0" runat="server" id="table6">
              <tr>
                <td>
                    <asp:ListView ID="ListViewMulte" runat="server" DataSourceID="sqlElencoMulte" DataKeyNames="ID">
                        <ItemTemplate>
                            <tr style="background-color:#DCDCDC; color: #000000;">
                                <td>
                                    <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumVerbale" runat="server" Width="70px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataNotifica" runat="server" Width="160px" Text='<%# IIF (Eval("DataNotifica")is nothing, "", Eval("DataNotifica", "{0:d}"))  %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTarga" runat="server" Width="60px" Text='<%# Eval("Targa") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreNumFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreNumFatt") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreDataFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreDataFatt", "{0:d}") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatore" runat="server" Width="60px" Text='<%# Eval("Locatore") %>'></asp:Label>
                                </td>
                                <td align="center" width="40px">
                                    <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="~/images/aggancia.jpg" style="width: 16px" CommandName="SelezMulta"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumVerbale" runat="server" Width="70px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataNotifica" runat="server" Width="160px" Text='<%# IIF (Eval("DataNotifica")is nothing, "", Eval("DataNotifica", "{0:d}"))  %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTarga" runat="server" Width="60px" Text='<%# Eval("Targa") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreNumFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreNumFatt") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreDataFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreDataFatt", "{0:d}") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatore" runat="server" Width="60px" Text='<%# Eval("Locatore") %>'></asp:Label>
                                </td>
                                <td align="center" width="40px">
                                    <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="~/images/aggancia.jpg" style="width: 16px" CommandName="SelezMulta"/>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>
                                    Nessuna riga di fattura Locatore selezionata.
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
                                                        Prot.</th>
                                                    <th id="Th3" runat="server">
                                                        Anno</th>
                                                    <th id="Th4" runat="server">
                                                        Num.Verbale</th>
                                                    <th id="Th5" runat="server">
                                                        Data Multa</th>
                                                    <th id="Th6" runat="server">
                                                        Data Notifica</th>
                                                    <th id="Th7" runat="server">
                                                        Importo</th>
                                                    <th id="Th8" runat="server">
                                                        Targa</th>
                                                    <th id="Th9" runat="server">
                                                        Ft.Loc.</th>
                                                    <th id="Th13" runat="server">
                                                        Data ft.loc</th>
                                                    <th id="Th14" runat="server">
                                                        Locatore</th>
                                                    <th id="Th10" runat="server">
                                                        Aggancia</th>
                                            </tr>
                                            <tr ID="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="lblIdMulta" runat="server" Width="60px"  Text='<%# Eval("ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProt" runat="server" Width="40px" Text='<%# Eval("Prot") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAnno" runat="server" Width="30px" Text='<%# Eval("Anno") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumVerbale" runat="server" Width="70px" Text='<%# Eval("NumVerbale") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataInfrazione" runat="server" Width="160px" Text='<%# IIF (Eval("DataInfrazione")is nothing, "", Eval("DataInfrazione")) %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataNotifica" runat="server" Width="160px" Text='<%# IIF (Eval("DataNotifica")is nothing, "", Eval("DataNotifica", "{0:d}"))  %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMultaImporto" runat="server" Width="60px" Text='<%# Eval("MultaImporto") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTarga" runat="server" Width="60px" Text='<%# Eval("Targa") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreNumFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreNumFatt") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatoreDataFatt" runat="server" Width="60px" Text='<%# Eval("LocatoreDataFatt", "{0:d}") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLocatore" runat="server" Width="60px" Text='<%# Eval("Locatore") %>'></asp:Label>
                                </td>
                                <td align="center" width="40px">
                                    <asp:ImageButton ID="SelezMulta" runat="server" ImageUrl="~/images/aggancia.jpg" style="width: 16px" CommandName="SelezMulta"/>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </td>
              </tr>
            </table>
            </asp:Panel>
            <table cellpadding="0" cellspacing="2" width="100%" border="0" runat="server" id="table4">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnRicercaAutMulte" runat="server" Text="Ricerca Automatica multe" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnChiudi" runat="server" Text="Chiudi" />
                    </td>
                </tr>
            </table>
        </div>
<asp:Label ID="lblidFatturaLocatore" runat="server" Visible="false"></asp:Label> 
<asp:Label ID="lblIdRigaFatturaLocSelezionata" runat="server" Visible="false"></asp:Label>        
<asp:Label ID="lblId_Locatore" runat="server" Visible="false"></asp:Label>        
<asp:ValidationSummary ID="ValidationSummary4" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="ImportFattura" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
ControlToValidate="DropLocatori" ErrorMessage="Nessun locatore selezionato." 
Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ImportFattura"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
ControlToValidate="txtNumFattLoc" ErrorMessage="Nessun numero fattura indicato." 
Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ImportFattura"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
ControlToValidate="txtDataFattLoc" ErrorMessage="Nessuna data fattura indicata." 
Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ImportFattura"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
ControlToValidate="txtTotFattLoc" ErrorMessage="Nessun importo fattura indicato." 
Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ImportFattura"></asp:RequiredFieldValidator> 

<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
ControlToValidate="FileUploadFattLocatori" ErrorMessage="Nessuna file selezionato." 
Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ImportFattura"></asp:RequiredFieldValidator> 
            
<asp:SqlDataSource ID="sqlLocatori" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [Id], [Locatore] FROM [multe_Locatori] WITH(NOLOCK)">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlFattureLocatori" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT multe_FattureLocatore.Id, multe_Locatori.Locatore, multe_Locatori.Id AS IdLocatore, multe_FattureLocatore.NFattura, multe_FattureLocatore.DataFattura, 
                      multe_FattureLocatore.TotFattura FROM multe_FattureLocatore WITH(NOLOCK) LEFT OUTER JOIN
                      multe_Locatori WITH(NOLOCK) ON multe_FattureLocatore.idLocatore = multe_Locatori.Id
                      ORDER BY multe_FattureLocatore.DataFattura DESC">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDettaglioFattLoc" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT Id, idFatturaLoc, Targa, DataInfrazione, NumVerbale, Ente, ComuneEnte, Importo, Spese, Totale, IdMulta, ProtMulta FROM multe_RigaFattLocatore WITH(NOLOCK) WHERE idFatturaLoc=0">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlElencoMulte" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" CacheDuration="0"
    SelectCommand="SELECT multe.ID, multe.Prot, multe.Anno, multe.NumVerbale, multe.DataInfrazione, multe.DataNotifica, multe.MultaImporto, multe.Targa, 
                      multe.LocatoreNumFatt, multe.LocatoreDataFatt, multe_Locatori.Locatore FROM multe WITH(NOLOCK) LEFT OUTER JOIN
                      multe_Locatori WITH(NOLOCK) ON multe.LocatoreId = multe_Locatori.Id WHERE multe.ID = 0">
</asp:SqlDataSource>        
</asp:Content>

