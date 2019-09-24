<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CentralStore_Master.Master" AutoEventWireup="true" CodeBehind="Divisions.aspx.cs" Inherits="IMS_PowerDept.CentralStore.Divisions" %>
<%@ Register src="../UserControls/DivisionsControl.ascx" tagname="DivisionsControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var gAutoPrint = true; // Tells whether to automatically call the print function

        function printSpecial() {
            if (document.getElementById != null) {
                var html = '<HTML>\n<HEAD>\n';

                if (document.getElementsByTagName != null) {
                    var headTags = document.getElementsByTagName("head");
                    if (headTags.length > 0)
                        html += headTags[0].innerHTML;
                }

                html += '\n</HE>\n<BODY>\n';

                var printReadyElem = document.getElementById("printReady");

                if (printReadyElem != null) {
                    html += printReadyElem.innerHTML;
                }
                else {
                    alert("Could not find the printReady function");
                    return;
                }

                html += '\n</BO>\n</HT>';

                var printWin = window.open("", "printSpecial");
                printWin.document.open();
                printWin.document.write(html);
                printWin.document.close();
                if (gAutoPrint)
                    printWin.print();
            }
            else {
                alert("The print ready feature is only available if you are using an browser. Please update your browswer.");
            }
        }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DivisionsControl ID="DivisionsControl1" runat="server" />
</asp:Content>
