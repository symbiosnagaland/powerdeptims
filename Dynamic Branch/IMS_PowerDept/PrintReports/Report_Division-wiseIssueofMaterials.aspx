<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Division-wiseIssueofMaterials.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_Division_wiseIssueofMaterials" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
        <asp:HiddenField ID="dddd" Visible="false" runat="server" />
  <div style="padding:10px; width:810px;">
    <h2 style="margin:4px">Division-wise Issue of Materials</h2>
      <asp:Label ID="DivisionName" runat="server" Font-Bold="true" Font-Size="18px" Text=""></asp:Label>
      <br />
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text=""></asp:Label><br />
       <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
       <hr />



      <table style="width:800px">
          <tr>
              <td>
<asp:GridView AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvChargeableHead_RowDataBound"  ID="gvChargeableHead" runat="server" ShowHeader="False">
          <Columns>
              <asp:TemplateField>
                  <ItemTemplate>
                       <td colspan="2" style="text-align:left;">
 <asp:Label Font-Bold="true" Font-Size="16px" ID="Label1" runat="server" Text="Chargeable Head :        "/>
                    <asp:Label ID="chName" Font-Bold="true" Font-Size="16px" runat="server" Text='<%# Eval("ChargeableHeadName")%>' />                 
                          
                       </td>
                            
                  </ItemTemplate>
              </asp:TemplateField>
              
              <asp:TemplateField>
                  <ItemTemplate>
                      <tr>
                    <td colspan="100%">
                      <asp:GridView ID="gvChallanDateNall" GridLines="None" DataKeyNames="ChargeableHeadName"  ShowHeader="false" OnRowDataBound="gvChallanDateNall_RowDataBound" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                     

                                          <td colspan="2">Challan No :  <asp:Label ID="chaalID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DeliveryItemsChallanID") %>' /></td>
                                                <td>Dated : <asp:Label ID="chName" runat="server" Text='<%# Eval("ChallanDate" ,"{0:dd/MM/yyyy}")%>' /></td>
                                                <td>Indent No : <%# Eval("IndentReference")%></td>
                                                <td colspan="2">Dated :
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Eval("IndentDate" ,"{0:dd/MM/yyyy}")%>' />
                                                </td>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                    <td colspan="100%">
                                        <asp:GridView ID="gvItems" Width="800px" HeaderStyle-BorderStyle="None" GridLines="None" OnRowCreated="gvItems_RowCreated" ShowFooter="true" 
                                            OnRowDataBound="gvItems_RowDataBound" HeaderStyle-HorizontalAlign="Left"  AutoGenerateColumns="false" runat="server">
                                             <Columns>
                                                 <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                                                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                                 <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                                                 <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                                 <asp:BoundField DataField="TotalAmount" Visible="false" HeaderText="TotalAmount" SortExpression="TotalAmount" />
                                                  <asp:TemplateField HeaderText="Amount">
                                                     <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Convert.ToDouble(Eval("amount"))%>' />
                                                       </ItemTemplate>
                                                       <FooterTemplate>
                                                        <asp:Label ID="lblTotalPrice" Font-Bold="true" runat="server" />
                                                   </FooterTemplate>
                                                 </asp:TemplateField>
                                                 <asp:BoundField DataField="IssueHeadName" HeaderText="IssueHeadName" SortExpression="IssueHeadName" />
                                                 <asp:BoundField DataField="DeliveryItemDetailsID" Visible="false" HeaderText="DeliveryItemDetailsID" />
                                                 
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
