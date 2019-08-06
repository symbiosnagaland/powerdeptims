using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Shared
{
    public partial class Transformers_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
                Label1.Text = Session["username"].ToString();
            else
                Response.Redirect("~/Login.aspx?message=session-timed-out");
        }

        protected void lbMainDashBoard_Click(object sender, EventArgs e)
        {
            //depending on session role will send to central store module or to admin module
            //temp -sending directly to central store
            Response.Redirect("~/CentralStore/Dashboard.aspx");
        }
    }
}