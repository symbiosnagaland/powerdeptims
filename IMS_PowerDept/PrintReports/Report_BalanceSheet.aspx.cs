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
    public partial class Report_BalanceSheet : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
      
        protected void Page_Load(object sender, EventArgs e)
        {
            st.Text = System.DateTime.Today.ToShortDateString();
            gv1.DataBind();
            gv1.UseAccessibleHeader = true;
            gv1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i = 1;
            int tempcounter = i + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        //protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
          
        //}
    }
}