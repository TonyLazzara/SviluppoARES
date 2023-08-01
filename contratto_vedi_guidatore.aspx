<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contratto_vedi_guidatore.aspx.vb" Inherits="contratto_vedi_guidatore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <link rel="StyleSheet" type="text/css" href="css/style.css" />    
    
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 81px;
        }
        .style3
        {
            width: 122px;
        }
        .style4
        {
            width: 98px;
        }
        .style5
        {
            width: 228px;
        }
        .style6
        {
            width: 111px;
        }
        .style7
        {
            width: 124px;
        }
        .style8
        {
            width: 130px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table cellpadding="0" cellspacing="2" width="740" border="0" runat="server" id="tab_anagrafica">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="2" width="100%" border="0">
                <tr>
                   <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="2">
                       <asp:Label ID="Label14" runat="server" Text="Dati attualmente salvati in anagrafica" CssClass="testo_titolo"></asp:Label>
                   </td>
                 </tr>
                <tr>
                  <td class="style1">
                    <asp:Label ID="lblNazionalita" runat="server" Text="Nazionalità:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:RadioButtonList ID="radioNazionalita" runat="server" 
                        RepeatDirection="Horizontal" CssClass="testo_bold" AutoPostBack="True">
                        <asp:ListItem Value="it">Italiana</asp:ListItem>
                        <asp:ListItem Value="ee">Estera</asp:ListItem>
                    </asp:RadioButtonList>     
                  </td>
                </tr>
              </table>
              <div id="form_conducente" runat="server" visible="true">
              <table cellpadding="1" cellspacing="1" width="100%" border="1">
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblNominativo" runat="server" Text="Nominativo:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtNominativo" runat="server" Width="230px" ReadOnly="True"></asp:TextBox>
                    </td>
                  <td class="style8">
                      </td>
                  <td class="style4">
                      </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblNome" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtNome" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblCognome" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtCognome" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblCitta" runat="server" Text="Provincia residenza:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtProvincia" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblComune" runat="server" Text="Comune residenza:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="txtComune" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblindirizzo" runat="server" Text="Indirizzo/CAP:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtIndirizzo" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                      <asp:TextBox ID="txtCap" runat="server" MaxLength="6" Width="50px" 
                          ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblNazione" runat="server" Text="Nazione Residenza:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="txtNazione" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblLuogoNascita" runat="server" Text="Comune di nascita:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtLuogoNascita" runat="server" ReadOnly="True" Width="222px"></asp:TextBox>
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblDataNascita" runat="server" Text="Data di nascita:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox runat="server" Width="74px" ID="txtDataNascita" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblLuogoNascita0" runat="server" Text="Nazione di nascita:" 
                          CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                      <asp:TextBox ID="txtNazioneNascita" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                  <td class="style8">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblSesso" runat="server" Text="Sesso:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:RadioButton ID="radioSessoM" GroupName="radioSesso" runat="server" /><asp:Label ID="lblSessoM" runat="server" Text="M" CssClass="testo_bold"></asp:Label>
                    <asp:RadioButton ID="radioSessoF" GroupName="radioSesso" runat="server" /><asp:Label ID="lblSessoF" runat="server" Text="F" CssClass="testo_bold"></asp:Label>
                    &nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblCodiceFiscale" runat="server" Text="Codice fiscale:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtCodiceFiscale" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio locale:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtDomicilio" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtTelefono" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblFax" runat="server" Text="Fax:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtFax" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblCellulare" runat="server" Text="Cellulare:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtCellulare" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblPatente" runat="server" Text="Patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtPatente" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                      <asp:Label ID="lblTipoPatente" runat="server" Text="Tipo patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                      <asp:TextBox ID="txtTipoPatente" runat="server" MaxLength="3" Width="45px" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                    </td>
                  <td class="style8">
                      <asp:Label ID="lblScadenzaPatente" runat="server" Text="Scadenza patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox runat="server" Width="74px" ID="txtScadenzaPatente" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                      <asp:Label ID="lblDataRilascioPatente" runat="server" Text="Rilasciata il:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                       <asp:TextBox runat="server" Width="74px" ID="txtDataRilascioPatente" ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblLuogoEmissionePatente" runat="server" Text="Luogo di emissione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtLuogoEmissionePatente" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style7">
                    <asp:Label ID="lblAltriDocumenti" runat="server" Text="Atri documenti:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtAltriDocumenti" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                  </td>
                  <td class="style8">
                    <asp:Label ID="lblConvenzione" runat="server" Text="Convenzione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:DropDownList ID="listConvenzioni" runat="server">
                        <asp:ListItem>Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                  &nbsp;</td>
                </tr>
              </table>
              </div>
            </td>
          </tr>
     </table>
     <table cellpadding="0" cellspacing="2" width="740" border="0" runat="server" id="tab_anagrafica_contratto">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="2" width="100%" border="0">
                <tr>
                   <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="2">
                       <asp:Label ID="Label14_cnt" runat="server" Text="Dati dell'utente salvati al momento del contratto" CssClass="testo_titolo"></asp:Label>
                   </td>
                 </tr>
                <tr>
                  <td class="style1">
                    <asp:Label ID="lblNazionalita_cnt" runat="server" Text="Nazionalità:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:RadioButtonList ID="radioNazionalita_cnt" runat="server" 
                        RepeatDirection="Horizontal" CssClass="testo_bold" AutoPostBack="True">
                        <asp:ListItem Value="it">Italiana</asp:ListItem>
                        <asp:ListItem Value="ee">Estera</asp:ListItem>
                    </asp:RadioButtonList>     
                  </td>
                </tr>
              </table>
              <div id="form_conducente_contratto" runat="server" visible="true">
              <table cellpadding="1" cellspacing="1" width="100%" border="1">
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblNome_cnt" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtNome_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblCognome_cnt" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtCognome_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblCitta_cnt" runat="server" Text="Provincia residenza:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtProvincia_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblComune_cnt" runat="server" Text="Comune residenza:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="txtComune_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblindirizzo_cnt" runat="server" Text="Indirizzo/CAP:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtIndirizzo_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                      <asp:TextBox ID="txtCap_cnt" runat="server" MaxLength="6" Width="50px" 
                          ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblNazione_cnt" runat="server" Text="Nazione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="txtNazione_cnt" runat="server" ReadOnly="True" Width="184px"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblLuogoNascita_cnt" runat="server" Text="Comune di nascita:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5" width="100%">
                    <asp:TextBox ID="txtLuogoNascita_cnt" runat="server" ReadOnly="True" 
                          Width="203px"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblDataNascita_cnt" runat="server" Text="Data di nascita:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox runat="server" Width="74px" ID="txtDataNascita_cnt" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblDomicilio_cnt" runat="server" Text="Domicilio locale:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtDomicilio_cnt" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblTelefono_cnt" runat="server" Text="Telefono:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtTelefono_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                   
                    <asp:Label ID="lblCodiceFiscale_cnt" runat="server" Text="Codice fiscale:" CssClass="testo_bold"></asp:Label>
                   
                  </td>
                  <td class="style5">

                    <asp:TextBox ID="txtCodiceFiscale_cnt" runat="server" ReadOnly="True"></asp:TextBox>

                  </td>
                  <td class="style3">
                    <asp:Label ID="lblCellulare_cnt" runat="server" Text="Cellulare:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtCellulare_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblEmail_cnt" runat="server" Text="E-mail:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtEmail_cnt" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblPatente_cnt" runat="server" Text="Patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtPatente_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                      <asp:Label ID="lblTipoPatente_cnt" runat="server" Text="Tipo patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                      <asp:TextBox ID="txtTipoPatente_cnt" runat="server" MaxLength="3" Width="45px" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                    </td>
                  <td class="style3">
                      <asp:Label ID="lblScadenzaPatente_cnt" runat="server" Text="Scadenza patente:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox runat="server" Width="74px" ID="txtScadenzaPatente_cnt" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                      <asp:Label ID="lblDataRilascioPatente_cnt" runat="server" Text="Rilasciata il:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                       <asp:TextBox runat="server" Width="74px" ID="txtDataRilascioPatente_cnt" ReadOnly="True"></asp:TextBox>
                  </td>
                  <td class="style3">
                    <asp:Label ID="lblLuogoEmissionePatente_cnt" runat="server" Text="Luogo di emissione:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtLuogoEmissionePatente_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="style6">
                    <asp:Label ID="lblAltriDocumenti_cnt" runat="server" Text="Atri documenti:" CssClass="testo_bold"></asp:Label>
                  </td>
                  <td class="style5">
                    <asp:TextBox ID="txtAltriDocumenti_cnt" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                  </td>
                  <td class="style3">
                    
                  </td>
                  <td>

                  </td>
                </tr>
              </table>
              </div>
            </td>
          </tr>
     </table>
     
        <asp:Label ID="idUtenteAnagrafica" runat="server" visible="false"></asp:Label>
        <asp:Label ID="numContratto" runat="server" visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
