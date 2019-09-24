<%@ Page Title="Issue Entry - View Details" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntriesList.aspx.cs" Inherits="IMS_PowerDept.Admin.IssueEntriesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%--  <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />

    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    
    
    <link rel="stylesheet" type="text/css" href="../js/sortingfile/jquery.dataTables.css"/>
    <link type="text/css" href="../js/sortingfile/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="jquery.datatables.yadcf.css" rel="stylesheet" />      
--%>
    <%--    <link href="../js/sortingfile/jquery.dataTables.css" rel="stylesheet" type="text/css" />--%>

    
    <!--New links for DataTable sorting and Calender-->

    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />

    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    
    
    <link rel="stylesheet" type="text/css" href="../js/sortingfile/jquery.dataTables.css"/>
    <link type="text/css" href="../js/sortingfile/jquery-ui.css" rel="stylesheet" />

    <link href="../css/jquery.dataTables.min.css" rel="stylesheet"/>   
    <link href="../css/dataTables.bootstrap.min.css" rel="stylesheet"/> 
    
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.bootstrap.min.js"></script>
  
		<script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/jquery.dataTables.js"></script>

    <script>
        $(document).ready(function () {

            var table = $('#example').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': -1
                }]
            });
        });
    </script>
    
    <script type="text/javascript">
        //  $(function () {
        //  $("#ContentPlaceHolder1_tbStartDateSearch").datepicker();
        //  $("#ContentPlaceHolder1_tbEndDateSearch").datepicker();
        // });

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
    
    <asp:SqlDataSource ID="_sdscnmae" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan"></asp:SqlDataSource>
    <asp:SqlDataSource ID="_sdsdivname" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT IndentingDivisionName FROM DeliveryItemsChallan"></asp:SqlDataSource>
    
    <div class="full_w" style="padding-bottom:20px;">
        <div class="h_title">List Issued Entries</div>
        
        <asp:Panel ID="panelAdvancedSearchFilters" Visible="true" runat="server">
            <div class="element2">
                
                <table>
                    
                    <tr>
                        <td style="width:200px">
                            <label for="comments">Select Non-Temporary /Temporary Issued Items</label>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px; width: 47%;">
                                <asp:DropDownList CssClass="err" ID="_istemp" Width="245px" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="_istemp_SelectedIndexChanged" >
                                    <asp:ListItem Text="All"/>
                                    <asp:ListItem Text="Regular Issued Items"/>
                                    <asp:ListItem Text="Temporary Issued Items"/>
                                </asp:DropDownList>
                            </span>
                        </td>
                        
                        <td>  </td>
                        <td>  </td>
                    </tr>
                    
                    <!--Add by khyo -->
                    <tr>                        
                        <th style="text-align:center; background-color:#fff; color: black;"></th>
                        <th style="background-color:#fff; color: black;">
                            <span style="text-align:center; margin-left:120px;">OR</span>

                        </th>                        
                    </tr>


                    <tr>
                        <td style="width:200px">
                            <label for="comments">Search by Challan ID, Indent Reference</label>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px; width: 47%;">
                                <input type="text" class="form-control" id="_txtsearch" runat="server" autocomplete ="off"  style="height: 22px; width:220px" placeholder="Search Keyword..." />
                            </span>
                            
                            <span class="input-group-btn" style="float: left; height: 50px; width: 55px; padding-left:45px;">
                                <button runat="server" id="Button2" onserverclick="btnSearchImage_ServerClick" class="btn btn-default" title="click to search" type="button" style="height: 36px; width: 35px; border-top-left-radius: 0px; border-bottom-left-radius: 0px;">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                        </td>
                        
                        <td>  </td>
                        <td>  </td>
                    </tr>
                    
                    <!--Add by khyo -->
                    <tr>                        
                        <th style="text-align:center; background-color:#fff; color: black;"></th>
                        <th style="background-color:#fff; color: black;">
                            <span style="text-align:center; margin-left:120px;">OR</span>

                        </th>                        
                    </tr>

                    <tr>
                        <td style="width:200px">
                            <label for="comments">Division Name </label>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px;">
                                <asp:DropDownList CssClass="err" ID="_ddldivname" Width="250px" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="_sdsdivname" DataTextField="IndentingDivisionName" DataValueField="IndentingDivisionName" OnSelectedIndexChanged="_ddldivname_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="%"/>
                                </asp:DropDownList>
                            </span>
                        </td>
                        
                        <td>  </td>
                        <td>  </td>
                    </tr>
                    
                    <!--Add by khyo -->
                    <tr>                        
                        <th style="text-align:center; background-color:#fff; color: black;"></th>
                        <th style="background-color:#fff; color: black;">
                            <span style="text-align:center; margin-left:120px;">OR</span>

                        </th>                        
                    </tr>
                    <tr>
                        <td style="width:250px">
                            <label for="comments">Chargeable Head</label>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px;">
                                <asp:DropDownList CssClass="err" ID="ddlChargeableHead" Width="250px" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="_sdscnmae" DataTextField="ChargeableHeadName" DataValueField="ChargeableHeadName" OnSelectedIndexChanged="ddlChargeableHead_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="%"/>
                                </asp:DropDownList>
                            </span>
                        </td>
                        
                        <td>  </td>
                        <td>  </td>
                    </tr>

                    <!--Add by khyo-->
                    <tr>                        
                        <th style="text-align:center; background-color:#fff; color: black;"></th>
                        <th style="background-color:#fff; color: black;">
                            <span style="text-align:center; margin-left:120px;">OR</span>

                        </th>                        
                    </tr>


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
                                <asp:Button ID="Button4" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="Button1_Click"/>
                            </span>
                        </td>
                    </tr>

                </table>

            </div>
        </asp:Panel>

    </div>
    <!--/full_w-->
    
    <div class="box clearfix">
        <div id="yadcf_example">
            
            <table border="0" class="table table-striped table-bordered table-hover" id="example">
                <thead>
                    <tr>
                        <th scope="col">Challan ID</th>
                        <th scope="col">Challan Date</th>
                        <th scope="col">Division</th>
                        <th scope="col">Chargeable Head</th>
                        <th style="text-align:center; color:white; z-index:1000; ">Actions</th>
                    </tr>
                </thead>
                
                <asp:ListView ID="_rprt" runat="server" CellPadding="4"  OnItemDeleting="_rprt_OnItemDeleting" OnItemCommand="_rprt_ItemCommand" AutoGenerateColumns="False"  PageSize="20">
                    <ItemTemplate>
                        
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID","{0:0}")%>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="cHEad" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChallanDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                            </td>
                            
                            <td>
                                <a href='<%# "IssuedEntryEdit.aspx?challanid="+Eval("DeliveryItemsChallanID") %>' class="table-icon edit" style="padding-left:20px;" title="Edit">Edit</a>
                                <a href='<%# "IssuedItemsDetails.aspx?Id="+Eval("DeliveryItemsChallanID","{0:0}") %>' target="_blank" class="table-icon archive" style="padding-left:20px;" title="View Details">View</a>
                                
                                <asp:LinkButton ID="lbtnDelete"  CommandArgument='<%# Eval("DeliveryItemsChallanID") %>' class="table-icon delete" style="padding-left:20px;" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:ListView>

            </table>
        
        </div>
    </div>
    <!--/box clearfix-->
    
    <%-- <script src="../js/sortingfile/jquery.min.js"></script>
    <script src="../js/sortingfile/jquery-ui.min.js"></script>
    <script type="text/javascript" charset="utf8" src="../js/sortingfile/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../js/sortingfile/jquery.dataTables.yadcf.js"></script>--%>  
    
        <script src="../js/sortingfile/jquery-ui.min.js"></script>

    <asp:SqlDataSource ID="mainsds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT * FROM [DeliveryItemsChallan] WHERE (([IndentingDivisionName] LIKE '%' + @IndentingDivisionName + '%') AND ([ChargeableHeadName] NOT LIKE '%' + @ChargeableHeadName + '%'))">
        <SelectParameters>
            <asp:ControlParameter ControlID="_ddldivname" Name="IndentingDivisionName" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="ddlChargeableHead" Name="ChargeableHeadName" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
