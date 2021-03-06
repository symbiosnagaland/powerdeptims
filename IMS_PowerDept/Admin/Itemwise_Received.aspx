﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Itemwise_Received.aspx.cs" Inherits="IMS_PowerDept.Admin.Itemwise_Received" %>

<%@ Register Src="~/UserControls/Receipt_Report_Excel.ascx" TagPrefix="uc1" TagName="Receipt_Report_Excel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- 
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
    --%>
    <script type="text/javascript">

        $(function () {
            $("#ContentPlaceHolder1_Receipt_Report_Excel_tbStartDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
            $("#ContentPlaceHolder1_Receipt_Report_Excel_tbEndDateSearch").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
        });

    </script>
    
    <style type="text/css">
        .ui-datepicker { font-size:8pt !important}       
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Receipt_Report_Excel runat="server" id="Receipt_Report_Excel" />  
        
</asp:Content>