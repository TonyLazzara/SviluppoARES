<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="rifornimenti.aspx.vb" Inherits="rifornimenti" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
   <meta http-equiv="Cache-Control" content="no-cache" />
   <meta http-equiv="Pragma" content="no-cache" />

    <script type="text/javascript">
        function ckdati(){
            var fornitore = document.getElementById('<%=DDLFornitore.ClientID()%>').value;
            var importo = document.getElementById('<%=TxtImporto.ClientID()%>').value;
            var litri = document.getElementById('<%=txtLitriRiforniti.ClientID()%>').value;
            var km = document.getElementById('<%=TxtKm.ClientID()%>').value;
            var conducente = document.getElementById('<%=DDLConducenti.ClientID()%>').value;
           //alert(fornitore);




            if ((litri == "") && (fornitore == "0") && (importo == "")) {
                alert("- Selezionare Fornitore\n- Inserire Litri\n- Inserire Importo");
                return false;
            }



            if ((km == "")) {
                alert("Inserire Km Attuali !");
                document.getElementById('<%=TxtKm.ClientID%>').focus()
                 return false;
            }
            else if ((conducente == "0")) {
                alert("Selezionare Conducente !");
                document.getElementById('<%=DDLConducenti.ClientID%>').focus()
                return false;
            }


            else if ((fornitore == "0")) {
                alert("Selezionare Fornitore !");
                document.getElementById('<%=DDLFornitore.ClientID%>').focus()
                return false;
            }
                        
            else if ((litri == "")) {
                alert("Inserire Litri !");
                document.getElementById('<%=txtLitriRiforniti.ClientID%>').focus()
                return false;
            }
            else if ((importo == "")) {
                alert("Inserire importo !");
                document.getElementById('<%=TxtImporto.ClientID%>').focus()
                return false;
            }

             else {
                    if (confirm('Confermi Salvataggio ?')) {
                        return true;
                    } else {
                        return false;
                    }
                }

        }
    </script>

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
              <asp:Label ID="lblLitriRiforniti" runat="server" Text="Litri Riforniti" CssClass="testo_bold" 
                  Visible="False"></asp:Label>              

          </td>   
          <td class="style15">
              <asp:Label ID="LblImporto" runat="server" Text="Importo" CssClass="testo_bold" 
                  Visible="False"></asp:Label>              
          </td>            
          <td class="style1">
              <asp:Label ID="lblCostoAlLitro" runat="server" Text="Costo al Litro" CssClass="testo_bold" Visible="False"></asp:Label>              
          </td>            
        </tr>         
        <tr>
          <td align="left" class="style5"> 
               <asp:TextBox ID="txtDataMovimento" runat="server" Width="70px" Enabled="false"></asp:TextBox>              
               <asp:CalendarExtender ID="txtDataMovimento_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDataMovimento">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="txtDataMovimento_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDataMovimento">
                </asp:MaskedEditExtender>  
                
                <!-- Separatore / -->
                <asp:Label ID="LblSeperatore" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                
                <asp:TextBox ID="txtOra" runat="server" Width="55px" Enabled="false" ></asp:TextBox>
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
            &nbsp;<asp:ImageButton ID="btnVediFornitore" runat="server" ImageUrl="/images/aggiorna.png" />
            </td>
          <td class="style8">
            <asp:TextBox ID="txtLitriRiforniti" runat="server" MaxLength="6" Width="40px" onKeyPress="return filterInputDouble(event);"
                  Visible="False"></asp:TextBox>       
          
                 
              </td>
          <td class="style15">
            <asp:TextBox ID="TxtImporto" runat="server" Width="60px" onKeyPress="return filterInputDouble(event);"
                  Visible="False"></asp:TextBox>                                
          </td>          
          <td>
              <asp:Label ID="lblCostoLitro" runat="server" CssClass="testo"></asp:Label>
          </td>          
        </tr>
        </table>                
   <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-top:0px;" border="0">   
     <tr>
       <td  align="right" class="style4" >  
         <br />         
         <asp:Button ID="BtnSalva" runat="server" Text="Salva" OnClientClick="return ckdati();" Height="25px" />
           <%--<asp:Button ID="Button1" runat="server" Text="Salva" ValidationGroup="Invio1" Height="25px" />--%>
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
   <table  cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-top:0px;" border="0">             
        <tr>
          <td>
             <asp:ListView ID="ListViewMovimenti" runat="server" DataSourceID="SqlMovimenti" 
               DataKeyNames="num_riferimento" EnableModelValidation="True">                             
               <ItemTemplate>
                   <tr style="background-color:#DCDCDC;color: #000000;">                                                             
                       <td>    
                           <asp:Label ID="id_tipo_movimento" runat="server" Text='<%# Eval("id_tipo_movimento") %>' Visible="false" />                       
                           <asp:HyperLink ID="num_riferimento" runat="server" Target="_blank"  Text='<%# Eval("num_riferimento") %>' NavigateUrl='<%# "PagTmp.aspx?parametro=" & Eval("num_riferimento")%>'>HyperLink</asp:HyperLink>                                                                               
                       </td>
                       <td>
                           <asp:Label ID="descrzioneLabel" runat="server" Text='<%# Eval("descrzione") %>' />
                       </td>
                       <td>
                           <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                       </td>
                       <td>
                           <asp:Label ID="differenzaLitri" runat="server" Text='<%# Eval("differenza_litri") %>' />
                       </td>
                       <td>
                           <asp:Label ID="importo_addebitato" runat="server"/>
                       </td>
                       <td>
                           <asp:Label ID="servizio_addebitato" runat="server"/>
                       </td>
                   </tr>
               </ItemTemplate>
               <AlternatingItemTemplate>
                   <tr>                                           
                       <td>
                           <asp:Label ID="id_tipo_movimento" runat="server" Text='<%# Eval("id_tipo_movimento") %>' Visible="false" /> 
                           <asp:HyperLink ID="num_riferimento" runat="server" Target="_blank"  Text='<%# Eval("num_riferimento") %>' NavigateUrl='<%# "PagTmp.aspx?parametro=" & Eval("num_riferimento")%>'>HyperLink</asp:HyperLink>                                                                               
                       </td>
                       <td>
                           <asp:Label ID="descrzioneLabel" runat="server" Text='<%# Eval("descrzione") %>' />
                       </td>
                       <td>
                           <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                       </td>
                       <td>
                           <asp:Label ID="differenzaLitri" runat="server" Text='<%# Eval("differenza_litri") %>' />
                       </td>
                       <td>
                           <asp:Label ID="importo_addebitato" runat="server"/>
                       </td>
                       <td>
                           <asp:Label ID="servizio_addebitato" runat="server"/>
                       </td>
                   </tr>
               </AlternatingItemTemplate>
               <EmptyDataTemplate>
                   <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
               </EmptyDataTemplate>
               <LayoutTemplate>
                   <table id="Table1" runat="server">
                       <tr id="Tr1" runat="server">
                           <td id="Td1" runat="server">
                               <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%"  
                                      style=" background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                   <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">                                                                              
                                       <th id="Th2" runat="server">
                                           Num. Riferimento</th>
                                       <th id="Th3" runat="server">
                                           Movimento</th>
                                       <th id="Th4" runat="server">
                                           Targa</th>
                                       <th id="Th1" runat="server">
                                           Diff. Lt.</th>
                                        <th id="Th5" runat="server">
                                           Imp. Addebitato
                                        </th>
                                        <th>
                                           Servizio Addebitato
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
       </tr>
  </table>
