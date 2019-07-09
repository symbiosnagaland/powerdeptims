<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntry.aspx.cs" Inherits="IMS_PowerDept.Transformers.IssueEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <link type="text/css" href="../calender/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="../calender/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../calender/jquery-ui-1.8.19.custom.min.js"></script>
<script type="text/javascript">
    $(function () {

        $("#ContentPlaceHolder1__tbChallanDate").datepicker();
    });
</script>

<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>

<asp:SqlDataSource ID="_sdsSaveDIChallan" runat="server" ConflictDetection="CompareAllValues"
    ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>"
    DeleteCommand="DELETE FROM [DeliveryItemsChallan] WHERE [DeliveryItemsChallanID] = @original_DeliveryItemsChallanID AND [IndentReference] = @original_IndentReference AND [IndentDate] = @original_IndentDate AND [IndentingDivisionName] = @original_IndentingDivisionName AND [ChargeableHeadName] = @original_ChargeableHeadName AND [TotalAmount] = @original_TotalAmount AND [IsDeliveredTemporary] = @original_IsDeliveredTemporary AND [ModifiedOn] = @original_ModifiedOn AND [ModifiedBy] = @original_ModifiedBy"
    InsertCommand="INSERT INTO [DeliveryItemsChallan] ([DeliveryItemsChallanID], [IndentReference], [IndentDate], [IndentingDivisionName], [ChargeableHeadName], [TotalAmount], [IsDeliveredTemporary], [ModifiedOn], [ModifiedBy]) VALUES (@DeliveryItemsChallanID, @IndentReference, @IndentDate, @IndentingDivisionName, @ChargeableHeadName, @TotalAmount, @IsDeliveredTemporary, @ModifiedOn, @ModifiedBy)"
    OldValuesParameterFormatString="original_{0}"
    SelectCommand="SELECT * FROM [DeliveryItemsChallan]"
    UpdateCommand="UPDATE [DeliveryItemsChallan] SET [IndentReference] = @IndentReference, [IndentDate] = @IndentDate, [IndentingDivisionName] = @IndentingDivisionName, [ChargeableHeadName] = @ChargeableHeadName, [TotalAmount] = @TotalAmount, [IsDeliveredTemporary] = @IsDeliveredTemporary, [ModifiedOn] = @ModifiedOn, [ModifiedBy] = @ModifiedBy WHERE [DeliveryItemsChallanID] = @original_DeliveryItemsChallanID AND [IndentReference] = @original_IndentReference AND [IndentDate] = @original_IndentDate AND [IndentingDivisionName] = @original_IndentingDivisionName AND [ChargeableHeadName] = @original_ChargeableHeadName AND [TotalAmount] = @original_TotalAmount AND [IsDeliveredTemporary] = @original_IsDeliveredTemporary AND [ModifiedOn] = @original_ModifiedOn AND [ModifiedBy] = @original_ModifiedBy">
    <DeleteParameters>
        <asp:Parameter Name="original_DeliveryItemsChallanID" Type="Int32" />
        <asp:Parameter Name="original_IndentReference" Type="String" />
        <asp:Parameter DbType="Date" Name="original_IndentDate" />
        <asp:Parameter Name="original_IndentingDivisionName" Type="String" />
        <asp:Parameter Name="original_ChargeableHeadName" Type="String" />
        <asp:Parameter Name="original_TotalAmount" Type="Decimal" />
        <asp:Parameter Name="original_IsDeliveredTemporary" Type="Boolean" />
        <asp:Parameter Name="original_ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="original_ModifiedBy" Type="Byte" />
    </DeleteParameters>
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
    <UpdateParameters>
        <asp:Parameter Name="IndentReference" Type="String" />
        <asp:Parameter DbType="Date" Name="IndentDate" />
        <asp:Parameter Name="IndentingDivisionName" Type="String" />
        <asp:Parameter Name="ChargeableHeadName" Type="String" />
        <asp:Parameter Name="TotalAmount" Type="Decimal" />
        <asp:Parameter Name="IsDeliveredTemporary" Type="Boolean" />
        <asp:Parameter Name="ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="ModifiedBy" Type="Byte" />
        <asp:Parameter Name="original_DeliveryItemsChallanID" Type="Int32" />
        <asp:Parameter Name="original_IndentReference" Type="String" />
        <asp:Parameter DbType="Date" Name="original_IndentDate" />
        <asp:Parameter Name="original_IndentingDivisionName" Type="String" />
        <asp:Parameter Name="original_ChargeableHeadName" Type="String" />
        <asp:Parameter Name="original_TotalAmount" Type="Decimal" />
        <asp:Parameter Name="original_IsDeliveredTemporary" Type="Boolean" />
        <asp:Parameter Name="original_ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="original_ModifiedBy" Type="Byte" />
    </UpdateParameters>
