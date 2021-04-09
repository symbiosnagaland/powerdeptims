using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Shared
{
    public partial class Admin_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Url.ToString().Contains("localhost:61111"))
           // Session["username"] = "0";
            if (Session["username"] != null)
                Label1.Text = Session["username"].ToString();
            else
                Response.Redirect("~/Login.aspx?message=session-timed-out");
        }
    }
}