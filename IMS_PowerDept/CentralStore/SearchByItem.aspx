<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchByItem.aspx.cs" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Shared/CentralStore_Master.Master" Inherits="IMS_PowerDept.CentralStore.SearchByItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    
    
      
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>

    <!--New links-->

    <link href="../css/components.css" rel="stylesheet" type="text/css"/>   
    <link href="../css/icons/icomoon/styles.css" rel="stylesheet" type="text/css"/>

    <script type="text/javascript" src="../js/core/jquery.min.js"></script>	
    <script type="text/javascript" src="../js/plugins/selects/select2.min.js"></script>
    <script type="text/javascript" src="../js/pages/form_select2.js"></script>
    
    <style type="text/css">
        .ui-datepicker { font-size:8pt !important}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:SqlDataSource ID="_sdscnmae" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan"></asp:SqlDataSource>
    <asp:SqlDataSource ID="_sdsdivname" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT itemname FROM [DeliveryItemsDetails]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dataSourceItemName" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT Distinct Itemname FROM ReceivedItemsDetails order by itemname asc"></asp:SqlDataSource>
    
    <div class="full_w" style="min-height:500px;">
        <div class="h_title">Item Received and Issued Lists</div>
        
        <div style="margin:0px auto;padding:10px">
            <h2>Search Received and Issued Lists by Item Name</h2>
            
            <div style="margin:0px auto;padding-left:15px">
                <%--  <asp:DropDownList CssClass="form-control err"  ID="ddlItemName" Width="385px" Height="35px" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="dataSourceItemName" title="Only items received are shown here" DataTextField="itemname"  OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                        <asp:ListItem Text="--select item name from inventory--" Value="00"/>
                      </asp:DropDownList>--%>
                
                <asp:DropDownList CssClass="form-control select-results-color"  ID="ddlItemName" Width="385px" Height="35px" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="dataSourceItemName" title="Only items received are shown here" DataTextField="itemname"  OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                    <asp:ListItem Text="--select item name from inventory--" Value="00"/>
                </asp:DropDownList>

            </div>
            
            <h3>
                <span style="float: left; padding-left:11px;">Item Received List</span>

                <span style="float:left; padding-left:266px;">
                    <asp:Label ID="Label1"  runat="server" Visible="false" Text="Total Received Quantity ="></asp:Label>
                    <asp:Label ID="LblTotal1" runat="server" Visible="true" Text="0"></asp:Label>
                </span>
            </h3>
            
            <div style="width: 860px; overflow: auto;">
                <asp:GridView ID="gvItemsReceived" Width="98%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  PageSize="20" OnDataBound="gvItemsReceived_DataBound1" OnPageIndexChanged="gvItemsReceived_PageIndexChanged" OnPageIndexChanging="gvItemsReceived_PageIndexChanging">
                    <Columns>
                        
                        <asp:BoundField DataField="ReceivedItemsOTEOID" ItemStyle-Width="150px" HeaderText="OTEO ID"  />
                        <asp:BoundField DataField="ReceivedItemOTEODate" ItemStyle-Width="190px" HeaderText="OTEO Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ChargeableHeadName" HeaderText="Chargeable Head" ReadOnly="True"  />
                        <asp:BoundField DataField="IssueHeadName" ItemStyle-Width="199px" HeaderText="Issue Head"  />
                        <asp:BoundField DataField="Unit" HeaderText="Unit"  />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px"> 
                            <ItemTemplate>
                                <a  href='<%# "ReceivedItemsDetails.aspx?Id="+Eval("ReceivedItemsOTEOID") %>' style="padding-left:20px;"  target="_blank" class="table-icon archive" title="View Details">View</a>  
                            </ItemTemplate>  
                        </asp:TemplateField>

                    </Columns>
                    
                    <HeaderStyle ForeColor="White"  CssClass="sortasc sortdesc" />
                    
                    <pagersettings mode="NumericFirstLast" firstpagetext="First" lastpagetext="Last" pagebuttoncount="5" Visible ="true" position="Bottom"/>
                    <SortedAscendingHeaderStyle CssClass="sortasc" />
                    <SortedDescendingHeaderStyle CssClass="sortdesc" />

                </asp:GridView>
            </div>
            
            <h3>
                <span style="float: left; padding-left:14px;">Item Issued List</span>
                
                <span style="float:left; padding-left:296px;">
                    <asp:Label ID="Label2" runat="server" Visible="false" Text="Total Issued Quantity = "></asp:Label>
                    <asp:Label ID="LblTotal2" runat="server" Visible="true" Text=" 0" ></asp:Label>
                </span>
            </h3>
            
            <div style="width: 860px; overflow: auto;">
                <asp:GridView ID="gvItemsIssued" Width="98%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  PageSize="20" OnDataBound="gvItemsIssued_DataBound1" OnPageIndexChanged="gvItemsIssued_PageIndexChanged" OnPageIndexChanging="gvItemsIssued_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="DeliveryItemsChallanID" ItemStyle-Width="150px" HeaderText="Challan ID"  />
                        <asp:BoundField DataField="IndentDate" ItemStyle-Width="190px" HeaderText="Indent Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ChargeableHeadName" ItemStyle-Width="199px" HeaderText="Chargeable Head"  />
                        <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" ReadOnly="True"  />
                        <asp:BoundField DataField="Unit" HeaderText="Unit"  />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px"> 
                            <ItemTemplate>  
                                <a href='<%# "IssuedItemsDetails.aspx?Id="+Eval("DeliveryItemsChallanID") %>' target="_blank" class="table-icon archive" style="padding-left:20px;" title="View Details">View</a> 
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    
                    <HeaderStyle ForeColor="White" CssClass="sortasc sortdesc" />
                    
                    <pagersettings mode="NumericFirstLast" firstpagetext="First" lastpagetext="Last" pagebuttoncount="5" Visible ="true" position="Bottom"/>
                    <SortedAscendingHeaderStyle CssClass="sortasc" />
                    <SortedDescendingHeaderStyle CssClass="sortdesc" />

                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
