<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="car_control.aspx.vb" Inherits="car_control" title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
      
        .style1
        {
            width: 281px;
        }
        .style2
        {
            width: 137px;
        }
        .style3
        {
            width: 87px;
        }
        .style4
        {
            width: 101px;
        }
      
        .style5
        {
            width: 387px;
        }
        .style6
        {
            width: 31px;
        }
        .style7
        {
            width: 36px;
        }
      
        .style8
        {
            width: 204px;
        }
        .style9
        {
            width: 14px;
        }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" 
                 class="style1">
             <b>&nbsp;Previsione x Targa </b>
           </td>
         </tr>
        </table>
        <table width="1024px" cellpadding="0" cellspacing="2" style="border: 4px solid #444">
         <tr>
           <td align="left" class="style2">
               <asp:Label ID="lblStazione" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
           </td>
           <td align="left" class="style2">
              <asp:Label ID="lblCat" runat="server" Text="Gruppi" CssClass="testo_bold"></asp:Label></td>
           <td class="style3">
           
             <asp:Label ID="lblDaData" runat="server" Text="Da data:" CssClass="testo_bold"></asp:Label>
             </td>
           <td class="style4">
           
             <asp:Label ID="lblAData" runat="server" Text="A data:" CssClass="testo_bold"></asp:Label>
             </td>
           <td>
           
             <asp:Label ID="lblAData0" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
             </td>
         </tr>
         <tr>
           <td align="left" class="style2">
               <asp:DropDownList ID="dropStazioni" runat="server" 
                   DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id" 
                   style="margin-left: 0px" AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
           </td>
           <td align="left" class="style2">
               <asp:DropDownList ID="dropGruppi" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="sqlGruppi" DataTextField="cod_gruppo" DataValueField="id_gruppo">
                   <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
               </asp:DropDownList>
             </td>
           <td class="style3">
           <a onclick="Calendar.show(document.getElementById('<%=txtDaData.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtDaData" runat="server" Width="74px"></asp:TextBox></a>

              <%-- <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDaData">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDaData">
                </asp:MaskedEditExtender>
           
           </td>
           <td class="style4">
           <a onclick="Calendar.show(document.getElementById('<%=txtAData.ClientID%>'), '%d/%m/%Y', false)"> 
               <asp:TextBox ID="txtAData" runat="server" Width="78px"></asp:TextBox></a>

              <%-- <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAData">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAData">
                </asp:MaskedEditExtender>
           
           &nbsp;&nbsp;
                          
           </td>
           <td>
           
               <asp:DropDownList ID="dropStato" runat="server" AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
                   <asp:ListItem Value="1">Disponibili Nolo</asp:ListItem>
                   <asp:ListItem Value="2">In Movimento</asp:ListItem>
               </asp:DropDownList>
             </td>
         </tr>
         <tr>
           <td align="center" colspan="5">
               <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" />
           
             </td>
         </tr>
   </table>
   <br />
   <%
       Dim cont As Integer = 0

       If dropStazioni.SelectedValue > 0 Then
    %>
   
    <table border="1" cellpadding="0" cellspacing="0">
   <tr> 
     <td bgcolor="white" width="80px">TARGA</td>
     <td bgcolor="white" width="242px">MODELLO</td>
     <td bgcolor="white" width="10px">CAT.</td>
   <% 
       Dim dataIniziale As DateTime
       Dim dataFinale As DateTime
       Dim Data As DateTime
       Dim fine As Integer

       If txtDaData.Text <> "" And txtAData.Text <> "" Then
           dataIniziale = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
           Data = funzioni_comuni.getDataDb_senza_orario2(txtAData.Text)
           fine = DateDiff(DateInterval.Day, dataIniziale, Data) + 1

           Data = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)

           dataFinale = dataIniziale.AddDays(fine - 1)


       ElseIf txtDaData.Text <> "" And txtAData.Text = "" Then
           dataIniziale = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
           Data = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
           fine = 7
           dataFinale = dataIniziale.AddDays(fine - 1)
       Else
           dataIniziale = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))
           Data = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))
           fine = 7
           dataFinale = dataIniziale.AddDays(fine - 1)
       End If


       Dim i As Integer = 0
       Do While i < fine


    %>
       <td width="20px" align="center" bgcolor="#FFFFCC"><%=Day(Data)%></td>
    <%
            Data = Data.AddDays(1)
            i = i + 1
        Loop
        %>
   </tr>
   
     <%
         Dim condizioneGruppo As String = ""

         If dropGruppi.SelectedValue > 0 Then
             condizioneGruppo = " AND modelli.id_gruppo='" & dropGruppi.SelectedValue & "'"
         End If

         Dim condizioneStato As String = ""
         If dropStato.SelectedValue <> "0" Then
             If dropStato.SelectedValue = "1" Then  'disponibili al nolo
                 condizioneStato = " AND veicoli.disponibile_nolo='1' "
             ElseIf dropStato.SelectedValue = "2" Then  'in movimento
                 condizioneStato = " AND (veicoli.noleggiata='1' OR NOT odl_aperto IS NULL) "
             End If
         End If

         Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
         Dbc.Open()
         Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
         Dbc1.Open()

         'SELEZIONO TUTTE LE AUTO DISPONIBILI AD OGGI NEL MIO PARCO E LE AUTO CHE NEL PERIODO SELEZIONATO ENTRERANNO NEL MIO PARCO DA ALTRA
         'STAZIONE (DA MOVIMENTI VEICOLI)

         Dim sqlStr As String = "(SELECT veicoli.da_riparare, veicoli.da_rifornire,veicoli.disponibile_nolo,veicoli.odl_aperto, veicoli.targa, veicoli.id,  modelli.descrizione As modello, gruppi.cod_gruppo As gruppo, '1' As disponibile_in_stazione, NULL AS presunto_arrivo_nondisp, NULL As tipo_movimento " &
         " FROM veicoli WITH(NOLOCK) INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello" &
         " INNER JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo = gruppi.id_gruppo " &
         " WHERE   ISNULL(veicoli.venduta,'0')='0' AND ISNULL(veicoli.in_vendita,'0')='0' AND ISNULL(veicoli.venduta_da_fattura,'0')='0' AND ISNULL(veicoli.furto,'0')='0'" &
         " AND veicoli.id_stazione='" & dropStazioni.SelectedValue & "' " & condizioneGruppo & condizioneStato & " AND da_rifornire='0') " &
         "UNION  " &
         " (SELECT veicoli.da_riparare, veicoli.da_rifornire,veicoli.disponibile_nolo,veicoli.odl_aperto, veicoli.targa, veicoli.id, modelli.descrizione As modello, gruppi.cod_gruppo As gruppo, '0' As disponibile_in_stazione, movimenti_targa.data_presunto_rientro As presunto_arrivo_nondisp, movimenti_targa.id_tipo_movimento As tipo_movimento  " &
         " FROM movimenti_targa WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo=veicoli.id " &
         " INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello " &
         " INNER JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo = gruppi.id_gruppo " &
         " WHERE id_stazione_uscita<>'" & dropStazioni.SelectedValue & "' AND id_stazione_presunto_rientro='" & dropStazioni.SelectedValue & "' AND veicoli.id_stazione<>'" & dropStazioni.SelectedValue & "' " &
         " AND movimento_attivo='1'" & condizioneGruppo & condizioneStato & " and data_presunto_rientro >= GETDATE()) " &
         "UNION  " &
         " (SELECT veicoli.da_riparare, veicoli.da_rifornire,veicoli.disponibile_nolo,veicoli.odl_aperto, veicoli.targa, veicoli.id, modelli.descrizione As modello, gruppi.cod_gruppo As gruppo, '0' As disponibile_in_stazione, movimenti_targa.data_presunto_rientro As presunto_arrivo_nondisp, movimenti_targa.id_tipo_movimento As tipo_movimento  " &
         " FROM movimenti_targa WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo=veicoli.id " &
         " INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello " &
         " INNER JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo = gruppi.id_gruppo " &
         " WHERE id_stazione_uscita<>'" & dropStazioni.SelectedValue & "' AND id_stazione_presunto_rientro='" & dropStazioni.SelectedValue & "' AND veicoli.id_stazione<>'" & dropStazioni.SelectedValue & "' " &
         " AND movimento_attivo='1'" & condizioneGruppo & condizioneStato & " and data_presunto_rientro < GETDATE()) " &
         " UNION " &
        " (SELECT veicoli.da_riparare, veicoli.da_rifornire, veicoli.disponibile_nolo,veicoli.odl_aperto, veicoli.targa, veicoli.id, modelli.descrizione As modello, " &
        " gruppi.cod_gruppo As gruppo, '0' As disponibile_in_stazione, movimenti_targa.data_presunto_rientro As " &
        " presunto_arrivo_nondisp, movimenti_targa.id_tipo_movimento As tipo_movimento  " &
        " From movimenti_targa WITH(NOLOCK) INNER Join veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo=veicoli.id  " &
        " INNER Join modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello  INNER JOIN gruppi WITH(NOLOCK) " &
        " On modelli.id_gruppo = gruppi.id_gruppo  "

         If dropStato.SelectedValue <> "0" Then
             If dropStato.SelectedValue = "1" Then  'disponibili al nolo comprese quelle in Rifornimento 02.10.2021
                 sqlStr += " WHERE(veicoli.id_stazione ='" & dropStazioni.SelectedValue & "' AND veicoli.da_rifornire='1' and movimento_attivo='1')) "
             ElseIf dropStato.SelectedValue = "2" Then  'in movimento viene esclusa quella in Rifornimento
                 sqlStr += " WHERE(veicoli.id_stazione ='" & dropStazioni.SelectedValue & "' AND veicoli.da_rifornire='0' and movimento_attivo='1' and id_tipo_movimento IS NULL)) "
             End If
         Else
             sqlStr += " WHERE(veicoli.id_stazione ='" & dropStazioni.SelectedValue & "' AND veicoli.da_rifornire='1' and movimento_attivo='1')) "  'Originale
         End If


         sqlStr += "ORDER BY gruppo, targa"

         'Response.Write(sqlStr) 'query OK 

         Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
         Dim Cmd1 As New Data.SqlClient.SqlCommand("'", Dbc1)

         Dim Rs As Data.SqlClient.SqlDataReader
         Rs = Cmd.ExecuteReader()



         Do While Rs.Read()
             'CREAZIONE DELLE RIGHE PER OGNI VEICOLO

      %>
         <tr>
            <td bgcolor="#E0E0E0" style="width:100px;"><%=Rs("targa")%></td>
            <td bgcolor="#E0E0E0"><%=Rs("modello")%></td>
            <td bgcolor="#E0E0E0"><%=Rs("gruppo")%></td>
             <% 




                 'PER OGNI VEICOLO CONTROLLO SE VI SONO DEI MOVIMENTI PREVISTI O IN CORSO
                 Dim interventi(30) As Char
                 Dim prenotazioni(30) As Char
                 Dim contratti(30) As Char
                 Dim trasferimenti(30) As Char
                 Dim lavaggi(30) As Char
                 Dim codice As String
                 Dim colore As String
                 Dim dimensione_font As String
                 Dim colore_font As String = ""

                 If Rs("targa") = "FZ 476 NS" Then
                     colore_font = ""
                 End If



                 If Rs("disponibile_in_stazione") Then
                     interventi = getLavoriIntervento(Rs("id"), dataIniziale, dataFinale, dropStazioni.SelectedValue)
                     prenotazioni = getPrenotazioni(Rs("targa"), dataIniziale, dataFinale, dropStazioni.SelectedValue)
                     contratti = getContratti(Rs("id"), dataIniziale, dataFinale, dropStazioni.SelectedValue)
                     trasferimenti = getTrasferimenti(Rs("id"), dataIniziale, dataFinale, dropStazioni.SelectedValue)
                     lavaggi = getLavaggi(Rs("id"), dataIniziale, dataFinale, dropStazioni.SelectedValue)
                 End If

                 i = 0
                 Data = dataIniziale

                 Do While i < fine
                     If Rs("disponibile_in_stazione") Then
                         'SE L'AUTO E' DISPONIBILE NELLA STAZIONE NELLA DATA DI INIZIO CALCOLO
                         If prenotazioni(i) = "0" And interventi(i) = "0" And contratti(i) = "0" And trasferimenti(i) = "0" And lavaggi(i) = "0" Then
                             If Rs("da_riparare") And Not Rs("disponibile_nolo") And (Rs("odl_aperto") & "") = "" Then
                                 codice = "F"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             Else
                                 colore = "green"
                                 codice = ""
                                 dimensione_font = ""
                                 colore_font = "black"

                             End If
                         Else
                             'IL CONTRATTO HA PRIORITA' SU TUTTO (UNICO DATO CERTO)

                             If contratti(i) = "1" And prenotazioni(i) = "1" Then
                                 'SE PER IL GIORNO HO SIA UN CONTRATTO CHE UNA PRENOTAZIONE (GRUPPO SPECIALE) DEVE ESSERE SEGNALATO
                                 codice = "C+P"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf contratti(i) = "1" Then
                                 codice = "C"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf contratti(i) = "3" Then
                                 codice = "C"
                                 colore = "purple"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf contratti(i) = "2" Then
                                 codice = ""
                                 colore = "black"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf trasferimenti(i) = "1" Then
                                 codice = "T"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf lavaggi(i) = "1" Then
                                 codice = "L"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf trasferimenti(i) = "3" Then
                                 codice = "T"
                                 colore = "purple"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf lavaggi(i) = "3" Then
                                 codice = "L"
                                 colore = "purple"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf trasferimenti(i) = "2" Then
                                 codice = "T"
                                 colore = "black"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf prenotazioni(i) = "1" Then
                                 codice = "P"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf interventi(i) = "1" Then
                                 codice = "O"
                                 colore = "red"
                                 dimensione_font = ""
                                 colore_font = "black"
                             ElseIf prenotazioni(i) = "2" Then
                                 codice = ""
                                 colore = "black"
                                 dimensione_font = ""
                                 'colore_font = "black"
                                 colore_font = "white"
                             ElseIf interventi(i) = "2" Then    'ODL
                                 codice = "O"
                                 colore = "black"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf interventi(i) = "3" Then
                                 codice = "O"
                                 colore = "purple"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf contratti(i) = "4" Then
                                 codice = "CRV"
                                 colore = "red"
                                 dimensione_font = "font-size:x-small"
                                 colore_font = "black"
                             Else
                                 'SERVE SOLO PER ACCORGERSI DI EVENTUALI BUG

                                 codice = ""
                                 colore = "white"
                                 dimensione_font = ""
                                 colore_font = "black"
                             End If
                         End If
                     Else
                         'ALTRIMENTI SI TRATTA DI UN VEICOLO CHE DEVE RIENTRARE NELLA MIA STAZIONE - SEGNO IN NERO I GIORNI IN CUI NON E' DISPONILE
                         If Rs("presunto_arrivo_nondisp") & "" <> "" Then
                             Dim presunto_arrivo As DateTime = funzioni_comuni.getDataDb_senza_orario2(Rs("presunto_arrivo_nondisp"))

                             Dim data_oggi As Date = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))


                             If DateDiff(DateInterval.Day, presunto_arrivo, Data) <= 0 Then
                                 If Rs("tipo_movimento") = Costanti.idMovimentoNoleggio Then
                                     codice = "C"
                                 ElseIf Rs("tipo_movimento") = Costanti.idMovimentoInterno Or Rs("tipo_movimento") = Costanti.idBisarca Then
                                     codice = "T"
                                 ElseIf Rs("tipo_movimento") = Costanti.idMovimentoODL Then
                                     codice = "O"
                                 Else
                                     codice = ""
                                 End If

                                 colore = "black"
                                 dimensione_font = ""
                                 colore_font = "white"
                             ElseIf DateDiff(DateInterval.Day, presunto_arrivo, data_oggi) > 0 Then
                                 'IN QUESTO CASO IL PRESUNTO RIENTRO DEL VEICOLO E' PRECEDNETE ALLA DATA ODIERNA
                                 If Rs("tipo_movimento") = Costanti.idMovimentoNoleggio Then
                                     codice = "C"
                                 ElseIf Rs("tipo_movimento") = Costanti.idMovimentoInterno Or Rs("tipo_movimento") = Costanti.idBisarca Then
                                     codice = "T"
                                 ElseIf Rs("tipo_movimento") = Costanti.idMovimentoODL Then
                                     codice = "O"
                                 Else
                                     codice = ""
                                 End If

                                 colore = "purple"
                                 dimensione_font = ""
                                 colore_font = "white"
                             Else
                                 codice = ""
                                 colore = "green"
                                 dimensione_font = ""
                                 colore_font = "black"
                             End If
                         Else
                             'PRESUNTO RIENTRO NON DISPONIBILE

                             colore_font = "white"
                             If Rs!da_rifornire = True Then
                                 dimensione_font = "font-size:small"
                                 codice = "R"   'rifornimento
                                 colore = "green"
                             Else
                                 codice = ""
                                 dimensione_font = ""
                                 colore = "black"
                                 colore_font = "white"
                             End If


                         End If
                     End If


              %>
                       <td width="20px" align="center" bgcolor='<%=colore%>' style='font-family:Verdana, Geneva, Tahoma, sans-serif; color:<%=colore_font%>;<%=dimensione_font%>; '>
                           &nbsp;<%=codice%>&nbsp;
                       </td>
              <%
                      i = i + 1
                      Data = Data.AddDays(1)



                  Loop
             %> 
         </tr>
      <%       
              cont += 1 'incrementa conteggio n.righe

          Loop

         %>

        <tr style="height:22px;">
            <td colspan="4" width="20px" align="left" style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:14px;border:hidden;padding:2px;'>
                  Totale: &nbsp;<%=cont %>&nbsp; <!-- inserita riga per conteggio 02.10.2021 -->
            </td>
        </tr>


   </table>
    
    
  
   <br />&nbsp;
  
   <table style="width:100%;" border="1" class="testo">
        <tr>
            <td colspan="6">
                <asp:Label ID="Label4" runat="server" Text="Legenda:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  bgcolor="green" class="style6">&nbsp;</td>
            <td class="style5">
                <asp:Label ID="Label11" runat="server" Text="Disponibile" ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label6" runat="server" Text="C" ></asp:Label>
            </td>
            <td class="style8">
               <asp:Label ID="Label5" runat="server" Text="Contratto" ></asp:Label>
            </td>
            <td class="style9" align="center">
              <asp:Label ID="Label15" runat="server" Text="O" ></asp:Label>
            </td>
            <td>
              <asp:Label ID="Label16" runat="server" Text="Odl" ></asp:Label>
            </td>
         </tr>
        <tr>
            <td  bgcolor="red" class="style6">&nbsp;</td>
            <td class="style5">
                <asp:Label ID="Label3" runat="server" Text="Non disponibile." ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label7" runat="server" Text="P" ></asp:Label>
            </td>
            <td class="style8">
               <asp:Label ID="Label8" runat="server" Text="Prenotazione" ></asp:Label>
            </td>
            <td class="style9" align="center">
              <asp:Label ID="Label17" runat="server" Text="F" ></asp:Label>
            </td>
            <td>
              <asp:Label ID="Label18" runat="server" Text="Fermo Tecnico" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td  bgcolor="black" class="style6">&nbsp;</td>
            <td class="style5">
               <asp:Label ID="Label2" runat="server" Text="In altra stazione." ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label9" runat="server" Text="T" ></asp:Label>
            </td>
            <td class="style8">
               <asp:Label ID="Label10" runat="server" Text="Trasferimento" ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label19" runat="server" Text="L" ></asp:Label>
            </td>
            <td class="style8">
               <asp:Label ID="Label20" runat="server" Text="Lavaggio" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td  bgcolor="purple" class="style6">&nbsp;</td>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Text="Non disponibile con rientro precedente rispetto alla data odierna." ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label12" runat="server" Text="CRV" ></asp:Label>
            </td>
            <td class="style8">
               <asp:Label ID="Label13" runat="server" Text="Crv - Auto in attesa di check in." ></asp:Label>
            </td>
            <td class="style7" align="center">
               <asp:Label ID="Label21" runat="server" Text="R" ></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="Label22" runat="server" Text="Rifornimento" ></asp:Label>
            </td>
        </tr>         
        <tr>
            <td colspan="6">
               <asp:Label ID="Label14" runat="server" Text="Il rientro della vettura avviene all'interno dell'ultima giornata con colore diverso da verde." ></asp:Label>
            </td>
        </tr>         
   </table>
   
   
   <%
       Rs.Close()
       Cmd.Dispose()
       Cmd = Nothing
       Dbc.Close()
       Rs = Nothing
       Dbc = Nothing

        %>
        
 <% End If 'IF SU CONTROLLO dropStazioni %>       
   
         <asp:Label ID="livelloAccesso" runat="server" visible="false"></asp:Label>
                 

         <asp:SqlDataSource ID="sqlStazioni" runat="server" 
             ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
             SelectCommand="SELECT ( STR(codice) + ' - ' + nome_stazione) As stazione, [id] FROM stazioni WITH(NOLOCK) where attiva=1 ORDER BY codice"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_gruppo, cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo"></asp:SqlDataSource>


            
</asp:Content>

