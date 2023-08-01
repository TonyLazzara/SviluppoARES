<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="StampaBolli.aspx.vb" Inherits="StampaBolli" title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function CalcolaBollo(Euro, KW, Tariffa, Mesi) {
            var valore = 0;
            // alert(Euro + " * " + KW + " * " + Tariffa + " * " + Mesi);
            // Bolli Annuali!------------------------------------
            if (Mesi == 12) {
                if (Tariffa == 1) {
                    if (KW >= 100) {
                        switch (Euro) {
                            case 0:
                                valore = 270 + (KW - 100) * 4.05;
                                break;
                            case 1:
                                valore = 261 + (KW - 100) * 3.92;
                                break;
                            case 2:
                                valore = 252 + (KW - 100) * 3.78;
                                break;
                            case 3:
                                valore = 243 + (KW - 100) * 3.65;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                valore = 232 + (KW - 100) * 3.48;
                                break;
                        }
                    } else {
                        switch (Euro) {
                            case 0:
                                valore = KW * 2.70;
                                break;
                            case 1:
                                valore = KW * 2.61;
                                break;
                            case 2:
                                valore = KW * 2.52;
                                break;
                            case 3:
                                valore = KW * 2.43;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                valore = KW * 2.32;
                                break;
                        }
                    }
                } else {
                    if (KW >= 100) {
                        switch (Euro) {
                            case 0:
                                valore = 300 + (KW - 100) * 4.50;
                                break;
                            case 1:
                                valore = 290 + (KW - 100) * 4.35;
                                break;
                            case 2:
                                valore = 280 + (KW - 100) * 4.20;
                                break;
                            case 3:
                                valore = 270 + (KW - 100) * 4.05;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                valore = 258 + (KW - 100) * 3.87;
                                break;
                        }
                    } else {
                        switch (Euro) {
                            case 0:
                                valore = KW * 3.00;
                                break;
                            case 1:
                                valore = KW * 2.90;
                                break;
                            case 2:
                                valore = KW * 2.80;
                                break;
                            case 3:
                                valore = KW * 2.70;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                valore = KW * 2.58;
                                break;
                        }
                    }
                }
                return valore;
            }
            // Bolli Frazionati!------------------------------------
            if (Tariffa == 1) {
                if (KW >= 100) {
                    switch (Euro) {
                        case 0:
                            valore = 278 + (KW - 100) * 4.13;
                            break;
                        case 1:
                            valore = 269 + (KW - 100) * 4.03;
                            break;
                        case 2:
                            valore = 259 + (KW - 100) * 3.90;
                            break;
                        case 3:
                            valore = 250 + (KW - 100) * 3.75;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            valore = 239 + (KW - 100) * 3.59;
                            break;
                    }
                } else {
                    switch (Euro) {
                        case 0:
                            valore = KW * 2.78;
                            break;
                        case 1:
                            valore = KW * 2.69;
                            break;
                        case 2:
                            valore = KW * 2.59;
                            break;
                        case 3:
                            valore = KW * 2.50;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            valore = KW * 2.39;
                            break;
                    }
                }
            } else {
                if (KW >= 100) {
                    switch (Euro) {
                        case 0:
                            valore = 309 + (KW - 100) * 4.59;
                            break;
                        case 1:
                            valore = 299 + (KW - 100) * 4.48;
                            break;
                        case 2:
                            valore = 288 + (KW - 100) * 4.33;
                            break;
                        case 3:
                            valore = 278 + (KW - 100) * 4.17;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            valore = 266 + (KW - 100) * 3.99;
                            break;
                    }
                } else {
                    switch (Euro) {
                        case 0:
                            valore = KW * 3.09;
                            break;
                        case 1:
                            valore = KW * 2.99;
                            break;
                        case 2:
                            valore = KW * 2.88;
                            break;
                        case 3:
                            valore = KW * 2.78;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            valore = KW * 2.66;
                            break;
                    }
                }
            }
            // alert("valore:" + valore);
            return valore / 12 * Mesi;
        }
        var blocco_ricorsione = 0;

        function AggiornaImporto(Elemento) {
//            if (blocco_ricorsione > 0)
//                return 0;
//            blocco_ricorsione = 1;

            //alert("Drop " + Drop.value);
            var txtImportoTassa = document.getElementById('<%= txtImportoTassa.UniqueID.replace("$","_") %>');
            var txtSanzioni = document.getElementById('<%= txtSanzioni.UniqueID.replace("$","_") %>');
            var txtInteressi = document.getElementById('<%= txtInteressi.UniqueID.replace("$","_") %>');
            var txtImporto = document.getElementById('<%= txtImporto.UniqueID.replace("$","_") %>');

            var txtKW = document.getElementById('<%= txtKW.UniqueID.replace("$","_") %>');
            var DropDownEuro = document.getElementById('<%= DropDownEuro.UniqueID.replace("$","_") %>');
            var rbTariffa = document.getElementsByName('<%= rbTariffa.UniqueID %>');
            var DropDownMesiValidita = document.getElementById('<%= DropDownMesiValidita.UniqueID.replace("$","_") %>'); 

            if (txtImportoTassa == null) {
                return 0;
            }
            if (txtSanzioni == null) {
                return 0;
            }
            if (txtInteressi == null) {
                return 0;
            }
            if (txtImporto == null) {
                return 0;
            }

            if (txtKW == null) {
                return 0;
            }
            if (DropDownEuro == null) {
                return 0;
            }
            if (rbTariffa == null) {
                return 0;
            }
            if (DropDownMesiValidita == null) {
                return 0;
            }

            //alert("Tutti elementi individuati");
            if (txtSanzioni.value == "") {
                txtSanzioni.value = "0.00";
            } else {
                if (!isNumber(txtSanzioni.value)) {
                    alert("L'importo delle sanzioni deve essere un valore numerico!");
                    return 0;
                }
                var sanzioni = eval(txtSanzioni.value);
                txtSanzioni.value = sanzioni.toFixed(2);
            }

            if (txtInteressi.value == "") {
                txtInteressi.value = "0.00";
            } else {
                if (!isNumber(txtInteressi.value)) {
                    alert("L'importo degli interessi deve essere un valore numerico!");
                    return 0;
                }
                var interessi = eval(txtInteressi.value);
                txtInteressi.value = interessi.toFixed(2);
            }

            if (Elemento == txtImportoTassa) {
                if (txtImportoTassa.value == "") {
                    txtImportoTassa.value = "0.00";
                } else {
                    if (!isNumber(txtImportoTassa.value)) {
                        alert("L'importo della tassa deve essere un valore numerico!");
                        return 0;
                    }
                    var importo = eval(txtImportoTassa.value);
                    txtImportoTassa.value = importo.toFixed(2);
                }
            }

            //alert("Verificati gli importi");
            if (Elemento == txtImportoTassa || Elemento == txtSanzioni || Elemento == txtInteressi) {
                var ImportoTotale = eval(txtImportoTassa.value) + eval(txtSanzioni.value) + eval(txtInteressi.value);
                txtImporto.value = ImportoTotale.toFixed(2);


                return 0;
                alert("Modifica solo importi!");
            }

            var Euro = eval(DropDownEuro.selectedIndex);
            var KW = eval(txtKW.value);
            var Tariffa = eval(getCheckedValue(rbTariffa));
            var Mesi = eval(DropDownMesiValidita.selectedIndex);

            if (!is_int(KW)) {
                alert("Il numero dei Kilowatt deve essere un numero intero!");
                return 0;
            }

//            if (Mesi == 0) {
//                alert("I mesi di validità non sono valorizzati!");
//                return 0;
//            }

            var ImportoTassa = CalcolaBollo(Euro, KW, Tariffa, Mesi);
            txtImportoTassa.value = ImportoTassa.toFixed(2);

            var ImportoTotale = eval(txtImportoTassa.value) + eval(txtSanzioni.value) + eval(txtInteressi.value);
            txtImporto.value = ImportoTotale.toFixed(2);

            return 0;
        }

        function getCheckedValue(radioObj) {
	        if(radioObj == null)
		        return "";
	        var radioLength = radioObj.length;
            // alert("radioLength(" + radioLength + ")");
	        if(radioLength == undefined)
		        if(radioObj.checked)
			        return radioObj.value;
		        else
			        return "";
	        for(var i = 0; i < radioLength; i++) {
		        if(radioObj[i].checked) {
			        return radioObj[i].value;
		        }
	        }
	        return "";
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
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

    </script>


<div id="DivElenco" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Intestazione Bollo</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
      <tr>
        <td> 
            
                                                       
            <asp:ListView ID="ListIntestazioni" runat="server" DataSourceID="SqlDataSourceIntestaziniBollo" DataKeyNames="id" >
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC;color: #000000;">
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Bollo" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Bollo"/>
                        </td>
                        <td>
                            <asp:Label ID="lb_IntestatoA" runat="server" Text='<%# Eval("IntestatoA") %>' />
                            <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="lb_ContoCorrente" runat="server" Text='<%# Eval("ContoCorrente") %>' />
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm ('Vuoi cancellare l\'intestazione selezionata?'));"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="">
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Bollo" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Bollo"/>
                        </td>
                        <td>
                            <asp:Label ID="lb_IntestatoA" runat="server" Text='<%# Eval("IntestatoA") %>' />
                            <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="lb_ContoCorrente" runat="server" Text='<%# Eval("ContoCorrente") %>' />
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                        </td>
                        <td align="center" width="32px">
                            <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm ('Vuoi cancellare l\'intestazione selezionata?'));" />
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
                    <table id="Table1" runat="server" width="100%">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr id="TrIntestazione" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                        <th id="Th5" runat="server">
                                          </th>
                                        <th id="Th1" runat="server">
                                            Intestazione</th>
                                        <th id="Th2" runat="server">
                                            Conto Corrente</th>
                                        <th id="Th3" runat="server">
                                          </th>
                                        <th id="Th4" runat="server">
                                          </th>
                                    </tr>
                                    <tr ID="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>                
            </asp:ListView>
           
   
        </td>
      </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
      <tr>
      
        <td align="center">
            <asp:Button ID="btnNuovaIntestazione" runat="server" Text="Nuova" />
            <asp:Button ID="btnChiudiElenco" runat="server" Text="Chiudi" />
        </td>
       
      </tr>
    </table>
