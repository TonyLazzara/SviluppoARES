<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gestione_mappe.ascx.vb" Inherits="gestione_danni_gestione_mappe" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="posizione_danno.ascx" TagName="posizione_danno" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/modelli.ascx" TagName="modelli" TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="/lytebox.js"></script>
<link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />

    <script type="text/javascript" language="JavaScript">
        function point_it_F(event) {
            var div_img_fronte = document.getElementById('<%= div_img_fronte.UniqueID.replace("$","_") %>');

            var tx_X = document.getElementById('<%= tx_X_F.UniqueID.replace("$","_") %>');
            var tx_Y = document.getElementById('<%= tx_Y_F.UniqueID.replace("$","_") %>');

            var pos_x = event.offsetX ? (event.offsetX) : event.pageX - div_img_fronte.offsetLeft;
            var pos_y = event.offsetY ? (event.offsetY) : event.pageY - div_img_fronte.offsetTop;

            tx_X.value = pos_x;
            tx_Y.value = pos_y;
        }

        function point_it_R(event) {
            var div_img_fronte = document.getElementById('<%= div_img_retro.UniqueID.replace("$","_") %>');

            var tx_X = document.getElementById('<%= tx_X_R.UniqueID.replace("$","_") %>');
            var tx_Y = document.getElementById('<%= tx_Y_R.UniqueID.replace("$","_") %>');

            var pos_x = event.offsetX ? (event.offsetX) : event.pageX - div_img_fronte.offsetLeft;
            var pos_y = event.offsetY ? (event.offsetY) : event.pageY - div_img_fronte.offsetTop;

            tx_X.value = pos_x;
            tx_Y.value = pos_y;

        }

        function verificaImmagineFronte() {
            var messaggio = CheckFileExtension(document.getElementById('<%= FileUpload_img_fronte.UniqueID.replace("$","_") %>'), "fronte");
            if (messaggio != "") {
                alert(messaggio);
                return false;
            }
            return true;
        }

        function verificaImmagineRetro() {
            var messaggio = CheckFileExtension(document.getElementById('<%= FileUpload_img_retro.UniqueID.replace("$","_") %>'), "retro");
            if (messaggio != "") {
                alert(messaggio);
                return false;
            }
            return true;
        }

        function verificaTutteImmagini() {
            var messaggio = "";
            var tx_descrizione_new_immagine = document.getElementById('<%= tx_descrizione_new_immagine.UniqueID.replace("$","_") %>');
            if (tx_descrizione_new_immagine.value == "") {
                messaggio += "Il campo descrizione modello deve essere valorizzato.";
            }
            var messaggio_fronte = CheckFileExtension(document.getElementById('<%= FileUpload_new_img_fronte.UniqueID.replace("$","_") %>'), "fronte");
            var messaggio_retro = CheckFileExtension(document.getElementById('<%= FileUpload_new_img_retro.UniqueID.replace("$","_") %>'), "retro");

            if (messaggio_fronte != "") {
                if (messaggio != "") {
                    messaggio += "\n";
                }
                messaggio += messaggio_fronte;
            }
            if (messaggio_retro != "") {
                if (messaggio != "") {
                    messaggio += "\n";
                }
                messaggio += messaggio_retro;
            }
            if (messaggio != "") {
                alert(messaggio);
                return false;
            }
            return true;
        }

        function CheckFileExtension(ctrlUpload, nome_file) {
            // http://shawpnendu.blogspot.it/2009/05/file-upload-with-aspnet-c.html

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                return "Selezionare un file da salvare per l'immagine " + nome_file + ".";
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".png");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension)
                    return "";
            }
            return "Il formato del file per l'immagine " + nome_file + " deve essere (jpg) o (png).";
        }

        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
            }
        }

    </script>


<div id="div_ricerca" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Ricerca Modelli Mappati</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Immagine Modello:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_descrizione_immagine" runat="server" Width="120px"></asp:TextBox>
                &nbsp;<asp:Button ID="bt_cerca_immagine" runat="server" Text="Cerca" />
            </td>
            <td align="right">
                <asp:Button ID="bt_aggiungi_immagine" runat="server" Text="Nuova Immagine" />
            </td>
        </tr>
    </table>
</div>

