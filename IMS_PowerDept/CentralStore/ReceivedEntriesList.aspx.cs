using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;

namespace IMS_PowerDept.CentralStore
{
    public partial class ReceivedItems_List : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
       
        protected void Page_Load(object sender, EventArgs e)
        {

            retriveData();
        }

        //retrive data in rptr
        private void retriveData()
        {
            try
            {
                SqlDataAdapter dadapter;
                DataSet dset;
                dadapter = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] ORDER BY [ReceivedItemsOTEOID] DESC", con);
                dset = new DataSet();
                dadapter.Fill(dset);
                _rprt.DataSource = dset.Tables[0];
                _rprt.DataBind();
            }
            catch
            {
                throw;
            }
        }

       protected void btnAdvancedSearchFilters_Click(object sender, EventArgs e)
        {
            if (etsearch.Value != "")
            {
                etsearch.Value = "";
            }
            panelAdvancedSearchFilters.Visible = true;
        }

        protected void ddlIssueHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIssueHead.SelectedItem.Text == "All")
                {
                    retriveData();
                }
                else
                {
                    SqlDataAdapter s;
                    DataSet d;
                    s = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ChargeableHeadName='" + ddlIssueHead.SelectedItem + "' ", con);
                    d = new DataSet();
                    s.Fill(d);
                    _rprt.DataSource = d.Tables[0];
                    _rprt.DataBind();
                }

            }
            catch (Exception xx)
            {
                xx.Message.ToString();
            }
        }

        protected void ddlChargeableHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlChargeableHead.SelectedItem.Text == "All")
                {
                    retriveData();
                }
                else
                {
                    SqlDataAdapter g;
                    DataSet fd;
                    g = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ChargeableHeadName='" + ddlChargeableHead.SelectedItem + "' ", con);
                    fd = new DataSet();
                    g.Fill(fd);
                    _rprt.DataSource = fd.Tables[0];
                    _rprt.DataBind();
                }

            }
            catch (Exception xx)
            {
                xx.Message.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter aa;
                DataSet bb;
                string stDate, edDate;

                stDate = DateTime.ParseExact(tbStartDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                edDate = DateTime.ParseExact(tbEndDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                aa = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemOTEODate between '" + stDate + "' and '" + edDate + "' or SupplyOrderDate between '" + stDate + "' and '" + edDate + "' ", con);
                //'%" + _txtsearch.Value.ToString() + "%' and IndentRefernce '%" + _txtsearch.Value.ToString() + "%'
                bb = new DataSet();
                aa.Fill(bb);
                _rprt.DataSource = bb.Tables[0];
                _rprt.DataBind();


            }
            catch (Exception xx)
            {
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelAdvancedSearchFilters.Visible = false;
        }

        protected void btnSearchImage_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter ss;
                DataSet xx;
                ss = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemsOTEOID='" + etsearch.Value + "' or SupplyOrderReference='" + etsearch.Value + "'  ", con);
                //'%" + _txtsearch.Value.ToString() + "%' and IndentRefernce '%" + _txtsearch.Value.ToString() + "%'
                xx = new DataSet();
                ss.Fill(xx);
                _rprt.DataSource = xx.Tables[0];
                _rprt.DataBind();


            }
            catch (Exception xx)
            {
                xx.Message.ToString();
            }
        }
    }
}