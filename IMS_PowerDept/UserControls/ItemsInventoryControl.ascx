<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemsInventoryControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.ItemsInventoryControl" %>

<div class="full_w">

				<div class="h_title">Item Inventory </div>
<div style="margin:0px auto;padding:10px">
     <p class="element2">
          Select an item to view its inventory details: 
        </p>
 <p class="element2 input-group custom-search-form" style="width: 80%;">
      <asp:SqlDataSource ID="dataSourceItems" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT Distinct Itemname,itemid FROM ReceivedItemsDetails order by itemname asc"></asp:SqlDataSource>
            <span style="float: left; width: 55%">
     
                <asp:DropDownList CssClass="form-control err"  ID="ddlItems" Width="385px" Height="35px" runat="server" 
                       AppendDataBoundItems="true" AutoPostBack="true"  DataSourceID="dataSourceItems" title="Only items received are shown here" DataTextField="itemname" DataValueField="itemid" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                        <asp:ListItem Text="--select item in inventory--" Value="00"/>
                    </asp:DropDownList> 
            </span>

          <%--  <span style="float: left; padding-left: 50px;">


                <asp:Button ID="btnAdvancedSearchFilters" CssClass="btn btn-default" Height="35px" runat="server" Text="Show Advanced Search Filters" OnClick="btnAdvancedSearchFilters_Click" />
            </span>--%>
        </p>


       <div class="entry">
            <div class="sep">
               <br />
            </div>
        </div>

      
     <div style="width: 860px; overflow: auto;">

        <%-- <asp:SqlDataSource ID="dataSourceItemsInventory" runat="server" 
             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>"
              SelectCommand="SELECT ItemID, IssueHeadName, GrossBalance, TemporaryIssued, NetActualBalance, MinimumUnitIndicator, ItemsInventoryID FROM ItemsInventory GROUP BY IssueHeadName, GrossBalance, TemporaryIssued, NetActualBalance, MinimumUnitIndicator, ItemID, ItemsInventoryID" 
             FilterExpression="itemname = {0}"
              UpdateCommand="UPDATE ItemsInventory SET MinimumUnitIndicator = @MinimumUnitIndicator WHERE (ItemsInventoryID = @ItemsInventoryID)">              
             <FilterParameters>
                 <asp:ControlParameter Name="itemid" ControlID="ddlItems" PropertyName="SelectedValue" />                
             </FilterParameters>
             <UpdateParameters>
                 <asp:Parameter Name="MinimumUnitIndicator" />
                 <asp:Parameter Name="ItemsInventoryID" />
             </UpdateParameters>
         </asp:SqlDataSource>--%>

         <asp:GridView ID="gvItemsInventory" ShowFooter="True" 
             OnRowCommand="gvItemsInventory_RowCommand" OnRowDataBound="gvItemsInventory_RowDataBound" OnRowUpdating="gvItemsInventory_RowUpdating"
             Width="95%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" 
             AutoGenerateColumns="False"  >
              <%-- <asp:GridView ID="GridView1" Width="97%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  PageSize="20" OnDataBound="gvItemsInventory_DataBound1" OnPageIndexChanged="gvItemsInventory_PageIndexChanged" OnPageIndexChanging="gvItemsInventory_PageIndexChanging">--%>

             <Columns>
                
           <asp:TemplateField HeaderText="Sl."> <ItemTemplate>  <%# Container.DataItemIndex+1 %>   </ItemTemplate>  </asp:TemplateField>
          <asp:BoundField DataField="ItemName" HeaderText="Item Name"  />
          <asp:BoundField DataField="unit" HeaderText="Unit" ReadOnly="True"  />
          <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head"  />
                    <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity"  />
                    <asp:BoundField DataField="RegularIssuedQuantity" HeaderText="Regular Issued" />
                    <asp:BoundField DataField="TempIssuedQuantity" HeaderText="Temp Issued" />

                 <asp:BoundField DataField="GrossBalancecheck" HeaderText="Gross Balance"  ItemStyle-HorizontalAlign="right"/>
              
                 <asp:BoundField DataField="NetActualBalanceCheck" HeaderText="Actual Balance"  ItemStyle-HorizontalAlign="right"/>
              
                
             </Columns>
           <%--  <Columns>
            
                  
                  <asp:TemplateField HeaderText="Issue Head">
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.issueheadname")%>'></asp:Label>
                          </ItemTemplate>
                    
                 </asp:TemplateField>
                
                   <asp:BoundField DataField="grossbalancecheck" HeaderText="Gross Balance" ReadOnly="True" SortExpression="grossbalancecheck" />
                 <asp:BoundField DataField="temporaryissued" HeaderText="Temporary Issued" ReadOnly="True" SortExpression="issueheadname" />
                 <asp:BoundField DataField="netactualbalance" HeaderText="Net(Actual) Balance" ReadOnly="True" SortExpression="issueheadname" />               
                 <asp:TemplateField HeaderText="Minimum Units Indicator" ItemStyle-Width="22%">
                  <ItemTemplate >
                      <asp:TextBox ID="tbMinimumUnitsIndicator" Text='<%#DataBinder.Eval(Container, "DataItem.minimumunitindicator")%>'  CssClass="form-control" Width="50px" runat="server"></asp:TextBox>

                     <asp:HiddenField Value='<%#DataBinder.Eval(Container, "DataItem.ItemsInventoryID")%>' ID="hiddenitemid" runat="server" />
                  </ItemTemplate>
                   
<ItemStyle Width="22%"></ItemStyle>
                    
                    </asp:TemplateField>
               
                         
                       
                   
             </Columns>--%>
              <HeaderStyle ForeColor="White" CssClass="sortasc sortdesc" />
                <SortedAscendingHeaderStyle CssClass="sortasc" />
      <SortedDescendingHeaderStyle CssClass="sortdesc" />
         </asp:GridView>
           <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
         </div>
    <table>
        <tr>
            <td>
        
                <div id="divprintArea" visible="false" runat="server" >
                   <%-- <asp:Button ID="btnUpdateMinimumIndicator" CssClass="btn btn-primary" runat="server" Text="Update Minimum Units Indicator" OnClick="btnUpdateMinimumIndicator_Click" />--%>
    
                       &nbsp;<img src="../img/print.png" style="width:20px; height:20px;" />

                    <asp:HyperLink ID="hyperlinkPrint" Target="_blank"  runat="server">Print Selected Item Details</asp:HyperLink>
                
                    </div>
            </td>
          
        </tr>
    </table>
  

</div>
        </div>
