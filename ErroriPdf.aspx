<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="ErroriPdf.aspx.vb" Inherits="ErroriPdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <% 
        Dim CodiceErrore As String = Request.Params("CodiceErrore")
        CodiceErrore = CodiceErrore
        If CodiceErrore Is Nothing Then
            Response.Write("Errore non specificato")
        Else
            CodiceErrore = CodiceErrore.Substring(0, IIf(CodiceErrore.Length < 20, CodiceErrore.Length, 20))
            If CodiceErrore = "NoFile" Then
                Response.Write("Non è stato specificato il file da stampare")
            ElseIf CodiceErrore = "FileNoExists" Then
                Response.Write("Il file specificato non esiste oppure e avvenuto un errore nella generazione dell'HTML")
            Else
                Response.Write("Parametro di errore: " & CodiceErrore)
            End If
        End If
    %>
</div>
</asp:Content>

