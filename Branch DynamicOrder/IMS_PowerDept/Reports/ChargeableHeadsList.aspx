<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PrintTemplate.Master" AutoEventWireup="true" CodeBehind="ChargeableHeadsList.aspx.cs" Inherits="IMS_PowerDept.Reports.ChargeableHeadsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:SqlDataSource ID="_sds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ChargeableHeads] where Status='A' ORDER BY [ChargeableHeadID] ASC"></asp:SqlDataSource>

 <h3 style="text-align:center; margin:12px">CHARGEABLE HEADS</h3>
    <div style=" margin:0px auto; width: 616px;">

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ChargeableHeadID" DataSourceID="_sds" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="100%">
            <Columns>
                <asp:BoundField DataField="ChargeableHeadID" HeaderText="ChargeableHeadID" InsertVisible="False" ReadOnly="True" SortExpression="ChargeableHeadID" />
                <asp:BoundField DataField="ChargeableHeadName" HeaderText="ChargeableHeadName" SortExpression="ChargeableHeadName" />
                <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" SortExpression="Status" />
                <asp:BoundField DataField="IssueHeadID" Visible="false" HeaderText="IssueHeadID" SortExpression="IssueHeadID" />
                <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHeadName" SortExpression="IssueHeadName" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        </div>
</asp:Content>
