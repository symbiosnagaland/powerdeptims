<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt_Report_Excel.ascx.cs" Inherits="IMS_PowerDept.UserControls.Receipt_Report_Excel" %>
<link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    
    
    
    <style type="text/css">
        .ui-datepicker { font-size:8pt !important}       
    </style>


  <div class="full_w">

        <div class="h_title">List Received Entries</div>
        
        <asp:Panel ID="panelAdvancedSearchFilters"  runat="server">
            <div class="element2">

                <table>
                    
                   
                    
                    <!--Add by khyo -->
                   
                    
                    <!--Add by khyo -->
                    
                    <tr>
                       <td>
                            <label>Search between  dates </label>
                        </td>
                        
                        <td>

                            <span style="float: left; padding-left:10px;">
                                <asp:TextBox CssClass="form-control" ID="tbStartDateSearch" placeholder="dd-mm-yyyy" autocomplete="off" Width="100px" runat="server"></asp:TextBox>
                            </span>
                            
                            <span style="float: left; padding-left:5px;">
                                <asp:TextBox CssClass="form-control" ID="tbEndDateSearch" placeholder="dd-mm-yyyy" autocomplete="off" Width="100px" runat="server"></asp:TextBox>
                           
                            </span>
                           

                        </td>
                        
                        <td>  </td>
                        
                        <td>
                            <span style="float: left; text-align:left;">
                                <asp:Button ID="Btn_Search" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="Btn_Search_Click"/>
                                <asp:Button ID="Button4" CssClass="btn btn-warning" runat="server" Text="Cancel"/>
                            </span>
                        </td>
                    </tr>

                </table>

            </div>
        </asp:Panel>
        
        <div class="entry">
            <div class="sep">
                <br />
            </div>
        </div>

        <!--table-->
        <div style="overflow: auto;">
            
           <table class="table table-striped table-bordered table-hover"  style=" text-align:center;">              

               <tr>
                   <td>
                        <span style="float: right; text-align:right;">
                            <asp:Button ID="btnExportToExcel" CssClass="btn btn-primary" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />                               
                        </span>
                   </td>
                </tr>
               
               <tr>
                   <td>
                       <asp:GridView OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="25" ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size ="8pt" DataKeyNames="ReceivedItemsOTEOID">
                             
                            <Columns>
                                <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="OTEOID" ReadOnly="True" SortExpression="ReceivedItemsOTEOID" />
                                <asp:BoundField DataField="ReceivedItemOTEODate" HeaderText="OTEODate" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ReceivedItemOTEODate" />
                                <asp:BoundField DataField="SupplyOrderReference" HeaderText="Supply Order Ref. " SortExpression="SupplyOrderReference" />
                                <asp:BoundField DataField="SupplyOrderDate" HeaderText="Supply Order Date" ControlStyle-Width="102px" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SupplyOrderDate" />
                                <asp:BoundField DataField="Supplier" HeaderText="Supplier" SortExpression="Supplier" />
                                <asp:BoundField DataField="itemname" HeaderText="Item" SortExpression="itemname" />
                                <asp:BoundField DataField="Quantity" HeaderText="QTY." SortExpression="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" SortExpression="IssueHeadName" />
                                <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch. Head" SortExpression="ChargeableHeadName" />
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                <asp:BoundField DataField="unit" HeaderText="unit" SortExpression="unit" />
                            </Columns>

                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                            <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/>

                       </asp:GridView>
                        
                       <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select ReceivedItemsOTEO.ReceivedItemsOTEOID,ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate,Supplier, itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit from ReceivedItemsOTEO,ReceivedItemsDetails where (ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID) order by itemname,ReceivedItemsOTEOID"></asp:SqlDataSource>--%>

                   </td>
               </tr>

           </table>

        </div>
        <!--/table-->
    </div>
    <!--/full_w-->