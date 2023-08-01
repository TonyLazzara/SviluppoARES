<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="lavaggi.aspx.vb" Inherits="lavaggi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
   <meta http-equiv="Cache-Control" content="no-cache" />
   <meta http-equiv="Pragma" content="no-cache" />

    <style type="text/css">
        .style1
        {
            width: 186px;
        }
        .style4
        {
            width: 456px;
        }
        .style5
        {
            width: 197px;
        }
        .style8
        {
            width: 122px;
        }
        .style9
        {
            width: 142px;
        }
        .style10
        {
            width: 206px;
        }
        .style11
        {
            width: 132px;
        }
        .style12
        {
            width: 230px;
        }
        .style13
        {
            width: 15%;
        }
        .style14
        {
            width: 169px;
        }
        .style15
        {
            width: 92px;
        }
        .style16
        {
            width: 112px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="tab_fornitore" runat="server">
     <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-bottom:0px;" border="0">   
       <tr>
          <td align="left" class="style16">
             <asp:Label ID="Label3" runat="server" Text="Nuovo Fornitore" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtNuovoFornitore" runat="server" MaxLength="50"></asp:TextBox>
          &nbsp;<asp:Button ID="btnSalvaFornitore" runat="server" Text="Salva" />
&nbsp;<asp:Button ID="btnAnnullaFornitore" runat="server" Text="Annulla" />
          </td>
       </tr>
     </table>
  </div>
  <div id="tab_pagina" runat="server">
   <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-bottom:0px;" border="0">   
        <tr>
          <td align="left">
              <asp:Label ID="lblNum" runat="server" Text="Num. Rif." CssClass="testo_bold"></asp:Label>
           </td>
          <td align="left">
              <asp:Label ID="LblTarga" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
           </td>
          <td align="left">
              <asp:Label ID="LblMarca" runat="server" Text="Marca" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
              <asp:Label ID="LblModello" runat="server" Text="Modello" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
               <asp:Label ID="lblStato" runat="server" CssClass="testo_bold" Text="Stato veicolo"></asp:Label>
          </td>                    
          <td align="left">
               <asp:Label ID="lblStato0" runat="server" CssClass="testo_bold" Text="Serb. Attuale/Serb.Max"></asp:Label>
          </td>                    
        </tr>
        <tr>
          <td align="left">
              <font color="#FF0000"><b><asp:Label ID="lblNumeroRifornimento" runat="server" ></asp:Label></b></font>
            </td>
          <td align="left">
              <font color="#FF0000"><b><asp:Label ID="LblTargaTesto" runat="server" Text="Label"></asp:Label></b></font>
          </td>
          <td align="left">            
              <font color="#FF0000"><b><asp:Label ID="LblMarcaTesto" runat="server" Text="Label"></asp:Label></b></font>
          </td>
          <td align="left">            
              <font color="#FF0000"><b><asp:Label ID="LblModelloTesto" runat="server" Text="Label"></asp:Label></b></font>
          </td>
          <td align="left">
               <font color="#FF0000"><b><asp:Label ID="LblStatoTesto" runat="server" Text="Label"></asp:Label></b></font>
           </td>   
          <td align="left">
               <font color="#FF0000"><b><asp:Label ID="lblSerbatoioAtt" runat="server" Text="Label"></asp:Label></b></font>
               <font color="#FF0000"><b><asp:Label ID="Label1" runat="server" Text="/"></asp:Label></b></font>
               <font color="#FF0000"><b><asp:Label ID="lblSerbatoioMax" runat="server" Text="Label"></asp:Label></b></font>
            </td>   
          <td align="left" >                                                      
               &nbsp;</td>       
        </tr>   
        <tr>
          <td align="left">
               &nbsp;</td>
          <td align="left">
               &nbsp;</td>
          <td align="left">
            
               &nbsp;</td>
          <td align="left">            
               &nbsp;</td>
          <td align="left">
               &nbsp;</td>   
          <td align="left">
               &nbsp;</td>   
          <td align="left" >                                                      
               &nbsp;</td>       
        </tr>              
        </table>   
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-bottom:0px;" border="0">   
          <tr>                    
          <td align="left" class="style5">            
               <asp:Label ID="LblDataMovimento" runat="server" Text="Label" CssClass="testo_bold"></asp:Label>
                               
          </td>                     
          <td  class="style13">
              <asp:Label ID="LblKm" runat="server" Text="Km Attuali" CssClass="testo_bold"></asp:Label>              

          </td>   
           <td class="style9">
              <asp:Label ID="LblConducenti" runat="server" Text="Conducente" CssClass="testo_bold" 
                  Visible="False"></asp:Label>              
          </td>  
           <td class="style14">
              <asp:Label ID="lblFornitore" runat="server" Text="Fornitore" CssClass="testo_bold" 
                  Visible="False"></asp:Label>              
          </td>  
          <td  class="style8">
              &nbsp;</td>   
          <td class="style15">
              <asp:Label ID="LblImporto" runat="server" Text="Importo" CssClass="testo_bold" 
                  Visible="False"></asp:Label>              
          </td>            
          <td class="style1">
              &nbsp;</td>            
        </tr>         
        <tr>
          <td align="left" class="style5">
               <asp:TextBox ID="txtDataMovimento" runat="server" Width="70px"></asp:TextBox>              
               <asp:CalendarExtender ID="txtDataMovimento_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataMovimento">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="txtDataMovimento_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataMovimento">
                </asp:MaskedEditExtender>  
                
                <!-- Separatore / -->
                <asp:Label ID="LblSeperatore" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                
                <asp:TextBox ID="txtOra" runat="server" Width="55px" ></asp:TextBox>
                  <asp:MaskedEditExtender ID="txtOra_MaskedEditExtender" runat="server" 
                          TargetControlID="txtOra"
                          Mask="99:99:99"
                          MessageValidatorTip="true"   
                          OnFocusCssClass="MaskedEditFocus"                                                
                          MaskType="Time">
                </asp:MaskedEditExtender>
               <%-- <asp:MaskedEditExtender ID="txtOra_MaskedEditExtender" runat="server" 
                          TargetControlID="txtOra"
                          Mask="99:99:99"
                          MessageValidatorTip="true"
                          ClearMaskOnLostFocus="true"
                          OnFocusCssClass="MaskedEditFocus"
                          OnInvalidCssClass="MaskedEditError"
                          MaskType="Time"
                          CultureName="en-US">
                </asp:MaskedEditExtender>--%>
         <asp:TextBox ID="txtDataMovimento1" runat="server" Width="70px" Visible="False"></asp:TextBox>      
         <!-- Separatore / -->
         <asp:Label ID="Separatore1" runat="server" Text="/" CssClass="testo_bold" 
                   Visible="False"></asp:Label>
         <asp:TextBox ID="txtOra1" runat="server" Width="56px" Visible="False" ></asp:TextBox>  
                               
          </td>          
          <td class="style13">
            <asp:TextBox ID="TxtKm" runat="server" MaxLength="6" Width="60px" Enabled="False"></asp:TextBox>
            <asp:Label ID="LblKmAttuali" runat="server" Text="      (KM...)" 
                  CssClass="testo_bold" Visible="False"></asp:Label>
          </td>
          <td class="style9">
            <asp:DropDownList ID="DDLConducenti" runat="server" Visible="False" 
                  DataSourceID="SqlDSConducenti" DataTextField="nominativo" DataValueField="id"
                   AppendDataBoundItems="True" >
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>    
          </td>
          <td class="style14">
            <asp:DropDownList ID="DDLFornitore" runat="server" Visible="False" 
                  DataSourceID="sqlFornitori" DataTextField="descrizione" DataValueField="id"
                   AppendDataBoundItems="True" >
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>    
            &nbsp;
            </td>
          <td class="style8">
              &nbsp;</td>
          <td class="style15">
              &nbsp;</td>          
          <td>
              &nbsp;</td>          
        </tr>
        </table>                
   <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-top:0px;" border="0">   
     <tr>
       <td  align="right" class="style4" >  
         <br />         
         <asp:Button ID="BtnSalva" runat="server" Text="Salva" ValidationGroup="Invio1" Height="25px" 
                />
         &nbsp;&nbsp;&nbsp;
       </td>
       <td  align="left" >       
        <br />           
         <asp:Button ID="BtnChiudi" runat="server" Text="Chiudi" Height="25px" />  
           &nbsp;        
           <asp:Label ID="LblSession" runat="server" Text="Label" Visible="False"></asp:Label>
       </td>
      </tr>        
   </table>  
  
   <!--Messaggi Errori  -->
   <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-top:0px;" border="0" runat="server" id="riga_totale" visible="false">   
     <tr>
       <td  align="left" class="style10"  >  
            <asp:Label ID="LblDataMovimento0" runat="server" Text="Guadagno/Perdita (solo refuel)" CssClass="testo_bold"></asp:Label>                     
       </td>
       <td  align="left" class="style11"  >  
             
               <font color="#FF0000"><b><asp:Label ID="lblTotSoloRefuel" runat="server" 
               Text="Label"></asp:Label></b></font>
             
       </td>
       <td  align="left" class="style12"  >  
            <asp:Label ID="Label2" runat="server" Text="Guadagno/Perdita (refuel+servizio)" CssClass="testo_bold"></asp:Label> 
       </td>
       <td  align="left"  >  
             
               <font color="#FF0000"><b><asp:Label ID="lblTotRefuelServizio" runat="server" 
               Text="Label"></asp:Label></b></font>
             
       </td>
    </tr>
   </table>  

</div>
   <asp:SqlDataSource ID="SqlSalvaDati" runat="server"
      ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      SelectCommand="">
   </asp:SqlDataSource>    
    <asp:SqlDataSource ID="SqlDSConducenti" runat="server"
     ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      SelectCommand="select id,nome + ' ' + cognome as nominativo from operatori WITH(NOLOCK) order by nominativo">
    </asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlMovimenti" runat="server"
     ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      
        SelectCommand="SELECT movimenti_targa.id, movimenti_targa.num_riferimento, tipologia_movimenti.descrzione, veicoli.targa FROM movimenti_targa INNER JOIN tipologia_movimenti ON movimenti_targa.id_tipo_movimento = tipologia_movimenti.id INNER JOIN veicoli ON movimenti_targa.id_veicolo = veicoli.id">
    </asp:SqlDataSource>--%>
    
    <asp:SqlDataSource ID="sqlFornitori" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
       SelectCommand="SELECT id, descrizione FROM alimentazione_fornitori_x_stazione WHERE id_stazione=@id_stazione ">
     <SelectParameters>
                <asp:ControlParameter ControlID="id_stazione" Name="id_stazione" PropertyName="Text" Type="Int32" />
     </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:Label ID="id_alimentazione" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="da_lavare" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="id_rifornimento" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="serb" runat="server" Text="" Visible="false"></asp:Label>
    
    <!-- Data Movimento -->
         <asp:RequiredFieldValidator ID="RFData" runat="server" 
                   ControlToValidate="txtDataMovimento" ErrorMessage="Specificare Data del movimento." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1">
         </asp:RequiredFieldValidator>

         <!-- Ora Movimento  -->
         <asp:RequiredFieldValidator ID="RFOra" runat="server" 
                   ControlToValidate="txtOra" ErrorMessage="Specificare Ora del movimento." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1">
         </asp:RequiredFieldValidator>

         <!-- Km Movimento  -->
         <asp:RequiredFieldValidator ID="RFKm" runat="server" 
                   ControlToValidate="TxtKm" ErrorMessage="Specificare Km del movimento." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1">
         </asp:RequiredFieldValidator>


         <!-- Conducente  -->
         <asp:RequiredFieldValidator ID="RFConducente" runat="server" 
                   ControlToValidate="DDLConducenti" ErrorMessage="Specificare Conducente." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1" 
               InitialValue="0"></asp:RequiredFieldValidator>           
         <!-- Fornitore  -->
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="DDLFornitore" ErrorMessage="Specificare il fornitore." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1" 
               InitialValue="0"></asp:RequiredFieldValidator> 
         
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   ValidationGroup="Invio1" BorderStyle="Groove" ForeColor="Black" 
               ShowMessageBox="True" ShowSummary="False" />      
</asp:Content>


