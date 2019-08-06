<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueList.ascx.cs" Inherits="IMS_PowerDept.UserControls.IssueList" %>
 <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
<script type="text/javascript">
    $(function () {
        //$("#ContentPlaceHolder1_tbStartDateSearch").datepicker();
        //$("#ContentPlaceHolder1_tbEndDateSearch").datepicker();
        $("#IssueList_tbStartDateSearch").datepicker();
        $("#IssueList_tbEndDateSearch").datepicker();

    });
</script>

 <asp:SqlDataSource ID="_sdscnmae" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan"></asp:SqlDataSource>
    <asp:SqlDataSource ID="_sdsdivname" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT DISTINCT IndentingDivisionName FROM DeliveryItemsChallan"></asp:SqlDataSource>
<div class="full_w">

				<div class="h_title">List Issued Entries</div>
<div style="margin:0px auto;padding:10px">
    <div class="element">
        <p>Select Non-Temporary /Temporary Issued Items</p>
         <asp:DropDownList CssClass="err" ID="_istemp" Width="250px" runat="server" 
                       AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="_istemp_SelectedIndexChanged" >
             <asp:ListItem Text="All"/>
                        <asp:ListItem Text="Regular Issued Items"/>
                        <asp:ListItem Text="Temporary Issued Items"/>
                    </asp:DropDownList>
    </div>

     <p class="element2">
            Search by Challan ID, Indent Reference

        </p>
 <p class="element2 input-group custom-search-form" style="width: 80%;">
            <span style="float: left; width: 55%">
                <input type="text" class="form-control" id="_txtsearch" runat="server" style="height: 22px; width:385px" placeholder="Search Keyword..." />
            </span>
            <span class="input-group-btn" style="float: left; height: 50px; width: 35px;">
                <button runat="server" id="btnSearchImage" onserverclick="btnSearchImage_ServerClick" class="btn btn-default" title="click to search" type="button" style="height: 36px; width: 35px; border-top-left-radius: 0px; border-bottom-left-radius: 0px;">
                    <i class="fa fa-search"></i>
                </button>
            </span>
    
            <span style="float: left; padding-left: 50px;">


                <asp:Button ID="btnAdvancedSearchFilters" CssClass="btn btn-default" Height="35px" runat="server" Text="Show Advanced Search Filters" OnClick="btnAdvancedSearchFilters_Click" />
            </span>
        </p>


       <div class="entry">
            <div class="sep">
               <br />
            </div>
        </div>

        <asp:Panel ID="panelAdvancedSearchFilters" Visible="false" runat="server">



            <div class="element2">

                <table >
                     <tr>
                        <td style="width:200px">
                    <label for="comments">Division Name </label>
                </td>
                        <td> <span style="float: left; padding-left: 10px;">

                    <asp:DropDownList CssClass="err" ID="_ddldivname" Width="250px" runat="server" 
                       AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="_sdsdivname" DataTextField="IndentingDivisionName" DataValueField="IndentingDivisionName" OnSelectedIndexChanged="_ddldivname_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="%"/>
                    </asp:DropDownList>
                </span></td><td></td>
                       
                    </tr>
                    <tr>
 <td style="width:250px"> 
                    <label for="comments">Chargeable Head</label>
                </td>
                        <td> <span style="float: left; padding-left: 10px;">

                    <asp:DropDownList CssClass="err" ID="ddlChargeableHead" Width="250px" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="_sdscnmae" 
                        DataTextField="ChargeableHeadName" DataValueField="ChargeableHeadName" OnSelectedIndexChanged="ddlChargeableHead_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="%"/>
                    </asp:DropDownList>
                </span></td><td></td>
                    </tr>
                    <tr>
                        <td ><span style="float: left;">
                    <label>Search between  dates </label>
                </span></td>
                        <td> <span style="float: left; padding-left: 10px;">
                    <asp:TextBox CssClass="form-control" ID="tbStartDateSearch" placeholder="mm/dd/yyyy" Width="220px" runat="server"></asp:TextBox>
                </span></td>
                        <td>  <span style="float: left; padding-left: 10px;">
                    <asp:TextBox CssClass="form-control" ID="tbEndDateSearch" placeholder="mm/dd/yyyy" Width="220px" runat="server"></asp:TextBox>
                </span></td>
                      
                    </tr>
                   <tr>
                       <td>
                           <asp:Panel ID="_pnlactions" runat="server">

                           </asp:Panel>
            
                       </td>

                       <td>
                           <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search Between Dates" OnClick="btnSearch_Click"/>
  <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="Button1_Click"/>

                       </td>
                       <td></td>
                   </tr>
                </table>
                <br />
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
       
                <tr>
                    <td>
                                 <asp:ListView ID="_rprt" runat="server">
             <ItemTemplate>
                 <tr>
                     <td>
                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID")%>'></asp:Label>
                     </td>
                     <td> <asp:Label ID="cHEad" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentDate")%>'></asp:Label></td>
                       <td>
                           <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
                     </td>
                     <td>
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                     </td>
                     <td>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IsDeliveredTemporary")%>'></asp:Label>
                     </td>
                      <td>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentReference")%>'></asp:Label>
                     </td>
                 </tr>
              
             </ItemTemplate>
         </asp:ListView>
                    </td>
                </tr>


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




