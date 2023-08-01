<%@ Page Title="" Language="VB" MasterPageFile="/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="prova_ws.aspx.vb" Inherits="prova_ws" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div id="Hello World">     
        &nbsp;   
        <asp:Button ID="Button1" runat="server" Text="In Sviluppo. Clicca qui per vedere il database in uso" />
        &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>        
    </div>
    <br />   
    <br />
    <div id="somma">
        &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Inserire primo addendo"></asp:Label>   
        &nbsp;
        <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Inserire secondo addendo"></asp:Label> 
        &nbsp;  
        <asp:TextBox ID="txt2" runat="server"></asp:TextBox>
        &nbsp;        
        <asp:Button ID="btnSomma" runat="server" Text="Fai la somma" />
        &nbsp;
        <asp:Label ID="lblSomma" runat="server" Text="Risultato"></asp:Label>        
    </div>
     <br />
     <br />
     <div id="PickUpDropOff">                                          
                    <asp:Label ID="Label4" runat="server" Text="Risultato Elenco Stazioni: " 
                        Font-Bold="True"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblElencoStazioni" runat="server" Text="Label"></asp:Label><br /><br />

                    <asp:Label ID="Label31" runat="server" Text="Risultato Elenco Tip. Clienti: " 
                        Font-Bold="True"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblElencoTipClienti" runat="server" Text="Label"></asp:Label><br /><br />

                    <asp:Label ID="Label39" runat="server" Text="Label" 
                        Font-Bold="True"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblElencoCodice" runat="server" Text="Risultato Codice Promo"></asp:Label><br /><br />
                    &nbsp;Ritiro <br />
                    &nbsp;
                    <asp:DropDownList ID="DropStazioneUscita" runat="server" class="txt_station" AppendDataBoundItems="True" 
                        DataSourceID="SqlStazioni" DataTextField="stazione" DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">- seleziona luogo -</asp:ListItem>
                    </asp:DropDownList>  
                     &nbsp;       
                    <asp:Label ID="Label5" runat="server" Text="Data Uscita: "></asp:Label>
                    &nbsp;                                                             
                    <asp:TextBox ID="data_uscita" runat="server" class="txt_date" ></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label6" runat="server" Text="Ora Uscita: "></asp:Label>                                                                              
                    <asp:DropDownList ID="ora_uscita" runat="server" AppendDataBoundItems="True" class="txt_hour">
                        <asp:ListItem>00:00</asp:ListItem>
                        <asp:ListItem >00:15</asp:ListItem>
                        <asp:ListItem>00:30</asp:ListItem>
                        <asp:ListItem>00:45</asp:ListItem>
                        <asp:ListItem >01:00</asp:ListItem>
                        <asp:ListItem >01:15</asp:ListItem>
                        <asp:ListItem >01:30</asp:ListItem>
                        <asp:ListItem >01:45</asp:ListItem>
                        <asp:ListItem >02:00</asp:ListItem>
                        <asp:ListItem >02:15</asp:ListItem>
                        <asp:ListItem >02:30</asp:ListItem>
                        <asp:ListItem >02:45</asp:ListItem>
                        <asp:ListItem >03:00</asp:ListItem>
                        <asp:ListItem >03:15</asp:ListItem>
                        <asp:ListItem >03:30</asp:ListItem>
                        <asp:ListItem >03:45</asp:ListItem>
                        <asp:ListItem >04:00</asp:ListItem>
                        <asp:ListItem >04:15</asp:ListItem>
                        <asp:ListItem >04:30</asp:ListItem>
                        <asp:ListItem >04:45</asp:ListItem>
                        <asp:ListItem >05:00</asp:ListItem>
                        <asp:ListItem >05:15</asp:ListItem>
                        <asp:ListItem >05:30</asp:ListItem>
                        <asp:ListItem >05:45</asp:ListItem>
                        <asp:ListItem >06:00</asp:ListItem>
                        <asp:ListItem >06:15</asp:ListItem>
                        <asp:ListItem >06:30</asp:ListItem>
                        <asp:ListItem >06:45</asp:ListItem>
                        <asp:ListItem >07:00</asp:ListItem>
                        <asp:ListItem >07:15</asp:ListItem>
                        <asp:ListItem >07:30</asp:ListItem>
                        <asp:ListItem >07:45</asp:ListItem>
                        <asp:ListItem >08:00</asp:ListItem>
                        <asp:ListItem >08:15</asp:ListItem>
                        <asp:ListItem >08:30</asp:ListItem>
                        <asp:ListItem >08:45</asp:ListItem>
                        <asp:ListItem >09:00</asp:ListItem>
                        <asp:ListItem >09:15</asp:ListItem>
                        <asp:ListItem >09:30</asp:ListItem>
                        <asp:ListItem >09:45</asp:ListItem>
                        <asp:ListItem >10:00</asp:ListItem>
                        <asp:ListItem >10:15</asp:ListItem>
                        <asp:ListItem >10:30</asp:ListItem>
                        <asp:ListItem >10:45</asp:ListItem>
                        <asp:ListItem >11:00</asp:ListItem>
                        <asp:ListItem >11:15</asp:ListItem>
                        <asp:ListItem >11:30</asp:ListItem>
                        <asp:ListItem >11:45</asp:ListItem>
                        <asp:ListItem selected="true">12:00</asp:ListItem>
                        <asp:ListItem >12:15</asp:ListItem>
                        <asp:ListItem >12:30</asp:ListItem>
                        <asp:ListItem >12:45</asp:ListItem>
                        <asp:ListItem >13:00</asp:ListItem>
                        <asp:ListItem >13:15</asp:ListItem>
                        <asp:ListItem >13:30</asp:ListItem>
                        <asp:ListItem >13:45</asp:ListItem>
                        <asp:ListItem >14:00</asp:ListItem>
                        <asp:ListItem >14:15</asp:ListItem>
                        <asp:ListItem >14:30</asp:ListItem>
                        <asp:ListItem >14:45</asp:ListItem>
                        <asp:ListItem >15:00</asp:ListItem>
                        <asp:ListItem >15:15</asp:ListItem>
                        <asp:ListItem >15:30</asp:ListItem>
                        <asp:ListItem >15:45</asp:ListItem>
                        <asp:ListItem >16:00</asp:ListItem>
                        <asp:ListItem >16:15</asp:ListItem>
                        <asp:ListItem >16:30</asp:ListItem>
                        <asp:ListItem >16:45</asp:ListItem>
                        <asp:ListItem >17:00</asp:ListItem>
                        <asp:ListItem >17:15</asp:ListItem>
                        <asp:ListItem >17:30</asp:ListItem>
                        <asp:ListItem >17:45</asp:ListItem>
                        <asp:ListItem >18:00</asp:ListItem>
                        <asp:ListItem >18:15</asp:ListItem>
                        <asp:ListItem >18:30</asp:ListItem>
                        <asp:ListItem >18:45</asp:ListItem>
                        <asp:ListItem >19:00</asp:ListItem>
                        <asp:ListItem >19:15</asp:ListItem>
                        <asp:ListItem >19:30</asp:ListItem>
                        <asp:ListItem >19:45</asp:ListItem>
                        <asp:ListItem >20:00</asp:ListItem>
                        <asp:ListItem >20:15</asp:ListItem>
                        <asp:ListItem >20:30</asp:ListItem>
                        <asp:ListItem >20:45</asp:ListItem>
                        <asp:ListItem >21:00</asp:ListItem>
                        <asp:ListItem >21:15</asp:ListItem>
                        <asp:ListItem >21:30</asp:ListItem>
                        <asp:ListItem >21:45</asp:ListItem>
                        <asp:ListItem >22:00</asp:ListItem>
                        <asp:ListItem >22:15</asp:ListItem>
                        <asp:ListItem >22:30</asp:ListItem>
                        <asp:ListItem >22:45</asp:ListItem>
                        <asp:ListItem >23:00</asp:ListItem>
                        <asp:ListItem >23:15</asp:ListItem>
                        <asp:ListItem >23:30</asp:ListItem>
                        <asp:ListItem >23:45</asp:ListItem>                                                                                                                                                 
                   </asp:DropDownList>
                   <br /><br />                                                                                                                                                                         
                    &nbsp; Riconsegna <br />
                    &nbsp;
                   <asp:DropDownList ID="DropStazioneRientro" runat="server" class="txt_station2" AppendDataBoundItems="True"
                        DataSourceID="SqlStazioni" DataTextField="stazione" DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">- seleziona luogo -</asp:ListItem>
                    </asp:DropDownList>          
                     &nbsp;       
                    <asp:Label ID="Label7" runat="server" Text="Data Rientro: "></asp:Label> 
                    &nbsp;                  
                    <asp:TextBox ID="data_rientro" runat="server" class="txt_date" ></asp:TextBox>   
                    &nbsp;
                    <asp:Label ID="Label8" runat="server" Text="Ora Uscita: "></asp:Label>
                    <asp:DropDownList ID="ora_rientro" runat="server" AppendDataBoundItems="True" class="txt_hour">
                        <asp:ListItem>00:00</asp:ListItem>
                        <asp:ListItem >00:15</asp:ListItem>
                        <asp:ListItem>00:30</asp:ListItem>
                        <asp:ListItem>00:45</asp:ListItem>
                        <asp:ListItem >01:00</asp:ListItem>
                        <asp:ListItem >01:15</asp:ListItem>
                        <asp:ListItem >01:30</asp:ListItem>
                        <asp:ListItem >01:45</asp:ListItem>
                        <asp:ListItem >02:00</asp:ListItem>
                        <asp:ListItem >02:15</asp:ListItem>
                        <asp:ListItem >02:30</asp:ListItem>
                        <asp:ListItem >02:45</asp:ListItem>
                        <asp:ListItem >03:00</asp:ListItem>
                        <asp:ListItem >03:15</asp:ListItem>
                        <asp:ListItem >03:30</asp:ListItem>
                        <asp:ListItem >03:45</asp:ListItem>
                        <asp:ListItem >04:00</asp:ListItem>
                        <asp:ListItem >04:15</asp:ListItem>
                        <asp:ListItem >04:30</asp:ListItem>
                        <asp:ListItem >04:45</asp:ListItem>
                        <asp:ListItem >05:00</asp:ListItem>
                        <asp:ListItem >05:15</asp:ListItem>
                        <asp:ListItem >05:30</asp:ListItem>
                        <asp:ListItem >05:45</asp:ListItem>
                        <asp:ListItem >06:00</asp:ListItem>
                        <asp:ListItem >06:15</asp:ListItem>
                        <asp:ListItem >06:30</asp:ListItem>
                        <asp:ListItem >06:45</asp:ListItem>
                        <asp:ListItem >07:00</asp:ListItem>
                        <asp:ListItem >07:15</asp:ListItem>
                        <asp:ListItem >07:30</asp:ListItem>
                        <asp:ListItem >07:45</asp:ListItem>
                        <asp:ListItem >08:00</asp:ListItem>
                        <asp:ListItem >08:15</asp:ListItem>
                        <asp:ListItem >08:30</asp:ListItem>
                        <asp:ListItem >08:45</asp:ListItem>
                        <asp:ListItem >09:00</asp:ListItem>
                        <asp:ListItem >09:15</asp:ListItem>
                        <asp:ListItem >09:30</asp:ListItem>
                        <asp:ListItem >09:45</asp:ListItem>
                        <asp:ListItem >10:00</asp:ListItem>
                        <asp:ListItem >10:15</asp:ListItem>
                        <asp:ListItem >10:30</asp:ListItem>
                        <asp:ListItem >10:45</asp:ListItem>
                        <asp:ListItem >11:00</asp:ListItem>
                        <asp:ListItem >11:15</asp:ListItem>
                        <asp:ListItem >11:30</asp:ListItem>
                        <asp:ListItem >11:45</asp:ListItem>
                        <asp:ListItem selected="true">12:00</asp:ListItem>
                        <asp:ListItem >12:15</asp:ListItem>
                        <asp:ListItem >12:30</asp:ListItem>
                        <asp:ListItem >12:45</asp:ListItem>
                        <asp:ListItem >13:00</asp:ListItem>
                        <asp:ListItem >13:15</asp:ListItem>
                        <asp:ListItem >13:30</asp:ListItem>
                        <asp:ListItem >13:45</asp:ListItem>
                        <asp:ListItem >14:00</asp:ListItem>
                        <asp:ListItem >14:15</asp:ListItem>
                        <asp:ListItem >14:30</asp:ListItem>
                        <asp:ListItem >14:45</asp:ListItem>
                        <asp:ListItem >15:00</asp:ListItem>
                        <asp:ListItem >15:15</asp:ListItem>
                        <asp:ListItem >15:30</asp:ListItem>
                        <asp:ListItem >15:45</asp:ListItem>
                        <asp:ListItem >16:00</asp:ListItem>
                        <asp:ListItem >16:15</asp:ListItem>
                        <asp:ListItem >16:30</asp:ListItem>
                        <asp:ListItem >16:45</asp:ListItem>
                        <asp:ListItem >17:00</asp:ListItem>
                        <asp:ListItem >17:15</asp:ListItem>
                        <asp:ListItem >17:30</asp:ListItem>
                        <asp:ListItem >17:45</asp:ListItem>
                        <asp:ListItem >18:00</asp:ListItem>
                        <asp:ListItem >18:15</asp:ListItem>
                        <asp:ListItem >18:30</asp:ListItem>
                        <asp:ListItem >18:45</asp:ListItem>
                        <asp:ListItem >19:00</asp:ListItem>
                        <asp:ListItem >19:15</asp:ListItem>
                        <asp:ListItem >19:30</asp:ListItem>
                        <asp:ListItem >19:45</asp:ListItem>
                        <asp:ListItem >20:00</asp:ListItem>
                        <asp:ListItem >20:15</asp:ListItem>
                        <asp:ListItem >20:30</asp:ListItem>
                        <asp:ListItem >20:45</asp:ListItem>
                        <asp:ListItem >21:00</asp:ListItem>
                        <asp:ListItem >21:15</asp:ListItem>
                        <asp:ListItem >21:30</asp:ListItem>
                        <asp:ListItem >21:45</asp:ListItem>
                        <asp:ListItem >22:00</asp:ListItem>
                        <asp:ListItem >22:15</asp:ListItem>
                        <asp:ListItem >22:30</asp:ListItem>
                        <asp:ListItem >22:45</asp:ListItem>
                        <asp:ListItem >23:00</asp:ListItem>
                        <asp:ListItem >23:15</asp:ListItem>
                        <asp:ListItem >23:30</asp:ListItem>
                        <asp:ListItem >23:45</asp:ListItem>                                                                                                                                                 
                   </asp:DropDownList>                                                                                                                                                                    
                    <br /><br />                                                                                                                                                                 
                     &nbsp;       
                        <asp:Label ID="Label9" runat="server" Text="Età Guidatore: "></asp:Label> 
                        <asp:TextBox ID="eta" runat="server" class="txt_eta" MaxLength="2" onKeyPress="return filterInputInt(event)" ></asp:TextBox>                        			                                           
                        &nbsp;  &nbsp; 
                         <asp:Label ID="Label32" runat="server" Text="Tipologia Cliente: "></asp:Label> 
                        <asp:TextBox ID="txtTipCliente" runat="server" class="txt_eta" MaxLength="2" onKeyPress="return filterInputInt(event)" ></asp:TextBox>                        			                                           
                        &nbsp;  &nbsp;  
                        <asp:Label ID="Label38" runat="server" Text="Codice Promozionale: "></asp:Label> 
                        <asp:TextBox ID="txtCodPromo" runat="server" class="txt_eta" MaxLength="10" ></asp:TextBox> 
                        &nbsp;  &nbsp;  
                        <asp:Label ID="Label40" runat="server" Text="Lingua: (ITA or ENG) "></asp:Label> 
                        <asp:TextBox ID="txtLingua" runat="server" class="txt_eta" MaxLength="10" 
                        Width="30px" ></asp:TextBox>                        			                                           
                        &nbsp;  &nbsp;
                        <asp:Button ID="btnCerca" runat="server" Text="Cerca" />
                        <br /><br />
                        <asp:Label ID="Label10" runat="server" Text="Risultato Elenco Modelli disponibili per la prenotazione: " Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblElenco" runat="server" Text="Label"></asp:Label>        
                        <br />
                        <br />                                               
                        <asp:Label ID="Label12" runat="server" Text="Id Pagamento: "></asp:Label>
                        <asp:TextBox ID="txtIdPagamento" runat="server" class="txt_eta" MaxLength="6" onKeyPress="return filterInputInt(event)" ></asp:TextBox>                        
                        &nbsp;  &nbsp;  
                        <asp:Button ID="btnDettaglio" runat="server" Text="Dettaglio" />
                         &nbsp;  &nbsp;  
                         <!--
                        <asp:CheckBox ID="chkPrepagato" runat="server" 
                        Text="Prepagato (pagamento on-line)" />   
                        -->                                                         
                        <br />
                        <asp:Label ID="Label13" runat="server" Text="Risultato Elenco Accessori: " Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblExtra" runat="server" Text="Label"></asp:Label> 
                        <br /><br />   
                        <asp:Label ID="Label14" runat="server" Text="Risultato Elenco Inclusi: " Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblCondizioni" runat="server" Text="Label"></asp:Label>   
                        <br /><br />   
                        <asp:Label ID="Label11" runat="server" Text="Risultato Elenco Franchigie: " Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblFranchiggie" runat="server" Text="Label"></asp:Label> 
                        <br /><br />   
                        <asp:Label ID="Label42" runat="server" Text="Risultato Nomi Franchigie: " Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="lblNomiFranchiggie" runat="server" Text="Label"></asp:Label>        
            </div>
            <br />
            <div>              
              <asp:Label ID="Label24" runat="server" Text="Id Preventivo: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtIdPreventivoAccessorio" runat="server" class="txt_eta" MaxLength="6" onKeyPress="return filterInputInt(event)" ></asp:TextBox>
              &nbsp;  &nbsp;                       
              <asp:Label ID="Label25" runat="server" Text="Id GruppoScelto: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtIdGruppoScelto" runat="server" class="txt_eta" MaxLength="6" onKeyPress="return filterInputInt(event)" ></asp:TextBox>  
              &nbsp;  &nbsp;     
              <asp:Label ID="Label15" runat="server" Text="Id Elemento selezionato: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtIdElemento" runat="server" class="txt_eta" MaxLength="6" onKeyPress="return filterInputInt(event)" ></asp:TextBox>              
              &nbsp;  &nbsp;                
              <asp:Label ID="Label26" runat="server" Text="Prepagato: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtPrepagato" runat="server" class="txt_eta" ></asp:TextBox>
              &nbsp;  &nbsp;                
              <asp:Label ID="Label27" runat="server" Text="(True oppure False): " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp;                
              <asp:Label ID="Label28" runat="server" Text="GPS:: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtGps" runat="server" class="txt_eta" ></asp:TextBox>
              &nbsp;  &nbsp;                
              <asp:Label ID="Label29" runat="server" Text="(True oppure False): " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp;                       
              <asp:Label ID="Label30" runat="server" Text="Numero Giorni: " Font-Bold="True"></asp:Label>
              &nbsp;  &nbsp; 
              <asp:TextBox ID="txtNumGiorni" runat="server" class="txt_eta" MaxLength="6" onKeyPress="return filterInputInt(event)" ></asp:TextBox>  
              <br /><br />                 
              <asp:Button ID="btnSelezionato" runat="server" Text="Selezionare Elemento" />
              &nbsp;&nbsp;
              <asp:Button ID="btnDeselezionato" runat="server" Text="Rimuovere Elemento" />
              <br />
              <asp:Label ID="lblElementiExtra" runat="server" Text="Label"></asp:Label>
            </div>
            <br />
            <div>
                <asp:Label ID="Label33" runat="server" Text="Prepagato: " Font-Bold="True"></asp:Label>
                &nbsp;  &nbsp; 
                <asp:TextBox ID="txtPrepagataPrenota" runat="server" class="txt_eta" ></asp:TextBox>
                &nbsp;  &nbsp;                
                <asp:Label ID="Label34" runat="server" Text="(True oppure False): " Font-Bold="True"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label35" runat="server" Text="Id Tariffa: " Font-Bold="True"></asp:Label>
                &nbsp;  &nbsp; 
                <asp:TextBox ID="txtIdTariffaPrenota" runat="server" class="txt_eta" ></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label36" runat="server" Text="Id Gruppo: " Font-Bold="True"></asp:Label>
                &nbsp;  &nbsp; 
                <asp:TextBox ID="txtIdGruppoPrenota" runat="server" class="txt_eta" ></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label37" runat="server" Text="Num Giorni: " Font-Bold="True"></asp:Label>
                &nbsp;  &nbsp; 
                <asp:TextBox ID="txtNumGiorniPrenota" runat="server" class="txt_eta" ></asp:TextBox>
                <br /><br />
                <asp:Label ID="Label16" runat="server" Text="Nome"></asp:Label>
                &nbsp;
                <asp:TextBox ID="TxtNome" runat="server"></asp:TextBox>                
                <asp:Label ID="Label17" runat="server" Text="Cognome"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtCognome" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Label ID="Label18" runat="server" Text="Email"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                <asp:Label ID="Label19" runat="server" Text="Data Nascita"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtDataNascita" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Label ID="Label20" runat="server" Text="Indirizzo"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtIndirizzo" runat="server"></asp:TextBox>
                <asp:Label ID="Label21" runat="server" Text="Telefono"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Label ID="Label22" runat="server" Text="Volo Partenza"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtVoloPartenza" runat="server"></asp:TextBox>
                <asp:Label ID="Label23" runat="server" Text="Volo Arrivo"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtVoloArrivo" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btnPrenota" runat="server" Text="Prenota" />
                &nbsp;&nbsp;
                <asp:Button ID="btnPrenotaPrepagato" runat="server" Text="Prenota Prepagato" />
                &nbsp;&nbsp;&nbsp;&nbsp;   
                <asp:Label ID="Label41" runat="server" Text="Totale da Pagare"></asp:Label>                                             
                <asp:TextBox ID="txtTotaleDaPagare" runat="server"></asp:TextBox>         
                &nbsp;&nbsp;
                <asp:Label ID="lblOkPrenota" runat="server" Text="Numero di prenotazione"></asp:Label>
                <br /><br />
            </div>

<asp:SqlDataSource ID="SqlStazioni" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            
                        
                SelectCommand="SELECT nome_stazione + ' - ' + indirizzo AS stazione, id, attiva FROM stazioni WHERE (id &gt; 1) AND (attiva = 'True') ">
        </asp:SqlDataSource>
</asp:Content>
