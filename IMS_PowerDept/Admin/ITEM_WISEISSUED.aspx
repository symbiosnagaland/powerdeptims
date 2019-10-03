<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ITEM_WISEISSUED.aspx.cs" Inherits="IMS_PowerDept.Admin.ITEM_WISEISSUED" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2> ITEMS ISSUED</h2>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="DeliveryItemsChallanID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="DeliveryItemsChallanID" HeaderText="Challan ID" ReadOnly="True" SortExpression="DeliveryItemsChallanID" />
            <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" SortExpression="ChallanDate" dataformatstring="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="IndentReference" HeaderText="Indent Ref" SortExpression="IndentReference" />
            <asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate" dataformatstring="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHead" SortExpression="IssueHeadName" />
            <asp:BoundField DataField="ChargeableHeadName" HeaderText="Chargeable Hd" SortExpression="ChargeableHeadName" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Amount" SortExpression="TotalAmount" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate, DeliveryItemsChallan.IndentReference,
DeliveryItemsChallan.IndentDate, DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,
DeliveryItemsDetails.IssueHeadName, DeliveryItemsChallan.ChargeableHeadName, DeliveryItemsChallan.TotalAmount
FROM DeliveryItemsChallan
INNER JOIN DeliveryItemsDetails ON DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID order by itemname, challandate;"></asp:SqlDataSource>
&nbsp;
</asp:Content>
