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
    public partial class Report_Division_wiseIssueofMaterials : System.Web.UI.Page
    {
        SqlDataAdapter dadapter;
        DataSet dset;
        
        //SqlDataAdapter et;
        //DataSet tet;
        string ch_headvalue = "";
        double totalPrice = 0;
        double qtyTotal = 0;
        //int grQtyTotal = 0;
        int storid = 0;
        int rowIndex = 1;
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
      
        protected void Page_Load(object sender, EventArgs e)
        {
            DivisionName.Text = Session["DivisionName"].ToString();
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
               //1 getting all charageable heads
                    dadapter = new SqlDataAdapter("SELECT DISTINCT  ChargeableHeadName FROM DeliveryItemsChallan WHERE (IndentingDivisionName ='" + DivisionName.Text + "') and ChallanDate between '" + st.Text + "' and '" + ed.Text + "' AND IsDeliveredTemporary = 'No'", con);
                    dset = new DataSet();
                    dadapter.Fill(dset);
                    gvChargeableHead.DataSource = dset.Tables[0];
                    gvChargeableHead.DataBind();
               
            }
            catch
            {
                throw;
            }
        }

        protected void gvChargeableHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //code to retrive in the seecond gridview
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //2 getting from main table based on chargeable head name
                GridView gv = (GridView)e.Row.FindControl("gvChallanDateNall");
                Label DEP_ID = e.Row.FindControl("chName") as Label;
                HiddenField hj = e.Row.FindControl("hfid") as HiddenField;
                SqlCommand cmd = new SqlCommand("Select * from DeliveryItemsChallan WHERE (IndentingDivisionName ='" + DivisionName.Text + "') and ChargeableHeadName='" + DEP_ID.Text.ToString() + "' and (challandate between @startdate and @enddate)", con);
                cmd.Parameters.AddWithValue("@startdate", st.Text);
                cmd.Parameters.AddWithValue("@enddate", ed.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void gvChallanDateNall_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvs = (GridView)e.Row.FindControl("gvItems");
                Label et = (Label)e.Row.FindControl("chaalID");
                string txttemp = et.Text;
                DataSet ds = new DataSet();
                //3 getting details table
                string cmdstr = "SELECT dbo.DeliveryItemsChallan.TotalAmount,dbo.DeliveryItemsDetails.DeliveryItemDetailsID , dbo.DeliveryItemsDetails.ItemName, dbo.DeliveryItemsDetails.IssueHeadName, dbo.DeliveryItemsDetails.Quantity, dbo.DeliveryItemsDetails.Unit, dbo.DeliveryItemsDetails.Rate, (dbo.DeliveryItemsDetails.Rate* dbo.DeliveryItemsDetails.quantity) as amount   FROM  dbo.DeliveryItemsChallan INNER JOIN dbo.DeliveryItemsDetails ON dbo.DeliveryItemsChallan.DeliveryItemsChallanID = dbo.DeliveryItemsDetails.DeliveryItemsChallanID WHERE dbo.DeliveryItemsChallan.DeliveryItemsChallanID = @DeliveryItemsChallanID and  ChallanDate between '" + st.Text + "' and '" + ed.Text + "' ";
                SqlCommand cmd = new SqlCommand(cmdstr, con);
                cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", txttemp);
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Close();
                gvs.DataSource = ds;
                gvs.DataBind();

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                double price = double.Parse(lblPrice.Text);
                totalPrice += price;

                storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeliveryItemDetailsID").ToString());
                ch_headvalue = DataBinder.Eval(e.Row.DataItem, "ItemName").ToString();

              
                double tmpTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount").ToString());
                qtyTotal += tmpTotal;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
                lblTotalPrice.Text = "<b>Total:</b> " + totalPrice.ToString();
                totalPrice = 0;
            }
        }

        protected void gvItems_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool newRow = false;
            //for the ch heads row in a divison but not the last row
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ItemName") != null))
            {
                if (ch_headvalue != DataBinder.Eval(e.Row.DataItem, "ItemName").ToString())
                    newRow = true;

            }
            //for the same ch head row as abobe but in the last row in a division
            if ((ch_headvalue != "") && (DataBinder.Eval(e.Row.DataItem, "ItemName") == null))
            {
                newRow = true;
                rowIndex = 0;
                //since its last row reseeting ch head value because if not then same value is passing over to the next division
                ch_headvalue = "";
            }
            if (newRow)
            {
                GridView GridView1 = (GridView)sender;
             //   GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
             //   NewTotalRow.Font.Bold = true;

              //  NewTotalRow.ForeColor = System.Drawing.Color.Black;
              //  TableCell HeaderCell = new TableCell();
              //  HeaderCell.Text = "Sub Total";
              //  HeaderCell.HorizontalAlign = HorizontalAlign.Left;
               // HeaderCell.ColumnSpan = 4;
               //   NewTotalRow.Cells.Add(HeaderCell);
              //    HeaderCell = new TableCell();
             //   HeaderCell.HorizontalAlign = HorizontalAlign.Center;
             //   HeaderCell.Text = qtyTotal.ToString();
                 // NewTotalRow.Cells.Add(HeaderCell);
                 // GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
                rowIndex++;
                qtyTotal = 0;
            }
        }
    }
}