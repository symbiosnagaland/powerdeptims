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
    public partial class Transformer_Jobs : System.Web.UI.Page
    {
        SqlDataAdapter dadapter;
        DataTable dt;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            st.Text = Session["BeginDate"].ToString();
            ed.Text = Session["EndingDate"].ToString();
            if (!IsPostBack)
            {
                retriveData();
            }
        }
        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("select tj.challanno, tj.challandate, tj.repairfirm, trd.voltage, trd.kva, trd.make, trd.seriel, trd.oil, tjd.jobno from transformer_Job AS tj inner JOIN  transformer_JobDetails AS tjd ON tj.ChallanNo = tjd.ChallanNo inner JOIN  Transformer_ReceiptsDetails AS trd ON tjd.challanno =tj.challanno and trd.receivedtransformerid = tjd.transformerid and tj.challandate between '" + st.Text + "' and '" + ed.Text + "'  order by tj.repairfirm", con);//using challan date



                dt = new DataTable();
                dadapter.Fill(dt);
                _gridChEdit.DataSource = dt;
                _gridChEdit.DataBind();

                lblCount.Text = dt.Rows.Count.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}