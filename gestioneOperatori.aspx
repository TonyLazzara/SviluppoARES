<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestioneOperatori.aspx.vb" Inherits="gestioneOperatori" title="Permessi Operatori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 

    <style type="text/css">
       
        .style1
        {
            width: 544px;
        }
        .style2
        {
            width: 372px;
        }
       
      


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="1" cellspacing="1" width="1024px" >
         <tr>
           <td align="left" style="background-color:#444;" class="testo_bold_bianco"> 
             <b>Permessi Operatori</b>
           </td>
         </tr>
</table>
<table border="0" cellpadding="1" cellspacing="1" width="1024px" >
 <tr>
   <td class="style1">
      <asp:Label ID="lblOperatore" runat="server" Text="Operatore:" CssClass="testo_bold"></asp:Label>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="dropOperatori" runat="server" AppendDataBoundItems="True" 
           AutoPostBack="True" DataSourceID="sqlOperatori" DataTextField="nome" 
           DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
       </asp:DropDownList>
   </td>
   <td align="right">
      <%--<asp:Label ID="lblProfilo" runat="server" Text="Profilo:" CssClass="testo_bold" Visible="false"></asp:Label>--%>
     </td>
   <td  class="style2">
       <%--<asp:DropDownList ID="dropProfili" runat="server" AppendDataBoundItems="True" 
           AutoPostBack="True" DataSourceID="sqlProfili" DataTextField="nome_profilo" 
           DataValueField="id" visible="false">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
       </asp:DropDownList>--%>
     </td>
 </tr>
 <tr>
   <td class="style1">
       &nbsp;</td>
   <td colspan="2">
       &nbsp;</td>
 </tr>