<div id="div_elenco_immagini" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table6">
    <tr>
      <td>
          <asp:ListView ID="listViewElencoImmagini" runat="server" DataSourceID="sqlElencoImmagini">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>'/>
                          <asp:Label ID="lb_img_fronte" runat="server" Text='<%# Eval("img_fronte") %>' Visible="false" />
                          <asp:Label ID="lb_img_retro" runat="server" Text='<%# Eval("img_retro") %>' Visible="false" />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <td align="center" width="40px">
                        <%--<asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della mappatura del modello?'));" ToolTip="Elimina Mappatura" />--%>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_img_fronte" runat="server" Text='<%# Eval("img_fronte") %>' Visible="false" />
                          <asp:Label ID="lb_img_retro" runat="server" Text='<%# Eval("img_retro") %>' Visible="false" />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <td align="center" width="40px">
                       <%-- <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della mappatura del modello?'));" ToolTip="Elimina Mappatura" />--%>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Per i filtri applicati non è stata trovata nessuna voce.
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
                                      <th>
                                          Modello Mappato</th>
                                      <th></th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="20" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
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
</div>

<div id="div_modifica_immagine" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Mappatura Modello</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label7" runat="server" Text="Descrizione Modello:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_descrizione_img" runat="server" Width="210px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="bt_modifica_immagine" runat="server" Text="Modifica Modello" Width="200px"/>
            </td>
            <td  align="right">
                <asp:Button ID="bt_mappa_modello" runat="server" Text="Mappa Modello" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lb_img_fronte_1" runat="server" Text="Immagine Fronte:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="center" width="40px">
                <a href="/images/Mappe/<%= lb_nome_img_fronte.text %>" rel='lytebox[mappa_img]'><img src="/images/lente.png" style="width: 16px" /></a>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload_img_fronte" runat="server" />
            </td>
            <td>
                <asp:Button ID="bt_upload_fronte" runat="server" Text="Sostituisci Immagine"  Width="200px" OnClientClick="javascript:return verificaImmagineFronte();" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lb_img_retro_1" runat="server" Text="Immagine Retro:" CssClass="testo_bold"></asp:Label>
            </td>
            <td align="center" width="40px">
                <a href="/images/Mappe/<%= lb_nome_img_retro.text %>" rel='lytebox[mappa_img]'><img src="/images/lente.png" style="width: 16px" /></a>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload_img_retro" runat="server" />
            </td>
            <td>
                <asp:Button ID="bt_upload_retro" runat="server" Text="Sostituisci Immagine"  Width="200px" OnClientClick="javascript:return verificaImmagineRetro();" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:Button ID="bt_chiudi_immagine" runat="server" Text="Chiudi" />
            </td>
        </tr>
    </table>    
</div>


<div id="div_nuova_immagine" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Mappatura Modello</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Descrizione Modello:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_descrizione_new_immagine" runat="server" Width="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Immagine Fronte:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload_new_img_fronte" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Immagine Retro:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload_new_img_retro" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="bt_salva_new_immagine" runat="server" Text="Salva" OnClientClick="javascript:return verificaTutteImmagini();"/>
                <asp:Button ID="bt_chiudi_new_immagine" runat="server" Text="Chiudi" />
            </td>
        </tr>
    </table>    
</div>

<div id="div_elenco_modelli_associati" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table5">
    <tr>
      <td>
          <asp:ListView ID="listView_modelli_associati" runat="server" DataSourceID="sqlElencoAssociazioneModelli">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id_modello") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione dell\'associazione del modello?'));" ToolTip="Elimina Associazione" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id_modello") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione dell\'associazione del modello?'));" ToolTip="Elimina Associazione" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Nessun modello associato alla mappa.
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
                                      <th>
                                          Modelli Associati</th>
                                      <th></th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="18" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
                                  </Fields>
                              </asp:DataPager>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
      </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="bt_aggiungi_modello" runat="server" Text="Aggiungi Modello" />
        </td>
    </tr>
  </table>
</div>

