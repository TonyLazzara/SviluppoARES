<%@ Control Language="VB" AutoEventWireup="false" CodeFile="edit_odl.ascx.vb" Inherits="gestione_danni_edit_odl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="gestione_note.ascx" TagName="gestione_note" TagPrefix="uc1" %>
<%@ Register Src="edit_danno.ascx" TagName="edit_danno" TagPrefix="uc1" %> 
<%@ Register Src="gestione_checkin.ascx" TagName="gestione_checkin" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_fornitori/anagrafica_fornitori.ascx" TagName="anagrafica_fornitori" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_fornitori/anagrafica_drivers.ascx" TagName="anagrafica_drivers" TagPrefix="uc1" %>



<script type="text/javascript" language="javascript">

    function ConfermaEsci() {
        if(<%= lb_nuovo_danno.text %>) {
            return window.confirm('Sono stati inseriti nuovi danni,\r\nSe esci non verranno salvati.\r\nConfermi l\'uscita senza salvare?');
        }
        return true;
    }

    function ckdata() {
        var dadt = document.getElementById('<%=tx_data_previsto_rientro.ClientID%>').value;
        alert("-" + dadt + "-");

        if (dadt != "") {
            alert("Specificare la data di presunto rientro")
            return false;
        } 

    }

    function GetCalendar(a, b) {
               
        if (b == 'e') { //24.04.2021 abilita calendario su modifica /
             Calendar.show(a, '%d/%m/%Y', false); 
        }
    }



</script>

<link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
<script type="text/javascript" language="javascript" src="/lytebox.js"></script>

<div id="div_targa" runat="server" visible="false">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td  align="left" style="color: #FFFFFF;background-color:#444;"> 
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Stazione: </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>    
            &nbsp;&nbsp;<b>Proprietario: </b><asp:Label ID="lb_proprietario" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Data Buy Back: </b><asp:Label ID="lb_data_buy_back" runat="server" Text="" ></asp:Label>   
        </td>
    </tr>
</table>
</div>

<div id="div_gestione_checkin" runat="server" visible="false">
    <uc1:gestione_checkin id="gestione_checkin" runat="server" />
</div>


<div id="div_dettaglio_danni" runat="server" visible="false">

<ajaxtoolkit:TabContainer ID="tabPanelODL" runat="server" ActiveTabIndex="0" 
        Width="100%" Visible="true">

 <ajaxtoolkit:TabPanel ID="tabElencoDanni" runat="server" HeaderText="Elenco Danni Aperti">
  <HeaderTemplate>Elenco Danni Aperti</HeaderTemplate>  
    <ContentTemplate>
       <table border="0" cellpadding="0" cellspacing="0" width="100%" >
      <tr>
        <td>
            <asp:ListView ID="listViewElencoDanniAperti" runat="server" DataSourceID="sqlElencoDanniCollegati" DataKeyNames="id_danno">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                          <asp:Label ID="lb_id_evento" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_danno_attivo" runat="server" Text='<%# Eval("danno_attivo") %>' Visible="false" />
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                      <td id="Td3" runat="server" visible='<%# lb_th_da_addebitare.Text %>'>
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
                      </td>
                      <td id="Td5" runat="server" visible='<%# lb_th_chiusura_danno.Text %>'  align="center">
                          <asp:CheckBox ID="ck_chiusura_danno" runat="server" Checked='<%# Eval("chiusura_danno") %>' />
                      </td>
                      <td id="Td6" runat="server" visible='<%# lb_th_riparato.Text %>' align="center">
                          <asp:CheckBox ID="ck_riparato" runat="server"  />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                          <asp:Label ID="lb_id_evento" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_danno_attivo" runat="server" Text='<%# Eval("danno_attivo") %>' Visible="false" />
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                      <td id="Td3" runat="server" visible='<%# lb_th_da_addebitare.Text %>'>
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
                      </td>
                      <td id="Td5" runat="server" visible='<%# lb_th_chiusura_danno.Text %>' align="center">
                          <asp:CheckBox ID="ck_chiusura_danno" runat="server" Checked='<%# Eval("chiusura_danno") %>' />
                      </td>
                      <td id="Td6" runat="server" visible='<%# lb_th_riparato.Text %>' align="center">
                          <asp:CheckBox ID="ck_riparato" runat="server"  />
                      </td>
                      <td id="Td4" align="center" width="40px" runat="server" visible='<%# lb_th_lente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Nessun danno aperto su questo veicolo.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th5" runat="server">
                                          RDS</th>
                                      <th id="Th4" runat="server">
                                          Tipo</th>
                                      <th id="Th1" runat="server">
                                          Posizione</th>
                                      <th id="Th2" runat="server">
                                          Danno</th>
                                      <th id="Th3" runat="server">
                                          Entità</th>
                                      <th id="Th6" runat="server">
                                          Riparato</th>
                                      <th id="th_da_addebitare" runat="server">
                                          Da Addebitare</th>
                                      <th id="th_chiusura_danno" runat="server">
                                          Seleziona</th>
                                      <th id="th_riparato" runat="server">
                                          Riparato</th>
                                      <th id="th_lente" runat="server">
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
      <tr>
        <td align="center">
            <asp:Button ID="bt_nuovo_danno" runat="server" Text="Nuovo Danno" />
        </td>
      </tr>
