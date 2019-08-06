<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PrintTemplate.Master" AutoEventWireup="true" CodeBehind="DivisionsList.aspx.cs" Inherits="IMS_PowerDept.Reports.DivisionsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="_sdsDivision" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [Divisions]  ORDER BY [division] ASC"></asp:SqlDataSource>

 <h3 style="text-align:center; margin:12px">LIST OF DIVISIONS</h3>
    <div style=" margin:0px auto; width: 411px;">
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="division" DataSourceID="_sdsDivision" ForeColor="Black" GridLines="Horizontal" ValidateRequestMode="Enabled" Width="100%">
            <Columns>
                <asp:BoundField DataField="division" HeaderText="Division" ReadOnly="True" SortExpression="division" />
                <asp:BoundField DataField="divisionName" HeaderText="Division Name" SortExpression="divisionName" />
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
