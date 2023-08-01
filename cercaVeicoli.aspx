<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="cercaVeicoli.aspx.vb" Inherits="cercaVeicoli" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
     <link rel="StyleSheet" type="text/css" href="css/style.css" />
    <style type="text/css">
        .style1
        {        	  
            width: 146px;
        }
        .style2
        {
        }
        .style4
        {
            color: #000000;
            font-weight: bold;
            width: 100px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style37
        {
            font-family: Courier New;
            font-size: small;
            color: #000000;
            width: 100px;
        }
        .style46
        {
            width: 147px;
        }
        .style50
        {
            width: 141px;
        }
        .style53
        {
        }
        .style54
        {
            width: 126px;
        }
        .style55
        {
            width: 136px;
        }
        .style56
        {
            width: 73px;
        }
        .style60
        {
            width: 125px;
        }
        .style61
        {
            width: 96px;
        }
        .style62
        {
            width: 92px;
        }
        .style63
        {
            width: 60px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <%-- <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>--%>

    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label2" runat="server" Text="Parco Veicoli" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
     </table>

 <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-bottom:0px;" border="0">
        <tr>
          <td align="left" class="style37">
              <asp:Label ID="LblTarga" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
           </td>
          <td align="left" class="style46">
              <asp:Label ID="LblTelaio" runat="server" Text="Telaio" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style50">
              <asp:Label ID="LblMarca" runat="server" Text="Marca" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style55">
              <asp:Label ID="LblModello" runat="server" Text="Modello" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style54">
               <asp:Label ID="lblGruppo" runat="server" CssClass="testo_bold" Text="Gruppo"></asp:Label>
          </td>
          <td align="left" class="style53">
               <asp:Label ID="lblStato" runat="server" CssClass="testo_bold" Text="Stato veicolo"></asp:Label>
          </td>
          <td align="left" class="style56">
               &nbsp;</td>
          <td align="left" class="style2">
               &nbsp;
         </td>
        </tr>
        <tr>
          <td align="left" class="style4">
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="85px"></asp:TextBox>
          </td>
          <td align="left" class="style46">
            
               <asp:TextBox ID="txtCercaTelaio" runat="server" Width="136px"></asp:TextBox>
            
          </td>
          <td align="left" class="style50">
            
               <asp:DropDownList ID="dropCercaMarca" runat="server"
                   DataSourceID="SqlCercaMarca" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style55">            
               <asp:DropDownList ID="dropCercaModello" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlCercaModello" DataTextField="descrizione" style="margin-left: 0px"
                   DataValueField="id_modello">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
            </td>
          <td align="left" class="style54">
          <asp:DropDownList ID="dropCercaGruppo" runat="server" AppendDataBoundItems="True" 
              DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
              DataValueField="id_gruppo">
              <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
          </asp:DropDownList>
            </td>
          <td align="left" class="style53">
            
               <asp:DropDownList ID="dropStatoVendita" runat="server" AppendDataBoundItems="True" 
                   style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Qualsiasi</asp:ListItem>
                    <asp:ListItem Selected="False" Value="1">In parco</asp:ListItem>
                   <asp:ListItem Selected="False" Value="2">Disponibile Nolo</asp:ListItem>
                   <asp:ListItem Selected="False" Value="7">In Noleggio</asp:ListItem>
                   <asp:ListItem Selected="False" Value="3">In vendita</asp:ListItem>
                   <asp:ListItem Selected="False" Value="4">Venduto</asp:ListItem>
                   <asp:ListItem Selected="False" Value="6">Buy Back</asp:ListItem>
                   <asp:ListItem Selected="False" Value="5">Rubato</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style56">
            
               &nbsp;</td>
          <td align="left" class="style2">
              &nbsp;</td>
        </tr>
        <tr>
          <td align="left" class="style4">
              <asp:Label ID="LblBolla" runat="server" Text="Bolla" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style46">
            
              <asp:Label ID="LblAttoDiVendita" runat="server" Text="Atto di Vendita" CssClass="testo_bold"></asp:Label>
            </td>
          <td align="left" class="style50">
            
              <asp:Label ID="LblProprietario" runat="server" Text="Proprietario" CssClass="testo_bold"></asp:Label>
            </td>
          <td align="left" class="style55">            
               <asp:Label ID="lblAmmortamento" runat="server" CssClass="testo_bold" Text="Ammortamento"></asp:Label>
            </td>
          <td align="left" class="style54">
               <asp:Label ID="lblLeasing" runat="server" CssClass="testo_bold" Text="Leasing"></asp:Label>
            </td>
          <td align="left" class="style53">
            
               <asp:Label ID="lblLeasing0" runat="server" CssClass="testo_bold" Text="Stazione"></asp:Label>
            </td>
          <td align="left" class="style56">
            
               &nbsp;</td>
          <td align="left" class="style2">
              &nbsp;</td>
        </tr>
        <tr>
          <td align="left" class="style4">
            
               <asp:DropDownList ID="dropCercaBolle" runat="server" 
                   AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Con bolla</asp:ListItem>
                   <asp:ListItem Selected="False" Value="2">Senza bolla</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style46">
            
               <asp:DropDownList ID="dropCercaAttoVendita" runat="server" 
                   AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Con atto</asp:ListItem>
                   <asp:ListItem Selected="False" Value="2">Senza atto</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style50">
            
               <asp:DropDownList ID="dropCercaProprietario" runat="server"
                   DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style55">            
            
               <asp:DropDownList ID="dropAmmortamento" runat="server" AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="-1">Tutte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="0">Incluse</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Escluse</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left" class="style54">
            
               <asp:DropDownList ID="dropLeasing" runat="server" 
                   AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="-1">Tutte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="0">No</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Si</asp:ListItem>
               </asp:DropDownList>
            </td>
          <td align="left" class="style53" colspan="3">
            
            <asp:DropDownList ID="dropCercaStazione" runat="server" 
                AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                DataValueField="id">
               <asp:ListItem Selected="True" Value="-1">Seleziona...</asp:ListItem>
               <asp:ListItem Value="0">Non assegnate...</asp:ListItem>
               <asp:ListItem Value="1">Assegnate...</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        </table>
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0">
        <tr>
          <td align="left" class="style63" >
              </td>
          <td align="left" class="style62">
              <asp:Label ID="LblBolloVendita" runat="server" Text="Data Bolla" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style62" >
              <asp:Label ID="LblAttoVendita" runat="server" Text="Data Atto" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style62">
              <asp:Label ID="LblFatturaVendita" runat="server" Text="Fatt. vendita:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style60" >
               <asp:Label ID="LblImmatricolazione" runat="server" Text="Immatricolazione:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style61" >
              <asp:Label ID="LblAcquisto" runat="server" Text="Data Acquisto:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style62" >
              <asp:Label ID="LblAcquisto0" runat="server" Text="Fatt. acquisto:" 
                  CssClass="testo_bold"></asp:Label>
            </td>
          <td align="left" class="style62">
              <asp:Label ID="LblImmFlotta" runat="server" Text="Imm. flotta:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
              <asp:Label ID="LblImmFlotta0" runat="server" Text="Scadenza Leasing:" CssClass="testo_bold"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" class="style63">
              <asp:Label ID="LblDaData" runat="server" Text="Da data:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style62" >
              <a onclick="Calendar.show(document.getElementById('<%=txtBollaDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtBollaDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
               <%--<asp:CalendarExtender ID="txtBollaDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtBollaDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtBollaDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtBollaDa">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style62" >   
              <a onclick="Calendar.show(document.getElementById('<%=txtAttoDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtAttoDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
               <%--<asp:CalendarExtender ID="txtAttoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAttoDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtAttoDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAttoDa">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style62" >
              <a onclick="Calendar.show(document.getElementById('<%=txtFattDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtFattDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
              <%-- <asp:CalendarExtender ID="txtFattDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtFattDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtFattDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtFattDa">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style60">
              <a onclick="Calendar.show(document.getElementById('<%=txtImmDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtImmDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
              <%-- <asp:CalendarExtender ID="txtDataBolla0_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtImmDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataBolla0_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtImmDa">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style61">
               <a onclick="Calendar.show(document.getElementById('<%=txtAcquistoDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtAcquistoDa" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%-- <asp:CalendarExtender ID="txtAcquistoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAcquistoDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtAcquistoDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAcquistoDa">
                </asp:MaskedEditExtender>    
                         
           
           
              </td>
          <td align="left" class="style62" >
              <a onclick="Calendar.show(document.getElementById('<%=txtFatturaAcquistoDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtFatturaAcquistoDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
               <%--<asp:CalendarExtender ID="txtFatturaAcquistoDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtFatturaAcquistoDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtFatturaAcquistoDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtFatturaAcquistoDa">
                </asp:MaskedEditExtender>    
              </td>
          <td align="left" class="style62" >
              <a onclick="Calendar.show(document.getElementById('<%=txtImmFlottaDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtImmFlottaDa" runat="server" Width="70px"></asp:TextBox>
                  </a>
       <%--        <asp:CalendarExtender ID="txtImmFlottaDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtImmFlottaDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtImmFlottaDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtImmFlottaDa">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left">
           <a onclick="Calendar.show(document.getElementById('<%=txtScadenzaLeasingDa.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtScadenzaLeasingDa" runat="server" Width="70px"></asp:TextBox>
               </a>
             <%--  <asp:CalendarExtender ID="txtScadenzaLeasingDa_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtScadenzaLeasingDa">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtScadenzaLeasingDa_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtScadenzaLeasingDa">
                </asp:MaskedEditExtender>    
          
          </td>
        </tr>
        <tr>
          <td align="left" class="style63">
              <asp:Label ID="LblAData" runat="server" Text="A data:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left" class="style62">
               <a onclick="Calendar.show(document.getElementById('<%=txtBollaA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtBollaA" runat="server" Width="70px"></asp:TextBox>
                   </a>
               <%--<asp:CalendarExtender ID="txtBollaA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtBollaA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtBollaA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtBollaA">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left" class="style62">
               <a onclick="Calendar.show(document.getElementById('<%=txtAttoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtAttoA" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%-- <asp:CalendarExtender ID="txtAttoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAttoA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtAttoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAttoA">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left" class="style62">
               <a onclick="Calendar.show(document.getElementById('<%=txtAttoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtFattA" runat="server" Width="70px"></asp:TextBox>
                   </a>
           <%--    <asp:CalendarExtender ID="txtFattA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtFattA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtFattA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtFattA">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left" class="style60">
                <a onclick="Calendar.show(document.getElementById('<%=txtImmA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtImmA" runat="server" Width="70px"></asp:TextBox>
                    </a>
             <%--  <asp:CalendarExtender ID="txtDataVendita0_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtImmA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtDataVendita0_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtImmA">
                </asp:MaskedEditExtender>    
                         
           
           
          </td>
          <td align="left" class="style61">
                <a onclick="Calendar.show(document.getElementById('<%=txtAcquistoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtAcquistoA" runat="server" Width="70px"></asp:TextBox>
                    </a>
            <%--   <asp:CalendarExtender ID="txtAcquistoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAcquistoA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtAcquistoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAcquistoA">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left" class="style62">
              <a onclick="Calendar.show(document.getElementById('<%=txtFatturaAcquistoA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtFatturaAcquistoA" runat="server" Width="70px"></asp:TextBox>
                  </a>
             <%--  <asp:CalendarExtender ID="txtFatturaAcquistoA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtFatturaAcquistoA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtFatturaAcquistoA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtFatturaAcquistoA">
                </asp:MaskedEditExtender>    
            </td>
          <td align="left" class="style62">
              <a onclick="Calendar.show(document.getElementById('<%=txtImmFlottaA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtImmFlottaA" runat="server" Width="70px"></asp:TextBox>
                  </a>
            <%--   <asp:CalendarExtender ID="txtImmFlottaA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtImmFlottaA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtImmFlottaA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtImmFlottaA">
                </asp:MaskedEditExtender>    
          </td>
          <td align="left">
               <a onclick="Calendar.show(document.getElementById('<%=txtScadenzaLeasingA.ClientID%>'), '%d/%m/%Y', false)">
               <asp:TextBox ID="txtScadenzaLeasingA" runat="server" Width="70px"></asp:TextBox>
                   </a>
            <%--   <asp:CalendarExtender ID="txtScadenzaLeasingA_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtScadenzaLeasingA">
                </asp:CalendarExtender>--%>
                <asp:MaskedEditExtender ID="txtScadenzaLeasingA_MaskedEditExtender" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtScadenzaLeasingA">
                </asp:MaskedEditExtender>    
          </td>
        </tr>   
        </table>
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;border-top:0px;" border="0"> 
        <tr>
          <td align="left" >
              <asp:Label ID="LblAData0" runat="server" Text="Acquirente" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
           
               <asp:DropDownList ID="dropAcquirenti" runat="server"
                   DataSourceID="sqlAcquirenti" DataTextField="rag_soc" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" 
                   AutoPostBack="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
            </td>
          <td align="left">
              <asp:Label ID="LblAData1" runat="server" Text="Venditore:" CssClass="testo_bold"></asp:Label>
          </td>
          <td align="left">
               <asp:DropDownList ID="dropVenditori" runat="server"
                   DataSourceID="sqlVenditori" DataTextField="nome" DataValueField="id" 
                   style="margin-left: 0px; height: 22px;" AppendDataBoundItems="True" >
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            </td>
        </tr>    
        <tr>
          <td align="left">              
               <asp:Label ID="LblReport" runat="server" Text="Report:" CssClass="testo_bold"></asp:Label>               
          </td>
          <td align="left" >
            
               <asp:DropDownList ID="dropReport" runat="server" AppendDataBoundItems="True" style="margin-left: 0px">
                   <asp:ListItem Selected="True" Value="0">Lista targhe ridotte</asp:ListItem>
                   <asp:ListItem Selected="False" Value="1">Lista targhe ridotte con dettaglio</asp:ListItem>
                   <asp:ListItem Selected="False" Value="2">Lista targhe con dettaglio fatture</asp:ListItem>
                   <asp:ListItem Selected="False" >---</asp:ListItem>
                   <asp:ListItem Selected="False" Value="3">Lista targhe per venditore</asp:ListItem>
                   <asp:ListItem Selected="False" Value="4">Lista targhe_X_Cespiti</asp:ListItem>
                   <asp:ListItem Selected="False" >---</asp:ListItem>
                   <asp:ListItem Selected="False" Value="5">Targhe auto in vendita o vendute</asp:ListItem>
                   <asp:ListItem Selected="False" Value="6">Targhe auto in vendita o vendute x Venditore</asp:ListItem>
               </asp:DropDownList>
            
               <asp:Button ID="btnReport" runat="server" Text="Stampa Report" />
               &nbsp;
               <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" />
              <asp:Label ID="LblCaricaElenco" runat="server" Text="Label" Visible="False"></asp:Label>
          </td>
          <td align="right">
            
               &nbsp;</td>
            <td align="right">
            
                <asp:Button ID="btnNuovo" runat="server" Text="Nuovo veicolo" Width="114px" />

               <asp:Label ID="lblNumRisultati" runat="server"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="risultati"></asp:Label>
            </td>
        </tr>
        </table>
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
        <tr>
          <td align="right">
                <asp:Button ID="btnEsportaFatture" runat="server" Text="Esporta fatture" 
                    Visible="False" />

                <asp:Button ID="btnImportVeicoli" runat="server" Text="Import veicoli da File" />  
                  
                <asp:Button ID="btnAggiornaVeicoli" runat="server" 
                    Text="Aggiorna veicoli da File" />  
                  
                <asp:Button ID="btnImportDismissioni" runat="server" Text="Import dismissioni da File" />  
                  
                <asp:Button ID="btnImportAssicurazioniVeicoli" runat="server" 
                    Text="Import assicurazioni veicoli da File" Visible="False" />   
          </td>
        </tr>        
        </table>
    <br />
    
        <% 
            If LblCaricaElenco.Text = "CaricaFile" Then
        %>
        <table border="0" cellpadding="0" cellspacing="0" width="1024px">             
        <tr>
          <td colspan="2">
              <asp:ListView ID="listVeicoli" runat="server" DataKeyNames="id" DataSourceID="sqlVeicoli">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td>
                              <asp:Label ID="gruppoLabel" runat="server" Text='<%# Eval("gruppo") %>' />
                          </td>
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                          </td>
                          <td>
                              <asp:Label ID="telaioLabel" runat="server" Text='<%# Eval("telaio") %>' />
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <%--<td>
                              <asp:Label ID="alimentazioneLabel" runat="server" 
                                  Text='<%# Eval("alimentazione") %>' />
                          </td>--%>
                          <td>
                            <asp:Label ID="Label3" runat="server" Text='<%#IIf(IsDBNull(Eval("km_attuali")), "0", Eval("km_attuali")) %>' />
                          </td>
                          <td>
                             <asp:Label ID="Label4" runat="server" Text='<%# IIf(IsDBNull(Eval("serbatoio_attuale")), "0", Eval("serbatoio_attuale")) %>' />
                             <asp:Label ID="Label5" runat="server" Text='/' />
                             <asp:Label ID="Label6" runat="server" Text='<%# Eval("capacita_serbatoio") %>' />
                          </td>
                          <td>
                              <asp:Label ID="proprietarioLabel" runat="server" 
                                  Text='<%# Eval("proprietario") %>' />
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="btnCancella" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il veicolo?'));" ToolTip="Elimina"  />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr>
                          <td>
                              <asp:Label ID="gruppoLabel" runat="server" Text='<%# Eval("gruppo") %>' />
                          </td>
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                          </td>
                          <td>
                              <asp:Label ID="telaioLabel" runat="server" Text='<%# Eval("telaio") %>' />
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <%--<td>
                              <asp:Label ID="alimentazioneLabel" runat="server" 
                                  Text='<%# Eval("alimentazione") %>' />
                          </td>--%>
                         <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# IIf(IsDBNull(Eval("km_attuali")), "0", Eval("km_attuali")) %>' />
                          </td>
                          <td>
                             <asp:Label ID="Label4" runat="server" Text='<%# IIf(IsDBNull(Eval("serbatoio_attuale")), "0", Eval("serbatoio_attuale")) %>' />
                             <asp:Label ID="Label5" runat="server" Text='/' />
                             <asp:Label ID="Label6" runat="server" Text='<%# Eval("capacita_serbatoio") %>' />
                          </td>
                          <td>
                              <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario") %>' />
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="btnCancella" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare il veicolo?'));" ToolTip="Elimina" />
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%"  
                                      style=" background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;
                                            border-style:none;border-width:1px;" class="testo_bold">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th4" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo">Gruppo</asp:LinkButton>
                                          </th>
                                          <th id="Th1" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_targa" CssClass="testo_titolo">Targa</asp:LinkButton>
                                          </th>
                                          <th id="Th2" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_Telaio" CssClass="testo_titolo">Telaio</asp:LinkButton>
                                          </th>
                                          <th id="Th3" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_modello" CssClass="testo_titolo">Modello</asp:LinkButton>
                                          </th>
                                          <%--<th id="Th4" runat="server" align="left">
                                              Alimentazione</th>--%>
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_km" CssClass="testo_titolo">KM</asp:LinkButton>
                                          </th>
                                          <th id="Th7" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_litri" CssClass="testo_titolo">Serb.</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server" align="left">
                                              <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_propr" CssClass="testo_titolo">Proprietario</asp:LinkButton> 
                                          </th>
                                          <th></th>
                                          <th></th>     
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
                                      <Fields>
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                          <asp:TemplatePagerField>              
                                          <PagerTemplate>
                                            Totale Record: <asp:Label  runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" /> 
                                            - Pagina corrente: <asp:Label  runat="server" ID="NumPaginalabel" Text="<%# ((Container.StartRowIndex \ Container.PageSize) + 1)%>" />
                                            di: <asp:Label  runat="server" ID="TotalePagineLabel" Text="<%# System.Math.Truncate(Container.TotalRowCount / Container.PageSize + 0.9999)%>" />         
                                        </PagerTemplate>
                                       </asp:TemplatePagerField>
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
        <% End If%>
    </div>
    <asp:SqlDataSource ID="sqlVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
        SelectCommand="SELECT veicoli.id, veicoli.targa, veicoli.telaio, marche.descrizione As marca, modelli.descrizione As modello, colori.descrizione As colore, proprietari_veicoli.descrizione As proprietario, gruppi.cod_gruppo As gruppo, veicoli.km_attuali, veicoli.serbatoio_attuale, modelli.capacita_serbatoio FROM veicoli WITH(NOLOCK) INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello INNER JOIN marche WITH(NOLOCK) ON modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo WHERE modelli.id_CasaAutomobilistica>0"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaMarca" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM marche WITH(NOLOCK) ORDER BY [descrizione]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaModello" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_modello, descrizione FROM modelli WITH(NOLOCK) WHERE (id_CasaAutomobilistica = @id_marca) ORDER BY descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropCercaMarca" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlAcquirenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, rag_soc FROM acquirenti_veicoli WITH(NOLOCK) ORDER BY rag_soc"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlVenditori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, nome FROM venditori WITH(NOLOCK) ORDER BY nome"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) ORDER BY cod_gruppo">
  </asp:SqlDataSource>
      
  <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) where attiva=1 ORDER BY nome_stazione">
  </asp:SqlDataSource>
      
<asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>

    <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox>

</asp:Content>

