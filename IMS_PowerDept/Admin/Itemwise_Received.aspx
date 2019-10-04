<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Itemwise_Received.aspx.cs" Inherits="IMS_PowerDept.Admin.Itemwise_Received" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <div class="h_title">Items Received</div>
          <div style="width:910px; margin:auto;">
          <table class="table">
                <tr>
                  
                    <td>
                         <span style="float: right; text-align:right;">
                                <asp:Button ID="btnExportToExcel" CssClass="btn btn-primary" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />                               
                         </span>
                   </td>
                </tr>
                               
                           
              
                <tr>
                    <td>
   
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ReceivedItemsOTEOID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                             
                            <Columns>
                                <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="OTEO ID" ReadOnly="True" SortExpression="ReceivedItemsOTEOID" />
                                <asp:BoundField DataField="ReceivedItemOTEODate" HeaderText="OTEO Date" SortExpression="ReceivedItemOTEODate" dataformatstring="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="SupplyOrderReference" HeaderText="Supply Reference" SortExpression="SupplyOrderReference"  />
                                

                                <asp:BoundField DataField="SupplyOrderDate" HeaderText="Supply Order Date" SortExpression="SupplyOrderDate" dataformatstring="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="itemname" HeaderText="Item" SortExpression="itemname" ItemStyle-CssClass="product" >

                                <ItemStyle CssClass="product"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" SortExpression="IssueHeadName" />
                                <asp:BoundField DataField="ChargeableHeadName" HeaderText="C Head" SortExpression="ChargeableHeadName" />
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                            </Columns>

                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                             <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/>



                        </asp:GridView>
                      </td>
                 </tr>
    
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select ReceivedItemsOTEO.ReceivedItemsOTEOID,
                        ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate,
                        itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit
                         from ReceivedItemsOTEO,ReceivedItemsDetails where 
                        (ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID) order
                        by itemname,ReceivedItemsOTEOID"></asp:SqlDataSource>
            </table>
              </div>
          </div>
</asp:Content>
