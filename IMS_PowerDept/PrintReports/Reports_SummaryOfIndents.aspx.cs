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
    public partial class Reports_SummaryOfIndents : System.Web.UI.Page
    {
        SqlDataAdapter dadapter;
        DataSet dset;

        string stDate, edDate;
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

            st.Text = Session["BeginDate"].ToString();
            ed.Text = Session["EndingDate"].ToString();

            stDate = DateTime.ParseExact(Session["BeginDate"].ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            edDate = DateTime.ParseExact(Session["EndingDate"].ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            if (!IsPostBack)
            {
                retriveData();
            }

            //SELECT DISTINCT ChargeableHeadName FROM DeliveryItemsChallan WHERE (IndentingDivisionName = @IndentingDivisionName) AND (IsDeliveredTemporary = 'No')
        }
        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("SELECT DISTINCT IndentingDivisionName FROM DeliveryItemsChallan WHERE ChallanDate between '" + stDate + "' and '" + edDate + "' AND IsDeliveredTemporary = 'No' order by ChallanDate ", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                gv1.DataSource = dset.Tables[0];
                gv1.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gv = (GridView)e.Row.FindControl("gv2");
                Label DEP_ID = e.Row.FindControl("indiv") as Label;
                //HiddenField hj = e.Row.FindControl("hfid") as HiddenField;
                SqlCommand cmd = new SqlCommand("Select * from DeliveryItemsChallan where IndentingDivisionName='" + DEP_ID.Text.ToString() + "'and (challandate between @startdate and @enddate)", con);
                cmd.Parameters.AddWithValue("@startdate", stDate);
                cmd.Parameters.AddWithValue("@enddate", edDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

    }
}