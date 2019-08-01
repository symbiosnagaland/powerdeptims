<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiveItems.ascx.cs" Inherits="IMS_PowerDept.UserControls.ReceiveItems" %>

<link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />

<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>



 <script type="text/javascript">
        $(function () {
          
            $("#ContentPlaceHolder1_ReceiveItems_tbSupplyDate").datepicker(
             {
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: 'dd-mm-yy'
             });

            $("#ContentPlaceHolder1_ReceiveItems_tbOTEODate").datepicker(
                 {
                     changeMonth: true,
                     changeYear: true,
                     dateFormat: 'dd-mm-yy'
                 });
        
        });
    </script>

    <%--<link href="../css/style.css" rel="stylesheet" />
    <link href="../css/navi.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />--%>

<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style> 

<script type="text/javascript">

    function SetUnitName(itemid)
    {
         var e = document.getElementById(itemid);
         var itemsvalue = e.options[e.selectedIndex].value;
        // alert(itemsvalue);
         var mySplitResult = itemsvalue.split(" ");
         //alert(mySplitResult);
         /* Store last character of string id of ddlitems */
         // var last_character = itemid[itemid.length - 1];
         var splitItemsID = itemid.split("_");
         //making sure the last digit is not 0
         //fetching the last part of the control id(which is dynamic)
         var dynamicidpart = splitItemsID[splitItemsID.length - 1];
         if (typeof mySplitResult[1] === "undefined")
         {
             document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = '';
             
             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = '';
         }
         else {
             document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = mySplitResult[1];
             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = mySplitResult[0];
         }

         //Bisu writes the code

        if (typeof mySplitResult[2] === "undefined")
        {
             document.getElementById("<%= gvItems.ClientID%>__tbOrderNo_" + dynamicidpart).value = '';            
         }
         else
         {
             document.getElementById("<%= gvItems.ClientID%>__tbOrderNo_" + dynamicidpart).value = mySplitResult[2];            
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
                      sum = sum + parseInt(inputs[i].value);
                  }

              }
          }
          document.getElementById("<%= gvItems.ClientID%>_tbtotalAmount").value = sum;
     }

    //this is bisu function

    

    function getOrder(orderId)
    {
        //alert(orderId.selectedIndex );

     }

    function SetTarget() {
        document.forms[0].target = "_blank";
    }

</script>

<%--content starts here--%>


