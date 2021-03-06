﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_ChallanAll.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_ChallanAll" %>

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
      <div style="width: 350px; margin: auto;">    <!--new addition-->
    <h2 style="margin:4px">Division-wise Issue of Materials</h2>
      <asp:Label ID="DivisionName" runat="server" Font-Bold="true" Font-Size="18px" Text=""></asp:Label>
      <br />
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text=""></asp:Label><br />
       <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
       <br />
          </div>
      <table style="width:800px;">
          <tr>
              <td>
<asp:GridView AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvDivisions_RowDataBound"  ID="gvDivisions" runat="server" ShowHeader="False" Width="800px">
          <Columns>

              <asp:TemplateField>
                  <ItemTemplate>
                      <tr>
                       <td colspan="2" style="text-align:left;">
 <asp:Label Font-Bold="true" Font-Size="16px" ID="Label1" runat="server" Text="Division Name :        "/>
                    <asp:Label ID="chName" Font-Bold="true" Font-Size="16px" runat="server" Text='<%# Eval("IndentingDivisionName")%>' /> 
               
                           </td></tr>
                            
                  </ItemTemplate>
              </asp:TemplateField>
              
              <asp:TemplateField>
                  <ItemTemplate>
                      <tr>
                    <td colspan="100%;">
                      <asp:GridView ID="gvChallanDateNall" GridLines="None" DataKeyNames="ChargeableHeadName"  ShowHeader="false" OnRowDataBound="gvChallanDateNall_RowDataBound" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                  <asp:TemplateField>


                                      <ItemTemplate>
                                          <table>
                                          <tr>
                                                             <td colspan="2" style="text-align:left;">
 <asp:Label Font-Bold="true" Font-Size="14px" ID="Label1" runat="server" Text="Chargeable Head :        "/>
                    <asp:Label ID="Label4" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("ChargeableHeadName")%>' />                 
                          
                       </td>
                            </tr>
<tr>
<td colspan="2">Challan No :  <asp:Label ID="chaalID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DeliveryItemsChallanID") %>' /></td>
                                                <td>Dated : <asp:Label ID="chName" runat="server" Text='<%# Eval("ChallanDate" ,"{0:dd/MM/yyyy}")%>' /></td>
                                                <td>Indent No : <%# Eval("IndentReference")%></td>
                                                <td colspan="2">Dated :
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Eval("IndentDate" ,"{0:dd/MM/yyyy}")%>' />
                                                </td>
    </tr>
                                              </table>
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
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Convert.ToDouble(Eval("Amount"))%>' />
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