<div id="div_elenco_modelli_non_ass" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Modelli Non Associati</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Filtro Modello:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_filtro_modello" runat="server" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="bt_filtra" runat="server" Text="Filtra" />
                <asp:Button ID="bt_chiudi_filtro" runat="server" Text="Chiudi" />
            </td>
        </tr>
    </table>
    <br />

  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table7">
    <tr>
      <td>
        <div style="height:440px; overflow-y: scroll;">
            <asp:ListView ID="listView_elenco_modelli_non_ass" runat="server" 
                DataSourceID="sqlElencoModelliNonAss" DataKeyNames="id_modello" >
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id_modello") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_macro_modello" runat="server" Text='<%# Eval("macro_modello") %>' style="color:Red;" />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id_modello") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_macro_modello" runat="server" Text='<%# Eval("macro_modello") %>' style="color:Red;" />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Nessun record ottenuto per il filtro selezionato.
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
                                      <th id="Th1" runat="server" style="width:16px;">
                                            <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" />
                                      </th>
                                      <th>
                                          Modello</th>
                                      <th>
                                          Macro Modello</th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="40" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
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
    <tr>
        <td align="center">
            <asp:Button ID="bt_aggiungi_modelli" runat="server" Text="Aggiungi Modelli" />
        </td>
    </tr>
  </table>
</div>

<div id="div_mappe" runat="server" visible="false">
<ajaxtoolkit:TabContainer ID="tab_mappe" runat="server" ActiveTabIndex="0" Width="100%">

      <ajaxtoolkit:TabPanel ID="tab_fronte" runat="server" HeaderText="Vista Fronte">
            <HeaderTemplate>Vista Fronte</HeaderTemplate>
            <ContentTemplate>
            
<div id="div_img_fronte" runat="server" style="position:relative;" >
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table4">
  <tr id="Tr1" runat="server">
    <td id="Td1" runat="server">
        <div style="position:absolute; top:0px; left:0px;">
            <asp:TextBox ID="tx_X_F" runat="server" Width="30px" text="0"></asp:TextBox>
            <asp:TextBox ID="tx_Y_F" runat="server" Width="30px" text="0"></asp:TextBox>
        </div>
        <asp:Image ID="img_fronte" runat="server"  ImageUrl="~/images/SchemaAuto.gif" style="position:relative;" onclick="javascript:point_it_F(event);" />

    </td>
    <td id="Td2" style="width:1px" runat="server">&nbsp;</td>
    <td id="Td3" align="left" valign="top" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table1">
          <tr id="Tr2" runat="server">
              <td id="Td4" runat="server">
                <asp:Label ID="Label3" runat="server" Text="Posizione Danno:" CssClass="testo_bold"></asp:Label>
              </td>
              <td id="Td5" runat="server">
                  <asp:DropDownList ID="DropDownPosizioneDanno_F" runat="server" AppendDataBoundItems="True"
                            DataSourceID="sqlPosizioneDanno" DataTextField="descrizione" DataValueField="id" ValidationGroup="salva_nuovo_punto_F">
                        <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                  </asp:DropDownList>
                  &nbsp;<asp:ImageButton ID="Add_PosizioneDanno_F" runat="server" ImageUrl="/images/aggiorna.png" style="width: 16px"/>
              </td>
          </tr>
          
          <tr id="Tr5" runat="server">
            <td id="Td10" colspan="2" runat="server">
                 &nbsp;
            </td>
          </tr>
          <tr id="Tr6" runat="server">
            <td id="Td11" colspan="2" align="center" runat="server">
                <asp:Button ID="bt_salva_nuovo_punto_F" runat="server" Text="Salva" ValidationGroup="salva_nuovo_punto_F" />
                <asp:Button ID="bt_chiudi_mappatura_F" runat="server" Text="Chiudi" />
            </td>
          </tr>
        </table>
    </td>
  </tr>
</table>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="salva_nuovo_punto_F" />

<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="DropDownPosizioneDanno_F" ErrorMessage="Specificare la posizione del danno associato."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="salva_nuovo_punto_F" ></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator4" runat="server" 
    ControlToValidate="tx_X_F" ErrorMessage="Specificare una coordinata valida per il punto mappa cliccando sull'immagine."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="salva_nuovo_punto_F" ></asp:CompareValidator>
    
</div>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tab_retro" runat="server" HeaderText="Vista Retro" Visible="false">
        <HeaderTemplate>Vista Retro</HeaderTemplate>
            <ContentTemplate>
