<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="IssuedEntryEdit.aspx.cs" Inherits="IMS_PowerDept.Admin.IssuedEntryEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>


    <style type="text/css">
  .hiddencol
  {
    display: none;
  }
</style>


    <script type="text/javascript">
       
        $(function () {
           
            $("#ContentPlaceHolder1__tbChallanDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
            $("#ContentPlaceHolder1__tbIntendDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                });
        });

</script>

<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style> 

<script type="text/javascript">
   

    //NOW MAKE SURE CONTROL IDs in the page are not changed since all these are dependent on them
    function UpdateAmountbyQTY(QTY) {

        var splitItemsID = QTY.split("_");
        var dynamicidpart = splitItemsID[splitItemsID.length - 1];


        document.getElementById("<%= gvItems.ClientID%>__tbAmt_" + dynamicidpart).value = document.getElementById("<%= gvItems.ClientID%>_tbRate_" + dynamicidpart).value * document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).value;
        //alert("hi");

        var EnteredQuantity = document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).value;
        var OriginalMaxQuantity = document.getElementById("<%= gvItems.ClientID%>_tbMaxQtyAvail_" + dynamicidpart).value;
        if (parseFloat(EnteredQuantity) > parseFloat(OriginalMaxQuantity)) {
            document.getElementById("<%= gvItems.ClientID%>_tbMaxQtyAvail_" + dynamicidpart).style.backgroundColor = "pink";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "pink";
            document.getElementById("<%= gvItems.ClientID%>").style.border = "red 2px outset";
            document.getElementById('<%=_btnUpdate.ClientID %>').disabled = true;
            document.getElementById('<%=_btnSave.ClientID %>').disabled = true;

        }
        else {
            document.getElementById("<%= gvItems.ClientID%>_tbMaxQtyAvail_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>").style.border = "none";
            document.getElementById('<%=_btnUpdate.ClientID %>').disabled = false;
            document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
        }



        var EnteredQuantity = document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).value;
        var OriginalQuantity = document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).value;
        if (parseFloat(EnteredQuantity) > parseFloat(OriginalQuantity)) {
            document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).style.backgroundColor = "pink";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "pink";
           // alert("Check Quantity");
        }
        else {
            document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "white";
            //alert("OK Quantity");
        }

       
        calculateQtySum();
    }

    function calculateQtySum() {
        var sum = 0;
        var grid = document.getElementById("<%= gvItems.ClientID%>");

        var inputs = grid.getElementsByTagName("input");
        // Loop through all the DOM elements we grabbed
        for (var i = 0; i < inputs.length; i++) {

            if (inputs[i].name.indexOf("_tbOrderQuantity") > 1) {
                if (inputs[i].value != "") {
                    sum = sum + parseFloat(inputs[i].value);
                }
            }


        }
        // document.getElementById("<%= gvItems.ClientID%>tbtotalAmount").value = sum;
        document.getElementById("ContentPlaceHolder1_gvItems__tbTotalAmt").value = sum;

        calculateTotAmountSum();
    }


    function calculateTotAmountSum() {
        var sum = 0;
        var grid = document.getElementById("<%= gvItems.ClientID%>");

        var inputs = grid.getElementsByTagName("input");
        // Loop through all the DOM elements we grabbed
        for (var i = 0; i < inputs.length; i++) {

            if (inputs[i].name.indexOf("_tbAmt") > 1) {
                if (inputs[i].value != "") {
                    sum = sum + parseFloat(inputs[i].value);
                }
            }

        }
       
        document.getElementById("ContentPlaceHolder1_gvItems__tbTotalAmt").value = sum;
    }

    function CheckRepetingItems()
    {
        var grid = document.getElementById("<%= gvItems.ClientID%>");
       var rCount = grid.rows.length;



       for (var rowIdx = 1; rowIdx < rCount - 1; rowIdx++) {
           var Itemid1 = grid.rows[rowIdx].cells[0].getElementsByTagName("*")[0].value;
           var IssueHead1 = grid.rows[rowIdx].cells[2].getElementsByTagName("*")[0].value;
           for (var rowIdx1 = rowIdx + 1; rowIdx1 < rCount - 1; rowIdx1++) {
               //alert(rowIdx1);
               var Itemid2 = grid.rows[rowIdx1].cells[0].getElementsByTagName("*")[0].value;
               var IssueHead2 = grid.rows[rowIdx1].cells[2].getElementsByTagName("*")[0].value;


               if ((Itemid1 != "") && (IssueHead1 != 0)) {
                   if ((Itemid1 == Itemid2) && (IssueHead1 == IssueHead2)) {
                       //alert("Same Item and same Issue Head Not Allowed");
                       document.getElementById('<%=_btnUpdate.ClientID %>').disabled = true;
                       document.getElementById('<%=_btnSave.ClientID %>').disabled = true;   
                       grid.style.border = "2px solid red";

                       return true;
                   }
                   else
                   {
                       document.getElementById('<%=_btnUpdate.ClientID %>').disabled = false;
                       document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
                       grid.style.border = "none";
                   }

               }


           }

       }
       return false;

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
                <asp:HiddenField ID="hdnFieldChallanNotoEdit" runat="server" />
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

                                                              <asp:Label ID="hdnFieldItemID" Text='<%# Eval("ItemId") %>'   runat="server" />
                                                     
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


                                                     
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete"  CommandArgument='<%# Eval("DeliveryItemDetailsID")+","+Eval("ItemName")+","+ Eval("rate")+","+Eval("IssueHeadName")+","+Eval("quantity")%>' CommandName='<%# Eval("amount") %>' runat="server">Delete</asp:LinkButton>                                                                                                           
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
        <asp:SqlDataSource ID="sds_gvitemsedit" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [DeliveryItemDetailsID],[DeliveryItemsChallanID],[ItemId], [ItemName], [IssueHeadName], [Quantity], [Unit], [Rate],(quantity* rate) as amount FROM [DeliveryItemsDetails] WHERE ([DeliveryItemsChallanID] = @DeliveryItemsChallanID)" >
    
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
                                                     <asp:GridView ShowFooter="true" EmptyDataText="Empty Rows" ShowHeader="true" ID="gvItems" OnRowDataBound="gvItems_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" BackColor="White"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Items">
                                <ItemTemplate>
                                    <asp:HiddenField ID="_hdnFieldItemID" runat="server" />
                                    <asp:DropDownList ID="_ddItems" OnSelectedIndexChanged="_ddItems_SelectedIndexChanged" onchange="calculateQtySum();"   AutoPostBack="true" AppendDataBoundItems="false" CssClass="err" Width="300px" Height="25px" runat="server"></asp:DropDownList>
                                               </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate></FooterTemplate>

                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:TextBox Width="60px" autocomplete="off" BorderColor="Transparent" BackColor="Transparent" ID="_tbUnit" runat="server">

                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Issue Head">
                                <ItemTemplate>
                                       <asp:DropDownList ID="ddlIhead"  OnSelectedIndexChanged="_ddlIhead_SelectedIndexChanged" onchange="if (CheckRepetingItems()) return false;calculateQtySum();"  AppendDataBoundItems="false" AutoPostBack="true" CssClass="err" Width="220px" runat="server">

                                    </asp:DropDownList>
                                    
                                  <%--  <asp:HiddenField ID="hdnSelectedIssueHead" runat="server" />
                                    <asp:HiddenField ID="hdnSelectedRate" runat="server" />--%>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--FOR HIDING COL--%>
                           <%-- FooterStyle-CssClass="hiddencol"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"--%>
                          
                            <asp:TemplateField HeaderText="Rate--Quanitity" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"  >
                                    <ItemTemplate>
                                       <%-- <asp:DropDownList CssClass="err" ID="ddlRates" onchange="UpdateAmountbyRate(this.id)" Width="150px" runat="server"></asp:DropDownList>
                                    --%>
                                        <asp:TextBox ID="tbRate" runat="server" Text="Rate"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="tbQty" runat="server" Text="Qty"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="tbOrderNO" runat="server" Text="ONO"></asp:TextBox>
                                        <asp:TextBox ID="tbAmount" runat="server" Text="Amt"></asp:TextBox>
                                        <asp:TextBox ID="tbMaxQtyAvail" runat="server" Text="Max QTY"></asp:TextBox>


                                    </ItemTemplate>
                                    
                                    <FooterTemplate >
                                        <asp:TextBox ID="tbtotalAmount"  CssClass="form-control" Width="80px" BorderColor="White" Text="0" runat="server"></asp:TextBox>
                                    </FooterTemplate>

                                </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox Width="60px" autocomplete="off"  CssClass="form-control" ID="_tbOrderQuantity"  onchange="UpdateAmountbyQTY(this.id);"  runat="server" BorderStyle="Solid" BorderWidth="1px"> </asp:TextBox>
                                </ItemTemplate>
                                    
                                <FooterTemplate >
                                    <asp:TextBox Width="60px" autocomplete="off"  CssClass="form-control" ID="_tbTotalQty" OnTextChanged = "OnTextChanged"  onchange="UpdateAmountbyQTY(this.id);"  runat="server"  BorderStyle="Solid"  BorderWidth="1px" > </asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Amount" FooterStyle-CssClass="hiddencol"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol">
                                <ItemTemplate>
                                    <asp:TextBox Width="60px" autocomplete="off"  CssClass="form-control" ID="_tbAmt" runat="server" BorderStyle="Solid" BorderWidth="1px"> </asp:TextBox>
                                </ItemTemplate>
                                 <FooterTemplate >
                                     <asp:TextBox Width="60px" autocomplete="off" BackColor ="pink"  CssClass="form-control" ID="_tbTotalAmt" runat="server" BorderStyle="Solid" BorderWidth="1px"> </asp:TextBox>
                                 </FooterTemplate>
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
