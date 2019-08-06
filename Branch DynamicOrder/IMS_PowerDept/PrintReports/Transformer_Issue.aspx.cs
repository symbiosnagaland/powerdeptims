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
    public partial class Transformer_Issue : System.Web.UI.Page
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
                dadapter = new SqlDataAdapter("select ti.challanno, ti.challandate, ti.division, trd.voltage, trd.kva, trd.make, trd.seriel, trd.oil, tid.oiltype from transformer_issue AS ti inner JOIN  transformer_IssueDetails AS tid ON ti.ChallanNo = tid.ChallanNo inner JOIN  Transformer_ReceiptsDetails AS trd ON tid.challanno = ti.challanno and trd.receivedtransformerid = tid.transformerid and ti.challandate between '" + st.Text + "' and '" + ed.Text + "'  order by ti.division", con);//using challan date
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