</div>


 <br />

 <%--inizio nuovo ListView per allegati con selezione multipla --%>

<div id="div_allegati" runat="server">
            
    
 <table style="border:4px solid #444;" border="0" cellspacing="1" cellpadding="1" width="1024px" id="tb_allegati" runat="server">
   <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="3">
         <asp:Label ID="Label71" runat="server" Text="Allegati" CssClass="testo_titolo"></asp:Label>        
      </td>
  </tr>
  <tr style="padding:5px;">

    <td class="style59">
        <asp:Label ID="Label72" runat="server" Text="Tipo Allegato:" CssClass="testo_bold" Width="120"/>
         &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dropNuovoAllegato" runat="server" DataSourceID="sqlTipoAllegati" DataTextField="descrizione"  CssClass="ddlist"
          DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
        </asp:DropDownList>  
    </td>
   <td>
         <asp:FileUpload ID="UploadAllegati" runat="server" style="float:left;"  />        
       &nbsp;<asp:Button ID="upload" runat="server" Text="Salva" OnClientClick="return preventMultipleSubmissions(this, event, 1);" style="float:right;position:absolute;margin-left:3px;background-color:#e88532;" />
           </td>
       </tr>
</table>




    
    <asp:ListView ID="ListViewAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="Id_allegato" >
            <ItemTemplate>
                <tr style="background-color:#FFFFFF; color: #000000; ">
                    <%--<td></td>--%>
                    <td><asp:Label ID="lblIdAllegato" runat="server" Text='<%# Eval("Id_allegato") %>' Visible="false"></asp:Label >
                        <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("NomeFile") %>'></asp:Label>
                        <asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label>
                    </td>
                  <%--  <td>
                        asp:Label ID="lblNomeFileOperatore" runat="server" Text='<%# Eval("nome_file_operatore") %>'  Visible="false"></asp:Label>
                    </td> --%>
                    <td style="width:150px;text-align:center;"><asp:Label ID="lbl_data_ins" runat="server" Text='<%#Eval("dataInserimento") %>'></asp:Label></td>                    
                    <td style="width:160px;text-align:center;"><asp:Label ID="lbl_operatore" runat="server" Text='<%# Eval("operatore") %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return verificaDelete();"  runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                    <%--<td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#FFFFFF; color: #000000;">
                  <%--  <td></td>--%>
                    <td><asp:Label ID="lblIdAllegato" runat="server" Text='<%# Eval("Id_Allegato") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("NomeFile") %>'></asp:Label>
                        <asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label>
                    </td>
                 <%--   <td>
                       <asp:Label ID="lblNomeFileOperatore" runat="server" Text='<%# Eval("nome_file_operatore") %>' Visible="false"></asp:Label>
                    </td>--%>
                    <td style="width:150px;text-align:center;"><asp:Label ID="lbl_data_ins" runat="server" Text='<%# Eval("dataInserimento")%>'></asp:Label></td>
                    <td style="width:160px;text-align:center;"><asp:Label ID="lbl_operatore" runat="server" Text='<%# Eval("operatore") %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return verificaDelete();" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                    <%--<td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table id="Table1" runat="server" style="">
                    <tr>
                        <td>Nessun allegato</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="Table2" runat="server" width="100%" style="font-family:Verdana;font-size:12px;">
                    <tr id="Tr1" runat="server" >
                        <td id="Td1" runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF;background-color:#000000;" >
                                    <%--<th id="Th1" runat="server">Id</th>--%>
                                    <th id="Th2" runat="server" style="width:150px;">Tipo</th>
                                    <th id="Th3" runat="server"  style="width:280px;">Nome File</th>
                                    <%--<th id="Th4" runat="server">File Operatore</th>--%>
                                    <th id="Th8" runat="server"  style="width:180px;text-align:center;">Data e Ora</th>
                                    <th id="Th9" runat="server"  style="width:250px;text-align:center;">Operatore</th>
                                    <th id="Th5" runat="server"></th>
                                    <th id="Th6" runat="server"></th>
                                    <%--<th id="Th7" runat="server"></th>--%>
                                </tr>
                                <tr ID="itemPlaceholder" runat="server"></tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td><asp:Label ID="lblIdAllegato" runat="server" Width="20px"  Text='<%# Eval("IdAllegato") %>'></asp:Label></td>
                    <td><asp:Label ID="lblTipo" runat="server" Width="40px" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Width="150px" Text='<%# Eval("Nome_File") %>'></asp:Label></td>
                    <td><asp:Label ID="lblPercorsoFile" runat="server" Width="180px" Font-Size="X-Small" Text='<%# Eval("my_path") %>'></asp:Label></td>
                    <td><asp:Label ID="lbl_data_ins" runat="server" Text='<%# Eval("dataInserimento") %>'></asp:Label></td>
                    <td><asp:Label ID="lbl_operatore" runat="server" Text='<%# Eval("operatore") %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" OnClientClick="return verificaDelete();" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                    <%--<td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <div style="margin-top:10px;font-family:Verdana;font-size:12px;">
            <asp:Label ID="lbl_allegati_inviati" runat="server" Text=""></asp:Label>
        </div>
         

     <asp:SqlDataSource ID="sqlTipoAllegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, tipoallegato as descrizione FROM rifornimenti_tipoAllegato WITH(NOLOCK) ORDER BY tipoallegato">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlAllegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT rifornimenti_Allegati.Id_allegato as IdAllegato, rifornimenti_Allegati.DataCreazione, rifornimenti_Allegati.IdTipoDocumento, rifornimenti_Allegati.NomeFile as nome_file, rifornimenti_Allegati.PercorsoFile as my_path, rifornimenti_Allegati.Id_Stazione, rifornimenti_Allegati.Id_Operatore,  
                  rifornimenti_TipoAllegato.TipoAllegato AS descrizione FROM rifornimenti_Allegati  WITH(NOLOCK) INNER JOIN rifornimenti_TipoAllegato ON rifornimenti_Allegati.IdTipoDocumento = rifornimenti_TipoAllegato.Id WHERE id_allegato='0'">
    </asp:SqlDataSource>
  
