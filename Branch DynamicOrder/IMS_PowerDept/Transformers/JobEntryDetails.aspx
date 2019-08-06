<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="JobEntryDetails.aspx.cs" Inherits="IMS_PowerDept.Transformers.JobEntryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
    <div class="full_w">

				<div class="h_title">Job Entry Details View</div>
     <div style="width: 100%; overflow: auto;">
<asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="95%" AutoGenerateRows="False" DataKeyNames="ChallanNo" DataSourceID="SqlDataSource1">
    <Fields>
        <asp:BoundField DataField="ChallanNo" HeaderText="Challan No." ReadOnly="True" SortExpression="ChallanNo" />
        <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ChallanDate" />
        
        <asp:BoundField DataField="RepairFirm" HeaderText="Repair Firm"  SortExpression="RepairFirm" />
    </Fields>
    </asp:DetailsView>
    


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [ChallanNo], [ChallanDate], [RepairFirm], [JobID] FROM [Transformer_Job] WHERE ([ChallanNo] = @ChallanNo)">
        <SelectParameters>
            <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:GridView ID="GridView1" Width="95%" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
        <Columns>
  <asp:TemplateField HeaderText="Sl.">
      <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
      </ItemTemplate>

  </asp:TemplateField>
            <asp:BoundField DataField="transformer" HeaderText="Transformer Received" SortExpression="transformer" />
          
        </Columns>
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [jobdetailsid], [challanno], [transformerid], [jobno], [transformer] FROM [Transformer_JobDetails] WHERE ([ChallanNo] = @ChallanNo)">
    <SelectParameters>
        <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
    </SelectParameters>
    </asp:SqlDataSource>

         </div>

        </div>
    
</asp:Content>
