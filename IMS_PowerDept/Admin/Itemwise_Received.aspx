<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Itemwise_Received.aspx.cs" Inherits="IMS_PowerDept.Admin.Itemwise_Received" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>

        .product{
            width:120px;
        }
        .table{
            width:100%;
            margin: 0 auto;
            padding:0;
            overflow-x:auto;

        }
       
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="full_w" style="padding-bottom:20px;">
        <div class="h_title">Items Received</div>
         
               <div class="element2">
          <table class="table table-responsive">
                <tr>
                  
                    <td>
                         <span style="float: right; text-align:right;">
                                <asp:Button ID="btnExportToExcel" CssClass="btn btn-primary" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />                               
                         </span>
                   </td>
                </tr>
                               
                           
              
                <tr>
                    <td>
   
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ReceivedItemsOTEOID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" pagesize=25 Font-Size="8pt" >
                             
                            <Columns>
                                <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="OTEOID" ReadOnly="True" SortExpression="ReceivedItemsOTEOID" />
                                <asp:BoundField DataField="ReceivedItemOTEODate" HeaderText="OTEODate" SortExpression="ReceivedItemOTEODate" dataformatstring="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="SupplyOrderReference" HeaderText="Supply Order" SortExpression="SupplyOrderReference"  />
                                

                                <asp:BoundField DataField="SupplyOrderDate" HeaderText="Supply Order Date" SortExpression="SupplyOrderDate" dataformatstring="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Supplier" HeaderText="Supplier" SortExpression="Supplier" ItemStyle-CssClass="product" >

<ItemStyle CssClass="product"></ItemStyle>

                                </asp:BoundField>

                                <asp:BoundField DataField="itemname" HeaderText="Item" SortExpression="itemname" ItemStyle-CssClass="product">
                                <ItemStyle CssClass="product"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField DataField="Quantity" HeaderText="Qty." SortExpression="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" SortExpression="IssueHeadName" />
                                <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch. Head" SortExpression="ChargeableHeadName" />
                                <asp:BoundField DataField="amount" HeaderText="amount" SortExpression="amount" />
                            </Columns>

                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                             <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/>



                        </asp:GridView>
                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select ReceivedItemsOTEO.ReceivedItemsOTEOID,
                        ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate,Supplier,
                        itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit
                         from ReceivedItemsOTEO,ReceivedItemsDetails where 
                        (ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID) order
                        by itemname,ReceivedItemsOTEOID"></asp:SqlDataSource>
                        
                      </td>
                 </tr>
    
                       
            </table>
                   </div>
            
          </div>
</asp:Content>
