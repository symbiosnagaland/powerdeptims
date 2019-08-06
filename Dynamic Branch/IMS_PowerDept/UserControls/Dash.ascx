<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dash.ascx.cs" Inherits="IMS_PowerDept.UserControls.Dash" %>
   <div style="margin:0px auto; padding:12px; width:800px">
        
  <div class="half_w half_left" style=" margin-left:20px">
				<div class="h_title">Master Data Statistics</div>
					<script src="js/highcharts_init.js"></script>
					<div id="container" style="min-width: 300px;  margin: 0 auto; width: 330px;"></div>
					<div class="stats" style="height:auto">
					<div class="today">
						<h3><a href="Divisions.aspx">Divisions</a></h3>
                        <asp:Label ID="div" CssClass="count" runat="server" Text=""></asp:Label>
                    
                          <br /><br />
                        
                       <h3><a href="IssueHead.aspx">  Issue Heads</a></h3>
						<asp:Label ID="headI" CssClass="count" runat="server" Text=""></asp:Label>
						
                      
					</div>
					<div class="week">
						<h3><a href="ChargeableHeads.aspx">Chargeable Heads</a></h3>
						<asp:Label ID="chhead" CssClass="count" runat="server" Text=""></asp:Label>
                          <br /><br />
                        
                         <h3><a href="ManageItems.aspx">Items</a></h3>
						<asp:Label ID="item" CssClass="count" runat="server" Text=""></asp:Label>
						
					</div>
				</div>

   
			</div>
      
			<div class="half_w half_right" style="margin-left:20px;">
			<div class="h_title"> Transformer Management</div>
				<div class="stats" style="height:130px">
					   <a href="~/Transformers/Dashboard.aspx" runat="server" title="go to Transformer Management Module"  >
                           <span style="float:left;">
                   <img src="../img/Power-Transformer.jpg" style="border:none; height:130px" />
                               </span>
                           <div style="text-align:center; padding-top:44px;">
                          <h3>Transformer Management Module</h3>
                               </div>
          </a>
                    
					</div>
				</div>

          <div class="half_w half_left" style=" margin-left:20px">
       	<div class="h_title"> Entries Statistics</div>
				<div class="stats" style="height:130px">
					<div class="today">
						<h3><a href="IssueEntry.aspx">Issued Entries</a></h3>
                        <asp:Label ID="issueditems" CssClass="count" runat="server" Text=""></asp:Label>
                        <%--&#8377;--%>
                         <br />
                        <br />
                        
                        <%--<h3><a href="IssueEntry.aspx">Total Amount</a></h3>
                          <asp:Label ID="Label2" CssClass="count" runat="server" Text="&#8377"></asp:Label>
						<asp:Label ID="issamount" CssClass="count" runat="server" Text=""></asp:Label>--%>

					</div>
                    <div class="week">
                        <h3><a href="ReceivedItems.aspx">Received Entries</a></h3>
                        <asp:Label ID="ttrcitm" CssClass="count" runat="server" Text=""></asp:Label>
                    </div>
					</div>

             </div>


    </div>

