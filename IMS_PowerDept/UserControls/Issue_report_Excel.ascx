<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Issue_report_Excel.ascx.cs" Inherits="IMS_PowerDept.UserControls.Issue_report_Excel" %>


 <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    
  
    
    <style type="text/css">
        .ui-datepicker { font-size:8pt !important}       
    </style>


  
    <div class="full_w">

        <div class="h_title">List Issued Entries</div>
        
        <asp:Panel ID="panelAdvancedSearchFilters"  runat="server">
            <div class="element2">

                <table>
                    
                    
                    
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
                                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click"/>
                                <asp:Button ID="Button4" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="btnClear_Click"/>
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
                                <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" Text="Export to excel"  OnClick="btnExportToExcel_Click" />
                               
                        </span>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView  OnPageIndexChanging="GridView1_PageIndexChanging" ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="DeliveryItemsChallanID" pagesize=25 Font-Size="8pt">
                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                            <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/> 
                        
                            <Columns>
                                    <asp:BoundField DataField="DeliveryItemsChallanID" HeaderText="ChallanID" ReadOnly="True" SortExpression="DeliveryItemsChallanID" />
                                    <asp:BoundField DataField="ChallanDate" HeaderText="ChallanDate" SortExpression="ChallanDate" />
                                    <asp:BoundField DataField="IndentReference" HeaderText="Indent Ref" SortExpression="IndentReference" />
                                    <asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate" />
            
                                    <asp:BoundField DataField="IndentingDivisionName" HeaderText="Division" SortExpression="IndentingDivisionName"/>            
                              
            
                                    <asp:BoundField DataField="ItemName" HeaderText="Item" SortExpression="ItemName" ItemStyle-CssClass="product" >
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" SortExpression="Quantity" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                    <asp:BoundField DataField="IssueHeadName" HeaderText="Issue HD" SortExpression="IssueHeadName" />
                                    <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch. Head" SortExpression="ChargeableHeadName" />
                                    <asp:BoundField DataField="UNIT" HeaderText="UNIT" SortExpression="UNIT" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total" ReadOnly="True" SortExpression="TotalAmount" />
                                </Columns>
                            </asp:GridView>
                            
                        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate, DeliveryItemsChallan.IndentReference,DeliveryItemsChallan.IndentDate, DeliveryItemsChallan.IndentingDivisionName, DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,DeliveryItemsDetails.IssueHeadName, DeliveryItemsChallan.ChargeableHeadName,DeliveryItemsDetails.UNIT,DeliveryItemsDetails.Rate*DeliveryItemsDetails.Quantity as TotalAmount FROM DeliveryItemsChallan INNER JOIN DeliveryItemsDetails ON DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID order by itemname, challandate asc"></asp:SqlDataSource>--%>
                    </td>
                </tr>

           </table>

        </div>
        <!--/table-->
    </div>
    <!--/full_w-->
        
