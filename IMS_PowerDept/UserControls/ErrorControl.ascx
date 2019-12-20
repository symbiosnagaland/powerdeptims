<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.ErrorControl" %>


	
	<div id="content">
	
		<div id="main">
			<div class="clear"></div>
			

            <%--<ul class="breadcrumb">
  <li><a href="#">ADMINISTRATOR PANEL</a></li>
 
  <li class="active">Error Page</li>
</ul>--%>


      <div class="full_w">
           <asp:Panel ID="_pnlSucc" Visible="false" runat="server" CssClass="n_ok">
               <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
           </asp:Panel>
    <asp:Panel ID="_pnlError" Visible="false" runat="server" CssClass="n_error">
         <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </asp:Panel>
          
       
          	<div class="h_title">Error Page</div>
          
			
               <div class="col-lg-4">
                    <div class="panel panel-danger">
                      
                        <div class="panel-body">
                            <p>
                                
                                Oops! Some unexpected error has occurred.
                                Click <a href="javascript: history.go(-1)" > here </a> to go to previous page and try again.
                                  </p>

                                <p>Or go to Login Page.
                              </p>
                                
                          <%--         <p>If the same error persists, you may also click <asp:LinkButton ID="lnkbtnMailErrorDetails" runat="server">here</asp:LinkButton> &nbsp;to send us the error details (*requires internet connection) 
                                </p>--%>
                                <p>

                                    <strong>
                                 <%-- SymBios Development Team--%>
</strong><br />

                                    
</p>

                          

                            <p>&nbsp;</p>
                            <p>
                                
                                            <label class="control-label" for="inputError">Exception Details:</label>
                                            
                                     <asp:TextBox ID="tbErrorDetails" TextMode="MultiLine" Visible="false" CssClass="form-control" runat="server" Height="400px" Width="100%"></asp:TextBox>
                                       
                                </p>
                        </div>
                      
                    </div>
                </div>

<div style="margin:0px auto;padding:10px">
    
</div>
  
    </div>












   


		
		</div>
		<div class="clear"></div>
	</div>

	<div id="footer">
		<div class="left"></div>
			<p>Powered by <a href="http://symbiostech.in" target="_blank">SymBios Soft Tech Pvt. Ltd.</a>	<span style="float:right">
			<a href="#"> Power Department Nagaland IMS 2014</a>
		</span>  </p>
		
	
	</div>
