<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaODL.aspx.vb" Inherits="gestione_danni_RiepilogoRDS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="NotePerStampa.ascx" TagName="NotePerStampa" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td>
                &nbsp;</td>
            <td style="width:50%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td align="center">
                            <b>Ordinativo Di Lavoro</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <b>N°: </b>
                            <asp:Label ID="lb_numero_odl" runat="server" Text="" ></asp:Label>
                            &nbsp;&nbsp;<b>Data: </b>
                            <asp:Label ID="lb_data_odl" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <b>Stazione Out </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <b>Compilatore ............................................</b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <hr />

    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td valign="top" style="width:30%;">
                <table border="1" cellpadding="0" cellspacing="0" width="95%" >
                <tr>
                    <td> 
                        Spett./le:<br />
                        <b><asp:Label ID="lb_fonitore" runat="server" Text="" ></asp:Label></b><br />
                        <asp:Label ID="lb_fonitore_indirizzo" runat="server" Text="" ></asp:Label><br />
                        <asp:Label ID="lb_fonitore_citta" runat="server" Text="" ></asp:Label><br />    
                        <asp:Label ID="lb_fonitore_telefono" runat="server" Text="" ></asp:Label>    
                    </td>
                </tr>
                </table>
            </td>
            <td valign="top" style="width:20%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                <tr>
                    <td> 
                        <b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label><br />
                        <b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label><br />
                        <b>Telaio: </b><asp:Label ID="lb_telaio" runat="server" Text="" ></asp:Label><br />    
                        <b>Proprietario: </b><asp:Label ID="lb_proprietario" runat="server" Text="" ></asp:Label><br />    
                    </td>
                </tr>
                </table>
            </td>
            <td valign="top" style="width:50%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="width:6%;">
                            
                        </td>
                        <td align="center" style="width:22%;">
                            Uscita
                        </td>
                        <td align="center" style="width:22%;">
                            Rientro
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Stazione
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_stazione_out" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_stazione_in" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Data
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_data_out" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_data_in" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Ora
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_ora_out" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_ora_in" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Km
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_km_out" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_km_in" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Litri
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_litri_out" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_litri_in" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:6%;">
                            Cons./Ritiro
                        </td>
                        <td  style="width:22%;">
                            <asp:Label ID="lb_consegnato_da" runat="server" Text="" ></asp:Label>
                        </td>
                        <td style="width:22%;">
                            <asp:Label ID="lb_ritirato_da" runat="server" Text="" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <hr />
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td>
            &nbsp;<b>RDS N.: </b>
            <asp:Label ID="lb_num_rds" runat="server" Text="" ></asp:Label>
            &nbsp;<b>Documento di Riferimento: </b>
            <asp:Label ID="lb_tipo_documento" runat="server" Text="" ></asp:Label>
            &nbsp;
            <asp:Label ID="lb_num_documento" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
    </table>

    <hr />
    
    <div >
        <table border="0" cellpadding="1" cellspacing="0" width="100%" style="border:1px solid #000000">
            <tr>
                <td >
                     <b>Conducente: </b><asp:Label ID="lb_conducente" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Indirizzo: </b><asp:Label ID="lb_conducente_indirizzo" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Città: </b><asp:Label ID="lb_conducente_citta" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                     <b>Citta nascita: </b><asp:Label ID="lb_conducente_citta_nascita" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Data nascita: </b><asp:Label ID="lb_conducente_data_nascita" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Codice Fiscale: </b><asp:Label ID="lb_conducente_codice_fiscale" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                     <b>Patente: </b><asp:Label ID="lb_conducente_patente" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Luogo Emissione: </b><asp:Label ID="lb_conducente_patente_emissione" runat="server" Text="" ></asp:Label>
                </td>
                <td >
                     <b>Scadenza patente: </b><asp:Label ID="lb_conducente_patente_scadenza" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div >

        <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border:1px solid #000000" >
            <tr>
                <td align="center" style="width:5%">
                     <b>Q.ta </b>
                </td>
                <td align="center" style="width:45%">
                     <b>Descrizione Acquisti: </b>
                </td>
                <td align="center" style="width:5%">
                     <b>Q.ta </b>
                </td>
                <td align="center" style="width:45%">
                     <b>Descrizione Acquisti: </b>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

    <hr />
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td>
                     &nbsp;<b>Descrizione Danni: </b>
                </td>
            </tr>
        </table>    
 <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table3">
    <tr>
      <td>
          <asp:ListView ID="listViewElencoDanniPerEvento" runat="server" DataSourceID="sqlElencoDanniCollegati">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_tipo_record" runat="server" Text='<%# Eval("tipo_record") %>' Visible="false" />
                          <asp:Label ID="lb_des_tipo_record" runat="server" Text=''/>
                      </td>                    
                      <td>
                          <asp:Label ID="lb_id_danno" runat="server" Text='<%# Eval("id_danno") %>' Visible="false" />
                          <asp:Label ID="lb_id_posizione_danno" runat="server" Text='<%# Eval("id_posizione_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_posizione_danno" runat="server" Text='<%# Eval("des_id_posizione_danno") %>'/>
                          <asp:Label ID="lb_id_dotazione" runat="server" Text='<%# Eval("id_dotazione") %>' Visible="false"/>
                          <asp:Label ID="lb_id_acessori" runat="server" Text='<%# Eval("id_acessori") %>' Visible="false"/>
                          <asp:Label ID="lb_des_des_dotazione" runat="server" Text='<%# Eval("des_dotazione") %>' />
                          <asp:Label ID="lb_des_des_acessori" runat="server" Text='<%# Eval("des_acessori") %>' />
                          <asp:Label ID="lb_descrizione_danno" runat="server" Text='<%# Eval("descrizione_danno") %>' Visible="false"/>
                      </td>
                       <td>
                          <asp:Label ID="lb_id_tipo_danno" runat="server" Text='<%# Eval("id_tipo_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_danno" runat="server" Text='<%# Eval("des_id_tipo_danno") %>' />
                          <asp:Label ID="lb_des_id_tipo_danno_tipo_record" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_entita_danno" runat="server" Text='<%# Eval("entita_danno") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_entita_danno" runat="server" Text='' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato" runat="server" Text='<%# Eval("stato") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_stato" runat="server" Text='' />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Nessun danno salvato per questo evento.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" >
                                      <th id="Th4" runat="server">
                                          Tipo</th>
                                      <th id="Th1" runat="server">
                                          Posizione</th>
                                      <th id="Th2" runat="server">
                                          Danno</th>
                                      <th id="Th3" runat="server">
                                          Entità</th>
                                      <th id="Th6" runat="server">
                                          Riparato</th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
      </td>
    </tr>
  </table>	
  <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td>
                &nbsp;<b>Annotazioni: </b>
        </td>
    </tr>
        <tr>
        <td>
                <uc1:NotePerStampa id="NotePerStampa" runat="server" />
        </td>
    </tr>
 </table>    
 <hr />
 <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td style="width:30%">
                &nbsp;<b>Emessa Fattura n.: </b>
                <asp:Label ID="lb_numero_fattura" runat="server" Text="" ></asp:Label>
        </td>
        <td style="width:20%">
                <b>del: </b>
                <asp:Label ID="lb_data_fattura" runat="server" Text="" ></asp:Label>
        </td>
        <td style="width:25%">
                <b>Importo: </b>
                <asp:Label ID="lb_importo_fattura" runat="server" Text="" ></asp:Label>
        </td>
        <td style="width:25%">
                &nbsp;</td>
    </tr>
 </table> 

 <hr />

 <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td style="width:60%">
                &nbsp;<b>Autorizzato preventivo da: </b>
                <asp:Label ID="lb_autorizzato_preventivo" runat="server" Text="" ></asp:Label>
        </td>
        <td style="width:15%">
                <b>il: </b>
                <asp:Label ID="lb_data_autorizzazione_preventivo" runat="server" Text="" ></asp:Label>
        </td>
        <td style="width:25%">
                <b>Importo: </b>
                <asp:Label ID="lb_importo_preventivo" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
 </table> 

