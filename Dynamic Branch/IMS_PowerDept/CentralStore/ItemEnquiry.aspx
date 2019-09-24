<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="ItemEnquiry.aspx.cs" Inherits="IMS_PowerDept.CentralStore.ItemEnquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
     <script type="text/javascript">
         function CheckSingleCheckbox(ob) {
             var grid = ob.parentNode.parentNode.parentNode;
             var inputs = grid.getElementsByTagName("input");
             for (var i = 0; i < inputs.length; i++) {
                 if (inputs[i].type == "checkbox") {
                     if (ob.checked && inputs[i] != ob && inputs[i].checked) {
                         inputs[i].checked = false;
                     }
                 }
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="full_w">

				<div class="h_title">Item Enquiry :
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </div>
<div style="margin:0px auto;padding:10px">
 


       

      
     <div style="width: 860px; overflow: auto;">

         <asp:SqlDataSource ID="dataSourceItemsInventory" runat="server" 
             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="select itemid,issueheadname, grossbalance, temporaryissued, netactualbalance,minimumunitindicator from ItemsInventory  group by issueheadname, grossbalance, temporaryissued, netactualbalance, minimumunitindicator,itemid" 
             FilterExpression="itemid = {0}">              
             <FilterParameters>
                 <asp:QueryStringParameter Name="itemid" QueryStringField="item" />                
             </FilterParameters>
         </asp:SqlDataSource>

         <br />

         <asp:GridView ID="gvItemsInventory" Width="95%" GridLines="None" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="itemid"  DataSourceID="dataSourceItemsInventory">
             <Columns>
          
                  
                  <asp:TemplateField HeaderText="Issue Head">
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.issueheadname")%>'></asp:Label>
                          </ItemTemplate>
                    
                 </asp:TemplateField>
                
                   <asp:BoundField DataField="grossbalance" HeaderText="Gross Balance" ReadOnly="True" SortExpression="grossbalance" />
                 <asp:BoundField DataField="temporaryissued" HeaderText="Temporary Issued" ReadOnly="True" SortExpression="issueheadname" />
                 <asp:BoundField DataField="netactualbalance" HeaderText="Net(Actual) Balance" ReadOnly="True" SortExpression="issueheadname" />               
             
  
             </Columns>
              <HeaderStyle ForeColor="White" CssClass="sortasc sortdesc" />
                <SortedAscendingHeaderStyle CssClass="sortasc" />
      <SortedDescendingHeaderStyle CssClass="sortdesc" />
         </asp:GridView>
           <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
         </div>
 

</div>
        </div>
</asp:Content>
