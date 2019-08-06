<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PrintTemplate.Master" AutoEventWireup="true" CodeBehind="ItemsList.aspx.cs" Inherits="IMS_PowerDept.Reports.ItemsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:SqlDataSource ID="_sdsItems" runat="server"
                             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [Items] where Status='A' ORDER BY [itemid] ASC"></asp:SqlDataSource>

      <h3 style="text-align:center; margin:12px">ITEMS LIST</h3>
    <div style=" margin:0px auto; width: 736px;">
       <asp:GridView ID="GridView1" runat="server" Width="100%" DataSourceID="_sdsItems" AutoGenerateColumns="False" DataKeyNames="itemid" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
        <Columns>
            <asp:BoundField DataField="itemid" HeaderText="Item ID" ReadOnly="True" SortExpression="itemid" />
            <asp:BoundField DataField="itemname" HeaderText="Item Name" SortExpression="itemname" />
            <asp:BoundField DataField="unit" HeaderText="Unit" SortExpression="unit" />
            <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" SortExpression="Status" />
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
