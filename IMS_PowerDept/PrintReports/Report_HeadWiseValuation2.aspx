<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_HeadWiseValuation2.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_HeadWiseValuation2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">

    <form id="form1" runat="server">
  <div style="padding:10px; width: 751px; margin:0px auto">
    <h2 style="margin:4px">Head-Wise Valuation</h2>
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text="Label"></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text="Label"></asp:Label>
       <hr />
        <table style="width:100%">
       <%--  <thead>
             <tr>
           <td class="auto-style1">Division</td>
          <td style="vertical-align:middle; text-align:left; font-weight:bold" class="auto-style2">Chargeable Head</td>
          <td style=" font-weight:bold">Amount</td>                                         
          </tr>
        </thead>  --%>  
                             
      <asp:GridView Width="100%" ShowHeader="false" ID="GridView1" OnRowDataBound="GridView1_RowDataBound" GridLines="None" AutoGenerateColumns="False" runat="server">
         <Columns>
            
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                  <%--     <asp:Label ID="ete" Font-Bold="true" Font-Size="14px" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID ")%>'></asp:Label>--%>
                                                        <asp:Label ID="_lbldivi"  Font-Bold="true" Font-Size="14px" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
                                                     
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                    <asp:GridView ID="GridView2" Width="500px" GridLines="None" ShowFooter="true" AutoGenerateColumns="false" 
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowCreated="GridView2_RowCreated" runat="server">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DeliveryItemsChallanID"  HeaderText="DeliveryItemsChallanID" Visible="false" SortExpression="DeliveryItemsChallanID"> </asp:BoundField>
                                                                       <asp:TemplateField HeaderText="Chargeable Head">
                                                                           <ItemTemplate>
                                                                             <asp:Label ID="eds" Width="200px" runat="server" Text='<%# Eval("chargeableheadname")%>' />
                                                                            </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center">
                                                                           <ItemTemplate>
                                                                             <asp:Label ID="lblPrice" Font-Bold="true" runat="server" Text='<%# Eval("TotalAmount")%>' />
                                                                            </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                 <asp:Label ID="lblTotalPrice" runat="server" />
                                                                            </FooterTemplate> 
                                                                         </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>                                       
                                            </asp:GridView>                          
                                   </table>
 </div>
    </form>
</body>
</html>
