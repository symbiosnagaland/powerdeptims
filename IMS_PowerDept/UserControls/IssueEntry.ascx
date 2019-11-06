<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueEntry.ascx.cs"  Inherits="IMS_PowerDept.UserControls.IssueEntry" %>

<link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />

<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>

<style type="text/css">
  .hiddencol
  {
    display: none;
  }
</style>





<style type="text/css">
    .ui-datepicker { font-size:8pt !important}
</style>

<script type="text/javascript">


    //NOW MAKE SURE CONTROL IDs in the page are not changed since all these are dependent on them
    function UpdateAmountbyQTY(QTY)
    {
       
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
            document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = 'Check Item available Quantity';
            document.getElementById('<%=save.ClientID %>').disabled = true;
            document.getElementById('<%=_btnSave.ClientID %>').disabled = true;

        }
        else {
            document.getElementById("<%= gvItems.ClientID%>_tbMaxQtyAvail_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>").style.border = "none";
            document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = '';
            document.getElementById('<%=save.ClientID %>').disabled = false;
            document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
        }



        var EnteredQuantity = document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).value;
        var OriginalQuantity = document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).value;
        if (parseFloat(EnteredQuantity) > parseFloat(OriginalQuantity))
        {
            //document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).style.backgroundColor = "pink";
            //document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "pink";
            //alert("Check Quantity");
        }
        else
        {
            document.getElementById("<%= gvItems.ClientID%>_tbQty_" + dynamicidpart).style.backgroundColor = "white";
            document.getElementById("<%= gvItems.ClientID%>__tbOrderQuantity_" + dynamicidpart).style.backgroundColor = "white";
            //alert("OK Quantity");
        }
        calculateQtySum();
    }

    function calculateQtySum()
    {
        var sum = 0;
        var grid = document.getElementById("<%= gvItems.ClientID%>");
 
         var inputs = grid.getElementsByTagName("input");
         // Loop through all the DOM elements we grabbed
         for (var i = 0; i < inputs.length; i++)
         {
            
             if (inputs[i].name.indexOf("_tbOrderQuantity") > 1)
             {
                 if (inputs[i].value != "")
                 {
                     sum = sum + parseFloat(inputs[i].value);
                 }
             }
             
             
         }
     
        document.getElementById("ContentPlaceHolder1_IssueEntryer_gvItems__tbTotalQty").value = sum;

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
      
        document.getElementById("ContentPlaceHolder1_IssueEntryer_gvItems__tbTotalAmt").value = sum;
    }

   function CheckRepetingItems()
   {
       var grid = document.getElementById("<%= gvItems.ClientID%>");
       var rCount = grid.rows.length;       
      
       for (var rowIdx = 1; rowIdx < rCount - 1; rowIdx++)
       {
           var Itemid1 = grid.rows[rowIdx].cells[0].getElementsByTagName("*")[0].value;
           var IssueHead1 = grid.rows[rowIdx].cells[2].getElementsByTagName("*")[0].value;
           for (var rowIdx1 = rowIdx+1; rowIdx1 < rCount - 1; rowIdx1++)
           {
              var Itemid2 = grid.rows[rowIdx1].cells[0].getElementsByTagName("*")[0].value;
               var IssueHead2 = grid.rows[rowIdx1].cells[2].getElementsByTagName("*")[0].value;
             

               if ((Itemid1!="") && (IssueHead1!=0))
               {
                   if ((Itemid1 == Itemid2) && (IssueHead1 == IssueHead2))
                   {
                      // alert("Same Item and same Issue Head Not Allowed");
                       document.getElementById('<%=save.ClientID %>').disabled = true;
                       document.getElementById('<%=_btnSave.ClientID %>').disabled = true;   
                       document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = 'Check Item Name and Issue Head Name';
                       grid.style.border = "2px solid red";                      
                       return true ;
                   }
                   else
                   {
                       document.getElementById('<%=save.ClientID %>').disabled = false;
                       document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
                       document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = '';
                       grid.style.border = "none";
                   }
                 
                  
               }

               if ((Itemid1!="") && (IssueHead1==0))
               {
                   document.getElementById('<%=save.ClientID %>').disabled = true;
                   document.getElementById('<%=_btnSave.ClientID %>').disabled = true;
                   document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = 'Select Issue Head Name';     
                   grid.style.border = "2px solid red";
                   return true;
               }
               else
               {
                   document.getElementById('<%=save.ClientID %>').disabled = false;
                   document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
                   document.getElementById('<%= ErrLabel.ClientID %>').innerHTML = '';
                   grid.style.border = "none";
               }


               
           }
          
       }

       return false;
    }

</script>




