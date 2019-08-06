<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PrintTemplate.Master" AutoEventWireup="true" CodeBehind="IssueHeadsList.aspx.cs" Inherits="IMS_PowerDept.Reports.IssueHeadsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:SqlDataSource ID="_sds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ISSUEHEADS] where Status='A' ORDER BY [ISSUEHEADID] ASC"></asp:SqlDataSource>
    <h3 style="text-align:center; margin:12px">ISSUE HEADS</h3>
    <div style=" margin:0px auto; width: 397px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IssueHeadID" DataSourceID="_sds" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="100%">
            <Columns>
                <asp:BoundField DataField="IssueHeadID" HeaderText="IssueHeadID" InsertVisible="False" ReadOnly="True" SortExpression="IssueHeadID" />
                <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHeadName" SortExpression="IssueHeadName" />
                <asp:BoundField DataField="Status" Visible="false" HeaderText="Status" SortExpression="Status" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Top" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        </div>
</asp:Content>
