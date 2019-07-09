<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ItemsListInventory.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ItemsListInventory" %>

<%@ Register Src="~/UserControls/ItemsListInventoryControl.ascx" TagPrefix="uc1" TagName="ItemsListInventoryControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ItemsListInventoryControl runat="server" ID="ItemsListInventoryControl" />
  
</asp:Content>
