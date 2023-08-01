<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="importDismissioni.aspx.vb" Inherits="importDismissioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <meta http-equiv="Expires" content="0" />
      <meta http-equiv="Cache-Control" content="no-cache" />
      <meta http-equiv="Pragma" content="no-cache" />
      <style type="text/css">
          .style1
          {
              width: 28px;
          }
          .style2
          {
              width: 168px;
          }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;<asp:Label ID="Label9" runat="server" Text="Import Dismissioni" CssClass="testo_titolo"></asp:Label></b>
           </td>
         </tr>
</table>
<table style="width:100%;">
        <tr>
            <td class="style2">&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" Text="Inserire file da importare" CssClass="testo"></asp:Label>
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
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:HyperLink ID="HyperLink1" runat="server" 
                             NavigateUrl="/Docs/importazioni/dismissioni.csv" Target="_blank">Scarica file...</asp:HyperLink>
                       </ContentTemplate>
                </asp:UpdatePanel>
            </td>                
         </tr>
         <tr>
            <td class="style2">&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
    </table>
    <% 
        If LblImportFile.Text = "CaricaFile" Then                 
    %>
     <div style="width:1024px;height: 480px; overflow:scroll;" >
      <table style="width:2180px;" border="1">
         <tr style="background-color:#19191b; font-weight:bold;">
            <td width="80px">
                <asp:Label ID="Label10" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="80px">
                <asp:Label ID="Label11" runat="server" Text="DDT" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="80px">
                <asp:Label ID="Label12" runat="server" Text="Data DDT" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="220px">
                <asp:Label ID="Label21" runat="server" Text="Presso Di" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnPressoDi" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td>
            <td width="80px">
                <asp:Label ID="Label13" runat="server" Text="Data Atto Vendita " CssClass="testo_titolo"></asp:Label>   
            </td>
            <td width="220px">
                <asp:Label ID="Label14" runat="server" Text="Venditore" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnVenditore" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td>
            <td width="220px">
                <asp:Label ID="Label15" runat="server" Text="Acquirente" CssClass="testo_titolo"></asp:Label>
                <asp:ImageButton ID="btnAcqirente" runat="server" ImageUrl="/images/aggiorna.png" style="height: 16px" />
            </td> 
            <td width="80px">
                <asp:Label ID="Label16" runat="server" Text="Leasing (SI/NO)" CssClass="testo_titolo"></asp:Label>
            </td>
            <td width="400px">
                <asp:Label ID="Label17" runat="server" Text="Note" CssClass="testo_titolo"></asp:Label>
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
                <asp:Label ID="Label18" runat="server" Text="Num.Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label19" runat="server" Text="Data Fattura" CssClass="testo_titolo"></asp:Label> 
            </td>
            <td width="80px">
                <asp:Label ID="Label20" runat="server" Text="Importo Fattura" CssClass="testo_titolo"></asp:Label> 
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
              Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
             Dbc.Open()
              Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM dismissioni_appoggio WHERE id_utente='" & Request.Cookies("SicilyRentCar")("idUtente") & "'", Dbc)
             Dim Rs As Data.SqlClient.SqlDataReader
              Rs = Cmd.ExecuteReader()
              
              Dim leasing As Boolean
              
              Do While Rs.Read
                  If (Rs("veicolo_venduto") & "") = "True" Then
                      
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
                    If UCase((Rs("leasing") & "")) = "SI" Then
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
                               'VEICOLO TROVATO E NON VENDUTO
                               %>
                             <td bgcolor="#369061">
                                  <%=Rs("targa") %>
                             </td>
                       <%End If%>
                       <%'NUMERO BOLLA (obbligatorio)------------------------------------------------------------------------------ %>
                         <% If (Rs("ddt") & "") = "" Then%>
                         <td bgcolor="#FF3333">
                            &nbsp;
                         </td>
                        <% Else%>
                         <td bgcolor="#369061">
                            <%=Rs("ddt")%>
                         </td>
                        <%  End If%>
                       <%'DATA BOLLA (obbligatorio)-------------------------------------------------------------------------------- %>
                        <% If (Rs("data_ddt") & "") = "" Then%>
                         <td bgcolor="#FF3333">
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
                             <td bgcolor="#FF3333">
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
                             <td bgcolor = "#369061">
                                 &nbsp;
                             </td>
                          <% Else%>
                            <% If leasing And (Rs("data_atto_di_vendita") & "") = (Rs("data_ddt") & "") Then%>
                              <td bgcolor = "#369061">
                                  <%=Rs("data_atto_di_vendita")%>
                              </td>
                            <% ElseIf leasing And (Rs("data_atto_di_vendita") & "") <> (Rs("data_ddt") & "") Then%>
                               <td bgcolor="#b4679d">
                                   <%=Rs("data_atto_di_vendita")%>
                               </td>
                            <%  ElseIf Not leasing Then
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
                            <% End If%>
                          <% End If %>
                        <%'VENDITORE ----------------------------------------------------------------------------------------------- %> 
                        <% If leasing And (Rs("venditore") & "") <> "" Then%>
                            <td bgcolor="#b4679d">
                                <%=Rs("venditore")%>
                            </td>
                        <% ElseIf leasing And (Rs("venditore") & "") = "" Then%>
                            <td bgcolor = "#369061">
                                &nbsp;
                            </td>
                        <% Else If Not leasing And (Rs("venditore") & "") = "" Then %>
                             <td bgcolor="#FF3333">
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
                             <td bgcolor="#FF3333">
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
                           <% If (Rs("leasing") & "") = "" Then%>
                                 <td bgcolor="#FF3333">
                                    &nbsp;
                                 </td>
                           <%  ElseIf UCase(Rs("leasing")) = "SI" Or UCase(Rs("leasing")) = "NO" Then%>
                              <% If ((UCase(Rs("leasing")) = "SI" And Rs("veicolo_is_leasing")) Or (UCase(Rs("leasing")) = "NO" And Not Rs("veicolo_is_leasing"))) Or ((Rs("id_veicolo") & "") = "") Then%>
                                 <td bgcolor = "#369061">
                                    <%=Rs("leasing")%>
                                 </td>
                              <% Else%>
                                  <td bgcolor="#b4679d">
                                    <%=Rs("leasing")%>
                                  </td>
                              <% End If%>
                           <% Else %>
                                 <td bgcolor="#b4679d">
                                    <%=Rs("leasing")%>
                                 </td>
                           <% End If%>
                        <%'--------------------------------------------------------------------------------------------------------- %>
                        <td bgcolor = "#369061">
                          <%=Rs("note")%>
                        </td>
                        <%  'FATTURE  
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
                          'FATTURA II
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
                          End If 'I NOTA DI CREDITO   
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
                <asp:Label ID="Label2" runat="server" Text="Legenda:" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  bgcolor="#FF3333" class="style1">&nbsp;</td>
            <td><asp:Label ID="Label3" runat="server" Text="Valore obbligatorio" CssClass="testo"></asp:Label></td>
        </tr>
        <tr>
            <td  bgcolor="#9999FF" class="style1">&nbsp;</td>
            <td><asp:Label ID="Label4" runat="server" Text="L'auto risulta già venduta." CssClass="testo"></asp:Label></td>
        </tr>        
        <tr>
            <td  bgcolor="#FFFF99" class="style1">&nbsp;</td>
            <td><asp:Label ID="Label5" runat="server" Text="Valore non presente sul database." CssClass="testo"></asp:Label></td>
        </tr>
        <tr>
            <td  bgcolor="#b4679d" class="style1">&nbsp;</td>
            <td><asp:Label ID="Label6" runat="server" Text="Valore non corretto o non valido (in caso di leasing la data dell'atto di vendita deve essere uguale a data DDT ed il venditore non deve essere specificato)." CssClass="testo"></asp:Label></td>
        </tr>        
        <tr>
            <td  bgcolor="#369061" class="style1">&nbsp;</td>
            <td><asp:Label ID="Label7" runat="server" Text="Valore Ok" CssClass="testo"></asp:Label></td>
        </tr>   
     </table>
    <%
        
    End If
       %>
    
    <asp:Label ID="nome_file" runat="server" Visible="true"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>

