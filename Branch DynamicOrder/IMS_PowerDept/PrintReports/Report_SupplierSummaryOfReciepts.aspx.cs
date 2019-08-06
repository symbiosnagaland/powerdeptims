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
    public partial class Report_SupplierSummaryOfReciepts : System.Web.UI.Page
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
        }
        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("select Supplier,SupplyOrderReference,SupplyOrderDate,ReceivedItemsOTEOID,ReceivedItemOTEODate,ChargeableHeadName from ReceivedItemsOTEO where SupplyOrderDate between '" + stDate + "' and '" + edDate + "'  order by Supplier", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                _gridChEdit.DataSource = dset.Tables[0];
                _gridChEdit.DataBind();
            }
            catch
            {
                throw;
            }
        }
    }
}