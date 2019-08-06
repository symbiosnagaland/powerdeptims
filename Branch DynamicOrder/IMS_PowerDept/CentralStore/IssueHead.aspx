<%@ Page Title="Issue Head" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="IssueHead.aspx.cs" Inherits="IMS_PowerDept.CentralStore.IssueHead" %>

<%@ Register Src="~/UserControls/IssueHeadsControl.ascx" TagPrefix="uc1" TagName="IssueHeadsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:SqlDataSource ID="_sdsIssueHeads" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [IssueHeads] ORDER BY [IssueHeadID] ASC"></asp:SqlDataSource>


    <uc1:IssueHeadsControl runat="server" ID="IssueHeadsControl" />
    
</asp:Content>
