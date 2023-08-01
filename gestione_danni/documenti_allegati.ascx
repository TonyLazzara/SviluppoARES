<%@ Control Language="VB" AutoEventWireup="false" CodeFile="documenti_allegati.ascx.vb" Inherits="gestione_danni_documenti_allegati" %>

<div id="div_allegati" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" style="border:4px solid #444">
    <tr>
      <td colspan="2">
         <asp:ListView ID="listViewDocumentiODL" runat="server" 
              DataSourceID="sqlDocumentiODL" DataKeyNames="id" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td> 
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni_F]'><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_des_tipo_documento" runat="server" Text='<%# Eval("des_tipo_documento") %>' />
                          <asp:Label ID="lb_tipo_documento" runat="server" Text='<%# Eval("tipo_documento") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_riferimento_foto" runat="server" Text='<%# Eval("riferimento_foto") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <a href="/images/FotoDanni/<%# Eval("riferimento_foto") %>" rel='lytebox[danni_F]'><img src="/images/lente.png" style="width: 16px" /></a>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del documento?'));" ToolTip="Elimina Documento" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun Documento Allegato.
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
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>
                                          Documento</th>
                                      <th>
                                          Nome</th>
                                      <th>
                                          </th>
                                      <th>
                                          </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Allega Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:FileUpload ID="FileUpload_odl" size="32" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label33" runat="server" Text="Tipo Documento:" CssClass="testo_bold"></asp:Label>
        </td>
        <td id="Td10" runat="server" >
            <asp:DropDownList ID="DropDownTipoDocumenti" runat="server" AppendDataBoundItems="True" 
                      DataSourceID="sqlTipoDocumentoImg" DataTextField="descrizione" DataValueField="id">
                <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="btnInviaFile_odl" runat="server" Text="Salvataggio" ValidationGroup="Upload_F" />
        </td>
    </tr>
   </table> 
</div>

<asp:Label ID="lb_id_tipo" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_documento" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_lente" runat="server" Text='false' Visible="false" />


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"
    SelectCommand="SELECT n.*, (o.cognome + ' ' + o.nome) utente FROM note n WITH(NOLOCK)
        INNER JOIN operatori o WITH(NOLOCK) ON o.id = n.id_utente
        WHERE n.id_tipo = @id_tipo
        AND n.id_documento = @id_documento
        ORDER BY n.data_creazione DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_tipo" Name="id_tipo" PropertyName="Text" Type="Int32"  />
            <asp:ControlParameter ControlID="lb_id_documento" Name="id_documento" PropertyName="Text" Type="Int32"  />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlDocumenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT vdf.*, td.descrizione des_tipo_documento
            FROM [veicoli_danni_foto] vdf WITH(NOLOCK) 
            INNER JOIN veicoli_danni_img_tipo_documenti td WITH(NOLOCK) ON td.id = vdf.tipo_documento 
            WHERE vdf.id_danno = @id_documento ORDER BY td.ordine, vdf.descrizione">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_documento" Name="id_documento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlTipoDocumentoImg" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_danni_img_tipo_documenti] WITH(NOLOCK) WHERE tipo = 1 ORDER BY ordine">
</asp:SqlDataSource>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Salva" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_nota" ErrorMessage="Nessuna nota immessa." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Salva"></asp:RequiredFieldValidator> 
