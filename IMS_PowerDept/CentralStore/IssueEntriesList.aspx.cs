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
    public partial class IssueEntry_List : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        SqlDataAdapter dadapter;
        DataSet dset;
        SqlDataAdapter dadaptera;
        DataSet dseta;

        string stDate, edDate;

        SqlDataAdapter dadapterava;
        DataSet dsetava;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                retriveData();
            }
        }

        //retrive data in rptr
        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] ORDER BY [DeliveryItemsChallanID] DESC", con);
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

            panelAdvancedSearchFilters.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            panelAdvancedSearchFilters.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter aa;
                DataSet bb;
                stDate = DateTime.ParseExact(tbStartDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                edDate = DateTime.ParseExact(tbEndDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                aa = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentDate between '" + stDate + "' and '" + edDate + "' or ChallanDate between '" + stDate + "' and '" + edDate + "' ", con);
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

        protected void btnSearchImage_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter ss;
                DataSet xx;
                ss = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where DeliveryItemsChallanID='" + _txtsearch.Value + "' or IndentReference='" + _txtsearch.Value + "'  ", con);
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

        protected void _ddldivname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_ddldivname.SelectedItem.Text == "All")
                {
                    retriveData();
                }
                else
                {
                    dadapter = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentingDivisionName='" + _ddldivname.SelectedItem + "' ", con);
                    dset = new DataSet();
                    dadapter.Fill(dset);
                    _rprt.DataSource = dset.Tables[0];
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
                    dadaptera = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where ChargeableHeadName='" + ddlChargeableHead.SelectedItem + "' ", con);
                    dseta = new DataSet();
                    dadaptera.Fill(dseta);
                    _rprt.DataSource = dseta.Tables[0];
                    _rprt.DataBind();
                }

            }
            catch (Exception xx)
            {
                xx.Message.ToString();
            }
        }

        protected void _istemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_istemp.SelectedItem.Text == "All")
                {
                    retriveData();
                }
                else
                {
                    if (_istemp.SelectedItem.Text == "Regular Issued Items")
                    {
                        dadapterava = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IsDeliveredTemporary='No' ", con);
                        dsetava = new DataSet();
                        dadapterava.Fill(dsetava);
                        _rprt.DataSource = dsetava.Tables[0];
                        _rprt.DataBind();
                    }
                    if (_istemp.SelectedItem.Text == "Temporary Issued Items")
                    {
                        dadapterava = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IsDeliveredTemporary='Yes' ", con);
                        dsetava = new DataSet();
                        dadapterava.Fill(dsetava);
                        _rprt.DataSource = dsetava.Tables[0];
                        _rprt.DataBind();
                    }
                }
            }
            catch (Exception xx)
            {
                xx.Message.ToString();
            }
        }
    }
}