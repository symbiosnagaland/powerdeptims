<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rpt_DivisionValuation.ascx.cs" Inherits="IMS_PowerDept.Reports.rpt_DivisionValuation" %>


<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="973px" Height="585px">
    <LocalReport ReportEmbeddedResource="IMS_PowerDept.Report_Files.rpt_DivisionValuatios.rdlc" ReportPath="Report_Files/rpt_DivisionValuatios.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DeliveryItesmChallan_ds" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="IMS_PowerDept.App_Data.ReportDatasetTableAdapters.DeliveryItemsChallanTableAdapter" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="Original_DeliveryItemsChallanID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="DeliveryItemsChallanID" Type="Int32" />
        <asp:Parameter Name="IndentReference" Type="String" />
        <asp:Parameter Name="IndentDate" Type="DateTime" />
        <asp:Parameter Name="ChallanDate" Type="DateTime" />
        <asp:Parameter Name="IndentingDivisionName" Type="String" />
        <asp:Parameter Name="ChargeableHeadName" Type="String" />
        <asp:Parameter Name="TotalAmount" Type="Decimal" />
        <asp:Parameter Name="IsDeliveredTemporary" Type="String" />
        <asp:Parameter Name="ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="ModifiedBy" Type="Byte" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IndentReference" Type="String" />
        <asp:Parameter Name="IndentDate" Type="DateTime" />
        <asp:Parameter Name="ChallanDate" Type="DateTime" />
        <asp:Parameter Name="IndentingDivisionName" Type="String" />
        <asp:Parameter Name="ChargeableHeadName" Type="String" />
        <asp:Parameter Name="TotalAmount" Type="Decimal" />
        <asp:Parameter Name="IsDeliveredTemporary" Type="String" />
        <asp:Parameter Name="ModifiedOn" Type="DateTime" />
        <asp:Parameter Name="ModifiedBy" Type="Byte" />
        <asp:Parameter Name="Original_DeliveryItemsChallanID" Type="Int32" />
    </UpdateParameters>
</asp:ObjectDataSource>

