<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="parcoVeicoli_old.aspx.vb" Inherits="parcoVeicoli2" title="ARES - AutoEuropa Rent System" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Src="parco_veicoli/auto_dati_generali.ascx" TagName="dati_generali" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_accessori.ascx" TagName="auto_accessori" TagPrefix="uc2" %>
<%@ Register Src="parco_veicoli/dati_acquisto.ascx" TagName="auto_dati_acquisto" TagPrefix="uc3" %>
<%@ Register Src="parco_veicoli/auto_assicurazioni.ascx" TagName="auto_assicurazioni" TagPrefix="uc4" %>
<%@ Register Src="parco_veicoli/auto_leasing.ascx" TagName="auto_leasing" TagPrefix="uc5" %>
<%@ Register Src="parco_veicoli/auto_manutenzione.ascx" TagName="auto_manutenzione" TagPrefix="uc6" %>
<%@ Register Src="parco_veicoli/auto_dati_vendita.ascx" TagName="dati_vendita" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css"> 
   .menu
           {
	        border:none;
	        border:0px;
	        margin:0px;
	        padding:0px;
	        font: 67.5% "Lucida Sans Unicode", "Bitstream Vera Sans", "Trebuchet Unicode MS", "Lucida Grande", "Verdana", "Helvetica", "sans-serif";
	        font-size:11px;
	        font-weight:bold;
	        }
	  .style44
      {
          width: 126px;
      }
	  .style45
      {
          width: 78px;
      }
      .style46
      {
          width: 80px;
      }
	</style>        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div>
       <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
         <tr>
           <td >
                <%--2--%>
                <asp:Button ID="DatiGenerali" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Dati Generali" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
            </td>
            <td >
                <%--3--%>
                <asp:Button ID="Accessori" runat="server" CssClass="menu"
                    Font-Names="Verdana" Height="21px" Text="Accessori" Width="121px" 
                    BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>
             <td>
                <%--4--%>
                <asp:Button ID="DatiAcquisto" runat="server" CssClass="menu" 
                    Font-Names="Verdana" Height="21px" Text="Dati Acquisto" Width="121px" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>
             <td>
                <%--5--%>
                <asp:Button ID="DatiVendita" runat="server" CssClass="menu" 
                    Font-Names="Verdana" Height="21px" Text="Dati Vendita" Width="121px" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>
             <td>
                <%--6--%>
                <asp:Button ID="Assicurazione" runat="server" CssClass="menu" 
                    Font-Names="Verdana" Height="21px" Text="Assicurazione e Sinistri" 
                     Width="177px" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>
             <td>
                <%--7--%>
                <asp:Button ID="Leasing" runat="server" CssClass="menu" 
                    Font-Names="Verdana" Height="21px" Text="Leasing e Lungo Termine" Width="186px" 
                    BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>
             <td>
                <%--8--%>
                <asp:Button ID="Manutenzione" runat="server" CssClass="menu" 
                    Font-Names="Verdana" Height="21px" Text="Manutenzione" Width="140px" 
                    BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
             </td>  
        </tr>
       </table>
      <asp:Panel ID="PanelDatiGenerali" runat="server" Visible="false" Width="100%">
        <uc1:dati_generali ID="dati_generali" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelAccessori" runat="server" Visible = "false" Width="100%">
        <uc2:auto_accessori ID="auto_accessori" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelDatiAcquisto" runat="server" Visible = "false" Width="100%">
       <uc3:auto_dati_acquisto ID="auto_dati_acquisto" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelDatiVendita" runat="server" Visible = "false" Width="100%">
       <uc7:dati_vendita ID="auto_dati_vendita" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelAssicurazione" runat="server" Visible = "false" Width="100%">
       <uc4:auto_assicurazioni ID="auto_assicurazioni" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelLeasing" runat="server" Visible = "false" Width="100%">
       <uc5:auto_leasing ID="auto_leasing" runat="server" />
      </asp:Panel>
      <asp:Panel ID="PanelManutenzione" runat="server" Visible = "false" Width="100%">
       <uc6:auto_manutenzione ID="auto_manutenzione" runat="server" />
      </asp:Panel>
      <hr />
      <table border="0" cellpadding="0" cellspacing="0" width="1024px">
        <tr>
          <td align="left" class="style46">
               Targa</td>
          <td align="left" class="style45">
               Marca</td>
          <td align="left" class="style44">
               Modello</td>
          <td align="left" class="style44">
               &nbsp;</td>
          <td align="right">
            
               &nbsp;</td>
        </tr>
        <tr>
          <td align="left" class="style46">
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="85px"></asp:TextBox>
          </td>
          <td align="left" class="style45">
            
               <asp:DropDownList ID="dropCercaMarca" runat="server" AutoPostBack="True" 
                   DataSourceID="SqlCercaMarca" DataTextField="descrizione" DataValueField="id" 
                   style="margin-left: 0px" AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
            
          </td>
          <td align="left" class="style44">
            
               <asp:DropDownList ID="dropCercaModello" runat="server" AppendDataBoundItems="True" 
                   DataSourceID="SqlCercaModello" DataTextField="descrizione" 
                   DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona..</asp:ListItem>
               </asp:DropDownList>
            </td>
          <td align="left" class="style44">
            
               <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" BackColor="#386587" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />

            </td>
          <td align="right">
            
               <asp:Button ID="btnNuovo" runat="server" Text="Nuovo veicolo" BackColor="#386587" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />

                <asp:Button ID="btnImportVeicoli" runat="server" Text="Import veicoli da File" BackColor="#386587" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />

          </td>
        </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="1024px">
        <tr>
          <td colspan="2">
              <asp:ListView ID="listVeicoli" runat="server" DataKeyNames="id" 
                  DataSourceID="sqlVeicoli">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                          </td>
                          <td>
                              <asp:Label ID="marcaLabel" runat="server" Text='<%# Eval("marca") %>' />
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <td>
                              <asp:Label ID="alimentazioneLabel" runat="server" 
                                  Text='<%# Eval("alimentazione") %>' />
                          </td>
                          <td>
                              <asp:Label ID="coloreLabel" runat="server" Text='<%# Eval("colore") %>' />
                          </td>
                          <td>
                              <asp:Label ID="proprietarioLabel" runat="server" 
                                  Text='<%# Eval("proprietario") %>' />
                          </td>
                          <td align="center">
                              <asp:Button ID="btnVedi" runat="server" Text="Vedi" CommandName="vedi" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr>
                          <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                          </td>
                          <td>
                              <asp:Label ID="marcaLabel" runat="server" Text='<%# Eval("marca") %>' />
                          </td>
                          <td>
                              <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                          </td>
                          <td>
                              <asp:Label ID="alimentazioneLabel" runat="server" 
                                  Text='<%# Eval("alimentazione") %>' />
                          </td>
                          <td>
                              <asp:Label ID="coloreLabel" runat="server" Text='<%# Eval("colore") %>' />
                          </td>
                          <td>
                              <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario") %>' />
                          </td>
                          <td align="center">
                              <asp:Button ID="btnVedi" runat="server" Text="Vedi" CommandName="vedi" />
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
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#215A87">
  
                                          <th id="Th1" runat="server" align="left">
                                              Targa</th>
                                          <th id="Th2" runat="server" align="left">
                                              Marca</th>
                                          <th id="Th3" runat="server" align="left">
                                              Modello</th>
                                          <th id="Th4" runat="server" align="left">
                                              Alimentazione</th>
                                          <th id="Th5" runat="server" align="left">
                                              Colore</th>
                                          <th id="Th6" runat="server" align="left">
                                              Proprietario</th>
                                          <th></th>   
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" runat="server" >
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
    <asp:SqlDataSource ID="sqlVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT veicoli.id, veicoli.targa, marche.descrizione As marca, modelli.descrizione As modello, alimentazione.descrizione As alimentazione, veicoli.colore, proprietari_veicoli.descrizione As proprietario FROM veicoli INNER JOIN modelli ON veicoli.id_modello = modelli.id INNER JOIN marche on modelli.id_marca = marche.id LEFT JOIN alimentazione ON veicoli.id_alimentazione = alimentazione.id LEFT JOIN proprietari_veicoli ON veicoli.id_proprietario = proprietari_veicoli.id WHERE id_marca>0"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaMarca" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [marche] ORDER BY [descrizione]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCercaModello" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [id], [descrizione] FROM [modelli] WHERE ([id_marca] = @id_marca) ORDER BY [descrizione]">
        <SelectParameters>
            <asp:ControlParameter ControlID="dropCercaMarca" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

