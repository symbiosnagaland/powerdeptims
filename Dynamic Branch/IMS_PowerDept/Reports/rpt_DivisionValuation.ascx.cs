using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace IMS_PowerDept.Reports
{
    public partial class rpt_DivisionValuation : System.Web.UI.UserControl
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            retrivedetails();
        }

        //edit the gridview

        private void FilterGridview()
        {
            try
            {
              
            }
            catch
            {
                throw;
            }
        }


        private void retrivedetails()
        {
            try
            {
               
            }
            catch
            {

            }
        }
    }
}