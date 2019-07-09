<%@ Page Title="Job Entries List" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="JobEntriesList.aspx.cs" Inherits="IMS_PowerDept.Transformers.JobEntriesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
     <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="full_w">

				<div class="h_title">List Job Entries</div>
<div style="margin:0px auto;padding:10px">
    

     <p class="element2">
            Search by Challan Number

        </p>
 <p class="element2 input-group custom-search-form" style="width: 80%;">
            <span style="float: left; width: 55%">
                <input type="text" class="form-control" id="_txtsearch" runat="server" style="height: 22px; width:385px" placeholder="Search Keyword..." />
            </span>
            <span class="input-group-btn" style="float: left; height: 50px; width: 35px;">
                <button runat="server" id="btnSearchImage" onserverclick="btnSearchImage_ServerClick" class="btn btn-default" title="click to search" type="button" style="height: 36px; width: 35px; border-top-left-radius: 0px; border-bottom-left-radius: 0px;">
                    <i class="fa fa-search"></i>
                </button>
            </span>
    
            <span style="float: left; padding-left: 50px;">


                
            </span>
        </p>


   

        
      
     <div style="width: 860px; overflow: auto;">
            
          <table  class="table table-striped table-bordered table-hover" style="width:750px; " >
           <thead>
            <tr>
                <td>Challan ID</td>
                    <td>Challan Date</td>   
             <td>Repair Firm</td>

                <td>Action</td>
             </tr>
             </thead>
       
            <asp:ListView ID="_rprt" runat="server">
             <ItemTemplate>
                
                 <tr>
                     <td>
                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ChallanNo")%>'></asp:Label>
                     </td>
                     <td> <asp:Label ID="cHEad" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ChallanDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                       

                     </td>
          
                 
                     <td>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.RepairFirm")%>'></asp:Label>


                         
                     </td>
                     <td>
                                                 
                           <a href='<%# "JobEntryDetails.aspx?challanno="+Eval("ChallanNo") %>' class="table-icon archive" title="View Details"></a>
                     </td>
                 </tr>
              
             </ItemTemplate>
         </asp:ListView>
                </table>
         </div>

</div>
        </div>
                







    
</asp:Content>
