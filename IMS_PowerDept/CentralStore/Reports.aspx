<%@ Page Title=" Run Valuation Reports" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IMS_PowerDept.CentralStore.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />



<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#ContentPlaceHolder1_tbStartDateSearch").datepicker(
            {
                changeMonth: true,
                changeYear: true
            });

        $("#ContentPlaceHolder1_tbEndDateSearch").datepicker(
            {
                changeMonth: true,
                changeYear: true
            });

        $("#ContentPlaceHolder1_chdate").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });
        $("#ContentPlaceHolder1_chdateend").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });


        $("#ContentPlaceHolder1_stratdiv").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });
        $("#ContentPlaceHolder1_enddiv").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });


        $("#ContentPlaceHolder1_valStart").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });
        $("#ContentPlaceHolder1_valEnd").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });

        $("#ContentPlaceHolder1_TextBox1").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });
        $("#ContentPlaceHolder1_TextBox2").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });

        $("#ContentPlaceHolder1_endtbsummary").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });
        $("#ContentPlaceHolder1_tbsummary").datepicker(
             {
                 changeMonth: true,
                 changeYear: true
             });

    });
</script>

    
   
<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>
    <style>
        .accordion,.accordion div,.accordion h1,.accordion p,.accordion a,.accordion img,.accordion span,.accordion em,.accordion ul,.accordion li {
	margin: 0;
	padding: 0;
	border: none;
}

/* Accordion Layout Styles */
.accordion {
	width: 886px;
	padding: 1px 5px 5px 5px;
	/*background: #141517;*/

	-webkit-box-shadow: 0px 1px 0px rgba(255,255,255, .05);
	-moz-box-shadow: 0px 1px 0px rgba(255,255,255, .05);
	box-shadow: 0px 1px 0px rgba(255,255,255, .05);

	-webkit-border-radius: 2px;
	-moz-border-radius: 2px;
	border-radius: 2px;
}

