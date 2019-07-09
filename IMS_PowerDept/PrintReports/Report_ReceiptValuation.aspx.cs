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
    public partial class Report_ReceiptValuation : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        SqlDataAdapter dadapter;
        DataSet dset;

        string ch_headvalue = "";
        double totalPrice = 0;
        double qtyTotal = 0;
        //int grQtyTotal = 0;
        int storid = 0;
        int rowIndex = 1;

      
        protected void Page_Load(object sender, EventArgs e)
        {
            st.Text = Session["BeginDate"].ToString();
            ed.Text = Session["EndingDate"].ToString();
            if (!IsPostBack)
            {
                retriveData();
            }
        }
        private void retriveData()
        {
            try
            {

                dadapter = new SqlDataAdapter("SELECT DISTINCT  ChargeableHeadName FROM  ReceivedItemsOTEO  where ReceivedItemOTEODate between '" + st.Text + "' and  '" + ed.Text + "'", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                gv1.DataSource = dset.Tables[0];
                gv1.DataBind();

           


            }
            catch
            {
                throw;
            }
        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gv = (GridView)e.Row.FindControl("gv2");
                Label DEP_ID = e.Row.FindControl("chName") as Label;
                HiddenField hj = e.Row.FindControl("hfid") as HiddenField;
                SqlCommand cmd = new SqlCommand("Select * from ReceivedItemsOTEO where  ChargeableHeadName='" + DEP_ID.Text.ToString() + "' and ReceivedItemOTEODate between '" + st.Text + "' and  '" + ed.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void gv3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                double price = Double.Parse(lblPrice.Text);
                totalPrice += price;

               // storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ReceivedItemsOTEOID").ToString());
               // ch_headvalue = DataBinder.Eval(e.Row.DataItem, "ItemName").ToString();

                //asen
             
                double tmpTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "amount"));

                qtyTotal += tmpTotal;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
                lblTotalPrice.Text = "<b>Total:</b> " + String.Format("{0:0.00}", totalPrice); ;
                totalPrice = 0;
            }
        }

        protected void gv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvs = (GridView)e.Row.FindControl("gv3");
                Label et = (Label)e.Row.FindControl("cno");
                string txttemp = et.Text;
                DataSet ds = new DataSet();
                con.Open();
             //   string cmdstr = "Select * from ReceivedItemsDetails WHERE ReceivedItemsOTEOID = @ReceivedItemsOTEOID and (ReceivedItemOTEODate between @startdate and @enddate)";
                string cmdstr = "Select * from ReceivedItemsDetails WHERE ReceivedItemsOTEOID = @ReceivedItemsOTEOID";

                SqlCommand cmd = new SqlCommand(cmdstr,con);
               // cmd.Parameters.AddWithValue("@startdate", st.Text);
               // cmd.Parameters.AddWithValue("@enddate", ed.Text);
                cmd.Parameters.AddWithValue("@ReceivedItemsOTEOID", txttemp);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
                adp.Fill(ds);
                con.Close();
                gvs.DataSource = ds;
                gvs.DataBind();

            }
        }

        protected void gv3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //bool newRow = false;
            ////for the ch heads row in a divison but not the last row
            //if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ItemName") != null))
            //{
            //    if (ch_headvalue != DataBinder.Eval(e.Row.DataItem, "ItemName").ToString())
            //        newRow = true;

            //}
            //for the same ch head row as abobe but in the last row in a division
            //if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ItemName") == null))
            //{
            //    newRow = true;
            //    rowIndex = 0;
            //    //since its last row reseeting ch head value because if not then same value is passing over to the next division
            //    ch_headvalue = "";
            //}
            //if (newRow)
            //{
            //    GridView GridView1 = (GridView)sender;
            //    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    NewTotalRow.Font.Bold = true;

            //    NewTotalRow.ForeColor = System.Drawing.Color.Black;
            //    TableCell HeaderCell = new TableCell();
            //    HeaderCell.Text = "Sub Total";
            //    HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //    HeaderCell.ColumnSpan = 4;
            //    NewTotalRow.Cells.Add(HeaderCell);
            //    HeaderCell = new TableCell();
            //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //    HeaderCell.Text = qtyTotal.ToString();
            //    NewTotalRow.Cells.Add(HeaderCell);
            //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            //    rowIndex++;
            //    qtyTotal = 0;
            //}
        }
    }
}