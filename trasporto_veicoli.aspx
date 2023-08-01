<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="true" CodeFile="trasporto_veicoli.aspx.vb" Inherits="trasporto_veicoli" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

     <link rel="StyleSheet" type="text/css" href="css/style.css" /> 






    <style type="text/css">
        .style3
        {
            width: 165px;
        }
        .style4
        {
        }
        .style5
        {
            width: 148px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style6
        {
            width: 148px;
            height: 30px;
        }
        .style7
        {
            height: 30px;
        }
        .style8
        {
            width: 125px;
            height: 30px;
        }
        .style9
        {
            width: 125px;
        }
        .style13
        {
            width: 126px;
        }
        .style14
        {
            width: 130px;
        }
        .style15
        {
            width: 176px;
        }
        .style17
        {
        }
        .style18
        {
            width: 155px;
        }
        .style19
        {
            width: 98px;
        }
        .style20
        {
            width: 202px;
        }
        .style21
        {
            width: 203px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" class="testo_bold_bianco">
             <b>Bisarca</b>
           </td>
         </tr>
     </table>
   <div id="pannello_ricerca" runat="server">  
     <table border="0" cellpadding="2" cellspacing="2" width="1024px" style="border: 4px solid #444">
        <tr>
          <td align="left" class="style5">
             <asp:Label ID="lblStatoTrasferimento" runat="server" Text="Stato trasferimento" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style4">
            <asp:Label ID="lblStazioneUscita" runat="server" Text="Stazione di uscita" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style3">
            <asp:Label ID="lblStazioneRientro" runat="server" Text="Stazione di rientro" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
            <asp:Label ID="lblDataUscitaDa" runat="server" Text="Data uscita da" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style9">
            <asp:Label ID="lblDataUscitaA" runat="server" Text="Data uscita a" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
            <asp:Label ID="lblDatarientroDa" runat="server" Text="Data rientro da" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
            <asp:Label ID="lblDataRientroA" runat="server" Text="Data rientro a" CssClass="testo_bold"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" class="style5">
               
              <asp:DropDownList ID="dropStatoTrasferimento" runat="server" 
                  AppendDataBoundItems="True">
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                  <asp:ListItem Value="E">Effettuati</asp:ListItem>
                  <asp:ListItem Value="A">Annullati</asp:ListItem>
                  <asp:ListItem Value="I">In Corso</asp:ListItem>
              </asp:DropDownList>
               
          </td>
          <td align="left" class="style4">
            <asp:DropDownList ID="dropStazioneUscita" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                   DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>     
          </td>
          <td align="left" class="style3">
              <asp:DropDownList ID="dropStazioneRientro" runat="server" 
                  AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                  DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
          </td>
          <td align="left">
              <a onclick="Calendar.show(document.getElementById('<%=data_uscita_da.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_uscita_da" runat="server" Width="70px"></asp:TextBox>
                  </a>
              <%-- <asp:CalendarExtender ID="data_uscita_da_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_uscita_da">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="data_uscita_da_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_uscita_da">
               </asp:MaskedEditExtender>
             </td>
          <td align="left" class="style9">
              <a onclick="Calendar.show(document.getElementById('<%=data_uscita_a.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_uscita_a" runat="server" Width="70px"></asp:TextBox>
                  </a>
              <%-- <asp:CalendarExtender ID="data_uscita_a_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_uscita_a">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="data_uscita_a_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_uscita_a">
               </asp:MaskedEditExtender>
          </td>
          <td align="left">
              <a onclick="Calendar.show(document.getElementById('<%=data_rientro_da.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_rientro_da" runat="server" Width="70px"></asp:TextBox>
                  </a>
     <%--          <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_rientro_da">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="MaskedEditExtender1" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_rientro_da">
               </asp:MaskedEditExtender>
             </td>
          <td align="left">
               <a onclick="Calendar.show(document.getElementById('<%=data_rientro_a.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_rientro_a" runat="server" Width="70px"></asp:TextBox>
                   </a>
               <%--<asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_rientro_a">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="MaskedEditExtender2" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_rientro_a">
               </asp:MaskedEditExtender>
          </td>
        </tr>
        <tr>
         
          <td align="left" class="style5">
            <asp:Label ID="lblDataPresRientroDa" runat="server" Text="Data pres. rientro da" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
            <asp:Label ID="lblDataPresRientroA" runat="server" Text="Data pres. rientro a" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style9">
            <asp:Label ID="lblNumeroDDT" runat="server" Text="Numero DDT" CssClass="testo_bold"></asp:Label>
          </td>
           <td align="left">
           
          </td>
          <td align="left">
           
          </td>
          <td align="left">
              &nbsp;</td>
          <td align="left">
              &nbsp;</td>
        </tr>
        <tr>
         
          <td align="left" class="style6">
               <a onclick="Calendar.show(document.getElementById('<%=data_presunto_rientro_da.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_presunto_rientro_da" runat="server" Width="70px"></asp:TextBox>
                   </a>
            <%--   <asp:CalendarExtender ID="data_uscita_da0_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_presunto_rientro_da">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="data_uscita_da0_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_presunto_rientro_da">
               </asp:MaskedEditExtender>
             </td>
          <td align="left" class="style7">
               <a onclick="Calendar.show(document.getElementById('<%=data_presunto_rientro_a.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="data_presunto_rientro_a" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%-- <asp:CalendarExtender ID="data_uscita_da1_CalendarExtender" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_presunto_rientro_a">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="data_uscita_da1_MaskedEditExtender" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_presunto_rientro_a">
               </asp:MaskedEditExtender>
             </td>
          <td align="left" class="style8">
              <asp:TextBox ID="txtNumeroDDT" runat="server" Width="50px"></asp:TextBox>
            </td>
           <td align="left" class="style7">
               
             
          </td>
          <td align="left" class="style7">
               
               
          </td>
          <td align="left" class="style7">
              </td>
          <td align="left" class="style7">
              </td>
        </tr>
        <tr>
          <td align="center" class="style4" colspan="7">
               
            <asp:Button ID="btnCerca" runat="server" Text="Cerca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  

            &nbsp;
            
            <asp:Button ID="btnNuovo" runat="server" Text="Nuova Bisarca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
          </td>
        </tr>
      </table>
      <table width="1024px">
        <tr>
          <td align="center" colspan="7">
          <br />&nbsp;
             <asp:ListView ID="listDDT" runat="server" DataKeyNames="id" 
                  DataSourceID="sqlDDT">
                 <ItemTemplate>
                     <tr style="background-color:#DCDCDC;color: #000000;">
                         <td align="left">
                            <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stato_trasferimentoLabel" runat="server" 
                                 Text='<%# Eval("stato_trasferimento") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stazione_uscitaLabel" runat="server" 
                                 Text='<%# Eval("stazione_uscita") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stazione_rientroLabel" runat="server" 
                                 Text='<%# Eval("stazione_rientro") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_uscita_label" runat="server" 
                                 Text='<%# Eval("data_uscita") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_presunto_rientroLabel" runat="server" 
                                 Text='<%# Eval("data_presunto_rientro") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_rientroLabel" runat="server" 
                                 Text='<%# Eval("data_rientro") %>' />
                         </td>
                          <td align="center">
                           <asp:ImageButton ID="btnVedi" ToolTip="dettagli" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                         </td>
                         <td align="center">
                          <asp:ImageButton ID="btnAnnulla" ToolTip="annulla" runat="server" ImageUrl="/images/elimina.png" CommandName="annulla" OnClientClick="javascript: return(window.confirm ('Il Documento di Trasporto selezionato verrà annullato. Sei sicuro di voler procedere?'));" />
                        </td>
                     </tr>
                 </ItemTemplate>
                 <AlternatingItemTemplate>
                     <tr style="">
                         <td align="left">
                            <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stato_trasferimentoLabel" runat="server" 
                                 Text='<%# Eval("stato_trasferimento") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stazione_uscitaLabel" runat="server" 
                                 Text='<%# Eval("stazione_uscita") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stazione_rientroLabel" runat="server" 
                                 Text='<%# Eval("stazione_rientro") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_uscita_label" runat="server" 
                                 Text='<%# Eval("data_uscita") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_presunto_rientroLabel" runat="server" 
                                 Text='<%# Eval("data_presunto_rientro") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="data_rientroLabel" runat="server" 
                                 Text='<%# Eval("data_rientro") %>' />
                         </td>
                          <td align="center">
                           <asp:ImageButton ID="btnVedi" ToolTip="Dettagli" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                         </td>
                         <td align="center">
                          <asp:ImageButton ID="btnAnnulla" ToolTip="Annulla" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Il Documento di Trasporto selezionato verrà annullato e le macchine ad esso collegato verranno nuovamente rese disponibili per la stazione di partenza. Sei sicuro di voler procedere?'));" />
                        </td>
                     </tr>
                 </AlternatingItemTemplate>
                 <EmptyDataTemplate>
                     <table runat="server" style="">
                         <tr>
                             <td>
                                 Non è stato restituito alcun dato.</td>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
                 <LayoutTemplate>
                     <table runat="server"  width="100%">
                         <tr runat="server">
                             <td runat="server">
                                 <table ID="itemPlaceholderContainer" width="100%" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif; font-size:small;">
                                     <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                         <th id="Th3" runat="server" align="left">
                                           N.
                                         </th>
                                         <th runat="server" align="left">
                                             Stato</th>
                                         <th runat="server" align="left">
                                             Uscita</th>
                                         <th runat="server" align="left">
                                             Rientro</th>
                                         <th runat="server" align="left">
                                             Data Uscita</th>
                                         <th runat="server" align="left">
                                             Pres. Rientro</th>
                                         <th runat="server" align="left">
                                             Data Rientro</th>
                                         <th id="Th1" runat="server" align="left">
                                         </th>
                                         <th id="Th2" runat="server" align="left">
                                         </th>
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
             </asp:ListView>        
          </td>
        </tr>
     </table>
    </div>
    <div runat="server" id="pannello_ddt" visible="false">
        <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
         <tr>
           <td align="left" class="style13">
             <asp:Label ID="lblStazioneDiUscita" runat="server" Text="Stazione di uscita" CssClass="testo_bold"></asp:Label>
           </td>
           <td align="left" class="style21">
            <%--<b>Data di uscita</b>--%>
           </td>
           <td align="left" class="style19">
            <asp:Label ID="lblStatoMaschera" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
           </td>
           <td align="left" class="style18">
               <asp:Label ID="lblDescDataUscita" runat="server" Text="Data di Uscita" CssClass="testo_bold"></asp:Label>
           </td>
           <td align="left">
               <asp:Label ID="lblDescDataRientro" runat="server" Text="Data di Rientro" 
                   CssClass="testo_bold"></asp:Label>
           </td>
         </tr>
         <tr>
           <td align="left" class="style13">
             
            <asp:DropDownList ID="stazione_uscita_ins" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                   DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>     
             
           </td>
           <td align="left" class="style21">

           &nbsp;&nbsp;
               
             
           </td>
           <td align="left" class="style19">
               <asp:Label ID="lblStato" runat="server" Text=""></asp:Label><asp:Label ID="lblCodiceStato" runat="server" visible="false"></asp:Label>
             </td>
           <td align="left" class="style18">
               <asp:Label ID="lblDataDiUscita" runat="server" Text=""></asp:Label>
             </td>
           <td align="left">
               <asp:Label ID="lblDataDiRientro" runat="server" Text=""></asp:Label>
             </td>
         </tr>
        
         <tr>
           <td align="left" class="style13">
             
             <asp:Label ID="lblStazioneDiRientro" runat="server" Text="Stazione di rientro" CssClass="testo_bold"></asp:Label>
             
           </td>
           <td align="left" class="style21">
             <asp:Label ID="lblDataDiPresuntorientro" runat="server" Text="Data di presunto rientro" CssClass="testo_bold"></asp:Label>

             
           </td>
           <td align="left" class="style19">
               &nbsp;</td>
           <td align="left" class="style18">
               &nbsp;</td>
           <td align="left">
               &nbsp;</td>
         </tr>
        
         <tr>
           <td align="left" class="style13">
             
            <asp:DropDownList ID="stazione_rientro_ins" runat="server" 
                   AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                   DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>     
             
           </td>
           <td align="left" class="style21">
               <a onclick="Calendar.show(document.getElementById('<%=data_rientro_ins.ClientID%>'), '%d/%m/%Y', false)"> 
              <asp:TextBox ID="data_rientro_ins" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%-- <asp:CalendarExtender ID="data_rientro_ins_CalendarExtender0" runat="server" 
                   Format="dd/MM/yyyy" TargetControlID="data_rientro_ins">
               </asp:CalendarExtender>--%>
               <asp:MaskedEditExtender ID="data_rientro_ins_MaskedEditExtender0" 
                   runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                   TargetControlID="data_rientro_ins">
               </asp:MaskedEditExtender>

               <asp:DropDownList ID="ore_rientro" runat="server" 
                   AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="-1">-</asp:ListItem>
                   <asp:ListItem>00</asp:ListItem>
                   <asp:ListItem>01</asp:ListItem>
                   <asp:ListItem>02</asp:ListItem>
                   <asp:ListItem>03</asp:ListItem>
                   <asp:ListItem>04</asp:ListItem>
                   <asp:ListItem>05</asp:ListItem>
                   <asp:ListItem>06</asp:ListItem>
                   <asp:ListItem>07</asp:ListItem>
                   <asp:ListItem>08</asp:ListItem>
                   <asp:ListItem>09</asp:ListItem>
                   <asp:ListItem>10</asp:ListItem>
                   <asp:ListItem>11</asp:ListItem>
                   <asp:ListItem>12</asp:ListItem>
                   <asp:ListItem>13</asp:ListItem>
                   <asp:ListItem>14</asp:ListItem>
                   <asp:ListItem>15</asp:ListItem>
                   <asp:ListItem>16</asp:ListItem>
                   <asp:ListItem>17</asp:ListItem>
                   <asp:ListItem>18</asp:ListItem>
                   <asp:ListItem>19</asp:ListItem>
                   <asp:ListItem>20</asp:ListItem>
                   <asp:ListItem>21</asp:ListItem>
                   <asp:ListItem>22</asp:ListItem>
                   <asp:ListItem>23</asp:ListItem>
                   <asp:ListItem></asp:ListItem>
               </asp:DropDownList> 
               <asp:DropDownList ID="minuti_rientro" runat="server" 
                   AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="-1">-</asp:ListItem>
                   <asp:ListItem>00</asp:ListItem>
                   <asp:ListItem>05</asp:ListItem>
                   <asp:ListItem>10</asp:ListItem>
                   <asp:ListItem>15</asp:ListItem>
                   <asp:ListItem>20</asp:ListItem>
                   <asp:ListItem>25</asp:ListItem>
                   <asp:ListItem>30</asp:ListItem>
                   <asp:ListItem>35</asp:ListItem>
                   <asp:ListItem>40</asp:ListItem>
                   <asp:ListItem>45</asp:ListItem>
                   <asp:ListItem>50</asp:ListItem>
                   <asp:ListItem>55</asp:ListItem>
                   <asp:ListItem></asp:ListItem>
               </asp:DropDownList>

             
           </td>
           <td align="left" class="style19">
               &nbsp;</td>
           <td align="left" class="style18">
               &nbsp;</td>
           <td align="left">
               &nbsp;</td>
         </tr>
        
         </table>
         <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
           <tr>
           <td>  
           <%  
               Dim carica_dati As Boolean

               'IN CASO DI NUOVO INSERIMENTO CONTERRA' GLI id_veicolo DELLA TABELLA documenti_di_trasporto_auto
               Dim id_veicoli_db As String()

               'NEL CASO IN CUI E' STATO EFFETTUATO IL SALVATAGGIO DI UN NUOVO TEMPO KM, CASO IN CUI L'ID_TEMPO_KM NON RESTA MEMORIZZATO NELL'APPOSITA LABEL,
               'RECUPERO IL VALORE PRECEDNETE CONSERVATO NELL'INPUT

               If id_ddt.Text = "" Then
                   id_ddt.Text = Request.Form("id_modifica")
               End If

               If id_ddt.Text = "" Then
                   carica_dati = False
               ElseIf Request.Form("numAuto") = "" Then
                   'IN QUESTO CASO SIAMO IN FASE DI MODIFICA
                   carica_dati = True
               Else
                   carica_dati = False
               End If


               Dim i As Integer

               If Request.Form("numAuto") = "" Then
                   If id_ddt.Text = "" Then
                       'IN CASO DI NUOVO INSERIMENTO
                       i = 0
                   Else
                       'IN QUESTO CASO SIAMO IN FASE DI MODIFICA, CARICO I DATI DA DB
                       i = getNumeroAuto(id_ddt.Text)
                       id_veicoli_db = get_veicoli_in_ddt(id_ddt.Text)
                   End If
               Else
                   i = Request.Form("numAuto")
               End If

               Dim auto_esiste As Boolean = True
               Dim auto_non_inserita As Boolean = True
               Dim auto_in_stazione_uscita As Boolean = True
               Dim auto_in_nolo As Boolean = False

               'CONTROLLO SE E' STATO RICHIESTO L'INSERIMENTO DI UN'AUTO-----------------------------------------------------------------
               Dim inserisci As String
               inserisci = Request.Form("inserisci")

               If inserisci <> "" Then
                   auto_esiste = auto_esistente(Trim(txtTarga.Text))

                   If auto_esiste Then
                       'SE L'AUTO ESISTE CONTROLLO CHE NON SIA GIA' STATA INSERITA NEL DDT CORRENTE

                       For k = 1 To i
                           If Request.Form("numAuto_" & k) = Trim(txtTarga.Text) Then
                               auto_non_inserita = False
                           End If
                       Next
                       If auto_non_inserita Then
                           If Not auto_in_stazione(Trim(txtTarga.Text), stazione_uscita_ins.SelectedValue) Then
                               auto_in_stazione_uscita = False
                           ElseIf auto_noleggiata(Trim(txtTarga.Text)) Then
                               auto_in_nolo = True
                           Else
                               i = i + 1
                           End If

                       End If

                   End If
               End If

               'SALVATAGGIO E ATTIVAZIONE DEL DTT---------------------------------------------------------------------------------------
               Dim salva_trasporto As String
               salva_trasporto = Request.Form("ctl00$ContentPlaceHolder1$btnSalva")
               'NEL CASO IN CUI SIA STATO RICHIESTA LA CHIUSURA DEL DDT DEVO PRIMA SALVARLO NUOVAMENTE, PER EVENTUALI MODIFICHE
               Dim chiudi_ddt As String = Request.Form("chiudi_ddt")

               If salva_trasporto <> "" Or chiudi_ddt <> "" Then
                   'CONTROLLO CHE NON SIANO PRESENTI ERRORI------ (SOLO NEL CASO DI CHECK OUT)
                   '1) TANGA AUTO NON SUPERIORE A TANGA MASSIMA E CORRETTA
                   '2) AUTO EFFETTIVAMENTE PRESENTE NELLA STAZIONE DI USCITA
                   '3) ALMENO UN'AUTO INSERITA 
                   '4) AUTO VENDUTA
                   Dim tangaOk As Boolean = True
                   Dim tanga1 As Integer
                   Dim tanga2 As Integer
                   Dim almeno_una_auto As Boolean = False
                   Dim tutte_auto_in_stazione_uscita As Boolean = True
                   Dim auto_errore As String = ""
                   Dim tutte_auto_non_vendute As Boolean = True
                   Dim auto_vendute As String
                   Dim tutte_auto_non_in_altro_ddt As Boolean = True
                   Dim auto_altro_ddt As String
                   Dim tutte_auto_non_in_nolo As Boolean = True
                   Dim auto_noleggiate As String

                   If salva_trasporto <> "" Then
                       For k = 1 To i
                           If Request.Form("numAuto_" & k) <> "" Then
                               almeno_una_auto = True
                               Try
                                   tanga1 = CInt(Request.Form("tangaAuto_" & k))    'serbatoio attuale
                                   tanga2 = CInt(Request.Form("tangaMaxAuto_" & k)) 'serbatoio MAX
                                   If tanga1 > tanga2 Then
                                       tangaOk = False
                                   End If
                               Catch ex As Exception
                                   tangaOk = False
                               End Try

                               If Not auto_in_stazione(Request.Form("numAuto_" & k), stazione_uscita_ins.SelectedValue) Then
                                   tutte_auto_in_stazione_uscita = False
                                   auto_errore = auto_errore & Request.Form("numAuto_" & k) & " "
                               End If
                               If auto_venduta(Request.Form("numAuto_" & k)) Then
                                   tutte_auto_non_vendute = False
                                   auto_vendute = auto_vendute & Request.Form("numAuto_" & k) & " "
                               End If
                               If auto_in_altro_ddt(Request.Form("numAuto_" & k), id_ddt.Text) Then
                                   tutte_auto_non_in_altro_ddt = False
                                   auto_altro_ddt = auto_altro_ddt & Request.Form("numAuto_" & k) & " "
                               End If
                               If auto_noleggiata(Request.Form("numAuto_" & k)) Then
                                   tutte_auto_non_in_nolo = False
                                   auto_noleggiate = auto_noleggiate & Request.Form("numAuto_" & k) & " "
                               End If
                           End If
                       Next
                   ElseIf chiudi_ddt <> "" Then
                       almeno_una_auto = True
                   End If

                   If almeno_una_auto And tangaOk And tutte_auto_in_stazione_uscita And tutte_auto_non_vendute And tutte_auto_non_in_altro_ddt And tutte_auto_non_in_nolo Then
                       If id_ddt.Text = "" Then
                           'NUOVO INSERIMENTO---------------------------------------------------------------------------------------------------
                           id_ddt.Text = creaDDT()

                           For k = 1 To i
                               If Request.Form("numAuto_" & k) <> "" Then
                                   inserisci_auto(Request("numAuto_" & k), Request("kmAuto_" & k), Request("tangaAuto_" & k), id_ddt.Text)
                               End If
                           Next

                           'SETTO LE AUTO COME NON DISPONIBILI: il DDT è direttamente attivo all'atto del salvataggio
                           setta_auto_non_disponibile(id_ddt.Text)
                              %> 
                                 <center><asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red" Text="Bisarca salvata correttamente." ></asp:Label></center>
                                 
                        <%
                        Else
                            ''MODIFICA - NON PIU' MODIFICABILE UNA VOLTA SALVATA L'USCITA DEI VEICOLI-------------------------------------------------------------
                            'modificaDDT(id_ddt.Text)  'MODIFICA DELL'INTESTAZIONE DEL DDT
                            'elimina_righe(id_ddt.Text)
                             
                            'For k = 1 To i
                            '    If Request.Form("numAuto_" & k) <> "" Then
                            '        inserisci_auto(Request("numAuto_" & k), Request("kmAuto_" & k), Request("tangaAuto_" & k), id_ddt.Text)
                            '    End If
                            'Next
                                 
                            ''SETTO LE AUTO COME NON DISPONIBILI: il DDT è direttamente attivo all'atto del salvataggio
                            'setta_auto_non_disponibile(id_ddt.Text)
                                 
                            If chiudi_ddt = "" Then
                                 %> 
                                   <center><asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red" Text="Modifica eseguita correttamente." ></asp:Label></center>
                                 <%
                                 End If
                                 
                                  'SE E' STATA RICHIESTA LA CHIUSURA DEL DDT: 
                                 '1) SETTO TUTTE LE AUTO COME DISPONIBILI PER LA STAZIONE DI RIENTRO
                                 '2) SETTO LO STATO E LE INFORMAZIONI DI RIENTRO DEL DDT
                                 'DA RICORDARE CHE CLICCANDO SU CHIUDI DDT HO ANCHE EFFETTUATO IL SALVAGGIO DEI DATI SU DB COME SE FOSSE UNA NORMALE
                                 'MODIFICA, PER CUI HO GIA' TUTTI I DATI CORRETTI NELLE TABELLE
                                 If chiudi_ddt <> "" Then
                                     btnSalva.Enabled = False
                                     chiudiDdt(id_ddt.Text)
                               %> 
                                   <center><asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Red" Text="Bisarca Chiusa correttamente." ></asp:Label></center>
                               <%
                               End If
                             End If
                         Else
                           If Not almeno_una_auto Then
                               
                          %> 
                            <center><asp:Label ID="lblErrore2" runat="server" Font-Bold="True" ForeColor="Red" Text="Specificare almeno un auto per la bisarca corrente." ></asp:Label></center>
                          <%
    End If

    If Not tangaOk Then 'verificare
                             %>
                               <center><asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Text="Una o più valori del campo serbatoio non è valido. Correggere i dati." ></asp:Label></center>
                          <%
    End If


    If Not tutte_auto_in_stazione_uscita Then
                              %>
                             <center><font color="red" style="font-weight:bold"><%="Le vetture " & auto_errore & " non risultano tra il parco veicoli della stazione di uscita. Impossibile salvare la bisarca."%></font></center>
                          <%
                          End If
                             
                          If Not tutte_auto_non_vendute Then
                              %>
                              <center><font color="red" style="font-weight:bold"><%="Le vetture " & auto_vendute & " risultano vendute. Impossibile salvare la bisarca."%></font></center>
                          <%
                          End If
                          
                          If Not tutte_auto_non_in_altro_ddt Then
                              %>
                              <center><font color="red" style="font-weight:bold"><%="Le vetture " & auto_altro_ddt & " risultano in un altro DDT attualmente aperto. Impossibile salvare la bisarca corrente."%></font></center>
                          <%
                          End If
                          
                          If Not tutte_auto_non_in_nolo Then
                              %>
                              <center><font color="red" style="font-weight:bold"><%="Le vetture " & auto_noleggiate & " risultano noleggiate. Impossibile salvare la bisarca corrente."%></font></center>
                          <%
                          End If
                      
                      End If
                  End If
                 
                 
                          
             %>
           </td>
           </tr>
         </table>
         <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
         <tr>
           <td align="left" class="style20" >
             <asp:Label ID="lblTarga3" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
               <asp:TextBox ID="txtTarga" runat="server"></asp:TextBox>
           </td>
           <td align="left" class="style14">
             <%If btnSalva.Enabled And id_ddt.Text = "" Then%>
               <input name="inserisci" title="no Stop sell" type="submit" value="Inserisci" />
             <%Else%>
               <input name="inserisci" disabled="disabled" title="no Stop sell" type="submit" value="Inserisci" />
             <%End If%>
           </td>
           <td align="left" class="style15">

               &nbsp;</td>
          
           <td align="right" class="style17" colspan="3">

               
           </td>
          
         </tr>
         </table>
         <table border="0" cellpadding="2" cellspacing="2" width="1024px" >
         <tr>
           <td align="left" colspan="4">
                
                 
 
              <input id="Text4" name="numAuto" type="hidden" style="width:50px;" value="<%=i %>" />
              
              <%  If Not auto_esiste Then
                  %> 
                      <center><asp:Label ID="lblErrore" runat="server" Font-Bold="True" ForeColor="Red" Text="Targa inesistente." ></asp:Label></center>
                  <%
                  End If
    
              %>

              <%  If Not auto_in_stazione_uscita Then
                  %> 
                      <center><asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red" Text="Il veicolo specificato risulta a carico di un'altra stazione." ></asp:Label></center>
                  <%
                  End If
    
              %>

             <% If auto_in_nolo Then
              %>
                <center><asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red" Text="L'auto è attualmente in movimento (noleggio o trasferimento). Impossibile inserire in bisarca." ></asp:Label></center>
             <%
             End If
              %>

              <%  If Not auto_non_inserita Then
                  %> 
                      <center><asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Text="Targa già inserita nel DDT corrente." ></asp:Label></center>
                  <%
                  End If
              %>
              
             <%  If carica_dati Then
  
                     %>
                 
                 <table border="0" cellpadding="0" cellspacing="0">
              <%  'COSTRUZIONE DELLA TABELLA DELLE AUTO DA DB
                  For k = 1 To i
                      
                      %>
                      <tr>
                          <td>
                            <asp:Label ID="lblTarga2" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
                            <input id="Text16" readonly="readonly" name="<%="numAuto_" & k %>" type="text" value="<%=getTarga(id_veicoli_db(k))%>" />&nbsp;
                            <asp:Label ID="lblKM" runat="server" Text="KM" CssClass="testo_bold"></asp:Label>
                            <input id="Text17" readonly="readonly" name="<%="kmAuto_" & k %>" type="text" value="<%=getKM_uscita(id_veicoli_db(k),id_ddt.Text)%>" style="width:50px;" />&nbsp;
                            <asp:Label ID="lblSerbatoio" runat="server" Text="Serbatoio" CssClass="testo_bold"></asp:Label>
                            <input id="Text18" name="<%="tangaAuto_" & k %>" type="text" value="<%=getSerbatoio_uscita(id_veicoli_db(k),id_ddt.Text)%>" style="width:50px;" />/<input id="Text19" readonly="readonly" name="<%="tangaMaxAuto_" & k %>" type="text" value="<%=getSerbatoioMax_byId(id_veicoli_db(k))%>" style="width:50px;" />
                            <% If btnSalva.Enabled And id_ddt.Text = "" Then%>
                             <input name="<%="elimina_" & k %>" title="Rimuovi auto" type="submit" value="X"   />
                            <% End If%>
                          </td>          
                      </tr>           
                      <%
                  Next
                  
                  %>
              </table>
             <% Else%>
              <table border="0" cellpadding="0" cellspacing="0">
              <%  'COSTRUZIONE DELLA TABELLA DELLE AUTO IN CASO DI NUOVO INSERIMENTO O SECONDO REFRESH
                  For k = 1 To i
                      dropStazioneUscita.Enabled = False
                      If Request.Form("elimina_" & k) = "" Then
                      %>
                      <tr>
                        <% If k < i And Request.Form("numAuto_" & k) <> "" Then%>
                          <td>
                            <b>Targa:</b> <input id="Text1" readonly="readonly" name="<%="numAuto_" & k %>" type="text" value="<%=Request.Form("numAuto_" & k) %>" />&nbsp;
                            <b>KM:</b> <input id="Text7" readonly="readonly" name="<%="kmAuto_" & k %>" type="text" value="<%=Request.Form("kmAuto_" & k)%>" style="width:50px;" />&nbsp;
                            <b>Serbatoio:</b> <input id="Text10" readonly="readonly"  name="<%="tangaAuto_" & k %>" type="text" value="<%=Request.Form("tangaAuto_" & k)%>" style="width:50px;" />/<input id="Text15" readonly="readonly" name="<%="tangaMaxAuto_" & k %>" type="text" value="<%=Request.Form("tangaMaxAuto_" & k)%>" style="width:50px;" />
                            <%If btnSalva.Enabled And id_ddt.Text = "" Then%><input name="<%="elimina_" & k %>" title="Rimuovi auto" type="submit" value="X"   /><%End If%>
                          </td>
                        <%ElseIf k = i And inserisci <> "" And auto_esiste And auto_non_inserita Then 'prima riga%>
                          <td>
                            <b>Targa:</b> <input id="Text3" readonly="readonly" name="<%="numAuto_" & k %>" type="text" value="<%=txtTarga.Text%>" />&nbsp;
                            <b>KM:</b> <input id="Text5" readonly="readonly" name="<%="kmAuto_" & k %>" type="text" value="<%=getKM(Trim(txtTarga.Text))%>" style="width:50px;" />&nbsp;
                            <b>Serbatoio:</b> <input id="Text8" readonly="readonly" name="<%="tangaAuto_" & k %>" type="text" value="<%=getSerbatoio(Trim(txtTarga.Text))%>" style="width:50px;" />/<input id="Text11" readonly="readonly" name="<%="tangaMaxAuto_" & k %>" type="text" value="<%=getSerbatoioMax(Trim(txtTarga.Text))%>" style="width:50px;" />
                            <%If btnSalva.Enabled And id_ddt.Text = "" Then%><input name="<%="elimina_" & k %>" title="Rimuovi auto" type="submit" value="X"   /><%End If%>
                          </td>
                        <%ElseIf Request.Form("numAuto_" & k) <> "" Then%>
                          <td>
                            <b>Targa:</b> <input id="Text2" readonly="readonly" name="<%="numAuto_" & k %>" type="text" value="<%=Request.Form("numAuto_" & k) %>" />&nbsp;
                            <b>KM:</b> <input id="Text6" readonly="readonly" name="<%="kmAuto_" & k %>" type="text" value="<%=Request.Form("kmAuto_" & k)%>" style="width:50px;" />&nbsp;
                            <b>Serbatoio:</b> <input id="Text9" readonly="readonly" name="<%="tangaAuto_" & k %>" type="text" value="<%=Request.Form("tangaAuto_" & k)%>" style="width:50px;" />/<input id="Text12" readonly="readonly" name="<%="tangaMaxAuto_" & k %>" type="text" value="<%=Request.Form("tangaMaxAuto_" & k)%>" style="width:50px;" />
                            <%If btnSalva.Enabled And id_ddt.Text = "" Then%><input name="<%="elimina_" & k %>" title="Rimuovi auto" type="submit" value="X"   /><%End If%>
                          </td>
                        <% End If%>
                      </tr>
                      
                      <%
                      Else
                        
                        %>
                        <%--<tr>
                        <td>
                            <b>Targa:</b> <input id="Text20" readonly name="<%="numAuto_" & k %>" type="text" value="" />&nbsp;
                            <b>KM:</b> <input id="Text21" readonly name="<%="kmAuto_" & k %>" type="text" value="" style="width:50px;" />&nbsp;
                            <b>Serbatoio:</b> <input id="Text22" readonly name="<%="tangaAuto_" & k %>" type="text" value="" style="width:50px;" />/<input id="Text23" readonly name="<%="tangaMaxAuto_" & k %>" type="text" value="" style="width:50px;" />  
                        </td>
                        </tr>--%>
                        <%
                      End If
                      Next
                  
                  %>
              </table>
              <% End If%>
           </td>
         </tr>
       </table>
          
        
        <table border="0" cellpadding="3" cellspacing="3" width="1024px" >
         <tr>
           <td align="center">   
           <%If id_ddt.Text = "" Then%>
            <asp:Button ID="btnSalva" runat="server" Text="Salva" BackColor="#369061" Font-Bold="True" Font-Size="Small" ForeColor="White" ValidationGroup="invia" />  
           <%End If%>
            &nbsp;
               
            <asp:Button ID="btnAnnulla" runat="server" Text="Chiudi" BackColor="#369061" Font-Bold="True" Font-Size="Small" ForeColor="White" />  

           </td>
         </tr>
         <tr>
           <td align="center">
              <% If btnSalva.Enabled And id_ddt.Text <> "" And id_stazione_utente.Text = stazione_rientro_ins.SelectedValue Then%>
                  <input name="chiudi_ddt" type="submit" value="Salva auto e conferma presa in carico." onclick="javascript: return(window.confirm ('Il Documento di Trasporto selezionato verrà memorizzato e chiuso; le auto attualmente presenti in lista verranno rese disponibili per la stazione di arrivo. Sei sicuro di voler procedere?'));" />
               <% End If%>
           </td>
         </tr>
     </table>
    </div>

    <%--pannello allegati 20.09.2022 salvo--%>

   <%-- pannello allegati (visibile solo su dettaglio) <%=id_ddt.Text %>--%>

                      <%--  <ContentTemplate>--%>
                            <div id="divAllegInvioDoc" runat="server" style="height:500px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" style="color: #FFFFFF" bgcolor="#444"><b>Documenti allegati </b> </td>

                                    </tr></table>
                                
                                <%--<asp:Panel ID="PanelAllegati" runat="server" ScrollBars="Auto">--%>

                                    <div id="allegati" runat="server" visible="true" style="max-height:120px">


                                         
                                <table style="margin-top:10px;">
                                    <tr>
                                        <td><asp:Label ID="lblUploaMaunDoc" runat="server" CssClass="testo_bold" ForeColor="#444"  Text="Allega documenti:"></asp:Label></td>
                                        <td>&nbsp; <asp:Label ID="lblTipoAlleg" runat="server" Text="Tipo Doc." CssClass="testo_bold"></asp:Label></td>
                                        <td >
                                            <asp:DropDownList ID="DropTipoAllegato" runat="server" AppendDataBoundItems="True" DataSourceID="sqlTipoAllegato" DataTextField="TipoAllegato" DataValueField="Id">
                                            <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td ><asp:FileUpload ID="FileUploadAllegati" size="42" runat="server" /></td>
                                        <td>
                                            <asp:Button ID="btnMemorizzaAlleg" runat="server" Text="Memorizza allegato" ValidationGroup="Upload_Allegati"  />
                                            
                                        </td>
                                    </tr>
                                </table> 

                                    <hr style="clear:both; color:White;" />

                                        <table cellpadding="0" cellspacing="2" width="100%" style="font-size:x-small;" border="0" runat="server" id="table2">
                                            <tr><td>
                                                <asp:ListView ID="ListViewAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="Id">
                                                    <ItemTemplate>
                                                        <tr style="background-color:#DCDCDC; color: #000000;">
                                                            <td><asp:Label ID="lblIdAllegato" runat="server" Width="20px"  Text='<%# Eval("Id") %>'></asp:Label>
                                                            </td>
                                                            <td><asp:Label ID="lblTipo" runat="server" Width="40px" Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server" Width="150px" Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblPercorsoFile" runat="server" Width="180px" Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione?');" /></td>
                                                            <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style=""><td><asp:Label ID="lblIdAllegato" runat="server" Width="20px"  Text='<%# Eval("Id") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblTipo" runat="server" Width="40px" Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server" Width="150px" Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblPercorsoFile" runat="server" Width="180px" Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione?');" /></td>
                                                            <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <table id="Table1" runat="server" style="">
                                                            <tr>
                                                                <td class="testo_bold">Non vi sono allegati </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table id="Table2" runat="server" width="100%">
                                                            <tr id="Tr1" runat="server">
                                                                <td id="Td1" runat="server">
                                                                    <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                                                        <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                                                            <th id="Th1" runat="server">Id</th>
                                                                            <th id="Th2" runat="server">Tipo</th>
                                                                            <th id="Th3" runat="server">Nome File</th>
                                                                            <th id="Th4" runat="server">Percorso File</th>
                                                                            <th id="Th5" runat="server"></th>
                                                                            <th id="Th6" runat="server"></th>
                                                                            <th id="Th7" runat="server"></th>
                                                                        </tr>
                                                                        <tr ID="itemPlaceholder" runat="server"></tr>
                                                                    </table>
                                                                </td>
                                                           </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <SelectedItemTemplate>
                                                        <tr style="">
                                                            <td><asp:Label ID="lblIdAllegato" runat="server" Width="20px"  Text='<%# Eval("Id") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblTipo" runat="server" Width="40px" Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server" Width="150px" Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblPercorsoFile" runat="server" Width="180px" Font-Size="X-Small" Text='<%# Eval("PercorsoFile") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                                                            <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>
                                                        </tr>
                                                    </SelectedItemTemplate>
                                                </asp:ListView>
                                                </td></tr>
                                        </table>
                                    </div>
                               <%-- </asp:Panel>--%>



                             




                        </div>

                  


<%--            </ContentTemplate>--%>

    <asp:SqlDataSource ID="sqlAllegati" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT dbo.trasporto_veicoli_allegati.Id, dbo.trasporto_veicoli_TipoAllegato.TipoAllegato, dbo.trasporto_veicoli_allegati.NomeFile, dbo.trasporto_veicoli_allegati.PercorsoFile 
                    FROM dbo.trasporto_veicoli_allegati WITH (NOLOCK) INNER JOIN dbo.trasporto_veicoli_TipoAllegato WITH (NOLOCK) ON dbo.trasporto_veicoli_allegati.Idtipodocumento = dbo.trasporto_veicoli_TipoAllegato.Id 
                    WHERE dbo.trasporto_veicoli_allegati.Id_rif=0">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlTipoAllegato" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [Id], [TipoAllegato] FROM [trasporto_veicoli_TipoAllegato] WITH(NOLOCK) order by TipoAllegato">
</asp:SqlDataSource>


    <%--end pannello allegati--%>


    <asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, STR(codice) + ' - ' + nome_stazione As stazione FROM stazioni ORDER BY codice"></asp:SqlDataSource>
    
    <%--<asp:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
    </asp:toolkitscriptmanager>--%>
        
    <asp:SqlDataSource ID="sqlDDT" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT documenti_di_trasporto.id, documenti_di_trasporto.stato_trasferimento, STR(stazioni_uscita.codice) + ' - ' + stazioni_uscita.nome_stazione As stazione_uscita, STR(stazioni_rientro.codice) + ' - ' + stazioni_rientro.nome_stazione As stazione_rientro,documenti_di_trasporto.data_uscita, documenti_di_trasporto.data_presunto_rientro, documenti_di_trasporto.data_rientro, documenti_di_trasporto.note  FROM documenti_di_trasporto INNER JOIN stazioni As stazioni_uscita ON documenti_di_trasporto.id_stazione_uscita=stazioni_uscita.id INNER JOIN stazioni As stazioni_rientro ON documenti_di_trasporto.id_stazione_rientro=stazioni_rientro.id"></asp:SqlDataSource>
   
   <% If Request.Form("id_modifica") <> "" And Not pannello_ricerca.Visible Then%>
       <input id="Text13" name="id_modifica" value="<%=Request.Form("id_modifica") %>" type="hidden" style="width:50px;" />
   <% Else%>
       <input id="Text14" name="id_modifica" value="<%=id_ddt.Text %>" type="hidden" style="width:50px;" />
   <%End If %>
      
   <asp:Label ID="txtQuery" runat="server" name="lbl_id_codice" visible="false" ></asp:Label>
   <asp:Label ID="id_ddt" runat="server" name="lbl_id_codice" visible="false" ></asp:Label>
   <asp:Label ID="id_stazione_utente" runat="server" name="lbl_id_codice" visible="false" ></asp:Label>
   
   
         
    <br />
               <asp:CompareValidator ID="CompareValidator7" runat="server" 
                    ControlToValidate="stazione_uscita_ins" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare la stazione di uscita.">
               </asp:CompareValidator>
               <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="stazione_rientro_ins" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare la stazione di rientro.">
               </asp:CompareValidator>

               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="data_rientro_ins" ErrorMessage="Specificare la data di rientro." 
                   Font-Size="0pt" SetFocusOnError="True" ValidationGroup="invia"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ControlToValidate="ore_rientro" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia" ValueToCompare="-1" Font-Size="0pt" 
                    ErrorMessage="Specificare l'ora di rientro.">
               </asp:CompareValidator>
               <asp:CompareValidator ID="CompareValidator5" runat="server" 
                    ControlToValidate="minuti_rientro" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="invia" ValueToCompare="-1" Font-Size="0pt" 
                    ErrorMessage="Specificare i minuti di rientro.">
               </asp:CompareValidator>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="invia" />    
         
</asp:Content>

