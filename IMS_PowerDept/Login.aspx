<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IMS_PowerDept.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Login</title><link rel="stylesheet" type="text/css" href="css/login.css" media="screen" />
    </head>
    
    <body>
        
        <form id="form1" runat="server">

            <div id="content">
                <div id="main">

                    <div class="full_w">
                        <h2>Power Department Nagaland</h2>
                        <h2>Inventory Management System - User Login</h2>
                        
                        <hr />
                        <br />
                        
                        <div class="element">
                            <label for="login">Username:</label>
                            <asp:TextBox ID="inputUsername"  autocomplete="off" placeholder="Username" Text="administrator"  Width="180px" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        
                        <br />
                        
                        <label for="pass">Password:</label>
                        <asp:TextBox ID="inputPassword" Width="180px" placeholder="Password" autocomplete="off" Text="tingten" TextMode="Password"   CssClass="form-control"  runat="server"></asp:TextBox>
                        <!-- TextMode="Password" -->
                        <div class="sep"></div>
                        
                        <button type="submit" runat="server" onserverclick="Unnamed_ServerClick" class="ok">Login</button> 
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Label ID="Label1" runat="server"  ForeColor="Red"></asp:Label>
                    </div>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <div class="footer">&nbsp;<a  href="#">Power Dept. IMS &copy 2014 - <%Response.Write(DateTime.Now.Year.ToString()); %></a> | Powered by <a  href="#">SymBios Soft Tech</a></div>

                </div>
            </div>

        </form>
    </body>

</html>
