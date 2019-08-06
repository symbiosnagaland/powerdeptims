using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.UserControls
{
    public partial class ChargeableHeadsControl : System.Web.UI.UserControl
    {
        DataTable data2;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string cmdText2 = "SELECT DISTINCT IssueHeadName, Issueheadid FROM IssueHeads;";
                    data2 = new DataTable();
                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmdText2, con);
                    adapter2.Fill(data2);
                    _ddIssueHead.DataSource = data2;
                    _ddIssueHead.DataTextField = "IssueHeadName";
                    _ddIssueHead.DataValueField = "IssueHeadID";
                    _ddIssueHead.DataBind();
                    // _ddIssueHead.Items.Insert(0, new ListItem("--Select Issue Head Name--", ""));
                }
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
              Response.Redirect("Error.aspx");



            }
        }

        protected void _gridChEdit_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void _gridChEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
            if (e.CommandName == "edit")
            {
                Label lbl = e.Item.FindControl("cHEad") as Label;
                lbl.Visible = false;
                TextBox aa = e.Item.FindControl("_tbChHead") as TextBox;
                aa.Visible = true;
                Label lbl2 = e.Item.FindControl("_lblHead") as Label;
                lbl2.Visible = false;
                TextBox ddlist = e.Item.FindControl("_grdIssueHead") as TextBox;
                ddlist.Visible = true;


                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = true;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = false;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;

                cancelBt.Visible = true;

            }
            //cancel editing
            if (e.CommandName == "cancel")
            {
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;


                Label lbl = e.Item.FindControl("cHEad") as Label;
                lbl.Visible = true;
                TextBox aa = e.Item.FindControl("_tbChHead") as TextBox;
                aa.Visible = false;
                Label lbl2 = e.Item.FindControl("_lblHead") as Label;
                lbl2.Visible = true;
                TextBox ddlist = e.Item.FindControl("_grdIssueHead") as TextBox;
                ddlist.Visible = false;


            }

            if (e.CommandName == "update")
            {
                Label lblHead = e.Item.FindControl("_lblIssueHead") as Label;
                TextBox aa = e.Item.FindControl("_tbChHead") as TextBox;
                //aa.Visible = false;
                Label lbl = e.Item.FindControl("_lblHead") as Label;
                lbl.Visible = false;
                TextBox aa2 = e.Item.FindControl("_grdIssueHead") as TextBox;


                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;

                SqlDataAdapter adp = new SqlDataAdapter("Update ChargeableHeads set ChargeableHeadName= @ChargeableHeadName , IssueHeadName=@IssueHeadName where ChargeableHeadID ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                adp.SelectCommand.Parameters.AddWithValue("@ChargeableHeadName", aa.Text);
                adp.SelectCommand.Parameters.AddWithValue("@IssueHeadName", aa2.Text);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                panelSuccess.Visible = true;
                lblSuccess.Text = "Chargeable Head Successfully updated.";

                Response.Redirect("ChargeableHeads.aspx");

                //Response.Write("<script>alert('Details Successfully Updated.');</script>");

            }


            if (e.CommandName == "delete")
            {
                Label lblHead = e.Item.FindControl("_lblIssueHead") as Label;
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from ChargeableHeads where ChargeableHeadID ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                panelSuccess.Visible = true;
                lblSuccess.Text = "Chargeable Head Deleted successfully.";

                Response.Redirect("ChargeableHeads.aspx");

                //Response.Write("<script>alert('Details Successfully Deleted.');</script>");

            }
        }
        /// <summary>
        /// saving chargeable head along with corresponding issueheadid and name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void save_ServerClick(object sender, EventArgs e)
        {
            if (_tbchhead.Text.Trim() == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Enter Chargeable HeadName";
                    return;
                }
                if (_ddIssueHead.Text.Trim() == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Enter Issue HeadName";
                    return;
                }
            try
            {
                
               
                    string conn = "";
                    conn = ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ToString();
                    SqlConnection objsqlconn = new SqlConnection(conn);
                    objsqlconn.Open();
                    SqlCommand objcmd = new SqlCommand("Insert into ChargeableHeads(ChargeableHeadName,Issueheadid, IssueHeadName) Values('" + _tbchhead.Text + "','" + _ddIssueHead.SelectedValue + "', '" + _ddIssueHead.SelectedItem + "')", objsqlconn);
                    objcmd.ExecuteNonQuery();
                    //panelSuccess.Visible = true;
                    //lblSuccess.Text = "Chargeable Head Successfully Uploaded.";
                    Response.Redirect("ChargeableHeads.aspx", false);
                
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
              Response.Redirect("Error.aspx");



            }
        }
    }
}