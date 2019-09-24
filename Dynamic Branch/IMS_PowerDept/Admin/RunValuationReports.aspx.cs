using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Admin
{
    public partial class RunValuationReports : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (tbStartDateSearch.Text.Trim()=="")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (tbEndDateSearch.Text.Trim()=="")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }
            if (CheckBox1.Checked == true)
            {
                Session["IssueHead"] = issuehead.Text;
                Session["BeginDate"] = tbStartDateSearch.Text;
                Session["EndDate"] = tbEndDateSearch.Text;
               
               // Response.Redirect("~/PrintReports/Report_HeadWiseValuation2.aspx");


              
                //Response.Write("<script> window.open( '~/PrintReports/Report_HeadWiseValuation2.aspx','_blank' ); </script>");
                //Response.End();

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                     "click", @"<script>window.open('/PrintReports/Report_HeadWiseValuation2.aspx','_newtab');</script>", false);


            }

            else
            {
                Session["BeginDate"] = tbStartDateSearch.Text;
                Session["EndDate"] = tbEndDateSearch.Text;

                //Response.Redirect("~/PrintReports/Report_HeadWiseValuation.aspx");
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.open('/PrintReports/Report_HeadWiseValuation.aspx','_newtab');</script>", false);


            }
        }

        protected void detailvaluatoinByDivision_Click(object sender, EventArgs e)
        {
            if (ck2.Checked==true & ck1.Checked==true)
            {
                panelError.Visible = true;
                lblError.Text = "Select Only One Option";
                return;
            }


            if (ck2.Checked==true)
            {
                Session["DivisionName"] = ddldiv.Text;
                Session["BeginDate"] = stratdiv.Text;
                Session["EndingDate"] = enddiv.Text;
                Session["ChHead"] = ddlchd.Text;
               // Response.Redirect("~/PrintReports/Report_DivisionIssueHead2.aspx");

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                   "click", @"<script>window.open('/PrintReports/Report_DivisionIssueHead2.aspx','_newtab');</script>", false);
            }
            else
            {
                Session["DivisionName"] = ddldiv.Text;
                Session["BeginDate"] = stratdiv.Text;
                Session["EndingDate"] = enddiv.Text;
               // Response.Redirect("~/PrintReports/Report_Division-wiseIssueofMaterials.aspx");

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                   "click", @"<script>window.open('/PrintReports/Report_Division-wiseIssueofMaterials.aspx','_newtab');</script>", false);
            }


        }

        protected void receiptvaluation_Click(object sender, EventArgs e)
        {
            if (valStart.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (valEnd.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }
            Session["BeginDate"] = valStart.Text;
            Session["EndingDate"] = valEnd.Text;
            //Response.Redirect("~/PrintReports/Report_ReceiptValuation.aspx");
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('/PrintReports/Report_ReceiptValuation.aspx','_newtab');</script>", false);
        }

        protected void supplierreceipts_Click(object sender, EventArgs e)
        {
            if (tbsummary.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (endtbsummary.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }
            Session["BeginDate"] = tbsummary.Text;
            Session["EndingDate"] = endtbsummary.Text;
           // Response.Redirect("~/PrintReports/Report_SupplierSummaryOfReciepts.aspx");
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('/PrintReports/Report_SupplierSummaryOfReciepts.aspx','_newtab');</script>", false);
        }

        protected void Indents_Click(object sender, EventArgs e)
        {
            if (tbsummary.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (endtbsummary.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }

            Session["BeginDate"] = tbsummary.Text;
            Session["EndingDate"] = endtbsummary.Text;
          //  Response.Redirect("~/PrintReports/Reports_SummaryOfIndents.aspx");

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
         "click", @"<script>window.open('/PrintReports/Reports_SummaryOfIndents.aspx','_newtab');</script>", false);
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (TextBox2.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }
            Session["BeginDate"] = TextBox1.Text;
            Session["EndingDate"] = TextBox2.Text;
            //Response.Redirect("~/PrintReports/Report_ChallanAll.aspx");
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
       "click", @"<script>window.open('/PrintReports/Report_ChallanAll.aspx','_newtab');</script>", false);
        }

        protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            Populate1();
        }
        
        public void Populate1()
        {
            if (ck1.Checked==true)
            {
                ddlchd.Text = "";
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT distinct ChargeableHeadName FROM DeliveryItemsChallan where IndentingDivisionName='" + ddldiv.SelectedItem.ToString() + "'", con);
                cmd.Connection.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                ddlchd.DataSource = ddlValues;
                ddlchd.DataValueField = "ChargeableHeadName";
                ddlchd.DataTextField = "ChargeableHeadName";
                ddlchd.DataBind();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            
        }


        protected void ck1_CheckedChanged(object sender, EventArgs e)
        {
            ddlchd.Text = "";
            ddldiv.Enabled = true;
            stratdiv.Enabled = true;
            enddiv.Enabled = true;
            ck2.Checked = false;
          
        }


        protected void ck2_CheckedChanged(object sender, EventArgs e)
        {
            ddldiv.Enabled = true;
            stratdiv.Enabled = true;
            enddiv.Enabled = true;
            ddlchd.Enabled = true;
            ck1.Checked = false;
           
        }


        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
          if (CheckBox1.Checked==true)
          {
              issuehead.Enabled = true;
          }
          else
          {
              issuehead.Enabled = false;
          }
        }


        protected void tempbutton_Click(object sender, EventArgs e)
        {
            if (ck2.Checked == true & ck1.Checked == true)
            {
                panelError.Visible = true;
                lblError.Text = "Select Only One Option";
                return;
            }


            if (ck2.Checked == true)
            {
                Session["DivisionName"] = ddldiv.Text;
                Session["BeginDate"] = stratdiv.Text;
                Session["EndingDate"] = enddiv.Text;
                Session["ChHead"] = ddlchd.Text;
                //Response.Redirect("~/PrintReports/Report_TempIssuedItems2.aspx");

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
      "click", @"<script>window.open('/PrintReports/Report_TempIssuedItems2.aspx','_newtab');</script>", false);


            }
            else
            {
                Session["DivisionName"] = ddldiv.Text;
                Session["BeginDate"] = stratdiv.Text;
                Session["EndingDate"] = enddiv.Text;
                //Response.Redirect("~/PrintReports/Report_TempIssuedItems.aspx");

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
     "click", @"<script>window.open('/PrintReports/Report_TempIssuedItems.aspx','_newtab');</script>", false);


            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (chdate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Start Date";
                return;
            }
            if (chdateend.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Ending Date";
                return;
            }
            Session["BeginDate"] = chdate.Text;
            Session["EndingDate"] = chdateend.Text;
           // Response.Redirect("~/PrintReports/Report_ChallanWiseValuationPrint.aspx");

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
   "click", @"<script>window.open('/PrintReports/Report_ChallanWiseValuationPrint.aspx','_newtab');</script>", false);
        }
    }
}