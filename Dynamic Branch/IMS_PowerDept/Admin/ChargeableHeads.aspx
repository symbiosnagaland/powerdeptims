<%@ Page Title="Chargeable Heads" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ChargeableHeads.aspx.cs" Inherits="IMS_PowerDept.Admin.ChargeableHeads" %>

<%@ Register Src="~/UserControls/ChargeableHeadsControl.ascx" TagPrefix="uc1" TagName="ChargeableHeadsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:SqlDataSource ID="_sds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ChargeableHeads] where Status='A' ORDER BY [ChargeableHeadID] ASC"></asp:SqlDataSource>
    <uc1:ChargeableHeadsControl runat="server" ID="ChargeableHeadsControl" />
</asp:Content>
