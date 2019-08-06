<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_SupplierSummaryOfReciepts.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Report_SupplierSummaryOfReciepts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div style="padding:10px; margin:0px auto; width: 961px;">
      <h2 style="margin:4px">Summary of Reciepts</h2>
        <asp:SqlDataSource ID="sdsscource" runat="server"></asp:SqlDataSource>
     
      Period :&nbsp; <asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp;&nbsp; and&nbsp;
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />


                                            <table style="width:940px; " >
                                            <thead>
                                             <tr>
                                                   <td style="text-align:left; font-size:16px; font-weight:bold; width:200px">
                                                        Supplier</td>

                                                 <td style="text-align:left; font-size:16px; font-weight:bold;  width:180px"> 
                                                    S Order Ref</td>

                                               <td style="text-align:left; font-size:16px; font-weight:bold;  width:120px"> 
                                                     S Order Date</td>

                                                 <td style="text-align:left; font-size:16px; font-weight:bold;  width:120px">
                                                    OTEO Ref</td>

                                                <td style="text-align:left; font-size:16px; font-weight:bold;  width:120px">  
                                                    OTEO Date</td>

                                                  <td style="text-align:left; font-size:16px; font-weight:bold;  width:160px">  
                                                    Ch Head</td>
                                            </tr>
                                            </thead>
     
                                     
                                   
     
                                <asp:Repeater ID="_gridChEdit" runat="server" >
                                    <ItemTemplate>
                                           <tr>
                                                 <td style="text-align:left; width:200px">                                                   
                                                    <asp:Label ID="aa" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.Supplier")%>'></asp:Label>
                                                </td>
                                            <td style="text-align:left; width:180px"> 
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.SupplyOrderReference")%>'></asp:Label>
                                               
                                                 </td>
                                                  <td style="text-align:left; width:120px">                                                   
                                                    <asp:Label ID="Label1" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.SupplyOrderDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </td>
                                            <td style="text-align:left; width:120px"> 
                                                <asp:Label ID="Label2" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.ReceivedItemsOTEOID")%>'></asp:Label>
                                               
                                                 </td>
                                                  <td style="text-align:left; width:120px">                                                   
                                                    <asp:Label ID="Label3" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.ReceivedItemOTEODate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </td>
                                            <td style="text-align:left; width:160px"> 
                                                <asp:Label ID="Label4" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.ChargeableHeadName")%>'></asp:Label>
                                               
                                                 </td>
                                         
                                           </tr>
                                                                              
                                    </ItemTemplate>
                                </asp:Repeater>    
                   
                                 </table>







        <%--<table>
            <tr>
                <td>

               
        <asp:GridView ID="gvSupplier" AutoGenerateColumns="False" GridLines="None" runat="server" Width="850px">
           <Columns>
                <asp:BoundField DataField="Supplier" HeaderText="Supplier" SortExpression="Supplier" >
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="SupplyOrderReference" HeaderStyle-HorizontalAlign="Left" HeaderText="S Order Re" SortExpression="SupplyOrderReference" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
               <asp:BoundField DataField="SupplyOrderDate" HeaderText="S Order Date" SortExpression="SupplyOrderDate" />
                 <asp:BoundField DataField="ReceivedItemsOTEOID" HeaderText="OTEO Ref" SortExpression="ReceivedItemsOTEOID" />
                 <asp:BoundField DataField="ReceivedItemOTEODate" HeaderText="OTEO Date" SortExpression="ReceivedItemOTEODate" />
                <asp:BoundField DataField="ChargeableHeadName" HeaderText="Ch Head" SortExpression="ChargeableHeadName" />
                                                                                                                
                                                 
           </Columns>
            <HeaderStyle Font-Size="15px" HorizontalAlign="Left" />
        </asp:GridView>
                     </td>
            </tr>
      
              </table>--%>
    </div>
    </form>
</body>
</html>
