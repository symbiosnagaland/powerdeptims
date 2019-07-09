<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ItemInventory.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ItemsInventory" %>

<%@ Register Src="~/UserControls/ItemsInventoryControl.ascx" TagPrefix="uc1" TagName="ItemsInventoryControl" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ItemsInventoryControl runat="server" id="ItemsInventoryControl" />
</asp:Content>


