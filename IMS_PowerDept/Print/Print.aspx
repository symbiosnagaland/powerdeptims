<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="IMS_PowerDept.Print.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            margin:0px;font-size:14px;
        }
        .pclass
        {
            margin:0px;
        }
      
        .auto-style1 {
            vertical-align: bottom;
            width: 262px;
        }
        .auto-style2 {
            height: 11px;
        }
        .auto-style3 {
            vertical-align: bottom;
            width: 331px;
        }
      
    </style>

</head>
<body onload="javascript:window.print()">

    <form id="form1" runat="server">

          <asp:SqlDataSource ID="_sdsChallan" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [DeliveryItemsChallan] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)">
        <SelectParameters>
            <asp:QueryStringParameter Name="DeliveryItemsChallanID" QueryStringField="Id" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>

        <div style="padding:5px; margin:0px auto; width: 913px;">
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
            <h2 style="text-align:center; margin:0px">Delivery Challan</h2>


            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="_sdsChallan">
                <ItemTemplate>
                  <span style="float:right; font-weight:bold">Challan No/Date : 

            <asp:Label ID="cno" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID","{0:0}")%>'></asp:Label> |
             <asp:Label ID="cdate" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChallanDate","{0:dd/MM/yyyy}")%>'></asp:Label> 
        </span><br />
        <hr />
        To<br />
        <span style="font-weight:bold;padding-left:30px">
            <asp:Label ID="divisionname" Font-Size="20px" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
        </span>
            <br />
           <br />
             <span style="padding-left:30px;  margin:0px auto">
                 Please find original and duplicate copy of the Delivery Challan for the issue of stores as per your
                    Indent No <asp:Label ID="indentNo" Font-Underline="true" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentReference")%>'></asp:Label> 
                    &nbsp;dated <asp:Label ID="idate" Font-Underline="true" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentDate","{0:dd/MM/yyyy}")%>'></asp:Label> 
                 &nbsp;against the name of 
                 work/account <asp:Label ID="ihead" Font-Bold="true" Font-Underline="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label> 
                 &nbsp;.The duplicate copy of the Challan duly signed by you may please be returned within fifteen days

                 </span>
                </ItemTemplate>
            </asp:Repeater>
       <br />
            <br />
            <asp:SqlDataSource ID="sdsitem" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [DeliveryItemsDetails] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="DeliveryItemsChallanID" QueryStringField="Id" Type="Decimal" />
            </SelectParameters>
        </asp:SqlDataSource>

            <asp:GridView ID="gvitems" Width="95%" runat="server" AutoGenerateColumns="False" DataKeyNames="DeliveryItemDetailsID" DataSourceID="sdsitem" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowHeaderWhenEmpty="True">
                <Columns>

                         <asp:TemplateField HeaderText="Sl."> 
                   <ItemTemplate>  <%# Container.DataItemIndex+1 %>.     </ItemTemplate>  
                     <ItemStyle Width="5%" HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                   </asp:TemplateField>


                    <asp:BoundField DataField="DeliveryItemDetailsID" Visible="false" HeaderText="DeliveryItemDetailsID" InsertVisible="False" ReadOnly="True" SortExpression="DeliveryItemDetailsID" />
                    <asp:BoundField DataField="DeliveryItemsChallanID" Visible="false" HeaderText="DeliveryItemsChallanID" SortExpression="DeliveryItemsChallanID" />
                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                   
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                    <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                    <asp:BoundField DataField="Rate" Visible="false" HeaderText="Rate" SortExpression="Rate" />
                     <asp:BoundField DataField="IssueHeadName"  HeaderText="I Head" SortExpression="IssueHeadName" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

        </div>
        <br />
         <div style="padding:10px; margin:0px auto; width: 885px;">
             <table style="width:875px; height: 300px; vertical-align:bottom">
                 <tr>
                     <td class="auto-style1">Issued By </td>
                     <td class="auto-style3">Checked and Recorded By</td>
                     <td>Recieved in Full and Good condition</td>
                 </tr>
                 <tr>
                     
                 </tr>

                  <tr>
                     <td class="auto-style1">Store Keeper </td>
                     <td class="auto-style3">Junior Engineer</td>
                     <td class="auto-style3">
                         <asp:Repeater ID="Repeater2" runat="server" DataSourceID="_sdsChallan">
                <ItemTemplate>
                               <p>Recievers Signature : </p>                       
                      <p>   Designation : <asp:Label ID="lblReceiverDesignation" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceiverDesignation")%>' ></asp:Label> </p>


                        <p> Vehicle No. : <asp:Label ID="lblVehicleNo" runat="server"  Text='<%#DataBinder.Eval(Container, "DataItem.VehicleNumber")%>'></asp:Label></p>
                   
                </ItemTemplate>
            </asp:Repeater>
             </td>
                 </tr>
             </table>
             </div>
    </form>
</body>
</html>
