<%@ Control Language="VB" AutoEventWireup="false" CodeFile="edit_danno.ascx.vb" Inherits="gestione_danni_edit_danno" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<script type="text/javascript" language="javascript" src="~/lytebox.js"></script>
<link rel="stylesheet" href="~/lytebox.css" type="text/css" media="screen" />


<script type="text/javascript" language="javascript">

    function ChiudiLyteBox() {
        if (parent.myLytebox != null) {
            parent.myLytebox.end();
        }
    }

 </script>

<div runat="server" id="div_targa" visible="false">
<table border="0" cellpadding="0" cellspacing="0" width="1024px" >
    <tr>
        <td  align="left" style="color: #FFFFFF;background-color:#444;">
                
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Stazione: </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>                                                       
        </td>
    </tr>
</table>
</div>


<div id="div_mappe" runat="server" visible="false">


<ajaxtoolkit:TabContainer ID="tab_mappe" runat="server" ActiveTabIndex="0" 
        Width="100%">

      <ajaxtoolkit:TabPanel ID="tab_fronte" runat="server" HeaderText="Vista Fronte">
            <HeaderTemplate>Vista Fronte</HeaderTemplate>
            <ContentTemplate>

<script type="text/javascript">

  <% DropDownPosizione_F.Enabled = False   %> //aggiunto 03.05.2021 1657 per punto 17 step 2 


 <%--   function enabled_ddl() {
        <% DropDownPosizione_F.Enabled = True   %> //aggiunto 03.05.2021 1657 per punto 17 step 2
    }

    function disabled_ddl() {
        <% DropDownPosizione_F.Enabled = False    %> //aggiunto 03.05.2021 1657 per punto 17 step 2
    }
--%>

    function point_it_F(Valore) {
        
        if(<%= lb_disabilita_mappa.text %>) {
            return false;
        }

        var DropDownPosizione = document.getElementById('<%= DropDownPosizione_F.UniqueID.replace("$", "_") %>');
        if (DropDownPosizione == null) {
            return false;
        }
             


        var valori = "";
       
        for (i = 0; i < DropDownPosizione.options.length; i++) {           
           
            if (DropDownPosizione.options[i].value == Valore) {
                DropDownPosizione.options[i].selected = true;
                document.getElementById('<%= lbl_posizione.UniqueID.Replace("$", "_") %>').value = Valore;
                document.getElementById('lblpos').value = Valore;  
                //document.getElementById('lbl_posizione').value = Valore;  
                
                return false;
            }
        }

        return false;
    }

</script>   
          
<div id="div_img_fronte" runat="server" style="position:relative;" >


