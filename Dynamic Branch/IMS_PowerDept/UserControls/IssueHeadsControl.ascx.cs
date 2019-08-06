using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.UserControls
{
    public partial class IssueHeadsControl : System.Web.UI.UserControl
    {
        
        protected void _gridChEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
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
                SqlCommand cmd = new SqlCommand("delete from IssueHeads where IssueHeadID ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                panelSuccess.Visible = true;
                lblSuccess.Text = "Issue Head Deleted successfully.";
                panelError.Visible = false;

                Response.Redirect("IssueHead.aspx");

                //Response.Write("<script>alert('Details Successfully Deleted.');</script>");

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

                SqlDataAdapter adp = new SqlDataAdapter("Update IssueHeads set IssueHeadName= @IssueHeadName where IssueHeadID ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                adp.SelectCommand.Parameters.AddWithValue("@IssueHeadName", aa.Text);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                panelSuccess.Visible = true;
                lblSuccess.Text = "Issue Head Successfully updated.";

                Response.Redirect("IssueHead.aspx");

                //Response.Write("<script>alert('Details Successfully Updated.');</script>");

            }
        }

        protected void _tbchID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
                //    con.Open();
                //    SqlCommand cmdd = new SqlCommand("select * from IssueHeads where IssueHeadID = @IssueHeadID", con);

                //    SqlParameter param = new SqlParameter();
                //    //SqlParameter param1 = new SqlParameter();
                //    param.ParameterName = "@IssueHeadID";
                //  //  param.Value = _tbchID.Text;
                //    cmdd.Parameters.Add(param);
                //cmdd.Parameters.Add(param1);

                // SqlDataReader reader = cmdd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    panelError.Visible = true;
                //    lblSuccess.Text = "This Issue Head ID already exists.Please choose another ID.";
                //}

                // else
                // {
                string conn = "";
                conn = ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ToString();
                SqlConnection objsqlconn = new SqlConnection(conn);
                objsqlconn.Open();
                // SqlCommand objcmd = new SqlCommand("Insert into IssueHeads(IssueHeadID,IssueHeadName) Values('" + _tbchID.Text + "','" + _tbHeadName.Text + "')", objsqlconn);

                SqlCommand objcmd = new SqlCommand("Insert into IssueHeads(IssueHeadName) Values('" + _tbHeadName.Text + "')", objsqlconn);
                objcmd.ExecuteNonQuery();
                panelSuccess.Visible = true;
                lblSuccess.Text = "Issue Head Successfully Uploaded.";
                Response.Redirect("IssueHead.aspx", false);
                // }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}