<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_BalanceSheet2.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_BalanceSheet2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <link rel="stylesheet" type="text/css" href="Report_style.css" media="screen" />

    
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
       <asp:HiddenField ID="dddd" Visible="false" runat="server" />
  <div style="padding:10px; width:810px;">
    <h2 style="margin:4px">Balance Sheet</h2>
     
     As On:&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       


      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
          ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="[sp_GetBalanceSheetByIssueHead] @IssueHeadName">
          <SelectParameters>
              <asp:SessionParameter Name="IssueHeadName" SessionField="Ihead" Type="String" />
          </SelectParameters>
      </asp:SqlDataSource>

   
      
       <hr />

      
      <table style="width:100%">
      
         <tr>
             <td>

           

<asp:GridView ID="gv1" runat="server" GridLines="None"  ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"  DataSourceID="SqlDataSource1" CellPadding="5" HorizontalAlign="Left" Width="98%" >
          <Columns>
               <asp:TemplateField HeaderText="Sl."> 
                   <ItemTemplate>  <%# Container.DataItemIndex+1 %>.     </ItemTemplate>  
                     <ItemStyle Width="5%" HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                   </asp:TemplateField>

              <asp:TemplateField HeaderText="Item Name">
                  <ItemTemplate>                    
                         <asp:Label ID="Label1"  Font-Size="14px" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ItemName") %>' />                     
                  </ItemTemplate>
                                   <ItemStyle Width="38%" HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />

              </asp:TemplateField>

               <asp:TemplateField HeaderText="Unit">
                  <ItemTemplate>     
                         <asp:Label ID="g" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "unit") %>' />
                  </ItemTemplate>
                                   <ItemStyle Width="5%"  HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
              </asp:TemplateField>


               <asp:TemplateField HeaderText ="Rate">
                  <ItemTemplate>
                      <asp:Label ID="ggd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rate") %>' />
                  </ItemTemplate>
     
                     <ItemStyle Width="13%"    HorizontalAlign="Right" />
                  <HeaderStyle HorizontalAlign="Right" />
              </asp:TemplateField>

                    <asp:TemplateField HeaderText="Issue Head">
                  <ItemTemplate>           
                          <asp:Label ID="g" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IssueHeadName") %>' />
                  </ItemTemplate>
                          <ItemStyle Width="20%" HorizontalAlign="Right" />
                      <HeaderStyle HorizontalAlign="Right" />
              </asp:TemplateField>


              <asp:TemplateField  HeaderText="Balance">
                  <ItemTemplate>
                       
                          <asp:Label ID="ggd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GrossBalance") %>' />
                  </ItemTemplate>
                     <ItemStyle Width="15%" HorizontalAlign="Right" />
                      <HeaderStyle HorizontalAlign="Right" />
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
