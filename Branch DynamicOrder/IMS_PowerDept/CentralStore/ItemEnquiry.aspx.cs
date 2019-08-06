using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.CentralStore
{
    public partial class ItemEnquiry : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        private bool tableCopied = false;
        private DataTable originalTable;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearchImage_ServerClick(object sender, EventArgs e)
        {

        }
        protected void btnAdvancedSearchFilters_Click(object sender, EventArgs e)
        {
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnUpdateMinimumIndicator_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItemsInventory.Rows.Count; i++)
            {
                foreach (GridViewRow row in gvItemsInventory.Rows)
                {
                    CheckBox chkUpdate = (CheckBox)
                       gvItemsInventory.Rows[i].Cells[0].FindControl("CHEK");

                    if (chkUpdate != null)
                    {
                        if (chkUpdate.Checked)
                        {
                            string strID = ((Label)row.FindControl("tbitemid")).Text;
                            //string ihead = ((Label)row.FindControl("Label1")).Text;
                            string lblname = ((TextBox)row.FindControl("tbMinimumUnitsIndicator")).Text;

                            con.Open();

                            string q = "UPDATE ItemsInventory SET MinimumUnitIndicator = @MinimumUnitIndicator where ItemsInventoryID='" + strID + "'";
                            SqlCommand comm = new SqlCommand(q, con);
                            comm.Parameters.AddWithValue("MinimumUnitIndicator", lblname);
                            comm.ExecuteNonQuery();
                            //Response.Redirect(Request.Url.ToString());
                            Label2.Text = " Details Updated Successfully";
                            Label2.ForeColor = Color.Green;

                            con.Close();
                        }
                    }

                }
            }
        }

    }
}