<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">

  <tr id="Tr1" runat="server">
    <td valign="top" runat="server">
        
        <asp:Image ID="img_fronte" runat="server"  ImageUrl="~/images/SchemaAuto.gif"   />

    </td>
    <td id="Td2" style="width:1px" runat="server">&nbsp;</td>
    <td id="Td3" align="left" valign="top" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table4">
    <tr id="Tr2" runat="server">
        <td id="Td4" runat="server">
            <asp:Label ID="Label4" runat="server" Text="Posizione:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td5" runat="server">
            <asp:DropDownList ID="DropDownPosizione_F" runat="server" AppendDataBoundItems="True"  Enabled="false"
                    DataSourceID="sqlPosizioneDanno" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True" >Seleziona.-..</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lbl_posizione" runat="server" Text="" Width="20" ForeColor="White" BorderColor="White"></asp:Label>   
          <input id="lblpos" name="lblpos" style="width:20px;color:white;border:none;" />
        </td>
    </tr>
    <tr id="Tr3" runat="server">
      <td id="Td6" runat="server">
        <asp:Label ID="Label6" runat="server" Text="Danno:" CssClass="testo_bold"></asp:Label>                       
      </td>
      <td id="Td7" runat="server">            
        <asp:DropDownList ID="DropDownTipoDanno_F" runat="server" AppendDataBoundItems="True" 
                DataSourceID="sqlTipoDanno" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
        </asp:DropDownList>
       </td>
    </tr>
    <tr id="Tr4" runat="server">
      <td id="Td8" runat="server">
        <asp:Label ID="Label9" runat="server" Text="Entità:" CssClass="testo_bold"></asp:Label>
      </td>
      <td id="Td9" runat="server">
          <asp:DropDownList ID="DropDownEntita_F" runat="server" AppendDataBoundItems="True">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                <asp:ListItem Value="1">Lieve</asp:ListItem>
                <asp:ListItem Value="2">Medio</asp:ListItem>
                <asp:ListItem Value="3">Grave</asp:ListItem>
          </asp:DropDownList>
      </td>
    </tr>
    <tr runat="server">
      <td  valign="top" runat="server" >
          <asp:Label ID="Label10" runat="server" Text="Nota:" CssClass="testo_bold"></asp:Label>
      </td>
      <td id="Td11" runat="server" >
          <asp:TextBox ID="tx_descrizione_F" runat="server" Width="300px" Height="65px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="Tr6" runat="server">
      <td id="Td12" colspan="2" runat="server">&nbsp;
      </td>
    </tr>
    <tr id="Tr7" runat="server">
      <td id="Td13" align="center" colspan="2" runat="server">
            <asp:Button ID="btnSalvaNuovo_F" runat="server" Text="Salva Nuovo Danno" ValidationGroup="Salva_F" />
            <asp:Button ID="btnChiudiNuovo_F" runat="server" Text="Chiudi" visible="False"/>
      </td>
    </tr>
    <tr id="Tr8" runat="server">
      <td id="Td14" colspan="2" runat="server">
<div runat="server" id="div_elenco_documenti_F">
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table7">
    <tr id="Tr9" runat="server" visible="False">
        <td id="Td15" align="center" colspan="2" runat="server" visible="False">
            <asp:Button ID="btnModifica_F" runat="server" Text="Modifica" ValidationGroup="Salva_F" />
            <asp:Button ID="btnChiudiModifica_F" runat="server" Text="Chiudi" />
        </td>
    </tr>
    <tr id="Tr10" runat="server">
        <td id="Td16" colspan="2" runat="server">&nbsp;
        </td>
    </tr>
    <tr id="Tr11" runat="server">
      <td id="Td17" colspan="2" runat="server">
         <asp:ListView ID="listViewDocumenti_F" runat="server" 
              DataSourceID="sqlDocumenti" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
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
                       <%-- <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni_F]'><img src="/images/lente.png" style="width: 16px" /></a>--%>
                       <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
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
                        <%--<a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni_F]'><img src="/images/lente.png" style="width: 16px" /></a>--%>
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
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
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Documento</th>
                                      <th>
                                          Nome</th>
                                      <th>
                                          </th>
                                      <th>
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
    <tr runat="server" >
        <td runat="server" >
            <asp:Label ID="Label11" runat="server" Text="Allega Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td runat="server"   >
            <asp:FileUpload ID="FileUpload1_F" size="32" runat="server" />
        </td>
    </tr>
    <tr runat="server">
        <td runat="server" >
            <asp:Label ID="Label12" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td runat="server" >
            <asp:DropDownList ID="DropDownTipoDocumentoImg_F" runat="server" AppendDataBoundItems="True" 
                      DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="btnInviaFile_F" runat="server" Text="Salvataggio" ValidationGroup="Upload_F" />
        </td>
    </tr>
   </table> 
</div>
<div id="div_da_addebitare_F" runat="server" visible="False">
	<br /><br />
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table11">
        <tr runat="server">
            <td runat="server" >
                <asp:Label ID="Label22" runat="server" Text="Motivo Non Addebito:" CssClass="testo_bold"></asp:Label>
            </td>
            <td runat="server">
                <asp:DropDownList ID="DropDownNonAddebito_F" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" >
            <td colspan="2" align="center" runat="server" >
                <asp:Button ID="bt_da_addebitare_F" runat="server" Text="Da Addebitare" />
                <asp:Button ID="bt_da_non_addebitare_F" runat="server" Text="Da Non Addebitare" ValidationGroup="Non_Addebito_F" />
            </td>
        </tr>
    </table>
