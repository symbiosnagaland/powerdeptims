﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;

namespace IMS_PowerDept.PrintReports
{
    public partial class Report_HeadWiseValuation2 : System.Web.UI.Page
    {
        double totalPrice = 0;
        double qtyTotal = 0;
        int grQtyTotal = 0;
        decimal storid = 0;
        int rowIndex = 1;
        SqlDataAdapter dadapter;
        DataSet dset;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        string isssuehead;
        string ch_headvalue = "";

        //this 2 variable are used for changing the date formats in mm/dd/yy
        string stDate;
        string edDate;

        protected void Page_Load(object sender, EventArgs e)
        {

            isssuehead = Session["IssueHead"].ToString();
            st.Text = Session["BeginDate"].ToString();
            stDate = DateTime.ParseExact(st.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            
            ed.Text = Session["EndDate"].ToString();
            edDate = DateTime.ParseExact(ed.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            rertivebyDate();
        }

        private void rertivebyDate()
        {
            try
            {
                dadapter = new SqlDataAdapter(" SELECT distinct dbo.DeliveryItemsChallan.IndentingDivisionName FROM  dbo.DeliveryItemsChallan INNER JOIN dbo.DeliveryItemsDetails ON dbo.DeliveryItemsChallan.DeliveryItemsChallanID = dbo.DeliveryItemsDetails.DeliveryItemsChallanID WHERE (dbo.DeliveryItemsChallan.IsDeliveredTemporary = 'No') AND (dbo.DeliveryItemsDetails.IssueHeadName = '" + Session["IssueHead"].ToString() + "') and dbo.DeliveryItemsChallan.ChallanDate between '" + stDate  + "' and  '" +  edDate  + "'", con);
                //dadapter = new SqlDataAdapter("select distinct IndentingDivisionName ,DeliveryItemsChallanID  from DeliveryItemsChallan where IsDeliveredTemporary = 'No' and ChallanDate between '" + st.Text + "' and  '" + ed.Text + "'", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                GridView1.DataSource = dset.Tables[0];
                GridView1.DataBind();
            }
            catch
            {
                //throw;
            }
        }

        protected void previewbtn_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gv = (GridView)e.Row.FindControl("GridView2");
                Label DEP_ID = e.Row.FindControl("_lbldivi") as Label;
                SqlCommand cmd = new SqlCommand(" SELECT distinct dbo.DeliveryItemsChallan.chargeableheadname, dbo.DeliveryItemsChallan.DeliveryItemsChallanID,  dbo.DeliveryItemsChallan.TotalAmount FROM  dbo.DeliveryItemsChallan INNER JOIN dbo.DeliveryItemsDetails ON dbo.DeliveryItemsChallan.DeliveryItemsChallanID = dbo.DeliveryItemsDetails.DeliveryItemsChallanID WHERE (dbo.DeliveryItemsChallan.IndentingDivisionName ='" + DEP_ID.Text.ToString() + "') and (dbo.DeliveryItemsDetails.IssueHeadName='" + Session["IssueHead"].ToString() + "') and dbo.DeliveryItemsChallan.ChallanDate between '" + st.Text + "' and  '" + ed.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                double price = Double.Parse(lblPrice.Text);
                totalPrice += price;

                storid = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeliveryItemsChallanID").ToString());
                ch_headvalue = DataBinder.Eval(e.Row.DataItem, "chargeableheadname").ToString();

                double tmpTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalAmount").ToString());
                qtyTotal += tmpTotal;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
                lblTotalPrice.Text = "<b>Total:</b> " + totalPrice.ToString();
                totalPrice = 0;

                //Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
                //lblTotalPrice.Text = "Total - " + totalPrice.ToString();
            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            bool newRow = false;
            //for the ch heads row in a divison but not the last row
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "chargeableheadname") != null))
            {
                if (ch_headvalue != DataBinder.Eval(e.Row.DataItem, "chargeableheadname").ToString())
                    newRow = true;

            }
            //for the same ch head row as abobe but in the last row in a division
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "chargeableheadname") == null))
            {
                newRow = true;
                rowIndex = 0;
                //since its last row reseeting ch head value because if not then same value is passing over to the next division
                ch_headvalue = "";
            }
            if (newRow)
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                NewTotalRow.Font.Bold = true;

                NewTotalRow.ForeColor = System.Drawing.Color.Black;
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Sub Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                //HeaderCell.ColumnSpan = 2;
                NewTotalRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Text = qtyTotal.ToString();
                NewTotalRow.Cells.Add(HeaderCell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
                rowIndex++;
                qtyTotal = 0;
            }
        }
    }
}