<asp:SqlDataSource ID="_sdsSaveDIChallan" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" DeleteCommand="DELETE FROM [DeliveryItemsChallan] WHERE [DeliveryItemsChallanID] = @original_DeliveryItemsChallanID AND [IndentReference] = @original_IndentReference AND [IndentDate] = @original_IndentDate AND [IndentingDivisionName] = @original_IndentingDivisionName AND [ChargeableHeadName] = @original_ChargeableHeadName AND [TotalAmount] = @original_TotalAmount AND [IsDeliveredTemporary] = @original_IsDeliveredTemporary AND [ModifiedOn] = @original_ModifiedOn AND [ModifiedBy] = @original_ModifiedBy" InsertCommand="INSERT INTO [DeliveryItemsChallan] ([DeliveryItemsChallanID], [IndentReference], [IndentDate], [IndentingDivisionName], [ChargeableHeadName], [TotalAmount], [IsDeliveredTemporary], [ModifiedOn], [ModifiedBy]) VALUES (@DeliveryItemsChallanID, @IndentReference, @IndentDate, @IndentingDivisionName, @ChargeableHeadName, @TotalAmount, @IsDeliveredTemporary, @ModifiedOn, @ModifiedBy)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [DeliveryItemsChallan]" UpdateCommand="UPDATE [DeliveryItemsChallan] SET [IndentReference] = @IndentReference, [IndentDate] = @IndentDate, [IndentingDivisionName] = @IndentingDivisionName, [ChargeableHeadName] = @ChargeableHeadName, [TotalAmount] = @TotalAmount, [IsDeliveredTemporary] = @IsDeliveredTemporary, [ModifiedOn] = @ModifiedOn, [ModifiedBy] = @ModifiedBy WHERE [DeliveryItemsChallanID] = @original_DeliveryItemsChallanID AND [IndentReference] = @original_IndentReference AND [IndentDate] = @original_IndentDate AND [IndentingDivisionName] = @original_IndentingDivisionName AND [ChargeableHeadName] = @original_ChargeableHeadName AND [TotalAmount] = @original_TotalAmount AND [IsDeliveredTemporary] = @original_IsDeliveredTemporary AND [ModifiedOn] = @original_ModifiedOn AND [ModifiedBy] = @original_ModifiedBy">
    <InsertParameters>
        <asp:Parameter Name="DeliveryItemsChallanID" Type="Int32" />
        <asp:Parameter Name="IndentReference" Type="String" />
        <asp:Parameter DbType="Date" Name="IndentDate" />
        <asp:Parameter Name="IndentingDivisionName" Type="String" />
        <asp:Parameter Name="ChargeableHeadName" Type="String" />
        <asp:Parameter Name="TotalAmount" Type="Decimal" />
        <asp:Parameter Name="IsDeliveredTemporary" Type="Boolean" />
        <asp:Parameter Name="ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="ModifiedBy" Type="Byte" />
    </InsertParameters>   
</asp:SqlDataSource>

<div class="full_w">
    
    <asp:Panel ID="panelSuccess"  runat="server" CssClass="n_ok" >
        <p>
            <asp:Label ID="lblSuccess" runat="server" ></asp:Label>
        </p>
    </asp:Panel>
    
    <asp:Panel ID="panelError"   runat="server" CssClass="n_error">
        <p>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>

  <%--  comment--%>

