<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="ImportVeicoli.aspx.vb" Inherits="ImportVeicoli" title="Pagina senza titolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <meta http-equiv="Expires" content="0" />
      <meta http-equiv="Cache-Control" content="no-cache" />
      <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <table border="0" cellpadding="0" cellspacing="0" width="1024px">
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" >
             <b>&nbsp;Import Veicoli</b>
           </td>
         </tr>
     </table> 
    <table>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
        <tr>
            <td>
                Inserire file da importare
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
                    SelectCommand="SELECT * FROM [veicoli]">
                </asp:SqlDataSource>     
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
                    SelectCommand="SELECT * FROM [movimenti_targa]">
                </asp:SqlDataSource>                           
            </td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="/Docs/importazioni/parco_da_inserire.csv">Scarica file...</asp:HyperLink>
            </td>                
         </tr>
         <tr>
            <td class="style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
    </table>
    <% 
        If LblImportFile.Text = "CaricaFile" Then                 
    %>
   <div style="width:1024px;height: 480px; overflow:scroll;" >
    <table border="1" width="1990px">
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
                <asp:Label ID="Label32" runat="server" Text="Prezzo_acquisto" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label33" runat="server" Text="Anticipo" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label34" runat="server" Text="Riscatto" CssClass="testo_titolo"></asp:Label> 
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
                <asp:Label ID="Label23" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label24" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label25" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label26" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label27" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label28" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label29" runat="server" Text="Num.NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label30" runat="server" Text="Data NC" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label31" runat="server" Text="Importo NC" CssClass="testo_titolo"></asp:Label> 
            </td>
         </tr>
         <%
             Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
             Dbc.Open()
             Dim Cmd As New Data.SqlClient.SqlCommand("SELECT CONVERT(char(10),data_immatricolazione,103) As data_immatricolazione ,* FROM veicoli_appoggio", Dbc)
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
                            Else 'VERDE - VALORE OK
                                %>
                                <td bgcolor="#369061"> 
                                    <%=Rs("targa")%>
                                </td>  
                            <%                                
                            End If
                        Case Is = 1 'Turchese valore duplicato in DB MA SOLO SE NON SI DEVE ESEGUIRE UN UPDATE SE NO E' OK
                            If Rs("eseguire_insert") Then%> 
                            <td  bgcolor="#9999FF"  >
                                <%=Rs("targa")%>
                            </td>  
                         <% ElseIf Rs("eseguire_update") Then
                            %>
                                <td bgcolor="#369061"> 
                                    <%=Rs("targa")%>
                                </td>  
                            <%
                            End If
                    End Select
                %>                                                                                        
            <!-- TELAIO -->            
            <%
                Select Case Rs("presente_telaio")
                    Case Is = 0
                        If Rs("telaio") = "" Then%>
                                <td bgcolor="#FF3333"  >
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
                            <td bgcolor="#9999FF" class="style4"  >
                                <%=Rs("telaio")%>
                             </td>   
                            <%                                            
                    End Select
                %>                                                                                        
                                            
            
            <!-- MODELLO -->            
            <%
                
                Dim ModelloStr As String = ""
                ModelloStr = Replace(Rs("modello"), "'", "''")
                ModelloStr = Replace(ModelloStr, "?", "ë")
                

                Select Case Rs("presente_modello")
                    Case Is = 0
                        If Rs("modello") = "" Then%>
                                <td  bgcolor="#FF3333"  >
                                     &nbsp;
                                </td>
                            <%
                            Else %>
                                <td  bgcolor="#FFFF99"  >
                                    <%=ModelloStr%>
                                </td>
                            <%                                
                            End If
                        Case Is = 1 %>
                            <td bgcolor="#369061">
                               <%=ModelloStr%>      
                            </td>                      
                            <%                                            
                    End Select
                %>                                                                                        
            <!-- COLORE -->            
            <%
                Select Case Rs("presente_colore")
                    Case Is = 0
                        If Rs("colore") = "" Then%>
                                <td  bgcolor="#FF3333"  >
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
                    <td  bgcolor="#FF3333"  >
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
                                <td  bgcolor="#FF3333"  >
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
                    If UCase(Rs("escludi_ammortamento")) = "SI" Or UCase(Rs("escludi_ammortamento")) = "  " Then%>
                         <td bgcolor="#369061">
                            <%=Rs("escludi_ammortamento")%>
                         </td> 
                    <%  Else %>
                         <td bgcolor="#FFFF99">
                            <%=Rs("escludi_ammortamento")%>  
                         </td>
                    <%                                
                    End If
                    %>                       
            
            <%  'SEZIONE LEASING - CONTROLLO I CAMPI SOLO SE HO SALVATO L'INFORMAZIONE CHE ALMENO UN DATO DELLA SEZIONE ' STATO VALORIZZATO ALTRIMENTI
                'LA MACCHINA VIENE CONSIDERATA COME NON LEASING
                If Not (Rs("is_leasing") Or Rs("is_leasing_sbc")) Then
                    %>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                      <td bgcolor="#369061">
                      </td>
                <%
                Else
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
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#FF3333"  >
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
                                  <td bgcolor="#369061"  >
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
                              End If %>

                              <!--Prezzo Acquisto-->                              
                                  <td bgcolor="#369061"  >
                                      <%=Rs("prezzo_acquisto")%> 
                                  </td> 

                               <!--Anticipo-->                              
                                  <td bgcolor="#369061"  >
                                      <%=Rs("anticipo")%> 
                                  </td> 
                                 
                                <!--Riscatto-->                              
                                  <td bgcolor="#369061"  >
                                      <%=Rs("riscatto")%> 
                                  </td>                          
                              
                              <%
                              'FRANCHIGIA KM INCLUSI---------------------------
                              If (Rs("franchigia_km_compresi") & "") = "" Then
                                %>
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#369061"  >
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
                                  <td bgcolor="#369061"  >
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
                          End If
                          'FATTURE  
                          If (Rs("numero_prima_fattura") & "") = "" And (Rs("data_prima_fattura") & "") = "" And (Rs("importo_prima_fattura") & "") = "" Then%>   
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
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
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
                             </td>
                             <td bgcolor="#369061">              
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
            <td  bgcolor="#369061">&nbsp;</td><td><asp:Label ID="Label5" runat="server" Text="Valore Ok." CssClass="testo"></asp:Label></td></tr></table><% End If %>
    
    <asp:Label ID="nome_file" runat="server" Visible="false"></asp:Label></asp:Content>