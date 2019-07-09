<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkItemsUploadControl.ascx.cs" Inherits="IMS_PowerDept.UserControls.BulkItemsUploadControl" %>
 <%--<ul class="breadcrumb">
  <li><a href="#">ADMINISTRATOR PANEL</a></li>
 
  <li class="active">Bulk Items Upload</li>
</ul> <br />--%>
<div class="full_w">
    <asp:Panel ID="panelSuccess" Visible="false" CssClass="n_ok" runat="server">

        <p>
            <asp:Label ID="lblSuccess" runat="server" Text="success message"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="panelError" Visible="false" CssClass="n_error" runat="server">
        <p>
            <asp:Label ID="lblError" runat="server" Text="error message"></asp:Label>
        </p>
    </asp:Panel>


				<div class="h_title">Upload Bulk Items from Excel File</div>
<div style="margin:0px auto;padding:10px">
    <form role="form">
                                <p>
                                    <a href="../AppContent/ImportItemsTemplate.xlsx" target="_blank">
                                        <img src="../img/exceldownload.png" style="border:none; width: 18px;margin-right:5px;" />
                                        Download Excel Template </a>&nbsp;</p>
        <p>&nbsp;</p>

    
                                <div class="form-group">
                                    <p>
                                    <label>Select the Excel file with data below:</label></p>
                                    <p><br />
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Label ID="Filename" runat="server" Text="Label" Visible="false"></asp:Label><br />

                                    </p>
                                </div>
        <p>&nbsp;</p>

        <p>
                              <button type="submit" runat="server" onserverclick="Unnamed_ServerClick" class="add">Upload and Save Items</button> 
        </p>
                            </form>

    </div>
     </div>