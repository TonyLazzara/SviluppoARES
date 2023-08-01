<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SelezionaLetteraRDS.ascx.vb" Inherits="gestione_danni_SelezionaLetteraRDS" %>

<script type="text/javascript">
    function chiudi_form() {
//        try {
//            window.opener.top.myLytebox.end();
//        } catch (e) { }
//        try {
//            self.close();
//        } catch (e) { }
//        try {
//            window.opener.top.LyteBox.end();
//        } catch (e) { }
//        try {
//            window.opener.lb.end();
//        } catch (e) { }
        try {
            parent.document.getElementById("lbMain").style.display = "none";
            parent.document.getElementById("lbOverlay").style.display = "none";
        } catch (e) { }
    }
</script>

<div id="div_seleziona_stampa" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;RDS - Seleziona Lettera per cliente</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Lingua:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:RadioButtonList ID="rb_linguaggio" runat="server" 
                    RepeatDirection="Horizontal" CssClass="testo_bold">
                    <asp:ListItem Value="1" Selected="True">Italiano</asp:ListItem>
                    <asp:ListItem Value="2">Inglese</asp:ListItem>
                </asp:RadioButtonList>  
            </td>
        </tr>

         <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Addebito danni:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_franchigia_parziale" runat="server" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Addebito franchigia:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_no_kasko" runat="server" />
            </td>
        </tr>
        <tr runat="server">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Addebito danni esclusi:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_no_dichiarazione" runat="server" />
            </td>
        </tr>        
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="Label3" runat="server" Text="Errato Rifornimento:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_errato_rifornimento" runat="server" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="Label4" runat="server" Text="Ruote:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_ruote" runat="server" />
            </td>
        </tr>
        <tr runat="server">
            <td>
                <asp:Label ID="Label5" runat="server" Text="Dolo colpa grave:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_imperizia" runat="server" />
            </td>
        </tr>
        <tr id="Tr1" runat="server">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Furto:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_furto" runat="server" />
            </td>
        </tr>
        <tr id="Tr2" runat="server" visible="false">
            <td>
                <asp:Label ID="Label6" runat="server" Text="Atti vandalici:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_atti_vandalici" runat="server" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="Label7" runat="server" Text="Guida Non Autorizzata:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_guida_non_autorizzata" runat="server" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="Label8" runat="server" Text="Sinistro Attivo:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_sinistro_attivo" runat="server" />
            </td>
        </tr>
       
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="Label10" runat="server" Text="Dichiarazione Parziale:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_dichiarazione_parziale" runat="server" />
            </td>
        </tr>
        <tr runat="server">
            <td>
                <asp:Label ID="Label13" runat="server" Text="Campania:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ck_paesi_vietati" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="bt_stampa" runat="server" Text="Stampa"/>
                <%--<asp:Button ID="btnInviaMail" runat="server" Text="Invia Mail"/>--%>
                <asp:Button ID="bt_chiudi" runat="server" Text="Chiudi" OnClientClick='javascript:chiudi_form(); return false;' />
            </td>
        </tr>
    </table>
</div>

<asp:Label ID="lb_id_evento" runat="server" Text='0' visible="false"></asp:Label>
<asp:Label ID="id_evento_apertura" runat="server" Text='0' visible="false"></asp:Label>