using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.UserControls
{
    public partial class DivisionsControl : System.Web.UI.UserControl
    {
        string str;
        SqlCommand com;
        int count;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                retrivemaxID();

            }
        }
        //retrive max id fromtable
        private void retrivemaxID()
        {
            try
            {
                str = "select max(division) + 1 from Divisions";
                com = new SqlCommand(str, con);
                con.Open();
                if (com.ExecuteScalar() != DBNull.Value)
                    count = Convert.ToInt16(com.ExecuteScalar());
                else
                    count = 1;
                
                _tbchID.Text = count.ToString();

                con.Close();
            }
            catch
            {
                throw;
            }
        }
        protected void addBtn_Click(object sender, EventArgs e)
        {
            if (_tbHeadName.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Division Name";
                return;
            }
            try
            {
                con.Open();
                SqlCommand cmdd = new SqlCommand("select * from Divisions where division = @division", con);
                SqlParameter param = new SqlParameter();
                //SqlParameter param1 = new SqlParameter();
                param.ParameterName = "@division";
                param.Value = _tbchID.Text;
                cmdd.Parameters.Add(param);
                //cmdd.Parameters.Add(param1);

                SqlDataReader reader = cmdd.ExecuteReader();

                if (reader.HasRows)
                {
                    panelError.Visible = true;
                    lblError.Text = "This Division ID already exists.Please choose another ID.";
                    return;
                }

                else
                {
                    string conn = "";
                    conn = ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ToString();
                    SqlConnection objsqlconn = new SqlConnection(conn);
                    objsqlconn.Open();
                    SqlCommand objcmd = new SqlCommand("Insert into Divisions (division,divisionName) Values('" + _tbchID.Text + "','" + _tbHeadName.Text + "')", objsqlconn);
                    objcmd.ExecuteNonQuery();
                    panelSuccess.Visible = true;
                    lblSuccess.Text = "Division Name Successfully Uploaded.";
                    Response.Redirect("Divisions.aspx", false);
                    // _tbHeadName.Text = "";
                    //retrivemaxID();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void _gridChEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                Label lbl = e.Item.FindControl("cHEad") as Label;
                lbl.Visible = false;
                TextBox aa = e.Item.FindControl("_tbHead") as TextBox;
                aa.Visible = true;
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
                TextBox aa = e.Item.FindControl("_tbHead") as TextBox;
                aa.Visible = false;
                Label lbl = e.Item.FindControl("cHEad") as Label;
                lbl.Visible = true;
            }
            if (e.CommandName == "delete")
            {
                Label lblHead = e.Item.FindControl("_lblIssueHead") as Label;

                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Divisions where division ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                // panelSuccess.Visible = true;
                //lblSuccess.Text = "Division Name Deleted successfully.";

                Response.Redirect("Divisions.aspx");


            }
            if (e.CommandName == "update")
            {
                Label lblHead = e.Item.FindControl("_lblIssueHead") as Label;
                TextBox aa = e.Item.FindControl("_tbHead") as TextBox;
                //aa.Visible = false;
                Label lbl = e.Item.FindControl("cHEad") as Label;
                lbl.Visible = true;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;

                SqlDataAdapter adp = new SqlDataAdapter("Update Divisions set divisionName= @divisionName where division ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                adp.SelectCommand.Parameters.AddWithValue("@divisionName", aa.Text);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                panelSuccess.Visible = true;
                lblSuccess.Text = "Division Name Successfully updated.";

                Response.Redirect("Divisions.aspx");

            }
        }
    }
}