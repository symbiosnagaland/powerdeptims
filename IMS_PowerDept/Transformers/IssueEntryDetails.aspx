<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Transformers_Master.Master" AutoEventWireup="true" CodeBehind="IssueEntryDetails.aspx.cs" Inherits="IMS_PowerDept.Transformers.IssueEntryDetails" %>

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
        
        <asp:BoundField DataField="Division" HeaderText="Division"  SortExpression="Division" />
    </Fields>
    </asp:DetailsView>
    


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [ChallanNo], [ChallanDate], [Division] FROM [Transformer_Issue] WHERE ([ChallanNo] = @ChallanNo)">
        <SelectParameters>
            <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:GridView ID="GridView1" Width="95%" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
        <Columns>
            <%--<asp:BoundField DataField="issuedetailsid" HeaderText="issuedetailsid" SortExpression="issuedetailsid" InsertVisible="False" ReadOnly="True" />--%>
          
            <asp:TemplateField HeaderText="Sl.">
      <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
      </ItemTemplate>
                </asp:TemplateField>

           <%-- <asp:BoundField DataField="transformerid" HeaderText="transformerid" SortExpression="transformerid" />--%>
            <asp:BoundField DataField="transformer" HeaderText="Transformer Issued" SortExpression="transformer" />
            <asp:BoundField DataField="oil" HeaderText="Oil" SortExpression="oil" />
            <asp:BoundField DataField="oiltype" HeaderText="Oil Type" SortExpression="oiltype" />
            
          
        </Columns>
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PowerDeptNagalandIMSConnectionString %>" SelectCommand="SELECT [issuedetailsid], [challanno], [transformerid], [oil],[oiltype], [transformer] FROM [Transformer_IssueDetails] WHERE ([ChallanNo] = @ChallanNo)">
    <SelectParameters>
        <asp:QueryStringParameter Name="ChallanNo" QueryStringField="challanno" Type="String" />
    </SelectParameters>
    </asp:SqlDataSource>

         </div>

        </div>
    
</asp:Content>
