<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntry.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="IMS_PowerDept.Admin.IssueEntry" %>

<%@ Register Src="~/UserControls/IssueEntry.ascx" TagPrefix="uc1" TagName="IssueEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:IssueEntry runat="server" ID="IssueEntryer" />

</asp:Content>
