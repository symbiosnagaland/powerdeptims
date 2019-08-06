<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportqe.aspx.cs" Inherits="IMS_PowerDept.reportqe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .Nested_ChildGrid td
        {
            background-color: #fff !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #2B579A !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div><div style="padding:10px; width: 962px; margin:0px auto">
    <h2 style="margin:4px">Division-wise Issue of Materials</h2>
      <asp:Label ID="DivisionName" runat="server" Font-Bold="true" Font-Size="18px" Text=""></asp:Label>
      <br />
      For Issues Between:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; and
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />
<asp:GridView ID="gvchead" runat="server" OnRowDataBound="gvchead_RowDataBound" AutoGenerateColumns="false" CssClass="Grid"
        DataKeyNames="ChargeableHeadName">
              <Columns>
                   <asp:BoundField  DataField="ChargeableHeadName" HeaderText="Chargable Head" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--2nd grid--%>
                        <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid"
                        ImageUrl="~/images/plus.png" CommandArgument="Show" />
                    <asp:Panel ID="pnlOrders" runat="server">
                        <asp:GridView ID="gvchildDetails" OnRowDataBound="gvchildDetails_RowDataBound" runat="server" AutoGenerateColumns="false" 
                            AllowPaging="true" OnPageIndexChanging="gvchildDetails_PageIndexChanging" CssClass="ChildGrid"
                            DataKeyNames="DeliveryItemsChallanID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>Challan ID : <%# Eval("DeliveryItemsChallanID")%></td>
                                                <td>Challan Date : <%# Eval("ChallanDate")%></td>
                                                <td>Indent No : <%# Eval("IndentReference")%></td>
                                                <td>I Date : <%# Eval("IndentDate")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                      <asp:ImageButton ID="imgProductsShow" runat="server" OnClick="Show_Hide_ProductsGrid"
                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                        <asp:Panel ID="pnlProducts" runat="server" >
                                        <asp:GridView ID="ggvitems" AutoGenerateColumns="false" runat="server">
                                            <Columns>
                                                   <asp:BoundField  DataField="DeliveryItemsChallanID" HeaderText="DeliveryItemsChallanID" Visible="false" />
                                              <asp:TemplateField HeaderText="Item">
                                                      <ItemTemplate>
                                                        <asp:Label ID="ee" runat="server" Text='<%# Eval("ItemName")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Quantity">
                                                      <ItemTemplate>
                                                        <asp:Label ID="rr" runat="server" Text='<%# Eval("Quantity")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Unit">
                                                      <ItemTemplate>
                                                        <asp:Label ID="chttNo" runat="server" Text='<%# Eval("Unit")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                      <ItemTemplate>
                                                        <asp:Label ID="chyyyNo" runat="server" Text='<%# Eval("Rate")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TotalAmount">
                                                      <ItemTemplate>
                                                        <asp:Label ID="kkk" runat="server" Text='<%# Eval("TotalAmount")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="I Head">
                                                      <ItemTemplate>
                                                        <asp:Label ID="uu" runat="server" Text='<%# Eval("IssueHeadName")%>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                              </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                     </asp:Panel>
                        </ItemTemplate>
                                       
            </asp:TemplateField>
           
            </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
