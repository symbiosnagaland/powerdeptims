<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntryTemporary.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="IMS_PowerDept.CentralStore.IssueEntryTemporary" %>


<%@ Register Src="~/UserControls/IssueEntryTemp.ascx" TagPrefix="uc1" TagName="IssueEntryTemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(function () {
            $("#ContentPlaceHolder1_IssueEntryTemp__tbChallanDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
            $("#ContentPlaceHolder1_IssueEntryTemp__tbIntendDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
        });

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:IssueEntryTemp runat="server" ID="IssueEntryTemp" />
</asp:Content>
