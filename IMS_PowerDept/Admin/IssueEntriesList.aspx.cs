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
    public partial class IssueEntriesList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        SqlDataAdapter dadapter;
        DataSet dset;
        SqlDataAdapter dadaptera;
        DataSet dseta;

        SqlDataAdapter dadapterava;
        DataSet dsetava;
        //string sql = "SELECT * FROM [DeliveryItemsChallan] ORDER BY [DeliveryItemsChallanID] DESC";

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

                //for converting date to MM/dd/yyyy again 

                string stDate = DateTime.ParseExact(tbStartDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                string endDate = DateTime.ParseExact(tbEndDateSearch.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //this was the old select code
                //aa = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentDate between '" + stDate + "' and '" + endDate + "' or ChallanDate between '" + stDate + "' and '" + endDate + "' ", con);

                //NEw Select Code
                aa = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentDate between '" + stDate + "' and '" + endDate + "' and  ChallanDate between '" + stDate + "' and '" + endDate + "' ", con);
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
        protected void _rprt_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //
        }

        
      protected void _rprt_ItemCommand(object sender, ListViewCommandEventArgs e)
        {


            if (e.CommandName == "Delete")
            {
                decimal index = Convert.ToDecimal(e.CommandArgument);             
                    SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
                    
                    SqlCommand cmd1 = conn.CreateCommand();
                    cmd1.CommandText = "Select count(*) as count from DeliveryItemsDetails where DeliveryItemsChallanID = @DeliveryItemsChallanID";
                    cmd1.Parameters.AddWithValue("@DeliveryItemsChallanID", index);

                    SqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandText = "DELETE FROM [DeliveryItemsChallan]  WHERE [DeliveryItemsChallanID]  = @DeliveryItemsChallanID";
                    cmd2.Parameters.AddWithValue("@DeliveryItemsChallanID", index);


                    conn.Open();
              
                 int count  = Convert.ToInt32(cmd1.ExecuteScalar());
                

                      if (count < 1)
                      {
                          cmd2.ExecuteNonQuery();
                          conn.Close();
                        
                         // DeleteData(index);
                          if (_ddldivname.Text != "%")
                          {
                              dadapter = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentingDivisionName='" + _ddldivname.SelectedItem + "' ", con);
                              dset = new DataSet();
                              dadapter.Fill(dset);
                              _rprt.DataSource = dset.Tables[0];
                              _rprt.DataBind();
                          }
                          else if (ddlChargeableHead.Text != "%")
                          {
                              dadaptera = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where ChargeableHeadName='" + ddlChargeableHead.SelectedItem + "' ", con);
                              dseta = new DataSet();
                              dadaptera.Fill(dseta);
                              _rprt.DataSource = dseta.Tables[0];
                              _rprt.DataBind();
                          }
                          else if (tbStartDateSearch.Text != "" && tbEndDateSearch.Text != "")
                          {
                              SqlDataAdapter aa;
                              DataSet bb;
                              aa = new SqlDataAdapter("SELECT * FROM [DeliveryItemsChallan] where IndentDate between '" + tbStartDateSearch.Text + "' and '" + tbEndDateSearch.Text + "' or ChallanDate between '" + tbStartDateSearch.Text + "' and '" + tbEndDateSearch.Text + "' ", con);
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
                      }
                      else
                      {
                          Response.Redirect("IssuedEntryEdit.aspx?challanid=" + index);
                      }  
                
                                    
            }

        }
    }
}