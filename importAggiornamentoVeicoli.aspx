<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="importAggiornamentoVeicoli.aspx.vb" Inherits="importAggiornamentoVeicoli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <meta http-equiv="Expires" content="0" />
      <meta http-equiv="Cache-Control" content="no-cache" />
      <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <table border="0" cellpadding="0" cellspacing="0" width="1024px">
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" >
               <asp:Label ID="Label28" runat="server" Text="Aggiornamento Veicoli" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
     </table> 
    <table>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="Tipo Aggiornamento" 
                    CssClass="testo"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dropTipo" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="1">Dati di acquisto</asp:ListItem>
                    <asp:ListItem Value="2">Dati di vendita</asp:ListItem>
                </asp:DropDownList>
            </td>
         </tr>
        <tr>
            <td>
                <asp:Label ID="Label23" runat="server" Text="Specificare file da importare" CssClass="testo"></asp:Label>
            </td>
            <td>
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                     <Triggers>
                        <asp:PostBackTrigger ControlID="btnImportaFile" />
                     </Triggers>
                     <ContentTemplate>
                     
                       <asp:FileUpload ID="FileUpload2" runat="server" />
                       <asp:Button ID="btnImportaFile" runat="server" Text="Importa file" />
                       &nbsp;&nbsp;                
                        <asp:Label ID="lblErrore2" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                        &nbsp;<asp:Label ID="LblImportFile" runat="server" Text="Label" Visible="False"></asp:Label>
                       </ContentTemplate>
                </asp:UpdatePanel>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
                    SelectCommand="SELECT * FROM veicoli WITH(NOLOCK)">
                </asp:SqlDataSource>     
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
                    SelectCommand="SELECT * FROM movimenti_targa WITH(NOLOCK)">
                </asp:SqlDataSource>                           
            </td>                
         </tr>
         <tr>
            <td class="style1">&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
    </table>
    <% 
        If LblImportFile.Text = "CaricaFile" And dropTipo.SelectedValue = "1" Then
    %>
   <div style="width:1024px;height: 480px; overflow:scroll;" >
    <table border="1" width="2710px">
         <tr style="background-color:#19191b;color: #FFFFFF; font-weight:bold;">
            <td width="16px">
                
            </td>
            <td width="80px">
                <asp:Label ID="Label7" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="220px">
                <asp:Label ID="Label8" runat="server" Text="Telaio" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="250px">
                <asp:Label ID="Label9" runat="server" Text="Modello" CssClass="testo_titolo"></asp:Label> 
                <asp:ImageButton ID="btnModello" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px; width: 16px;" />
            </td>
            <td width="180px">
                <asp:Label ID="Label10" runat="server" Text="Colore" CssClass="testo_titolo"></asp:Label> 
                <asp:ImageButton ID="btnColore" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />            
            </td>
            <td width="80px">
                <asp:Label ID="Label11" runat="server" Text="Immatricolazione" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="220px">
                <asp:Label ID="Label12" runat="server" Text="Proprietario" CssClass="testo_titolo"></asp:Label> 
                <asp:ImageButton ID="btnProprietario" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td>
            <td width="80px">
                <asp:Label ID="Label13" runat="server" Text="Escludi ammortamento" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label14" runat="server" Text="Inizio Leasing" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label15" runat="server" Text="Fine Leasing" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label16" runat="server" Text="Canone mensile" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label17" runat="server" Text="Mesi" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="240px">
                <asp:Label ID="Label18" runat="server" Text="Ente" CssClass="testo_titolo"></asp:Label>
                 <asp:ImageButton ID="btnEnte" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" /> 
            </td>
            <td width="80px">
                <asp:Label ID="Label19" runat="server" Text="Km inclusi" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label21" runat="server" Text="Addiz.100 Km EXTRA" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label22" runat="server" Text="Rimborso 100 Km EXTRA" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label20" runat="server" Text="Franchigia Km inclusi" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label25" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label26" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label27" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label29" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label30" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label31" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label32" runat="server" Text="Num.NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label33" runat="server" Text="Data NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label34" runat="server" Text="Importo NC" CssClass="testo_titolo"></asp:Label> 
            </td>
         </tr>
         <%
             Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
             Dbc.Open()
             Dim Cmd As New Data.SqlClient.SqlCommand("SELECT CONVERT(char(10),data_immatricolazione,103) As data_immatricolazione ,* FROM veicoli_appoggio WITH(NOLOCK)", Dbc)
             'Response.Write(Cmd.CommandText & "<br><br>")
             Dim Rs As Data.SqlClient.SqlDataReader
             Rs = Cmd.ExecuteReader()
             Do While Rs.Read %>
                 
            <tr>
                <td>
                  <% If Rs("riga_ok") = "False" Then%>
                    <img src="images/meno.jpg" width="13" height="13" />
                  <% End If%>
                </td>
                <!-- TARGA -->
                <%
                    Select Case Rs("presente_targa")
                        Case Is = 0 'Rosso campo obbligatorio non valorizzato
                            If Rs("targa") = "" Then%>
                                <td  bgcolor="#FF3333"  >
                                    &nbsp;
                                </td>  
                            <%
                            Else
                                %>
                                <td bgcolor="#FFFF99"> 
                                    <%=Rs("targa")%>
                                </td>  
                            <%                                
                            End If
                        Case Is = 1
                           %> 
                            <td  bgcolor="#369061"  >
                                <%=Rs("targa")%>
                            </td>  
                          <%
                    End Select
                %>                                                                                        
            <!-- TELAIO -->            
            <%
                Select Case Rs("presente_telaio")
                    Case Is = 0
                        If Rs("telaio") = "" Then%>
                                <td>
                                    &nbsp;
                                </td>   
                            <%
                            Else %>
                                <td bgcolor="#369061">
                                    <%=Rs("telaio")%>
                                 </td>   
                            <%                                
                            End If
                        Case Is = 1 %>
                            <td bgcolor="#9999FF"  >
                                <%=Rs("telaio")%>
                             </td>   
                            <%                                            
                    End Select
                %>                                                                                        
                                            
            
            <!-- MODELLO -->            
            <%
                Select Case Rs("presente_modello")
                    Case Is = 0
                        If Rs("modello") = "" Then%>
                                <td>
                                     &nbsp;
                                </td>
                            <%
                            Else %>
                                <td  bgcolor="#FFFF99"  >
                                    <%=Rs("modello")%>
                                </td>
                            <%                                
                            End If
                        Case Is = 1 %>
                            <td bgcolor="#369061">
                               <%=Rs("modello")%>      
                            </td>                      
                            <%                                            
                    End Select
                %>                                                                                        
            <!-- COLORE -->            
            <%
                Select Case Rs("presente_colore")
                    Case Is = 0
                        If Rs("colore") = "" Then%>
                                <td>
                                 &nbsp;
                                </td>
                            <%
                            Else %>
                                <td  bgcolor="#FFFF99"  >
                                    <%=Rs("colore")%>
                                </td>
                            <%                                
                            End If
                        Case Is = 1 %>
                            <td bgcolor="#369061">
                               <%=Rs("colore")%>      
                            </td>                      
                            <%                                            
                    End Select
                %>                                                                                        
            <!-- Data Immatricolazione -->            
            <%                
                If (Rs("data_immatricolazione") & "") = "01/01/1900 0.00.00" Or (Rs("data_immatricolazione") & "") = "" Then%>
                    <td>
                        &nbsp;
                    </td>
                <%
                Else
                    Dim test_data As String
                    Try
                        test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_immatricolazione"))
                            %>
                               <td bgcolor="#369061">
                                  <%=Rs("data_immatricolazione")%>
                               </td> 
                            <%         
                    Catch ex As Exception
                            %>
                               <td bgcolor="#b4679d">
                                  <%=Rs("data_immatricolazione")%>
                               </td> 
                            <%        
                    End Try
                                          
                End If
                %>                                                                                        
                     
            
            <!-- PROPRIETARIO -->            
            <%
                Select Case Rs("presente_proprietario")
                    Case Is = 0
                        If Rs("proprietario") = "" Then%>
                                <td>
                                    &nbsp;
                                </td>                          
                            <%
                            Else %>
                                <td  bgcolor="#FFFF99">
                                    <%=Rs("proprietario")%>
                                </td>  
                            <%                                
                            End If
                        Case Is = 1 %>
                            <td bgcolor="#369061">
                               <%=Rs("proprietario")%>    
                            </td>                          
                            <%                                            
                    End Select
                %>                                                                                              
            <!-- Escludi Ammortamento -->                      
                <%                                                                   
                    If UCase(Rs("escludi_ammortamento")) = "SI" Or UCase(Rs("escludi_ammortamento")) = "NO" Then%>
                         <td bgcolor="#369061">
                            <%=Rs("escludi_ammortamento") & ""%>
                         </td> 
                    <% ElseIf Trim(Rs("escludi_ammortamento")) = "" Then%>     
                         <td>
                            &nbsp;
                         </td> 
                    <%  Else %>
                         <td bgcolor="#b4679d">
                            <%=Rs("escludi_ammortamento")%>  
                         </td>
                    <%                                
                    End If
                    %>                       
            
            <%  
                
                    'CONTROLLO PRIMA SE LE DATA FINALE E' PRECEDENTE A QUELLA INIZIALE (SALVO QUESTA INFORMAZIONE SOLO SE LE DATE SONO
                    'TUTTE E DUE SPECIFICATE E SONO CORRETTE MA CON QUESTO ERRORE)
                    
                    If Rs("data_finale_precedente") Then
                        %>
                          <td bgcolor="#b4679d">
                               <%=Rs("data_inizio_leasing")%>  
                          </td>
                          <td bgcolor="#b4679d">
                               <%=Rs("data_fine_leasing")%>  
                          </td>
                        <%
                    Else
                        'INIZIO LEASING -----------------------------------------------------------------------------------------------------
                            If (Rs("data_inizio_leasing") & "") = "" Then
                            %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                            <%
                            Else
                                    Try
                                        Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(Rs("data_inizio_leasing"))
                                        %>
                                          <td bgcolor="#369061">
                                              <%=Rs("data_inizio_leasing")%>  
                                          </td>
                                        <%
                                    Catch ex As Exception
                                        %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("data_inizio_leasing")%>  
                                          </td>
                                 <%
                                    End Try
                             End If
                             'FINE LEASING----------------------------------------------------------------------------------------------------
                             If (Rs("data_fine_leasing") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(Rs("data_fine_leasing"))
                                        %>
                                          <td bgcolor="#369061">
                                              <%=Rs("data_fine_leasing")%>  
                                          </td>
                                        <%
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("data_fine_leasing")%>  
                                          </td>
                                  <%
                                    End Try
                              End If 
                          End If
                          'CANONE ---------------------------
                          If (Rs("canone_mensile") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_dbl As Double = CDbl(Rs("canone_mensile"))
                                      If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("canone_mensile")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("canone_mensile")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("canone_mensile")%>  
                                          </td>
                                  <%
                                    End Try
                              End If    
                              'DURATA IN MESI ---------------------------
                              If (Rs("mesi_leasing") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_int As Integer = CInt(Rs("mesi_leasing"))
                                      If test_int > 0 And Not (Rs("mesi_leasing").Contains(",") Or Rs("mesi_leasing").Contains(".")) Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("mesi_leasing")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("mesi_leasing")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("mesi_leasing")%>  
                                          </td>
                                  <%
                                    End Try
                              End If    
                              'ENTE------------------------------------------------
                              If (Rs("ente_finanziatore") & "") = "" Then
                                  %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  If (Rs("id_ente_finanziatore") & "") = "" Then
                                      %>
                                      <td  bgcolor="#FFFF99">
                                            <%=Rs("ente_finanziatore")%>
                                      </td>  
                                  <%
                                  Else
                                          %>
                                      <td  bgcolor="#369061">
                                            <%=Rs("ente_finanziatore")%>
                                      </td>  
                                  <%
                                  End If
                              End If
                              'KM INCLUSI ---------------------------
                              If (Rs("km_compresi") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_int As Integer = CInt(Rs("km_compresi"))
                                      If test_int >= 0 And Not (Rs("km_compresi").Contains(",") Or Rs("km_compresi").Contains(".")) Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("km_compresi")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("km_compresi")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("km_compresi")%>  
                                          </td>
                                  <%
                                    End Try
                              End If
                              'FRANCHIGIA KM INCLUSI---------------------------
                              If (Rs("franchigia_km_compresi") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_dbl As Double = CDbl(Rs("franchigia_km_compresi"))
                                      If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("franchigia_km_compresi")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("franchigia_km_compresi")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("franchigia_km_compresi")%>  
                                          </td>
                                  <%
                                    End Try
                              End If
                              'FRANCHIGIA KM INCLUSI---------------------------
                              If (Rs("addizionale_100_extra") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_dbl As Double = CDbl(Rs("addizionale_100_extra"))
                                      If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("addizionale_100_extra")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("addizionale_100_extra")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("addizionale_100_extra")%>  
                                          </td>
                                  <%
                                    End Try
                              End If
                              'RIMBORSO KM INCLUSI---------------------------
                              If (Rs("rimborso_100_extra") & "") = "" Then
                                %>
                                  <td>
                                      &nbsp;
                                  </td>                          
                              <%
                              Else
                                  Try
                                      Dim test_dbl As Double = CDbl(Rs("rimborso_100_extra"))
                                      If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("rimborso_100_extra")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("rimborso_100_extra")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("rimborso_100_extra")%>  
                                          </td>
                                  <%
                                    End Try
                              End If
                              'FATTURE  
                          If (Rs("numero_prima_fattura") & "") = "" And (Rs("data_prima_fattura") & "") = "" And (Rs("importo_prima_fattura") & "") = "" Then%>   
                             <td>              
                             </td>
                             <td>              
                             </td>
                             <td>              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_prima_fattura") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_prima_fattura")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_prima_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                            test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_prima_fattura")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_prima_fattura")%>
                                                   </td> 
                                                <%        
                                        End Try
                                                              
                                    End If
                                         
                                               
                                            If (Rs("importo_prima_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_prima_fattura"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If
                       'II FATTURA        
                          
                          If (Rs("numero_seconda_fattura") & "") = "" And (Rs("data_seconda_fattura") & "") = "" And (Rs("importo_seconda_fattura") & "") = "" Then%>   
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_seconda_fattura") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_seconda_fattura")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_seconda_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                        test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_seconda_fattura")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_seconda_fattura")%>
                                                   </td> 
                                                <%        
                                        End Try
                                                              
                                    End If
                                         
                                               
                                            If (Rs("importo_seconda_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_seconda_fattura"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If
                         'I NOTA DI CREDITO   
                          If (Rs("numero_prima_nc") & "") = "" And (Rs("data_prima_nc") & "") = "" And (Rs("importo_prima_nc") & "") = "" Then%>   
                             <td>              
                             </td>
                             <td>              
                             </td>
                             <td>              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_prima_nc") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_prima_nc")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_prima_nc") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                        test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_prima_nc")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_prima_nc")%>
                                                   </td> 
                                                <%        
                                        End Try
                                                              
                                    End If
                                         
                                               
                                            If (Rs("importo_prima_nc") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_prima_nc"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If%>
            </tr>
                 
             <%
             Loop

             Rs.Close()
             Dbc.Close()
             Rs = Nothing
             Dbc = Nothing
             
         %>         
     </table> 
    </div>
     <br /><br />
     
     <!--Legenda -->
     <table style="width:100%;" border="1">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label4" runat="server" Text="Legenda:" CssClass="testo"></asp:Label></td></tr><tr>
            <td  bgcolor="#FF3333">&nbsp;</td><td><asp:Label ID="Label3" runat="server" Text="Valore obbligatorio." CssClass="testo"></asp:Label></td></tr><tr>
            <td  bgcolor="#9999FF">&nbsp;</td><td><asp:Label ID="Label2" runat="server" Text="Valore già presente sul database." CssClass="testo"></asp:Label></td></tr><tr>
            <td  bgcolor="#FFFF99">&nbsp;</td><td><asp:Label ID="Label1" runat="server" Text="Valore non presente sul database." CssClass="testo"></asp:Label></td></tr><tr>
            <td  bgcolor="#b4679d">&nbsp;</td><td><asp:Label ID="Label6" runat="server" Text="Valore non corretto." CssClass="testo"></asp:Label></td></tr><tr>
            <td  bgcolor="#369061">&nbsp;</td><td><asp:Label ID="Label5" runat="server" Text="Valore Ok." CssClass="testo"></asp:Label></td></tr></table>
    <% End If
        If LblImportFile.Text = "CaricaFile" And dropTipo.SelectedValue = "2" Then%>
    <div style="width:1024px;height: 480px; overflow:scroll;" >
      <table style="width:2180px;" border="1">
         <tr style="background-color:#19191b; font-weight:bold;">
            <td width="80px">
                <asp:Label ID="Label35" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="80px">
                <asp:Label ID="Label36" runat="server" Text="DDT" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="80px">
                <asp:Label ID="Label37" runat="server" Text="Data DDT" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="220px">
                <asp:Label ID="Label38" runat="server" Text="Presso Di" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnPressoDi" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td>
            <td width="80px">
                <asp:Label ID="Label39" runat="server" Text="Data Atto Vendita " CssClass="testo_titolo"></asp:Label>   
            </td>
            <td width="220px">
                <asp:Label ID="Label40" runat="server" Text="Venditore" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnVenditore" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td>
            <td width="220px">
                <asp:Label ID="Label41" runat="server" Text="Acquirente" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnAcqirente" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td> 
            <td width="80px">
                <asp:Label ID="Label42" runat="server" Text="Leasing (SI/NO)" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="400px">
                <asp:Label ID="Label43" runat="server" Text="Note" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="80px">
                <asp:Label ID="Label44" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label45" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label46" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label47" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label48" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label49" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label50" runat="server" Text="Num.NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label51" runat="server" Text="Data NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label52" runat="server" Text="Importo NC" CssClass="testo_titolo"></asp:Label> 
            </td>
         </tr>
          <%
              Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
             Dbc.Open()
              Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM dismissioni_appoggio WITH(NOLOCK) WHERE id_utente='" & Request.Cookies("SicilyRentCar")("idUtente") & "'", Dbc)
             Dim Rs As Data.SqlClient.SqlDataReader
              Rs = Cmd.ExecuteReader()
              
              Dim leasing As Boolean
              
              Do While Rs.Read
                  If (Rs("veicolo_venduto") & "") = "False" Then
                      
                  %>
                      <tr>
                        <td bgcolor="#9999FF">
                           <%=Rs("targa") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("DDT") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("data_ddt") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("presso_di") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("data_atto_di_vendita") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("venditore") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("acquirente") & ""%>
                        </td> 
                        <td bgcolor="#9999FF">
                            <%=Rs("leasing") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("Note") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("numero_prima_fattura") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("data_prima_fattura") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("importo_prima_fattura") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("numero_seconda_fattura") & ""%>
                        </td> 
                        <td bgcolor="#9999FF">
                            <%=Rs("data_seconda_fattura") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("importo_seconda_fattura") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("numero_prima_nc") & ""%>
                        </td> 
                        <td bgcolor="#9999FF">
                            <%=Rs("data_prima_nc") & ""%>
                        </td>
                        <td bgcolor="#9999FF">
                            <%=Rs("importo_prima_nc") & ""%>
                        </td>
                     </tr>
                <%
                Else
                    If Rs("veicolo_is_leasing") Then
                        leasing = True
                    Else
                        leasing = False
                    End If
                 %>
                      <tr>
                      <%'TARGA-------------------------------------------------------------------------------------------------- %>
                       <%  If (Rs("id_veicolo") & "") = "" And Rs("targa") <> "" Then
                               'VEICOLO NON TROVATO%>
                             <td bgcolor="#FFFF99">
                                <%=Rs("targa") %>
                             </td>
                       <%  ElseIf (Rs("id_veicolo") & "") = "" And Rs("targa") = "" Then
                               'TARGA NON FORNITA  %>
                              <td bgcolor="#FF3333">
                                &nbsp;
                              </td>
                       <% Else
                               %>
                             <td bgcolor="#369061">
                                  <%=Rs("targa") %>
                             </td>
                       <%End If%>
                       <%'NUMERO BOLLA (obbligatorio)------------------------------------------------------------------------------ %>
                        <%  If (Rs("ddt") & "") = "" Then%>
                          <td>
                           &nbsp;
                         </td>
                        <%  Else%> 
                         <td bgcolor="#369061">
                            <%=Rs("ddt")%>
                         </td>
                       <% End If%>
                       <%'DATA BOLLA (obbligatorio)-------------------------------------------------------------------------------- %>
                        <% If (Rs("data_ddt") & "") = "" Then%>
                         <td>
                            &nbsp;
                         </td>
                        <%  Else
                                Dim test As String
                                Try
                                    test = funzioni_comuni.getDataDb_senza_orario(Rs("data_ddt"))
                                    'VALORE CORRETTO
                                    %>
                                     <td bgcolor = "#369061">
                                        <%=Rs("data_ddt")%>
                                     </td>
                                    <%
                                Catch ex As Exception
                                    'VALORE NON CORRETTO
                                    %>
                                     <td bgcolor="#b4679d">
                                        <%=Rs("data_ddt")%>
                                     </td>
                                    <%
                                End Try
                                                                
                                %>
                        <%  End If%>
                       <%'PRESSO DI ---------------------------------------------------------------------------------------------------- %> 
                        <% If (Rs("presso_di") & "") = "" Then%>
                             <td>
                                 &nbsp;
                             </td>
                        <% ElseIf (Rs("id_presso_di") & "") = "" Then%>
                            <td  bgcolor="#FFFF99">
                                 <%=Rs("presso_di")%>
                             </td>
                        <% ElseIf (Rs("id_presso_di") & "") <> "" Then%>
                              <td bgcolor = "#369061">
                                  <%=Rs("presso_di")%>
                             </td>
                        <% End If%>
                       <%'DATA ATTO DI VENDITA ------------------------------------------------------------------------------------- %> 
                          <% If (Rs("data_atto_di_vendita") & "") = "" Then%>
                             <td>
                                 &nbsp;
                             </td>
                          <% Else
                            
                              Dim test As String
                              Try
                                  test = funzioni_comuni.getDataDb_senza_orario(Rs("data_atto_di_vendita"))
                                        %>
                                         <td bgcolor = "#369061">
                                            <%=Rs("data_atto_di_vendita")%>
                                         </td>
                                        <%
                                    Catch ex As Exception
                                       %>
                                         <td bgcolor = "#b4679d">
                                            <%=Rs("data_atto_di_vendita")%>
                                         </td>
                                       <%
                                    End Try
                                    %>

                        <% End If %>
                        <%'VENDITORE ----------------------------------------------------------------------------------------------- %> 
                        <% If leasing And (Rs("venditore") & "") <> "" Then%>
                            <td bgcolor="#b4679d">
                                <%=Rs("venditore")%>
                            </td>
                        <% ElseIf leasing And (Rs("venditore") & "") = "" Then%>
                            <td>
                                &nbsp;
                            </td>
                        <% Else If Not leasing And (Rs("venditore") & "") = "" Then %>
                             <td>
                                 &nbsp;
                             </td>
                        <% ElseIf Not leasing And (Rs("id_venditore") & "") = "" Then%>
                            <td  bgcolor="#FFFF99">
                                 <%=Rs("venditore")%>
                             </td>
                        <% ElseIf Not leasing And (Rs("id_venditore") & "") <> "" Then%>
                              <td bgcolor = "#369061">
                                  <%=Rs("venditore")%>
                             </td>
                        <% End If%>
                       <%'ACQUIRENTE ----------------------------------------------------------------------------------------------- %> 
                        <% If (Rs("acquirente") & "") = "" Then%>
                             <td>
                                 &nbsp;
                             </td>
                        <% ElseIf (Rs("id_acquirente") & "") = "" Then%>
                            <td  bgcolor="#FFFF99">
                                 <%=Rs("acquirente")%>
                             </td>
                        <% ElseIf (Rs("id_acquirente") & "") <> "" Then%>
                              <td bgcolor = "#369061">
                                  <%=Rs("acquirente")%>
                             </td>
                        <% End If%>
                        <%'LEASING SI/NO (obbligatorio)----------------------------------------------------------------------------- %> 
                               <%If leasing = "True" Then%>
                                 <td bgcolor = "#369061">
                                    SI
                                 </td>
                               <% ElseIf leasing="False" Then %>
				 <td bgcolor = "#369061">
                                    NO
                                 </td>
			       <% End If %>
                        <%'--------------------------------------------------------------------------------------------------------- %>
                        <%  If (Rs("note") & "") = "" Then%>
                          <td>
                           &nbsp;
                         </td>
                        <%  Else%> 
                         <td bgcolor="#369061">
                            <%=Rs("note")%>
                         </td>
                       <% End If%>
                        <%  'FATTURE  
                          If (Rs("numero_prima_fattura") & "") = "" And (Rs("data_prima_fattura") & "") = "" And (Rs("importo_prima_fattura") & "") = "" Then%>   
                             <td>              
                             </td>
                             <td>              
                             </td>
                             <td>              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_prima_fattura") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_prima_fattura")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_prima_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                            test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_prima_fattura")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_prima_fattura")%>
                                                   </td> 
                                       <%        
                                        End Try                     
                                    End If                                 
                                               
                                   If (Rs("importo_prima_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_prima_fattura"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_fattura")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If
                          'FATTURA II
                          If (Rs("numero_seconda_fattura") & "") = "" And (Rs("data_seconda_fattura") & "") = "" And (Rs("importo_seconda_fattura") & "") = "" Then%>   
                             <td>              
                             </td>
                             <td>              
                             </td>
                             <td>              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_seconda_fattura") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_seconda_fattura")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_seconda_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                        test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_seconda_fattura")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_seconda_fattura")%>
                                                   </td> 
                                       <%        
                                        End Try                     
                                    End If                                 
                                               
                                   If (Rs("importo_seconda_fattura") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_seconda_fattura"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_seconda_fattura")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If 'I NOTA DI CREDITO   
                          If (Rs("numero_prima_nc") & "") = "" And (Rs("data_prima_nc") & "") = "" And (Rs("importo_prima_nc") & "") = "" Then%>   
                             <td>              
                             </td>
                             <td>              
                             </td>
                             <td>              
                             </td>
                          <% Else%>
                              <% If (Rs("numero_prima_nc") & "") <> "" Then%>   
                                  <td bgcolor="#369061"> 
                                     <%=Rs("numero_prima_nc")%>             
                                  </td>
                              <% Else%>
                                   <td bgcolor="#FF3333"> 
                                     &nbsp;          
                                   </td>
                              <% End If%>
                                   
                              <%                
                                  If (Rs("data_prima_nc") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                <%
                                    Else
                                        Dim test_data As String
                                        Try
                                        test_data = funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc"))
                                                %>
                                                   <td bgcolor="#369061">
                                                      <%=Rs("data_prima_nc")%>
                                                   </td> 
                                                <%         
                                        Catch ex As Exception
                                                %>
                                                   <td bgcolor="#b4679d">
                                                      <%=Rs("data_prima_nc")%>
                                                   </td> 
                                                <%        
                                        End Try
                                                              
                                    End If
                                         
                                               
                                            If (Rs("importo_prima_nc") & "") = "" Then%>
                                    <td  bgcolor="#FF3333"  >
                                        &nbsp;
                                    </td>
                                   <%
                                   Else
                                       Try
                                           Dim test_dbl As Double = CDbl(Rs("importo_prima_nc"))
                                           If test_dbl >= 0 Then%>
                                          <td bgcolor="#369061">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                        <%
                                        Else
                                            %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                            <%
                                        End If
                                    Catch ex As Exception
                                          %>
                                          <td bgcolor="#b4679d">
                                              <%=Rs("importo_prima_nc")%>  
                                          </td>
                                  <%
                                  End Try
                              End If
                          End If%>
                     </tr>
                 <%
                End If
            Loop
            
            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            
                %>
         </table>
       </div>
         <!--Legenda -->
     <table style="width:100%;" border="1">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label53" runat="server" Text="Legenda:" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  bgcolor="#FF3333">&nbsp;</td>
            <td><asp:Label ID="Label54" runat="server" Text="Valore obbligatorio" CssClass="testo"></asp:Label></td>
        </tr>
        <tr>
            <td  bgcolor="#9999FF">&nbsp;</td>
            <td><asp:Label ID="Label55" runat="server" Text="L'auto non risulta venduta." CssClass="testo"></asp:Label></td>
        </tr>        
        <tr>
            <td  bgcolor="#FFFF99">&nbsp;</td>
            <td><asp:Label ID="Label56" runat="server" Text="Valore non presente sul database." CssClass="testo"></asp:Label></td>
        </tr>
        <tr>
            <td  bgcolor="#b4679d">&nbsp;</td>
            <td><asp:Label ID="Label57" runat="server" Text="Valore non corretto o non valido." CssClass="testo"></asp:Label></td>
        </tr>        
        <tr>
            <td  bgcolor="#369061">&nbsp;</td>
            <td><asp:Label ID="Label58" runat="server" Text="Valore Ok" CssClass="testo"></asp:Label></td>
        </tr>   
     </table>
    <%
        
    End If
       %>
    
   
    <asp:Label ID="nome_file" runat="server" Visible="false"></asp:Label></asp:Content>

