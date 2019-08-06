using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System;
using IMS_PowerDept.AppCode;
using System.Web;

namespace IMS_PowerDept
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Int32 i = 999999999;
          //  Int16 i2= 9999;

        }
        private void valiUsername()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
                con.Open();
                string cmdstr = " Select count (*) from Users where username='" + inputUsername.Text + "'";
                SqlCommand userExist = new SqlCommand(cmdstr, con);
                int temp = Convert.ToInt32(userExist.ExecuteScalar().ToString());
                if (temp != 1)
                {
                    Label1.Text = "Invalid UserName/Password";
                    return;
                }
                else
                {
                    if (temp == 1)
                    {
                        //check username and password in the database
                        using (SqlCommand cmd = new SqlCommand("Select Role,username,password from Users where username=@username", con))
                        {
                            cmd.Parameters.AddWithValue("@username", inputUsername.Text);

                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            string roleEt = dt.Rows[0]["Role"].ToString();
                            string userid = dt.Rows[0]["username"].ToString();
                            string password = dt.Rows[0]["password"].ToString();

                            bool flag = Helper.VerifyHash(inputPassword.Text, "SHA512", password);


                            SqlDataReader rec = userExist.ExecuteReader();
                            if (rec.Read())
                            {
                                Session["User"] = inputUsername.Text;

                                // create a new GUID and save into the session  
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                // now create a new cookie with this guid value  
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            }
                            rec.Close();
                            con.Close();

                            if (userid == inputUsername.Text && flag == true && roleEt == "Admin")
                            {
                                Session["username"] = inputUsername.Text;
                                // create a new GUID and save into the session
                                //string guid = Guid.NewGuid().ToString();
                                //Session["AuthToken"] = guid;
                                // now create a new cookie with this guid value
                                //Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                Response.Redirect("~/Admin/Dashboard.aspx");
                            }
                            if (userid == inputUsername.Text && flag == true && roleEt == "Store")
                            {
                                Session["username"] = inputUsername.Text;

                                Response.Redirect("~/CentralStore/Dashboard.aspx");
                            }
                            else
                            {
                                Label1.Text = "Invalid UserName/Password";
                            }
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                //do nothing 
            }

            catch (Exception ex)
            {

                Session["ERRORMSG"] = ex.ToString();
              Response.Redirect("Error.aspx");



            }
          
                }
      
                
       
        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            valiUsername();
        }
    }
}