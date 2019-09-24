<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="IMS_PowerDept.Admin.Dashboard" %>

<%@ Register Src="~/UserControls/Dash.ascx" TagPrefix="uc1" TagName="Dash" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <uc1:Dash runat="server" ID="Dash" />
     
   

      <div style="margin:0px auto; padding:12px; width:800px">
    
			<div class="half_w half_left" style=" margin-left:20px">
				<div class="h_title">Users Statistics</div>
					
					<div id="" style="min-width: 300px;  margin: 0 auto; width: 330px;"></div>
					<div class="stats" style="height:auto">
					<div class="today">
                      
						<h3><a href="User_New.aspx">Users</a></h3>
						<asp:Label ID="Label1" CssClass="count" runat="server" Text=""></asp:Label>
					</div>
				</div>
			</div>
          </div>
</asp:Content>
