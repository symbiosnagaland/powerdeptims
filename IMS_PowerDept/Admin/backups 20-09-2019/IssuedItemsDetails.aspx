<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssuedItemsDetails.aspx.cs" Inherits="IMS_PowerDept.Admin.IssuedItemsDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Items Details</title>
    <style>
        body {
            background: #f3f3f3 url(../img/shadow.png) repeat-x;
            color: #bebebe;
            font-family: Tahoma, Arial, Helvetica, sans-serif;
            font-size: 12px;
            margin-top: 50px;
        }

        table {
            *border-collapse: collapse; /* IE7 and lower */
            border-spacing: 0;
            width: 100%;
        }

        .bordered {
            border: solid #ccc 1px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            -webkit-box-shadow: 0 1px 1px #ccc;
            -moz-box-shadow: 0 1px 1px #ccc;
            box-shadow: 0 1px 1px #ccc;
        }

            .bordered tr:hover {
                background: #fbf8e9;
                -o-transition: all 0.1s ease-in-out;
                -webkit-transition: all 0.1s ease-in-out;
                -moz-transition: all 0.1s ease-in-out;
                -ms-transition: all 0.1s ease-in-out;
                transition: all 0.1s ease-in-out;
            }

            .bordered td, .bordered th {
                border-left: 1px solid #ccc;
                border-top: 1px solid #ccc;
                padding: 10px;
                text-align: left;
            }

            .bordered th {
                background-color: #dce9f9;
                background-image: -webkit-gradient(linear, left top, left bottom, from(#ebf3fc), to(#dce9f9));
                background-image: -webkit-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -moz-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -ms-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -o-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: linear-gradient(top, #ebf3fc, #dce9f9);
                -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                -moz-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                border-top: none;
                text-shadow: 0 1px 0 rgba(255,255,255,.5);
            }

                .bordered td:first-child, .bordered th:first-child {
                    border-left: none;
                }

                .bordered th:first-child {
                    -moz-border-radius: 6px 0 0 0;
                    -webkit-border-radius: 6px 0 0 0;
                    border-radius: 6px 0 0 0;
                }

                .bordered th:last-child {
                    -moz-border-radius: 0 6px 0 0;
                    -webkit-border-radius: 0 6px 0 0;
                    border-radius: 0 6px 0 0;
                }

                .bordered th:only-child {
                    -moz-border-radius: 6px 6px 0 0;
                    -webkit-border-radius: 6px 6px 0 0;
                    border-radius: 6px 6px 0 0;
                }

            .bordered tr:last-child td:first-child {
                -moz-border-radius: 0 0 0 6px;
                -webkit-border-radius: 0 0 0 6px;
                border-radius: 0 0 0 6px;
            }

            .bordered tr:last-child td:last-child {
                -moz-border-radius: 0 0 6px 0;
                -webkit-border-radius: 0 0 6px 0;
                border-radius: 0 0 6px 0;
            }



        /*----------------------*/

        .zebra td, .zebra th {
            padding: 10px;
            border-bottom: 1px solid #f2f2f2;
        }

        .zebra tbody tr:nth-child(even) {
            background: #f5f5f5;
            -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
            -moz-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
            box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
        }

        .zebra th {
            text-align: left;
            text-shadow: 0 1px 0 rgba(255,255,255,.5);
            border-bottom: 1px solid #ccc;
            background-color: #eee;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#f5f5f5), to(#eee));
            background-image: -webkit-linear-gradient(top, #f5f5f5, #eee);
            background-image: -moz-linear-gradient(top, #f5f5f5, #eee);
            background-image: -ms-linear-gradient(top, #f5f5f5, #eee);
            background-image: -o-linear-gradient(top, #f5f5f5, #eee);
            background-image: linear-gradient(top, #f5f5f5, #eee);
        }

            .zebra th:first-child {
                -moz-border-radius: 6px 0 0 0;
                -webkit-border-radius: 6px 0 0 0;
                border-radius: 6px 0 0 0;
            }

            .zebra th:last-child {
                -moz-border-radius: 0 6px 0 0;
                -webkit-border-radius: 0 6px 0 0;
                border-radius: 0 6px 0 0;
            }

            .zebra th:only-child {
                -moz-border-radius: 6px 6px 0 0;
                -webkit-border-radius: 6px 6px 0 0;
                border-radius: 6px 6px 0 0;
            }

        .zebra tfoot td {
            border-bottom: 0;
            border-top: 1px solid #fff;
            background-color: #f1f1f1;
        }

            .zebra tfoot td:first-child {
                -moz-border-radius: 0 0 0 6px;
                -webkit-border-radius: 0 0 0 6px;
                border-radius: 0 0 0 6px;
            }

            .zebra tfoot td:last-child {
                -moz-border-radius: 0 0 6px 0;
                -webkit-border-radius: 0 0 6px 0;
                border-radius: 0 0 6px 0;
            }

            .zebra tfoot td:only-child {
                -moz-border-radius: 0 0 6px 6px;
                -webkit-border-radius: 0 0 6px 6px border-radius: 0 0 6px 6px;
            }

        .titleet {
            background: url(../img/bg_box_head.jpg) repeat-x;
            color: #bebebe;
            font-size: 15px;
            font-weight: bold;
            height: 30px;
            padding: 7px 0 0 15px;
            text-shadow: #0E0E0E 0px 1px 1px;
            margin-bottom: 8px;
        }

        .labelet {
            color: #575757;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="_sdsChallan" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>"
            SelectCommand="SELECT * FROM [DeliveryItemsChallan] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="DeliveryItemsChallanID" QueryStringField="Id" Type="Decimal" />
            </SelectParameters>
        </asp:SqlDataSource>

        <div style="background: #ffffff; border: 1px solid #DCDDE1; color: #848484; margin: 0px auto; width: 900px;">
            <div class="titleet">Delivery Item Details</div>
            <div style="padding: 12px">
                <table>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                </table>
                <asp:ListView ID="_lvdetails" OnItemCommand="_lvdetails_ItemCommand" OnItemDataBound="_lvdetails_ItemDataBound" runat="server" DataSourceID="_sdsChallan">
                    <ItemTemplate>
                        <table class="zebra">
                            <tr>
                                <td>Challan ID :</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField1" Value='<%#DataBinder.Eval(Container, "DataItem.IsDeliveredTemporary")%>' runat="server" />
                                    <asp:Label ID="cid" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.DeliveryItemsChallanID")%>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Indent Reference</td>
                                <td>
                                    <asp:Label ID="ref" Font-Size="14px" Font-Bold="true" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IndentReference")%>'></asp:Label></td>
                                <td>Indent Date</td>
                                <td>
                               <%--     <asp:Label ID="idate" Font-Size="14px" Font-Bold="true" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IndentDate", "{0:yyyy/MM/dd}")%>'></asp:Label></td> --%>
                                    <asp:Label ID="idate" Font-Size="14px" Font-Bold="true" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IndentDate", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Division Name</td>
                                <td>
                                    <asp:Label ID="Label1" Font-Size="14px" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IndentingDivisionName")%>'></asp:Label></td>
                                <td>Head Name</td>
                                <td>
                                    <asp:Label ID="Label2" Font-Size="14px" Font-Bold="true" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Status :
                                </td>
                                <td>
                                    <asp:Label ID="status" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                 <%--   <asp:Panel ID="Panel1" Visible="false" runat="server">
                                        <asp:LinkButton ID="LinkButton2" CommandName="Update" runat="server">Update to Regular Issue Items</asp:LinkButton>
                                        
                                    </asp:Panel>--%>

                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:ListView>
                <hr />
                <asp:SqlDataSource ID="sdsitem" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [DeliveryItemsDetails] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="DeliveryItemsChallanID" QueryStringField="Id" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <table class="bordered">
                    <thead>
                        <tr>

                            <th>Sl. </th>
                            <th>Item Name</th>
                            <th>Head Name</th>
                            <th>Quantity</th>
                            <th style="width: 60px">Unit</th>
                            <th>Rate</th>
                        </tr>
                    </thead>
                    <asp:ListView ID="_lvDItemsChallan" runat="server" DataSourceID="sdsitem">
                        <ItemTemplate>
                            <tr>


                                <td><b><%# Container.DataItemIndex+1 %>. </b></td>
                                <td>
                                    <asp:Label ID="cid" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.ItemName")%>'></asp:Label></td>

                                <td>
                                    <asp:Label ID="Label4" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label5" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.Quantity")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label6" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.Unit")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label7" CssClass="labelet" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.Rate")%>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
                <br />
                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Print as Challan" ImageUrl="~/img/print.png" OnClick="ImageButton1_Click" Style="height: 26px" />
                &nbsp;<asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server" Font-Bold="True" Font-Underline="True">PRINT DETAILS</asp:LinkButton>
            </div>
        </div>
        <asp:HiddenField ID="querystringid" Visible="false" runat="server" />

    </form>
</body>
</html>
