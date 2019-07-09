<%@ Page Title="User Setting" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="User_Setting.aspx.cs" Inherits="IMS_PowerDept.CentralStore.User_Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<ul class="breadcrumb">
  <li><a href="#">ADMINISTRATOR PANEL</a></li>
 
  <li class="active">Chargeable Heads</li>
</ul> <br />--%>
    <div class="full_w">
        <asp:Panel ID="panelSuccess" runat="server" CssClass="n_ok" Visible="false">
            <p>
                <asp:Label ID="lblSuccess" runat="server" Text="success message"></asp:Label></p>
        </asp:Panel>

        <asp:Panel ID="panelError" runat="server" CssClass="n_error" Visible="false">
            <p>
                <asp:Label ID="lblError" runat="server" Text="error message"></asp:Label></p>
        </asp:Panel>
        <div class="h_title">Change Password</div>
<div style="margin:0px auto;padding:10px">
<table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    
                                    <tbody>
                                      
                                         
                                        <tr class="odd gradeA">
                                            <td>Username</td>
                                           <td> <asp:TextBox Width="300px" CssClass="form-control"  ID="_tbUsername" runat="server"></asp:TextBox>

                                           </td>
                                           
                                        </tr>
                                        <tr class="even gradeA">
                                            <td>Password</td>
                                           <td> <asp:TextBox Width="300px" TextMode="Password" CssClass="form-control" ID="_tbPassword" runat="server"></asp:TextBox>

                                           </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
<asp:Button ID="_btnSave" OnClick="_btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Update Password" />
                     <asp:Button ID="_btnCancel" OnClick="_btnCancel_Click" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                      
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
    </div>
        </div>
</asp:Content>
