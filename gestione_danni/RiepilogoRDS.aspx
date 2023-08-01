<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiepilogoRDS.aspx.vb" Inherits="gestione_danni_RiepilogoRDS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <h2>Riepilogo Documenti Sinistri</h2>
        <h3> Stazione <asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label></h3>
    </center>
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td>
            &nbsp;<b>Numero RDS: </b>
            <asp:Label ID="lb_numero_rds" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Data: </b>
            <asp:Label ID="lb_data_rds" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
    </table>
    <hr />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td>
            &nbsp;<b>Documento di Riferimento: </b>
            <asp:Label ID="lb_tipo_documento" runat="server" Text="" ></asp:Label>
            &nbsp;
            <asp:Label ID="lb_num_documento" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td> 
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>    
            &nbsp;&nbsp;<b>Proprietario: </b><asp:Label ID="lb_proprietario" runat="server" Text="" ></asp:Label>    
        </td>
    </tr>
</table>
    <hr />
    <div >
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td style="width:30%">
                     &nbsp;<b>Data incidente: </b>
                     <asp:Label ID="lb_data_incidente" runat="server" Text="" ></asp:Label>
                </td>
                <td style="width:70%">
                     <b>Luogo Incidente: </b>
                     <asp:Label ID="lb_luogo_incidente" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <div >
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td colspan="2">
                     &nbsp;<b>Documenti Allegati: </b>
                </td>
            </tr>
            <tr>
                <td style="width:30%">
                    &nbsp;<b>CID: </b>
                </td>
                <td>
                    <asp:Label ID="lb_CID" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<b>Denuncia o altro attestato: </b>
                </td>
                <td>
                    <asp:Label ID="lb_denuncia" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<b>PREVENTIVO: </b>
                </td>
                <td>
                    <asp:Label ID="lb_preventivo" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<b>Fotografie Numero: </b>
                </td>
                <td>
                    <asp:Label ID="lb_num_fotografie" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td>
                    &nbsp;<b>Chiavi AUTO Rubata: </b>
                </td>
                <td>
                    <asp:Label ID="lb_chiavi_auto_rubata" runat="server" Text="" ></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                    &nbsp;<b>Fotocopia Documenti: </b>
                </td>
                <td>
                    <asp:Label ID="lb_fotocopia_documenti" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <div >
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td style="width:38%">
                     &nbsp;<b>Compilato da: </b><asp:Label ID="lb_compilato_da" runat="server" Text="" ></asp:Label>
                </td>
                <td style="width:26%">
                     &nbsp;<b>Spedito il: </b><asp:Label ID="lb_data_spedizione" runat="server" Text="" ></asp:Label>
                </td>
                <td style="width:38%">
                     &nbsp;<b>Spedito da: </b><asp:Label ID="lb_spedito_da" runat="server" Text="" ></asp:Label>
                </td>
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
          <asp:ListView ID="listViewElencoDanniPerEvento" runat="server" DataSourceID="sqlElencoDanniPerEvento">
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
                      <td id="Td3" runat="server" >
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
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
                      <td id="Td3" runat="server" >
                          <asp:Label ID="lb_da_addebitare" runat="server" Text='<%# Eval("da_addebitare") %>' Visible="false" />
                          <asp:Label ID="lb_des_da_addebitare" runat="server" Text='' />
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
                                      <th id="th_da_addebitare" runat="server">
                                          Da Addebitare</th>
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
                <asp:Label ID="lb_note" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
 </table>    
 <hr />
 <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td colspan="4">
                &nbsp;<b>Importo (escluso I.V.A.): </b>
                &nbsp;<asp:Label ID="lb_importo" runat="server" Text="" ></asp:Label>
                &nbsp;&nbsp;<b>I.V.A.: </b>
                &nbsp;<asp:Label ID="lb_aliquota_iva" runat="server" Text="" ></asp:Label>
                &nbsp;&nbsp;<b>Spese Apertura pratica Sinistro: </b>
                &nbsp;<asp:Label ID="lb_spese_postali" runat="server" Text="" ></asp:Label>
                &nbsp;&nbsp;<b>Totale: </b>
                &nbsp;<asp:Label ID="lb_totale" runat="server" Text="" ></asp:Label>
                <br />
                &nbsp;<b>Perizia Effettuata: </b>
                &nbsp;<asp:Label ID="lb_perizia_effettuata" runat="server" Text="" ></asp:Label>
                &nbsp;&nbsp;<b>in data: </b>
                &nbsp;<asp:Label ID="lb_perizia_data" runat="server" Text="" ></asp:Label>
                <br />
                &nbsp;<b>Giorni Fermo Tecnico: </b>
                &nbsp;<asp:Label ID="lb_giorni_fermo_tecnico" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
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
                <b>Incasso: </b>
                <asp:Label ID="lb_incasso_fattura" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
                &nbsp;<b>Note: </b>
                <asp:Label ID="lb_note_fattura" runat="server" Text="" ></asp:Label>
        </td>
    </tr>
 </table> 
 <hr />
 <center>
 
    <table border="0" cellpadding="0" cellspacing="0" width="70%">
        <tr>
            <td colspan="4" style="text-align:center">
                <b>RISERVATO UFFICIO SINISTRI</b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <b>Ricevuto il: </b>
            </td>
            <td align="left">
                <b>__/__/____</b>
            </td>
            <td align="left">
                <b>Protocollo Sede: </b>
            </td>
            <td align="left">
                <b>_______________</b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <b>Giudicato: </b>
            </td>
            <td align="left">
                <b>ATTIVO PASSIVO</b>
            </td>
            <td align="left">
                <b>Firma: </b>
            </td>
            <td align="left">
                <b>___________________________</b>
            </td>
        </tr>
    </table>
</center>
</div>

    <asp:Label ID="lb_id_evento" runat="server" Text='0' visible="false"></asp:Label>

    <asp:SqlDataSource ID="sqlElencoDanniPerEvento" runat="server" 
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
</asp:SqlDataSource>
    </form>
</body>
</html>
