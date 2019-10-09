using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Admin
{
    public partial class ITEM_WISEISSUED : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            // DisableControls(_gvActiveBroadBandUsers);// comment out if there are any asp.net controls
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Issued_Items" +  DateTime.Now.ToString("dd-MM-yyy_hh:mm:ss") +".xls");
            Response.Charset = "";

            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);



            Response.Write(stringWrite.ToString());
            Response.End();



        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            panelAdvancedSearchFilters.Visible = false;
        }
    }
}