<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="tempo_km.aspx.vb" Inherits="tempo_km" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .style1
    {
        width: 453px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table border="0" cellpadding="0" cellspacing="0" width="1000px" >
         <tr>
           <td align="left" class="style1">
             <b>Codice</b>
           </td>
           <td align="left">
             <b>Data inserimento</b>
           </td>
         </tr>
         <tr>
           <td align="left" class="style1">
               <asp:DropDownList ID="dropTempoKm" runat="server" AutoPostBack="True" 
                   DataSourceID="sqlTempoKm" DataTextField="codice" DataValueField="codice" 
                   AppendDataBoundItems="True" Height="19px">
                   <asp:ListItem Selected="True" Value="0">Nuovo tmp+km...</asp:ListItem>
               </asp:DropDownList>
&nbsp;
               <asp:TextBox ID="txtCodice" runat="server"></asp:TextBox>
           </td>
           <td align="left">
               <asp:Label ID="lblDataInserimento" runat="server" Text=""></asp:Label>
             </td>
         </tr>
</table>

<br />
               
<br />

<% 
    
    'PER PRIMA COSA SELEZIONO TUTTI I GRUPPI PRESENTI A SISTEMA
    Dim gruppi(20) As String
    gruppi = getGruppi()
    
    Dim numColonne As Integer
    
    If txtCodice.Text <> "" Then
        numColonne = getNumColonne(txtCodice.Text)
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
                    valore = Request.Form("valore_" & gruppi(i) & "_" & k)
                
                    modifica_valore(gruppi(i), valore, k)
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
            Dim da As String = Request.Form("da_" & numColonne)
            Dim a As String = Request.Form("a_" & numColonne)
             
            Dim valore As String
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
                  i = 1
            
                  'SE TUTTI I VALORI DA SALVARE SONO CORRETTI PROCEDO CON L'INSERIMENTO DELLA COLONNA 
                  Do While gruppi(i) <> "000"
                      valore = Request.Form("valore_" & gruppi(i) & "_" & numColonne)
                
                      'Response.Write(gruppi(i) & " " & da & " " & a & " " & valore & "<br>")
                      memorizza_valore(gruppi(i), da, a, valore, numColonne)
               
                      i = i + 1
                  Loop
            
                  i = 0
             
                  numColonne = getNumColonne(txtCodice.Text)
              End If
              
              'SE E' UNA NUOVA TARIFFA FACCIO IN MODO DA AVERLA SELEZIONATA NELLA DROP
              If dropTempoKm.SelectedValue = "0" Then
                  txtCodice.ReadOnly = False
              End If
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------
    
        Dim lastDa As Integer = 1
        Dim lastA As Integer = 0
 %>
   <input name="aggiungi" type="submit"  title="no Stop sell" value="Aggiungi colonna" />&nbsp;&nbsp;
   
   <% If txtCodice.Text <> "" Then%>
       <input name="modifica" type="submit"  title="Modifica" value="Modifica" />&nbsp;&nbsp;
   <% End If %>
   
   <table border="1" cellpadding="1" cellspacing="1">
      <%  Do While gruppi(i) <> "000"
              %> 
         <tr>
           <% If i = 0 Then%>
             <td><b>Gruppo</b></td>
           <%Else%>
             <td align="center"><b><%=gruppi(i)%></b></td>
           <% End If %>
         
           <% Do While k <= numColonne%>   
        
               <% If i = 0 Then%>
                        <% If k < numColonne Then%>
                            <%  lastDa = getColonnaDa(txtCodice.Text, k)
                                lastA = getColonnaA(txtCodice.Text, k)
                                %>
                             <td ><input id="Text2" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastDa %>" readonly="true" /> - <input id="Text3" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA%>" readonly="true" /></td>
                        <%Else%>
                             <td ><input id="<%= "da_" & k %>" name="<%= "da_" & k %>" type="text" maxlength="3" style="width:20px;" value="<%=lastA + 1 %>" readonly="true" /> - <input id="<%= "a_" & k %>" name="<%= "a_" & k %>" type="text" maxlength="3" style="width:20px;" /></td>
                        <%End If %>
               <%ElseIf k < numColonne Then%>
                        <td align="center"><input id="Text1" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" value="<%=getValore(txtCodice.Text,gruppi(i),k) %>"  /></td>
               <%ElseIf k = numColonne Then%>
                        <td align="center"><input id="<%= "valore_" & gruppi(i) & "_" & k %>" name="<%= "valore_" & gruppi(i) & "_" & k %>" type="text" style="width:50px;" /></td>
               <% End If %>
           
           <%  k = k + 1
           Loop
           k = 1%>
           
         </tr>
      <% 
          i = i + 1
      Loop
      %>   
   </table>
 
 
<asp:SqlDataSource ID="sqlTempoKm" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT DISTINCT codice FROM [tempo_km] ORDER BY [codice]"></asp:SqlDataSource>
</asp:Content>