<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Shared.Master" AutoEventWireup="true" CodeBehind="ItemEnquiry.aspx.cs" Inherits="IMS_PowerDept.Shared.ItemEnquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />

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

				<div class="h_title">Item Enquiry </div>
<div style="margin:0px auto;padding:10px">
     <h2>
        Selected Itemname : <asp:Label ID="lblItemName" runat="server"></asp:Label>
        </h2>
 


       <div class="entry">
            <div class="sep">
               <br />
            </div>
        </div>

      
     <div style="width: 860px; overflow: auto;">

        

         <asp:GridView ID="gvItemsInventory" ShowFooter="True" 
            
             Width="95%" GridLines="None" runat="server"
             AutoGenerateColumns="False"  >
             

             <Columns>
                
           <asp:TemplateField HeaderText="Sl."> <ItemTemplate>  <%# Container.DataItemIndex+1 %>   </ItemTemplate>  </asp:TemplateField>
          <asp:BoundField DataField="ItemName" HeaderText="Item Name"  />
          <asp:BoundField DataField="unit" HeaderText="Unit" ReadOnly="True"  />

          <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head"  />
                 <asp:BoundField DataField="Rate" HeaderText="Rate"  />
                    <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity"  />
                    <asp:BoundField DataField="RegularIssuedQuantity" HeaderText="Regular Issued" />
                    <asp:BoundField DataField="TempIssuedQuantity" HeaderText="Temp Issued" />

                 <asp:BoundField DataField="GrossBalancecheck" HeaderText="Gross Balance"  ItemStyle-HorizontalAlign="right"/>
              
                 <asp:BoundField DataField="NetActualBalanceCheck" HeaderText="Actual Balance"  ItemStyle-HorizontalAlign="right"/>
              
                
             </Columns>
           
              <HeaderStyle ForeColor="White" CssClass="sortasc sortdesc" />
                <SortedAscendingHeaderStyle CssClass="sortasc" />
      <SortedDescendingHeaderStyle CssClass="sortdesc" />
         </asp:GridView>
           <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
         </div>
    
  

</div>
        </div>
    <asp:HiddenField ID="h1" Value="1" runat="server" />
    <asp:HiddenField ID="h2" Value="2" runat="server" />
</asp:Content>
