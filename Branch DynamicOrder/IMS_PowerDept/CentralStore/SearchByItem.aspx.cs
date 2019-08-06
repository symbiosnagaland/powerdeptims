using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.CentralStore
{
    public partial class SearchByItem : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        SqlDataAdapter dadapter;
        DataSet dset;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
        }


        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedItemNameDetails(ddlItemName.SelectedValue.ToString());


            }
            catch (Exception xx)
            {
                throw xx;
            }
        }
        protected void gvItemsIssued_PageIndexChanged(object sender, EventArgs e)
        {

            //  
        }
        protected void gvItemsIssued_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvItemsIssued.PageIndex = e.NewPageIndex;
            SelectedItemNameDetails(ddlItemName.SelectedValue.ToString());


        }

        protected void gvItemsReceived_DataBound1(object sender, EventArgs e)
        {

        }

        protected void gvItemsReceived_PageIndexChanged(object sender, EventArgs e)
        {

            // 
        }
        protected void gvItemsReceived_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvItemsReceived.PageIndex = e.NewPageIndex;
            SelectedItemNameDetails(ddlItemName.SelectedValue.ToString());

        }
        protected void gvItemsIssued_DataBound1(object sender, EventArgs e)
        {
            //
        }

        private void SelectedItemNameDetails(string pStrItemName)
        {

            gvItemsReceived.DataSource = SelectedIssueHeadDetails.GetDetailsOfSelectedItemName(pStrItemName).Tables[0];
            gvItemsReceived.DataBind();
            gvItemsReceived.Visible = true;
            gvItemsIssued.DataSource = SelectedIssueHeadDetails.GetDetailsOfSelectedItemName(pStrItemName).Tables[1];
            gvItemsIssued.DataBind();
            gvItemsIssued.Visible = true;

            if (gvItemsReceived.Rows.Count > 0)
            {
                gvItemsReceived.FooterRow.Cells[6].Text = SelectedIssueHeadDetails.GetDetailsOfSelectedItemName(pStrItemName).Tables[0].Compute("sum(Quantity)", "").ToString();
                LblTotal1.Text = gvItemsReceived.FooterRow.Cells[6].Text;
            }
            else
            {
                 LblTotal1.Text = "0.00";
            }
            if (gvItemsIssued.Rows.Count >0)
            {
                gvItemsIssued.FooterRow.Cells[6].Text = SelectedIssueHeadDetails.GetDetailsOfSelectedItemName(pStrItemName).Tables[1].Compute("sum(Quantity)", "").ToString();
                LblTotal2.Text = gvItemsIssued.FooterRow.Cells[6].Text;
            }
            else
            {
                LblTotal2.Text = "0.00";
            }

            Label1.Visible = true;
            Label2.Visible = true;
        }

    }
}