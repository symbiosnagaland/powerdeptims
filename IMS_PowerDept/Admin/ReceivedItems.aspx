<%@ Page Title="Received Items" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ReceivedItems.aspx.cs" Inherits="IMS_PowerDept.Admin.ReceivedItems" %>

<%@ Register Src="~/UserControls/ReceiveItems.ascx" TagPrefix="uc1" TagName="ReceiveItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">     
       $(function () {
           $("#ContentPlaceHolder1_ReceiveItems_tbSupplyDate").datepicker(
               {
                   changeMonth: true,
                   changeYear: true,
                   dateFormat: 'dd/mm/yy'
               });
           $("#ContentPlaceHolder1_ReceiveItems_tbOTEODate").datepicker(
               {
                   changeMonth: true,
                   changeYear: true,
                   dateFormat: 'dd/mm/yy'
               });
       });
</script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReceiveItems runat="server" ID="ReceiveItems" />
</asp:Content>
