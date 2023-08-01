<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ribaltamento_tony.aspx.vb" Inherits="ribaltamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        Dim StringaSQL As String
        StringaSQL = "SELECT  TOP (100) PERCENT dbo.PRENOTAZIONI.IDPREN, dbo.PRENOTAZIONI.NUMPREN, dbo.PRENOTAZIONI.DATAPREN, dbo.PRENOTAZIONI.GRUPPO," & _
                            "dbo.PRENOTAZIONI.IDUFFNOL, dbo.PRENOTAZIONI.DATANOL, dbo.PRENOTAZIONI.VOLONOL, dbo.PRENOTAZIONI.IDUFFRES, dbo.PRENOTAZIONI.DATARES," & _
                            "dbo.PRENOTAZIONI.VOLORES, dbo.PRENOTAZIONI.IDCLIENTE, dbo.PRENOTAZIONI.IDTARIFFA, dbo.PRENOTAZIONI.NOMETAR, dbo.PRENOTAZIONI.TIPO_TAR," & _
                            "dbo.PRENOTAZIONI.NOTE, dbo.PRENOTAZIONI.IDAUTO, dbo.PRENOTAZIONI.GNOL, dbo.PRENOTAZIONI.FREESALE, dbo.PRENOTAZIONI.GRP_SPEC," & _
                            "dbo.PRENOTAZIONI.AZ, dbo.PRENOTAZIONI.ATTESA_TRANS, dbo.PRENOTAZIONI.TRANS_PAG, dbo.PRENOTAZIONI.TAG_DEP, dbo.PRENOTAZIONI.IMP_DEP," & _
                            "dbo.PRENOTAZIONI.TRANSATA, dbo.PRENOTAZIONI.TOTALE, dbo.PRENOTAZIONI.SCARICATADA, dbo.PRENOTAZIONI.DATA_SCARICO, dbo.PRENOTAZIONI.CONV," & _
                            "dbo.PRENOTAZIONI.L675, dbo.PRENOTAZIONI.ANNULLATA, dbo.PRENOTAZIONI.ANNULLATADA, dbo.PRENOTAZIONI.ANNULLATAIL, dbo.PRENOTAZIONI.SCARICATA," & _
                            "dbo.PRENOTAZIONI.DATASCARICO, dbo.PRENOTAZIONI.FILESCARICO, dbo.PRENOTAZIONI.INTER, dbo.PRENOTAZIONI.PAESE_INT," & _
                            "dbo.PRENOTAZIONI.CONFERMATA, dbo.PRENOTAZIONI.CONFERMATADA, dbo.PRENOTAZIONI.CONFERMATAIL, dbo.PRENOTAZIONI.SESSIONE_PREN," & _
                            "dbo.PRENOTAZIONI.DETTAGLIO_IMPORTI_HTML, dbo.PRENOTAZIONI.RIFIUTATA, dbo.PRENOTAZIONI.RIFIUTATADA, dbo.PRENOTAZIONI.RIFIUTATAIL," & _
                            "dbo.PRENOTAZIONI.PNOLO, DEV_DB_GESTIONE.dbo.UTENTI.cognome, DEV_DB_GESTIONE.dbo.UTENTI.nome, DEV_DB_GESTIONE.dbo.UTENTI.indirizzo," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.citta, DEV_DB_GESTIONE.dbo.UTENTI.cap, DEV_DB_GESTIONE.dbo.UTENTI.prov, DEV_DB_GESTIONE.dbo.UTENTI.nazione," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.telefono, DEV_DB_GESTIONE.dbo.UTENTI.fax, DEV_DB_GESTIONE.dbo.UTENTI.cell, DEV_DB_GESTIONE.dbo.UTENTI.email," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.codfisc, DEV_DB_GESTIONE.dbo.UTENTI.luogo_nascita, DEV_DB_GESTIONE.dbo.UTENTI.data_nascita," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.n_patente, DEV_DB_GESTIONE.dbo.UTENTI.ril_patente, DEV_DB_GESTIONE.dbo.UTENTI.data_patente," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.scad_patente, DEV_DB_GESTIONE.dbo.UTENTI.password, DEV_DB_GESTIONE.dbo.UTENTI.conoscenza," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.nome_azienda, DEV_DB_GESTIONE.dbo.UTENTI.indirizzo_az, DEV_DB_GESTIONE.dbo.UTENTI.citta_az," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.cap_az, DEV_DB_GESTIONE.dbo.UTENTI.prov_az, DEV_DB_GESTIONE.dbo.UTENTI.tel_az," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.fax_az, DEV_DB_GESTIONE.dbo.UTENTI.cell_az, DEV_DB_GESTIONE.dbo.UTENTI.email_az," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.piva, DEV_DB_GESTIONE.dbo.UTENTI.informativa, DEV_DB_GESTIONE.dbo.UTENTI.consenso," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.condizioni, DEV_DB_GESTIONE.dbo.UTENTI.fatt_az, DEV_DB_GESTIONE.dbo.UTENTI.cod_interno_p1000," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.GUID, DEV_DB_GESTIONE.dbo.UTENTI.SHACODE, DEV_DB_GESTIONE.dbo.UTENTI.attivato," & _
                            "DEV_DB_GESTIONE.dbo.UTENTI.attivato_il, DEV_DB_GESTIONE.dbo.UTENTI.COD_CONV, dbo.TRANS_BANCA.CCNUMAUT, dbo.TRANS_BANCA.CCDATA," & _
                            "dbo.TRANS_BANCA.CCIMPORTO, dbo.TRANS_BANCA.CCNUMOPE, dbo.TRANS_BANCA.CCRISP, dbo.TRANS_BANCA.CCTRANS, dbo.TRANS_BANCA.CCOMPAGNIA," & _
                            "dbo.TRANS_BANCA.TRANSOK, dbo.TRANS_BANCA.TERMINAL_ID, dbo.PRENOTAZIONI.ACCESSORI, DEV_DB_GESTIONE.dbo.CONVENZIONI.PNOLO_CONV " & _
                    "FROM dbo.PRENOTAZIONI LEFT OUTER JOIN " & _
                            "DEV_DB_GESTIONE.dbo.CONVENZIONI ON " & _
                            "dbo.PRENOTAZIONI.CONV = DEV_DB_GESTIONE.dbo.CONVENZIONI.NEW_COD_CONV COLLATE Latin1_General_BIN LEFT OUTER JOIN " & _
                            "dbo.TRANS_BANCA ON dbo.PRENOTAZIONI.NUMPREN = dbo.TRANS_BANCA.NUMPREN AND " & _
                            "dbo.PRENOTAZIONI.IDPREN = dbo.TRANS_BANCA.ID_PREN LEFT OUTER JOIN " & _
                            "DEV_DB_GESTIONE.dbo.UTENTI ON dbo.PRENOTAZIONI.IDCLIENTE = DEV_DB_GESTIONE.dbo.UTENTI.id_utente " & _
                    "WHERE (dbo.PRENOTAZIONI.FREESALE = 1) AND (dbo.PRENOTAZIONI.ATTESA_TRANS = 0) AND (PRENOTAZIONI.SCARICATADA IS NULL)" & _
                    "ORDER BY dbo.PRENOTAZIONI.IDPREN"
                    
        Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(StringaSQL, Dbc)
        'Response.Write(Cmd.CommandText & "<br><br>")
        'Response.End()
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()        
        Do While Rs.Read
            Response.Write(Rs("cognome") & " " & Rs("nome") & "<br>")
        Loop

        Rs.Close()
        Dbc.Close()
        Rs = Nothing
        Dbc = Nothing
     %>
    </div>
    </form>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RibaltamentoConnectionString %>" >
    </asp:SqlDataSource>
</body>
</html>
