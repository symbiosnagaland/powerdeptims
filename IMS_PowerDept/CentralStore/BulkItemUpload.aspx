<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="BulkItemUpload.aspx.cs" Inherits="IMS_PowerDept.CentralStore.BulkItemUpload" %>

<%@ Register Src="~/UserControls/BulkItemsUploadControl.ascx" TagPrefix="uc1" TagName="BulkItemsUploadControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:BulkItemsUploadControl runat="server" id="BulkItemsUploadControl" />
</asp:Content>
