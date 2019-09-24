<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_ItemsInventory.aspx.cs" Inherits="IMS_PowerDept.Print.Print_ItemsInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        table, td {
        text-align: left;
        }
        tr td:last-child {
        text-align: right;
        }
        tr th:last-child {
        text-align: right;
        }
        tr th:nth-child(5) {
        text-align: right;
        }
        tr td:nth-child(5) {
        text-align: right;
        }
    </style>
</head>
<body onload="javascript:window.print()">

    <form id="form1" runat="server">
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
            <h2 style="text-align:center; margin:0px">Items Inventory</h2>
              <div style="margin:0 auto; padding:10px; width: 820px;">
                  <asp:GridView Width="100%" ID="temp" HorizontalAlign="Center" GridLines="None"  AutoGenerateColumns="false" runat="server" CellPadding="5">
                  <Columns>
                          <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>


                      <asp:TemplateField  HeaderText="Item">                        
                          <ItemTemplate>
                              <asp:Label ID="a1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ItemName")%>'></asp:Label>
                          </ItemTemplate>                         
                      </asp:TemplateField>

                          <asp:TemplateField  HeaderText="UNIT">                       
                        <ItemTemplate>
                              <asp:Label ID="a1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.UNIT")%>'></asp:Label>
                          </ItemTemplate><ItemStyle Width="50px" />
                     </asp:TemplateField>

                       <asp:TemplateField HeaderText="Issue Head">
                          <ItemTemplate>
                              <asp:Label ID="a4" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                          </ItemTemplate><ItemStyle Width="80px" />
                      </asp:TemplateField>

                       <asp:TemplateField HeaderText="Gross Balance" >
                          <ItemStyle Width="80px"/>
                           <ItemTemplate >
                              <asp:Label ID="a2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.GrossBalance")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     
                  </Columns>
                      <HeaderStyle  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
              </asp:GridView>


                  <asp:GridView Width="100%" ID="GridView1" HorizontalAlign="Center" GridLines="None"  AutoGenerateColumns="false" runat="server" CellPadding="5">
                  <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>
                       <asp:TemplateField  HeaderText="Item">
                          <ItemTemplate>
                              <asp:Label ID="a1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ItemName")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>

                         <asp:TemplateField  HeaderText="UNIT">                       
                        <ItemTemplate>
                              <asp:Label ID="a1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.UNIT")%>'></asp:Label>
                          </ItemTemplate>
                     </asp:TemplateField>

                       <asp:TemplateField HeaderText="Issue Head">
                          <ItemTemplate>
                              <asp:Label ID="a4" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Gross Balance">
                          <ItemTemplate>
                              <asp:Label ID="a2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.GrossBalance")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Temporary Balance">
                          <ItemTemplate>
                              <asp:Label ID="a3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.TemporaryIssued")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                       <HeaderStyle  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
              </asp:GridView>

                    
              </div>
    
    </div>
    </form>
</body>
</html>