</div>
      </td> 
    </tr>
  </table>        
    </td>
  </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary2" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Salva_F" />

<asp:CompareValidator ID="CompareValidator3" runat="server" 
    ControlToValidate="DropDownPosizione_F" ErrorMessage="Specificare la posizione del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_F" ></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator6" runat="server" 
    ControlToValidate="DropDownTipoDanno_F" ErrorMessage="Specificare il danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_F" ></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator7" runat="server" 
    ControlToValidate="DropDownEntita_F" ErrorMessage="Specificare l'entità del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_F" ></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary4" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Upload_F" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
    ControlToValidate="FileUpload1_F" ErrorMessage="Nessuna immagine selezionata." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload_F"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator8" runat="server" 
    ControlToValidate="DropDownTipoDocumentoImg_F" ErrorMessage="Specificare il tipo del documento."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Upload_F" ></asp:CompareValidator>
    
<asp:ValidationSummary ID="ValidationSummary9" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Non_Addebito_F" />

<asp:CompareValidator ID="CompareValidator13" runat="server" 
    ControlToValidate="DropDownNonAddebito_F" ErrorMessage="Specificare il motivo di non addebito del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Non_Addebito_F" ></asp:CompareValidator>

</div>

            </ContentTemplate>
        </ajaxtoolkit:TabPanel>

<%--<ajaxtoolkit:TabPanel ID="tab_retro" runat="server" HeaderText="Vista Retro">
            <HeaderTemplate>Vista Retro</HeaderTemplate>
   <ContentTemplate>

<script type="text/javascript" language="javascript">

    function point_it_R(Valore) {
        if(<%= lb_disabilita_mappa.text %>) {
            return false;
        }
        var DropDownPosizione = document.getElementById('<%= DropDownPosizione_R.UniqueID.replace("$","_") %>');
        if (DropDownPosizione == null) {
            return false;
        }

        for (i = 0; i < DropDownPosizione.options.length; i++) {
            if (DropDownPosizione.options[i].value == Valore) {
                DropDownPosizione.options[i].selected = true;
                return false;
            }
        }
        return false;
    }

 </script>

<div id="div_img_retro" runat="server" style="position:relative;" >


<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table6">
  <tr id="Tr14" runat="server">
    <td valign="top" runat="server">
        
        <asp:Image ID="img_retro" runat="server"  ImageUrl="~/images/SchemaAuto.gif" style="position:relative;" />
        
    </td>
    <td id="Td23" style="width:1px" runat="server">&nbsp;</td>
    <td id="Td24" align="left" valign="top" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table3">
    <tr id="Tr15" runat="server">
        <td id="Td25" runat="server">
            <asp:Label ID="Label1" runat="server" Text="Posizione:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td26" runat="server">
            <asp:DropDownList ID="DropDownPosizione_R" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlPosizioneDanno" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="Tr16" runat="server">
      <td id="Td27" runat="server">
        <asp:Label ID="Label8" runat="server" Text="Danno:" CssClass="testo_bold"></asp:Label>                       
      </td>
      <td id="Td28" runat="server">            
        <asp:DropDownList ID="DropDownTipoDanno_R" runat="server" AppendDataBoundItems="True" 
                DataSourceID="sqlTipoDanno" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
        </asp:DropDownList>
       </td>
    </tr>
    <tr id="Tr17" runat="server">
      <td id="Td29" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Entità:" CssClass="testo_bold"></asp:Label>
      </td>
      <td id="Td30" runat="server">
          <asp:DropDownList ID="DropDownEntita_R" runat="server" AppendDataBoundItems="True">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                <asp:ListItem Value="1">Lieve</asp:ListItem>
                <asp:ListItem Value="2">Medio</asp:ListItem>
                <asp:ListItem Value="3">Grave</asp:ListItem>
          </asp:DropDownList>
      </td>
    </tr>
    <tr runat="server">
      <td valign="top" runat="server" >
          <asp:Label ID="Label3" runat="server" Text="Nota:" CssClass="testo_bold"></asp:Label>
      </td>
      <td id="Td32" runat="server" >
          <asp:TextBox ID="tx_descrizione_R" runat="server" Width="300px" Height="65px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="Tr19" runat="server">
      <td colspan="2" runat="server">&nbsp;
      </td>
    </tr>
    <tr runat="server">
      <td align="center" colspan="2" runat="server">
            <asp:Button ID="btnSalvaNuovo_R" runat="server" Text="Salva Nuovo Danno" ValidationGroup="Salva_R" />
            <asp:Button ID="btnChiudiNuovo_R" runat="server" Text="Chiudi" 
                visible="False" />
      </td>
    </tr>
    <tr runat="server">
      <td colspan="2" runat="server">
