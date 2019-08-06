<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ItemInventory.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ItemsInventory" %>

<%@ Register Src="~/UserControls/ItemsInventoryControl.ascx" TagPrefix="uc1" TagName="ItemsInventoryControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../css/components.css" rel="stylesheet" type="text/css"/>   
    <link href="../css/icons/icomoon/styles.css" rel="stylesheet" type="text/css"/>

    <script type="text/javascript" src="../js/core/jquery.min.js"></script>	
    <script type="text/javascript" src="../js/plugins/selects/select2.min.js"></script>
    <script type="text/javascript" src="../js/pages/form_select2.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ItemsInventoryControl runat="server" id="ItemsInventoryControl" />
</asp:Content>


