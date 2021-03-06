﻿
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Net.Mail;
using System.Text;
using System.IO;




namespace IMS_PowerDept.UserControls
{
    public partial class Issue_report_Excel : System.Web.UI.UserControl
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

        SqlDataAdapter SqlDataAdapter;
        DataSet myDataSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _reteriveData();

            }
        }

        protected void _reteriveData()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

                SqlDataAdapter = new SqlDataAdapter("SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate," +
                    "DeliveryItemsChallan.IndentReference,DeliveryItemsChallan.IndentDate, DeliveryItemsChallan.IndentingDivisionName, " +
                    "DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,DeliveryItemsDetails.IssueHeadName, " +
                    "DeliveryItemsChallan.ChargeableHeadName,DeliveryItemsDetails.UNIT,DeliveryItemsDetails.Rate*DeliveryItemsDetails.Quantity as TotalAmount " +
                    " FROM DeliveryItemsChallan INNER JOIN DeliveryItemsDetails ON " +
                    "DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID order by itemname, challandate asc", con);

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
            // _reteriveData(); //bindgridview will get the data source and bind it again


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
            if (tbStartDateSearch.Text == "" || tbEndDateSearch.Text == "")
            {
                _reteriveData();
            }
            else
            {
                _reteriveDataSearch();
            }

            // DisableControls(_gvActiveBroadBandUsers);// comment out if there are any asp.net controls
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Issued_Items" + DateTime.Now.ToString("dd-MM-yyy_hh:mm:ss") + ".xls");
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

        

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _reteriveDataSearch();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

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






                    SqlDataAdapter = new SqlDataAdapter("SELECT DeliveryItemsChallan.DeliveryItemsChallanID, DeliveryItemsChallan.ChallanDate," +
                    "DeliveryItemsChallan.IndentReference,DeliveryItemsChallan.IndentDate, DeliveryItemsChallan.IndentingDivisionName, " +
                    "DeliveryItemsDetails.ItemName, DeliveryItemsDetails.Quantity,DeliveryItemsDetails.Rate,DeliveryItemsDetails.IssueHeadName, " +
                    "DeliveryItemsChallan.ChargeableHeadName,DeliveryItemsDetails.UNIT,DeliveryItemsDetails.Rate*DeliveryItemsDetails.Quantity as TotalAmount " +
                    " FROM DeliveryItemsChallan INNER JOIN DeliveryItemsDetails ON " +
                    "DeliveryItemsChallan.DeliveryItemsChallanID=DeliveryItemsDetails.DeliveryItemsChallanID  and " +
                        " ChallanDate between '" + stDate + "' and '" + endDate + "' order by itemname, challandate asc ", con);
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