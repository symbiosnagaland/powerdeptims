<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DivisionsControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.DivisionsControl" %>

<style>

    #print-button {
	display: none;
}

</style>

    <asp:SqlDataSource ID="_sdsDivision" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [Divisions] ORDER BY [division] ASC"></asp:SqlDataSource>


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
    
				<div class="h_title">Manage Divisions</div>
<div style="margin:0px auto;padding:10px">
    <table  class="table table-striped table-bordered table-hover" style="width:750px; " >
        <thead>
                                               
<tr>
                                                    <td>Division ID</td>
                                                <td>Division Name</td>
                                                 <td>Actions</td>
</tr>
                                            </thead>
        <tr>
                                        <td style="background:#fff;" colspan="3"></td>
                                    </tr>
                                     <tr>
                                            <td>
                                                <asp:TextBox ID="_tbchID" Width="90px" placeholder="Division ID" Enabled="false" CssClass="form-control"  runat="server" Text="" ></asp:TextBox>

                                                 </td>
                                         <td> <asp:TextBox ID="_tbHeadName" Width="225px" placeholder="Division Name" CssClass="form-control"  runat="server" Text=""></asp:TextBox></td>
                                         <td>
                                               <%-- <asp:Button ID="addBtn"   CssClass="btn btn-info" runat="server" Text="Add New Division" />  --%>
                                                                     <button id="Button1" type="submit" runat="server" onserverclick="addBtn_Click" class="add">Save Division</button> 
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
        <div id="printReady">
                                <asp:Repeater ID="_gridChEdit" runat="server" OnItemCommand="_gridChEdit_ItemCommand"  DataSourceID="_sdsDivision">
                                    <ItemTemplate>
                                           <tr>
                                                <td>                                                   
                                                    <asp:Label ID="_lblIssueHead" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.division")%>'></asp:Label>
                                                </td>
                                            <td>
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.divisionName")%>'></asp:Label>
                                                <asp:TextBox ID="_tbHead" CssClass="form-control" Width="225px"  runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.divisionName")%>' Visible="false"></asp:TextBox>
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
            </div>            
                                 </table>
    <p>
        <a href="../Reports/DivisionsList.aspx" target="_blank">
            <img src="../img/print.png" style="border: none; margin-right: 7px;" border="0" alt="print this list" />Print Divisions List</a>
    </p>

   
</div>
            </div>