<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="IMS_PowerDept.Transformers.Dashboard" %>

<%@ Register Src="~/UserControls/Dash.ascx" TagPrefix="uc1" TagName="Dash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Dash runat="server" ID="Dash" />
</asp:Content>


