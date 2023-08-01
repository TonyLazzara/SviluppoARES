<%@ Page Title="ARES - Condizioni" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="lista_condizioni.aspx.vb" Inherits="lista_condizioni" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 

    <style type="text/css">
        .style2
        {
            width: 183px;
        }
        .style3
        {
            width: 107px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style4
        {
            width: 180px;
        }
        .style5
        {
            width: 20%;
        }
        .style6
        {
        }
        .style7
        {
        }
        .style9
        {
            width: 199px;
        }
        .style10
        {
            width: 253px;
        }
        .style13
        {
            width: 198px;
        }
        .style14
        {
            width: 109px;
        }
        .style16
        {
            width: 78px;
        }
        .style18
        {
            width: 102px;
        }

        .applicabilita{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size:12px;
            font-weight:bold;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   


    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Condizioni</b>
           </td>
         </tr>
    </table>
    <div id="tab_cerca" runat="server">
    <table border="0" cellpadding="2" cellspacing="0" width="1024px" runat="server" style="border: 4px solid #444">
    <tr>
      <td class="style2">
        <asp:Label ID="lblCodiceCerca" runat="server" Text="Codice" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style3">
         <asp:Label ID="lblRiferimentoDaCerca" runat="server" Text="Riferimento da" CssClass="testo_bold"></asp:Label>
      </td>
      <td colspan="2">
         <asp:Label ID="lblRiferimentoACerca" runat="server" Text="Riferimento A" CssClass="testo_bold"></asp:Label>
      </td>
    </tr>
     <tr>
     <td class="style2">
        
         <asp:TextBox ID="txtCodice" runat="server"></asp:TextBox>
        
      </td>
      <td class="style3">
            <a onclick="Calendar.show(document.getElementById('<%=txtCercaValidoDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtCercaValidoDa" runat="server" Width="70px"></asp:TextBox></a>

              <%-- <asp:CalendarExtender ID="txtCercaValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtCercaValidoDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtCercaValidoDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtCercaValidoDa">
                </asp:MaskedEditExtender>    

      </td>
      <td>
          <a onclick="Calendar.show(document.getElementById('<%=txtCercaValidoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtCercaValidoA" runat="server" Width="70px"></asp:TextBox></a>
             <%--  <asp:CalendarExtender ID="txtCercaValidoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtCercaValidoA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtCercaValidoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtCercaValidoA">
                </asp:MaskedEditExtender>    
                            
      &nbsp;&nbsp;
            
            <asp:Button ID="btnCerca" runat="server" Text="Cerca" />  
                         
           
           
            &nbsp;&nbsp;
            
                       
           
      </td>
      <td align="right">

            <asp:Button ID="btnNuovo" runat="server" Text="Nuova condizione" />  
       
      </td>
    </tr>
  </table>
  <table width="1024px">
     <tr> 
      <td colspan="4">
        <asp:Label ID="lbl_errore_sup" runat="server" Font-Bold="True" ForeColor="Red" Text="" ></asp:Label>
          <br />&nbsp;
          <asp:ListView ID="listCondizioni" runat="server" DataKeyNames="id" DataSourceID="sqlCondizioni">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC;color: #000000;">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="iva_inclusa" runat="server" Text='<%# Eval("iva_inclusa") %>' Visible="false" />
                          <asp:TextBox ID="codiceText" runat="server" Text='<%# Eval("codice") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="codiceText" ErrorMessage="Specificare il codice del tempo+km." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                          <asp:Label ID="id_template_val" runat="server" Text='<%# Eval("id_template_val") %>' Visible="false" />
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' Visible="false" />
                      </td>
                      <td>         
                           <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' Visible="false" />     
                          <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_daText").ClientID %>'), '%d/%m/%Y', false)"> 
                           <asp:TextBox ID="valido_daText" runat="server" Width="70px" Text='<%# Eval("valido_da") %>'></asp:TextBox></a>
                          <%-- <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_daText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_daText">
                            </asp:MaskedEditExtender>    
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="valido_daText" ErrorMessage="Specificare il campo 'Riferimento da'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                ControlToValidate="valido_daText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento da'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
      
                      </td>
                      <td>
                           <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' Visible="false" />
                           <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_aText").ClientID %>'), '%d/%m/%Y', false)"> 
                           <asp:TextBox ID="valido_aText" runat="server" Width="70px" Text='<%# Eval("valido_a") %>'></asp:TextBox></a>
                          <%-- <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_aText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_aText">
                            </asp:MaskedEditExtender> 
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="valido_aText" ErrorMessage="Specificare il campo 'Riferimento a'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="valido_aText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento a'." Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareFieldValidator2" runat="server"
                                 ControlToValidate="valido_aText"
                                 ControlToCompare="valido_daText"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale è precedente alla data iniziale."
                                 ValidationGroup="invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>
                      </td>
                      <td>
                          <asp:Label ID="data_creazioneLabel" runat="server" Text='<%# Eval("data_creazione") %>' />
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnDuplica" runat="server" ImageUrl="/images/aggiorna.png" CommandName="duplica" ToolTip="Duplica" />
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/salva.png" CommandName="modifica" ValidationGroup="invia_lista" />
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="iva_inclusa" runat="server" Text='<%# Eval("iva_inclusa") %>' Visible="false" />
                          <asp:TextBox ID="codiceText" runat="server" Text='<%# Eval("codice") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                               ControlToValidate="codiceText" ErrorMessage="Specificare il codice del tempo+km." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                          <asp:Label ID="id_template_val" runat="server" Text='<%# Eval("id_template_val") %>' Visible="false" />
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' Visible="false" />
                      </td>
                     <td>         
                           <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' 
                               Visible="false" />               
                          <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_daText").ClientID %>'), '%d/%m/%Y', false)"> 
                           <asp:TextBox ID="valido_daText" runat="server" Width="70px" 
                               Text='<%# Eval("valido_da") %>'></asp:TextBox></a>
                         <%--  <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_daText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_daText">
                            </asp:MaskedEditExtender>    
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                               ControlToValidate="valido_daText" ErrorMessage="Specificare il campo 'Riferimento da'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator15" runat="server" 
                                ControlToValidate="valido_daText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                
                               ErrorMessage="Specificare un valore corretto per il campo 'Riferimento da'." 
                               Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
      
                      </td>
                      <td>
                           <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' 
                               Visible="false" />
                           <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_aText").ClientID %>'), '%d/%m/%Y', false)"> 
                           <asp:TextBox ID="valido_aText" runat="server" Width="70px" 
                               Text='<%# Eval("valido_a") %>'></asp:TextBox></a>
                          <%-- <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_aText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_aText">
                            </asp:MaskedEditExtender> 
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                               ControlToValidate="valido_aText" ErrorMessage="Specificare il campo 'Riferimento a'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator16" runat="server" 
                                ControlToValidate="valido_aText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                
                               ErrorMessage="Specificare un valore corretto per il campo 'Riferimento a'." 
                               Type="Date" ValidationGroup="invia_lista"> </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareFieldValidator3" runat="server"
                                 ControlToValidate="valido_aText"
                                 ControlToCompare="valido_daText"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale è precedente alla data iniziale."
                                 ValidationGroup = "invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>
                      </td>
                      <td>
                          <asp:Label ID="data_creazioneLabel" runat="server" Text='<%# Eval("data_creazione") %>' />
                      </td>
                      <td align="center">
                          <asp:ImageButton ID="btnDuplica" runat="server" ImageUrl="/images/aggiorna.png" CommandName="duplica" ToolTip="Duplica" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" 
                                  CommandName="vedi" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/salva.png" 
                                  CommandName="modifica" ValidationGroup="invia_lista" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" 
                                  CommandName="elimina" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table runat="server" style="">
                      <tr>
                          <td>
                              Nessuna Condizione memorizzato.</td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table runat="server" width="100%">
                      <tr runat="server">
                          <td runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" class="testo_bold_nero"
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                  <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                      <th runat="server" align="left">
                                          Codice</th>
                                      <th runat="server" align="left">
                                          Riferimento da</th>
                                      <th runat="server" align="left">
                                          Riferimento a</th>
                                      <th runat="server" align="left">
                                          Data creazione</th>
                                      <th></th>
                                      <th></th>
                                      <th></th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr runat="server">
                          <td runat="server" style="" align="center">
                              <asp:DataPager ID="DataPager1" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowLastPageButton="True" />
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
<asp:Panel ID="tab_vedi" runat="server" Visible="false">
    <table border="1" cellpadding="2" cellspacing="2" width="1024px" >
         <tr>
           <td align="left" class="style10">
             <asp:Label ID="Label1" runat="server" Text="Codice" CssClass="testo_bold"></asp:Label>
               &nbsp;
               <asp:TextBox ID="lblCodice" runat="server"></asp:TextBox>
           </td>
           <td align="left" class="style13">
               <asp:Label ID="Label2" runat="server" Text="Riferimento da" CssClass="testo_bold"></asp:Label>
               &nbsp;
               <a onclick="Calendar.show(document.getElementById('<%=txtValidoDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtValidoDa" runat="server" Width="70px"></asp:TextBox></a>
              <%-- <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="txtValidoDa">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="txtValidoDa">
               </asp:MaskedEditExtender>
             </td>
           <td align="left" class="style13">
               <asp:Label ID="Label3" runat="server" Text="Riferimento a" CssClass="testo_bold"></asp:Label>&nbsp;
               <a onclick="Calendar.show(document.getElementById('<%=txtValidoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtValidoA" runat="server" Width="70px"></asp:TextBox></a>
             <%--  <asp:CalendarExtender ID="txtValidoA_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="txtValidoA">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="txtValidoA_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="txtValidoA">
               </asp:MaskedEditExtender>
               &nbsp;
           </td>
           <td align="left" >
        
               <asp:Label ID="Label4" runat="server" CssClass="testo_bold" Text="VAL"></asp:Label>
               &nbsp;<asp:DropDownList ID="dropTemplateVal" runat="server" AutoPostBack="True" 
                   AppendDataBoundItems="True" DataSourceID="sqlTemplateVAL" 
                   DataTextField="descrizione" DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
               
               &nbsp;<asp:Button ID="btnModificaVAL" runat="server" Text="Modifica VAL"  OnClientClick="javascript: return(window.confirm ('La modifica del tipo di val comporterà l\'eliminazione degli accessori val attualmente assegnati alla condizione. \n\nSei sicuro di voler procedere?'));" Visible="false" />
             </td> 
          
         </tr>
         <tr>
             <td align="left" class="style10">
                 <asp:Label ID="Label8" runat="server" CssClass="testo_bold" Text="Iva:"></asp:Label>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="dropIva" runat="server" AppendDataBoundItems="True">
                     <asp:ListItem Selected="True" Value="1">Inclusa</asp:ListItem>
                     <asp:ListItem Value="0">Esclusa</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td align="left" class="style13">
                 &nbsp;</td>
             <td align="left" class="style13">
                 &nbsp;</td>
             <td align="left">
                 &nbsp;</td>
         </tr>
         <tr>
             <td align="center" colspan="4">
                 <asp:Button ID="btnModificaIntestazione" runat="server" style="height: 26px" Text="Modifica Intestazione" ValidationGroup="invia" Visible="false" />
             </td>
         </tr>
         </table>
         <table border="1" cellpadding="2" cellspacing="2" width="1024px" >
         <tr>
             <td align="center" class="style6" colspan="5">
             </td>
         </tr>
         <tr>
           <td class="style9" rowspan="2">
             <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
               <tr>
                 <td class="style14">
                    <asp:Label ID="Label5" runat="server" Text="Elemento:" CssClass="testo_bold"></asp:Label>
                 </td>
                 <td>
                    <asp:DropDownList ID="dropElementi" runat="server" AppendDataBoundItems="True" Width="242"
                       DataSourceID="sqlElementi" DataTextField="descrizione" DataValueField="id">
                       <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                 </td>
               </tr>
               <tr>
                 <td class="style14">
                   <asp:Label ID="Label6" runat="server" Text="Elemento VAL:" CssClass="testo_bold"></asp:Label>
                 </td>
                 <td>
                 
                     <asp:DropDownList ID="dropElementoVal" runat="server" 
                         AppendDataBoundItems="True" DataSourceID="SqlRigheVAL" 
                         DataTextField="descrizione" DataValueField="id_accessori_val">
                         <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                     </asp:DropDownList>
                 
                 </td>
               </tr>
               <tr>
                 <td class="style14">
                   <asp:Label ID="Label7" runat="server" Text="Regola stampa:" CssClass="testo_bold"></asp:Label>
                 </td>
                 <td>
                 
                     <asp:DropDownList ID="dropTipoStampa" runat="server" 
                         AppendDataBoundItems="True" DataSourceID="sqlTipoStampa" 
                         DataTextField="descrizione" DataValueField="id">
                         <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                     </asp:DropDownList>
                 
                 </td>
               </tr>
               
             </table>
              
               
           </td>
           <td class="style4" >
               <asp:Label ID="lblACaricoDiNuovo" runat="server" Text="A carico di:" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style18">
               <asp:Label ID="lblPackedNuovo" runat="server" Text="Packed:" CssClass="testo_bold"></asp:Label>
           </td>
             <td class="style16">
               <asp:Label ID="lblObbligatorioNuovo" runat="server" Text="Obbligatorio:" CssClass="testo_bold"></asp:Label>
             </td>
           <td class="style2" width="25%">
               <asp:Label ID="lblGruppoNuovo" runat="server" Text="Gruppo (ctrl per selezionare più gruppi):" CssClass="testo_bold"></asp:Label>
           </td>
         </tr>
         <tr>
           <td class="style4">
               
               <asp:DropDownList ID="dropACaricoDi" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlACaricoDi" DataTextField="descrizione" DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
               
           </td>
           <td class="style18">
               <asp:CheckBox ID="chkPacked" runat="server" />
           </td>
             <td class="style16">
                 <asp:CheckBox ID="chkObbligatorio" runat="server" />
             </td>
           <td rowspan="3">
            <asp:ListBox ID="ListBoxGruppiAuto" runat="server" DataSourceID="sqlGruppiAuto" 
                   DataTextField="cod_gruppo" DataValueField="id_gruppo" SelectionMode="Multiple" 
                   Width="202px"></asp:ListBox>   
             </td>
           
         </tr>
         <tr>
           <td class="style9">

              <table style="margin-left:-3px;">
                  <tr>
                      <td>
                          <asp:Label ID="lblApplicabilitaNuovo" runat="server" Text="Applicabilità:" CssClass="testo_bold"></asp:Label>
                      </td>
                      <td>
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                   <asp:CheckBox ID="chkSenzaLimite" runat="server" Text="Senza limite"  AutoPostBack="True" CssClass="testo_bold" />
                                   &nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Text="da" CssClass="testo_bold"></asp:Label>
                                   &nbsp;<asp:TextBox ID="txtApplicabilitaDa" runat="server" Width="29px"></asp:TextBox>
                                     <asp:RangeValidator 
                                         id="rangeval" 
                                         ControlToValidate="txtApplicabilitaDa" 
                                         MinimumValue="0"
                                         MaximumValue="999" 
                                         Type="Integer"  
                                         ErrorMessage="<br/>Valore compreso tra 0 e 999"                             
                                        text=""
                                        Display="Dynamic"
                                         ForeColor="red"                              
                                         runat="server" >
                                    </asp:RangeValidator>     

                                   &nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Text="a" CssClass="testo_bold"></asp:Label>
                                   &nbsp;<asp:TextBox ID="txtApplicabilitaA" runat="server" Width="29px" ></asp:TextBox>
                                     <asp:RangeValidator 
                                         id="RangeValidator1" 
                                         ControlToValidate="txtApplicabilitaA" 
                                         MinimumValue="0"
                                         MaximumValue="999" 
                                         Type="Integer"  
                                         ErrorMessage="<br/>Valore compreso tra 0 e 999"                             
                                        text=""
                                        Display="Dynamic"
                                         ForeColor="red"                              
                                         runat="server" >
                                    </asp:RangeValidator>  
                               </ContentTemplate>
                            </asp:UpdatePanel>
                      </td>
                  </tr>
              </table>  
              

               

           </td>
           <td class="style4">
              <asp:Label ID="lblUnitaDiMisuraNuovo" runat="server" Text="Unità di misura:" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style5" colspan="2">
             <asp:Label ID="lblCostoNuovo" runat="server" Text="Costo:" CssClass="testo_bold"></asp:Label>
           </td>
         </tr>
         <tr>
           <td valign="top" class="style9">
               <asp:Label ID="Label11" runat="server" Text="Periodo:" CssClass="testo_bold"></asp:Label>
               &nbsp;&nbsp;<asp:Label ID="Label12" runat="server" Text="da" CssClass="testo_bold"></asp:Label>

               &nbsp;<a onclick="Calendar.show(document.getElementById('<%=txt_periodo_da.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txt_periodo_da" runat="server" Width="70px"></asp:TextBox></a>              
                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txt_periodo_da">
                </asp:MaskedEditExtender>    

               &nbsp;<a onclick="Calendar.show(document.getElementById('<%=txt_periodo_a.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txt_periodo_a" runat="server" Width="70px" Visible="false"></asp:TextBox></a>              
                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txt_periodo_a">
                </asp:MaskedEditExtender>    
             </td>

           <td valign="top" class="style4">
               
               <asp:DropDownList ID="dropUnitaDiMisura" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlUnitaDiMisura" 
                   DataTextField="descrizione" DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
             </td>
           <td valign="top" class="style5" colspan="2">
               <asp:TextBox ID="txtCosto" runat="server" Width="80px" onKeyPress="return filterInputDouble(event)"></asp:TextBox>
               &nbsp;<asp:DropDownList ID="dropPerc" runat="server" 
                   AppendDataBoundItems="True">
                   <asp:ListItem Selected="True">€</asp:ListItem>
                   <asp:ListItem>%</asp:ListItem>
               </asp:DropDownList>
            </td>
           
         </tr>
         <tr>
             <td class="style7" valign="top" colspan="5" align="center">
                   <asp:Label ID="lbl_id_riga" runat="server" Text="" ></asp:Label>
                 <asp:Button ID="btnAggiungi" runat="server" style="height: 26px" Text="Aggiungi" />
                 &nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Annulla" style="height: 26px" />
                <%--<div align="right" runat="server" id="div_stampa"><a target="_blank" href="GeneraPdf.aspx?pagina=orizzontale&DocPdf=/stampe/stampa_condizione&id_cond=<%= lbl_id_codice.Text %>">Stampa</a></div>--%>
                 <asp:Button ID="btnStampa" runat="server" Text="Stampa" style="height: 26px" />
             </td>
         </tr>
 </table>

<%--Salvo aggiunta per visuali--%>
<table border="1" cellpadding="2" cellspacing="0" width="1024px" id="table_salvo" runat="server">
    <tr>
        <td><asp:Label ID="lbl_righe_visualizzazione" runat="server" Text="" ></asp:Label></td>
    </tr>
</table>
<%--end Salvo  aggiunta per visuali--%>

<%--Inizio righe--%>
 <table border="1" cellpadding="2" cellspacing="0" width="1024px" >
         <tr>
            <%-- <td style="background-color:White">
             &nbsp;
           </td>--%>
           <td style="background-color:White">
             &nbsp;
           </td>
           <td width="80px" style="background-color:White">
              <b>OBBLIG.</b>
           </td>
           <td style="background-color:White">
              <b>COSTO</b>
           </td>
           <td widht="20%" style="background-color:White">&nbsp;</td>
         </tr>
<% 
    'CONTROLLO SE E' STATA RICHIESTA L'ELIMINAZIONE DI UNA CONDIZIONE
    Dim i As Integer = 1

    For i = 1 To 50
        If Request.Form("elimina_" & i) <> "" Then
            elimina_condizione(i)
        End If
    Next

    'CONTROLLO SE E' STATA RICHIESTA L'ELIMINAZIONE DI UNA RIGA
    'RECUPERO TUTTI GLI id DI CONDIZIONI_RIGHE COLLEGATE ALLA CONDIZIONE CORRENTE. IL NOME DEL TASTO elimina_riga INFATTI CONTIENE
    'L'ID DELLA RIGA DA ELIMINARE
    Dim array_id(100) As String
    Dim array_idrec(100) As String
    array_id = get_id_condizione(lbl_id_codice.Text)


    i = 1

    Do While array_id(i) <> "000"
        If Request.Form("elimina_riga_" & array_id(i)) <> "" Then
            elimina_riga(array_id(i))
        End If
        If Request.Form("vedi_riga_" & array_id(i)) <> "" Then
            vedi_riga(array_id(i), "0")
        End If

        i = i + 1
    Loop


    Dim num_riga As String

    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    Dbc.Open()
    Dim sqlstr As String = "Select condizioni_righe.id As id_riga, condizioni_righe.periodo_da, condizioni_righe.periodo_a, condizioni_metodo_stampa.descrizione As stampa, condizioni_righe.tipo_costo, condizioni_righe.obbligatorio, condizioni_righe.num_riga, condizioni_righe.id_unita_misura, condizioni_righe.id_a_carico_di, condizioni_righe.id, condizioni_elementi.descrizione As elemento, elementi_val.descrizione As elemento_val, condizioni_a_carico_di.descrizione As a_carico_di, applicabilita_da, applicabilita_a, condizioni_unita_misura.descrizione As unita_di_misura, condizioni_righe.costo, condizioni_righe.pac FROM condizioni_righe With(NOLOCK) LEFT JOIN condizioni_elementi With(NOLOCK) On condizioni_righe.id_elemento= condizioni_elementi.id LEFT JOIN condizioni_a_carico_di With(NOLOCK) On condizioni_righe.id_a_carico_di=condizioni_a_carico_di.id LEFT JOIN condizioni_unita_misura With(NOLOCK) On condizioni_righe.id_unita_misura=condizioni_unita_misura.id LEFT JOIN condizioni_elementi As elementi_val With(NOLOCK) On condizioni_righe.id_elemento_val=elementi_val.id INNER JOIN condizioni_metodo_stampa With(NOLOCK) On condizioni_metodo_stampa.id=condizioni_righe.id_metodo_stampa "
    sqlstr += "WHERE (id_condizione='" & lbl_id_codice.Text & "') order by num_riga, applicabilita_da"

    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
    Dim Rs As Data.SqlClient.SqlDataReader
    Rs = Cmd.ExecuteReader()

    Dim continua As Boolean = Rs.Read()
    Dim continua_ciclo As Boolean

    Do While continua
        continua_ciclo = True

        num_riga = Rs("num_riga")

     %>
  
        <tr style="height:30px;">
           <%-- <td valign="top">
                vedi
                </td>--%>

           <td valign="top">
             <b><%=Rs("elemento") & Rs("elemento_val")%></b>&nbsp;<br />
             (<%=Rs("stampa")%>)
           </td>
           <td align="center">
             <% 
                 If Rs("obbligatorio") = "True" Then
                     %><b>X</b><%
                 End If
                 
                 %>&nbsp;
           </td>
           <td style="height:30px;">
             <% Do While continua_ciclo%>
               
               <input name="<%= "vedi_riga_" & Rs("id") %>" value="    " type="submit" 
                   style="border:none;color:#fff;background-image:url('http://ares.sicilyrentcar.it/images/lente.png');
                        width:16px;height:16px;background-color:transparent;cursor:zoom-in;" title="Vedi dettaglio"/>&nbsp;
               
               <%-- <% If Rs("applicabilita_da") <> "0" Or (Rs("costo") & "") <> "" Then%>--%>

                    <input name="<%= "elimina_riga_" & Rs("id") %>" type="submit"  title="Elimina condizione" value="X" 
                        onclick="return confirm('Vuoi eliminare la riga?')";/>&nbsp;
              <%--   <%End If%>--%>
                 
                 <% If Rs("id_a_carico_di") = "5" Then
                     %><b> INCLUSO - </b><%
                                             End If %>  
                 
                  <%=Rs("costo") %> 
                 <% If (Rs("costo") & "") <> "" Then %> 
                     <%= Rs("tipo_costo") %>
                 <% End If%> 
                 <%  If Rs("pac") = "True" Then%>
                    packed
                 <%  Else
                         If Rs("id_unita_misura") = "9" Then
                             %> al giorno
                          <%
                              ElseIf Rs("id_unita_misura") = "10" Then
                              %> ogni ora
                          <%
                              ElseIf Rs("id_unita_misura") = "11" Then
                              %> al km
                          <%
                              ElseIf Rs("id_unita_misura") = "16" Then
                              %> al km tra stazioni
                          <%
                              End If

                         %>
                 <% End If%>
                 <% If Rs("applicabilita_da") <> "0" Then%> 
                    <%="da " & Rs("applicabilita_da") & " a " & Rs("applicabilita_a")%> <%=Rs("unita_di_misura") %> 
                 <% End If%> <b> <%=getGruppi(Rs("id"))%></b>
               
               
                <% If IsDate(Rs("periodo_da")) Then%> 
               
                    <% 'If DateDiff("d", Rs("periodo_da"), Date.Now) < 0 Then%> 
                        <%'=" (valido dal " & FormatDateTime(Rs("periodo_da"), vbShortDate) & ")" %> 
                        <%=getCondizioniPeriodiCosti(Rs!id_riga) %>
                   <% 'End If%> 
               <% End If%> 

               <br />
               
               <%  continua = Rs.Read()
                   If Not continua Then
                       continua_ciclo = False
                   Else
                       If num_riga <> Rs("num_riga") Then
                           continua_ciclo = False
                       End If
                   End If
                %>
                 
                   
             <%Loop %>&nbsp;
           </td>
           <td align="center">
             <input name="<%= "elimina_" & num_riga %>" type="submit"  title="Elimina condizione" value="X2" 
                 onclick="return confirm('Vuoi eliminare la riga?')"/>

           </td>

        </tr>

    <%
        Loop
        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        'txtValidoDa.Text = Request.QueryString("valda")
        'txtValidoA.Text = Request.QueryString("vala")

    %>

     
     </table>
 
 <%-- fine righe tabella --%>  

 <table border="0" cellpadding="1" cellspacing="1" width="1024px">
     <tr>
       <td align="center">
         <br />

          <asp:Button ID="btnSalva" runat="server" Text="Salva condizione" ValidationGroup="invia" /> &nbsp;&nbsp;
          </td>
     </tr>
   </table>  
    <asp:Label ID="lbl_id_codice" runat="server"  visible="false" ></asp:Label>
    <asp:Label ID="lbl_tipo_op" runat="server"  visible="false" ></asp:Label>
    <asp:Label ID="txtQuery" runat="server"  visible="false" ></asp:Label>
</asp:Panel>
   
    <asp:SqlDataSource ID="sqlCondizioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice, valido_da, valido_a, data_creazione, id_template_val, iva_inclusa FROM condizioni WITH(NOLOCK) WHERE attivo='1' ORDER BY data_creazione DESC"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlElementi" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrizione FROM condizioni_elementi WITH(NOLOCK) WHERE (tipologia='CONDIZIONE' OR tipologia='ONERE' OR tipologia='JOUNG' OR tipologia='FUORI' OR tipologia='FRANCHIGIA RID' OR tipologia='SPESE_SPED_FATT' OR tipologia='VAL_GPS' OR tipologia='KM_EXTRA') ORDER BY descrizione"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlACaricoDi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM condizioni_a_carico_di WITH(NOLOCK) ORDER BY [descrizione]"></asp:SqlDataSource>


    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_gruppo, cod_gruppo FROM gruppi WITH(NOLOCK) ORDER BY cod_gruppo"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnitaDiMisura" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [descrizione], [id] FROM condizioni_unita_misura WITH(NOLOCK) ORDER BY [descrizione]"></asp:SqlDataSource>    
 
     <asp:SqlDataSource ID="sqlTemplateVAL" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM val_template WITH(NOLOCK) WHERE attivo='1' ORDER BY descrizione "></asp:SqlDataSource>
 
     <asp:SqlDataSource ID="SqlRigheVAL" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT DISTINCT val_template_righe.id_accessori_val,condizioni_elementi.descrizione FROM val_template_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON val_template_righe.id_accessori_val=condizioni_elementi.id WHERE (id_val_template = @id_val_template)">
         <SelectParameters>
             <asp:ControlParameter ControlID="dropTemplateVal" Name="id_val_template" 
                 PropertyName="SelectedValue" Type="Int32" />
         </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlTipoStampa" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM condizioni_metodo_stampa WITH(NOLOCK) ORDER BY id">
    </asp:SqlDataSource>
 

  
    <asp:SqlDataSource ID="sqlIva" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM aliquote_iva WITH(NOLOCK) ORDER BY [id]"></asp:SqlDataSource>
  

  
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="lblCodice" 
        ErrorMessage="Specificare il codice della condizione." Font-Size="0pt" 
        SetFocusOnError="True" ValidationGroup="invia"> </asp:RequiredFieldValidator>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="txtValidoDa" ErrorMessage="Specificare il campo 'Riferimento da'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia"> </asp:RequiredFieldValidator>
                            
    <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                ControlToValidate="txtValidoDa" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Valido da'." Type="Date" ValidationGroup="invia"> </asp:CompareValidator>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txtValidoA" ErrorMessage="Specificare il campo 'Riferimento a'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia"> </asp:RequiredFieldValidator>
                            
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="txtValidoA" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento a'." Type="Date" ValidationGroup="invia"> </asp:CompareValidator>
                            
    <asp:CompareValidator id="CompareFieldValidator2" runat="server"
                                 ControlToValidate="txtValidoA"
                                 ControlToCompare="txtValidoDa"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale è precedente alla data iniziale."
                                 ValidationGroup="invia"
                                 Font-Size="0pt"> </asp:CompareValidator>
      
 
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="invia" />    

    <asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>







</asp:Content>

