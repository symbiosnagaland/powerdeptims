<%@ Page Title="Create New Users" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="User_New.aspx.cs" Inherits="IMS_PowerDept.Admin.User_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
   
     <div class="full_w">
         <asp:Panel ID="panelSuccess" runat="server" CssClass="n_ok" Visible="false">
             <p>
                 <asp:Label ID="lblSuccess" runat="server" Text="success message"></asp:Label></p>
         </asp:Panel>

         <asp:Panel ID="panelError" runat="server" CssClass="n_error" Visible="false">
             <p>
                 <asp:Label ID="lblError" runat="server" Text="error message"></asp:Label></p>
         </asp:Panel>

				<div class="h_title">Add New User</div>
<div style="margin:0px auto;padding:10px">
    <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    
                                    <tbody>
                                      
                                        <tr class="even gradeC">
                                            <td>User Type</td>
                                    <td>
                                         <asp:DropDownList Width="300px" Height="32px" CssClass="err" ID="_ddUsertype" runat="server">
                                                          <asp:ListItem>--Selct User Role--</asp:ListItem>
                                             <asp:ListItem>Store</asp:ListItem>
                                             <asp:ListItem>Admin</asp:ListItem>
                                                      </asp:DropDownList> 
                                    </td>
                                        </tr>
                                        <tr class="odd gradeA">
                                            <td>Username</td>
                                           <td> <asp:TextBox Width="300px" CssClass="form-control"  ID="_tbUsername" autocomplete="off" runat="server"></asp:TextBox>

                                           </td>
                                           
                                        </tr>
                                        <tr class="even gradeA">
                                            <td>Password</td>
                                           <td> <asp:TextBox Width="300px" TextMode="Password" autocomplete="off"  CssClass ="form-control" ID="_tbPassword" runat="server"></asp:TextBox>

                                           </td>
                                        </tr>
                                        <tr class="odd gradeA">
                                            <td>Retype Password</td>
                                           <td> <asp:TextBox CssClass="form-control"  autocomplete="off"  TextMode="Password" Width="300px" ID="_tbrePassword" runat="server"></asp:TextBox>
                                           </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                 <button type="submit" runat="server" id="save" onserverclick="save_ServerClick" class="add">Save User</button>  
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

    </div>
         </div>
</asp:Content>
