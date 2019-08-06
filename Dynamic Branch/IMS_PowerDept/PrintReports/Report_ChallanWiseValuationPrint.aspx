<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_ChallanWiseValuationPrint.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_ChallanWiseValuationPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="Report_style.css" />
    <style>
        .labelamount
        {
            text-align:left;
        }
    </style>
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
   <div style="padding:10px; width:892px; margin:0px auto">
    <h2 style="margin:4px">Monthly Division-wise Valuation</h2>
     
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />
        <asp:SqlDataSource ID="sds1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select distinct IndentingDivisionName  from DeliveryItemsChallan"></asp:SqlDataSource>
    <table>
       
        <asp:GridView ID="GridView1" Width="880px" runat="server" ShowHeader="false" GridLines="None" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="_lbldivi" runat="server" Font-Size="16px" Font-Bold="true" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label>
                            </td>
                        </tr>
                        
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:GridView Width="880px" HeaderStyle-HorizontalAlign="Left"  
                                    ID="GridView2" ShowFooter="true" ShowHeader="True" GridLines="None"  
                                    OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="false" runat="server">
                            <Columns>
                           
                            <asp:BoundField DataField="ChargeableHeadName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Chargeable Head" SortExpression="ChargeableHeadName" />
                           <asp:BoundField DataField="DeliveryItemsChallanID"  HeaderText="DeliveryItemsChallanID" SortExpression="DeliveryItemsChallanID">
                            </asp:BoundField>
                                  <asp:BoundField DataField="ChallanDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="ChallanDate" SortExpression="ChallanDate" />
                            <asp:BoundField DataField="IndentReference" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="IndentReference" SortExpression="IndentReference" />
                            <asp:BoundField DataField="IndentDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="IndentDate" SortExpression="IndentDate" />
                                <asp:TemplateField HeaderText="Amount"  HeaderStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                         
                                        <asp:Label ID="lblPrice" CssClass="labelamount" runat="server" Text='<%# Eval("TotalAmount")%>' />
                                     </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:HiddenField ID="cId" Value='<%# Eval("ChargeableHeadName")%>' runat="server" />
                                         <asp:Label ID="lblTotalPrice" Font-Bold="true" runat="server" />
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
