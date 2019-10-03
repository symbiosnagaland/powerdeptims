<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Itemwise_Received.aspx.cs" Inherits="IMS_PowerDept.Admin.Itemwise_Received" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Items_Wise Received</h2>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ReceivedItemsOTEOID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="OTEOID" ReadOnly="True" SortExpression="ReceivedItemsOTEOID" />
            <asp:BoundField DataField="ReceivedItemOTEODate" HeaderText="OTEODate" SortExpression="ReceivedItemOTEODate" />
            <asp:BoundField DataField="SupplyOrderReference" HeaderText="SupplyReference" SortExpression="SupplyOrderReference" />
            <asp:BoundField DataField="SupplyOrderDate" HeaderText="SupplyOrderDate" SortExpression="SupplyOrderDate" />
            <asp:BoundField DataField="itemname" HeaderText="item" SortExpression="itemname" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHead" SortExpression="IssueHeadName" />
            <asp:BoundField DataField="ChargeableHeadName" HeaderText="ChargeableHead" SortExpression="ChargeableHeadName" />
            <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
        </Columns>
    </asp:GridView>
     <asp:Button ID="btnExportToExcel" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select ReceivedItemsOTEO.ReceivedItemsOTEOID,
ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate,
itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit
 from ReceivedItemsOTEO,ReceivedItemsDetails where 
(ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID) order
by ReceivedItemsOTEOID"></asp:SqlDataSource>
</asp:Content>
