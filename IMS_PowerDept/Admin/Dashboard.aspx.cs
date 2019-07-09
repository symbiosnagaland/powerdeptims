using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace IMS_PowerDept.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            countuser();
           
        }



        //count user
        private void countuser()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
            string str;
            SqlCommand com;
            try
            {
                con.Open();
                str = "select count (username) as uname from Users";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    Label1.Text = reader["uname"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }
        }
    }
}