<%--     <asp:Panel ID="panel1" Visible="false"  runat="server" CssClass="n_ok">
        <p>
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
        </p>
    </asp:Panel>
    
    <asp:Panel ID="panel2" Visible="false"  runat="server" CssClass="n_error">
        <p>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>--%>
    
    <div class="h_title">Issue Entry</div>
    
    <!--First div-->
    <div style="margin: 0px auto; padding: 10px">
        
        <div class="half_w half_left">
            <div class="h_title">Challan No. / Date(dd-mm-yyyy) </div>
            
            <div style="margin: 0px auto; padding: 10px">
                <asp:TextBox CssClass="form-control" AutoComplete="off" ID="_tbChalanNo" placeholder="Challan No" Width="280px" runat="server"></asp:TextBox>
                
                <br />
                
                <asp:TextBox CssClass="form-control" ID="_tbChallanDate" autocomplete="off"  placeholder="Date" Width="280px" runat="server"></asp:TextBox>
            </div>
        </div>
        
        <div class="half_w half_right">            
            <div class="h_title">Indent Number / Date(dd-mm-yyyy)</div>
            
            <div style="margin: 0px auto; padding: 10px">
                <asp:TextBox CssClass="form-control" ID="_tbIndentValue" autocomplete="off"  placeholder="Indent Number" Width="180px" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox CssClass="form-control" autocomplete="off" ID="_tbIntendDate" placeholder="Date" Width="180px" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="element">
            <label for="comments">Indenting Division</label><br />            
           
        <asp:DropDownList CssClass="err" ID="_ddIntendDivisions" Width="250px" runat="server">  </asp:DropDownList>

        </div>

        <div class="element">
            <label for="comments">Chargeable Head</label>
            <asp:DropDownList CssClass="err" ID="_ddCHead" Width="250px" runat="server">  </asp:DropDownList>
        </div>

        <div class="element2">
            <asp:CheckBox ID="istemporary" Text=" Select if Issued Entry is Temporary" Checked ="false" Visible ="false" runat="server" />
        </div>
        
        <div class="element2">
            
            <span style="float: left;">
                <label for="comments">Enter Number of Items Rows to use</label>
            </span>  

            <span style="float: left;padding-left: 10px;">
                <asp:TextBox CssClass="form-control" ID="tbItemsRows" TextMode="Number" Text="20" Width="30px" runat="server"></asp:TextBox>
            </span>
            
            <span style="float: left;padding-left: 10px;"> 
                <asp:Button ID="btnRowsAdd" class="btn btn-outline btn-primary" ToolTip="This will create the total number of rows to be used for adding items. By default, 10 rows are always displayed." runat="server" Text="CREATE ROWS" OnClick="btnRowsAdd_Click"  />
                <asp:Button ID="_btnCancel" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click"  />
            </span>        
        </div>

        <div class="element"></div>
        
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
       &nbsp;&nbsp;&nbsp; <asp:Label ID="ErrLabel" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"></asp:Label>
        
        <div class="table-responsive">
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DynamicLayout="False">
                <ProgressTemplate>
                    <span style="color:green">  Loading relevant data ...</span> 
                </ProgressTemplate>
            </asp:UpdateProgress>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ShowFooter="true" EmptyDataText="Empty Rows" ShowHeader="true" ID="gvItems" OnRowDataBound="gvItems_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" BackColor="White"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Items">
                                <ItemTemplate>
                                    <asp:HiddenField ID="_hdnFieldItemID" runat="server" />
                                    <asp:DropDownList ID="_ddItems" OnSelectedIndexChanged="_ddItems_SelectedIndexChanged" onchange="calculateQtySum();"    AutoPostBack="true" AppendDataBoundItems="false" CssClass="err" Width="300px" Height="25px" runat="server"></asp:DropDownList>
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
                                     <%--onchange="calculateQtySum();CheckRepetingItems();"--%>
                                       <asp:DropDownList ID="ddlIhead"  OnSelectedIndexChanged="_ddlIhead_SelectedIndexChanged" onchange="if (CheckRepetingItems()) return false; calculateQtySum();" AppendDataBoundItems="false" AutoPostBack="true" CssClass="err" Width="220px" runat="server">

                                    </asp:DropDownList>
                                    
                                  <%--  <asp:HiddenField ID="hdnSelectedIssueHead" runat="server" />
                                    <asp:HiddenField ID="hdnSelectedRate" runat="server" />--%>

                                </ItemTemplate>
                            </asp:TemplateField>

                          <%--this is css for hiding--%>
                            <%--FooterStyle-CssClass="hiddencol"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"--%>
                          
                            
                              <asp:TemplateField HeaderText="Rate--Quanitity" FooterStyle-CssClass="hiddencol"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"  >
                                    <ItemTemplate>
                                       <%-- <asp:DropDownList CssClass="err" ID="ddlRates" onchange="UpdateAmountbyRate(this.id)" Width="150px" runat="server"></asp:DropDownList>
                                    --%>
                                        <asp:TextBox ID="tbRate" runat="server" Text="rate"></asp:TextBox>&nbsp;&nbsp;&nbsp;
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
                    <asp:AsyncPostBackTrigger ControlID="gvitems" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
        <!--/table-responsive-->

        <div class="element">
            <span style="float:left; width: 111px;"> 
                <label for="comments">VEHICLE NUMBER</label>
            </span>     
            
            <span style="float:left; padding-left: 11px;";> 
               <asp:TextBox CssClass="form-control" autocomplete="off"  ID="tbVehicleNumberCaps" style="text-transform:uppercase"  placeholder="VEHICLE NUMBER" Width="280px" runat="server"></asp:TextBox>
            </span>
        </div>

        <div class="element">
            <span style="float:left; width: 111px;">    
                <label  for="comments">Receiver's Designation</label>
            </span>
            
            <span style="float:left; padding-left: 11px;";>   
                <asp:TextBox CssClass="form-control" ID="tbReceiverDesignation" autocomplete="off"  style="text-transform:capitalize" placeholder="Receiver's Designation" Width="280px" runat="server"></asp:TextBox>
            </span> 
        </div>

        <div class="element">
            <span style="float:left; width: 111px;"> 
                <label for="comments">Remarks</label>
            </span>     
            
            <span style="float:left; padding-left: 11px;";> 
               <asp:TextBox CssClass="form-control" autocomplete="off"  ID="tbRemarks"   placeholder="Remarks" TextMode ="MultiLine"   Width="280px" runat="server"></asp:TextBox>
            </span>
        </div>
        
        <br />
        <br />
        <br />

        <div class="entry">
            <p>
                <asp:Button ID="save" CssClass="btn btn-primary" runat="server" Text="SAVE" OnClick="Save_Click" />
                <asp:Button ID="_btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE & PRINT ISSUED ENTRY" OnClick ="_btnSave_Click" />
                <asp:Button ID="btnReset" CssClass="btn btn-danger"  runat="server" Text="RESET PAGE" />
            </p>
        </div>

    </div>
    <!--/First div-->

    <asp:TextBox ID="_tbtotalAmount" onchange="calculateSum()"  Visible="false" runat="server"></asp:TextBox>
</div>
<!--/full_w-->
