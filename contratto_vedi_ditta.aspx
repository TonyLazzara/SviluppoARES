<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contratto_vedi_ditta.aspx.vb" Inherits="contratto_vedi_ditta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <link rel="StyleSheet" type="text/css" href="css/style.css" />    
  
    <style type="text/css">
        .style1
        {
            width: 340px;
        }
        .style2
        {
            width: 175px;
        }
        .style3
        {
            width: 154px;
        }
        .style4
        {
            width: 198px;
        }
        #tab_anagrafica_contratto
        {
            width: 898px;
        }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table cellpadding="0" cellspacing="2" border="0" runat="server" id="table3">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="2" width="100%" border="1">
                <tr>
                   <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="4">
                       <asp:Label ID="Label14" runat="server" Text="Dati attualmente salvati in anagrafica" CssClass="testo_titolo"></asp:Label>
                   </td>
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblRagioneSociale" runat="server" Text="Regione sociale:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtRagioneSociale" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="Codice Cliente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtCodiceEDP" runat="server" ReadOnly="true"></asp:TextBox>
                  </td>  
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblRagioneSociale0" runat="server" Text="Tipo Cliente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtTipoCliente" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                      &nbsp;</td>
                  <td class="style4">
                      &nbsp;</td>  
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblPartitaIva" runat="server" Text="Partita IVA:" CssClass="testo_bold"></asp:Label>
                    </td>
                  <td class="style1">
                    <asp:TextBox ID="txtPartitaIva" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>          
                   <td class="style3">
                    <asp:Label ID="lblNazione" runat="server" Text="Nazione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtNazione" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>
              
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblCitta" runat="server" Text="Provincia:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtProvincia" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="Label1" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                      <asp:TextBox ID="txtComune" runat="server" ReadOnly="true" Width="176px"></asp:TextBox>
                  </td>
                </tr>        
                <tr>
                  <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Indirizzo:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtIndirizzo" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                    &nbsp;
                  </td>
                  <td class="style3">
                    <asp:Label ID="Label4" runat="server" Text="CAP:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtCAP" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblPartitaIvaEstera" runat="server" Text="Partita IVA estera:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtPartitaIvaEstera" runat="server" Width="128px" 
                          ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblCodiceFiscale" runat="server" Text="Codice fiscale:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtCodiceFiscale" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblFax" runat="server" Text="Fax:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtFax" runat="server" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtTelefono" runat="server" ReadOnly="true"></asp:TextBox>
                  </td>
                </tr>
                <%--<tr>
                  <td class="style5">
                    <asp:Label ID="lblSconto" runat="server" Text="Sconto:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style2">
                    <asp:DropDownList ID="listSconto" runat="server">
                      <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td class="style4">
                    <asp:Label ID="lblCategoria" runat="server" Text="Categoria:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:DropDownList ID="listCategoria" runat="server">
                      <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>--%>
                <%--<tr>
                  <td class="style5">
                    <asp:Label ID="lblFullCredit" runat="server" Text="Full credit:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style2">
                    <asp:CheckBox ID="checkFullCredit" runat="server" />
                  </td>
                  <td class="style4">
                    <asp:Label ID="lblStatoCliente" runat="server" Text="Stato cliente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:DropDownList ID="listStatoCliente" runat="server">
                      <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>--%>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblEmailPec" runat="server" Text="E-mail PEC:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtEmailPec" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="Label19" runat="server" Text="Codice SDI:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtCodiceSDI" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style2">
                    <%--<asp:Label ID="lblProduttore" runat="server" Text="Produttore:" CssClass="testo_bold"></asp:Label>--%><asp:Label ID="lblTipoSpedizioneFattura" runat="server" Text="Spedizione fattura:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <%--<asp:DropDownList ID="listProduttore" runat="server">
                      <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>--%>
                      <asp:RadioButtonList ID="radioSpedizioneFattura" runat="server" 
                          RepeatDirection="Horizontal" CssClass="testo_bold">
                          <asp:ListItem Value="N">Non Spedire</asp:ListItem>
                          <asp:ListItem Value="D">Da definire</asp:ListItem>
                          <asp:ListItem Value="M">E-Mail</asp:ListItem>
                          <asp:ListItem Value="P">Posta</asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblTourOperator" runat="server" Text="Tour operator:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:CheckBox ID="checkTourOperator" runat="server" />
                  </td>
                </tr>
                <%--<tr>
                  <td class="style5">
                    <asp:Label ID="lblArticoloEsenzione" runat="server" Text="Articolo esenzione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style2">
                    <asp:DropDownList ID="listArticoloEsenzione" runat="server">
                      <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td class="style4">
                    <asp:Label ID="lblSenzaIva" runat="server" Text="Senza IVA:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:CheckBox ID="checkSenzaIva" runat="server" />
                  </td>
                </tr>--%>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblModalitaPagamento" runat="server" Text="Modalità di pagamento:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtModalitaDiPagamento" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblPagamento" runat="server" Text="Pagamento:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:TextBox ID="txtPagamento" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                  </td>
                </tr>
                <%--<tr>
                  <td class="style5">
                    <asp:Label ID="lblConvenzione" runat="server" Text="Convenzione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style2">
                    <asp:DropDownList ID="listConvenzioni" runat="server">
                        <asp:ListItem Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td class="style4">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
                </tr>--%>
                <tr>
                  <td class="style2">
                    <asp:Label ID="lblInvioEmail" runat="server" Text="E-mail:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style1">
                    <asp:TextBox ID="txtEmail" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblInvioMail" runat="server" Text="Invio E-mail:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style4">
                    <asp:CheckBox ID="checkInvioEmail" runat="server" />
                  </td>
                </tr>
                </table>
            </td>
          </tr>
        </table>
        <table cellpadding="0" cellspacing="2" border="0" runat="server" id="tab_anagrafica_contratto">
          <tr>
            <td>
                <table cellpadding="2" cellspacing="2" width="100%" border="1">
                   <tr>
                      <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="4" >
                          <asp:Label ID="Label14_cnt" runat="server" Text="Dati dell'utente salvati al momento del contratto" CssClass="testo_titolo"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td class="style2">
                          <asp:Label ID="Label5" runat="server" Text="Regione sociale:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                          <asp:TextBox ID="txtRagioneSociale_cnt" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                          <asp:Label ID="Label6" runat="server" Text="Codice Cliente:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                          <asp:TextBox ID="txtCodiceEdp_cnt" runat="server" ReadOnly="true"></asp:TextBox>
                      </td>  
                    </tr>
                     <tr>
                      <td class="style2">
                        <asp:Label ID="Label7" runat="server" Text="Tipo Cliente:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtTipoCliente_cnt" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                          &nbsp;</td>
                      <td class="style4">
                          &nbsp;</td>  
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="Label8" runat="server" Text="Partita IVA:" CssClass="testo_bold"></asp:Label>
                        </td>
                      <td class="style1">
                        <asp:TextBox ID="txtPartitaIva_cnt" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                      </td>          
                       <td class="style3">
                        <asp:Label ID="Label9" runat="server" Text="Nazione:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                        <asp:TextBox ID="txtNazione_cnt" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="Label10" runat="server" Text="Provincia:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtProvincia_cnt" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                      </td>
                      <td class="style3">
                        <asp:Label ID="Label11" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                          <asp:TextBox ID="txtComune_cnt" runat="server" ReadOnly="true" Width="176px"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="Label12" runat="server" Text="Indirizzo:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtIndirizzo_cnt" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                        &nbsp;
                      </td>
                      <td class="style3">
                        <asp:Label ID="Label13" runat="server" Text="CAP:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                        <asp:TextBox ID="txtCap_cnt" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="Label15" runat="server" Text="Partita IVA estera:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtPartitaIvaEstera_cnt" runat="server" Width="128px" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                        <asp:Label ID="Label16" runat="server" Text="Codice fiscale:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                        <asp:TextBox ID="txtCodiceFiscale_cnt" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="Label17" runat="server" Text="Fax:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtFax_cnt" runat="server" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                        <asp:Label ID="Label18" runat="server" Text="Telefono:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                        <asp:TextBox ID="txtTelefono_cnt" runat="server" ReadOnly="true"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                        <asp:Label ID="lblEmailCC0" runat="server" Text="E-mail PEC:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                        <asp:TextBox ID="txtEmailPec_cnt" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                        <asp:Label ID="Label20" runat="server" Text="Codice SDI:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style4">
                        <asp:TextBox ID="txtCodiceSdi_cnt" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="style2">
                          <asp:Label ID="lblTipoSpedizioneFattura0" runat="server" 
                              Text="Spedizione fattura:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                      <asp:RadioButtonList ID="radioSpedizioneFattura_cnt" runat="server" 
                          RepeatDirection="Horizontal" CssClass="testo_bold">
                          <asp:ListItem Value="N">Non Spedire</asp:ListItem>
                          <asp:ListItem Value="D">Da definire</asp:ListItem>
                          <asp:ListItem Value="M">E-Mail</asp:ListItem>
                          <asp:ListItem Value="P">Posta</asp:ListItem>
                      </asp:RadioButtonList>
                      </td>
                      <td class="style3">
                          &nbsp;</td>
                      <td class="style4">
                          &nbsp;</td>
                    </tr>
                    <tr>
                      <td class="style2">
                    <asp:Label ID="lblInvioEmail0" runat="server" Text="E-mail:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td class="style1">
                    <asp:TextBox ID="txtEmail_cnt" runat="server" Width="320px" ReadOnly="true"></asp:TextBox>
                      </td>
                      <td class="style3">
                    <asp:Label ID="lblInvioMail0" runat="server" Text="Invio E-mail:" 
                              CssClass="testo_bold"></asp:Label>
                        </td>
                      <td class="style4">
                    <asp:CheckBox ID="checkInvioEmail_cnt" runat="server" />
                        </td>
                    </tr>
                    </table>
            </td>
           </tr>
        </table>
        <asp:Label ID="idDitta" runat="server" visible="false"></asp:Label>
        <asp:Label ID="num_contratto" runat="server" visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
