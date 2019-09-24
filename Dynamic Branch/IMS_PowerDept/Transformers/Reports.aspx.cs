using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IMS_PowerDept.Transformers
{
    public partial class Reports : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);


        private bool ValidateDates()
        {

            if (tbBeginDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return false;

            }
            if (tbEndDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return false;

            }
            else
            {
                return true;
            }
        }
        private void OpenNewPage(string pageName)
        {

         string appPath = HttpRuntime.AppDomainAppVirtualPath;
           if (appPath != "/")

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>
                   window.open('" + appPath + "'/PrintReports/" +pageName + "','_newtab');                    </script>", false);

          else
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "click", "window.open('/PrintReports/Transformer_Receipts.aspx?sd='" + beginDate + "&ed='" + endDate + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page),
              "click", "window.open('/PrintReports/" +pageName + "','_newtab');", true);
            }
    

     
        protected void btnReceipt_Click(object sender, EventArgs e)
        {

            if (ValidateDates())
            {
                //string beginDate = tbBeginDate.Text;
                //string endDate = tbEndDate.Text;
                Session["BeginDate"] = tbBeginDate.Text;
                Session["EndingDate"] = tbEndDate.Text;
               OpenNewPage("Transformer_Receipts.aspx");
            }
        
        }

        protected void btnJobs_Click(object sender, EventArgs e)
        {
            if (ValidateDates())
            {
                Session["BeginDate"] = tbBeginDate.Text;
                Session["EndingDate"] = tbEndDate.Text;
                OpenNewPage("Transformer_Jobs.aspx");
            }
        }

        protected void btnIssues_Click(object sender, EventArgs e)
        {
            if (ValidateDates())
            {
                Session["BeginDate"] = tbBeginDate.Text;
                Session["EndingDate"] = tbEndDate.Text;
                OpenNewPage("Transformer_Issue.aspx");
            }
        }


    }
}