</div>

    <asp:Label ID="lb_num_odl" runat="server" Text='0' visible="false"></asp:Label>
    <asp:Label ID="lb_id_veicolo" runat="server" Text='0' visible="false"></asp:Label>
    <asp:Label ID="lb_id_gruppo_apertura" runat="server" Text='0' visible="false"></asp:Label>

   <%-- <asp:SqlDataSource ID="sqlElencoDanniAperti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT d.id id_danno, d.tipo_record, d.stato, d.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno, d.da_addebitare,
            a.id id_dotazione, a.descrizione des_dotazione, ce.id id_acessori, ce.descrizione des_acessori, SUBSTRING(d.descrizione, 1, 30) + '...' descrizione_danno
            FROM veicoli_danni d WITH(NOLOCK)
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            LEFT JOIN accessori a WITH(NOLOCK) ON d.id_dotazione = a.id
            LEFT JOIN condizioni_elementi ce WITH(NOLOCK) ON d.id_acessori = ce.id
            WHERE d.id_evento_apertura = @id_evento
            ORDER BY d.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_evento" Name="id_evento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>--%>

<asp:SqlDataSource ID="sqlElencoDanniCollegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT e.id_rds, e.id id_evento, d.id id_danno, d.tipo_record, d.stato, CASE WHEN d.num_odl IS NULL THEN 0 ELSE 1 END chiusura_danno, 
            d.id_posizione_danno, pd.descrizione des_id_posizione_danno, 
            d.id_tipo_danno, td.descrizione des_id_tipo_danno, d.entita_danno, d.da_addebitare,
            a.id id_dotazione, a.descrizione des_dotazione, ce.id id_acessori, ce.descrizione des_acessori, SUBSTRING(d.descrizione, 1, 30) + '...' descrizione_danno
            FROM veicoli_danni d WITH(NOLOCK)
            INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON d.id = gd.id_danno 
            INNER JOIN veicoli_evento_apertura_danno e WITH(NOLOCK) ON d.id_evento_apertura = e.id
            LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = d.id_posizione_danno
            LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = d.id_tipo_danno
            LEFT JOIN accessori a WITH(NOLOCK) ON d.id_dotazione = a.id
            LEFT JOIN condizioni_elementi ce WITH(NOLOCK) ON d.id_acessori = ce.id
            WHERE gd.id_evento_apertura = @id_gruppo_apertura
            AND d.attivo = 1 
            ORDER BY e.data_rds DESC, e.id_rds, d.id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_gruppo_apertura" Name="id_gruppo_apertura" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>        

    </form>
</body>
</html>