</table>     
    </ContentTemplate>
</ajaxtoolkit:TabPanel>
  
 
 <ajaxtoolkit:TabPanel ID="TabElencoRDS" runat="server" HeaderText="Elenco RDS">
  <HeaderTemplate>Elenco RDS</HeaderTemplate>  
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
      <tr>
        <td>
            <asp:ListView ID="listViewElencoRDS" runat="server" DataSourceID="sqlElencoRDS" DataKeyNames="id_rds">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_sospeso_rds" runat="server" Text='<%# Eval("sospeso_rds") %>' Visible="false" />
                          <asp:Label ID="lb_attivo" runat="server" Text='<%# Eval("attivo") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_rds"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_documento_apertura" runat="server" Text='<%# Eval("id_tipo_documento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_documento_apertura" runat="server" Text='<%# Eval("des_id_tipo_documento_apertura") %>' />
                          <asp:Label ID="lb_id_documento_apertura" runat="server" Text='<%# Eval("id_documento_apertura") %>' />
                          <asp:Label ID="lb_num_crv_apertura" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_stato_rds" runat="server" Text='<%# Eval("stato_rds") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_rds" runat="server" Text='<%# Eval("des_stato_rds") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("importo"), "0.00") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="Label1" runat="server" Text='<%# libreria.myFormatta(Eval("incasso"), "0.00") %>' />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>  
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_sospeso_rds" runat="server" Text='<%# Eval("sospeso_rds") %>' Visible="false" />
                          <asp:Label ID="lb_attivo" runat="server" Text='<%# Eval("attivo") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_rds"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_documento_apertura" runat="server" Text='<%# Eval("id_tipo_documento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_documento_apertura" runat="server" Text='<%# Eval("des_id_tipo_documento_apertura") %>' />
                          <asp:Label ID="lb_id_documento_apertura" runat="server" Text='<%# Eval("id_documento_apertura") %>' />
                          <asp:Label ID="lb_num_crv_apertura" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_stato_rds" runat="server" Text='<%# Eval("stato_rds") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_rds" runat="server" Text='<%# Eval("des_stato_rds") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("importo"), "0.00") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="Label1" runat="server" Text='<%# libreria.myFormatta(Eval("incasso"), "0.00") %>' />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr> 
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun RDS presente sul veicolo.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>RDS</th>
                                      <th>Data</th>
                                      <th>Documento</th>
                                      <th>Stato</th>
                                      <th>Stimato</th>
                                      <th>Incasso</th>
                                      <th id="th_lente"></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                         <tr>
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" PageSize="30" runat="server"  >
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
        </td>
      </tr>
</table>   
        
    </ContentTemplate>
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="TabElencoDocumenti" runat="server" HeaderText="Elenco Documenti">
  <HeaderTemplate>Elenco Documenti</HeaderTemplate>  
    <ContentTemplate>
        
        
   <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table7">
    <tr id="Tr40" runat="server">
      <td id="Td3" runat="server">
         <asp:ListView ID="listViewDocumenti" runat="server" 
              DataSourceID="sqlDocumentiRDS" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td> 
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/DocDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun Documento Allegato.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>RDS</th>
                                      <th id="Th5" runat="server">
                                          Documento</th>
                                      <th id="Th2" runat="server">
                                          Nome</th>
                                      <th id="th_lente" runat="server">
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

    </ContentTemplate>
</ajaxtoolkit:TabPanel>

</ajaxtoolkit:TabContainer>    
</div>

<div id="div_edit_danno" runat="server" visible="false">
<br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td  align="left" style="color: #FFFFFF;background-color:#444;">
                &nbsp;<b><asp:Label ID="Label39" runat="server" Text="Dettaglio Danno"></asp:Label></b> 
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
      <tr>
        <td>
<uc1:edit_danno id="edit_danno" runat="server" />
        </td>
      </tr>
    </table>
</div>

<div id="div_fornitori" runat="server" visible="false">
    <uc1:anagrafica_fornitori id="anagrafica_fornitori" runat="server" />
</div>

<div id="div_drivers" runat="server" visible="false">
    <uc1:anagrafica_drivers id="anagrafica_drivers" runat="server" />
</div>

