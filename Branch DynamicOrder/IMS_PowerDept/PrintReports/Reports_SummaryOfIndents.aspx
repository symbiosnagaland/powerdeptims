<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports_SummaryOfIndents.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Reports_SummaryOfIndents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div style="padding:10px; width:892px; margin:0px auto">
    <h2 style="margin:4px">Summary of Indents</h2>
      <asp:Label ID="DivisionName" runat="server" Font-Bold="true" Font-Size="18px" Text=""></asp:Label>
      <br />
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />
        <table style="width:800px">
            
          
                <asp:GridView ID="gv1" OnRowDataBound="gv1_RowDataBound" Width="100%" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                 <tr>
                                    <td>
                                      <asp:Label ID="Label1" Font-Bold="true" Font-Size="15px" runat="server" Text="Division : " />
                                <asp:Label ID="indiv" runat="server" Font-Bold="true" Font-Size="15px" Text='<%#DataBinder.Eval(Container.DataItem, "IndentingDivisionName") %>' />
                                        </td>
                                     </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                   <%-- <asp:BoundField DataField="IndentingDivisionName" HeaderText="IndentingDivisionName" SortExpression="IndentingDivisionName" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv2" Width="100%" AutoGenerateColumns="false" GridLines="None"  runat="server">
                                    <Columns>
                                         <asp:BoundField DataField="IndentReference" HeaderText="Indent No" SortExpression="IndentReference" />
                                         <asp:BoundField DataField="IndentDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Indent Date"  SortExpression="IndentDate" />
                                         <asp:BoundField DataField="DeliveryItemsChallanID" HeaderText="Challan No"  SortExpression="DeliveryItemsChallanID" />
                                         <asp:BoundField DataField="ChallanDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Challan Date"  SortExpression="ChallanDate" />
                                         <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch Head"  SortExpression="ChargeableHeadName">
                                              <ItemStyle Width="200px" />
                                             </asp:BoundField>
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
