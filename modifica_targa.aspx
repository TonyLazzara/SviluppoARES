<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="modifica_targa.aspx.vb" Inherits="modifica_targa" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

    <style type="text/css">
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
        .style1
        {
            width: 104px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Modifica KM Auto" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
  </table>
  <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">
    <tr>
      <td class="style1">
        <asp:Label ID="LblTarga" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:Label ID="Label14" runat="server" Text="Km Attuali" CssClass="testo_bold"></asp:Label>
      </td>
    </tr>
    <tr>
      <td class="style1">
       <asp:TextBox ID="txtTarga" runat="server" Width="84px"></asp:TextBox>
      </td>
      <td>
       <asp:TextBox ID="txtKmAttuali" runat="server" Width="84px" Enabled="false"></asp:TextBox>
       <asp:TextBox ID="txtKmAttualiNascosti" runat="server" Width="84px" Visible="false"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
         <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" />
         <asp:Button ID="btnModificaMovimenti" runat="server" 
              Text="Modifica Movimenti Auto" Width="196px" />
      </td>
    </tr>
  </table>
  <br />
  <table border="0" cellpadding="0" cellspacing="0" width="1024px">
   <tr>
     <td>
       <asp:ListView ID="listMovimentiTarga" runat="server" DataKeyNames="id" DataSourceID="sqlMovimentiTarga" Visible="false">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_riferimento" runat="server" Text='<%# Eval("num_riferimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tipo_movimento" runat="server" Text='<%# Eval("tipo_movimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                             <asp:Label ID="gps" runat="server" Text='<%# Eval("gps") %>' CssClass="testo_piccolo" />
                             <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("modello") %>' controltovalidate="targa" header="Dettagli Auto" CssHeader="toolheader"  CssBody="toolbody"   />
                             <boxover:BoxOver ID="BoxOver2" runat="server" body='<%# Eval("modello") %>' controltovalidate="gps" header="GPS" CssHeader="toolheader"  CssBody="toolbody"   />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_uscita" runat="server" Text='<%# Eval("stazione_uscita") %>' CssClass="testo_piccolo" Font-Bold="true"  />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_uscita" runat="server" Text='<%# Left(Eval("ora_uscita") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:TextBox ID="txtKmUscita" runat="server" Width="70px" Text='<%# Eval("km_uscita") %>'></asp:TextBox>
                             <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_rientro" runat="server" Text='<%# Eval("stazione_rientro") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_rientro" runat="server" Text='<%# Left(Eval("ora_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:TextBox ID="txtKmRientro" runat="server" Width="70px" Text='<%# Eval("km_rientro") %>'></asp:TextBox>
                             <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_rientro" runat="server" Text='<%# Eval("serbatoio_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_riferimento" runat="server" Text='<%# Eval("num_riferimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tipo_movimento" runat="server" Text='<%# Eval("tipo_movimento") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                             <asp:Label ID="gps" runat="server" Text='<%# Eval("gps") %>' CssClass="testo_piccolo" />
                             <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("modello") %>' controltovalidate="targa" header="Dettagli Auto" CssHeader="toolheader"  CssBody="toolbody"   />
                             <boxover:BoxOver ID="BoxOver2" runat="server" body='<%# Eval("modello") %>' controltovalidate="gps" header="GPS" CssHeader="toolheader"  CssBody="toolbody"   />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_uscita" runat="server" Text='<%# Eval("stazione_uscita") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_uscita" runat="server" Text='<%# Left(Eval("ora_uscita") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:TextBox ID="txtKmUscita" runat="server" Width="70px" Text='<%# Eval("km_uscita") %>'></asp:TextBox>
                             <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="stazione_rientro" runat="server" Text='<%# Eval("stazione_rientro") %>' CssClass="testo_piccolo" Font-Bold="true" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="ora_rientro" runat="server" Text='<%# Left(Eval("ora_rientro") & "",5) %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:TextBox ID="txtKmRientro" runat="server" Width="70px" Text='<%# Eval("km_rientro") %>'></asp:TextBox>
                             <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="serbatoio_rientro" runat="server" Text='<%# Eval("serbatoio_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table1" runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th1" runat="server" align="left">
                                             <asp:Label ID="Label22" runat="server" Text="Rif." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th2" runat="server" align="left">
                                             <asp:Label ID="Label4" runat="server" Text="Tipo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th7" runat="server" align="left">
                                             <asp:Label ID="Label8" runat="server" Text="Veicolo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th6" runat="server" align="left">
                                              <asp:Label ID="Label6" runat="server" Text="Staz.Out" CssClass="testo_titolo_piccolo" Font-Bold="true"></asp:Label>
                                          </th>
                                          <th id="Th3" runat="server" align="left">
                                              <asp:Label ID="Label5" runat="server" Text="Data Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th4" runat="server" align="left">
                                              <asp:Label ID="Label7" runat="server" Text="Ora" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                              <asp:Label ID="Label9" runat="server" Text="Km Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th>
                                              <asp:Label ID="Label10" runat="server" Text="Lt Out" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th8" runat="server" align="left">
                                              <asp:Label ID="Label1" runat="server" Text="Staz.In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th9" runat="server" align="left">
                                              <asp:Label ID="Label3" runat="server" Text="Data In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th10" runat="server" align="left">
                                              <asp:Label ID="Label11" runat="server" Text="Ora" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th11" runat="server" align="left">
                                              <asp:Label ID="Label12" runat="server" Text="Km In" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th>
                                              <asp:Label ID="Label13" runat="server" Text="Lt In" CssClass="testo_titolo_piccolo"></asp:Label>
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

  <asp:SqlDataSource ID="sqlMovimentiTarga" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT movimenti_targa.id, movimenti_targa.num_riferimento, veicoli.targa,modelli.descrizione As modello, CONVERT(Char(10), movimenti_targa.data_uscita, 103) As data_uscita,CONVERT(Char(10), movimenti_targa.data_rientro, 103) As data_rientro,CONVERT(Char(8), movimenti_targa.data_uscita, 108) As ora_uscita, tipologia_movimenti.descrzione As tipo_movimento,CONVERT(Char(8), movimenti_targa.data_rientro, 108) As ora_rientro,(stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro,km_uscita, km_rientro, serbatoio_uscita, serbatoio_rientro FROM movimenti_targa INNER JOIN veicoli ON movimenti_targa.id_veicolo=veicoli.id LEFT JOIN stazioni As stazioni1 ON movimenti_targa.id_stazione_uscita=stazioni.id LEFT JOIN stazioni As stazioni2 ON movimenti_targa.id_stazione_rientro=stazioni.id INNER JOIN tipologia_movimenti ON movimenti_targa.id_tipo_movimento=tipologia_movimenti.id INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE movimenti_targa.id>0 ORDER BY movimenti_targa.id DESC"></asp:SqlDataSource>
    
  <asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
</asp:Content>

