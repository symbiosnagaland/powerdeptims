<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ItemsListInventory.aspx.cs" Inherits="IMS_PowerDept.Admin.ItemsListInventory" %>

<%@ Register Src="~/UserControls/ItemsListInventoryControl.ascx" TagPrefix="uc1" TagName="ItemsListInventoryControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ContentPlaceHolder1_ItemsListInventoryControl_tbDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
            
        });
</script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ItemsListInventoryControl runat="server" id="ItemsListInventoryControl" />
</asp:Content>
