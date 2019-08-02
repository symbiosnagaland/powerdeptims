<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_ReceiptValuation.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_ReceiptValuation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div style="padding:10px; margin:0px auto; width: 961px;">
      <h2 style="margin:4px">Receipt of Materials</h2>
     
      <br />
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp;&nbsp; and&nbsp;
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />
         <table style="width:800px">
          <tr>
              <td>
                  <asp:GridView ID="gv1" OnRowDataBound="gv1_RowDataBound" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" runat="server">
                      <Columns>
                          <asp:TemplateField>
                  <ItemTemplate>
                       
                             <asp:Label Font-Bold="true" Font-Size="16px" ID="Label1" runat="server" Text="Chargeable Head :        "/>
                    <asp:Label ID="chName" Font-Bold="true" Font-Size="16px" runat="server" Text='<%# Eval("ChargeableHeadName")%>' /> 
                     
                  </ItemTemplate>
              </asp:TemplateField>
                           <asp:TemplateField>
                               <ItemTemplate>
                               <tr>
                               <td colspan="100%">

                                   <asp:GridView AutoGenerateColumns="false" OnRowDataBound="gv2_RowDataBound" ShowHeader="false" GridLines="None" ID="gv2" HorizontalAlign="Left"  runat="server">
                                       <Columns>
                                           <asp:TemplateField>
                                               <ItemTemplate>

                                                   <div style="margin:7px">
                                                   Supplier : &nbsp; <asp:Label ID="Label2" Font-Size="15px"  Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Supplier")%>'></asp:Label>
                                                </div>
                                                 
                                                  <div style="margin:7px">
                                                     S Order Ref./Date : &nbsp; <asp:Label ID="divisionname" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SupplyOrderReference")%>'></asp:Label>
                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       Dated :<asp:Label ID="Label1"  runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SupplyOrderDate","{0:yyyy/MM/dd}")%>'></asp:Label>
                                                  
                                                   </div>
                                                   
                                                  <div style="margin:7px">
                                                   OTEO Ref : &nbsp; <asp:Label ID="cno" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceivedItemsOTEOID")%>'></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       Dated : &nbsp;&nbsp;<asp:Label ID="cdate" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ReceivedItemOTEODate","{0:yyyy/MM/dd}")%>'></asp:Label> 
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Issue Head :&nbsp; <asp:Label ID="Label4"  runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                                                   </div>


                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField>
                                               <ItemTemplate>
                                                   <tr>
                                                      <td colspan="100%">
                                                   <asp:GridView ID="gv3" Width="800px" OnRowCreated="gv3_RowCreated" GridLines="None" OnRowDataBound="gv3_RowDataBound" HorizontalAlign="Left" ShowFooter="true" AutoGenerateColumns="false" runat="server">
                                                       <Columns>
                                                                           
                                                 <asp:TemplateField HeaderText="Sl.">   
                                                 <ItemTemplate>  <%# Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                                 <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="ReceivedItemsOTEOID" Visible="false" SortExpression="ReceivedItemsOTEOID" />
                                                        
                                                 <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName"/>
                                                          
                                                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                                 <asp:BoundField DataField="unit" HeaderText="Unit" SortExpression="unit" />
                                                 <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                                           <asp:TemplateField HeaderText="Amount">
                                                     <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("amount")%>' />
                                                       </ItemTemplate>
                                                       <FooterTemplate>
                                                        <asp:Label ID="lblTotalPrice" Font-Bold="true" Text="Total" runat="server" />
                                                   </FooterTemplate>
                                                 </asp:TemplateField>

                                                 <asp:BoundField DataField="amount" Visible="false" HeaderText="amount" SortExpression="amount" />
                                                          
                                                       </Columns>
                                                     
                                                   </asp:GridView>
                                                          </td>
                                                       </tr>
                                               </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Left" />
                                           </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>

                               </td>
                               </tr>
                               </ItemTemplate>
                               </asp:TemplateField>
                      </Columns>
                  </asp:GridView>

              </td>
              </tr>
             </table>

    </div>
    </form>
</body>
</html>
