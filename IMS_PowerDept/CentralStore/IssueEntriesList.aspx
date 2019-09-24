﻿<%@ Page Title="Issue Entry List" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="IssueEntriesList.aspx.cs" Inherits="IMS_PowerDept.CentralStore.IssueEntry_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        
        <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
        <link href="../css/sb-admin.css" rel="stylesheet" />
        <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />

        <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>



       <!-- <script type="text/javascript">
            $(function () {
               // $("#ContentPlaceHolder1_tbStartDateSearch").datepicker();
               // $("#ContentPlaceHolder1_tbEndDateSearch").datepicker();
            });
        </script>  -->

    <script type="text/javascript">


        $(function () {
            $("#ContentPlaceHolder1_tbStartDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
            $("#ContentPlaceHolder1_tbEndDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
        });

    </script>
        <style type="text/css">
            .auto-style1 {
                height: 33px;
            }
        </style>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:SqlDataSource ID="_sdscnmae" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan"></asp:SqlDataSource>
   
     <asp:SqlDataSource ID="_sdsdivname" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT IndentingDivisionName FROM DeliveryItemsChallan"></asp:SqlDataSource>

        <div class="full_w">				
           <div class="h_title">List Issued Entries</div>
            




        <asp:Panel ID="panelAdvancedSearchFilters"  runat="server">



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
                                <input type="text" class="form-control" id="_txtsearch" runat="server" style="height: 22px; width:220px" placeholder="Search Keyword..." />
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
                                <asp:TextBox CssClass="form-control" ID="tbStartDateSearch" placeholder="dd/mm/yyyy" autocomplete="off" Width="100px" runat="server"></asp:TextBox>
                            </span>
                            
                            <span style="float: left; padding-left:5px;">
                                <asp:TextBox CssClass="form-control" ID="tbEndDateSearch" placeholder="dd/mm/yyyy" autocomplete="off" Width="100px" runat="server"></asp:TextBox>
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

          
            <div class="entry">
                <div class="sep"></div>
            </div>
        </asp:Panel>
      
     <div style="width: 860px; overflow: auto;">
            
          <table  class="table table-striped table-bordered table-hover" style="width:750px; " >
           <thead>
            <tr>
                <td>Challan ID</td>
             <td>IndentDate</td>
              <td>Division</td>
             <td>Chargeable Head</td>
                <td>Actions</td>
             </tr>
             </thead>
       
            <asp:ListView ID="_rprt" runat="server">
             <ItemTemplate>
                
                 <tr>
                     <td>
                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID")%>'></asp:Label>
                     </td>
                     <td> <asp:Label ID="cHEad" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentDate","{0:dd/MM/yyyy}")%>'></asp:Label></td>
                       <td>
                           <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
                     </td>
                     <td>
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                     </td>
                     <td>
                                                 
                           <a href='<%# "IssuedItemsDetails.aspx?Id="+Eval("DeliveryItemsChallanID") %>' target="_blank" class="table-icon archive" title="View Details"></a>
                     </td>
                 </tr>
              
             </ItemTemplate>
         </asp:ListView>
                </table>
         </div>

</div>
        </div>
                



<asp:SqlDataSource ID="mainsds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT * FROM [DeliveryItemsChallan] WHERE (([IndentingDivisionName] LIKE '%' + @IndentingDivisionName + '%') AND ([ChargeableHeadName] NOT LIKE '%' + @ChargeableHeadName + '%'))">
    <SelectParameters>
        <asp:ControlParameter ControlID="_ddldivname" Name="IndentingDivisionName" PropertyName="SelectedValue" Type="String" />
        <asp:ControlParameter ControlID="ddlChargeableHead" Name="ChargeableHeadName" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>



    
</asp:Content>
