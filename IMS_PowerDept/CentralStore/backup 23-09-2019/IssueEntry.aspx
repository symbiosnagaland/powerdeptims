﻿<%@ Page Title="Issue Entry" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntry.aspx.cs" Inherits="IMS_PowerDept.CentralStore.IssueEntry" %>

<%@ Register Src="~/UserControls/IssueEntry.ascx" TagPrefix="uc1" TagName="IssueEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript">


         $(function () {
             $("#ContentPlaceHolder1_IssueEntryer__tbIntendDate").datepicker(
                 {
                     changeMonth: true,
                     changeYear: true,
                     dateFormat: 'dd-mm-yy'
                 });
             $("#ContentPlaceHolder1_IssueEntryer__tbChallanDate").datepicker(
                 {
                     changeMonth: true,
                     changeYear: true,
                     dateFormat: 'dd-mm-yy'
                 });
         });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:IssueEntry runat="server" ID="IssueEntryer" />
</asp:Content>
