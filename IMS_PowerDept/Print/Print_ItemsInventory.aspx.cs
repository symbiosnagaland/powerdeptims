using IMS_PowerDept.AppCode;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Print
{
    public partial class Print_ItemsInventory : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

            //this is for one item
            if (Request.QueryString["item"] != null)
            {
                string itemNameDecoded = HttpUtility.UrlDecode(Request.QueryString["item"].ToString());
                con.Open();             
                SqlCommand cmdw = new SqlCommand("sp_GetDetailedInventoryByItemName '" + itemNameDecoded + "'", con);
                SqlDataReader drd = cmdw.ExecuteReader();              
                GridView1.DataSource = drd;
                GridView1.DataBind();
                drd.Close();
                con.Close();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
             
            }
            else  if (Request.QueryString["IssueHeadName"] != null) 
                {
                    string decodedIHN = Utilities.QueryStringDecode(Request.QueryString["IssueHeadName"]);
                    string dateStockPosition = "";
                
                //from date and to date - all issue head , specific issue head, dates can be empty or not empty
                //1 no dates and all issuehead
                //2 no dates and specific issuehead
                //3 dates and all issue heads
                //4 dates and specific issue head
                    if ((decodedIHN == "All")  && (Request.QueryString["date"] == null)) //1
                    {
                        SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
                        SqlCommand cmd = new SqlCommand("sp_GetDetailedInventory", conn);
                        DataTable dt = new DataTable();
                        try
                        {                          
                            conn.Open();                            
                            SqlDataReader drd = cmd.ExecuteReader();
                            dt.Load(drd);
                                  drd.Close();
                                      }
                                     catch (Exception ex)
                                     {
                                ex.ToString();

                                    }
                                 finally
                                  {
                                conn.Close();
                                         }

                        if (Request.QueryString["Id"] == "1")
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.UseAccessibleHeader = true;
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                            GridView1.Visible = true;
                        }
                        else if (Request.QueryString["Id"] == "2")
                        {
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            GridView2.UseAccessibleHeader = true;
                            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }     

                    }

                    else if ((decodedIHN != "All") && (Request.QueryString["date"] == null))//if specific Ihn 2
                    {
                      if (Request.QueryString["Id"] == "1")
                        {
                        GridView1.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(decodedIHN).Tables[0];
                        GridView1.DataBind();
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.Visible = true;
                      }              
                       else if (Request.QueryString["Id"] == "2")                      
                      {
                                                
                            GridView2.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(decodedIHN).Tables[0];
                            GridView2.DataBind();
                            GridView2.Visible = true;
                            GridView2.UseAccessibleHeader = true;
                            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                        }

                    }
                //3
                    else if ((decodedIHN == "All") && (Request.QueryString["date"] != null))//if all Ihn  and dates 3
                    {
                        dateStockPosition = Request.QueryString["date"];
                       
                        lblFromDate.Visible = true;

                       

                       

                        lblFromDate.Text = "Stock Position Date: " + dateStockPosition;
                        ///


                     
                        if (Request.QueryString["Id"] == "1")
                        {
                            GridView1.DataSource = SelectedIssueHeadDetails.GetAllDetailsByDate(dateStockPosition);
                            GridView1.DataBind();
                            GridView1.UseAccessibleHeader = true;
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                            GridView1.Visible = true;
                        }
                        else if (Request.QueryString["Id"] == "2") 
                        {
                            GridView2.DataSource = SelectedIssueHeadDetails.GetAllDetailsByDate(dateStockPosition);
                            GridView2.DataBind();
                            GridView2.UseAccessibleHeader = true;
                            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                            GridView2.Visible = true;
                        }
                    }
                  else if ((decodedIHN != "All") && (Request.QueryString["date"] != "") )//if specific Ihn and dates 4
                    {
                        dateStockPosition = Request.QueryString["date"];
               
                        lblFromDate.Visible  = true;

                        lblFromDate.Text = "Stock Position Date: " + dateStockPosition;
                      
                       

                        if (Request.QueryString["Id"] == "1")
                        {

                            GridView1.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(decodedIHN, dateStockPosition);
                                GridView1.DataBind();
                                GridView1.UseAccessibleHeader = true;
                                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                                GridView1.Visible = true;

                           
                        }
                        else if (Request.QueryString["Id"] == "2")
                        {
                            GridView2.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(decodedIHN, dateStockPosition);
                            GridView2.DataBind();
                            GridView2.UseAccessibleHeader = true;
                            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                            GridView2.Visible = true;
                        }


                    }

                }
            }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i = 1;
            int tempcounter = i + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
        }
        }
    

     