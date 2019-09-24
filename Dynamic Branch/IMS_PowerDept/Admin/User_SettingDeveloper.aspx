<%@ Page Title="User Setting" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="User_SettingDeveloper.aspx.cs" Inherits="IMS_PowerDept.Admin.User_SettingDeveloper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:SqlDataSource ID="_sdsusers" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
   
     <div class="full_w">
           <asp:Panel ID="_pnlSucc" Visible="false" runat="server" CssClass="n_ok">
               <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
           </asp:Panel>
    <asp:Panel ID="_pnlError" Visible="false" runat="server" CssClass="n_error">
         <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </asp:Panel>

				<div class="h_title">User Setting</div>
<div style="margin:0px auto;padding:10px">
      <table class="table table-striped table-bordered table-hover" >

                                             <asp:Repeater OnItemCommand="_rpUsersEdit_ItemCommand" ID="_rpUsersEdit" runat="server" DataSourceID="_sdsusers">
                                     <HeaderTemplate>
                                            <thead>
                                                <td>Role</td>
                                                <td>UserName</td>
                                               <td>Password</td> <td>Action</td>
                                            </thead>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                           <tr>
                                                <td>
                                                    <asp:Label ID="_lbluserid" Visible="false" runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.userid")%>'></asp:Label>
                                                    <asp:Label ID="_lblIssueHead" runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.Role")%>'></asp:Label>
                                                </td>
                                            <td>
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.username")%>'></asp:Label>
                                                 
                                                 </td>
                                               <td>
                                            <asp:TextBox ID="_tbpass1" Width="150px" CssClass="form-control" Text="***********"  runat="server" ></asp:TextBox>
                                                 <asp:TextBox ID="_tbpasword"  Width="150px"  CssClass="form-control"  runat="server" placeholder="Enter New Password" Visible="false"></asp:TextBox>
                                                    </td>
                                            <td> 
                                             <asp:Button ID="edirBt" CommandName="edit" CssClass="btn btn-success" runat="server" Text="Change Password" />  
                                             <asp:Button ID="updateBt" CommandName="update" CssClass="btn btn-info" runat="server" Visible="false" Text="Update Password" />  
                                             <asp:Button ID="cancelBt" CommandName="cancel" CssClass="btn btn-warning" runat="server" Visible="false" Text="Cancel" />  
                                             <asp:Button ID="deleteBt" CommandName="delete"  OnClientClick='javascript:return confirm("Permanently delete this user?")'  CssClass="btn btn-danger" runat="server" Text="Delete Current User" />  

                            
                                            </td>
                                           </tr>
                                     </ItemTemplate>
                                 </asp:Repeater>

                                       </table>

    </div>
         </div>
</asp:Content>