</div>


<%--end div allegati - salvo 22.11.2022 --%>


   <asp:SqlDataSource ID="SqlSalvaDati" runat="server"
      ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      SelectCommand="">
   </asp:SqlDataSource>    
    <asp:SqlDataSource ID="SqlDSConducenti" runat="server"
     ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      SelectCommand="select id,nome + ' ' + cognome as nominativo from drivers WITH(NOLOCK) order by nominativo">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlMovimenti" runat="server"
     ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      
        SelectCommand="SELECT movimenti_targa.id, movimenti_targa.num_riferimento, tipologia_movimenti.descrzione, veicoli.targa FROM movimenti_targa INNER JOIN tipologia_movimenti ON movimenti_targa.id_tipo_movimento = tipologia_movimenti.id INNER JOIN veicoli ON movimenti_targa.id_veicolo = veicoli.id">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlFornitori" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
       SelectCommand="SELECT id, descrizione FROM alimentazione_fornitori_x_stazione WHERE id_stazione=@id_stazione order by descrizione">
     <SelectParameters>
                <asp:ControlParameter ControlID="id_stazione_x_fornitori" Name="id_stazione" PropertyName="Text" Type="Int32" />
     </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:Label ID="id_alimentazione" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione_x_fornitori" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="stazione_out" runat="server" Text="" Visible="false"></asp:Label>
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

         <!-- Litri riforniti  -->
         <asp:RequiredFieldValidator ID="RFLitriRiforniti" runat="server" 
                   ControlToValidate="txtLitriRiforniti" ErrorMessage="Specificare i litri riforniti." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1" enabled="false">
         </asp:RequiredFieldValidator>

         <!-- Importo  -->
         <asp:RequiredFieldValidator ID="RFImporto" runat="server" 
                   ControlToValidate="TxtImporto" ErrorMessage="Specificare Importo." 
                   Font-Size="0pt" SetFocusOnError="true" ValidationGroup="Invio1" enabled="false">
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



