<%@ Page Title="Manage Items" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ManageItems.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ManageItems" %>

<%@ Register Src="~/UserControls/ItemsControl.ascx" TagPrefix="uc1" TagName="ItemsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <ul class="breadcrumb">
  <li><a href="#">ADMINISTRATOR PANEL</a></li>
 
  <li class="active">Manage Items</li>
</ul>
    	<br />--%>

    <uc1:ItemsControl runat="server" id="ItemsControl" />

</asp:Content>
