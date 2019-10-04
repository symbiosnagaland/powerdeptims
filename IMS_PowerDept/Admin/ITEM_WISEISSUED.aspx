<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ITEM_WISEISSUED.aspx.cs" Inherits="IMS_PowerDept.Admin.ITEM_WISEISSUED" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="../css/sb-admin.css" rel="stylesheet" />
    <style>

         .product{
            width:120px;
        }
        .table{
            width:870px;
            margin: 0 auto;
            padding:0;

        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w" style="padding-bottom:20px;">
        <div class="h_title">Items Issued</div>

        <table class="table">

            <tr style="w">
                <td>
                    <span style="float: right; text-align:right;">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />
                               
                    </span>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="DeliveryItemsChallanID" DataSourceID="SqlDataSource1">
                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                             <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/> 
                        
                        <Columns>
                                <asp:BoundField DataField="DeliveryItemsChallanID" HeaderText="Challan ID" ReadOnly="True" SortExpression="DeliveryItemsChallanID" />
                                <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" SortExpression="ChallanDate" dataformatstring="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="IndentReference" HeaderText="Indent Ref" SortExpression="IndentReference" />
                                <asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate" dataformatstring="{0:dd/MM/yyyy}" />
            
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName" ItemStyle-CssClass="product" >
            
                                 <ItemStyle CssClass="product"></ItemStyle>
                                </asp:BoundField>
            
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"  />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" SortExpression="IssueHeadName" />
                                <asp:BoundField DataField="ChargeableHeadName" HeaderText="C H" SortExpression="ChargeableHeadName" />
                                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" SortExpression="TotalAmount" />
                            </Columns>
                        </asp:GridView>
                   </td>
            </tr>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate, DeliveryItemsChallan.IndentReference,
            DeliveryItemsChallan.IndentDate, DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,
            DeliveryItemsDetails.IssueHeadName, DeliveryItemsChallan.ChargeableHeadName, DeliveryItemsChallan.TotalAmount
            FROM DeliveryItemsChallan
            INNER JOIN DeliveryItemsDetails ON DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID order by itemname, challandate asc;"></asp:SqlDataSource>
            &nbsp;

</table>
</div>

</asp:Content>
