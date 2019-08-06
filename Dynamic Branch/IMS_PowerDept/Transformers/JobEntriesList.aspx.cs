using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;


namespace IMS_PowerDept.Transformers
{
    public partial class JobEntriesList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        SqlDataAdapter dadapter;
        DataSet dset;
  
        protected void Page_Load(object sender, EventArgs e)
        {
            retriveData();
        }

        //retrive data in rptr
        private void retriveData()
        {
            try
            {
                dadapter = new SqlDataAdapter("SELECT * FROM [transformer_job] ORDER BY [challanno] DESC", con);
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



        protected void btnSearchImage_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter ss;
                DataSet xx;
                ss = new SqlDataAdapter("SELECT * FROM [transformer_job] where challanno='" + _txtsearch.Value + "' ", con);

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