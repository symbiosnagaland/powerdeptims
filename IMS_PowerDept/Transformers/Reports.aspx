<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IMS_PowerDept.Transformers.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />

   <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
<script type="text/javascript">
    $(function () {

        $("#ContentPlaceHolder1_tbBeginDate").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'yy-mm-dd'
            
        });
        $("#ContentPlaceHolder1_tbEndDate").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'yy-mm-dd'
        });
    });
</script>

     <script>
         $(function () {
             $("#datepicker").datepicker({
                 changeMonth: true,
                 changeYear: true
             });
         });
  </script>

   

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="full_w">
    <div class="h_title">Transformer Reports</div>
    <div style="margin: 0px auto; padding: 10px">

            <asp:Panel ID="panelSuccess" Visible="false"  runat="server" CssClass="n_ok">
    <p>    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label></p>
    </asp:Panel>

    <asp:Panel ID="panelError" Visible="false"  runat="server" CssClass="n_error">
    <p><asp:Label ID="lblError" runat="server" Text=""></asp:Label></p>
    </asp:Panel>

        <div class="element2">
           <span style="float:left; padding-right:44px;">
            <label class="label" for="comments">Begin Date*</label>
            <asp:TextBox CssClass="form-control" ID="tbBeginDate" placeholder="Date" Width="207px" runat="server"></asp:TextBox>
      </span>
            <span>
             <label class="label" for="comments">End Date*</label>
           <asp:TextBox CssClass="form-control" ID="tbEndDate" placeholder="Date" Width="207px" runat="server"></asp:TextBox>
           </span>
        </div>

        <div style="height: 150px">&nbsp;</div>
		

        
                <div class="err" style="width:100%">
          
                <div class="err" style="width:30%; float:left; padding-left:11px;">
                                  <asp:Button ID="btnReceipt" CssClass="btn btn-primary" runat="server" Text="Receipt Entries Report" Height="42px" OnClick="btnReceipt_Click"  />
                </div> 

    
                   <div style="width:30%; float:left;">

            <asp:Button ID="btnJobs" CssClass="btn btn-info" runat="server" Text="Jobs Entries Report" Height="42px" OnClick="btnJobs_Click"  />
              

            </div> 
                 <div style="width:30%; float:left;">

                  <asp:Button ID="btnIssues" CssClass="btn btn-primary" runat="server" Text="Issues Entries Report" Height="42px" OnClick="btnIssues_Click"  />
                </div> 
     
        </div>
          <div style="height: 100px">&nbsp;</div>
		
     <div class="element2">
     
            <label  for="comments">*Report page will open in new tab. Pop up page must be allowed in the browser if new tab doesn't open.(Select Always Allow Popups on the right hand side of the URL address bar)</label>
           
   
           
        </div>

    </div>
    </div>

    
</asp:Content>
