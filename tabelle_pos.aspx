<%@ Page Language="VB" MasterPageFile="MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_pos.aspx.vb" Inherits="tabelle_pos" title="" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         tr{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
<table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
      <tr>
         <td >
            <%--63--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Enti Proprietari" title="Enti Proprietari" runat="server" id="puntoCensimentoEntiProprietari" />
                <asp:Button ID="btnCensimentoEntiProprietari" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Enti Proprietari" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>                     
            <%--64--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento Circuiti" title="Censimento Circuiti" runat="server" id="puntoCensimentoCircuiti" />
                <asp:Button ID="btnCensimentoCircuiti" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Circuiti" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--69--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Funzionalità" title="Funzionalità" runat="server" id="puntoFunzionalità" />
                <asp:Button ID="btnFunzionalita" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Funzionalità" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--59--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento POS" title="Censimento POS" runat="server" id="puntoCensimentoPOS" />&nbsp;<asp:Button 
                   ID="btnCensimentoPOS" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Stazione --> POS" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White"  />
                </span>   
            <%--65--%>
                <%--<span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento Acquires" title="Censimento Acquires" runat="server" id="puntoCensimentoAcquires" />
                <asp:Button ID="btnCensimentoAcquires" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Acquires" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>--%>
            <%--66--%>
                <%--<span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento Tipologie Errori" title="Censimento Tipologie Errori" runat="server" id="puntoCensimentoTipologieErrori" />
                <asp:Button ID="btnCensimentoTipologieErrori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipologie Errori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>--%>
            <%--67--%>
                <%--<span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento Action Code" title="Censimento Action Code" runat="server" id="puntoCensimentoActionCode" />
                <asp:Button ID="btnCensimentoActionCode" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Action Code" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span> --%>           
            <%--146--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Enti Transazione Telefonica" title="Enti Transazione Telefonica" runat="server" id="puntoEntiTransazioneTelefono" />
                <asp:Button ID="btnEntiTransazioneTelefono" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Enti Transazione Telefonica" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--146--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Anagrafica Codici Esercenti Telefonici" title="Anagrafica Codici Esercenti Telefonici" runat="server" id="punto_anagrafica_esercenti_telefonici" />
                <asp:Button ID="btn_anagrafica_esercenti_telefonici" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Anagrafica Codici Esercenti Telefonici" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
                
         </td>
      </tr>
  </table>
    
    
</asp:Content>

