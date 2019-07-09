<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ItemsInventory.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ItemsInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="full_w">

				<div class="h_title">Item Inventory </div>
<div style="margin:0px auto;padding:10px">
     <p class="element2">
          Select an item to view its inventory details: 
        </p>
 <p class="element2 input-group custom-search-form" style="width: 80%;">
      <asp:SqlDataSource ID="dataSourceItems" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT Itemname,itemid FROM itemsinventory"></asp:SqlDataSource>
            <span style="float: left; width: 55%">
     
                <asp:DropDownList CssClass="form-control err"  ID="ddlItems" Width="385px" Height="35px" runat="server" 
                       AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="dataSourceItems" title="Only items received are shown here" DataTextField="itemname" DataValueField="itemid">
                        <asp:ListItem Text="--select item in inventory--" Value="00"/>
                    </asp:DropDownList> 
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
                        <td ><span style="float: left;">
                    <label>Search between  dates </label>
                </span></td>
                        <td> <span style="float: left; padding-left: 10px;">
                    <asp:TextBox CssClass="form-control" ID="tbStartDateSearch" TextMode="Date" Width="220px" runat="server"></asp:TextBox>
                </span></td>
                        <td>  <span style="float: left; padding-left: 10px;">
                    <asp:TextBox CssClass="form-control" ID="tbEndDateSearch"  TextMode="Date" Width="220px" runat="server" ></asp:TextBox>
                </span></td>
                      
                    </tr>
                   <tr>
                       <td>
  <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Advanced Search" />
            
                       </td>

                       <td>
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

         <asp:SqlDataSource ID="dataSourceItemsInventory" runat="server" 
             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="select itemid,issueheadname, grossbalance, temporaryissued, netactualbalance,minimumunitindicator from ItemsInventory  group by issueheadname, grossbalance, temporaryissued, netactualbalance, minimumunitindicator,itemid" 
             FilterExpression="itemid = {0}">              
             <FilterParameters>
                 <asp:ControlParameter Name="itemid" ControlID="ddlItems" PropertyName="SelectedValue" />                
             </FilterParameters>
         </asp:SqlDataSource>

         <asp:GridView ID="gvItemsInventory" Width="95%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="itemid"  DataSourceID="dataSourceItemsInventory">
             <Columns>
                 <asp:BoundField DataField="issueheadname" HeaderText="Issue Head" ReadOnly="True" SortExpression="issueheadname" />
                   <asp:BoundField DataField="grossbalance" HeaderText="Gross Balance" ReadOnly="True" SortExpression="grossbalance" />
                 <asp:BoundField DataField="temporaryissued" HeaderText="Temporary Issued" ReadOnly="True" SortExpression="issueheadname" />
                 <asp:BoundField DataField="netactualbalance" HeaderText="Net(Actual) Balance" ReadOnly="True" SortExpression="issueheadname" />               
                 <asp:TemplateField HeaderText="Minimum Units Indicator" ItemStyle-Width="15%">
                  <ItemTemplate >
                      <asp:TextBox ID="tbMinimumUnitsIndicator" Text='<%#DataBinder.Eval(Container, "DataItem.minimumunitindicator")%>'  TextMode="Number" CssClass="form-control" Width="50px" runat="server"></asp:TextBox>
                  </ItemTemplate>
                    </asp:TemplateField>
             </Columns>
              <HeaderStyle ForeColor="White" CssClass="sortasc sortdesc" />
                <SortedAscendingHeaderStyle CssClass="sortasc" />
      <SortedDescendingHeaderStyle CssClass="sortdesc" />
         </asp:GridView>

         </div>
    <p>
          <asp:Button ID="btnUpdateMinimumIndicator" CssClass="btn btn-primary" runat="server" Text="Update Minimum Units Indicator" />
            
                       
    </p>

</div>
        </div>
</asp:Content>