<div class="full_w">
    <asp:Panel ID="panelSuccess" Visible="false" CssClass="n_ok" runat="server">
        <p>
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>
    
    <asp:Panel ID="panelError" Visible="false"  CssClass="n_error" runat="server">
        <p>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>
    
    <div class="h_title">Received Items Entry</div>
    
    <div style="margin:0px auto;padding:10px">
        <div class="half_w half_left">
            <div class="h_title"> Supply Order Reference / Date (dd-mm-yyyy)</div>
            
            <div style="margin:0px auto; padding:10px">
                <asp:TextBox CssClass="form-control" ID="tbSupplyOrderReference"  autocomplete="off"  placeholder="Supply Order Reference" Width="280px" runat="server"></asp:TextBox>
                
                <br />
                
                <asp:TextBox CssClass="form-control" ID="tbSupplyDate" autocomplete="off"  placeholder="Supply Order Date" Width="280px" runat="server"></asp:TextBox>
            </div>

        </div>
        
        <div class="half_w half_right">
            <div class="h_title">OTEO ID / Date (dd-mm-yyyy)</div>
            
            <div style="margin:0px auto; padding:10px">
                <asp:TextBox CssClass="form-control" ID="tbOtEONumber" autocomplete="off" placeholder="OTEO No." Width="280px" runat="server"></asp:TextBox>
                
                <br />
                
                <asp:TextBox CssClass="form-control"  autocomplete="off" ID="tbOTEODate" placeholder="OTEO Date" Width="280px" runat="server" OnTextChanged="tbOTEODate_TextChanged1" ></asp:TextBox>
            </div>
        </div>
        
        <div class="element">
            <label>Supplier Name</label>
            
            <asp:TextBox ID="tbSupplierName" Width="350px" autocomplete="off"  placeholder="Add Supplier Name" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true"  runat="server"></asp:ScriptManager>
        
        <div class="element2">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"></asp:UpdateProgress>
                    
                    <div class="form-group ">
                        <div style="float:left;">
                            <span class="singleLineLeft">
                                <label> Issue Head </label>
                            </span>
                            
                            <span class="singleLineRight">
                                <asp:DropDownList  CssClass="form-control" ID="ddlIssueHead" Height="30px" Width="200px"
                                     runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueHead_SelectedIndexChanged">
                                    <asp:ListItem Value="">Select Issue Head</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </div>
                        
                        <div style="float:left;padding-left: 20px;">
                            <span class="singleLineLeft">
                                <label>Chargeable Head </label>
                            </span>
                            
                            <span class="singleLineRight">
                                <asp:DropDownList Enabled="false"  Height="30px" Width="200px" CssClass="form-control" ID="ddlChargeableHead" runat="server" >
                                    <asp:ListItem Value="">Select Chargeable Head</asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:UpdateProgress ID="UpdateProgress3" DynamicLayout="false"  runat="server">
                                    <ProgressTemplate><b>Loading Chargeable Heads...</b></ProgressTemplate>
                                </asp:UpdateProgress>
                            </span>
                        </div>
                    </div>
                    

                </ContentTemplate>
            </asp:UpdatePanel>
            
            <br />
            
            <div class="element2">
                <span style="float: left;">
                    <label for="comments">Enter Number of Items Rows to use</label>
                </span>  
                
                <span style="float: left;padding-left: 10px;">
                    <asp:TextBox CssClass="form-control" ID="tbItemsRows" TextMode="Number" Text="10" Width="30px" runat="server"></asp:TextBox>
                </span>                
                
                <span style="float: left;padding-left: 10px;">
                    <asp:Button ID="btnRowsAdd" class="btn btn-outline btn-primary" ToolTip="This will create the total number of rows to be used for adding items. By default, 10 rows are always displayed." runat="server" Text="CREATE ROWS" OnClick="btnRowsAdd_Click" />
                    <asp:Button ID="Button1" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="Button1_Click" />
                </span>

            </div>
        </div>
        <div class="element clear"></div>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvItems" runat="server" OnRowDataBound="gvItems_RowDataBound"  AutoGenerateColumns="False" ShowFooter="true" CssClass="table table-bordered" BackColor="White" OnSelectedIndexChanged="gvItems_SelectedIndexChanged">
                   
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Item">
                            
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnFieldItemID" runat="server"  />
                                <asp:DropDownList AppendDataBoundItems="true"  onchange="SetUnitName(this.id)" 
                                    CssClass="err" Width="280px" ID="_ddItems" runat="server" >

                                </asp:DropDownList>
                            </ItemTemplate>
                            
                            <FooterStyle HorizontalAlign="Right" />

                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                <asp:TextBox Width="70px"  TabIndex="999" autocomplete="off"  BorderColor="Transparent"  ID="_tbUnit" runat="server" BorderStyle="None" BackColor="Transparent"   BorderWidth="1px">

                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox Width="80px"  onchange="UpdateAmountbyQuantity(this.id)"  autocomplete="off" CssClass="form-control" ID="_tbQuantity" runat="server" BorderStyle="Solid" BorderWidth="1px">

                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Rate">
                            
                            <ItemTemplate>
                                <asp:TextBox Width="80px"  onchange="UpdateAmountbyRate(this.id)" CssClass="form-control" autocomplete="off"  ID="tbRate" runat="server" BorderStyle="Solid" BorderWidth="1px">

                                </asp:TextBox>
                            </ItemTemplate>
                            
                            <FooterStyle HorizontalAlign="Right" />
                            
                            <FooterTemplate>
                                Total Amount
                            </FooterTemplate>

                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:TextBox TabIndex="999"  Width="120px" BorderColor="Transparent" 
                                    BackColor="Transparent" autocomplete="off"  ID="tbAmount" 
                                    runat="server" BorderStyle="Solid" BorderWidth="1px">

                                </asp:TextBox>
                            </ItemTemplate>
                            
                            <FooterStyle HorizontalAlign="Right" />
                            
                            <FooterTemplate>
                                <asp:TextBox ID="tbtotalAmount" TabIndex="999"   Width="80px" BorderColor="Transparent" BackColor="Transparent"  Text="0" runat="server"></asp:TextBox>
                            </FooterTemplate>

                        </asp:TemplateField>

                        <%--Bisu writes code here for temporary purpose--%>

                         <asp:TemplateField HeaderText="order No">
                            <ItemTemplate>
                                <asp:TextBox TabIndex="999"  Width="120px" BorderColor="Transparent" 
                                    BackColor="Transparent" autocomplete="off"  ID="_tbOrderNo" runat="server"
                                     BorderStyle="Solid" BorderWidth="1px">

                                </asp:TextBox>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                       <%-- Bisu Ends his code here--%>
                    </Columns>                                      
                </asp:GridView>
            </ContentTemplate>
            
            <Triggers>
                <asp:PostBackTrigger ControlID="_btnSave" />
            </Triggers>

        </asp:UpdatePanel>
        
        <div class="element2">
            <asp:Button ID="save" EnableViewState="true" OnClick="_save_Click" CssClass="btn btn-primary" runat="server" Text="SAVE" />
            <asp:Button ID="_btnSave" EnableViewState="true" Visible="false" OnClick="_btnSave_Click" CssClass="btn btn-primary" runat="server" Text="SAVE & PRINT RECEIVED ITEMS" />
            <asp:Button ID="_btnCancel" Visible="false" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
        </div>

    </div>
</div>