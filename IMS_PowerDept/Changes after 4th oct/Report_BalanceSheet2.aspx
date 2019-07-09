<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_BalanceSheet2.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_BalanceSheet2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <link rel="stylesheet" type="text/css" href="Report_style.css" media="screen" />

    <style>
        tr th:nth-child(2) 
       {
        text-align: right;
        }

       tr td:nth-child(2) 
       {
        text-align: right;
        }

       tr th:nth-child(3) 
       {
        text-align: right;
        }

       tr td:nth-child(3) 
       {
        text-align: right;
        }
    </style>
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
       <asp:HiddenField ID="dddd" Visible="false" runat="server" />
  <div style="padding:10px; width:810px;">
    <h2 style="margin:4px">Balance Sheet</h2>
     
     As On:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       
 <%--   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
          ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" 
          SelectCommand="SELECT DISTINCT ItemName, MAX(Rate) AS Rate, ItemID, unit FROM ReceivedItemsDetails GROUP BY ItemID, ItemName, unit"></asp:SqlDataSource>--%>

      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
          ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select  itemid, ItemName,GrossBalance, issueheadname from ItemsInventory  where ([IssueHeadName] = @IssueHeadName) and (GrossBalance <>0 and NetActualBalance <>0) order by ItemName">
          <SelectParameters>
              <asp:SessionParameter Name="IssueHeadName" SessionField="Ihead" Type="String" />
          </SelectParameters>
      </asp:SqlDataSource>

   
      
       <hr />

      
      <table style="width:100%">
      
         <tr>
             <td>

<asp:GridView ID="gv1" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowDataBound="gv1_RowDataBound" DataKeyNames="itemid" DataSourceID="SqlDataSource1" CellPadding="5" HorizontalAlign="Left" >
          <Columns>
              <asp:TemplateField HeaderText="Item Name - Unit - Rate">
                  <ItemTemplate>
                      <asp:Label ID="chaalID" runat="server" Visible="false"  Text='<%#DataBinder.Eval(Container.DataItem, "ItemID") %>' />
                         <asp:Label ID="Label1" Font-Bold="true" Font-Size="14px" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ItemName") %>' />

                      <span>
                          <asp:GridView ID="gev" ShowHeader="false" GridLines="None" AutoGenerateColumns="false" OnRowDataBound="gv_RowDataBound" CellPadding="5" HorizontalAlign="Left" runat="server">
                          <Columns>
                                <asp:TemplateField>
                  <ItemTemplate>
                      
                         <asp:Label ID="g" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "unit") %>' />
                      
                  </ItemTemplate>
                                   <ItemStyle Width="140px" />
              </asp:TemplateField>
               <asp:TemplateField>
                  <ItemTemplate>
                      <asp:Label ID="ggd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rate") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                      </span>


                  </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Issue Head">
                  <ItemTemplate>           
                          <asp:Label ID="g" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IssueHeadName") %>' />
                  </ItemTemplate> <ItemStyle Width="120px" />
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Balance">
                  <ItemTemplate>                
                        <asp:Label ID="ggd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GrossBalance") %>' />
          
                  </ItemTemplate> <ItemStyle Width="120px" />
              </asp:TemplateField>
               
              <asp:TemplateField>
                  <ItemTemplate>
                       <tr>
                    <td colspan="100%">
                      
                        </td>
                           </tr>
                  </ItemTemplate>
              </asp:TemplateField>

          </Columns>
          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="True" Font-Italic="False" />
      </asp:GridView>
             </td>

         </tr>

      </table>

      <asp:HiddenField ID="h1" runat="server" />
       
   </div>
    </form>
</body>
</html>