</asp:SqlDataSource>




<div class="full_w">
    <div class="h_title">Issue Entry</div>
    <div style="margin: 0px auto; padding: 10px">

            <asp:Panel ID="panelSuccess" Visible="false"  runat="server" CssClass="n_ok">
    <p>    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label></p>
    </asp:Panel>

    <asp:Panel ID="panelError" Visible="false"  runat="server" CssClass="n_error">
    <p><asp:Label ID="lblError" runat="server" Text=""></asp:Label></p>
    </asp:Panel>

        <div class="element">
            <span style="float:left; padding-right:22px;">
            <label for="comments">Challan No.</label>
            <asp:TextBox CssClass="form-control" ID="_tbChalanNo" placeholder="Challan No" Width="180px" runat="server"></asp:TextBox>
            </span><span>
             <label for="comments">Challan Date</label>
           <asp:TextBox CssClass="form-control" ID="_tbChallanDate" placeholder="Date" Width="180px" runat="server"></asp:TextBox>
                </span>
        </div>
      
       

        
            

        <div class="element">
            <label for="comments">Indenting Division</label>
            <asp:DropDownList CssClass="err" ID="_ddIntendDivisions" Width="250px" runat="server">
            </asp:DropDownList>
        </div>
      
          

        <div class="element2">
        </div>
             <div class="element2">
                 <span style="float: left;">
            <label for="comments">Enter Number of Items Rows to use</label></span>  <span style="float: left;padding-left: 10px;">
            <asp:TextBox CssClass="form-control" ID="tbItemsRows" TextMode="Number" Text="5" Width="30px" runat="server"></asp:TextBox></span>
                <span style="float: left;padding-left: 10px;"> <asp:Button ID="btnRowsAdd" class="btn btn-outline btn-primary" ToolTip="This will create the total number of rows to be used for adding items. By default, 10 rows are always displayed." runat="server" Text="CREATE ROWS" OnClick="btnRowsAdd_Click" />

                     <asp:Button ID="_btnCancel" CssClass="btn btn-danger" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
                </span>

                 
        </div>
        <div class="element"></div>
        <br />
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="table-responsive">
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DynamicLayout="False">
                <ProgressTemplate>
                    Loading relevant data ...
                </ProgressTemplate>

            </asp:UpdateProgress>
         
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                
            <asp:GridView ShowFooter="true" EmptyDataText="Empty Rows" ShowHeader="true" ID="gvItems" OnRowDataBound="gvItems_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" BackColor="White" OnRowCommand="gvItems_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Transformer Received">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlTransfomer"  AppendDataBoundItems="false" CssClass="err" Width="450px" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />                       
                    </asp:TemplateField>
            
               
                    <asp:TemplateField HeaderText="Oil">
                        <ItemTemplate>
                         <asp:TextBox Width="100px" ID="tbOil"  CssClass="form-control" runat="server">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Oil Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlOilType"  AppendDataBoundItems="false" CssClass="err"  runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />                       
                    </asp:TemplateField>

                             <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnReceipt" CommandName="receipt" CommandArgument="<%# Container.DataItemIndex %>"  runat="server">Receipt</asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />                       
                    </asp:TemplateField>
         
                </Columns>

            </asp:GridView>
                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="_btnSave" />
                </Triggers>
                
            </asp:UpdatePanel>
        </div>
        <div class="entry">
            <p>
            <asp:Button ID="_btnSave" CssClass="btn btn-primary" Visible="false" runat="server" Text="SAVE ENTRY" OnClick="_btnSave_Click" />
                <asp:Button ID="btnReset" CssClass="btn btn-danger" Visible="false" runat="server" Text="RESET PAGE" OnClick="_btnCancel_Click" />
           </p>
        </div>
    </div>
    
</div>

</asp:Content>
