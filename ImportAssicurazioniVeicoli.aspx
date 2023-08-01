<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="ImportAssicurazioniVeicoli.aspx.vb" Inherits="ImportAssicurazioniVeicoli" title="Pagina senza titolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style1
    {
            width: 154px;
        }
        .style2
        {
            width: 167px;
        }
        .style3
        {
            width: 197px;
        }
        .style6
        {
            width: 111px;
        }
        .style7
        {
            width: 204px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" 
                 class="style1">
             <b>&nbsp;Import Assicurazioni</b>
           </td>
         </tr>
     </table> 
<table style="width:100%;">
        <tr>
            <td class="style7">
               <asp:Label ID="Label2" runat="server" Text="Specificare la compagnia" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dropCompagnie" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlCompagnieAssicurative" DataTextField="compagnia" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="/Docs/importazioni/PARCO_ASSICURAZIONI_DA_INSERIRE.csv" Target="_blank">Scarica file...</asp:HyperLink>
            </td>
         </tr>
        <tr>
            <td class="style7"> 
                <asp:Label ID="Label1" runat="server" Text="Specificare il file da importare" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
            
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                 <Triggers>
                  <asp:PostBackTrigger ControlID="btnImportaFile" />
                 </Triggers>
                 <ContentTemplate>
                 
                  <asp:FileUpload ID="FileUpload2" runat="server" />
                  <asp:Button ID="btnImportaFile" runat="server" Text="Importa file" BackColor="#369061" 
                            Font-Bold="True" Font-Size="Small" ForeColor="White" />
                  &nbsp;&nbsp;                
                  <asp:Label ID="lblErrore2" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                 </ContentTemplate>
                </asp:UpdatePanel>
            
              
            
                
               
                
                
                &nbsp;<asp:Label ID="LblImportFile" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
                    SelectCommand="SELECT * FROM [veicoli]">
                </asp:SqlDataSource>                                
            </td>                
         </tr>
         <tr>
            <td class="style7">&nbsp;</td>
            <td>&nbsp;</td>
         </tr>
    </table>
    <% 
        If LblImportFile.Text = "CaricaFile" Then                 
    %>
    <table style="width:100%;" border="1">
         <tr style="background-color:#19191b;color: #FFFFFF; font-weight:bold;">
            <td class="style6">
                Num. Ordine
            </td>
            <td>
                Targa</td>
            <td class="style2">
                Data Inclusione
            </td>
            <td class="style3">
                Valore Assicurato</td>                        
         </tr>
         <%
             Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
             Dbc.Open()
             Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM assicurazioni_appoggio", Dbc)
             'Response.Write(Cmd.CommandText & "<br><br>")
             Dim Rs As Data.SqlClient.SqlDataReader
             Rs = Cmd.ExecuteReader()
             

             Do While Rs.Read %>
                 
            <tr>
                <!-- Num Ordine -->            
            <%                
                If Rs("num_ordine") = "" Then%>
                    <td bgcolor="#FF3333" class="style6"  >
                    &nbsp;
            <%
            Else
                Dim valore_ok As Boolean
                
                Try
                    Dim test As Integer = CInt(Rs("num_ordine"))
                    valore_ok = True
                Catch ex As Exception
                    valore_ok = False
                End Try
                                
                If valore_ok Then  
                    If Not Rs("riga_duplicata") Then
                    %>
                                    <td  bgcolor="#369061"  >
                                    <%=Rs("num_ordine")%>
                                
                                <% 
                                Else
                                    %>
                                    <td  bgcolor="#9999FF"  >
                                    <%=Rs("num_ordine")%>
                                    <%
                                End If
                                    
                            Else
                                    %>
                                      <td  bgcolor="#b4679d"  >
                                          <%=Rs("num_ordine")%>
                                    <%
                                    End If
                                   
            End If
            %>                                                                                        
            </td>                 
            
                <!-- TARGA -->
                <%
                    Select Case Rs("presente_targa")
                        Case Is = 0
                            If Rs("targa") = "" Then%>
                                <td  bgcolor="#FF3333"  >
                                &nbsp;
                            <%
                            Else %>
                                <td bgcolor="#FFFF99" class="style3">
                                    <%=Rs("targa")%>
                            <%                                
                            End If
                        Case Is = 1
                            If Not Rs("riga_duplicata") Then
                            
                            %>
                            <td  bgcolor="#369061"  >
                                <%=Rs("targa")%>
                            <%              
                            Else
                                %>
                                <td  bgcolor="#9999FF"  >
                                <%=Rs("targa")%>
                                <%
                            End If
                    End Select
                %>                                                                                        
            </td>          
                                           
            
             <!-- Data Inclusione -->            
            <%                
                If Rs("data_inclusione") = "01/01/1900 0.00.00" Then%>
                    <td  bgcolor="#FF3333"  >
                        &nbsp;
                <%
                ElseIf IsDate(Rs("data_inclusione")) Then%>
                   <td bgcolor="#369061">
                      <%=Rs("data_inclusione").ToString.Replace("00:00:00", "")%>
                <%    
                Else %>
                 <td bgcolor="#b4679d">
                      <%=Rs("data_inclusione")%>
                <%    
                End If
                %>                                                                                        
            </td>          
            
            <!-- Valore Assicurato -->            
            <%
                
                If Rs("valore_assicurato") = "" Then%>
                                <td  bgcolor="#FF3333"  >
                                &nbsp;
                            <%
                            Else
                                Dim valore_ok As Boolean
                                Try
                                    Dim test As Double = CDbl(Rs("valore_assicurato"))
                                    valore_ok = True
                                Catch ex As Exception
                                    valore_ok = False
                                End Try
                                
                                If valore_ok Then%>
                                    <td  bgcolor="#369061"  >
                                    <%=FormatNumber(CDbl(Rs("valore_assicurato")), 2, , , TriState.True)%>
                                
                                <% Else
                                    %>
                                      <td  bgcolor="#b4679d"  >
                                          <%=Rs("valore_assicurato")%>
                                    <%
                                    End If%>
                            <%                                
                            End If                        
                %>                                                                                        
            </td>                                              
         </tr>
                 
             <%
             Loop

             Rs.Close()
             Dbc.Close()
             Rs = Nothing
             Dbc = Nothing
             
         %>
     </table> 
     <br /><br />
     
     <!--Legenda -->
     <table style="width:100%;" border="1">
        <tr>
            <td colspan="2">
                Legenda:
            </td>
        </tr>
        <tr>
            <td  bgcolor="#FF3333">&nbsp;</td>
            <td>Valore obbligatorio</td>
        </tr>
        <tr>
            <td  bgcolor="#9999FF">&nbsp;</td>
            <td>Numero d'ordine e targa già inseriti (dato duplicato)</td>
        </tr>          
        <tr>
            <td  bgcolor="#FFFF99">&nbsp;</td>
            <td>Valore non presente sul database</td>
        </tr>
        <tr>
            <td  bgcolor="#b4679d">&nbsp;</td>
            <td>Valore non corretto</td>
        </tr>     
        <tr>
            <td  bgcolor="#369061">&nbsp;</td>
            <td>Valore Ok</td>
        </tr>   
     </table>
    <% End If %>
    
    
    <asp:SqlDataSource ID="sqlCompagnieAssicurative" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [compagnia] FROM [compagnie_assicurative] ORDER BY [compagnia]"></asp:SqlDataSource>
</asp:Content>

