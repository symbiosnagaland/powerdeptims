<%@ Page Title="Issue Head" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="IssueHead.aspx.cs" Inherits="IMS_PowerDept.Admin.IssueHead" %>

<%@ Register Src="~/UserControls/IssueHeadsControl.ascx" TagPrefix="uc1" TagName="IssueHeadsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:SqlDataSource ID="_sdsIssueHeads" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [IssueHeads] ORDER BY [IssueHeadID] ASC"></asp:SqlDataSource>
<%--     <ul class="breadcrumb">
  <li><a href="#">ADMINISTRATOR PANEL</a></li>
 
  <li class="active">Issue Head</li>
</ul> 
    <br />--%>

    <uc1:IssueHeadsControl runat="server" ID="IssueHeadsControl" />
    
</asp:Content>
