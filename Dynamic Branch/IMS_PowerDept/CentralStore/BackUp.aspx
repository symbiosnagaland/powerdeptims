<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="BackUp.aspx.cs" Inherits="IMS_PowerDept.CentralStore.BackUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Button ID="btnTakeBackup" runat="server" Text="Backup Database" OnClick="btnTakeBackup_Click" />


    <br />
    <br />
    <br />

    <br />

    List of available backups : (Latest to oldest displayed)<br />
    <br />
&nbsp;<asp:ListBox ID="listBoxBackupFiles" runat="server" Width="468px" Height="215px"></asp:ListBox>


    <br />
    <br />
    Select any of the available backup above and click on Restore or Delete buttons&nbsp; below<br />
    <br />


    <asp:Button ID="btnRestore" runat="server" Text="Restore Selected Backup" OnClick="btnRestore_Click" />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete the selected backup?');"
   Text="Delete Selected BackUp" OnClick="btnDelete_Click" />

<br />
<br />

<asp:Label ID="lblmessage" runat="server" Text="" style="font-weight: 700; font-size: medium; color: #0000FF"></asp:Label>

</asp:Content>
