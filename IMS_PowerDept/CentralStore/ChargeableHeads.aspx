<%@ Page Title="Chargeable Heads" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ChargeableHeads.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ChargeableHeads" %>

<%@ Register Src="~/UserControls/ChargeableHeadsControl.ascx" TagPrefix="uc1" TagName="ChargeableHeadsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ChargeableHeadsControl runat="server" ID="ChargeableHeadsControl" />
    
</asp:Content>
