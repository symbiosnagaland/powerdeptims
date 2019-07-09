using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;

namespace IMS_PowerDept.Transformers
{
    public partial class User_Setting : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["username"] = _tbUsername.Text;

            if (IsPostBack)
            {
                //if (Session)

            }
        }

        protected void _btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("update Users set password=@Password where username=@UserName", con))
                {
                    cmd.Parameters.AddWithValue("@UserName", _tbUsername.Text);
                    //Here i have implemented the code for doing encryption of password
                    string ePass = Helper.ComputeHash(_tbPassword.Text, "SHA512", null);
                    cmd.Parameters.AddWithValue("@Password", ePass);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    panelSuccess.Visible = true;
                    lblSuccess.Text = "Your password has been Updated Sucessfully";
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Setting.aspx");
        }
    }
}