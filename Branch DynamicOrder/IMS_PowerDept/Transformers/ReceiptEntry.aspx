<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="ReceiptEntry.aspx.cs" Inherits="IMS_PowerDept.Transformers.ReceiptEntry" %>
<%@ Register src="../UserControls/TransformerReceiptEntry.ascx" tagname="TransformerReceiptEntry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:TransformerReceiptEntry ID="TransformerReceiptEntry1" runat="server" />


</asp:Content>
