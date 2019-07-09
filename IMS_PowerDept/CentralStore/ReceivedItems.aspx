<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ReceivedItems.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ReceivedItems" %>

<%@ Register Src="~/UserControls/ReceiveItems.ascx" TagPrefix="uc1" TagName="ReceiveItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReceiveItems runat="server" ID="ReceiveItems" />
</asp:Content>