</div>

<div id="DivEditIntestazione" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Bollo Auto</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
        <tr>
          <td class="style2" >    
              <asp:Label ID="Label13" runat="server" Text="Intestato A:" CssClass="testo_bold"></asp:Label>          
          </td>
          <td class="style3">
            <asp:TextBox ID="txtEdIntestatoA" runat="server" Width="99%"></asp:TextBox>
          </td>
            <td class="style2" >   
                <asp:Label ID="Label14" runat="server" Text="Conto Corrente:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtEdContoCorrente" runat="server" MaxLength="8" Width="80px"></asp:TextBox>
            </td>
        </tr>
       <tr>
          <td class="style4">  
             <asp:Label ID="Label15" runat="server" Text="Partita IVA o Codice Fiscale:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdPartitaIVA" runat="server" Width="210px" MaxLength="16"></asp:TextBox>
          </td>
          <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>       
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label16" runat="server" Text="Eseguito Da:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdEseguitoDa" runat="server" Width="210px" MaxLength="23"></asp:TextBox>
          </td>
          <td class="style4">  
             <asp:Label ID="Label17" runat="server" Text="Indirizzo:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdIndirizzo" runat="server" Width="210px" MaxLength="20"></asp:TextBox>
          </td>          
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label18" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdComune" runat="server" Width="210px" MaxLength="19"></asp:TextBox>
          </td>
          <td class="style4">  
             <asp:Label ID="Label19" runat="server" Text="CAP:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdCAP" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
          </td>          
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label20" runat="server" Text="Provincia:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEdProvincia" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
          </td>
          <td class="style4"> 
                <asp:Label ID="Label25" runat="server" Text="Tariffario:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style9">
              <asp:RadioButtonList ID="rbEdTariffa" runat="server" 
                  RepeatDirection="Horizontal" CssClass="testo_bold">
                  <asp:ListItem Value="0" Selected="True">Sicilia</asp:ListItem>
                  <asp:ListItem Value="1">Bolzano</asp:ListItem>
              </asp:RadioButtonList>
            </td>          
        </tr>
        <tr>
          <td class="style4">  
              &nbsp;</td>
          <td class="style9">           
              &nbsp;</td>
          <td class="style4">  
              &nbsp;</td>
          <td class="style9">           
              &nbsp;</td>          
        </tr>
        <tr>
          <td colspan="4" align="center">
            <asp:Button ID="btnSalvaIntestazione" runat="server" Text="Salva" ValidationGroup="Salva" /> 
            <asp:Button ID="btnChiudiEdit" runat="server" Text="Chiudi" />                     
          </td>
        </tr>  
    </table>
