<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transformer_Issue.aspx.cs" Inherits="IMS_PowerDept.PrintReports.Transformer_Issue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="Report_style.css" />
</head>
<body onload="javascript:window.print()">
    <form id="form1" runat="server">
    <div style="padding:10px; margin:0px auto; width: 961px;">
      <h2 style="margin:4px">Transformer Issued Details </h2>
        <asp:SqlDataSource ID="sdsscource" runat="server"></asp:SqlDataSource>
     
     &nbsp;  &nbsp; Period :<asp:Label ID="st" runat="server" Text=""></asp:Label>
       &nbsp; to &nbsp;
       <asp:Label ID="ed" runat="server" Text=""></asp:Label>
       <hr />


                                            <table style="width:100%" >
                                            <thead>
                                             <tr>
                                                   <td style="text-align:left;font-weight:bold; font-size:16px;   width:22%">
                                                        Division</td>

                                                 <td style="text-align:left; font-size:16px; font-weight:bold;  width:12%"> 
                                                  Challan No.</td>

                                               <td style="text-align:left; font-size:16px; font-weight:bold;  width:10%"> 
                                                     Ch. Date</td>

                                       

                                                <td style="text-align:left; font-size:16px; font-weight:bold;  width:12%">  
                                                    Voltage</td>

                                                  <td style="text-align:left; font-size:16px; font-weight:bold;  width:10%">  
                                                    kVA</td>

                                                      <td style="text-align:left; font-size:16px; font-weight:bold;  width:12%">  
                                                    Make</td>
                                                    <td style="text-align:left; font-size:16px; font-weight:bold;  width:10%">  

                                                     Seriel
                                                 </td>
                                                   <td style="text-align:left; font-size:16px; font-weight:bold;  width:10%">  

                                                     Oil Type
                                                 </td>
                                            </tr>
                                            </thead>
     
                                     
                                   
     
                                <asp:Repeater ID="_gridChEdit" runat="server" >
                                    <ItemTemplate>
                                           <tr>
                                                 <td>                                                   
                                                    <asp:Label ID="aa" runat="server" Text=' <%#DataBinder.Eval(Container, "DataItem.division")%>'></asp:Label>
                                                </td>
                                            <td >
                                                <asp:Label ID="cHEad" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.challanno")%>'></asp:Label>
                                               
                                                 </td>
                                                  <td >                                                
                                                    <asp:Label ID="Label1" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.challandate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </td>

                                       


                                            <td >
                                                <asp:Label ID="Label2" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.voltage")%>'></asp:Label>
                                               
                                                 </td>
                                                  <td >                                                
                                                    <asp:Label ID="Label3" runat="server" Width="90px" Text=' <%#DataBinder.Eval(Container, "DataItem.kva")%>'></asp:Label>
                                                </td>
                                            <td > 
                                                <asp:Label ID="Label4" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.Make")%>'></asp:Label>
                                                                </td> 

                                                       <td > 
                                                <asp:Label ID="Label5" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.seriel")%>'></asp:Label>
                                                                </td> 
                               

                                                     <td >
                                                <asp:Label ID="Label7" runat="server" Text='  <%#DataBinder.Eval(Container, "DataItem.oiltype")%>'></asp:Label>
                                               
                                                 </td>
                                         
                                           </tr>

                                 
                                                                              
                                    </ItemTemplate>
                               
                                </asp:Repeater>    
                   

                                                <tr>
                                                    <td>
                                                        <h3>
                                                        Total Jobs:  <asp:Label ID="lblCount" runat="server" ></asp:Label>
                                                    </h3>
                                                            </td>
                                                </tr>
                                 </table>







      
    </div>
    </form>
</body>
</html>