<div runat="server" id="div_elenco_documenti_R">
<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table5">
    <tr runat="server">
        <td id="Td19" align="center" colspan="2" runat="server" visible="False" >
            <asp:Button ID="btnModifica_R" runat="server" Text="Modifica" ValidationGroup="Salva_R" />
            <asp:Button ID="btnChiudiModifica_R" runat="server" Text="Chiudi" />
        </td>
    </tr>
    <tr runat="server">
        <td colspan="2" runat="server">&nbsp;
        </td>
    </tr>
    <tr runat="server">
      <td colspan="2" runat="server">
         <asp:ListView ID="listViewDocumenti_R" runat="server" 
              DataSourceID="sqlDocumenti" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
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
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
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
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
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
                                      <th id="Th5" runat="server">
                                          Documento</th>
                                      <th id="Th2" runat="server">
                                          Nome</th>
                                      <th id="Th_lente" runat="server">
                                          </th>
                                      <th id="Th_elimina" runat="server">
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
    <tr runat="server">
        <td runat="server">
            <asp:Label ID="Label5" runat="server" Text="Allega Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td runat="server" >
            <asp:FileUpload ID="FileUpload1_R" size="32" runat="server" />
        </td>
    </tr>
    <tr runat="server">
        <td runat="server">
            <asp:Label ID="Label7" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td runat="server" >
            <asp:DropDownList ID="DropDownTipoDocumentoImg_R" runat="server" AppendDataBoundItems="True" 
                      DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="btnInviaFile_R" runat="server" Text="Salvataggio" ValidationGroup="Upload_R" />
        </td>
    </tr>
   </table> 
</div>
<div id="div_da_addebitare_R" runat="server" visible="False">
    <br /><br />
    <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
            <td runat="server" >
                    <asp:Label ID="Label23" runat="server" Text="Motivo Non Addebito:" CssClass="testo_bold"></asp:Label>
            </td>
            <td runat="server" >
                <asp:DropDownList ID="DropDownNonAddebito_R" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server">
            <td id="Td10" align="center" runat="server" colspan="2" >
                <asp:Button ID="bt_da_addebitare_R" runat="server" Text="Da Addebitare" />
                <asp:Button ID="bt_da_non_addebitare_R" runat="server" Text="Da Non Addebitare" ValidationGroup="Non_Addebito_R" />
            </td>
        </tr>
    </table>
</div>
      </td> 
    </tr>
  </table>        
    </td>
  </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Salva_R" />

<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="DropDownPosizione_R" ErrorMessage="Specificare la posizione del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_R" ></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator5" runat="server" 
    ControlToValidate="DropDownTipoDanno_R" ErrorMessage="Specificare il danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_R" ></asp:CompareValidator>

<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropDownEntita_R" ErrorMessage="Specificare l'entità del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Salva_R" ></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Upload_R" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
    ControlToValidate="FileUpload1_R" ErrorMessage="Nessuna immagine selezionata." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload_R"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator4" runat="server" 
    ControlToValidate="DropDownTipoDocumentoImg_R" ErrorMessage="Specificare il tipo del documento."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Upload_R" ></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary8" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Non_Addebito_R" />

