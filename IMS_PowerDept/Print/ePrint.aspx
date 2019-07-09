<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ePrint.aspx.cs" Inherits="IMS_PowerDept.Print.ePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        @media print
        {
            input
            {
                display: none;
            }
        }
    </style>
    <title></title>
</head>
<body onload="javascript:window.print()">

    <form id="form1" runat="server">
          <asp:SqlDataSource ID="_sdsoteo" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ReceivedItemsOTEO] WHERE ([ReceivedItemsOTEOID] = @ReceivedItemsOTEOID)">
        <SelectParameters>
            <asp:QueryStringParameter Name="ReceivedItemsOTEOID" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsreceivedetails" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ReceivedItemsDetails] WHERE ([ReceivedItemsOTEOID] = @ReceivedItemsOTEOID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="ReceivedItemsOTEOID" QueryStringField="Id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

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
            <h2 style="text-align:center; margin:0px">O.T.E.O</h2>


        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="_sdsoteo" >  
            <ItemTemplate>
                 <span style="float:right; font-weight:bold">OTEO No/Date : 

            <asp:Label ID="cno" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceivedItemsOTEOID")%>'></asp:Label> |
             <asp:Label ID="cdate" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceivedItemOTEODate","{0:yyyy/MM/dd}")%>'></asp:Label> 
        </span><br />

        <hr />
        <span style="margin:8px;font-weight:bold;padding-left:30px">
           S Order Ref./Date : <asp:Label ID="divisionname" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SupplyOrderReference")%>'></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" Font-Bold="true" Font-Size="18px"  runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SupplyOrderDate","{0:yyyy/MM/dd}")%>'></asp:Label>
        </span>
        <br />  
        <span style="margin:8px;font-weight:bold;padding-left:30px">
           Supplier : <asp:Label ID="Label2" Font-Size="18px"  Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Supplier")%>'></asp:Label>
        </span>
       <br />  
        <span style="margin:8px;font-weight:bold;padding-left:30px">
           Chargeable Head : <asp:Label ID="Label3" Font-Size="18px" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
        </span>
        <br />  
        <span style="margin:8px;font-weight:bold;padding-left:30px">
           Issue Head : <asp:Label ID="Label4" Font-Size="18px"  Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
        </span>
            </ItemTemplate>
        </asp:Repeater>
  
            <br />
           <br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="ReceivedItemID" DataSourceID="sdsreceivedetails" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" HorizontalAlign="Center" Width="80%">
            <Columns>
                <asp:BoundField Visible="false" DataField="ReceivedItemID" HeaderText="ReceivedItemID" InsertVisible="False" ReadOnly="True" SortExpression="ReceivedItemID" />
                <asp:BoundField Visible="false" DataField="ReceivedItemsOTEOID" HeaderText="ReceivedItemsOTEOID" SortExpression="ReceivedItemsOTEOID" />
                <asp:BoundField Visible="false" DataField="ItemID" HeaderText="ItemID" SortExpression="ItemID" />

                 <asp:TemplateField HeaderText="Sl."> 
                   <ItemTemplate>  <%# Container.DataItemIndex+1 %>.     </ItemTemplate>  
                     <ItemStyle Width="5%" HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                   </asp:TemplateField>

                <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="unit" HeaderText="Unit" SortExpression="unit" />
                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" /><RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
        </asp:GridView>

    </div>
        <div style="padding-top:158px; margin:0px auto; width: 562px;">
            <span style="float:right">
                Sub-Divisional Officer<br />
                Electrical Store Sub-Division<br />
                Dimapur
            </span>
            <span>
                Junior Engineer (E)<br />
              
                Electrical Store<br />
               
                Dimapur
            </span>
            
        </div>
    </form>
</body>
</html>
