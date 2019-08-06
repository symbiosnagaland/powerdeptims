<%@ Page Title="Divisions" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Divisions.aspx.cs" Inherits="IMS_PowerDept.Admin.Divisions" %>

<%@ Register Src="~/UserControls/DivisionsControl.ascx" TagPrefix="uc1" TagName="DivisionsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <uc1:DivisionsControl runat="server" id="DivisionsControl" />
</asp:Content>