<asp:CompareValidator ID="CompareValidator12" runat="server" 
    ControlToValidate="DropDownNonAddebito_R" ErrorMessage="Specificare il motivo di non addebito del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Non_Addebito_R" ></asp:CompareValidator>

</div>

            </ContentTemplate>
</ajaxtoolkit:TabPanel>--%>


<ajaxtoolkit:TabPanel ID="tab_accessori" runat="server" HeaderText="Accessori/Dotazioni">
            <HeaderTemplate>Accessori/Dotazioni</HeaderTemplate>
            <ContentTemplate>

<div id="div_acessori" runat="server" style="position:relative;" >
&nbsp;<asp:Label ID="Label14" runat="server" Text="Dotazioni" CssClass="testo_bold"></asp:Label>

<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table8">
  <tr runat="server">
    <td style="vertical-align:top;" runat="server">
        <asp:ListView ID="ListViewDotazioni" runat="server" 
            DataSourceID="sqlDanniDotazioni" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si'/>
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                       </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si' />
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun elemento in dotazione previsto.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Disponibile al Check Out</th>
                                      <th>
                                          Descrizione</th>
                                      <th>
                                          Assente al Check In</th>
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
<br />
&nbsp;<asp:Label ID="Label15" runat="server" Text="Acessori" CssClass="testo_bold"></asp:Label>

<table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table9">
  <tr runat="server">
    <td style="vertical-align:top;" runat="server">
        <asp:ListView ID="ListViewAcessori" runat="server" DataSourceID="sqlDanniAcessori" 
            DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si'/>
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                       </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si' />
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" />
                       </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun acessorio previsto.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Disponibile al Check Out</th>
                                      <th>
                                          Descrizione</th>
                                      <th>
                                          Assente al Check In</th>
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

<br />
<table border="0" cellpadding="0" cellspacing="2" width="100%">
    <tr>
    <td align="center">
        <asp:Button ID="bt_salva_acessori" runat="server" Text="Salva Acessori/Dotazioni Assenti"/>
    </td>
    </tr>
</table>    
</div>


<div id="div_acessori_read" runat="server" style="position:relative;" visible="False" >
&nbsp;<asp:Label ID="Label19" runat="server" Text="Dotazioni" CssClass="testo_bold"></asp:Label>

<table width="100%" cellpadding="0" cellspacing="0">
  <tr >
    <td style="vertical-align:top;">
        <asp:ListView ID="ListViewDotazioni_read" runat="server" 
            DataSourceID="sql_Dotazioni" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                         <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                         <asp:Label ID="lb_presente" runat="server" Text='' />
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_dotazione" runat="server" Text='<%# Eval("dotazione") %>' Visible="false" />
                            <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                            <asp:CheckBox ID="ck_da_addebitare" runat="server"/>
                       </td>
                       <td>
                            <asp:Label ID="lb_motivo_non_addebito" runat="server" Text='<%# Eval("motivo_non_addebito") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownNonAddebito" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>
                       </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                         <asp:Label ID="lb_assente" runat="server" Text='<%# Eval("assente") %>' Visible="false" />
                         <asp:Label ID="lb_presente" runat="server" Text='' />
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_dotazione" runat="server" Text='<%# Eval("dotazione") %>' Visible="false" />
                            <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                            <asp:CheckBox ID="ck_da_addebitare" runat="server"/>
                       </td>
                       <td>
                            <asp:Label ID="lb_motivo_non_addebito" runat="server" Text='<%# Eval("motivo_non_addebito") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownNonAddebito" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>
                       </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun elemento in dotazione previsto.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Disponibile al Check Out</th>
                                      <th>
                                          Descrizione</th>
                                      <th>
                                          Assente al Check In</th>
                                      <th>
                                          Da Addebitare</th>
                                      <th>Motivo non addebito</th>
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
<br />
&nbsp;<asp:Label ID="Label20" runat="server" Text="Acessori" CssClass="testo_bold"></asp:Label>

