﻿<%@ Page Title="Received Items List" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ReceivedEntriesList.aspx.cs" Inherits="IMS_PowerDept.Admin.ReceivedEntriesList" %>

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
                                <button runat="server" id="btnSearchImage" class="btn btn-default" title="click to search" onserverclick="btnSearchImage_ServerClick" type="button"  style="height: 36px; width:30px; border-top-left-radius: 0px; border-bottom-left-radius: 0px;"><i class="fa fa-search"></i></button>
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


                    <tr>
                        <td>
                            <span style="float: left;"><label for="comments">Issue Head </label></span>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px;">
                                <asp:DropDownList CssClass="err" AppendDataBoundItems="true" AutoPostBack="true" ID="ddlIssueHead" Width="250px" runat="server" DataSourceID="sdsihead" DataTextField="IssueHeadName" DataValueField="IssueHeadName" OnSelectedIndexChanged="ddlIssueHead_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="%"/>
                                </asp:DropDownList>
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

                    <tr>
                        <td>
                            <span style="float: left;"><label for="comments">Chargeable Head</label></span>
                        </td>
                        
                        <td>
                            <span style="float: left; padding-left: 10px;">
                                <asp:DropDownList CssClass="err" ID="ddlChargeableHead" AppendDataBoundItems="true" AutoPostBack="true" Width="250px" runat="server" DataSourceID="sdschead" DataTextField="ChargeableHeadName" DataValueField="ChargeableHeadName" OnSelectedIndexChanged="ddlChargeableHead_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="%"/>
                                </asp:DropDownList>
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
                            <br />

                             &nbsp;<br /> &nbsp; sort by <asp:RadioButtonList ID="rblOrderBy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                 <asp:ListItem Selected="True"  Value="ReceivedItemOTEODate">OTEO Date&nbsp;</asp:ListItem>
                                 
                                   <asp:ListItem Value="ReceivedItemsOTEOID">OTEO ID &nbsp;</asp:ListItem>
                             
                            </asp:RadioButtonList>

                            &nbsp;&nbsp;
                             <asp:RadioButtonList ID="rblAscOrDesc" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="ASC">Ascending &nbsp;</asp:ListItem>
                                <asp:ListItem Value="DESC">Descending</asp:ListItem>
                            </asp:RadioButtonList>

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
        
        <div class="entry">
            <div class="sep">
                <br />
            </div>
        </div>

        <!--table-->
        <div style="overflow: auto;">
            
            <table  class="table table-striped table-bordered table-hover" style="width:790px; text-align:center;">
                
                <thead>                    
                    <tr>
                        <th scope="col"  style="text-align:center;">OTEO ID</th>
                        <th scope="col" style="text-align:center;">OTEO Date</th>             
                        <th scope="col" style="text-align:center;">Chargeable Head</th>
                        <th scope="col" style="text-align:center;">Issue Head</th>
                        <th scope="col" style="text-align:center;">Total Amount</th>
                        <th scope="col" style="text-align:center;">Actions</th>
                    </tr>
                </thead>
                
                <tbody>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        
                        <Triggers>
                            <asp:PostBackTrigger ControlID="_rprt" />
                        </Triggers>
                        
                        <ContentTemplate>
                            <asp:ListView ID="_rprt" runat="server" autopostback="true" OnItemDeleting="_rprt_OnItemDeleting" OnItemCommand="_rprt_ItemCommand" ItemPlaceholderID="itemPlaceHolder1">
                                <EmptyDataTemplate>
                                    <h3 style="padding-left:20px; padding-top:10px; color:orange;">No Records Found. Try Different Keywords.</h3>
                                </EmptyDataTemplate>
                                
                                <ItemTemplate>
                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceivedItemsOTEOID")%>'></asp:Label>
                                        </td>

                                        <td> 
                                            <asp:Label ID="cHEad" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ReceivedItemOTEODate")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                        </td>

                                        <td>
                                           <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.TotalAmount")%>'></asp:Label>
                                        </td>
                                        
                                        <td>
                                            <a href='<%# "ReceivedItemsEdit.aspx?oteoid="+Eval("ReceivedItemsOTEOID") %>' class="table-icon edit" style="padding-left:20px;" title="Edit">Edit</a>
                                            <a  href='<%# "ReceivedItemsDetails.aspx?Id="+Eval("ReceivedItemsOTEOID") %>' style="padding-left:20px;"  target="_blank" class="table-icon archive" title="View Details">View</a>
                                            
                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Eval("ReceivedItemsOTEOID") %>' CommandName="Delete" class="table-icon delete" style="padding-left:20px;" runat="server">Delete</asp:LinkButton>
                                        </td>

                                    </tr>

                                </ItemTemplate>

                            </asp:ListView>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </tbody>

            </table>

        </div>
        <!--/table-->
    </div>
    <!--/full_w-->
        
</asp:Content>
