using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.CentralStore
{
    public partial class ReceivedItems_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            querystringid.Value = Request.QueryString["Id"];
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
          //  Response.Redirect("~/Print/ePrint.aspx?Id=" + querystringid.Value);
            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            if (appPath != "/")
                Response.Redirect(appPath + "/Print/ePrint.aspx?Id=" + querystringid.Value);
            else
                Response.Redirect("/Print/ePrint.aspx?Id=" + querystringid.Value);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
          //  Response.Redirect("~/Print/ePrint.aspx?Id=" + querystringid.Value);

            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            if (appPath != "/")
                Response.Redirect(appPath + "/Print/ePrint.aspx?Id=" + querystringid.Value);
            else
                Response.Redirect("/Print/ePrint.aspx?Id=" + querystringid.Value);
        }
    }
}