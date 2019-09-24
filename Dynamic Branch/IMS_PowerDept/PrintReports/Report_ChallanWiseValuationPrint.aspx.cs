using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace IMS_PowerDept.PrintReports
{
    public partial class Report_ChallanWiseValuationPrint : System.Web.UI.Page
    {
        SqlDataAdapter dadapter;
        DataSet dset;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        double totalPrice = 0;
        double qtyTotal = 0;
       // int grQtyTotal = 0;
       // int storid = 0;
        int rowIndex = 1;
        string stDate, edDate;

        string ch_headvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            st.Text = Session["BeginDate"].ToString();
            stDate = DateTime.ParseExact(Session["BeginDate"].ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            ed.Text = Session["EndingDate"].ToString();
            edDate = DateTime.ParseExact(Session["EndingDate"].ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            if (!IsPostBack)
            {
                retriveData();
            }
          
        }

        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("select distinct IndentingDivisionName from DeliveryItemsChallan where IsDeliveredTemporary = 'No' and ChallanDate between '" + stDate + "' and  '" + edDate + "' ", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                GridView1.DataSource = dset.Tables[0];
                GridView1.DataBind();
            }
            catch
            {
                throw;
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gv = (GridView)e.Row.FindControl("GridView2");
                Label DEP_ID = e.Row.FindControl("_lbldivi") as Label;
                SqlCommand cmd = new SqlCommand("Select * from DeliveryItemsChallan where IndentingDivisionName='" + DEP_ID.Text.ToString() + "' and ChallanDate between '" + stDate + "' and  '" + edDate + "'  order by ChargeableHeadName", con);
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

                //HiddenField storid = e.Row.FindControl("cId") as HiddenField;
                //storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeliveryItemsChallanID").ToString());
                ch_headvalue = DataBinder.Eval(e.Row.DataItem, "ChargeableHeadName").ToString();


                double tmpTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalAmount").ToString());
                qtyTotal += tmpTotal;
                // grQtyTotal += tmpTotal;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");

                lblTotalPrice.Text = "<b>Total:</b> " + totalPrice.ToString();
                totalPrice = 0;
                //Label lblTotalqty = (Label)e.Row.FindControl("lblsubtotal");
                //lblTotalqty.Text = grQtyTotal.ToString();

            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            bool newRow = false;
            //for the ch heads row in a divison but not the last row
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ChargeableHeadName") != null))
            {
                if (ch_headvalue != DataBinder.Eval(e.Row.DataItem, "ChargeableHeadName").ToString())
                    newRow = true;

            }
            //for the same ch head row as abobe but in the last row in a division
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ChargeableHeadName") == null))
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
                HeaderCell.ColumnSpan = 5;
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