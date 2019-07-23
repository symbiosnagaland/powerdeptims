<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="IssuedEntryEdit.aspx.cs" Inherits="IMS_PowerDept.Admin.IssuedEntryEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>

    <script type="text/javascript">
        /*$(function () {
            $("#ContentPlaceHolder1_tbSupplyDate").datepicker();
            $("#ContentPlaceHolder1_tbOTEODate").datepicker();
        });*/

        $(function () {
           
            $("#ContentPlaceHolder1__tbChallanDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd-mm-yy'
                });
            $("#ContentPlaceHolder1__tbIntendDate").datepicker(
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
<script type="text/javascript">

    function SetUnitName(itemid) {
        var e = document.getElementById(itemid);
        var itemsvalue = e.options[e.selectedIndex].value;
        var mySplitResult = itemsvalue.split(" ");
        /* Store last character of string id of ddlitems */
        // var last_character = itemid[itemid.length - 1];
        var splitItemsID = itemid.split("_");
        //making sure the last digit is not 0
        //fetching the last part of the control id(which is dynamic)
        var dynamicidpart = splitItemsID[splitItemsID.length - 1];
        if (typeof mySplitResult[1] === "undefined") {
            document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = '';
             // document.getElementById('ContentPlaceHolder1_gvItems_lblUnit_' + dynamicidpart).textContent = '';
             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = '';
         }
         else {
             document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = mySplitResult[1];
             // document.getElementById('ContentPlaceHolder1_gvItems_lblUnit_' + dynamicidpart).textContent = '';
             //SAVING item id for saving to db also
             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = mySplitResult[0];
         }
     }
     //NOW MAKE SURE CONTROL IDs in the page are not changed since all these are dependent on them
     function UpdateAmountbyRate(rateid) {
         // var last_character = itemid[itemid.length - 1];
         var splitItemsID = rateid.split("_");
         //making sure the last digit is not 0
         //fetching the last part of the control id(which is dynamic)
         var dynamicidpart = splitItemsID[splitItemsID.length - 1];
         //update amount textbox for this row
         //amount =rate * quantity
         document.getElementById("<%= gvItems.ClientID%>_tbAmount_" + dynamicidpart).value = (document.getElementById("<%= gvItems.ClientID%>_tbRate_" + dynamicidpart).value) * (document.getElementById("<%= gvItems.ClientID%>__tbQuantity_" + dynamicidpart).value);
         calculateSum();
     }

     function UpdateAmountbyQuantity(quantityid) {
         // var last_character = itemid[itemid.length - 1];
         var splitItemsID = quantityid.split("_");
         //making sure the last digit is not 0
         //fetching the last part of the control id(which is dynamic)
         var dynamicidpart = splitItemsID[splitItemsID.length - 1];
         document.getElementById("<%= gvItems.ClientID%>_tbAmount_" + dynamicidpart).value = (document.getElementById("<%= gvItems.ClientID%>_tbRate_" + dynamicidpart).value) * (document.getElementById("<%= gvItems.ClientID%>__tbQuantity_" + dynamicidpart).value);
         //updating the main total amount also
         //ContentPlaceHolder1_gvItems__tbtotalAmount =
         calculateSum();
     }

     function calculateSum() {
         var sum = 0;

         // Get the gridview
         var grid = document.getElementById("<%= gvItems.ClientID%>");
         //ContentPlaceHolder1_ReceiveItems_gvItems

         // Get all the input controls (can be any DOM element you would like)
         var inputs = grid.getElementsByTagName("input");

         // Loop through all the DOM elements we grabbed
         for (var i = 0; i < inputs.length; i++) {

             // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
             if (inputs[i].name.indexOf("tbAmount") > 1) {
                 if (inputs[i].value != "") {
                     sum = sum + parseFloat(inputs[i].value);
                 }

             }
         }
         document.getElementById("<%= gvItems.ClientID%>_tbtotalAmount").value = parseFloat(document.getElementById("ContentPlaceHolder1_tbtotalAmountAddedItems").value) + sum;
      }


      function SetTarget() {
          document.forms[0].target = "_blank";
      }
    </script>
<%--content starts   here--%> 
     <div class="full_w">
          <asp:Panel ID="panelSuccess" Visible="false"   CssClass="n_ok" runat="server">
      
      <p>  <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>            
      </p>
    </asp:Panel>

    <asp:Panel ID="panelError" Visible="false"  CssClass="n_error" runat="server">
     <p>  
       <asp:Label ID="lblError" runat="server" Text=""></asp:Label>          
     </p> 
    </asp:Panel>

				<div class="h_title">Issued Items Entry Edit</div>
<div style="margin:0px auto;padding:10px">
    
   <div class="half_w half_left">
            <div class="h_title">Challan No. / Date(dd-mm-yyyy) </div>
            <div style="margin: 0px auto; padding: 10px">
                <asp:TextBox CssClass="form-control" ID="_tbChalanNo" placeholder="Challan No" Width="280px" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox CssClass="form-control" ID="_tbChallanDate" placeholder="Challan Date" Width="280px" runat="server"></asp:TextBox>

            </div>

        </div>

        <div class="half_w half_right">
            <div class="h_title">Indent Number / Date(dd-mm-yyyy)</div>
            <div style="margin: 0px auto; padding: 10px">
                <asp:TextBox CssClass="form-control" ID="_tbIndentValue" placeholder="Indent Number" Width="280px" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox CssClass="form-control" ID="_tbIntendDate" placeholder="Indent Date" Width="280px" runat="server"></asp:TextBox>

            </div>

        </div>


    <div class="clear"></div>
        <div class="entry">

            <span style="float: left;width:155px"">
              <label>Indenting Division Selected :</label>
            </span>  
            
            <span style="float: left;padding-left: 10px; width:200px">
       <asp:Label ID="lblDivisionOld" runat="server"></asp:Label>
            </span>
                <span style="float: left;padding-left: 10px;">  
                                <label for="comments">Select New only if you require update :</label>                   
                      <asp:DropDownList CssClass="err" ID="_ddIntendDivisions" Width="250px" runat="server">
            </asp:DropDownList>
                </span>
          
        </div>
      <div class="clear"></div>
        <div class="entry">
                <span style="float: left;width:155px"">
              <label for="comments">Chargeable Head Selected:</label> 
            </span>  
            
            <span style="float: left;padding-left: 10px; width:200px">
          <asp:Label ID="lblChHeadOld" runat="server"></asp:Label>
            </span>
                <span style="float: left;padding-left: 10px;"> 
                           <label for="comments">Select New only if you require update :</label> 
            <asp:DropDownList CssClass="err" ID="_ddCHead" Width="250px" runat="server">
            </asp:DropDownList>
                </span> 
        </div>

        <div class="element2">
            <asp:CheckBox ID="istemporary"  Text=" Select if Issued Entry is Temporary" runat="server" />


                    



                 
        
        </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="element2">
        
           
        <h2>Issued Items</h2>

        <asp:GridView ID="gvItems_Edit" runat="server" OnRowDataBound="gvItems_RowDataBound"  AutoGenerateColumns="False" CssClass="table table-bordered" BackColor="White" DataSourceID="sds_gvitemsedit"  OnRowCommand="gvItems_Edit_RowCommand" >
                                                <Columns>
                                                  
                                                       <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate><%# Container.DataItemIndex+1 %>.
                                                               </ItemTemplate>
                                                        
                                                       
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                              <asp:HiddenField ID="hdnFieldItemID" runat="server" />
                                                     
                                                            <asp:Label ID="lblItem" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterStyle HorizontalAlign="Right" />
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" Text='<%# Eval("unit") %>' runat="server"></asp:Label>
                                                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" Text='<%# Eval("quantity") %>' runat="server"></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Issue Head">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblIhead" Text='<%# Eval("IssueHeadName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" Text='<%# Eval("rate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                     <%--    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit"  CommandArgument='<%# Eval("DeliveryItemDetailsID") %>' CommandName="edit" ToolTip='<%# Eval("amount") %>' runat="server">Edit Quantity</asp:LinkButton>                                                                                                           
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    --%>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete"  CommandArgument='<%# Eval("DeliveryItemDetailsID") %>' CommandName='<%# Eval("amount") %>' runat="server">Delete</asp:LinkButton>                                                                                                           
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
        <asp:SqlDataSource ID="sds_gvitemsedit" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [DeliveryItemDetailsID], [ItemName], [IssueHeadName], [Quantity], [Unit], [Rate],(quantity* rate) as amount FROM [DeliveryItemsDetails] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)" >
    
            <SelectParameters>
                <asp:QueryStringParameter Name="DeliveryItemsChallanID" QueryStringField="challanid" Type="Decimal" />
            </SelectParameters>
     
        </asp:SqlDataSource>
        <h2> Insert New Issued Items</h2>

          <div class="entry">
                 <span style="float: left; padding-left:11px;">
            <label for="comments">Enter Number of Items Rows to use</label></span>  <span style="float: left;padding-left: 10px;">
            <asp:TextBox CssClass="form-control" ID="tbItemsRows" TextMode="Number" Text="10" Width="30px" runat="server"></asp:TextBox></span>
                <span style="float: left;padding-left: 10px;"> <asp:Button ID="btnRowsAdd" class="btn btn-outline btn-primary" ToolTip="This will create the total number of rows to be used for adding items. By default, 10 rows are always displayed." runat="server" Text="CREATE ROWS" OnClick="btnRowsAdd_Click" />

                     <asp:Button ID="_btnCancel" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
                </span>

                 
        </div>
        </div>
    <div class="element clear"></div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                            <asp:GridView EmptyDataText="Empty Rows" ShowHeader="true" ID="gvItems" OnRowDataBound="gvItems_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" BackColor="White">
                <Columns>
                    <asp:TemplateField HeaderText="Items">
                        <ItemTemplate>
                            <asp:DropDownList ID="_ddItems"  OnSelectedIndexChanged="_ddItems_SelectedIndexChanged"  AutoPostBack="true" AppendDataBoundItems="false" CssClass="err" Width="250px" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:TextBox Width="60px" BorderColor="Transparent" BackColor="Transparent" ID="_tbUnit" runat="server">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
            
               <%--     <asp:TemplateField HeaderText="I Head">
                        <ItemTemplate>
                            <asp:DropDownList ID="_ddIhead" OnSelectedIndexChanged="_ddIhead_SelectedIndexChanged" AppendDataBoundItems="false" AutoPostBack="true" CssClass="err" Width="170px" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                      <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                           <asp:DropDownList CssClass="err" ID="ddlRates" Width="150px" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                   --%>


                    <asp:TemplateField HeaderText="Issue Head : Rate : Net Balance">
                        <ItemTemplate>
                          <asp:DropDownList ID="ddlIheadRateActualBalance"  OnSelectedIndexChanged="ddlIheadRateActualBalance_SelectedIndexChanged" AppendDataBoundItems="false" AutoPostBack="true" CssClass="err" Width="220px" runat="server">
                            </asp:DropDownList>
                              <asp:HiddenField ID="hdnSelectedIssueHead" runat="server" />
                            <asp:HiddenField ID="hdnSelectedRate" runat="server" />                            
                        </ItemTemplate>
                    </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox Width="60px" CssClass="form-control" ID="_tbQuantity" AutoComplete="off" runat="server" BorderStyle="Solid" BorderWidth="1px">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
                                           </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="_btnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>


       <div class="element">
           <span style="float:left; width: 111px;"> <label for="comments">VEHICLE NUMBER</label></span>    
            <span style="float:left; padding-left: 11px;";> <asp:TextBox CssClass="form-control" ID="tbVehicleNumberCaps"  AutoComplete="off"   style="text-transform:uppercase"  placeholder="VEHICLE NUMBER" Width="280px" runat="server"></asp:TextBox>
        </span></div>

        <div class="element">
              <span style="float:left; width: 111px;">    <label  for="comments">Receiver's Designation</label></span>
          <span style="float:left; padding-left: 11px;";>   <asp:TextBox CssClass="form-control" ID="tbReceiverDesignation" style="text-transform:capitalize" placeholder="Receiver's Designation" AutoComplete="off" Width="280px" runat="server"></asp:TextBox>
       </span> </div>

     <div class="element">
              <span style="float:left; width: 111px;">    <label  for="comments">Remarks</label></span>
          <span style="float:left; padding-left: 11px;";>  
               <asp:TextBox CssClass="form-control" ID="tbremarks" TextMode ="MultiLine"  style="text-transform:capitalize" placeholder="Remarks" AutoComplete="off" Width="280px" runat="server"></asp:TextBox>
       </span> </div>

                 <br />
                <br />
    
    <div class="entry">
            <p>


                <asp:Button ID="_btnUpdate" CssClass="btn btn-primary"  runat="server" Text="UPDATE" Click="_btnUpdate_Click" OnClick="_btnUpdate_Click" />

            <asp:Button ID="_btnSave" CssClass="btn btn-primary" Visible="false" runat="server" Text="UPDATE & PRINT ISSUED ENTRY" OnClick="_btnSave_Click" />
                
                 

                <asp:Button ID="btnReset" CssClass="btn btn-danger" Visible="false" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
                  <asp:Button ID="_btnDelete" Visible="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="_btnDelete_Click" />
           </p>
        </div>

     <asp:TextBox ID="_tbtotalAmount" Visible="false" Text="" runat="server"></asp:TextBox>
              
        <asp:HiddenField ID="hdnFieldChallanID" runat="server" />
              
        </div>
</div>
</asp:Content>
