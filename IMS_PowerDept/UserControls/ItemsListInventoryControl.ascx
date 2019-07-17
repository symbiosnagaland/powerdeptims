<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemsListInventoryControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.ItemsListInventoryControl" %>

  <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>



    <script type="text/javascript">
        $(function ()
        {
            $("#ContentPlaceHolder1_ItemsListInventoryControl_tbDate").datepicker();

        });
    </script>


    <style type="text/css">

        .ui-datepicker { font-size:8pt !important}

    </style>

     <div class="full_w" style="width: 900px;">

				<div class="h_title" style="margin-bottom: 30px;">Items Inventory
                    <br /><br />
                    &nbsp&nbsp&nbsp<asp:Label ID="lbl_search" runat="server" Text="Search by Issue Head Name" style="color:#777777;font-size: 13px;font-weight: bold; text-shadow: none;"></asp:Label>
                    <asp:DropDownList ID="IssueHeadList" runat="server" style="height:25px; Width:125px; margin-top: 7px;"  DataTextField="IssueHeadName" DataValueField="IssueHeadName" AppendDataBoundItems="True" DataSourceID="IssueHeadListdatasource"  >
                      
                        <asp:ListItem Value="All" Selected="True"  >All</asp:ListItem>
    
                    </asp:DropDownList>
                  <asp:Button ID="_btnSelectedIssueHead"  runat="server" OnClick="_btnSelectedIssueHead_Click" Height="25px" Width="170px" Text="View Latest Inventory List"  />
                    <asp:SqlDataSource ID="IssueHeadListdatasource" runat="server" 
             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select IssueHeadName from IssueHeads" >  
         </asp:SqlDataSource>

                    
                </div>
         <br />
                    <table>
                    <tr>
                    <td> <span style="float: left;">
                        <label>Stock Position Date</label></span></td>

                    <td> 
                        <span style="float: left; padding-left: 10px;">
                            <asp:TextBox CssClass="form-control" ID="tbDate" autocomplete="off"  placeholder="Date" Width="155px" runat="server" OnTextChanged="tbDate_TextChanged"></asp:TextBox>
                        </span>
                     <%--    <span style="float: left; padding-left: 5px;">
                            <asp:TextBox CssClass="form-control" ID="tbEndDateSearch" placeholder="To Date" Width="155px" runat="server"></asp:TextBox>
                        </span>--%>
                         
                    </td>
                     

                    <td> 
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click"  />
                       
                    </td>

                </tr>
                        </table>

 
                   
<div style="margin:0px auto;padding:10px">
      
     <div style="overflow: auto;">
          
         <asp:GridView ID="gvItemsInventory" Width="99%" GridLines="None" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False"  PageSize="40" OnDataBound="gvItemsInventory_DataBound1" OnPageIndexChanged="gvItemsInventory_PageIndexChanged" OnPageIndexChanging="gvItemsInventory_PageIndexChanging">

             <Columns>
                
           <asp:TemplateField HeaderText="Sl."> <ItemTemplate>  <%# Container.DataItemIndex+1 %>      </ItemTemplate>  </asp:TemplateField>
          <asp:BoundField DataField="ItemName" ItemStyle-Width="250px" HeaderText="Item Name"  />
          <asp:BoundField DataField="unit" HeaderText="Unit" ReadOnly="True"  />
          <asp:BoundField DataField="IssueHeadName" ItemStyle-Width="99px" HeaderText="Issue Head"  />
                    <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity"  />
                    <asp:BoundField DataField="RegularIssuedQuantity" HeaderText="Regular Issued" />
                    <asp:BoundField DataField="TempIssuedQuantity" HeaderText="Temp Issued" />

                 <asp:BoundField DataField="GrossBalancecheck" HeaderText="Gross Balance"  ItemStyle-HorizontalAlign="right"/>
              
                 <asp:BoundField DataField="NetActualBalanceCheck" HeaderText="Actual Balance"  ItemStyle-HorizontalAlign="right"/>
              
                
             </Columns>
              <HeaderStyle ForeColor="White"  CssClass="sortasc sortdesc" />
         
            
             
               
             <pagersettings mode="NumericFirstLast" firstpagetext="1" lastpagetext="Last" pagebuttoncount="30" Visible ="true" position="Bottom"/> 
                <SortedAscendingHeaderStyle CssClass="sortasc" />
      <SortedDescendingHeaderStyle CssClass="sortdesc" />             
         </asp:GridView>

         <div style="padding-left: 39px; margin-top:10px;">
         <asp:Label ID="Message" ForeColor="Green" Font-Bold="true" runat="server" Font-Size="Small" ></asp:Label> 
         </div>

    <table>
        <tr>
            <td>
 <asp:Button ID="btn1" CssClass="btn btn-primary" runat="server" OnClick="btn1_Click" Text="Print Gross Balance" />
          
            </td>
            <td>
 <asp:Button ID="btn2" CssClass="btn btn-primary" runat="server" OnClick="btn2_Click" Text="Print All"  />
            
            </td>
        </tr>
    </table>

</div>

</div>
        </div>