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
    public partial class Transformer_Receipts : System.Web.UI.Page
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
                dadapter = new SqlDataAdapter("select tr.challanno, tr.challandate, tr.division, tr.receiptdate, trd.voltage, trd.kva, trd.make, trd.seriel, trd.oil from transformer_receipts tr, transformer_receiptsdetails trd where tr.challanno=trd.challanno and tr.challandate between '" + st.Text + "' and '" + ed.Text + "'  order by tr.division", con);//using challan date

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