<table width="100%" cellpadding="0" cellspacing="0">
  <tr>
    <td style="vertical-align:top;">
        <asp:ListView ID="ListViewAccessori_read" runat="server" DataSourceID="sql_Acessori" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                       <td>
                         <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si'/>
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("assente") %>' />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                            <asp:CheckBox ID="ck_da_addebitare" runat="server"/>
                       </td>
                       <td>
                            <asp:Label ID="lb_motivo_non_addebito" runat="server" Text='<%# Eval("motivo_non_addebito") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownNonAddebito" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>
                       </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                         <asp:Label ID="lb_id_accessorio" runat="server" Text='<%# Eval("id_accessorio") %>' Visible="false" />
                         <asp:Label ID="Label13" runat="server" Text='Si' />
                       </td>
                       <td>
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                       </td>
                       <td align="center" >
                            <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("assente") %>' />
                       </td>
                       <td align="center" >
                            <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                            <asp:CheckBox ID="ck_da_addebitare" runat="server"/>
                       </td>
                       <td>
                            <asp:Label ID="lb_motivo_non_addebito" runat="server" Text='<%# Eval("motivo_non_addebito") %>' Visible="false" />
                            <asp:DropDownList ID="DropDownNonAddebito" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                            </asp:DropDownList>
                       </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun acessorio previsto.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Disponibile al Check Out</th>
                                      <th>
                                          Descrizione</th>
                                      <th>
                                          Assente al Check In</th>
                                      <th>
                                          Da Addebitare</th>
                                      <th>Motivo non addebito</th>
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
<div id="div_da_addebitare_D" runat="server" visible="False">
	<br />
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table13">
        <tr id="Tr18" runat="server">
            <td id="Td21" align="center" runat="server" >
                <asp:Button ID="bt_salva_da_addebitare" runat="server" Text="Salva Se Da Addebitare" />
            </td>
        </tr>
    </table>
</div>     
</div>

            </ContentTemplate>
      </ajaxtoolkit:TabPanel>

      <ajaxtoolkit:TabPanel ID="tab_meccanici" runat="server" HeaderText="Danni Meccanici/Elettrici">
            <HeaderTemplate>Danni Meccanici/Elettrici/Altro</HeaderTemplate>
            <ContentTemplate>

<div id="div_meccanici_elettrici" runat="server" style="position:relative;" >
<div id="div_danno_meccanico" visible="true">
<table width="100%" cellpadding="0" cellspacing="0">
 <tr>
    <td>
        <asp:Label ID="Label16" runat="server" Text="Tipo Guasto:" CssClass="testo_bold"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="DropDownTipoRecordDanno" runat="server">
            <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            <asp:ListItem Value="2">Danno Meccanico</asp:ListItem>
            <asp:ListItem Value="3">Danno Elettrico</asp:ListItem>
            <asp:ListItem Value="7">Altro</asp:ListItem>
        </asp:DropDownList>
    </td>
 </tr>
 <tr>
    <td valign="top">
        <asp:Label ID="Label18" runat="server" Text="Descrizione:" CssClass="testo_bold"></asp:Label>       
    </td>
    <td>
        <asp:TextBox ID="tx_descrizione_meccanico" runat="server" Width="400px" Height="165px" TextMode="MultiLine"></asp:TextBox>
    </td>
    </tr>  
 <tr>
    <td colspan="2" align="center">
        <asp:Button ID="bt_salva_danno_meccanico" runat="server" Text="Salva" ValidationGroup="Danno_Meccanico" />
    </td>
  </tr>  
</table>   
</div>

