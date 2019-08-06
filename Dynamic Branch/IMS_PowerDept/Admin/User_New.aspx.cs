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
    public partial class User_New : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
       
        protected void save_ServerClick(object sender, EventArgs e)
        {
            if (_tbUsername.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblSuccess.Text = "Enter User Name";
            }
            if (_tbPassword.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblSuccess.Text = "Enter Password";
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("Insert into Users(Role,username,password) values (@Role,@username,@password)", con))
                {

                    cmd.Parameters.AddWithValue("@Role", _ddUsertype.Text);
                    cmd.Parameters.AddWithValue("@username", _tbUsername.Text);
                    string ePass = Helper.ComputeHash(_tbPassword.Text, "SHA512", null);
                    cmd.Parameters.AddWithValue("@password", ePass);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    panelSuccess.Visible = true;
                    lblSuccess.Text = "User Created Successfully";

                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    panelSuccess.Visible = false;
                    lblSuccess.Text = "Username already exists. Please choose another username.";
                }
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
              Response.Redirect("Error.aspx");
            }

          
        }


    }
}