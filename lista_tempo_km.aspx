<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="lista_tempo_km.aspx.vb" Inherits="tariffe_lista_tempo_km" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 197px;
        }
        .style3
        {
            width: 112px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style4
        {
            width: 207px;
        }
        .style5
        {
            width: 193px;
        }
        .style6
        {
            width: 237px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Tempo-Km</b>
          
           </td>
         </tr>
     </table>
  
  <div id="tab_cerca" runat="server">
  <table border="0" cellpadding="2" cellspacing="0" width="1024px" runat="server" style="border: 4px solid #2e5d54">
    <tr>
      <td class="style2">
        <asp:Label ID="lblCodiceRicerca" runat="server" Text="Codice" CssClass="testo_bold"></asp:Label>
      </td>
      <td class="style3">
         <asp:Label ID="lblRiferimentoDaRicerca" runat="server" Text="Riferimento Da" CssClass="testo_bold"></asp:Label>
      </td>
      <td colspan="2">
         <asp:Label ID="lblRiferimentoARicerca" runat="server" Text="Riferimento A" CssClass="testo_bold"></asp:Label>
      </td>
    </tr>
     <tr>
     <td class="style2">
        
         <asp:TextBox ID="txtCodice" runat="server"></asp:TextBox>
        
      </td>
      <td class="style3">
        <a onclick="Calendar.show(document.getElementById('<%=txtCercaValidoDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtCercaValidoDa" runat="server" Width="70px"></asp:TextBox>
            </a>
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
               <asp:TextBox ID="txtCercaValidoA" runat="server" Width="70px"></asp:TextBox>
               </a>
              <%-- <asp:CalendarExtender ID="txtCercaValidoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtCercaValidoA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtCercaValidoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtCercaValidoA">
                </asp:MaskedEditExtender>    
                            
      &nbsp;&nbsp;
            
            <asp:Button ID="btnCerca" runat="server" Text="Cerca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
                         
           
           
            &nbsp;&nbsp;
            
                       
           
      </td>
      <td align="right">

            <asp:Button ID="btnNuovo" runat="server" Text="Nuovo Tempo-KM" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
                         
           
           
      </td>
    </tr>
  </table>
  <table width="1024px">
    <tr> 
      <td colspan="4">
        <asp:Label ID="lbl_errore_sup" runat="server" Font-Bold="True" ForeColor="Red" Text="" ></asp:Label>
          <br />&nbsp;
          <asp:ListView ID="listTempoKm" runat="server" DataKeyNames="id" DataSourceID="sqlTempoKm">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC;color: #000000;">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="iva_inclusa" runat="server" Text='<%# Eval("iva_inclusa") %>' Visible="false" />
                          <asp:Label ID="id_aliquota_iva" runat="server" Text='<%# Eval("id_aliquota_iva") %>' Visible="false" />
                          
                          <asp:TextBox ID="codiceText" runat="server" Text='<%# Eval("codice") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="codiceText" ErrorMessage="Specificare il codice del tempo+km." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                          </asp:RequiredFieldValidator>
                          
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' Visible="false" />
                      </td>
                      <td>         
                           <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' Visible="false" />     
                          
                           <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_daText").ClientID %>'), '%d/%m/%Y', false)">                       
                           <asp:TextBox ID="valido_daText" runat="server" Width="70px" Text='<%# Eval("valido_da", "{0: dd/MM/yyyy}") %>'></asp:TextBox>
                           </a>

                          <%-- <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_daText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_daText">
                            </asp:MaskedEditExtender>    
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="valido_daText" ErrorMessage="Specificare il campo 'Riferimento da'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                            </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                ControlToValidate="valido_daText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento da'." Type="Date" ValidationGroup="invia_lista">
                            </asp:CompareValidator>
      
                      </td>
                      <td>
                           <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' Visible="false" />
                           <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_aText").ClientID %>'), '%d/%m/%Y', false)">    
                           <asp:TextBox ID="valido_aText" runat="server" Width="70px" Text='<%# Eval("valido_a", "{0: dd/MM/yyyy}") %>'></asp:TextBox>
                               </a>
                         <%--  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_aText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_aText">
                            </asp:MaskedEditExtender> 
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="valido_aText" ErrorMessage="Specificare il campo 'Riferimento a'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                            </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="valido_aText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento a'." Type="Date" ValidationGroup="invia_lista">
                            </asp:CompareValidator>
                            
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
                          <asp:TextBox ID="txtVariazione" runat="server" Width="40px"></asp:TextBox>
                          &nbsp;
                          <asp:ImageButton ID="btnDuplica" runat="server" ImageUrl="/images/aggiorna.png" CommandName="duplica" />
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
                          <asp:Label ID="id_aliquota_iva" runat="server" Text='<%# Eval("id_aliquota_iva") %>' Visible="false" />
                          
                          <asp:TextBox ID="codiceText" runat="server" Text='<%# Eval("codice") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="codiceText" ErrorMessage="Specificare il codice del tempo+km." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                          </asp:RequiredFieldValidator>
                          
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' Visible="false" />
                      </td>
                     <td>         
                           <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' Visible="false" />    
                          <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_daText").ClientID %>'), '%d/%m/%Y', false)">  
                           <asp:TextBox ID="valido_daText" runat="server" Width="70px" Text='<%# Eval("valido_da") %>'></asp:TextBox>
                              </a>
                       <%--    <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_daText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_daText">
                            </asp:MaskedEditExtender>    
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="valido_daText" ErrorMessage="Specificare il campo 'Riferimento da'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                            </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                ControlToValidate="valido_daText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento da'." Type="Date" ValidationGroup="invia_lista">
                            </asp:CompareValidator>
      
                      </td>
                      <td>
                           <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' Visible="false" />
                           <a onclick="Calendar.show(document.getElementById('<%# Container.FindControl("valido_aText").ClientID %>'), '%d/%m/%Y', false)">  
                           <asp:TextBox ID="valido_aText" runat="server" Width="70px" Text='<%# Eval("valido_a") %>'></asp:TextBox>
                               </a>
                         <%--  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="valido_aText">
                            </asp:CalendarExtender>--%>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                                TargetControlID="valido_aText">
                            </asp:MaskedEditExtender> 
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="valido_aText" ErrorMessage="Specificare il campo 'Riferimento a'." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia_lista">
                            </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="valido_aText" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per il campo 'Riferimento a'." Type="Date" ValidationGroup="invia_lista">
                            </asp:CompareValidator>
                            
                            <asp:CompareValidator id="CompareFieldValidator2" runat="server"
                                 ControlToValidate="valido_aText"
                                 ControlToCompare="valido_daText"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale è precedente alla data iniziale."
                                 ValidationGroup = "invia_lista"
                                 Font-Size="0pt"> </asp:CompareValidator>
                      </td>
                      <td>
                          <asp:TextBox ID="txtVariazione" runat="server" Width="40px"></asp:TextBox>
                          &nbsp;
                          <asp:ImageButton ID="btnDuplica" runat="server" ImageUrl="/images/aggiorna.png" CommandName="duplica" />
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
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table runat="server" style="">
                      <tr>
                          <td>
                              Nessun Tempo-Km memorizzato.</td>
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
                                          <asp:Label ID="Label8" runat="server" Text="Riferimento a" CssClass="testo_titolo"></asp:Label>
                                      </th>
                                      <th runat="server" align="left">
                                          <asp:Label ID="Label7" runat="server" Text="Aggiorna" CssClass="testo_titolo"></asp:Label>
                                      </th>
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
    <table border="0" cellpadding="2" cellspacing="0" width="1000px" >
         <tr>
           <td align="left" class="style6">
             <asp:Label ID="lblCodiceNuovo" runat="server" Text="Codice" CssClass="testo_bold"></asp:Label>
               &nbsp;
               <asp:TextBox ID="lblCodice" runat="server"></asp:TextBox>
           </td>
           <td align="left" class="style4">
               <asp:Label ID="lblRiferimentoDaNuovo" runat="server" Text="Riferimento da" CssClass="testo_bold"></asp:Label>&nbsp;
                <a onclick="Calendar.show(document.getElementById('<%=txtValidoDa.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtValidoDa" runat="server" Width="70px"></asp:TextBox>
                    </a>
               <%--<asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="txtValidoDa">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="txtValidoDa">
               </asp:MaskedEditExtender>
             </td>
           <td align="left" class="style5">
               <asp:Label ID="lblRiferimentoA" runat="server" Text="Riferimento a" CssClass="testo_bold"></asp:Label>
               &nbsp;
               <a onclick="Calendar.show(document.getElementById('<%=txtValidoA.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtValidoA" runat="server" Width="70px"></asp:TextBox>
                   </a>
             <%--  <asp:CalendarExtender ID="txtValidoA_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="txtValidoA">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="txtValidoA_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="txtValidoA">
               </asp:MaskedEditExtender>
               &nbsp;
           </td>
           <td align="right">
               <asp:Button ID="btnStampa" runat="server" Text="Stampa" />
             <%--<div align="right" runat="server" id="div_stampa"><a target="_blank" href="GeneraPdf.aspx?DocPdf=/stampe/stampa_tempo_km&pagina=verticale&id_tempo_km=<%= lbl_id_codice.Text %>">Stampa</a></div>--%>
           </td> 
          
         </tr>

         <tr>
             <td align="left" class="style1" colspan="3">
                 <asp:Label ID="Label5" runat="server" CssClass="testo_bold" Text="Iva:"></asp:Label>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="dropIva" runat="server" AppendDataBoundItems="True">
                     <asp:ListItem Selected="True" Value="1">Inclusa</asp:ListItem>
                     <asp:ListItem Value="0">Esclusa</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;
                 <asp:Label ID="Label6" runat="server" CssClass="testo_bold" Text="Aliquota:"></asp:Label>
                 &nbsp;<asp:DropDownList ID="dropAliquota" runat="server" DataSourceID="sqlIva" 
                     DataTextField="descrizione" DataValueField="id">
                 </asp:DropDownList>
             </td>
             <td align="left">
                 &nbsp;</td>
         </tr>

</table>

      &nbsp;
      <br />
      <% 

          'NEL CASO IN CUI E' STATO EFFETTUATO IL SALVATAGGIO DI UN NUOVO TEMPO KM, CASO IN CUI L'ID_TEMPO_KM NON RESTA MEMORIZZATO NELL'APPOSITA LABEL,
          'RECUPERO IL VALORE PRECEDNETE CONSERVATO NELL'INPUT

          If lbl_id_codice.Text = "" Then
              lbl_id_codice.Text = Request.Form("last_id_tempo_km")
          End If

          'PER PRIMA COSA SELEZIONO TUTTI I GRUPPI PRESENTI A SISTEMA
          Dim gruppi(200) As String
          gruppi = getGruppi()

          Dim i As Integer = 0
          Dim k As Integer = 1

          Dim numColonne As String = Request.Form("numColonne")
          Dim caricaDati As Boolean

          'I PULSANTI DI AGGIUNTA DEVONO ESSERE VISIBILI SOLAMENTE FINCHE' LA TARIFFA NON E' "CHIUSA" CON LA COLONNA EXTRA DAY
          Dim esisteExtra As Boolean

          'LA PRIMA VOLTA CARICO I DATI DA DATABASE; SUCCESSIVAMENTE LI RECUPERO DAL PRECEDENTE STATO-------------------------------------------
          If numColonne = "" Then
              caricaDati = True
              numColonne = getNumColonne(lbl_id_codice.Text)
              esisteExtra = esiste_extra()
          Else
              caricaDati = False

              'CONTROLLO SE L'ULTIMA VOLTA E' STATA IMPOSTATA LA COLONNA EXTRA DAY
              Dim testA As String = Request("a_" & numColonne)
              Dim testA2 As String = Request("a_" & numColonne - 1)


              If testA = "999" Or testA2 = "999" Then
                  esisteExtra = True
              Else
                  esisteExtra = False
              End If
          End If
          '-------------------------------------------------------------------------------------------------------------------------------------
          'CONTROLLO SE E' STATA RICHIESTA L'ELIMINAZIONE DELL'ULTIMA COLONNA-------------------------------------------------------------------
          Dim elimina As String
          elimina = Request.Form("elimina")

          If elimina <> "" Then
              numColonne = numColonne - 1
              esisteExtra = False
          End If

          '-------------------------------------------------------------------------------------------------------------------------------------


          'CONTROLLO SE E' STATO RICHIESTO L'INSERIMENTO DI UNA COLONNA-------------------------------------------------------------------------
          Dim aggiungi As String
          aggiungi = Request.Form("aggiungi")

          If aggiungi <> "" Then
              'CONTROLLO CHE NON SIANO STATI PASSATI ELEMENTI ERRATI O MANCANTI-

              Dim valore As String
              Dim valori_corretti As Boolean = True

              Dim test As Double
              Dim packed As String
              Dim test_da As Integer
              Dim test_a As Integer

              'PER PRIMA COSA CONTROLLO CHE NON SIANO STATI PASSATI VALORI NON CORRETTI

              i = 1
              k = 1

              Try
                  Do While gruppi(i) <> "000"
                      Do While k <= numColonne


                          valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                          If Trim(valore) = "" Then
                              valore = 0
                          End If
                          test = CDbl(valore)

                          valore = Request.Form("da_" & k)
                          test_da = CInt(valore)

                          valore = Request.Form("a_" & k)
                          test_a = CInt(valore)

                          If test < 0 Then
                              valori_corretti = False
                          End If

                          If test_da <= 0 Or test_a <= 0 Or test_a < test_da Then
                              valori_corretti = False
                          End If

                          k = k + 1
                      Loop

                      k = 1
                      i = i + 1
                  Loop
              Catch
                  'NEL CASO IN CUI VENGA TROVATO UN VALORE NON CORRETTO NON PROSEGUO CON LA MODIFICA ED AVVERTO L'UTENTE
                  valori_corretti = False
              End Try



              If valori_corretti Then
                  numColonne = numColonne + 1
              Else
                  'SE C'E' UN ERRORE AZZERO IL VALORE (A FALSE) DI esisteExtra, ALTRIMENTI NON VERREBBE MOSTRATO IL PULSANTE AGGIUNGI
                  %> 
                      <asp:Label ID="lblErrore" runat="server" Font-Bold="True" ForeColor="Red" Text="Rilevato uno o più valori non corretti: correggerli per poter continuare" ></asp:Label><br />
                  <%
                     
                      esisteExtra = False
                  End If
       
    End If
    '-------------------------------------------------------------------------------------------------------------------------------------
  
    'CONTROLLO SE E' STATO RICHIESTO IL SALVATAGGIO/MODIFICA DELLA TABELLA----------------------------------------------------------------
              i = 0
              k = 1
              Dim memorizza As String
              memorizza = Request.Form("memorizza")
              
              If memorizza <> "" Then
                  
                  
                  If True Then
                      'If tempo_km_non_collegato(lbl_id_codice.Text) Then
                
                      'CONTROLLO CHE NON SIANO STATI PASSATI ELEMENTI ERRATI O MANCANTI-
        
                      Dim valore As String
                      Dim valore1 As String
                         Dim valori_corretti As Boolean = True
                         Dim gg_extra_e_packed As Boolean = False
                      Dim test As Double
                      Dim packed As String
                      Dim gg_extra As String
                      Dim test_da As Integer
                      Dim test_a As Integer
                      Dim j As Integer
                      'PER PRIMA COSA CONTROLLO CHE NON SIANO STATI PASSATI VALORI NON CORRETTI
        
                      i = 1
                      k = 1
        
                      Try
                          'SE NELL'ULTIMA COLONNA SONO PRESENTI SOLO VALORI VUOTI ALLORA LA IGNORO DAI CONTROLLI E FACCIO IN MODO CHE NON 
                          'VENGA SALVATA NE VENGA CREATA UNA NUOVA COLONNA ALLA FINE DELLA PROCEDURA A MENO CHE NON SIA L'UNICA COLONNA
                      
                          j = numColonne
                          Dim considerareUltimaColonna = False
                      
                          Do While gruppi(i) <> "000"
                              valore = Request.Form("valore_" & gruppi(i) & "_" & numColonne)
               
                              valore1 = Request.Form("a_" & numColonne)
                          
                              If valore1 <> "" Or valore <> "" Then
                                  considerareUltimaColonna = True
                              End If
                              i = i + 1
                          Loop

                          If numColonne = "1" Then
                              considerareUltimaColonna = True
                          End If
                      
                          If Not considerareUltimaColonna Then
                              j = j - 1
                          End If
                      
                          i = 1
                          k = 1
                      
                          Do While gruppi(i) <> "000"
                                 Do While k <= j
                                     'SE LA COLONNA E' SIA PACKED CHE GIORNI EXTRA MOSTRO UN ERRORE - INFATTO IN CASO DI GIORNO EXTRA LA COLONNA NON PUO' ESSERE PACKED
                                     Dim pac As String = Request.Form("packed_" & k)
                                     Dim extra As String = Request.Form("extra_" & k)
                          
                                     If pac <> "" And extra <> "" Then
                                         valori_corretti = False
                                         gg_extra_e_packed = True
                                     End If
                                     
                                     valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                                     If Trim(valore) = "" Then
                                         valore = "0"
                                     End If
                                  
                                     test = CDbl(valore)
                    
                                     valore = Request.Form("da_" & k)
                                     test_da = CInt(valore)
                    
                                     valore = Request.Form("a_" & k)
                                     test_a = CInt(valore)
        
                    
                                     If test < 0 Then
                                         valori_corretti = False
                                     End If
                    
                                     If test_da <= 0 Or test_a <= 0 Or test_a < test_da Then
                                         valori_corretti = False
                                     End If
                
                                     k = k + 1
                                 Loop
            
                              k = 1
                              i = i + 1
                          Loop
                      Catch
                          'NEL CASO IN CUI VENGA TROVATO UN VALORE NON CORRETTO NON PROSEGUO CON LA MODIFICA ED AVVERTO L'UTENTE
                          valori_corretti = False
                      End Try
            
                      Dim tempo_km_non_esistente As Boolean = True
                  
                      'PRIMA DI SALVARE CONTROLLO SE IL TEMPO-KM E' GIA' ESISTENTE (CODICE - VALIDO_DA - VALIDO_A)
                      If Trim(lblCodice.Text) <> "" And Trim(txtValidoDa.Text) <> "" And Trim(txtValidoA.Text) <> "" Then
                          tempo_km_non_esistente = check_tempo_km(Trim(lblCodice.Text), Trim(txtValidoDa.Text), Trim(txtValidoA.Text), lbl_id_codice.Text)
                      Else
                          valori_corretti = False
                      End If
            
                      If valori_corretti And tempo_km_non_esistente Then
                          'IN QUESTO CASO PROCEDO COL SALVATAGGIO 
                          'PER PRIMA COSA ELIMINO LE RIGHE ATTUALMENTE SALVATE SE SIAMO IN FASE DI MODIFICA. MODIFICO O CREDO LA RIGA 
                          'NELLA TABELLA tempo_km
                          If lbl_id_codice.Text <> "" Then
                              elimina_righe(lbl_id_codice.Text)
                          End If
                      
                          salvaTempoKm(Trim(lblCodice.Text), Trim(txtValidoDa.Text), Trim(txtValidoA.Text), lbl_id_codice.Text)
                     
                          'INSERISCO LE RIGHE
                          i = 2
                          k = 1
                      
        
                          Do While gruppi(i) <> "000"
                              Do While k <= j
                                  packed = Request.Form("packed_" & k)
                                  gg_extra = Request.Form("extra_" & k)

                                  If packed <> "" Then
                                      packed = "1"
                                  Else
                                      packed = "0"
                                  End If
                                  
                                  If gg_extra <> "" Then
                                      gg_extra = "1"
                                  Else
                                      gg_extra = "0"
                                  End If
                              
                                  test_da = Request.Form("da_" & k)
                                  test_a = Request.Form("a_" & k)
                              
                                  valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                                  If Trim(valore) = "" Then
                                      valore = "0"
                                  End If
                
                                  memorizza_valore(lbl_id_codice.Text, gruppi(i), test_da, test_a, valore, k, packed, gg_extra)
                                  k = k + 1
                              Loop
            
                              k = 1
                              i = i + 1
                          Loop
                       
                          i = 0
                          k = 1
                           
                          If (Not esisteExtra And j = numColonne) Or (esisteExtra And Request.Form("a_" & numColonne) = "999") Or (esisteExtra And Request.Form("a_" & numColonne) = "9999") Then
                              numColonne = numColonne + 1
                          End If

                       %> 
                      <center><asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Black" Text="Salvataggio effettuato correttamente." ></asp:Label></center><br />&nbsp;
                       <%
                  ElseIf Not valori_corretti Then
                           'SE C'E' UN ERRORE AZZERO IL VALORE (A FALSE) DI esisteExtra, ALTRIMENTI NON VERREBBE MOSTRATO IL PULSANTE AGGIUNGI
                          
                  %> 
                      <center><asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Text="Rilevato uno o più valori non corretti: salvataggio non effettuato." ></asp:Label></center><br />&nbsp;
                  <%
                             If gg_extra_e_packed Then
                          %> 
                        <center> <asp:Label ID="lblErrore2" runat="server" Font-Bold="True" ForeColor="Red" Text="Attenzione: la colonna GIORNI EXTRA non può essere packed." ></asp:Label><br /></center>
                         <%
                             End If
                      esisteExtra = False
                  ElseIf Not tempo_km_non_esistente Then
                           'SE C'E' UN ERRORE AZZERO IL VALORE (A FALSE) DI esisteExtra, ALTRIMENTI NON VERREBBE MOSTRATO IL PULSANTE AGGIUNGI
                  %> 
                      <center><asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Text="Tempo-Km già esistente: salvataggio non effettuato." ></asp:Label></center><br />&nbsp;
                  <%
                      esisteExtra = False
                  End If
              
              Else
                   %> 
                      <center><asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red" Text="Tempo-Km collegato ad una tariffa: salvataggio non effettuato." ></asp:Label></center><br />&nbsp;
                  <%
              End If
                  
          End If
          '-------------------------------------------------------------------------------------------------------------------------------------              
                  
          i = 0
          k = 1
    
          Dim lastDa As String
          Dim lastA As String
          Dim is_checked As String
 %>
   <% If Not esisteExtra Then%>
       <input name="aggiungi" type="submit"  title="no Stop sell" value="Aggiungi colonna" />&nbsp;&nbsp;
    <% Else %>
        <%--IN QUESTO CASO PERMETTO DI SETTARE COME MASTER IL TMP+KM ATTUALE--%>
    <% End If%>
    
    <% If numColonne > 1 Or esisteExtra Then%>
       <input name="elimina" type="submit"  title="Elimina ultima colonna" value="Elimina ultima colonna" />&nbsp;&nbsp;
   <% End If %>
    
   <input id="Text4" name="numColonne" type="hidden" style="width:50px;" value="<%=numColonne %>"    />
   
   <br />&nbsp;

   <% If caricaDati Then%>
   
   <table border="1" cellpadding="1" cellspacing="1">
      <%  Do While gruppi(i) <> "000"%> 
         <tr>
           <% If i = 0 Then%>
                <td align="center" style="background-color:White"><b>PACK</b></td>
           <%ElseIf i = 1 Then%>
              <td style="background-color:White"><b>Gruppo</b></td>
           <%ElseIf i = 2 Then%>
             <%-- <td style="background-color:White"><b>G.EXTR</b></td>--%>
             <td ><b></b></td>
           <%Else%>
             <td align="center" style="background-color:White"><b><%=gruppi(i)%></b></td>
           <% End If %>
         
           <% Do While k <= numColonne%>   
               <% If i = 0 Then%>
                      <% If k < numColonne Then%>
                          <% If getPacked(k) Then%>
                             <td align="center"><input id="Checkbox1" name="<%= "packed_" & k %>" type="checkbox" checked  /></td>  
                          <% Else%>
                              <td align="center"><input id="Checkbox1" name="<%= "packed_" & k %>" type="checkbox"  /></td>  
                          <%End If%>
                      <%ElseIf Not esisteExtra Then%>
                          <%--CREO UNA NUOVA COLONNA SOLAMENTE SE NON E' PRESENTE LA COLONNA EXTRA DAY--%>
                          <td align="center"><input id="Checkbox2" name="<%= "packed_" & k %>" type="checkbox" /></td> 
                      <%End If %>
               <%ElseIf i = 1 Then%>
                  <% If k < numColonne Then%>
                            <%  lastDa = getColonnaDa(lbl_id_codice.Text, k)
                                lastA = getColonnaA(lbl_id_codice.Text, k)
                                %>
                             <td ><input id="Text2" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastDa %>" readonly="true" /> - <input id="Text3" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:24px;" value="<%=lastA%>" readonly="true" /></td>
                  <%ElseIf (lastA <> "999" And lastA <> "9999") Then%>
                             <%--CREO UNA NUOVA COLONNA SOLAMENTE SE NON E' PRESENTE LA COLONNA EXTRA DAY--%>
                          <td ><input id="<%= "da_" & k %>" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA + 1 %>" readonly="true" /> - <input id="<%= "a_" & k %>" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" /></td>
                  <%End If %>
             <%  ElseIf i = 2 Then
                     'COMPORTAMENTO DA GIORNO EXTRA - VALIDO SOLO PER L'ULTIMA COLONNA

                     If (k < numColonne - 1) Then%>
                            <td align="center"></td>
                          <%-- <td align="center"><input id="Checkbox2" name="<%= "extra_" & k %>" type="checkbox" disabled  /></td> --%>
                       <% ElseIf (k = numColonne - 1) And getGGExtra(k) Then%>
                            <%--<td align="center"><input id="Checkbox6" name="<%= "extra_" & k %>" type="checkbox" checked  /></td>--%>
                            <td align="center"></td>
                       <% ElseIf (k = numColonne - 1) And Not getGGExtra(k) Then%>
                             <td align="center"></td>
                            <%--<td align="center"><input id="Checkbox7" name="<%= "extra_" & k %>" type="checkbox" /></td>--%>
                       <% End If %>  



               <%ElseIf k < numColonne Then%>
                        <td align="center"><input id="Text1" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" value="<%=getValore(lbl_id_codice.Text,gruppi(i),k) %>"  /></td>
               <%ElseIf k = numColonne And (lastA <> "999" And lastA <> "9999") Then%>
                        <td align="center"><input id="<%= "valore_" & gruppi(i) & "_" & k %>" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" /></td>
               <% End If %>
           
           <%  k = k + 1
           Loop
           k = 1

           %>
           
         </tr>
      <% 
          i = i + 1
         
      Loop
      %>   
   </table>
   
   <%  Else '-----------------------TABELLA IN CASO DI REFRESH-----------------------------------------------------------------------------
           Dim valore_precedente As String
          
           i = 0
           k = 1
           
           %>
      <table border="1" cellpadding="1" cellspacing="1">
        <%  Do While gruppi(i) <> "000" %> 
          <tr>
            <% If i = 0 Then%>
                <td align="center" style="background-color:White"><b>PACK</b></td>
           <%ElseIf i = 1 Then%>
              <td style="background-color:White"><b>Gruppo</b></td>
           <%ElseIf i = 2 Then%>
              <%--<td colspan="3" style="background-color:White"><b>G.EXTR</b></td>--%>
              <td ><b></b></td>
           <%Else%>
             <td align="center" style="background-color:White"><b><%=gruppi(i)%></b></td>
           <% End If %>
           <% Do While k <= numColonne%>
                <% If i = 0 Then%>
                   <% If k < numColonne Then%>
                       <%  
                           is_checked = Request.Form("packed_" & k)
                           If is_checked <> "" Then
                       %>
                              <td align="center"><input id="Checkbox3" name="<%= "packed_" & k %>" type="checkbox" checked  /></td>
                       <%  Else%>
                              <td align="center"><input id="Checkbox5" name="<%= "packed_" & k %>" type="checkbox"  /></td>
                       <%  End If%>
                       
                    <%ElseIf Not esisteExtra Then%>
                          <%--CREO UNA NUOVA COLONNA SOLAMENTE SE NON E' PRESENTE LA COLONNA EXTRA DAY--%>
                          <td align="center"><input id="Checkbox4" name="<%= "packed_" & k %>" type="checkbox" /></td> 
                      <%End If %>
                <%ElseIf i = 1 Then%>
                        <% If k < numColonne Then%>
                            <%  lastDa = Request.Form("da_" & k)
                                lastA = Request.Form("a_" & k)
                                %>
                             <td ><input id="Text5" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastDa %>" readonly="true" /> - <input id="Text6" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:24px;" value="<%=lastA%>" readonly="true" /></td>
                        <%  ElseIf (lastA <> "999" And lastA <> "9999") Then
                                %>
                             <%--CREO UNA NUOVA COLONNA SOLAMENTE SE NON E' PRESENTE LA COLONNA EXTRA DAY--%>
                             <%  'SE E' STATA RICHIESTA L'ELIMINAZIONE DELL'ULTIMA COLONNA NON MOSTRO I VALORI PRECEDENTI DELL'ULTIMA COLONNA
                                 If elimina = "" Then
                                 %>
                                    <td ><input id="Text7" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA + 1 %>" readonly="true" /> - <input id="Text7" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=Request.Form("a_" & k)%>" /></td>
                                    <%
                                        Else
                                    %>
                                    <td ><input id="Text11" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA + 1 %>" readonly="true" /> - <input id="Text12" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" /></td>
                                 <%    
                                     End If%>
                             
                        <%End If %>
                <%  ElseIf i = 2 Then
                        'COMPORTAMENTO DA GIORNO EXTRA - VALIDO SOLO PER L'ULTIMA COLONNA
                        If (k < numColonne - 1) Then%>
              <td align="center"></td>
                           <%--<td align="center"><input id="Checkbox8" name="<%= "extra_" & k %>" type="checkbox" disabled  /></td> --%>
                       <% ElseIf (k = numColonne - 1) And getGGExtra(k) Then%>
              <td align="center"></td>
                            <%--<td align="center"><input id="Checkbox9" name="<%= "extra_" & k %>" type="checkbox" checked  /></td>--%>
                       <% ElseIf (k = numColonne - 1) And Not getGGExtra(k) Then%>
              <td align="center"></td>
                            <%--<td align="center"><input id="Checkbox10" name="<%= "extra_" & k %>" type="checkbox"  /></td>--%>
                       <% End If %>
                <%  ElseIf k < numColonne Then
                        
                        valore_precedente = Request.Form("valore_" & gruppi(i) & "_" & k)
                        If valore_precedente <> "" Then
                            Try
                                valore_precedente = FormatNumber(valore_precedente, 2, , , TriState.False)
                            Catch ex As Exception
                                'NEL CASO IN CUI NEL CASO PRECEDENTE ERA STATO INSERITO UN ERRORE NON NUMERICO
                            End Try
                        End If
                        
                        %>
                        <td align="center"><input id="Text8" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" value="<%= valore_precedente %>"  /></td>
                <%ElseIf k = numColonne And (lastA <> "999" And lastA <> "9999") Then%>
                    <%  'SE E' STATA RICHIESTA L'ELIMINAZIONE DELL'ULTIMA COLONNA NON MOSTRO I VALORI PRECEDENTI DELL'ULTIMA COLONNA
                        If elimina = "" Then
                            valore_precedente = Request.Form("valore_" & gruppi(i) & "_" & k)
                            If valore_precedente <> "" Then
                                Try
                                    valore_precedente = FormatNumber(valore_precedente, 2, , , TriState.False)
                                Catch ex As Exception
                                    'NEL CASO IN CUI NEL CASO PRECEDENTE ERA STATO INSERITO UN ERRORE NON NUMERICO
                                End Try
                            End If
                            %>
                            <td align="center"><input id="Text9" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" value="<%= valore_precedente %>" /></td>
                            <%
                            Else
                            %>
                            <td align="center"><input id="Text10" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" /></td>
                            <%    
                            End If%>
                        
                <% End If%>
           <%
                  k = k + 1
           Loop
           k = 1
               %>
          
          </tr> 
           
          <%  
              i = i + 1
          Loop
          %>
      </table>
   <% End If%>
   
   <table border="0" cellpadding="1" cellspacing="1" width="1024px">
     <tr>
       <td align="center">
         <br />
           <input name="memorizza" type="submit"  title="no Stop sell" value="Salva Tempo-Km" />

         
         &nbsp;&nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Annulla" BackColor="#369061" Font-Bold="True" Font-Size="Small" ForeColor="White" />
       </td>
     </tr>
   </table>
   
   <% If Request.Form("last_id_tempo_km") <> "" Then%>
       <input id="Text13" name="last_id_tempo_km" value="<%=Request.Form("last_id_tempo_km") %>" type="hidden" style="width:50px;" />
   <% Else%>
       <input id="Text14" name="last_id_tempo_km" value="<%=lbl_id_codice.Text %>" type="hidden" style="width:50px;" />
   <%End If %>
   
   <asp:Label ID="lbl_id_codice" runat="server" name="lbl_id_codice" visible="false" ></asp:Label>
   <asp:Label ID="txtQuery" runat="server" name="lbl_id_codice" visible="false" ></asp:Label>
  </asp:Panel>
  
  <asp:SqlDataSource ID="sqlTempoKm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice, valido_da, valido_a, data_creazione, iva_inclusa, id_aliquota_iva FROM tempo_km WHERE attivo='1' ORDER BY id DESC"></asp:SqlDataSource>
  

  
    <asp:SqlDataSource ID="sqlIva" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [aliquote_iva] ORDER BY [id]"></asp:SqlDataSource>
  

  
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="invia_lista" />    
</asp:Content>

