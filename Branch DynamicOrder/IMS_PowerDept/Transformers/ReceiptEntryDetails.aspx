<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="ReceiptEntryDetails.aspx.cs" Inherits="IMS_PowerDept.Transformers.ReceiptEntryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
    <div class="full_w">

				<div class="h_title">Receipt Entry Details View</div>
     <div style="width: 100%; overflow: auto;">
<asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="95%" AutoGenerateRows="False" DataKeyNames="ChallanNo" DataSourceID="SqlDataSource1">
    <Fields>
        <asp:BoundField DataField="ChallanNo" HeaderText="ChallanNo" ReadOnly="True" SortExpression="ChallanNo" />
        <asp:BoundField DataField="ChallanDate" HeaderText="ChallanDate" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ChallanDate" />
        <asp:BoundField DataField="Division" HeaderText="Division" SortExpression="Division" />
        <asp:BoundField DataField="ReceiptDate" HeaderText="ReceiptDate"  DataFormatString="{0:yyyy-MM-dd}" SortExpression="ReceiptDate" />
        <asp:BoundField DataField="ProbableCause" HeaderText="ProbableCause"  SortExpression="ProbableCause" />
    </Fields>
    </asp:DetailsView>
    


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [ChallanNo], [ChallanDate], [Division], [ReceiptDate], [ProbableCause] FROM [Transformer_Receipts] WHERE ([ChallanNo] = @ChallanNo)">
        <SelectParameters>
            <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:GridView ID="GridView1" Width="95%" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="Voltage" HeaderText="Voltage" SortExpression="Voltage" />
            <asp:BoundField DataField="kVA" HeaderText="kVA" SortExpression="kVA" />
            <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
            <asp:BoundField DataField="Seriel" HeaderText="Seriel" SortExpression="Seriel" />
            <asp:BoundField DataField="Oil" HeaderText="Oil" SortExpression="Oil" />
        </Columns>
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [Voltage], [kVA], [Make], [Seriel], [Oil] FROM [Transformer_ReceiptsDetails] WHERE ([ChallanNo] = @ChallanNo)">
    <SelectParameters>
        <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
    </SelectParameters>
    </asp:SqlDataSource>

         </div>

        </div>
    
</asp:Content>
