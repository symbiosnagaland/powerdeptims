using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;
namespace IMS_PowerDept.Admin
{
    public partial class ReceivedEntriesList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                retriveData();
            }

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

                if ((tbStartDateSearch.Text == "") || (tbEndDateSearch.Text == ""))
                {
                    aa = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemsOTEOID='" + etsearch.Value + "' or SupplyOrderReference='" + etsearch.Value + "'  ", con);
                }
                else
                {
                    string stDate = DateTime.ParseExact(tbStartDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string endDate = DateTime.ParseExact(tbEndDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    aa = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemOTEODate between '" + stDate + "' and '" + endDate + "' or SupplyOrderDate between '" + stDate + "' and '" + endDate + "' ", con);

                }


                //aa = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemsOTEOID='" + etsearch.Value + "' or SupplyOrderReference='" + etsearch.Value + "'  ", con);


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
        protected void _rprt_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //
        }
        //private void DeleteData(decimal index)
        //{
        //    try
        //    {

        //        SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
        //        SqlCommand cmd2 = conn.CreateCommand();
        //        conn.Open();
        //        cmd2.CommandText = "DELETE FROM [ReceivedItemsOTEO]  WHERE [ReceivedItemsOTEOID]  = @ReceivedItemsOTEOID";
        //        cmd2.Parameters.AddWithValue("@ReceivedItemsOTEOID", index);

        //        cmd2.ExecuteNonQuery();

        //        retriveData();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        protected void _rprt_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());

                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "SELECT COUNT (*) FROM ReceivedItemsDetails INNER JOIN ReceivedItemsOTEO ON ReceivedItemsDetails.ReceivedItemsOTEOID = ReceivedItemsOTEO.ReceivedItemsOTEOID WHERE (ReceivedItemsDetails.ReceivedItemsOTEOID = @ReceivedItemsOTEOID)";
                cmd1.Parameters.AddWithValue("@ReceivedItemsOTEOID", index);

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "DELETE FROM [ReceivedItemsOTEO]  WHERE [ReceivedItemsOTEOID]  = @ReceivedItemsOTEOID";
                cmd2.Parameters.AddWithValue("@ReceivedItemsOTEOID", index);



                conn.Open();

                int count = Convert.ToInt32(cmd1.ExecuteScalar());

                if (count < 1)
                {
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    if (ddlIssueHead.Text != "%")
                    {
                        SqlDataAdapter s;
                        DataSet d;
                        s = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ChargeableHeadName='" + ddlIssueHead.SelectedItem + "' ", con);
                        d = new DataSet();
                        s.Fill(d);
                        _rprt.DataSource = d.Tables[0];
                        _rprt.DataBind();
                    }
                    else if (ddlChargeableHead.Text != "%")
                    {
                        SqlDataAdapter g;
                        DataSet fd;
                        g = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ChargeableHeadName='" + ddlChargeableHead.SelectedItem + "' ", con);
                        fd = new DataSet();
                        g.Fill(fd);
                        _rprt.DataSource = fd.Tables[0];
                        _rprt.DataBind();
                    }
                    else if (tbStartDateSearch.Text != "" && tbEndDateSearch.Text != "")
                    {
                        SqlDataAdapter aa;
                        DataSet bb;
                        aa = new SqlDataAdapter("SELECT * FROM [ReceivedItemsOTEO] where ReceivedItemOTEODate between '" + tbStartDateSearch.Text + "' and '" + tbEndDateSearch.Text + "' or SupplyOrderDate between '" + tbStartDateSearch.Text + "' and '" + tbEndDateSearch.Text + "' ", con);
                        //'%" + _txtsearch.Value.ToString() + "%' and IndentRefernce '%" + _txtsearch.Value.ToString() + "%'
                        bb = new DataSet();
                        aa.Fill(bb);
                        _rprt.DataSource = bb.Tables[0];
                        _rprt.DataBind();
                    }
                    else
                    {
                        retriveData();
                    }

                    //DeleteData(index);
                }
                else
                {
                    Response.Redirect("ReceivedItemsEdit.aspx?oteoid=" + index);
                }

            }

        }
    }
}