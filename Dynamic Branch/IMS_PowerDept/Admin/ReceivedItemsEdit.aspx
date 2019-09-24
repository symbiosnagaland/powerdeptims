<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ReceivedItemsEdit.aspx.cs" Inherits="IMS_PowerDept.Admin.ReceivedItemsEdit" %>
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
        $("#ContentPlaceHolder1_tbSupplyDate").datepicker(
            {
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
        $("#ContentPlaceHolder1_tbOTEODate").datepicker(
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

    function SetUnitName(itemid)
    {

        //bisu  script To check if the same item is repeated
        //alert(itemid+" >>I am ItemID");

       



        var tbl = $("[id$=gvItems]");
        var rows = tbl.find('tr');
        //alert(rows.length);

        for (var i = 1; i < rows.length - 1; i++)
        {
            var row = rows[i];
            //alert("i am  " + row);
            for (var j = i + 1; j < rows.length - 1; j++)
            {
                var row1 = rows[j];
                //  alert("second for" + j);

                var ItemNO = $(row).find("[id*=_ddItems]").val().toString();
                var ItemNO2 = $(row1).find("[id*=_ddItems]").val().toString();

                if ((ItemNO == 0) || (ItemNO2 == 0))
                {
                    // alert("oK");
                }
                else
                {
                    if (ItemNO == ItemNO2)
                    {
                        // alert("Duplicate Items In the List. Cannot Save");                       
                        document.getElementById('<%=btnUpdate.ClientID %>').disabled = true;
                        document.getElementById('<%=_btnSave.ClientID %>').disabled = true;
                        document.getElementById('<%=gvItems.ClientID %>').style.border = "5px solid red";
                        document.getElementById('myError1').innerHTML = "Duplicate Items NOt Allowed";
                        return;
                    }
                    else
                    {
                        document.getElementById('<%=btnUpdate.ClientID %>').disabled = false;
                        document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
                        document.getElementById('<%=gvItems.ClientID %>').style.border = "none"
                        document.getElementById('myError1').innerHTML = "";
                    }
                }



            }
        }

        //bisu new scrpt values
       // alert("Hello");
        var e = document.getElementById(itemid);
        //alert(e);
        var itemsvalue = e.options[e.selectedIndex].value;
        //alert(itemsvalue);
       // alert("hi");
        var mySplitResult = itemsvalue.split("$");
        //alert("Hello222");
        //alert(mySplitResult[0]);
       // alert(3);
        //alert(mySplitResult[3]);
        //alert(mySplitResult);
        /* Store last character of string id of ddlitems */
        // var last_character = itemid[itemid.length - 1];
        var splitItemsID = itemid.split("_");
        //making sure the last digit is not 0
        //fetching the last part of the control id(which is dynamic)
        var dynamicidpart = splitItemsID[splitItemsID.length - 1];
        if (typeof mySplitResult[1] === "undefined") {
            document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = '';

             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = '';
         }
         else {
             document.getElementById("<%= gvItems.ClientID%>__tbUnit_" + dynamicidpart).value = mySplitResult[1];
             document.getElementById("<%= gvItems.ClientID%>_hdnFieldItemID_" + dynamicidpart).value = mySplitResult[0];
         }

        //Bisu writes the code
        <%--
        if (typeof mySplitResult[2] === "undefined")
        {
            document.getElementById("<%= gvItems.ClientID%>__tbIssueName_" + dynamicidpart).value = '';            
         }
         else
         {
            document.getElementById("<%= gvItems.ClientID%>__tbIssueName_" + dynamicidpart).value = mySplitResult[2];            
        }
       --%>

        if (typeof mySplitResult[3] === "undefined")
        {
            document.getElementById("<%= gvItems.ClientID%>_tbOrderNo_" + dynamicidpart).value = '';
        }
        else
        {
            //alert("near order no");
            document.getElementById("<%= gvItems.ClientID%>_tbOrderNO_" + dynamicidpart).value = mySplitResult[3];
        }


        //check with old gridview details with new inserts
        var gridView = document.getElementById("<%=gvItems.ClientID %>");
        var NewRow = gridView.getElementsByTagName("tr");

        var gridView1 = document.getElementById("<%=gvItems_Edit.ClientID %>");
        var OldRow = gridView1.getElementsByTagName("tr");
        //alert("oldRow" + OldRow.length);

        for (var i = 1; i < NewRow.length - 1; i++)
        {
            var NR = NewRow[i];
            var NItem = $(NR).find("[id*=_hdnFieldItemID]").val().toString();
           // alert(NItem+"New Item");
            for (var j = 1; j < OldRow.length - 1; j++)
            {
                //alert("hi");
                var OR1 = OldRow[j];
                //alert("I am or1 "+OR1);
                var OItem = $(OR1).find("[id*=hdnFieldItemID]").val().toString();
                //alert(OItem+"Old ITem");
                if(OItem==NItem)
                {
                    OR1.style.backgroundColor = "red";
                    NR.style.fontweight = "bold";
                    document.getElementById('<%=btnUpdate.ClientID %>').disabled = true;
                    document.getElementById('<%=_btnSave.ClientID %>').disabled = true;
                    document.getElementById('<%=gvItems.ClientID %>').style.border = "5px solid red";
                    document.getElementById('myError1').innerHTML = "Duplicate Items Already Issued NOt Allowed";
                    return;
                }
                else
                {
                    OR1.style.backgroundColor = "white";
                    NR.style.backgroundColor = "white";
                    document.getElementById('<%=btnUpdate.ClientID %>').disabled = false;
                    document.getElementById('<%=_btnSave.ClientID %>').disabled = false;
                    document.getElementById('<%=gvItems.ClientID %>').style.border = "none"
                    document.getElementById('myError1').innerHTML = "";

                }
            }

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

				<div class="h_title">Received Items Entry Edit</div>
<div style="margin:0px auto;padding:10px">
    
     <div class="half_w half_left">
				<div class="h_title"> Supply Order Reference / Date (dd-mm-yyyy)</div>
        <div style="margin:0px auto; padding:10px">
            <asp:TextBox CssClass="form-control" ID="tbSupplyOrderReference" placeholder="Supply Order Reference" AutoComplete="off" Width="280px" runat="server"></asp:TextBox>
             
           <br />
             <asp:TextBox CssClass="form-control" ID="tbSupplyDate" placeholder="Supply Order Date" Width="280px" runat="server"></asp:TextBox>
             </div>

</div>

    <div class="half_w half_right">
				<div class="h_title">OTEO ID / Date (dd-mm-yyyy)</div>
                <div style="margin:0px auto; padding:10px">
                 <asp:TextBox CssClass="form-control"  ID="tbOtEONumber" placeholder="OTEO No." Width="180px" runat="server"></asp:TextBox>   
                    <br />
                   
                     <asp:TextBox CssClass="form-control" ID="tbOTEODate" placeholder="OTEO Date" Width="180px" runat="server"></asp:TextBox>                                             
     </div>
    </div>

  <div class="element">
               <label>Supplier Name</label>
               <asp:TextBox ID="tbSupplierName" Width="350px" AutoComplete="off" placeholder="Add Supplier Name" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>

        <div class="form-group">


                                             <div style="float:left;">
                                              <span class="singleLineLeft">
                                            <label> Issue Head(Ledger) Selected : </label></span>

                                        <span class="singleLineRight">  
                                            <asp:Label ID="lblIssueHeadOld" runat="server"></asp:Label>
                                        </span>                                 


</div>         

                                        <div style="float:left;padding-left: 20px;">
                                        <span class="singleLineLeft">
                                            <label>Chargeable Head Selected : </label></span>
                                        <span class="singleLineRight">
                                     
 <asp:Label ID="lblChargeableHead" runat="server" ></asp:Label>
                                         </span>
                                            </div>
                                  

                                    </div>

        <div class="clear" style="height:10px;"></div>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"></asp:UpdateProgress>



                                    <div class="form-group">


                                             <div style="float:left;">
                                              <span class="singleLineLeft">
                                            <label>Select New only if you require update </label></span>

                                        <span class="singleLineRight">  
                                              <asp:DropDownList  CssClass="form-control" ID="ddlIssueHead" Height="30px" Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueHead_SelectedIndexChanged">
                                                <asp:ListItem Value="">Select Issue Head</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>                                 


</div>         

                                        <div style="float:left;padding-left: 20px;">
                                        <span class="singleLineLeft">
                                            <label>Select New only if you require update </label></span>
                                        <span class="singleLineRight">
                                     
                                                  <asp:DropDownList  Height="30px" Width="200px" CssClass="form-control" ID="ddlChargeableHead" runat="server" >
                                                <asp:ListItem Value="">Chargeable Head</asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:UpdateProgress ID="UpdateProgress3" DynamicLayout="false"  runat="server">
                                    <ProgressTemplate><b>Loading Chargeable Heads...</b></ProgressTemplate>
                                </asp:UpdateProgress>
                                            
                                            </div>
                                  

                                    </div>
                           
                                 
                                    </span>
                                    
                                 
                                </ContentTemplate>
                             

                            </asp:UpdatePanel>
           <div class="element clear"></div>
        <h2>Added Items</h2>

        <%-- OnRowDataBound="gvItems_RowDataBound"
         OnRowCommand="gvItems_Edit_RowCommand" OnSelectedIndexChanged="gvItems_Edit_SelectedIndexChanged" --%>

        <asp:GridView ID="gvItems_Edit" runat="server"  AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-bordered" BackColor="White" DataSourceID="sds_gvitemsedit" >
                                                <Columns>

                                                     <asp:TemplateField HeaderText="Sl."> 
                   <ItemTemplate>  <%# Container.DataItemIndex+1 %>.     </ItemTemplate>  
                     <ItemStyle Width="5%" HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                   </asp:TemplateField>


                                                  
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                              <asp:HiddenField ID="hdnFieldItemID" value ='<%# Eval("itemid") %>' runat="server" />
                                                     
                                                            <asp:Label ID="lblItem" Text='<%# Eval("itemname") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterStyle HorizontalAlign="Right" />
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" Text='<%# Eval("unit") %>'  runat="server"></asp:Label>
                                                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" Text='<%# Eval("quantity") %>' runat="server"></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" Text='<%# Eval("rate") %>' runat="server"></asp:Label>
                                                         
                                                         
                                                        </ItemTemplate>
                                                     <%--     <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            Total Amount
                                                           
                                                        </FooterTemplate>--%>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblAmount" Text='<%# Eval("amount") %>' runat="server"></asp:Label>          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                  <%--  <asp:TemplateField HeaderText="Order No"   >
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblOrderNo" Text='<%# Eval("OrderNo") %>' runat="server"></asp:Label>          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete"  CommandArgument='<%# Eval("ReceivedItemID") +","+ Eval("ItemId") %>' CommandName='<%# Eval("amount") %>'  runat="server">Delete</asp:LinkButton>                                                                                                           
                                                        </ItemTemplate>
                                                        </asp:TemplateField>


                                                </Columns>

                                            </asp:GridView>
       
        <table class="table table-bordered"><tr> <td style="padding-left: 311px;">
            Total Amount

                    </td>
            <td>
                 <asp:TextBox ID="tbtotalAmountAddedItems" TabIndex="999"  Width="80px" BorderColor="Transparent" BackColor="Transparent"   runat="server"></asp:TextBox>

            </td>

               </tr></table>
      

        <asp:SqlDataSource ID="sds_gvitemsedit" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSV2ConnectionString2 %>" SelectCommand="SELECT ReceivedItemsDetails.ReceivedItemID, ReceivedItemsDetails.ItemID,
 ReceivedItemsDetails.ItemName, ReceivedItemsDetails.Quantity, ReceivedItemsDetails.unit,
  ReceivedItemsDetails.Rate, ReceivedItemsDetails.amount
   FROM ReceivedItemsDetails where ReceivedItemsOTEOID=@ReceivedItemsOTEOID" DeleteCommand="DELETE FROM [ReceivedItemsDetails] WHERE [ReceivedItemID] = @ReceivedItemID" >
            <DeleteParameters>
                <asp:Parameter Name="ReceivedItemID" Type="Int32" />
            </DeleteParameters>
    
            <SelectParameters>
                <asp:Parameter Name="ReceivedItemsOTEOID" />
            </SelectParameters>
    
        </asp:SqlDataSource>
        <h2> Insert New Items</h2>

          <div class="element2">
                 <span style="float: left;">
            <label for="comments">Enter Number of Items Rows to use</label></span>  <span style="float: left;padding-left: 10px;">
            <asp:TextBox CssClass="form-control" ID="tbItemsRows" TextMode="Number" Text="10" Width="30px" runat="server"></asp:TextBox></span>
                <span style="float: left;padding-left: 10px;"> <asp:Button ID="btnRowsAdd" class="btn btn-outline btn-primary" ToolTip="This will create the total number of rows to be used for adding items. By default, 10 rows are always displayed." runat="server" Text="CREATE ROWS" OnClick="btnRowsAdd_Click" />

                     <asp:Button ID="btnReset1" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="Button1_Click" />
                </span>

                 
        </div>
        </div>
    <div class="element clear"></div>
    <div id="myError1" style ="color:red;font-size :x-large; margin-left :15px;"></div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                            <asp:GridView ID="gvItems" runat="server" OnRowDataBound="gvItems_RowDataBound"  AutoGenerateColumns="False" ShowFooter="true" CssClass="table table-bordered" BackColor="White">
                                                <Columns>
                                                  
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                              <asp:HiddenField ID="hdnFieldItemID" runat="server" />
                                                            <asp:DropDownList AppendDataBoundItems="true" Height="30px" onchange="SetUnitName(this.id)" CssClass="form-control" Width="250px" ID="_ddItems" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                           <FooterStyle HorizontalAlign="Right" />
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox Width="50px"  TabIndex="999" BorderColor="Transparent" AutoComplete="off"  ID="_tbUnit" runat="server" BorderStyle="None" BackColor="Transparent"   BorderWidth="1px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="50px"  onchange="UpdateAmountbyQuantity(this.id)"  CssClass="form-control" AutoComplete="off" ID="_tbQuantity" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="70px"   onchange="UpdateAmountbyRate(this.id)" CssClass="form-control"  ID="tbRate" AutoComplete="off" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                          <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            Total Amount
                                                           
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox TabIndex="999"  Width="80px" BorderColor="Transparent" BackColor="Transparent"   ID="tbAmount" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                            </asp:TextBox>                                                     
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="tbtotalAmount" TabIndex="999"   Width="80px" BorderColor="Transparent" BackColor="Transparent"  Text="0" runat="server"></asp:TextBox>              
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                     <asp:TemplateField HeaderText="Order NO">
                                                        <ItemTemplate>
                                                            <asp:TextBox  Width="80px" BorderColor="Transparent" BackColor="Transparent"   ID="tbOrderNO" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                            </asp:TextBox>                                                     
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="tbOrder" TabIndex="999"   Width="80px" BorderColor="Transparent" BackColor="Transparent"  Text="0" runat="server"></asp:TextBox>              
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                </Columns>

                                            </asp:GridView>
                                          

                                           </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="_btnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                 
    
    <div class="element2">

        <asp:Button ID="btnUpdate" EnableViewState="true"  OnClick="btnUpdate_Click" CssClass="btn btn-primary" runat="server" Text="UPDATE" />
       

          <asp:Button ID="_btnSave" EnableViewState="true" Visible="false" OnClick="_btnSave_Click" CssClass="btn btn-primary" runat="server" Text="UPDATE & PRINT RECEIVED ITEMS" />
           
                                    <asp:Button ID="_btnCancel" Visible="false" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
            <asp:Button ID="_btnDelete" Visible="false" CssClass="btn btn-danger" runat="server" Text="Delete"  />

    </div>
    <asp:HiddenField ID="hdnFieldOTEOID" runat="server" />
              
        </div>
</div>
  
</asp:Content>
