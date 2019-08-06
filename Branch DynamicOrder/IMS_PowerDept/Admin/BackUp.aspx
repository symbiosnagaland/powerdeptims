<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="BackUp.aspx.cs" Inherits="IMS_PowerDept.Admin.BackUp" %>
<%@ Register Src="~/UserControls/BackUp.ascx" TagPrefix="uc1" TagName="BackUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <uc1:BackUp runat="server" ID="BackUp" />
</asp:Content>
