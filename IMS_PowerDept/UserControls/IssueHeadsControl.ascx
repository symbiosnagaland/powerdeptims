<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueHeadsControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.IssueHeadsControl" %>
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

				<div class="h_title">Manage Issue Heads</div>
<div style="margin:0px auto;padding:10px">

    <table class="table table-striped table-bordered table-hover" style="width:750px; " >
         <thead>
                                                <tr>
                                                    <td>I Head ID</td>
                                                <td>Issue Head</td>
                                                <td>Action</td>
                                                </tr>
                                            </thead>
                                     <tr>
                                              
                                            <td>
                                 

                                                 </td>
                                         <td> <asp:TextBox ID="_tbHeadName" placeholder="Add Issue Head" CssClass="form-control" Width="225px"   runat="server" Text=""></asp:TextBox></td>
                                         <td>
                                                  <button id="Button1" type="submit" runat="server" onserverclick="Unnamed_ServerClick"  class="add">Save Issue Head</button> 
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>

                              
                                    <asp:Repeater ID="_gridChEdit" runat="server" OnItemCommand="_gridChEdit_ItemCommand" DataSourceID="_sdsIssueHeads">
                                    
                                    <ItemTemplate>

                                           <tr>
                                                <td>
                                                   
                                                    <asp:Label ID="_lblIssueHead" runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.IssueHeadID")%>'></asp:Label>
                                                </td>
                                            <td>
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>'></asp:Label>
                                                <asp:TextBox ID="_tbHead" CssClass="form-control"  runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.IssueHeadName")%>' Visible="false"></asp:TextBox>
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
        <a href="../Reports/IssueHeadsList.aspx" target="_blank">
            <img alt="print this list" border="0" src="../img/print.png" style="border: none; margin-right: 7px;" />Print Issue Heads List</a>
    </p>


</div>
         </div>