</table> 
<table runat="server" id="lista_funzionalita" visible="false" border="0" cellpadding="1" cellspacing="1" width="1024px" class="testo_bold_bianco" >
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Parco veicoli - Accesso alla funzionalità:<asp:CheckBox ID="checkParcoVeicoli" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
     <b>Tabelle Auto - Accesso alla funzionalità:<asp:CheckBox ID="checkTabelle" runat="server" /></b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top">
     
       <asp:ListView ID="listPermessiParcoVeicoli" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiParcoVeicoli">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;" >
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr >
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table runat="server" width="100%" class="testo_bold_nero">
                   <tr runat="server">
                       <td runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;" width="100%">
                               <tr runat="server" style="">
                                   <th runat="server">
                                   </th>
                                   <th runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr runat="server">
                       <td runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
   <td width="50%" valign="top">
     
       <asp:ListView ID="listPermessiTabelle" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiTabelle">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table runat="server" width="100%" class="testo_bold_nero">
                   <tr runat="server">
                       <td runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr runat="server" style="">
                                   <th runat="server">
                                   </th>
                                   <th runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr runat="server">
                       <td runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
 </tr>
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Gestione Stazioni - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneStazioni" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
        <b>Gestione VAL - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneVAL" runat="server" /></b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >
     <asp:ListView ID="listPermessiStazioni" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiStazioni">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table5" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table6" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr7" runat="server">
                       <td id="Td5" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr8" runat="server" style="">
                                   <th id="Th5" runat="server">
                                   </th>
                                   <th id="Th6" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr9" runat="server">
                       <td id="Td6" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
   </td>
   <td width="50%" valign="top" >
     <asp:ListView ID="listPermessiGestioneVAL" runat="server" DataKeyNames="id" DataSourceID="sqlPermessiGestioneVAL" >
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table7" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table8" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr10" runat="server">
                       <td id="Td7" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1"  style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr11" runat="server" style="">
                                   <th id="Th7" runat="server">
                                   </th>
                                   <th id="Th8" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr12" runat="server">
                       <td id="Td8" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
   </td>
 </tr>
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Gestione operatori - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneOperatori" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
       <b>Anagrafica - Accesso alla funzionalità:<asp:CheckBox ID="checkAnagarfica" runat="server" /></b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >  
      <asp:ListView ID="listGestioneOperatori" runat="server" DataKeyNames="id" DataSourceID="sqlGestioneOperatori">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table15" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table16" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr22" runat="server">
                       <td id="Td15" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr23" runat="server" style="">
                                   <th id="Th15" runat="server">
                                   </th>
                                   <th id="Th16" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr24" runat="server">
                       <td id="Td16" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
   <td width="50%">
      <asp:ListView ID="listAnagrafica" runat="server" DataKeyNames="id" 
           DataSourceID="sqlAnagrafica">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table13" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table14" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr19" runat="server">
                       <td id="Td13" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr20" runat="server" style="">
                                   <th id="Th13" runat="server">
                                   </th>
                                   <th id="Th14" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr21" runat="server">
                       <td id="Td14" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
   </td>
 </tr>
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Ordinativo lavori - Accesso alla funzionalità:</b><asp:CheckBox ID="checkOrdinativoLavori" runat="server" />
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
     <b>Funzioni veicoli - Accesso alle funzionalità:</b><asp:CheckBox ID="chkFunzioniVeicoli" runat="server" />
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >  
     
       <asp:ListView ID="listPermessiOrdinativoLavori" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiOrdinativoLavori">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table runat="server" width="100%" class="testo_bold_nero">
                   <tr runat="server">
                       <td runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr runat="server" style="">
                                   <th runat="server">
                                   </th>
                                   <th runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr runat="server">
                       <td runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
   <td width="50%" valign="top" >
       
       <asp:ListView ID="listPermessiFunzioniVeicoli" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiFunzioniVeicoli">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table runat="server" width="100%" class="testo_bold_nero">
                   <tr runat="server">
                       <td runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr runat="server" style="">
                                   <th runat="server">
                                   </th>
                                   <th runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr runat="server">
                       <td runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
 </tr>
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Gestione Listini - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneListini" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
       <b>Tabelle Listini - Accesso alla funzionalità:<asp:CheckBox ID="checkTabelleListini" runat="server" /></b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >  
     
       <asp:ListView ID="listGestioneListini" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiGestioneListini">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table1" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table2" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr1" runat="server">
                       <td id="Td1" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr2" runat="server" style="">
                                   <th id="Th1" runat="server">
                                   </th>
                                   <th id="Th2" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr3" runat="server">
                       <td id="Td2" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
   <td width="50%" valign="top" >
       
       <asp:ListView ID="listTabelleListini" runat="server" DataKeyNames="id" 
           DataSourceID="sqlPermessiTabelleListini">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table3" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table4" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr4" runat="server">
                       <td id="Td3" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr5" runat="server" style="">
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
                   <tr id="Tr6" runat="server">
                       <td id="Td4" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       </asp:ListView>
     </td>
 </tr>
 <tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Gestione POS e Pagamenti - Accesso alla funzionalità:<asp:CheckBox ID="checkGestionePOS" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
       <b>Preventivi/Prenotazioni/Contratti - Accesso alla funzionalità:<asp:CheckBox ID="checkPrevenvitiPrenotazioniContratti" runat="server" />
       </b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >  
       <asp:ListView ID="listGestionePOS" runat="server" 
           DataSourceID="sqlPermessiGestinePos">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table1" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table2" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr1" runat="server">
                       <td id="Td1" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr2" runat="server" style="">
                                   <th id="Th1" runat="server">
                                   </th>
                                   <th id="Th2" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr3" runat="server">
                       <td id="Td2" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       
       </asp:ListView>
     </td>
   <td width="50%" valign="top" >
       
       <asp:ListView ID="listPreventiviPrenotazioniContratti" runat="server" 
           DataSourceID="sqlPreventiviPrenotazioniContratti">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table9" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table10" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr13" runat="server">
                       <td id="Td9" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr14" runat="server" style="">
                                   <th id="Th9" runat="server">
                                   </th>
                                   <th id="Th10" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr15" runat="server">
                       <td id="Td10" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       
       </asp:ListView>
     </td>
 </tr>
