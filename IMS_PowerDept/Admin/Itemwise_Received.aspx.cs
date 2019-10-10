using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IMS_PowerDept.Admin
{
    
    public partial class Itemwise_Received : System.Web.UI.Page
    {
        SqlDataAdapter SqlDataAdapter;
        DataSet myDataSet;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                _reteriveData();
             
            }
            
        }

        protected void _reteriveData()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

                SqlDataAdapter = new SqlDataAdapter("select ReceivedItemsOTEO.ReceivedItemsOTEOID,ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate," +
                         " Supplier, itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit from ReceivedItemsOTEO,ReceivedItemsDetails " +
                         " where ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID  order by itemname,ReceivedItemsOTEOID", con);

                myDataSet = new DataSet();
                SqlDataAdapter.Fill(myDataSet);
                GridView1.DataSource = myDataSet.Tables[0];
                GridView1.DataBind();
                


            }
            catch (Exception xx)
            {
                throw;
            }

        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            if (tbStartDateSearch.Text == "" || tbEndDateSearch.Text == "")
            {
                _reteriveData();
            }
            else
            {
                _reteriveDataSearch();
            }
        }

       

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.AllowPaging = false;
            //GridView1.DataBind();
            if (tbStartDateSearch.Text == "" || tbEndDateSearch.Text =="")
            {
                _reteriveData();
            }
            else
            {
                _reteriveDataSearch();
            }
           
            // DisableControls(_gvActiveBroadBandUsers);// comment out if there are any asp.net controls
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Received_Items" + DateTime.Now.ToString("dd/MM/yyy_hh:mm:ss") + ".xls");
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

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            
            _reteriveDataSearch();
        }

        protected void _reteriveDataSearch()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

                if (tbStartDateSearch.Text != "" && tbEndDateSearch.Text != "")
                {
                    //string stDate = DateTime.ParseExact(tbStartDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                    //string endDate = DateTime.ParseExact(tbEndDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");


                    string stDate; string endDate;

                    if (tbStartDateSearch.Text.Contains("/"))
                        stDate = DateTime.ParseExact(tbStartDateSearch.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    else
                        stDate = DateTime.ParseExact(tbStartDateSearch.Text, @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    if (tbEndDateSearch.Text.Contains("/"))
                        endDate = DateTime.ParseExact(tbEndDateSearch.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    else
                        endDate = DateTime.ParseExact(tbEndDateSearch.Text, @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");


                    SqlDataAdapter = new SqlDataAdapter("select ReceivedItemsOTEO.ReceivedItemsOTEOID,ReceivedItemOTEODate,SupplyOrderReference,SupplyOrderDate," +
                        " Supplier, itemname,Quantity,Rate,IssueHeadName,ChargeableHeadName,amount,unit from ReceivedItemsOTEO,ReceivedItemsDetails " +
                        " where ReceivedItemsOTEO.ReceivedItemsOTEOID=ReceivedItemsDetails.ReceivedItemsOTEOID and " +
                        " ReceivedItemOTEODate between '" + stDate + "' and '" + endDate + "' order by itemname,ReceivedItemsOTEOID", con);
                }
                else
                {
                    return;
                }
                myDataSet = new DataSet();
                SqlDataAdapter.Fill(myDataSet);
                GridView1.DataSource = myDataSet.Tables[0];
                GridView1.DataBind();
               


            }
            catch (Exception xx)
            {
                throw;
            }
        }




       
    }
}