<div id="div_dettaglio" runat="server" visible="false">
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;<asp:Label ID="lb_enum_odl_stato" runat="server" Text='' ></asp:Label>&nbsp;:&nbsp;</b>
             <asp:Label ID="lb_odl" runat="server" Text='0' ></asp:Label>
             &nbsp;&nbsp;<asp:Label ID="lb_data_odl" runat="server" Text='' ></asp:Label>
             
           </td>
           <td align="right" bgcolor="#444">
                <asp:Button ID="bt_check_out" runat="server" Text="Check Out" />
                <asp:Button ID="bt_check_in" runat="server" Text="Check In" />
                <asp:Button ID="bt_stampa" runat="server" Text="Stampa ODL" />
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr id="tr_noleggio_in_corso" runat="server" visible="false">
            <td colspan="6" align="center">
                <asp:Label ID="Label20" runat="server" style="color:Red; font-size:large;" Text="Noleggio in Corso" CssClass="testo_bold"  ></asp:Label><br />
                <asp:Label ID="lb_dicitura_documento" runat="server" Text='RA' CssClass="testo_bold" ></asp:Label>
                <asp:LinkButton ID="lb_num_documento" runat="server" CssClass="testo_bold" Text="" />
                &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text='CRV' CssClass="testo_bold" ></asp:Label>
                <asp:Label ID="lb_num_crv" runat="server" Text='0'/>
                &nbsp;&nbsp;<asp:Label ID="Label22" runat="server" Text='RDS' CssClass="testo_bold" ></asp:Label>
                <asp:Label ID="lb_num_rds" runat="server" Text='0'/><br /><br />
            </td>
        </tr>
        <tr id="tr_attenzione" runat="server" visible="false">
            <td colspan="6" align="center">
                <asp:Label ID="lb_attenzione" runat="server" style="color:Red; font-size:large;" Text="ATTENZIONE: è necessario far rientrare il veicolo per chiudere l'ODL" CssClass="testo_bold"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="Label2" runat="server" Text="Stato ODL:" CssClass="testo_bold"></asp:Label>
                <asp:DropDownList ID="DropDown_stato_odl" runat="server" >
                </asp:DropDownList>
                &nbsp;&nbsp;                
                <asp:Label ID="Label6" runat="server" Text="Lavoro eseguito:" CssClass="testo_bold"></asp:Label>
                <asp:CheckBox ID="ck_alvoro_eseguito" runat="server" />
                <asp:Label ID="Label11" runat="server" Text="RDS attivo:" CssClass="testo_bold" 
                    Visible="False"></asp:Label>
                <asp:CheckBox ID="ck_rds_attivo" runat="server" Visible="False" />
                &nbsp;&nbsp;
                
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Tipo Riparazione:" CssClass="testo_bold"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDown_tipo_riparazione" runat="server" AppendDataBoundItems="True" 
                          DataSourceID="sql_tipo_riparazione" DataTextField="descrizione" DataValueField="id">
                            <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Fornitore:" CssClass="testo_bold"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDown_fornitore" runat="server" AppendDataBoundItems="True" 
                          DataSourceID="sql_fornitore" DataTextField="ragione_sociale" DataValueField="id">
                            <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:ImageButton ID="Add_fornitore" runat="server" ImageUrl="/images/aggiorna.png" style="width: 16px"/>
                    </td>        
                </tr>
                <tr id="tr_autorizzato_da" runat="server">
                    <td>
                        <asp:Label ID="Label24" runat="server" Text="Operatore:" CssClass="testo_bold" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDown_autorizzato_da" runat="server" AppendDataBoundItems="True" 
                          DataSourceID="sql_operatore" DataTextField="descrizione" DataValueField="id" Enabled="false">
                            <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </td>
            <td colspan="4" valign="top">
            <table>       
                 <tr>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="Lavori da eseguire:" CssClass="testo_bold"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDown_lavori_da_eseguire" runat="server" AppendDataBoundItems="True" 
                          DataSourceID="sql_odl_tipo_lavoro" DataTextField="descrizione" DataValueField="id">
                            <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>         
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label26" runat="server" Text="descrizione:" CssClass="testo_bold"></asp:Label>
                    </td>
                    <td rowspan="2" valign="top">
                        <asp:TextBox ID="tx_descrizione_lavori" runat="server"  Width="340px" Height="65px" TextMode="MultiLine"></asp:TextBox>
                    </td>       
                </tr>               
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr id="tr1" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
            <td colspan="6" align="left">
                <asp:Label ID="lb_conducente" runat="server" Text="Conducente:" CssClass="testo_bold"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="DropDownDrivers" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sql_drivers" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:ImageButton ID="Add_drivers" runat="server" ImageUrl="/images/aggiorna.png" style="width: 16px"/>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table id="tb_uscita" runat="server">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label9" runat="server" Text="Uscita:" CssClass="testo_bold"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Data:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                           <%-- <a onclick="Calendar.show(document.getElementById('<%= tx_data_uscita.ClientID%>'), '%d/%m/%Y', false)">--%>
                                <asp:TextBox ID="tx_data_uscita" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                            <%--</a>--%>
                           <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_uscita" ID="CalendarExtender1">
                            </ajaxtoolkit:CalendarExtender>--%>
                            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_data_uscita" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender1">
                            </ajaxtoolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Ora:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_ora_uscita" runat="server" Width="40px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                                              TargetControlID="tx_ora_uscita"
                                              Mask="99:99"
                                              MessageValidatorTip="true"
                                              ClearMaskOnLostFocus="true"
                                              OnFocusCssClass="MaskedEditFocus"
                                              OnInvalidCssClass="MaskedEditError"
                                              MaskType="Time"
                                              CultureName="en-US">
                            </ajaxToolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr id="tr2" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDown_stazione_uscita" runat="server" AppendDataBoundItems="True" 
                              DataSourceID="sql_stazione" DataTextField="descrizione" DataValueField="id">
                                <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr3" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_km_uscita" runat="server" Width="70px"  onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                            (<asp:Label ID="lb_km_uscita" runat="server" Text='' CssClass="testo_bold"></asp:Label>)
                        </td>
                    </tr>
                    <tr id="tr4" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label28" runat="server" Text="Litri:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_litri_uscita" runat="server" Width="40px"  onKecyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                            /<asp:Label ID="lb_serbatoio" runat="server" Text="" CssClass="testo_bold"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2" valign="top">
                <table id="tb_previsto_rientro" runat="server">
                    <tr>
                        <td colspan="2" style="text-align:left;">
                            <asp:Label ID="Label4" runat="server" Text="Previsto Rientro:" CssClass="testo_bold"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Data:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            
                            <a onclick="Calendar.show(document.getElementById('<%= tx_data_previsto_rientro.ClientID%>'), '%d/%m/%Y', false)">
                                <asp:TextBox ID="tx_data_previsto_rientro" runat="server" Width="70px" ></asp:TextBox>
                                </a>
                         <%--   <ajaxtoolkit:CalendarExtender runat="server" TargetControlID="tx_data_previsto_rientro" Format="dd/MM/yyyy" Enabled="True" ID="CalendarExtender2">
                            </ajaxtoolkit:CalendarExtender>--%>
                            <ajaxtoolkit:MaskedEditExtender runat="server" TargetControlID="tx_data_previsto_rientro" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender2">
                            </ajaxtoolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Ora:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_ora_prevista_rientro" runat="server" Width="40px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" 
                                              TargetControlID="tx_ora_prevista_rientro"
                                              Mask="99:99"
                                              MessageValidatorTip="true"
                                              ClearMaskOnLostFocus="true"
                                              OnFocusCssClass="MaskedEditFocus"
                                              OnInvalidCssClass="MaskedEditError"
                                              MaskType="Time"
                                              CultureName="en-US">
                            </ajaxToolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr id="tr5" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDown_stazione_previsto_rientro" runat="server" AppendDataBoundItems="True" 
                              DataSourceID="sql_stazione" DataTextField="descrizione" DataValueField="id">
                                <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr6" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="tr7" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2" valign="top">
                <table id="tab_rientro" runat="server">
                    <tr>
                        <td colspan="2" style="text-align:left;">
                            <asp:Label ID="Label5" runat="server" Text="Rientro:" CssClass="testo_bold"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Data:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_data_rientro" runat="server" Width="70px"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_rientro" ID="CalendarExtender3">
                            </ajaxtoolkit:CalendarExtender>
                            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_data_rientro" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender3">
                            </ajaxtoolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Ora:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_ora_rientro" runat="server" Width="40px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" 
                                              TargetControlID="tx_ora_rientro"
                                              Mask="99:99"
                                              MessageValidatorTip="true"
                                              ClearMaskOnLostFocus="true"
                                              OnFocusCssClass="MaskedEditFocus"
                                              OnInvalidCssClass="MaskedEditError"
                                              MaskType="Time"
                                              CultureName="en-US">
                            </ajaxToolkit:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr id="tr8" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDown_stazione_rientro" runat="server" AppendDataBoundItems="True" 
                              DataSourceID="sql_stazione" DataTextField="descrizione" DataValueField="id">
                                <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr9" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Km:" CssClass="testo_bold" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_km_rientro" runat="server" Width="70px"  onKeyPress="javascript:return filterInputInt(event)" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="tr10" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
                        <td>
                            <asp:Label ID="Label30" runat="server" Text="Litri:" CssClass="testo_bold" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tx_litri_rientro" runat="server" Width="40px"  onKeyPress="javascript:return filterInputInt(event)" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr11" runat="server" visible='<%# lb_visibile_uscita_rientro.text %>'>
            <td colspan="2" align="center">
                &nbsp;</td>
            <td colspan="2" align="center">
                
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lb_ritirato_da" runat="server" Text="Ritirato Da:" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        <tr id="tr12" runat="server" visible='<%# lb_visibile_uscita_rientro.Text %>'>
            <td colspan="2" style="text-align:left;">
                <asp:Label ID="lb_consegnato_da" runat="server" Text="Consegnato Da:" CssClass="testo_bold"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDown_consegnato_da" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sql_operatore" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="-1">Cliente</asp:ListItem>
                    <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2" align="center">
                &nbsp;
            </td>
            <td colspan="2" align="center">
                <asp:DropDownList ID="DropDown_ritirato_da" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sql_operatore" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                    &nbsp;
                </td>
        </tr>
        <tr id="tr_autorizzo_preventivo" runat="server">
            <td>
                <asp:Label ID="Label29" runat="server" Text="Autorizzato Da:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDown_autorizzato_pagamento" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sql_operatore" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Seleziona</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label32" runat="server" Text="Data Autorizzazione:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <a onclick="Calendar.show(document.getElementById('<%= tx_data_autorizzato_pagamento.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox ID="tx_data_autorizzato_pagamento" runat="server" Width="70px" ></asp:TextBox>
                    </a>
              <%--  <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_autorizzato_pagamento" ID="CalendarExtender4">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_data_autorizzato_pagamento" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender7">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label31" runat="server" Text="Importo:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_importo_odl" runat="server" Width="70px"  onKeyPress="javascript:return filterInputDouble(event)" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="10" align="center">
                <asp:Button ID="bt_modifica_form_odl" runat="server" Text="Modifica ODL Senza Cambiare Stato" ValidationGroup="ModificaODL" visible="false" />
                <asp:Button ID="bt_salva_form_odl" runat="server" Text="Salva" ValidationGroup="SalvaODL" />
                <asp:Button ID="bt_chiudi_form_odl" runat="server" Text="Chiudi" OnClientClick="javascript:return ConfermaEsci();" />
            </td>
        </tr>
    </table>
</div>

<div id="div_nota" runat="server" visible="false">
    <br />
    <div>
        <uc1:gestione_note id="gestione_note" runat="server" />
    </div>
</div>

<div id="div_allegati" runat="server" visible="false">
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Allegati:&nbsp;</b>
           </td>
         </tr>
    </table>
    
</div>

<asp:Label ID="lb_id_veicolo" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_odl" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_num_odl" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_gruppo_danni_uscita" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_gruppo_danni_rientro" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_gruppo_danni_durante_odl" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_gruppo_danni_filtro" runat="server" Text='0' Visible="true" />
<asp:Label ID="lb_id_evento_apertura_danno" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_movimento_targa" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_tipo_doc_apertura" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_noleggio_in_corso" runat="server" Text='false' Visible="false" />
<asp:Label ID="lb_nuovo_danno" runat="server" Text='0' Visible="false" />


<asp:Label ID="lb_th_lente" runat="server" Text='true' Visible="false" />
<asp:Label ID="lb_th_da_addebitare" runat="server" Text='true' Visible="false" />
<asp:Label ID="lb_th_chiusura_danno" runat="server" Text='true' Visible="false" />
<asp:Label ID="lb_th_riparato" runat="server" Text='false' Visible="false" />
<asp:Label ID="lb_AbilitaLente" runat="server" Text='true' Visible="false" />
<asp:Label ID="lb_visibile_uscita_rientro" runat="server" Text='false' Visible="false" />
<asp:Label ID="lb_abilita_ck_chiusura_danno" runat="server" Text='True' Visible="false" />


<%--<asp:SqlDataSource ID="sql_stato_odl" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [odl_stato] WITH(NOLOCK)
            WHERE stato_scelte = @stato_scelte
            ORDER BY id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_stato_scelte" Name="stato_scelte" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>--%>

<asp:SqlDataSource ID="sql_stazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (codice + ' - ' + nome_stazione) descrizione FROM [stazioni] WITH(NOLOCK) WHERE [attiva] = 1 ORDER BY [codice]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_operatore" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (cognome + ' ' + nome) descrizione FROM [operatori] WITH(NOLOCK) ORDER BY [cognome]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_tipo_riparazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM [fornitore_tipo] WITH(NOLOCK) ORDER BY id">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_fornitore" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, ragione_sociale FROM [fornitore] WITH(NOLOCK)
            ORDER BY ragione_sociale">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_drivers" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (cognome + ' ' + nome) descrizione FROM [drivers] WITH(NOLOCK) ORDER BY [cognome]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_odl_tipo_lavoro" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM [odl_tipo_lavoro] WITH(NOLOCK) ORDER BY [descrizione]">
</asp:SqlDataSource>

<%--<asp:SqlDataSource ID="sqlElencoDanniAperti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT e.id_rds, e.id id_evento, d.id id_danno, d.tipo_record, d.stato, CASE WHEN d.num_odl IS NULL THEN 0 ELSE 1 END chiusura_danno, 
            d.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno, d.da_addebitare,
            a.id id_dotazione, a.descrizione des_dotazione, ce.id id_acessori, ce.descrizione des_acessori, SUBSTRING(d.descrizione, 1, 30) + '...' descrizione_danno
            FROM veicoli_danni d WITH(NOLOCK)
            INNER JOIN veicoli_evento_apertura_danno e WITH(NOLOCK) ON d.id_evento_apertura = e.id
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            LEFT JOIN accessori a WITH(NOLOCK) ON d.id_dotazione = a.id
            LEFT JOIN condizioni_elementi ce WITH(NOLOCK) ON d.id_acessori = ce.id
            WHERE d.id_veicolo = @id_veicolo
            AND d.attivo = 1 
            AND d.stato = 1
            ORDER BY e.data_rds DESC, e.id_rds, d.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_veicolo" Name="id_veicolo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>--%>

<%--<asp:SqlDataSource ID="sqlElencoDanniCollegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT e.id_rds, e.id id_evento, d.id id_danno, d.attivo danno_attivo, d.tipo_record, d.stato, CASE WHEN d.num_odl IS NULL THEN 0 ELSE 1 END chiusura_danno, 
            d.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno, d.da_addebitare,
            a.id id_dotazione, a.descrizione des_dotazione, ce.id id_acessori, ce.descrizione des_acessori, SUBSTRING(d.descrizione, 1, 30) + '...' descrizione_danno
            FROM veicoli_danni d WITH(NOLOCK)
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            INNER JOIN veicoli_evento_apertura_danno e WITH(NOLOCK) ON d.id_evento_apertura = e.id
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            LEFT JOIN accessori a WITH(NOLOCK) ON d.id_dotazione = a.id
            LEFT JOIN condizioni_elementi ce WITH(NOLOCK) ON d.id_acessori = ce.id
            WHERE gd.id_evento_apertura = @id_gruppo_apertura
            
            ORDER BY e.data_rds DESC, e.id_rds, d.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>--%>

<asp:SqlDataSource ID="sqlElencoDanniCollegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT     e.id_rds, e.id AS id_evento, d.id AS id_danno, d.attivo AS danno_attivo, d.tipo_record, d.stato, CASE WHEN d .num_odl IS NULL 
                      THEN 0 ELSE 1 END AS chiusura_danno, d.id_posizione_danno, pd.descrizione AS des_id_posizione_danno, d.id_tipo_danno, td.descrizione AS des_id_tipo_danno, 
                      d.entita_danno, d.da_addebitare, a.id AS id_dotazione, a.descrizione AS des_dotazione, ce.id AS id_acessori, ce.descrizione AS des_acessori, 
                      SUBSTRING(d.descrizione, 1, 30) + '...' AS descrizione_danno
FROM         veicoli_danni AS d WITH (NOLOCK) LEFT OUTER JOIN
                      veicoli_gruppo_danni AS gd WITH (NOLOCK) ON d.id = gd.id_danno INNER JOIN
                      veicoli_evento_apertura_danno AS e WITH (NOLOCK) ON d.id_evento_apertura = e.id LEFT OUTER JOIN
                      veicoli_posizione_danno AS pd WITH (NOLOCK) ON pd.id = d.id_posizione_danno LEFT OUTER JOIN
                      veicoli_tipo_danno AS td WITH (NOLOCK) ON td.id = d.id_tipo_danno LEFT OUTER JOIN
                      accessori AS a WITH (NOLOCK) ON d.id_dotazione = a.id LEFT OUTER JOIN
                      condizioni_elementi AS ce WITH (NOLOCK) ON d.id_acessori = ce.id
WHERE     (gd.id_evento_apertura = @id_gruppo_apertura)  
UNION

SELECT     e.id_rds, e.id AS id_evento, d.id AS id_danno, d.attivo AS danno_attivo, d.tipo_record, d.stato, CASE WHEN d .num_odl IS NULL 
                      THEN 0 ELSE 1 END AS chiusura_danno, d.id_posizione_danno, pd.descrizione AS des_id_posizione_danno, d.id_tipo_danno, td.descrizione AS des_id_tipo_danno, 
                      d.entita_danno, d.da_addebitare, a.id AS id_dotazione, a.descrizione AS des_dotazione, ce.id AS id_acessori, ce.descrizione AS des_acessori, 
                      SUBSTRING(d.descrizione, 1, 30) + '...' AS descrizione_danno
FROM         veicoli_danni AS d WITH (NOLOCK) LEFT OUTER JOIN
                      veicoli_gruppo_accessori AS gd WITH (NOLOCK) ON d.id_veicolo = gd.id_veicolo  INNER JOIN
                      veicoli_evento_apertura_danno AS e WITH (NOLOCK) ON d.id_evento_apertura = e.id LEFT OUTER JOIN
                      veicoli_posizione_danno AS pd WITH (NOLOCK) ON pd.id = d.id_posizione_danno LEFT OUTER JOIN
                      veicoli_tipo_danno AS td WITH (NOLOCK) ON td.id = d.id_tipo_danno LEFT OUTER JOIN
                      accessori AS a WITH (NOLOCK) ON d.id_dotazione = a.id LEFT OUTER JOIN
                      condizioni_elementi AS ce WITH (NOLOCK) ON d.id_acessori = ce.id
WHERE     (gd.id_evento_apertura = @id_gruppo_apertura) AND (d.stato=1 OR (NOT d.id_dotazione IS NULL AND d.num_odl=@num_odl))  AND d.attivo = 1 ">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_odl" Name="num_odl" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<%--  <asp:SqlDataSource ID="sqlElencoDanniCollegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT     e.id_rds, e.id AS id_evento, d.id AS id_danno, d.attivo AS danno_attivo, d.tipo_record, d.stato, CASE WHEN d .num_odl IS NULL 
                      THEN 0 ELSE 1 END AS chiusura_danno, d.id_posizione_danno, pd.descrizione AS des_id_posizione_danno, d.id_tipo_danno, td.descrizione AS des_id_tipo_danno, 
                      d.entita_danno, d.da_addebitare, a.id AS id_dotazione, a.descrizione AS des_dotazione, ce.id AS id_acessori, ce.descrizione AS des_acessori, 
                      SUBSTRING(d.descrizione, 1, 30) + '...' AS descrizione_danno
FROM         veicoli_danni AS d WITH (NOLOCK) LEFT OUTER JOIN
                      veicoli_gruppo_danni AS gd WITH (NOLOCK) ON d.id = gd.id_danno INNER JOIN
                      veicoli_evento_apertura_danno AS e WITH (NOLOCK) ON d.id_evento_apertura = e.id LEFT OUTER JOIN
                      veicoli_posizione_danno AS pd WITH (NOLOCK) ON pd.id = d.id_posizione_danno LEFT OUTER JOIN
                      veicoli_tipo_danno AS td WITH (NOLOCK) ON td.id = d.id_tipo_danno LEFT OUTER JOIN
                      accessori AS a WITH (NOLOCK) ON d.id_dotazione = a.id LEFT OUTER JOIN
                      condizioni_elementi AS ce WITH (NOLOCK) ON d.id_acessori = ce.id
WHERE     (gd.id_evento_apertura = @id_gruppo_apertura)  
UNION

SELECT     e.id_rds, e.id AS id_evento, d.id AS id_danno, d.attivo AS danno_attivo, d.tipo_record, d.stato, CASE WHEN d .num_odl IS NULL 
                      THEN 0 ELSE 1 END AS chiusura_danno, d.id_posizione_danno, pd.descrizione AS des_id_posizione_danno, d.id_tipo_danno, td.descrizione AS des_id_tipo_danno, 
                      d.entita_danno, d.da_addebitare, a.id AS id_dotazione, a.descrizione AS des_dotazione, ce.id AS id_acessori, ce.descrizione AS des_acessori, 
                      SUBSTRING(d.descrizione, 1, 30) + '...' AS descrizione_danno
FROM         veicoli_danni AS d WITH (NOLOCK) LEFT OUTER JOIN
                      veicoli_gruppo_accessori AS gd WITH (NOLOCK) ON d.id_veicolo = gd.id_veicolo AND d.stato=1 INNER JOIN
                      veicoli_evento_apertura_danno AS e WITH (NOLOCK) ON d.id_evento_apertura = e.id LEFT OUTER JOIN
                      veicoli_posizione_danno AS pd WITH (NOLOCK) ON pd.id = d.id_posizione_danno LEFT OUTER JOIN
                      veicoli_tipo_danno AS td WITH (NOLOCK) ON td.id = d.id_tipo_danno LEFT OUTER JOIN
                      accessori AS a WITH (NOLOCK) ON d.id_dotazione = a.id LEFT OUTER JOIN
                      condizioni_elementi AS ce WITH (NOLOCK) ON d.id_acessori = ce.id
WHERE     (gd.id_evento_apertura = @id_gruppo_apertura) AND d.stato=1 AND d.attivo = 1 ">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource> %>

<%--AND d.attivo = 1 sono cmq filtrati con la tabella veicoli_gruppo_danni, ma se non salvo l'odl, devo cancellare il record nella tabella --%>

<%--<asp:SqlDataSource ID="sqlElencoRDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT DISTINCT tdd.codice_sintetico des_id_tipo_documento_apertura, sr.descrizione des_stato_rds,
            ed.id id_evento, ed.sospeso_rds, ed.attivo,ed.id_veicolo, ed.id_tipo_documento_apertura, ed.id_documento_apertura, 
            ed.num_crv, ed.data_rds, ed.id_rds, ed.stato_rds, ed.importo, ed.incasso
            FROM veicoli_danni d WITH(NOLOCK)
            LEFT JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            LEFT JOIN veicoli_gruppo_accessori ON d.id_veicolo = veicoli_gruppo_accessori.id_veicolo AND d.stato=1
            LEFT JOIN veicoli_evento_apertura_danno ed WITH(NOLOCK) ON d.id_evento_apertura = ed.id
            LEFT JOIN veicoli_stato_rds sr WITH(NOLOCK) ON ed.stato_rds = sr.id
            LEFT JOIN veicoli_tipo_documento_apertura_danno tdd WITH(NOLOCK) ON ed.id_tipo_documento_apertura = tdd.id
            WHERE (veicoli_gruppo_accessori.id_evento_apertura = @id_gruppo_apertura) OR(gd.id_evento_apertura = @id_gruppo_apertura)
            ORDER BY ed.data_rds DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>--%>

<asp:SqlDataSource ID="sqlElencoRDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT DISTINCT tdd.codice_sintetico des_id_tipo_documento_apertura, sr.descrizione des_stato_rds,
            ed.id id_evento, ed.sospeso_rds, ed.attivo,ed.id_veicolo, ed.id_tipo_documento_apertura, ed.id_documento_apertura, 
            ed.num_crv, ed.data_rds, ed.id_rds, ed.stato_rds, ed.importo, ed.incasso
            FROM veicoli_danni d WITH(NOLOCK)
            LEFT JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            LEFT JOIN veicoli_gruppo_accessori ON d.id_veicolo = veicoli_gruppo_accessori.id_veicolo AND d.stato=1
            LEFT JOIN veicoli_evento_apertura_danno ed WITH(NOLOCK) ON d.id_evento_apertura = ed.id
            LEFT JOIN veicoli_stato_rds sr WITH(NOLOCK) ON ed.stato_rds = sr.id
            LEFT JOIN veicoli_tipo_documento_apertura_danno tdd WITH(NOLOCK) ON ed.id_tipo_documento_apertura = tdd.id
            WHERE (veicoli_gruppo_accessori.id_evento_apertura = @id_gruppo_apertura) 
            UNION
            SELECT DISTINCT tdd.codice_sintetico des_id_tipo_documento_apertura, sr.descrizione des_stato_rds,
            ed.id id_evento, ed.sospeso_rds, ed.attivo,ed.id_veicolo, ed.id_tipo_documento_apertura, ed.id_documento_apertura, 
            ed.num_crv, ed.data_rds, ed.id_rds, ed.stato_rds, ed.importo, ed.incasso
            FROM veicoli_danni d WITH(NOLOCK)
            LEFT JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            LEFT JOIN veicoli_gruppo_accessori ON d.id_veicolo = veicoli_gruppo_accessori.id_veicolo AND d.stato=1
            LEFT JOIN veicoli_evento_apertura_danno ed WITH(NOLOCK) ON d.id_evento_apertura = ed.id
            LEFT JOIN veicoli_stato_rds sr WITH(NOLOCK) ON ed.stato_rds = sr.id
            LEFT JOIN veicoli_tipo_documento_apertura_danno tdd WITH(NOLOCK) ON ed.id_tipo_documento_apertura = tdd.id
            WHERE (gd.id_evento_apertura = @id_gruppo_apertura)
            ">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDocumentiRDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT DISTINCT ed.id_rds, ed.data_rds, vdf.*, td.descrizione des_tipo_documento
            FROM veicoli_danni d WITH(NOLOCK) 
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            INNER JOIN veicoli_evento_apertura_danno ed WITH(NOLOCK) ON d.id_evento_apertura = ed.id
            INNER JOIN veicoli_danni_evento_foto vdf WITH(NOLOCK) ON d.id_evento_apertura = vdf.id_evento
            INNER JOIN veicoli_danni_img_tipo_documenti td WITH(NOLOCK) ON td.id = vdf.tipo_documento 
            WHERE gd.id_evento_apertura = @id_gruppo_apertura
            ORDER BY ed.data_rds DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_danni_filtro" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="SalvaODL" />






<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="DropDown_stato_odl" ErrorMessage="Specificare il nuovo stato dell'ODL."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="SalvaODL" > </asp:CompareValidator>


<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropDown_tipo_riparazione" ErrorMessage="Specificare il tipo di riparazione."
    Type="Integer" Operator="GreaterThan"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL"></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator3" runat="server" 
    ControlToValidate="DropDown_fornitore" ErrorMessage="Specificare il fornitore."
    Type="Integer" Operator="GreaterThan"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL"></asp:CompareValidator>


<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_data_previsto_rientro" ErrorMessage="Specificare la data di presunto rientro."   
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL">
</asp:RequiredFieldValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="tx_ora_prevista_rientro" ErrorMessage="Specificare l'orario di presunto rientro."   
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL">
</asp:RequiredFieldValidator>

<asp:CompareValidator ID="CompareValidator9" runat="server" 
    ControlToValidate="DropDown_stazione_previsto_rientro" ErrorMessage="Specificare la stazione di presunto rientro"
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="SalvaODL" > </asp:CompareValidator>



<asp:CompareValidator ID="Compare_km_uscita_1" runat="server" 
    ControlToValidate="tx_km_uscita" ErrorMessage="Specificare i km di uscita."
    Type="Integer" Operator="GreaterThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL"></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator7" runat="server" 
    ControlToValidate="tx_litri_uscita" ErrorMessage="I litri di uscita non possono essere inferiori a 0."
    Type="Integer" Operator="GreaterThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL"></asp:CompareValidator>

<asp:CompareValidator ID="Compare_tx_litri_uscita_1" runat="server" 
    ControlToValidate="tx_litri_uscita" ErrorMessage="I litri di uscita non possono essere superiori a 0."
    Type="Integer" Operator="LessThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaODL"></asp:CompareValidator>







<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="ModificaODL" />


<asp:CompareValidator ID="CompareValidator4" runat="server" 
    ControlToValidate="DropDown_stato_odl" ErrorMessage="Non deve essere selezionato un nuovo stato dell'ODL."
    Type="Integer" Operator="Equal" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="ModificaODL" > </asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator5" runat="server" 
    ControlToValidate="DropDown_tipo_riparazione" ErrorMessage="Specificare il tipo di riparazione."
    Type="Integer" Operator="GreaterThan"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ModificaODL"></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator6" runat="server" 
    ControlToValidate="DropDown_fornitore" ErrorMessage="Specificare il fornitore."
    Type="Integer" Operator="GreaterThan"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ModificaODL"></asp:CompareValidator>

<asp:CompareValidator ID="Compare_km_uscita_2" runat="server" 
    ControlToValidate="tx_km_uscita" ErrorMessage="Specificare i km di uscita."
    Type="Integer" Operator="GreaterThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ModificaODL"></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator8" runat="server" 
    ControlToValidate="tx_litri_uscita" ErrorMessage="I litri di uscita non possono essere inferiori a 0."
    Type="Integer" Operator="GreaterThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ModificaODL"></asp:CompareValidator>

<asp:CompareValidator ID="Compare_tx_litri_uscita_2" runat="server" 
    ControlToValidate="tx_litri_uscita" ErrorMessage="I litri di uscita non possono essere superiori a 0."
    Type="Integer" Operator="LessThanEqual"  ValueToCompare="0" 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="ModificaODL"></asp:CompareValidator>