<tr>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">  
       <b>Gestione Multe - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneMulte" runat="server" /></b>
   </td>
   <td width="50%"  style="color: #FFFFFF" bgcolor="#369061">
       <b>Gestione Danni - Accesso alla funzionalità:<asp:CheckBox ID="checkGestioneDanni" runat="server" /></b>
   </td>
 </tr>
 <tr>
   <td width="50%" valign="top" >  
     <asp:ListView ID="listGestioneMulte" runat="server" 
           DataSourceID="sqlGestioneMulte">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table1" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table2" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr1" runat="server">
                       <td id="Td1" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr2" runat="server" style="">
                                   <th id="Th1" runat="server">
                                   </th>
                                   <th id="Th2" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr3" runat="server">
                       <td id="Td2" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       
       </asp:ListView>
   </td>
   <td width="50%" valign="top" >  
     <asp:ListView ID="listGestioneDanni" runat="server" 
           DataSourceID="sqlGestioneDanni">
           <ItemTemplate>
               <tr style="background-color:#DCDCDC;color: #000000;">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>'>
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </ItemTemplate>
           <AlternatingItemTemplate>
               <tr style="">
                   <td>
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                           Visible="false" />
                       <asp:Label ID="idFunzionalita" runat="server" 
                           Text='<%# Eval("id_funzionalita") %>' Visible="false" />
                       <asp:Label ID="funzionalitaLabel" runat="server" 
                           Text='<%# Eval("funzionalita") %>' />
                   </td>
                   <td align="right">
                       <asp:Label ID="id_livello_accessoLabel" runat="server" 
                           Text='<%# Eval("id_livello_accesso") %>' Visible="false" />
                       <asp:DropDownList ID="dropPermessi" runat="server" AppendDataBoundItems="True" 
                           selectedValue='<%# Eval("id_livello_accesso") %>' >
                               <asp:ListItem Value="1">Accesso negato</asp:ListItem>
                               <asp:ListItem Value="2">Sola lettura</asp:ListItem>
                               <asp:ListItem Value="3">Lettura/Scrittura</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
           </AlternatingItemTemplate>
           <EmptyDataTemplate>
               <table id="Table11" runat="server" style="">
                   <tr>
                       <td>
                       </td>
                   </tr>
               </table>
           </EmptyDataTemplate>
           <LayoutTemplate>
               <table id="Table12" runat="server" width="100%" class="testo_bold_nero">
                   <tr id="Tr16" runat="server">
                       <td id="Td11" runat="server">
                           <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       
                               style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" 
                               width="100%">
                               <tr id="Tr17" runat="server" style="">
                                   <th id="Th11" runat="server">
                                   </th>
                                   <th id="Th12" runat="server">
                                   </th>
                               </tr>
                               <tr ID="itemPlaceholder" runat="server">
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr id="Tr18" runat="server">
                       <td id="Td12" runat="server" style="">
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
       
       </asp:ListView>
   </td>
  </tr>
 <tr>
   <td width="50%" align="center" colspan="2" style="width: 100%">
        
       <asp:Button ID="btnAggiorna" runat="server" Text="Aggiorna" Enabled="false"/>
        
   </td>
 </tr>
</table>
    <asp:SqlDataSource ID="sqlOperatori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT (cognome + ' ' + nome) As nome, id FROM operatori WITH(NOLOCK) WHERE attivo='1' ORDER BY cognome"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlProfili" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT nome_profilo, id FROM permessi_profili WITH(NOLOCK) WHERE attivo='1' ORDER BY nome_profilo"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiParcoVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='1') AND (funzionalita.id <> '1') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiTabelle" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='2') AND (funzionalita.id <> '9') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiOrdinativoLavori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='5') AND (funzionalita.id <> '30') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiFunzioniVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='6') AND (funzionalita.id <> '33') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiGestioneListini" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='8') AND (funzionalita.id <> '44') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPermessiTabelleListini" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='7') AND (funzionalita.id <> '39') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
     <asp:SqlDataSource ID="sqlPermessiStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='9') AND (funzionalita.id <> '51') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPermessiGestioneVal" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='10') AND (funzionalita.id <> '58') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPermessiGestinePos" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='11') AND (funzionalita.id <> '61') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPreventiviPrenotazioniContratti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='12') AND (funzionalita.id <> '71') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlGestioneMulte" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='13') AND (funzionalita.id <> '89') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlGestioneDanni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='14') AND (funzionalita.id <> '103') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlAnagrafica" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='4') AND (funzionalita.id <> '119') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlGestioneOperatori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT permessi_operatori.id, funzionalita.descrizione As funzionalita,funzionalita.id As id_funzionalita, id_livello_accesso FROM permessi_operatori WITH(NOLOCK) INNER JOIN funzionalita WITH(NOLOCK) ON permessi_operatori.id_funzionalita=funzionalita.id WHERE (id_operatore = @id_operatore) AND (funzionalita.id_macro_funzionalita='3') AND (funzionalita.id <> '21') ORDER BY funzionalita.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropOperatori" Name="id_operatore" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:Label ID="livello_accesso" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_profilo" runat="server" Visible="false"></asp:Label>
</asp:Content>

