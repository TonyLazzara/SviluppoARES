<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="vedi_foto_danni.aspx.vb" Inherits="vedi_foto_danni" title="" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=0">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div runat="server" id="div_targa" visible="true">
  <table border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td  align="left" style="color: #FFFFFF;background-color:#444;"> 
               
            <div id="div_num_documento" runat="server" visible="true" >
                &nbsp;<b>Numero Documento: </b>
                <asp:Label ID="lb_num_documento1" runat="server" Text="" ></asp:Label>
                <asp:LinkButton ID="lb_num_documento" runat="server" CssClass="testo_bold" style="color: #FFFFFF" Text="" />
                &nbsp;
            </div>
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Stazione: </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>    
            <asp:Label ID="IDdelVeicolo" runat="server" Text="ID" Visible="false" ></asp:Label>  
        </td>
    </tr>
</table>
</div>

    <div class="container" style="padding-right: 146px;">
  <h2><asp:Label ID="lblNumContratto" runat="server" Text=""></asp:Label></h2>  
  <div id="myCarousel" class="carousel slide" data-ride="carousel">
    <!-- Indicators -->
    <ol class="carousel-indicators">
      <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
      <li data-target="#myCarousel" data-slide-to="1"></li> 
      <li data-target="#myCarousel" data-slide-to="2"></li>
      <li data-target="#myCarousel" data-slide-to="3"></li>      
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner">
        <%
            Dim ArrayStinga(2) As String
            
            ArrayStinga = Split(Session("DatiSlideImg"), "@")
            For i = 0 To UBound(ArrayStinga)
                Response.Write(ArrayStinga(i))
            Next
        %>
        
    </div>

    <!-- Left and right controls -->
    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
      <span class="glyphicon glyphicon-chevron-left"></span>
      <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#myCarousel" data-slide="next">
      <span class="glyphicon glyphicon-chevron-right"></span>
      <span class="sr-only">Next</span>
    </a>
  </div>
</div>
</asp:Content>

