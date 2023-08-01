<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="anagrafica_operatori.aspx.vb" Inherits="anagrafica_operatori" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />

    <link rel="StyleSheet" type="text/css" href="css/style.css" /> 


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
        </style>  

    <style type="text/css" >
    .style1
    {
        width: 349px;
    }
    .style3
    {
    }
    .style7
    {
        width: 125px;
    }
    .style10
    {
        width: 270px;
    }
    .style13
    {
        width: 129px;
    }
    .style17
    {
    }
    .style18
    {
        width: 52px;
    }
    .style19
    {
        width: 69px;
    }
    .style24
    {
        width: 32px;
    }
    .style25
    {
        width: 65px;
    }
    .style26
    {
        width: 56px;
        float: left;
    }
    .style28
    {
        width: 68px;
    }
    .style29
    {
        width: 167px;
    }
    .style30
    {
        width: 187px;
    }
    .style31
    {
        width: 66px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    



<table border="0" cellpadding="0" cellspacing="0" width="1024px" runat="server" id="tab_titolo">
  <tr>
    <td colspan="8" align="left" style="background-color:#444;" class="testo_bold_bianco">
      <b>&nbsp;Operatori</b>
    </td>
  </tr>
</table>

<div id="cerca_operatore" runat="server" visible="true">
<table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0" runat="server" id="table1">
  <tr>
    <td class="style18">
      <asp:Label ID="lblCercaNome" runat="server" Text="Nome:" 
            CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style30">
      <asp:TextBox ID="txtCercaNome" runat="server" Width="150px"></asp:TextBox>&nbsp;</td>
      <td class="style25">
      <asp:Label ID="lblCercaCognome" runat="server" Text="Cognome:" 
              CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style29">
      <asp:TextBox ID="txtCercaCognome" runat="server" Width="150px"></asp:TextBox>
      </td>
      <td class="style28">
      <asp:Label ID="lblCercaEmail" runat="server" Text="Email:" 
              CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style19">
          <asp:TextBox ID="txtCercaEmail" runat="server" Width="150px"></asp:TextBox>
      </td>
      <td class="style24">
          &nbsp;</td>
      <td class="style31">
      <asp:Label ID="lblCercaUsername" runat="server" Text="Username:" 
              CssClass="testo_bold"></asp:Label>
      </td>
        <td align="right" class="style26">
          <asp:TextBox ID="txtCercaUsername" runat="server" Width="150px"></asp:TextBox>
    </td>
      
      
  </tr>
  <tr>
    <td class="style18">
      <asp:Label ID="lblCercaStazione" runat="server" Text="Stazione:" 
              CssClass="testo_bold"></asp:Label>
      </td>
    <td class="style30">
          <asp:DropDownList ID="dropCercaStazione" runat="server" 
              DataSourceID="SqlStazioni" DataTextField="stazione" 
              DataValueField="id" Height="22px" Width="160px" 
            AppendDataBoundItems="True">
              <asp:ListItem Selected="True" Value="-1">...</asp:ListItem>
          </asp:DropDownList>
      </td>
      <td>
        <asp:Label ID="lblZonaFiltro0" runat="server" Text="Attivi" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style11">    
        <asp:DropDownList runat="server" AppendDataBoundItems="True" DataTextField="nome" ID="dropAttive">
            <asp:ListItem Selected="False" Value="-1">Tutti</asp:ListItem>
            <asp:ListItem Selected="True" Value="1" >Si</asp:ListItem>
            <asp:ListItem Selected="False" Value="0">No</asp:ListItem>
        </asp:DropDownList>
        </td>      
        <td align="right" class="style31">
            <asp:Button 
                ID="btnCerca" runat="server" Text="Cerca" style="height: 26px; float: right;" 
                UseSubmitBehavior="False" />
      </td>
      
      
      <td>
          <asp:Button ID="btnNuovoOperatore" runat="server" Text="Nuovo operatore" 
                CommandName="NuovoOperatore" UseSubmitBehavior="False" Height="28px"/>
      </td>
      
      
  </tr>
</table>
<br />
</div>

<div id="risultati" runat="server" visible="false">
<table cellpadding="0" cellspacing="2" width="1024px" border="0" runat="server" id="table2" >
  <tr>
    <td>
      
        <asp:ListView ID="listConducenti" runat="server" DataSourceID="SqlOperatori" 
            DataKeyNames="id" EnableModelValidation="True">
            <ItemTemplate>
                <tr style="background-color:#DCDCDC; color: #000000;" class="testo_bold_nero ">
                   <td align="center"> 
                       <asp:Label ID="lblAttivo" runat="server" Text=""></asp:Label>
                   </td>
                   <td>
                        <asp:Label ID="Id" runat="server" Text='<%# Eval("id") %>' Visible="False" />
                        <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                    </td>
                    <td>
                        <asp:Label ID="cognomeLabel" runat="server" Text='<%# Eval("cognome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("username") %>' />
                    </td>
                    <td>
                        <asp:Label ID="stazioneLabel" runat="server" Text='<%# Eval("id_stazione") %>' />
                    </td>
                    <td>
                        <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("mail") %>' />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="ModificaOperatore" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="ModificaOperatore"/>
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="DuplicaOperatore" runat="server" ImageUrl="/images/duplica.png" style="width: 16px" CommandName="DuplicaOperatore"/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="" class="testo_bold_nero ">
                    <td align="center"> 
                        <asp:Label ID="lblAttivo" runat="server" Text="Label"></asp:Label>
                   </td>
                    <td>                        
                        <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                    </td>
                    <td>
                        <asp:Label ID="cognomeLabel" runat="server" Text='<%# Eval("cognome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("username") %>' />
                    </td>
                    <td>
                        <asp:Label ID="stazioneLabel" runat="server" Text='<%# Eval("id_stazione") %>' />
                    </td>
                    <td>
                        <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("mail") %>' />
                    </td>
                    <td align="center" width="40px">
                        <asp:ImageButton ID="ModificaOperatore" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="ModificaOperatore"/>
                    </td>
                   <td align="center" width="40px">
                        <asp:ImageButton ID="DuplicaOperatore" runat="server" ImageUrl="/images/duplica.png" style="width: 16px" CommandName="DuplicaOperatore"/>
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
                    <tr id="Tr1" runat="server" class="testo_bold_nero">
                        <td id="Td1" runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                    <th id="Th6" runat="server" align="left">  
                                        Attivo                                      
                                    </th>
                                    <th id="Th1" runat="server" align="left">
                                        Nome</th>
                                    <th id="Th2" runat="server" align="left">
                                        Cognome</th>
                                    <th id="Th3" runat="server" align="left">
                                        Username</th>                                    
                                    <th id="Th7" runat="server" align="left">
                                        Stazione</th>
                                    <th id="Th4" runat="server" align="left">
                                        Email</th>
                                    <th id="Th5" runat="server" align="left">
                                        </th>
                                    <th id="Th8" runat="server" align="left">
                                        </th>                                    
                                </tr>
                                <tr ID="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td2" runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" Visible="True" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" Visible="True" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <EditItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                            Text="Aggiorna" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                            Text="Annulla" />
                    </td>
                    <td>
                        <asp:TextBox ID="nomeTextBox" runat="server" 
                            Text='<%# Bind("nome") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="cognomeTextBox" runat="server" Text='<%# Bind("cognome") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="usernameTextBox" runat="server" 
                            Text='<%# Bind("username") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("mail") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="stazioneTextBox" runat="server" Text='<%# Bind("id_stazione") %>' />
                    </td>
                </tr>
            </EditItemTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <td>
                        <asp:Label ID="nomeLabel" runat="server" Text='<%# Eval("nome") %>' />
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                    </td>
                    <td>
                        <asp:Label ID="cognomeLabel" runat="server" Text='<%# Eval("cognome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("username") %>' />
                    </td>
                    <td>
                        <asp:Label ID="stazioneLabel" runat="server" Text='<%# Eval("id_stazione") %>' />
                    </td>
                    <td>
                        <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("mail") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
      
    </td>
  </tr>
</table>
<br />
</div>

<div id="nuovo_operatore" runat="server" visible="false">
<table cellpadding="0" cellspacing="2" width="1024px" border="0" runat="server" id="table3">
  <tr>
    <td>
      <div id="form_operatore" runat="server" visible="false">
      <table cellpadding="2" cellspacing="2" width="100%" border="1">
        <tr>
          <td class="style13">
            <asp:Label ID="lblCognome" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtCognome" runat="server" MaxLength="50"></asp:TextBox>
          </td>
          <td class="style7">
            <asp:Label ID="lblNome" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="50"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="style13">
            <asp:Label ID="lblDataRilascioPatente" runat="server" Text="Username:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtusername" runat="server" Rows="30"></asp:TextBox>
          </td>
          <td class="style7">
            <asp:Label ID="lblLuogoEmissionePatente" runat="server" Text="Password:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtPassword" runat="server" Rows="30"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="style13">
            <asp:Label ID="lblAltriDocumenti" runat="server" Text="Email:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtEmail" runat="server" Rows="30"></asp:TextBox>
            &nbsp;
          </td>
          <td class="style7">
            <asp:Label ID="lblConvenzione" runat="server" Text="Stazione:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:DropDownList ID="listStazioni" runat="server" DataSourceID="SqlStazioni" 
                  DataTextField="stazione" DataValueField="id" 
                  AppendDataBoundItems="True" CausesValidation="True">
                <asp:ListItem Value="-1" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
          &nbsp;</td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="Label1" runat="server" Text="Attivo:" CssClass="testo_bold"></asp:Label>
          </td>
          <td colspan="3">
              <asp:CheckBox ID="chkBxAttivo" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="4" align="center">
            <asp:Button ID="btnSeleziona" runat="server" Text="Modifica e seleziona" 
                  ValidationGroup="invia" Visible="false" UseSubmitBehavior="False" />
               &nbsp;
            <asp:Button ID="btnSalva" runat="server" Text="Salva operatore" 
                  ValidationGroup="invia" UseSubmitBehavior="False" />&nbsp;&nbsp;<asp:Button 
                  ID="btnAnnulla" runat="server" Text="Annulla" UseSubmitBehavior="False" 
                  CausesValidation="False" />
          </td>
        </tr>
      </table>
      </div>
    </td>
  </tr>
</table>
</div>

<div id="duplica" runat="server" visible="false">
<table cellpadding="0" cellspacing="2" width="1024px" border="0" runat="server" id="table4">
  <tr>
     <td>
        <asp:Label ID="lblIdDuplicatoreScelto" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Verrano Duplicati i permessi di: "></asp:Label>
        <asp:Label ID="lblNominativo" runat="server" Text="Nominativo"></asp:Label>
     </td>
  </tr>
  <tr>
    <td>
      <div id="form_duplica" runat="server" visible="false">
      <table cellpadding="2" cellspacing="2" width="100%" border="1">
        <tr>
          <td class="style13">
            <asp:Label ID="lblCognomeduplica_operatore" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtCognomeduplica_operatore" runat="server" MaxLength="50"></asp:TextBox>
          </td>
          <td class="style7">
            <asp:Label ID="lblNomeduplica_operatore" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtNomeduplica_operatore" runat="server" MaxLength="50"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="style13">
            <asp:Label ID="lblUsrduplica_operatore" runat="server" Text="Username:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtusernameduplica_operatore" runat="server" Rows="30"></asp:TextBox>
          </td>
          <td class="style7">
            <asp:Label ID="lblPwdduplica_operatore" runat="server" Text="Password:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtPasswordduplica_operatore" runat="server" Rows="30"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="style13">
            <asp:Label ID="lblemailduplica_operatore" runat="server" Text="Email:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style10">
            <asp:TextBox ID="txtEmailduplica_operatore" runat="server" Rows="30"></asp:TextBox>
            &nbsp;
          </td>
          <td class="style7">
            <asp:Label ID="lblStazioneduplica_operatore" runat="server" Text="Stazione:" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td>
            <asp:DropDownList ID="listStazioniduplica_operatore" runat="server" DataSourceID="SqlStazioni" 
                  DataTextField="stazione" DataValueField="id" 
                  AppendDataBoundItems="True" CausesValidation="True">
                <asp:ListItem Value="-1" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
          &nbsp;</td>
        </tr>        
        <tr>
          <td colspan="4" align="center">            
            <asp:Button ID="btnSalvaduplica_operatore" runat="server" Text="Duplica operatore" 
                  ValidationGroup="invia" UseSubmitBehavior="False" />&nbsp;&nbsp;
	        <asp:Button 
                  ID="btnAnnullaDuplica" runat="server" Text="Annulla" UseSubmitBehavior="False" 
                  CausesValidation="False" />
          </td>
        </tr>
      </table>          
      </div>
    </td>
  </tr>
</table>
</div>



              <asp:SqlDataSource ID="SqlStazioni" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
                  
        SelectCommand="SELECT id, (STR(codice) + ' - ' + nome_stazione) As stazione FROM [stazioni] ORDER BY [codice]">
              </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOperatori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT TOP 100 id, nome,cognome,username,password,id_stazione,mail FROM operatori  WITH(NOLOCK)">
    </asp:SqlDataSource>
<asp:Label ID="query" runat="server" 
        Text="SELECT TOP 100 id, nome, cognome, username, password, id_stazione,mail FROM operatori WITH(NOLOCK) WHERE  id>0 ORDER BY Cognome" 
        Visible="False"></asp:Label>
<asp:Label ID="id_operatore" runat="server" Visible="False"></asp:Label>

 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
    ControlToValidate="txtCognome" 
    ErrorMessage="Specificare campo Cognome" Font-Size="0pt" 
    SetFocusOnError="False" ValidationGroup="invia"></asp:RequiredFieldValidator>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
    ControlToValidate="txtNome" 
    ErrorMessage="Specificare campo nome" Font-Size="0pt" 
    SetFocusOnError="False" ValidationGroup="invia"></asp:RequiredFieldValidator>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
    ControlToValidate="txtusername" 
    ErrorMessage="Specificare campo username" Font-Size="0pt" 
    SetFocusOnError="False" ValidationGroup="invia"></asp:RequiredFieldValidator>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
    ControlToValidate="txtPassword" 
    ErrorMessage="Specificare campo password" Font-Size="0pt" 
    SetFocusOnError="False" ValidationGroup="invia"></asp:RequiredFieldValidator>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
    ControlToValidate="txtEmail" 
    ErrorMessage="Specificare campo email" Font-Size="0pt" 
    SetFocusOnError="False" ValidationGroup="invia"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ControlToValidate="listStazioni" ErrorMessage="Specificare stazione" 
        ValueToCompare="-1" Font-Size="0pt" Operator="NotEqual" Type="Integer" 
        ValidationGroup="invia"></asp:CompareValidator>
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="invia" />
    <asp:Label ID="id_staz" runat="server" Visible="False"></asp:Label>
</asp:Content>
    
    



