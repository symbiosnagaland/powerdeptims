<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChargeableHeadsControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.ChargeableHeadsControl" %>
     <asp:SqlDataSource ID="_sds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [ChargeableHeads] where Status='A' ORDER BY [ChargeableHeadID] ASC"></asp:SqlDataSource>

<div class="full_w">
     <asp:Panel ID="panelSuccess" Visible="false" CssClass="n_ok" runat="server">

        <p>
            <asp:Label ID="lblSuccess" runat="server" Text="success message"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="panelError" Visible="false" CssClass="n_error" runat="server">
        <p>
            <asp:Label ID="lblError" runat="server" Text="error message"></asp:Label>
        </p>
    </asp:Panel>

				<div class="h_title">Manage Chargeable Heads</div>
<div style="margin:0px auto;padding:10px">
    <table class="table table-striped table-bordered table-hover" style="width:750px; " >
 <thead>
                                               
                                                <tr>
                                                    <td>Chargeable Head Name</td>
                                                 <td>Issue Head Name</td>
                                                <td>Action </td>
                                                </tr>
                                            </thead>
                                     <tr>
                                              
                                            <td>
                                                <asp:TextBox ID="_tbchhead" placeholder="Add Chargeable Head Name" CssClass="form-control" Width="200px" runat="server" Text=""></asp:TextBox>

                                                 </td>
                                         <td> 
                                            <asp:DropDownList CssClass="text"  Width="200px" Height="30px" ID="_ddIssueHead" runat="server"></asp:DropDownList>
                                  
                                             </td>
                                         <td>
                                                 <button type="submit" runat="server" id="save" onserverclick="save_ServerClick" class="add">Save Chargeable Head</button>  
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>

   
                                    <asp:Repeater ID="_gridChEdit" runat="server" OnItemDataBound="_gridChEdit_ItemDataBound"  OnItemCommand="_gridChEdit_ItemCommand" DataSourceID="_sds">
                                    
                                    <ItemTemplate>

                                           <tr>
                                              
                                            <td>
                                                 <asp:Label ID="_lblIssueHead" Visible="false" runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.ChargeableHeadID")%>'></asp:Label>
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                                                <asp:TextBox ID="_tbChHead" CssClass="form-control" Width="200px"   runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>' Visible="false"></asp:TextBox>
                                                 </td>
                                               <td>
                                               <asp:Label ID="_lblHead" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                                                    <%--<asp:DropDownList CssClass="form-control" Visible="false" Width="350px" ID="_grdIssueHead" runat="server"></asp:DropDownList>--%>
                                                <asp:TextBox ID="_grdIssueHead" CssClass="form-control" Width="200px"  runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>' Visible="false"></asp:TextBox>
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
        <a href="../Reports/ChargeableHeadsList.aspx" target="_blank">
            <img alt="print this list" border="0" src="../img/print.png" style="border: none; margin-right: 7px;" />Print Chargeable Heads List</a>
    </p>


</div>
         </div>