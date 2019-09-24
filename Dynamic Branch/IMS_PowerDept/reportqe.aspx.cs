using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace IMS_PowerDept
{
    public partial class reportqe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DivisionName.Text = Session["DivisionName"].ToString();
            st.Text = Session["BeginDate"].ToString();
            ed.Text = Session["EndingDate"].ToString();
            if (!IsPostBack)
            {
                gvchead.DataSource = GetData("SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan WHERE (IndentingDivisionName = '"+Session["DivisionName"].ToString()+"') AND (IsDeliveredTemporary = 'No')");
                gvchead.DataBind();
            }
           
        }

        private static DataTable GetData(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private void retrivechallnAll()
        {

        }
        protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlOrders").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                string chHead = gvchead.DataKeys[row.RowIndex].Value.ToString();
                GridView gvOrders = row.FindControl("gvchildDetails") as GridView;
                BindOrders(chHead, gvOrders);
            }
            else
            {
                //row.FindControl("pnlOrders").Visible = false;
                //imgShowHide.CommandArgument = "Show";
                //imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }
        private void BindOrders(string chHead, GridView gvOrders)
        {
           
            gvOrders.DataSource = GetData(string.Format("Select * from DeliveryItemsChallan where ChargeableHeadName='{0}'", chHead));
            gvOrders.DataBind();
        }

        protected void Show_Hide_ProductsGrid(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
                {
                row.FindControl("pnlProducts").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            int DeliveryItemsChallanID = Convert.ToInt32((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView ggvitems = row.FindControl("ggvitems") as GridView;
            BindProducts(DeliveryItemsChallanID, ggvitems);
                }
            else
            {
                //row.FindControl("pnlProducts").Visible = false;
                //imgShowHide.CommandArgument = "Show";
                //imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }

        private void BindProducts(int DeliveryItemsChallanID, GridView ggvitems)
        {
            //ggvitems.ToolTip = DeliveryItemsChallanID.ToString();
            ggvitems.DataSource = GetData(string.Format("SELECT dbo.DeliveryItemsChallan.TotalAmount, dbo.DeliveryItemsDetails.ItemName, dbo.DeliveryItemsDetails.IssueHeadName, dbo.DeliveryItemsDetails.Quantity, dbo.DeliveryItemsDetails.Unit, dbo.DeliveryItemsDetails.Rate FROM  dbo.DeliveryItemsChallan INNER JOIN dbo.DeliveryItemsDetails ON dbo.DeliveryItemsChallan.DeliveryItemsChallanID = dbo.DeliveryItemsDetails.DeliveryItemsChallanID WHERE (dbo.DeliveryItemsChallan.DeliveryItemsChallanID  = '{0}')", DeliveryItemsChallanID));
            ggvitems.DataBind();
        }

        protected void gvchildDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvchildDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void gvchead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
    }
}