using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;

namespace IMS_PowerDept.UserControls
{
    public partial class ItemsControl : System.Web.UI.UserControl
    {
        string str;
        SqlCommand com;
        int count;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
                public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rptPages.ItemCommand +=
               new RepeaterCommandEventHandler(rptPages_ItemCommand);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                retrivemaxID(); LoadData();
            }
        }

        private void LoadData()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Items", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            PagedDataSource pgitems = new PagedDataSource();
            DataView dv = new DataView(dt);
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize =50;
            pgitems.CurrentPageIndex = PageNumber;
            if (pgitems.PageCount > 0)
            {
                _gridChEdit.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i < pgitems.PageCount; i++)
                    pages.Add((i + 1).ToString());
                rptPages.DataSource = pages;
                rptPages.DataBind();
            }
            else
                _gridChEdit.Visible = false;
            _gridChEdit.DataSource = pgitems;
            _gridChEdit.DataBind();
            //display page number in label
            lblPageNumber.Text = "Page " + (Convert.ToInt32(pgitems.CurrentPageIndex) + 1) + " of " + pgitems.PageCount;
        }
        void rptPages_ItemCommand(object source,
                                  RepeaterCommandEventArgs e)
        {
            PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
           
            LoadData();
        }
        private void retrivemaxID()
        {
            try
            {
                str = "select max(itemid) + 10 from Items";
                com = new SqlCommand(str, con);
                con.Open();
                if (com.ExecuteScalar() != DBNull.Value)
                    count = Convert.ToInt32(com.ExecuteScalar());
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
                TextBox uniname = e.Item.FindControl("_tbunit") as TextBox;
                uniname.Visible = true;
                Label unitLabel = e.Item.FindControl("_lblunit") as Label;
                unitLabel.Visible = false;
            }
            if (e.CommandName == "cancel")
            {
                Label unitLabel = e.Item.FindControl("_lblunit") as Label;
                unitLabel.Visible = true;
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
                TextBox uniname = e.Item.FindControl("_tbunit") as TextBox;
                uniname.Visible = false;
            }
            if (e.CommandName == "delete")
            {
                try
                {
                    Label lblHead = e.Item.FindControl("_lblitemID") as Label;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Items where itemid ='" + Convert.ToInt32(lblHead.Text) + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Response.Redirect("ManageItems.aspx");

                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("REFERENCE constraint"))
                    {
                        panelError.Visible = true;
                        lblError.Text = "The item to delete is already used in the inventory and cannot be deleted.";
                    }
                }
               

                finally
                {
                    con.Close();
                }
                

            }
            if (e.CommandName == "update")
            {
                TextBox aa = e.Item.FindControl("_tbHead") as TextBox;
                aa.Visible = false;
                Label lbl = e.Item.FindControl("_lblitemID") as Label;
                lbl.Visible = true;
                Button cancelBt = e.Item.FindControl("cancelBt") as Button;
                cancelBt.Visible = false;
                Button mm = e.Item.FindControl("updateBt") as Button;
                mm.Visible = false;
                Button ff = e.Item.FindControl("edirBt") as Button;
                ff.Visible = true;
                Label itemlabel = e.Item.FindControl("_lblitemID") as Label;
                TextBox itemunitET = e.Item.FindControl("_tbunit") as TextBox;

                //upodate logic
                // SqlDataAdapter adp = new SqlDataAdapter("Update Items set itemname=@itemname,unit=@unit where itemid ='" + Convert.ToInt32(lbl.Text) + "'", con);
                SqlCommand cmd = new SqlCommand("Update Items set itemname=@itemname,unit=@unit where itemid ='" + Convert.ToInt32(lbl.Text) + "'",  con);
                con.Open();

                SqlParameter param = new SqlParameter("@itemname", SqlDbType.NVarChar, 200);
                param.Value = aa.Text;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@unit", itemunitET.Text);
                cmd.ExecuteScalar();
                con.Close();
                //   DataSet ds = new DataSet();
                // adp.Fill(ds);
                panelSuccess.Visible = true;
                lblSuccess.Text = "Item Details Successfully updated.";

               // Response.Redirect("ManageItems.aspx");
            }

        }

        protected void saveItems_ServerClick(object sender, EventArgs e)
        {
            if (_tbHeadName.Text.Trim()=="")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Item Name";
                return;
            }
            if (_tbItemUnit.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Enter Item Unit";
                return;
            }
            try
            {
                con.Open();
                SqlCommand cmdd = new SqlCommand("select * from Items where itemid = @itemid", con);
                SqlParameter param = new SqlParameter();
                //SqlParameter param1 = new SqlParameter();
                param.ParameterName = "@itemid";
                param.Value = _tbchID.Text;
                cmdd.Parameters.Add(param);
                //cmdd.Parameters.Add(param1);

                SqlDataReader reader = cmdd.ExecuteReader();

                if (reader.HasRows)
                {
                    panelError.Visible = true;
                    lblSuccess.Text = "This Item ID already exists.Please choose another ID.";
                    return;
                }

                else
                {
                    string conn = "";
                    conn = ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ToString();
                    SqlConnection objsqlconn = new SqlConnection(conn);
                    objsqlconn.Open();
                    SqlCommand objcmd = new SqlCommand("Insert into Items (itemid,itemname,unit,Status) Values('" + _tbchID.Text + "','" + _tbHeadName.Text + "' , '" + _tbItemUnit.Text + "','" + _status.Text + "')", objsqlconn);
                    objcmd.ExecuteNonQuery();
                    panelSuccess.Visible = true;
                    lblSuccess.Text = "Item Name Successfully Uploaded.";
                    Response.Redirect("ManageItems.aspx", false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}