<div id="div_img_retro" runat="server" style="position:relative;" >
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">
  <tr>
    <td>
        <div style="position:absolute; top:0px; left:0px; ">
            <asp:TextBox ID="tx_X_R" runat="server" Width="30px" text="0"></asp:TextBox>
            <asp:TextBox ID="tx_Y_R" runat="server" Width="30px" text="0"></asp:TextBox>
        </div>
        <asp:Image ID="img_retro" runat="server"  ImageUrl="~/images/SchemaAuto.gif" style="position:relative;" onclick="javascript:point_it_R(event);" />
        
    </td>
    <td style="width:1px">&nbsp;</td>
    <td align="left" valign="top">
        <table width="100%" cellpadding="0" cellspacing="0" runat="server" >
          <tr>
              <td>
                <asp:Label ID="Label4" runat="server" Text="Posizione Danno:" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                  <asp:DropDownList ID="DropDownPosizioneDanno_R" runat="server" AppendDataBoundItems="True"
                            DataSourceID="sqlPosizioneDanno" DataTextField="descrizione" DataValueField="id" >
                        <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                  </asp:DropDownList>
                  &nbsp;<asp:ImageButton ID="Add_PosizioneDanno_R" runat="server" ImageUrl="/images/aggiorna.png" style="width: 16px"/>
              </td>
          </tr>

          <tr>
            <td colspan="2">
                 &nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="2" align="center">
                <asp:Button ID="bt_salva_nuovo_punto_R" runat="server" Text="Salva" ValidationGroup="salva_nuovo_punto_R" />
                <asp:Button ID="bt_chiudi_mappatura_R" runat="server" Text="Chiudi" />
            </td>
          </tr>
        </table>
        &nbsp;</td>
  </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary2" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="salva_nuovo_punto_R" />

<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropDownPosizioneDanno_R" ErrorMessage="Specificare la posizione del danno associato."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="salva_nuovo_punto_R" > </asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator3" runat="server" 
    ControlToValidate="tx_X_R" ErrorMessage="Specificare una coordinata valida per il punto mappa cliccando sull'immagine."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="salva_nuovo_punto_R" > </asp:CompareValidator>

</div>
     </ContentTemplate>
        </ajaxtoolkit:TabPanel>
      
 </ajaxtoolkit:TabContainer>  

 </div>

 
<div id="div_EditPosizioneDanno" runat="server">
    <uc1:posizione_danno id="posizione_danno" runat="server" />
</div>

<div id="div_edit_modelli" runat="server">
    <uc1:modelli id="modelli" runat="server" />
</div>

<asp:Label ID="lb_descrizione_immagine" runat="server" Text="%" Visible="false"></asp:Label> 
<asp:Label ID="lb_descrizione_modello" runat="server" Text="%" Visible="false"></asp:Label> 
<asp:Label ID="lb_id_img_modello" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lb_add_posizione_origine" runat="server" Text="" Visible="false"></asp:Label>

<asp:Label ID="lb_nome_img_retro" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lb_nome_img_fronte" runat="server" Text="" Visible="false"></asp:Label>

<asp:Label ID="lb_stato_visibilita" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lb_stato_visibilita_old" runat="server" Text="" Visible="false"></asp:Label>
 
<asp:SqlDataSource ID="sqlElencoImmagini" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_img_modelli]  
            WHERE descrizione like @descrizione ORDER BY descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_descrizione_immagine" Name="descrizione" PropertyName="Text" Type="String" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlPosizioneDanno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM [veicoli_posizione_danno] ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlElencoAssociazioneModelli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT m.id_modello, m.descrizione FROM [veicoli_img_associazione_modelli] am
            INNER JOIN [MODELLI] m ON am.id_modello = m.id_modello  AND am.id_img_modello = @id_img_modello
            ORDER BY m.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_img_modello" Name="id_img_modello" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlElencoModelliNonAss" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT m.id_modello, m.descrizione, mm.descrizione macro_modello FROM [MODELLI] m
            LEFT JOIN [veicoli_img_associazione_modelli] am ON am.id_modello = m.id_modello AND am.id_img_modello = @id_img_modello
            LEFT JOIN [veicoli_img_associazione_modelli] am2 ON am2.id_modello = m.id_modello
            LEFT JOIN [veicoli_img_modelli] mm ON mm.id = am2.id_img_modello
            WHERE am.id_img_modello IS NULL AND m.descrizione like @descrizione ORDER BY m.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_img_modello" Name="id_img_modello" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_descrizione_modello" Name="descrizione" PropertyName="Text" Type="String" />
        </SelectParameters>
</asp:SqlDataSource>