.accordion .tab {
	display: block;
	height: 35px;
	margin-top: 4px;
	padding-left: 20px;
	font: bold 12px/35px Arial, sans-serif;
	text-decoration: none;
	color: #eee;
	text-shadow: 1px 1px 0px rgba(0,0,0, .2);

	-webkit-border-radius: 2px;
	-moz-border-radius: 2px;
	border-radius: 2px;
	background: #6c6e74; /* Old browsers */
background: -webkit-linear-gradient(top, #6c6e74 0%, #4b4d51 100%);

	background: -moz-linear-gradient(top, #6c6e74 0%, #4b4d51 100%);
	background: -o-linear-gradient(top, #6c6e74 0%, #4b4d51 100%);
	background: -ms-linear-gradient(top, #6c6e74 0%, #4b4d51 100%);
	background: linear-gradient(top, #6c6e74 0%, #4b4d51 100%); /* W3C */

-webkit-box-shadow: 0px 1px 0px rgba(0,0,0, .1), inset 0px 1px 0px rgba(255,255,255, .1);
	-moz-box-shadow: 0px 1px 0px rgba(0,0,0, .1), inset 0px 1px 0px rgba(255,255,255, .1);
	box-shadow: 0px 1px 0px rgba(0,0,0, .1), inset 0px 1px 0px rgba(255,255,255, .1);
}

.accordion .tab:hover,.accordion div:target .tab {
	color: #2b3b06;
	text-shadow: 0px 1px 0px rgba(255,255,255, .15);
	background: #a5cd4e; /* Old browsers */
background: -webkit-linear-gradient(top, #a5cd4e 0%, #6b8f1a 100%);

	background: -moz-linear-gradient(top, #a5cd4e 0%, #6b8f1a 100%);
	background: -o-linear-gradient(top, #a5cd4e 0%, #6b8f1a 100%);
	background: -ms-linear-gradient(top, #a5cd4e 0%, #6b8f1a 100%);
	background: linear-gradient(top, #a5cd4e 0%, #6b8f1a 100%); /* W3C */

-webkit-box-shadow: 1px 1px 1px rgba(0,0,0, .3), inset 1px 1px 1px rgba(255,255,255, .45);
	-moz-box-shadow: 1px 1px 1px rgba(0,0,0, .3), inset 1px 1px 1px rgba(255,255,255, .45);
	box-shadow: 1px 1px 1px rgba(0,0,0, .3), inset 1px 1px 1px rgba(255,255,255, .45);
}

.accordion div .content {
	display: none;
	margin: 5px 0;
}

.accordion div:target .content {
	display: block;
}

.accordion > div {
	height: 40px;
	overflow: hidden;

	-webkit-transition: all .3s ease-in-out;
	-moz-transition: all .3s ease-in-out;
	-o-transition: all .3s ease-in-out;
	-ms-transition: all .3s ease-in-out;
	transition: all .3s ease-in-out;
}

.accordion > div:target {
	height: auto;
}

/* Accordion Content Styles */
.accordion .content h1 {
	color: white;
	font: 18px/32px Arial, sans-serif;
}

.accordion .content p {
	margin: 10px 0;
	color: white;
	font: 11px/16px Arial, sans-serif;
}

.accordion .content span {
	font: italic 11px/12px Georgia, Arial, sans-serif;
	color: #4f4f4f;
}

.accordion .content em.bullet {
	width: 5px;
	height: 5px;
	margin: 0 5px;
	background: #6b8f1a;
	display: inline-block;

	-webkit-box-shadow: inset 1px 1px 1px rgba(255,255,255, 0.4);
	-moz-box-shadow: inset 1px 1px 1px rgba(255,255,255, 0.4);
	box-shadow: inset 1px 1px 1px rgba(255,255,255, 0.4);

	-webkit-border-radius: 5px;
	-moz-border-radius: 5px;
	border-radius: 5px;
}

.accordion .content ul li {
	list-style: none;
	float: left;
	margin: 5px 10px 5px 0;
}

.accordion .content img {
	-webkit-box-shadow: 2px 2px 6px rgba(0,0,0, .5);
	-moz-box-shadow: 2px 2px 6px rgba(0,0,0, .5);
	box-shadow: 2px 2px 6px rgba(0,0,0, .5);
}

    </style>
      
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:SqlDataSource ID="sds" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DISTINCT IssueHeadName FROM DeliveryItemsDetails"></asp:SqlDataSource>
  
     <asp:SqlDataSource ID="iheads" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="select distinct IssueHeadName from IssueHeads"></asp:SqlDataSource>
   <%--  "Select issue head" is the name of Issue head(items inventory table)--%>
    
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           
     <asp:Panel ID="panelSuccess" Visible="false" CssClass="n_ok" runat="server">

        <p>
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="panelError" Visible="false" CssClass="n_error" runat="server">
        <p>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>
    <div id="contain">
	<div class="accordion">
        	

		<div id="tab-1">
			<a href="#tab-1" class="tab">Head-Wise Valuation Abstract</a>
			<div class="content">

                <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CheckBox1" EnableViewState="true" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Preview by Issue Head and Date" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td>Select Issue Head</td>
                        <td>

                            <asp:DropDownList ID="issuehead" Enabled="False" Width="250px"  runat="server" DataSourceID="sds" DataTextField="IssueHeadName" DataValueField="IssueHeadName"></asp:DropDownList>
                        </td>
                    </tr>
                <tr>
                    <td>
                        Begin Date
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="tbStartDateSearch" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                    <tr>
                    <td>
                    Ending Date
                    </td>
                    <td>
                          <asp:TextBox CssClass="form-control" ID="tbEndDateSearch"  placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                         <asp:Button ID="btnSearch"  CssClass="btn btn-primary" runat="server" Text="Search Between Dates" OnClick="btnSearch_Click" />
                    </td>
                </tr>   
               
            </table>
</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CheckBox1" 
                    EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
				
           
                     
                  
			</div>
		</div>
		<div id="tab-2">
			<a href="#tab-2" class="tab">Challan-Wise Valuation (Abstract)</a>
			<div class="content">
				     <table style="width:100%">
                <tr>
                    <td>
                        Begin Date
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="chdate" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    Ending Date
                    </td>
                    <td>
                          <asp:TextBox CssClass="form-control" ID="chdateend" placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Preview Challan-Wise Valuation" OnClick="Button1_Click1"  />
                    </td>
                </tr>
            </table>
					
			</div>
		</div>
		<div id="tab-3">
			<a href="#tab-3" class="tab">Details Valuation - ALL</a>
			<div class="content">
					 <table style="width:100%">
                <tr>
                    <td>
                        Begin Date
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="TextBox1" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    Ending Date
                    </td>
                    <td>
                          <asp:TextBox CssClass="form-control" ID="TextBox2" placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Preview" OnClick="Button2_Click"  />
                    </td>
                </tr>
            </table>
			</div>			
		</div>
        <div id="tab-4">
			<a href="#tab-4" class="tab">Details Valuation - By Division</a>
			<div class="content">
              
                               <asp:UpdatePanel ID="upSetSession" runat="server">
            <ContentTemplate>
					<table style="width:100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="ck1" EnableViewState="true" Checked="true" AutoPostBack="true" OnCheckedChanged="ck1_CheckedChanged" Text="Preview by Division and Date" runat="server" />
                            </td>
                             <td>
                                <asp:CheckBox ID="ck2"  EnableViewState="true"  AutoPostBack="true" OnCheckedChanged="ck2_CheckedChanged" Text="Preview by Division , Chargeable Head and Date" runat="server" />
                            </td>
                        </tr>

                        <asp:Panel ID="Panel1"  runat="server">
                             <tr>
                        <td>
                            Division
                        </td>
                        <td>
                            
                 <asp:DropDownList CssClass="err"   EnableViewState="true" AutoPostBack="true" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" AppendDataBoundItems="True" ID="ddldiv" Width="250px" runat="server" DataSourceID="divname" DataTextField="IndentingDivisionName" DataValueField="IndentingDivisionName">
                    </asp:DropDownList>
           
                        
                            <asp:SqlDataSource ID="divname" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString_server %>" SelectCommand="SELECT DISTINCT IndentingDivisionName FROM DeliveryItemsChallan"></asp:SqlDataSource>
                        </td>
                    </tr>

                            <tr>
                                <td>
                                    Chargeable Head
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlchd" Enabled="false" Width="250px" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                
             
                        </asp:Panel>
            </table>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddldiv" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
                <table>
                    <tr>
                    <td>
                        Begin Date
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control"  ID="stratdiv" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                   <tr>
                    <td>
                    Ending Date
                    </td>
                    <td>
                          <asp:TextBox CssClass="form-control" ID="enddiv" placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:Button ID="detailvaluatoinByDivision" CssClass="btn btn-primary" runat="server" Text="Preview Details Valuations" OnClick="detailvaluatoinByDivision_Click" />
                    </td>
                    <td>
                        <asp:Button ID="tempbutton" CssClass="btn btn-primary" runat="server" Text="Preview Temporary Details Valuations" OnClick="tempbutton_Click"
                             />
                    </td>
                </tr>
                    </table>
			</div>			
		</div>
        <div id="tab-5">
			<a href="#tab-5" class="tab">Receipt Valuation</a>
			<div class="content">
					<table style="width:100%">
                     <tr>
                    <td>
                        Begin Date
                    </td>
                    <td colspan="2">
                        <asp:TextBox CssClass="form-control" ID="valStart" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    Ending Date
                    </td>
                   <td colspan="2">
                          <asp:TextBox CssClass="form-control" ID="valEnd" placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                     <tr>
                    <td >
                         <asp:Button ID="receiptvaluation" CssClass="btn btn-primary" runat="server" Text="Receipt Valuation" OnClick="receiptvaluation_Click" />
                    </td>
                         
                </tr>
                </table>
			</div>			
		</div>


         <div id="tab-6">
            <a href="#tab-6" class="tab">Summary of Receipts/Summary of Indents</a>
            <div class="content">
                <table style="width:100%">
  <tr>
                    <td>
                        Begin Date
                    </td>
                    <td colspan="2">
                        <asp:TextBox CssClass="form-control" ID="tbsummary" placeholder="Begin Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    Ending Date
                    </td>
                   <td colspan="2">
                          <asp:TextBox CssClass="form-control" ID="endtbsummary" placeholder="Ending Date" Width="220px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                    <tr>
                         <td >
                         <asp:Button ID="supplierreceipts" CssClass="btn btn-primary" runat="server" Text="Summary of Reciepts" OnClick="supplierreceipts_Click" />
                    </td>
                         <td>
                             <asp:Button ID="Indents" CssClass="btn btn-primary" runat="server" Text="Summary of Indents" OnClick="Indents_Click" />
                         </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="tab-7">
			<a href="#tab-7" class="tab">Balance Sheet</a>
			<div class="content">
                 <table style="width:100%">
                      
                      <tr>
                          <td colspan="3">
                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Generate Balance Sheet" OnClick="Button3_Click" />
                          </td>
                          
                      </tr>
                      <tr>
                          <td>Balance Sheet by Issue Head</td>
                          <td>

                              <asp:DropDownList ID="DropDownList1" Width="250px"  runat="server" DataSourceID="iheads" DataTextField="IssueHeadName" AppendDataBoundItems="True" DataValueField="IssueHeadName" >
                                 <%--  "Select issue head" is the name of Issue head(items inventory table)--%>
                              </asp:DropDownList>
                          </td>
                          <td>
                              <asp:Button ID="Button4" CssClass="btn btn-primary" runat="server" Text="Generate Balance Sheet By Issue Head" OnClick="Button4_Click" />
                          </td>
                      </tr>
                      </table>
                </div>
                </div>
        
	</div>
</div>
    

</asp:Content>