<div id="div_upload_meccanici" runat="server" visible="False">
<br />
<table width="100%" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="2">
    <asp:ListView ID="listViewDanniMeccanici" runat="server" 
              DataSourceID="sqlDocDanniMeccanci" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
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
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
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
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" target="_blank"><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
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
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Documento</th>
                                      <th>
                                          Nome</th>
                                      <th>
                                          </th>
                                      <th>
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
   <td>
        <asp:Label ID="Label17" runat="server" Text="Allega documento:" CssClass="testo_bold"></asp:Label>
    </td>
    <td>
        <asp:FileUpload ID="FileUploadMeccanici" size="38" runat="server" />
    </td>
   </tr>
   <tr>
    <td>
        <asp:Label ID="Label21" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="DropDownTipoDocMeccanico" runat="server" AppendDataBoundItems="True" 
		          DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
	        <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
        </asp:DropDownList> 
        <asp:Button ID="bt_salva_doc_meccanico" runat="server" Text="Salvataggio" 
            ValidationGroup="Upload_Danno_Meccanico" style="height: 26px" />
    </td>
   </tr>
</table>
</div>
<div id="div_da_addebitare_M" runat="server" visible="False">
	<br /><br />
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table12">
        <tr id="Tr13" runat="server">
            <td id="Td18" runat="server" >
                    <asp:Label ID="Label24" runat="server" Text="Motivo Non Addebito:" CssClass="testo_bold"></asp:Label>
            </td>
            <td id="Td22" runat="server" >
                <asp:DropDownList ID="DropDownNonAddebito_M" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="Sql_non_addebito" DataTextField="descrizione" DataValueField="id" >
                    <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td id="Td20" align="center" runat="server" >
                <asp:Button ID="bt_da_addebitare_M" runat="server" Text="Da Addebitare" />
                <asp:Button ID="bt_da_non_addebitare_M" runat="server" Text="Da Non Addebitare" ValidationGroup="Non_Addebito_Meccanico" />
            </td>
        </tr>
    </table>
</div>
</div>

<asp:ValidationSummary ID="ValidationSummary5" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Danno_Meccanico" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
    ControlToValidate="tx_descrizione_meccanico" ErrorMessage="Specificare una descrizione del guasto." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Danno_Meccanico"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator9" runat="server" 
    ControlToValidate="DropDownTipoRecordDanno" ErrorMessage="Specificare il tipo di guasto."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Danno_Meccanico" ></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary6" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Upload_Danno_Meccanico" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
    ControlToValidate="FileUploadMeccanici" ErrorMessage="Nessun documento selezionato." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Upload_Danno_Meccanico"></asp:RequiredFieldValidator> 

<asp:CompareValidator ID="CompareValidator10" runat="server" 
    ControlToValidate="DropDownTipoDocMeccanico" ErrorMessage="Specificare il tipo del documento."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Upload_Danno_Meccanico" ></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary7" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Non_Addebito_Meccanico" />

<asp:CompareValidator ID="CompareValidator11" runat="server" 
    ControlToValidate="DropDownNonAddebito_M" ErrorMessage="Specificare il motivo di non addebito del danno."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Non_Addebito_Meccanico" ></asp:CompareValidator>

            </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      
 </ajaxtoolkit:TabContainer>  

 </div>

<asp:Label ID="lb_id_evento" runat="server" Text='0' Visible="true" />
<asp:Label ID="lb_id_veicolo" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_danno" runat="server" Text='0' Visible="true" />

<asp:Label ID="lb_id_posizione_danno" runat="server" Text='0' Visible="true" />


<asp:Label ID="lb_id_ditta" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_gruppo_evento" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_tipo_documento" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_documento" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_a_carico_del_cliente" runat="server" Text='2' Visible="false" />

<asp:Label ID="lb_id_danno_meccanico" runat="server" Text='0' Visible="False" />

<asp:Label ID="lb_disabilita_mappa" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_stato_rds" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_attesa_rds" runat="server" Text='false' Visible="false" />



