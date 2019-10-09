<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ITEM_WISEISSUED.aspx.cs" Inherits="IMS_PowerDept.Admin.ITEM_WISEISSUED" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    
    <script type="text/javascript">

        $(function () {
            $("#ContentPlaceHolder1_tbStartDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
            $("#ContentPlaceHolder1_tbEndDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
        });

    </script>
    
    <style type="text/css">
        .ui-datepicker { font-size:8pt !important}       
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="sdschead" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT ChargeableHeadName FROM ReceivedItemsOTEO"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsihead" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT IssueHeadName FROM ReceivedItemsOTEO"></asp:SqlDataSource>
    
    <div class="full_w">

        <div class="h_title">List Received Entries</div>
        
        <asp:Panel ID="panelAdvancedSearchFilters"  runat="server">
            <div class="element2">

                <table>
                    
                    <tr>
                        <td style="width:200px">
                            <label for="comments"> Search by OTEO No., Supply Order No. or Supplier</label>
                        </td>
                        
                        <td>
                            <span style="float: left; width: 55%; padding-left:10px;">
                                <input type="text" class="form-control" id="etsearch" runat="server" autocomplete ="off" style="height: 22px; width:220px" placeholder="Search Keyword..." />
                            </span>
                            
                            <span class="input-group-btn" style="float: left; height: 50px; width:10px; padding-left:2px;">
                                <%--<button runat="server" id="btnSearchImage" class="btn btn-default" title="click to search" onserverclick="btnSearchImage_ServerClick" type="button"  style="height: 36px; width:30px; border-top-left-radius: 0px; border-bottom-left-radius: 0px;"><i class="fa fa-search"></i></button>--%>
                            </span>
                        </td>
                        
                        <td></td>
                    </tr>
                    
                    <!--Add by khyo -->
                    <tr>                        
                        <th style="text-align:center; background-color:#fff; color: black;"></th>
                        <th style="background-color:#fff; color: black;">
                            <span style="text-align:center; margin-left:120px;">OR</span>

                        </th>                        
                    </tr>
                    
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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="DeliveryItemsChallanID" DataSourceID="SqlDataSource1" pagesize=25 Font-Size="8pt">
                            <pagersettings mode="Numeric" position="Bottom" PageButtonCount="20"/>
                            <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="left"/> 
                        
                            <Columns>
                                    <asp:BoundField DataField="DeliveryItemsChallanID" HeaderText="ChallanID" ReadOnly="True" SortExpression="DeliveryItemsChallanID" />
                                    <asp:BoundField DataField="ChallanDate" HeaderText="ChallanDate" SortExpression="ChallanDate" dataformatstring="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="IndentReference" HeaderText="IndentRef" SortExpression="IndentReference" />
                                    <asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate" dataformatstring="{0:dd/MM/yyyy}" />
            
                                    <asp:BoundField DataField="IndentingDivisionName" HeaderText="Division" SortExpression="IndentingDivisionName"/>            
                              
            
                                    <asp:BoundField DataField="ItemName" HeaderText="Item" SortExpression="ItemName" ItemStyle-CssClass="product" >
                                        <ItemStyle CssClass="product"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                    <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHead" SortExpression="IssueHeadName" />
                                    <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch. Head" SortExpression="ChargeableHeadName" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Amount" SortExpression="TotalAmount" />
                                </Columns>
                            </asp:GridView>
                            
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate, DeliveryItemsChallan.IndentReference,DeliveryItemsChallan.IndentDate, DeliveryItemsChallan.IndentingDivisionName, DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,DeliveryItemsDetails.IssueHeadName, DeliveryItemsChallan.ChargeableHeadName,DeliveryItemsDetails.Rate*DeliveryItemsDetails.Quantity as TotalAmount FROM DeliveryItemsChallan INNER JOIN DeliveryItemsDetails ON DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID order by itemname, challandate asc"></asp:SqlDataSource>
                    </td>
                </tr>

           </table>

        </div>
        <!--/table-->
    </div>
    <!--/full_w-->
        
</asp:Content>