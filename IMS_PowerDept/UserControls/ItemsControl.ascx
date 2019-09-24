<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemsControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.ItemsControl" %>
  <%--<asp:SqlDataSource ID="_sdsItems" runat="server"
                             ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [Items] where Status='A' ORDER BY [itemid] DESC"></asp:SqlDataSource>--%>
    

<div class="full_w">
         <asp:Panel ID="panelSuccess" Visible="false" CssClass="n_ok" runat="server">

        <p>
            <asp:Label ID="lblSuccess" runat="server" Text="success message"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="panelError" Visible="false" CssClass="n_error" runat="server">
        <p>
            <asp:Label ID="lblError"  runat="server" Text="error message"></asp:Label>
        </p>
    </asp:Panel>

				<div class="h_title">Manage Items</div>
<div style="margin:0px auto;padding:10px">

   

     <table class="table table-striped table-bordered table-hover" style="width:860px; "  >
          <thead>
                                               <tr>
                                                    <td>Item ID</td>
                                                <td>Item Name</td>
                                                <td>Unit</td> <td>Action</td>
                                               </tr>
                                            </thead>
                                     <tr>
                                              
                                            <td>
                                                <asp:TextBox ID="_tbchID" placeholder="Item ID" CssClass="form-control" Width="60px"  runat="server" Text=""></asp:TextBox>

                                                 </td>
                                         <td> <asp:TextBox ID="_tbHeadName" Width="220px" placeholder="Add ItemName" CssClass="form-control"  runat="server" Text=""></asp:TextBox></td>
                                         <td> <asp:TextBox ID="_tbItemUnit" Width="80px" placeholder="Add Item Unit" CssClass="form-control"  runat="server" Text=""></asp:TextBox></td>
                                         <td>
                                           <button type="submit" runat="server" id="saveItems" onserverclick="saveItems_ServerClick" class="add">Save Item</button>  
                                         </td>
                                    </tr>
       
                                <asp:Repeater ID="_gridChEdit" runat="server" OnItemCommand="_gridChEdit_ItemCommand">
                                    <ItemTemplate>

                                           <tr>
                                                <td>
                                                <asp:Label ID="_lblitemID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.itemid")%>'></asp:Label>
                                                </td>
                                            <td>
                                                <asp:Label ID="cHEad" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.itemname")%>'></asp:Label>
                                                <asp:TextBox ID="_tbHead" CssClass="form-control" Width="220px"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "itemname")%>' Visible="false"></asp:TextBox>
                                                 </td>
                                               <td>
                                                <asp:Label ID="_lblunit" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.unit")%>'></asp:Label>
                                                    <asp:TextBox ID="_tbunit" CssClass="form-control" Width="80px"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "unit")%>' Visible="false"></asp:TextBox>
                                               </td>
                                            <td>
                                             <asp:Button ID="edirBt" CommandName="edit" CssClass="btn btn-success" runat="server" Text="Edit" />  
                                             <asp:Button ID="updateBt" CommandName="update" CssClass="btn btn-info" runat="server" Visible="false" Text="Update" />  
                                             <asp:Button ID="cancelBt" CommandName="cancel" CssClass="btn btn-warning" runat="server" Visible="false" Text="Cancel" />  
                                             <asp:Button ID="deleteBt" CommandName="delete"  CssClass="btn btn-danger" runat="server" Text="Delete" />  

                            
                                            </td>
                                           </tr>
                                       
                                    </ItemTemplate>
                                </asp:Repeater>
                                   

                                 </table>
    <p>
     <asp:Label ID="lblPageNumber" runat="server" Text=""></asp:Label>
        </p>
  <%-- <pagersettings mode="NumericFirstLast" firstpagetext="1" lastpagetext="Last" pagebuttoncount="20" Visible ="true" position="Bottom"/> --%>
                  <table style="width:650px"  class="table table-striped table-bordered table-hover" >
    
        <tr>
           
             <asp:Repeater ID="rptPages" Runat="server">
      <ItemTemplate>
          <td colspan="4">
                    <asp:LinkButton ID="btnPage"
                         CommandName="Page"
                         CommandArgument="<%#
                         Container.DataItem %>"
                         CssClass="text"
                         Runat="server"><%# Container.DataItem %>
                         </asp:LinkButton>&nbsp;
          </td>
      </ItemTemplate>
      </asp:Repeater>
       </tr>
    </table> 
    <p>
        <a href="javascript:window.open('../Reports/ItemsList.aspx', 'name','height=1024,width=768,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no')" target="name">
            <img alt="print this list" border="0" src="../img/print.png" style="border: none; margin-right: 7px;" />Print Items List</a>
    </p>


</div>
     </div>
    <asp:TextBox ID="_status" Visible="false" CssClass="form-control"  runat="server" Text="A"></asp:TextBox>