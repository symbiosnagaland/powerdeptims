using IMS_PowerDept.AppCode;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace IMS_PowerDept.Admin
{
    public partial class User_Setting : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _rpUsersEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {

                TextBox aa = e.Item.FindControl("_tbpasword") as TextBox;
                aa.Visible = true;

                TextBox pp = e.Item.FindControl("_tbpass1") as TextBox;
                pp.Visible = false;

                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = true;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = false;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;

                cancelBt.Visible = true;
            }
            if (e.CommandName == "cancel")
            {
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;
                TextBox aa = e.Item.FindControl("_tbpasword") as TextBox;
                aa.Visible = false;

                TextBox pp = e.Item.FindControl("_tbpass1") as TextBox;
                pp.Visible = true;
            }
            if (e.CommandName == "delete")
            {
                Label lblHead = e.Item.FindControl("_lbluserid") as Label;
               
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Users where userid ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                //panelSuccess.Visible = true;
                //lblSuccess.Text = "Division Name Deleted successfully.";

                Response.Redirect("User_Setting.aspx");


            }
            if (e.CommandName == "update")
            {
                Label lblHead = e.Item.FindControl("_lbluserid") as Label;
                Label cHEad = e.Item.FindControl("cHEad") as Label;
                TextBox aa = e.Item.FindControl("_tbpasword") as TextBox;
                //aa.Visible = false;

                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;

                using (SqlCommand cmd = new SqlCommand("update Users set password=@Password where username=@UserName", con))
                {
                    cmd.Parameters.AddWithValue("@UserName", cHEad.Text);
                    //Here i have implemented the code for doing encryption of password
                    string ePass = Helper.ComputeHash(aa.Text, "SHA512", null);
                    cmd.Parameters.AddWithValue("@Password", ePass);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //lblmsg.Text = "Your password has been Updated Sucessfully";

                    Response.Redirect("User_Setting.aspx");
                }
            }
        }
    }
}