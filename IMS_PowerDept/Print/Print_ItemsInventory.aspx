<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_ItemsInventory.aspx.cs" Inherits="IMS_PowerDept.Print.Print_ItemsInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        table, td {
        text-align: left;
        }
        tr td:last-child {
        text-align: left;
        }
        tr th:last-child {
        text-align: left;
        }
        tr th:nth-child(5) {
        text-align: left;
        }
        tr td:nth-child(5) {
        text-align: left;
        }
    </style>
</head>
<body onload="javascript:window.print()">

    <form id="form1" runat="server">
          <div style="padding:10px; margin:0px auto; width: 913px;">
            <div style="text-align:center;">
            <p style="margin:5px;">
                GOVERNMENT OF NAGALAND</p>
             <p style="margin:5px;">
&nbsp;DEPARTMENT OF POWER : ELECTRICAL STORE DIVISION
            </p>
               <p style="margin:5px;">
                DIMAPUR : NAGALAND</p>
        </div>
            <hr />
            <h2 style="text-align:center; margin:0px">Items Inventory</h2>

                   <h2 style="text-align:center; margin:2px">

                       <asp:Label ID="lblFromDate"  runat="server" Visible="false" ></asp:Label>
                      <asp:Label ID="lbltoDate" runat="server" Visible="false" ></asp:Label>
                   </h2>

              <div style="margin:0 auto; padding:10px; width: 820px;">
<asp:GridView Width="100%" ID="GridView2" HorizontalAlign="Center" GridLines="None"  AutoGenerateColumns="false" runat="server" CellPadding="5">

             <Columns>
                
         <asp:TemplateField HeaderText="Sl." HeaderStyle-HorizontalAlign="left"> <ItemStyle HorizontalAlign="Left" />
 <ItemTemplate>  <%# Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
        
          <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-HorizontalAlign="Left"  SortExpression="ItemName" />
          <asp:BoundField DataField="unit" HeaderText="Unit" ItemStyle-HorizontalAlign="Left"  ReadOnly="True" SortExpression="unit" />
          <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="133px"  SortExpression="IssueHeadName" />
                    <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Left" SortExpression="ReceivedQuantity" />
                    <asp:BoundField DataField="RegularIssuedQuantity" HeaderText="Regular Issued" ItemStyle-HorizontalAlign="Left" SortExpression="RegularIssuedQuantity" />
                    <asp:BoundField DataField="TempIssuedQuantity" HeaderText="Temp Issued" ItemStyle-HorizontalAlign="Left" SortExpression="TempIssuedQuantity" />

                 <asp:BoundField DataField="GrossBalancecheck" HeaderText="Gross Balance" HeaderStyle-HorizontalAlign="left" SortExpression="GrossBalancecheck" ItemStyle-HorizontalAlign="Left"/>
              
                 <asp:BoundField DataField="NetActualBalanceCheck" HeaderText="Net Balance"  HeaderStyle-HorizontalAlign="left"  SortExpression="NetActualBalanceCheck" ItemStyle-HorizontalAlign="Left"/>
                           
                
             </Columns>
              <HeaderStyle  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                  </asp:GridView>




<asp:GridView Width="100%" ID="GridView1" HorizontalAlign="Center" GridLines="None"  AutoGenerateColumns="false" runat="server" CellPadding="5">

             <Columns>
                
           <asp:TemplateField HeaderText="Sl."> <ItemTemplate>  <%# Container.DataItemIndex+1 %>      </ItemTemplate>  </asp:TemplateField>
          <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName" ItemStyle-HorizontalAlign="Left" />
          <asp:BoundField DataField="unit" HeaderText="Unit" ReadOnly="True" SortExpression="unit" ItemStyle-HorizontalAlign="Left" />
          <asp:BoundField DataField="IssueHeadName" HeaderText="Issue Head" SortExpression="IssueHeadName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="133px"  />
                    <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" SortExpression="ReceivedQuantity" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="RegularIssuedQuantity" HeaderText="Regular Issued" SortExpression="RegularIssuedQuantity" ItemStyle-HorizontalAlign="Left" />
                    
                 <asp:BoundField DataField="GrossBalancecheck" HeaderText="Gross Balance" SortExpression="GrossBalancecheck" ItemStyle-HorizontalAlign="Left"/>
             </Columns>

              <HeaderStyle  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                  </asp:GridView>

                    
              </div>
    
    </div>
    </form>
</body>
</html>
