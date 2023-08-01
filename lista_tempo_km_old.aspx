<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="lista_tempo_km.aspx.vb" Inherits="tariffe_lista_tempo_km" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .style1
        {
            width: 230px;
        }
        .style2
        {
            width: 129px;
        }
        .style3
        {
            width: 83px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
    </asp:toolkitscriptmanager>
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Tempo-Km</b>
          
           </td>
         </tr>
     </table>
  
  <table border="0" cellpadding="2" cellspacing="0" width="1024px" runat="server" id="tab_cerca">
    <tr>
      <td class="style2">
        Codice
      </td>
      <td class="style3">
         Valido Da
      </td>
      <td>
         Valido A
      </td>
    </tr>
     <tr>
     <td class="style2">
        
         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        
      </td>
      <td class="style3">
         
                       
           
               <asp:TextBox ID="txtValidoDa" runat="server" Width="70px"></asp:TextBox>
               <asp:CalendarExtender ID="txtValidoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtValidoDa">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="txtValidoDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtValidoDa">
                </asp:MaskedEditExtender>    
                         
           
           
      </td>
      <td>
        
                       
           
               <asp:TextBox ID="txtValidoA" runat="server" Width="70px"></asp:TextBox>
               <asp:CalendarExtender ID="txtValidoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtValidoA">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="txtValidoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtValidoA">
                </asp:MaskedEditExtender>    
                         
           
           
      &nbsp;&nbsp;
            
            <asp:Button ID="btnCerca" runat="server" Text="Cerca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
                         
           
           
            &nbsp;&nbsp;
            
            <asp:Button ID="btnNuovo" runat="server" Text="Nuovo Tempo-KM" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
                         
           
           
      </td>
    </tr>
    <tr> 
      <td colspan="3">
          <asp:ListView ID="listTempoKm" runat="server" DataKeyNames="id" DataSourceID="sqlTempoKm">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC;color: #000000;">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' />
                      </td>
                      <td>
                          <asp:Label ID="data_creazioneLabel" runat="server" 
                              Text='<%# Eval("data_creazione") %>' />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="vedi" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' />
                      </td>
                      <td>
                          <asp:Label ID="data_creazioneLabel" runat="server" 
                              Text='<%# Eval("data_creazione") %>' />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                      </td>
                      <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="vedi" />
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
                              <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                  <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                      <th runat="server" align="left">
                                          Codice</th>
                                      <th runat="server" align="left">
                                          Valido da</th>
                                      <th runat="server" align="left">
                                          Valido fino a</th>
                                      <th runat="server" align="left">
                                          Data creazione</th>
                                      <th></th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr runat="server">
                          <td runat="server" style="">
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
              <EditItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                              Text="Aggiorna" />
                          <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                              Text="Annulla" />
                      </td>
                      <td>
                          <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                      </td>
                      <td>
                          <asp:TextBox ID="codiceTextBox" runat="server" Text='<%# Bind("codice") %>' />
                      </td>
                      <td>
                          <asp:TextBox ID="valido_daTextBox" runat="server" 
                              Text='<%# Bind("valido_da") %>' />
                      </td>
                      <td>
                          <asp:TextBox ID="valido_aTextBox" runat="server" 
                              Text='<%# Bind("valido_a") %>' />
                      </td>
                      <td>
                          <asp:TextBox ID="data_creazioneTextBox" runat="server" 
                              Text='<%# Bind("data_creazione") %>' />
                      </td>
                  </tr>
              </EditItemTemplate>
              <SelectedItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                      </td>
                      <td>
                          <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_daLabel" runat="server" Text='<%# Eval("valido_da") %>' />
                      </td>
                      <td>
                          <asp:Label ID="valido_aLabel" runat="server" Text='<%# Eval("valido_a") %>' />
                      </td>
                      <td>
                          <asp:Label ID="data_creazioneLabel" runat="server" 
                              Text='<%# Eval("data_creazione") %>' />
                      </td>
                  </tr>
              </SelectedItemTemplate>
          </asp:ListView>
      </td>
    </tr>
  </table>
  <asp:Panel ID="tab_vedi" runat="server" Visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="1000px" >
         <tr>
           <td align="left" class="style1">
             <b>Codice</b>
               :
               <asp:Label ID="lblCodice" runat="server" Text=""></asp:Label>
               <asp:Label ID="lbl_id_codice" runat="server" visible="false"></asp:Label>
           </td>
           <td align="left">
               &nbsp;</td>
         </tr>

</table>

<br />
              

<% 
    
    'PER PRIMA COSA SELEZIONO TUTTI I GRUPPI PRESENTI A SISTEMA
    Dim gruppi(20) As String
    gruppi = getGruppi()
    
    'CONTROLLO SE E' STATA RICHIESTA L'ELIMINAZIONE DELL'ULTIMA COLONNA------------------------------------------------------------------
    Dim elimina As String
    elimina = Request.Form("elimina")
    
    If elimina <> "" Then
        EliminaUltimaColonna()
    End If
    '------------------------------------------------------------------------------------------------------------------------------------
    
    Dim numColonne As Integer
    
    If lbl_id_codice.Text <> "" Then
        numColonne = getNumColonne(lbl_id_codice.Text)
    Else
        numColonne = 1
    End If
   
    Dim i As Integer = 0
    Dim k As Integer = 1

    'CONTROLLO SE E' STATA RICHIESTA LA MODIFICA DELLA TABELLA---------------------------------------------------------------------------
    Dim modifica As String
    modifica = Request.Form("modifica")
    
    If modifica <> "" Then
        Dim valore As String
        Dim valori_corretti As Boolean = True
        Dim test As Double
        Dim packed As String
        
        'PER PRIMA COSA CONTROLLO CHE NON SIANO STATI PASSATI VALORI NON CORRETTI
        
        i = 1
        k = 1
        
        Try
            Do While gruppi(i) <> "000"
                Do While k < numColonne
                    valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                    test = CDbl(valore)
                    
                    If test < 0 Then
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
            %> 
               <asp:Label ID="lblErrore" runat="server" Font-Bold="True" ForeColor="Red" Text="Rilevato uno o più valori non corretti: modifica interrotta." ></asp:Label><br />
            <%
        End Try
        
            i = 0
            k = 1
            
        If valori_corretti Then
            'IN CASO POSITIVO PROCEDO CON LA MODIFICA DELLA TABELLA
            i = 1
            k = 1
        
            Do While gruppi(i) <> "000"
                    Do While k < numColonne
                        packed = Request.Form("packed_" & k)

                        If packed <> "" Then
                            packed = "1"
                        Else
                            packed = "0"
                        End If
                        valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                
                        modifica_valore(gruppi(i), valore, k, packed)
                        k = k + 1
                    Loop
            
                k = 1
                i = i + 1
            Loop
        
            i = 0
            k = 1
        End If
            
    End If
    '-------------------------------------------------------------------------------------------------------------------------------------
    
    'CONTROLLO SE E' STATO RICHIESTO IL SALVATAGGIO DI UNA COLONNA------------------------------------------------------------------------
    Dim aggiungi As String
    aggiungi = Request.Form("aggiungi")
    
        If aggiungi <> "" Then
            Dim da As String
            Dim a As String
            'If aggiungi = "Aggiungi EXTRA" Then
            '    'SE E' STATO CHIESTO DI AGGIUNGERE LA COLONNA EXTRA DAY ALLORA SETTO DA E A  AL VALORE 999
            '    da = Request.Form("da_" & numColonne)
            '    a = 999
            'Else
            'ALTRIMENTI SELEZIONO I VALORI PASSATI DALL'UTENTE
            da = Request.Form("da_" & numColonne)
            a = Request.Form("a_" & numColonne)
            'End If
           
             
            Dim valore As String
            Dim packed As String
            Dim valori_corretti As Boolean = True
            Dim test_dbl As Double
            Dim test_int As Integer
        
            'PER PRIMA COSA CONTROLLO CHE NON SIANO STATI PASSATI VALORI NON CORRETTI O VUOTI
            
            Try
                test_int = CInt(da)
                If test_int < 0 Then
                    valori_corretti = False
                End If
                test_int = CInt(a)
                If test_int < 0 Then
                    valori_corretti = False
                End If
                
                If CInt(a) < CInt(da) Then
                    valori_corretti = False
                End If
                
                Do While gruppi(i) <> "000"
                    valore = Request.Form("valore_" & gruppi(i) & "_" & numColonne)
                    test_dbl = CDbl(valore)
                    If test_dbl < 0 Then
                        valori_corretti = False
                    End If
                    
                    i = i + 1
                Loop
            Catch ex As Exception
                'NEL CASO IN CUI VENGA TROVATO UN VALORE NON CORRETTO NON PROSEGUO CON L'INSERIMENTO ED AVVERTO L'UTENTE
             
                valori_corretti = False
                %> 
                  <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Text="Rilevato uno o più valori non corretti: inserimento colonna non eseguita." ></asp:Label><br />
                <%
              End Try
              
              i = 0
              If valori_corretti Then
                  'SE SI STA INSERENDO UN NUOVO tempo_km CONTROLLO CHE IL NOME NON SIA GIA' STATO USATO

                    i = 2
                      packed = Request.Form("packed_" & numColonne)
                    If packed <> "" Then
                        packed = "1"
                    Else
                        packed = "0"
                    End If
                      'SE TUTTI I VALORI DA SALVARE SONO CORRETTI PROCEDO CON L'INSERIMENTO DELLA COLONNA 
                      Do While gruppi(i) <> "000"
                          valore = Request.Form("valore_" & gruppi(i) & "_" & numColonne)
                
                          'Response.Write(gruppi(i) & " " & da & " " & a & " " & valore & "<br>")
                          memorizza_valore(gruppi(i), da, a, valore, numColonne, packed)
               
                          i = i + 1
                      Loop
            
                      i = 0
             
                    numColonne = getNumColonne(lbl_id_codice.Text)
            
                        End If
              End If
              '-------------------------------------------------------------------------------------------------------------------------------------
              'I PULSANTI DI AGGIUNTA DEVONO ESSERE VISIBILI SOLAMENTE FINCHE' LA TARIFFA NON E' "CHIUSA" CON LA COLONNA EXTRA DAY
              Dim esisteExtra As Boolean = esiste_extra()
              
              Dim lastDa As Integer = 1
              Dim lastA As Integer = 0
 %>
    <% If Not esisteExtra Then%>
       <input name="aggiungi" type="submit"  title="no Stop sell" value="Aggiungi colonna" />&nbsp;&nbsp;
    <% Else %>
        <%--IN QUESTO CASO PERMETTO DI SETTARE COME MASTER IL TMP+KM ATTUALE--%>
    <% End If%>
   
   <% If numColonne > 1 Then%>
     <%--<% If Not esisteExtra Then%>
       <input name="aggiungi" type="submit"  title="no Stop sell" value="Aggiungi EXTRA" />&nbsp;&nbsp;
     <% End If%>--%>
       <input name="modifica" type="submit"  title="Modifica" value="Modifica" />&nbsp;&nbsp;
       <input name="elimina" type="submit"  title="Elimina ultima colonna" value="Elimina ultima colonna" />&nbsp;&nbsp;
   <% End If %>
   
   <br />
   <br /> 
   
   
   <table border="1" cellpadding="1" cellspacing="1">
      <%  Do While gruppi(i) <> "000"
              %> 
         <tr>
           <% If i = 0 Then%>
                <td align="center"><b>PACK</b></td>
           <%ElseIf i = 1 Then%>
              <td><b>Gruppo</b></td>
           <%Else%>
             <td align="center"><b><%=gruppi(i)%></b></td>
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
                             <td ><input id="Text2" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastDa %>" readonly="true" /> - <input id="Text3" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA%>" readonly="true" /></td>
                        <%ElseIf lastA <> "999" Then%>
                             <%--CREO UNA NUOVA COLONNA SOLAMENTE SE NON E' PRESENTE LA COLONNA EXTRA DAY--%>
                             <td ><input id="<%= "da_" & k %>" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA + 1 %>" readonly="true" /> - <input id="<%= "a_" & k %>" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" /></td>
                        <%End If %>
                
               <%ElseIf k < numColonne Then%>
                        <td align="center"><input id="Text1" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" value="<%=getValore(lbl_id_codice.Text,gruppi(i),k) %>"  /></td>
               <%ElseIf k = numColonne And lastA <> "999" Then%>
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
  </asp:Panel>
  
  
  <asp:SqlDataSource ID="sqlTempoKm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [codice], [valido_da], [valido_a], [data_creazione] FROM [tempo_km] ORDER BY [id] DESC"></asp:SqlDataSource> 
</asp:Content>