</div>

<asp:Label ID="lb_id_Intestazione" runat="server" Text="" Visible="false" />
<asp:Label ID="lb_predefinito" runat="server" Text="" Visible="false" />


<div id="DivDatiBollettino" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Bollo Auto</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
        <tr>
          <td class="style2" >    
              <asp:Label ID="lbIntestatoA" runat="server" Text="Intestato A:" CssClass="testo_bold"></asp:Label>          
          </td>
          <td class="style3">
            <asp:TextBox ID="txtIntestatoA" runat="server" Width="99%"></asp:TextBox>
          </td>
            <td class="style2" >   
                <asp:Label ID="lbContoCorrente" runat="server" Text="Conto Corrente:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtContoCorrente" runat="server" MaxLength="8" Width="80px"></asp:TextBox>
            </td>
        </tr>
       <tr>
          <td class="style4">  
             <asp:Label ID="Label3" runat="server" Text="Partita IVA o Codice Fiscale:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtPartitaIVA" runat="server" Width="210px" MaxLength="16"></asp:TextBox>
          </td>
          <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>       
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label5" runat="server" Text="Eseguito Da:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtEseguitoDa" runat="server" Width="210px" MaxLength="23"></asp:TextBox>
          </td>
          <td class="style4">  
             <asp:Label ID="Label6" runat="server" Text="Indirizzo:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtIndirizzo" runat="server" Width="210px" MaxLength="20"></asp:TextBox>
          </td>          
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label7" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtComune" runat="server" Width="210px" MaxLength="19"></asp:TextBox>
          </td>
          <td class="style4">  
             <asp:Label ID="Label8" runat="server" Text="CAP:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtCAP" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
          </td>          
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label9" runat="server" Text="Provincia:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtProvincia" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
          </td>
          <td class="style4">  
              &nbsp;</td>
          <td class="style9">           
              &nbsp;</td>          
        </tr>     
        <tr>
            <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
           <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4"> 
                <asp:Label ID="Label23" runat="server" Text="Tariffario:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style9">
              <asp:RadioButtonList ID="rbTariffa" runat="server" 
                  RepeatDirection="Horizontal" CssClass="testo_bold" onchange='javascript:AggiornaImporto(this);'>
                  <asp:ListItem Value="0" Selected="True">Sicilia</asp:ListItem>
                  <asp:ListItem Value="1">Bolzano</asp:ListItem>
              </asp:RadioButtonList>
            </td>
           <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4"> 
                <asp:Label ID="lbtarga" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>               
            </td>
            <td class="style9">
                <asp:TextBox ID="txtTarga" runat="server" MaxLength="11" Width="120px"></asp:TextBox>
                &nbsp; <asp:Label ID="lb_errore_targa" runat="server" Text="Targa non esistente" 
                    CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td class="style2" >   
                <asp:Label ID="Label10" runat="server" Text="Importo Tassa:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtImportoTassa" runat="server" MaxLength="9" Width="120px" onblur="javascript: AggiornaImporto(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4"> 
                <asp:Label ID="Label11" runat="server" Text="Euro:" CssClass="testo_bold"></asp:Label>               
            </td>
            <td class="style9">
                <asp:DropDownList ID="DropDownEuro" runat="server" onchange='javascript:AggiornaImporto(this);' >
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>  
                <asp:Label ID="Label12" runat="server" Text="Sanzioni:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtSanzioni" runat="server" MaxLength="9" Width="120px" onblur="javascript: AggiornaImporto(this);"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="style4"> 
                <asp:Label ID="Label21" runat="server" Text="KW:" CssClass="testo_bold"></asp:Label>               
            </td>
            <td class="style9">
                <asp:TextBox ID="txtKW" runat="server" MaxLength="3" Width="35px" onblur="javascript: AggiornaImporto(this);" ></asp:TextBox>
            </td>
            <td class="style2" >   
                <asp:Label ID="Label22" runat="server" Text="Interessi:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtInteressi" runat="server" MaxLength="9" Width="120px" onblur="javascript: AggiornaImporto(this);" ></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style4">  
             <asp:Label ID="Label1" runat="server" Text="Mesi Validità:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:DropDownList ID="DropDownMesiValidita" runat="server" onchange='javascript:AggiornaImporto(this);'>
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
          </td>   
            <td class="style2" >   
                <asp:Label ID="Label24" runat="server" Text="Importo Totale:" CssClass="testo_bold"></asp:Label>             
            </td>
            <td class="style3">
                <asp:TextBox ID="txtImporto" runat="server" MaxLength="9" Width="120px"></asp:TextBox>
            </td>
        </tr> 
        <tr>
           <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
           <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                <asp:Button ID="Button1" runat="server" Text="Stampa" Visible="false" OnClientClick='javascript:AggiornaImporto(this);' />
            </td>
        </tr>      
        <tr>
          <td class="style4">  
             <asp:Label ID="lbScadenza" runat="server" Text="Scadenza Mese:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:DropDownList ID="DropDownScadenzaMese" runat="server" >
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
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
          <td class="style4">  
             <asp:Label ID="lbScadenzaAnno" runat="server" Text="Scadenza Anno:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:TextBox ID="txtScadenzaAnno" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
          </td>          
        </tr>
        <tr>
          <td class="style4">  
             <asp:Label ID="Label2" runat="server" Text="Riduzione:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:DropDownList ID="DropDownRiduzione" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSourceRiduzione" DataTextField="descrizione" 
                    DataValueField="codice" style="margin-left: 0px">
              </asp:DropDownList>
          </td>
          <td class="style4">  
             <asp:Label ID="Label4" runat="server" Text="Categoria:" CssClass="testo_bold"></asp:Label>         
          </td>
          <td class="style9">           
              <asp:RadioButtonList ID="radioCategoria" runat="server" 
                  RepeatDirection="Horizontal" CssClass="testo_bold">
                  <asp:ListItem Value="1" Selected="True">Autoveicolo</asp:ListItem>
                  <asp:ListItem Value="2">Motoveicolo</asp:ListItem>
                  <asp:ListItem Value="3">Rimorchio</asp:ListItem>
              </asp:RadioButtonList>
          </td>  
          
                    
        </tr>
        <tr>
          <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style4"> 
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
          <td colspan="8" align="center">
            <asp:Button ID="btnStampa" runat="server" Text="Stampa" /> 
            <asp:Button ID="btnChiudiStampa" runat="server" Text="Chiudi" />                     
          </td>
        </tr>   
    </table>
    <br />
</div> 

<asp:SqlDataSource ID="SqlDataSourceRiduzione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"                 
        SelectCommand="SELECT [codice], [descrizione] FROM [veicoli_riduzione_bollo] ORDER BY [codice]">
   </asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSourceIntestaziniBollo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM veicoli_intestazione_bollo order by IntestatoA">
    </asp:SqlDataSource>  

</asp:Content>

 
