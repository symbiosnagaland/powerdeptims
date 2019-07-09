<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="IMS_PowerDept.CentralStore.Error" %>

<%@ Register Src="~/UserControls/ErrorControl.ascx" TagPrefix="uc1" TagName="ErrorControl" %>
<%@ Register Src="~/UserControls/menuPartial.ascx" TagPrefix="uc1" TagName="menuPartial" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
          <link rel="stylesheet" type="text/css" href="../css/style.css" media="screen" />
<link rel="stylesheet" type="text/css" href="../css/navi.css" media="screen" />
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
 
<div class="wrap">
	<div id="header">
		<div id="top">
			<div class="left">
			<h2 style="color:#fff; ">POWER DEPARTMENT NAGALAND - Inventory Management System</h2>
			</div>
			<div class="right">
				<div class="align-right">
				</div>
			</div>
		</div>
		<div id="nav">
           
			<ul>
   
                <uc1:menuPartial runat="server" ID="menuPartial" />

   

   
				
			</ul>
		</div>
	</div>

       <%--breadcrumbs--%>
                 
	
    <uc1:ErrorControl runat="server" ID="ErrorControl" />

	
</div>
    </form>
</body>
</html>
