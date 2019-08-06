using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.PrintReports
{
    public partial class Report_BalanceSheet2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            st.Text = System.DateTime.Today.ToShortDateString();
            h1.Value = Session["Ihead"].ToString();
            gv1.DataBind();
            gv1.UseAccessibleHeader = true;   
            gv1.HeaderRow.TableSection = TableRowSection.TableHeader;
           
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i = 1;
            int tempcounter = i + 1;
            if (tempcounter == 20)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    GridView gv = (GridView)e.Row.FindControl("gev");
            //    Label DEP_ID = e.Row.FindControl("chaalID") as Label;

            //   // SqlCommand cmd = new SqlCommand("select distinct  unit, Rate from ReceivedItemsDetails where  ItemID='" + DEP_ID.Text + "' and Rate = (select MAX(rate) from ReceivedItemsDetails where ItemID='" + DEP_ID.Text + "')", con);
            //    SqlCommand cmd = new SqlCommand("SELECT DISTINCT ItemName, MAX(Rate) AS Rate, ItemID, unit FROM ReceivedItemsDetails  where ItemID='" + DEP_ID.Text + "' GROUP BY ItemID, ItemName, unit", con);
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    con.Close();
            //    gv.DataSource = ds;
            //    gv.DataBind();
            //}
        }
    }
}