<asp:SqlDataSource ID="sqlTipoDanno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_tipo_danno] WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlTipoDocumentoImg" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_danni_img_tipo_documenti] WITH(NOLOCK) WHERE tipo = 1 ORDER BY ordine">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlPosizioneDanno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [veicoli_posizione_danno] WITH(NOLOCK) ORDER BY [descrizione]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDocumenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT vdf.*, td.descrizione des_tipo_documento
            FROM [veicoli_danni_foto] vdf WITH(NOLOCK) 
            INNER JOIN veicoli_danni_img_tipo_documenti td WITH(NOLOCK) ON td.id = vdf.tipo_documento 
            WHERE vdf.id_danno = @id_danno ORDER BY td.ordine, vdf.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_danno" Name="id_danno" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDocDanniMeccanci" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT vdf.*, td.descrizione des_tipo_documento
            FROM [veicoli_danni_foto] vdf WITH(NOLOCK) 
            INNER JOIN veicoli_danni_img_tipo_documenti td WITH(NOLOCK) ON td.id = vdf.tipo_documento 
            WHERE vdf.id_danno = @id_danno ORDER BY td.ordine, vdf.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_danno_meccanico" Name="id_danno" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>




<asp:SqlDataSource ID="sqlDanniDotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT a.* 
            FROM accessori_veicoli av WITH(NOLOCK)
            INNER JOIN accessori a WITH(NOLOCK) ON av.id_accessorio = a.id
            LEFT JOIN veicoli_danni d WITH(NOLOCK) ON d.id_veicolo = av.id_veicolo 
			            AND d.attivo = 1 AND d.stato = 1 
			            AND d.tipo_record = 4 AND d.id_dotazione = av.id_accessorio
            WHERE av.id_veicolo = @id_veicolo
            AND d.id IS NULL">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_veicolo" Name="id_veicolo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDanniAcessori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ce.id, ce.descrizione 
            FROM contratti_costi cc WITH(NOLOCK)
            INNER JOIN contratti c WITH(NOLOCK) ON c.id = cc.id_documento 
            INNER JOIN condizioni_elementi ce WITH(NOLOCK) ON cc.id_elemento = ce.id 
            WHERE (c.num_contratto = @id_documento AND c.attivo = '1') 
            AND (cc.id_a_carico_di = @id_a_carico_del_cliente) 
            AND (cc.obbligatorio = '0') 
            AND (cc.selezionato = '1')
            AND (ce.accessorio_check = '1')">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_documento" Name="id_documento" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_id_a_carico_del_cliente" Name="id_a_carico_del_cliente" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_Dotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ga.id, ga.assente, ga.id_accessorio, a.descrizione, a.descrizione_ing, ISNULL(d.da_addebitare,0) da_addebitare, ISNULL(d.motivo_non_addebito,0) motivo_non_addebito, ISNULL(d.id_dotazione,0) dotazione
            FROM veicoli_gruppo_accessori ga WITH(NOLOCK) 
            INNER JOIN accessori a WITH(NOLOCK) ON a.id = ga.id_accessorio
            LEFT JOIN veicoli_danni d ON d.id_evento_apertura = @id_evento AND d.tipo_record = 4 AND d.id_dotazione = ga.id_accessorio
            WHERE ga.id_evento_apertura = @id_gruppo_evento
            ORDER BY ga.assente, d.id_dotazione desc, a.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_Acessori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT ga.id, ga.id_evento_apertura, ga.id_veicolo, ga.id_accessorio, ISNULL(d.da_addebitare,0) da_addebitare, ISNULL(d.motivo_non_addebito,0) motivo_non_addebito, ga.assente, ce.descrizione
            FROM veicoli_gruppo_accessori_contratto ga WITH(NOLOCK) 
            INNER JOIN condizioni_elementi ce WITH(NOLOCK) ON ga.id_accessorio = ce.id 
            LEFT JOIN veicoli_danni d ON d.id_evento_apertura = @id_evento AND d.tipo_record = 5 AND d.id_acessori = ga.id_accessorio
            WHERE ga.id_evento_apertura = @id_gruppo_evento
            ORDER BY ga.assente, ce.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_evento" Name="id_gruppo_evento" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="Sql_non_addebito" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_motivo_non_addebito_rds] WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>