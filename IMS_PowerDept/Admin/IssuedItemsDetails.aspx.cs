using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Admin
{
    public partial class IssuedItemsDetails : System.Web.UI.Page
    { 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            querystringid.Value = Request.QueryString["Id"];

           
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            if (appPath != "/")            
                Response.Redirect(appPath + "/Print/Print.aspx?Id=" + querystringid.Value);            
            else            
                Response.Redirect("/Print/Print.aspx?Id=" + querystringid.Value);          

           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            if (appPath != "/")
                Response.Redirect(appPath + "/Print/Print.aspx?Id=" + querystringid.Value);
            else
                Response.Redirect("/Print/Print.aspx?Id=" + querystringid.Value);

        }

        protected void _lvdetails_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
             if (e.CommandName == "Update")
             {
                 Label ide = e.Item.FindControl("cid") as Label;
                 SqlDataAdapter adp = new SqlDataAdapter("Update DeliveryItemsChallan set IsDeliveredTemporary= @IsDeliveredTemporary where DeliveryItemsChallanID ='" + Convert.ToInt32(ide.Text) + "'", con);
                 adp.SelectCommand.Parameters.AddWithValue("@IsDeliveredTemporary", "No");

                 DataSet ds = new DataSet();
                 adp.Fill(ds);

                 //Response.Redirect("IssuedItemsDetails.aspx=Id" + querystringid.Value);
                 Response.Redirect("IssuedItemsDetails.aspx?Id=" + querystringid.Value);
             }
        }

        protected void _lvdetails_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            
                HiddenField lbl = e.Item.FindControl("HiddenField1") as HiddenField;
                Label status = e.Item.FindControl("status") as Label;
                Panel lk = e.Item.FindControl("Panel1") as Panel;
                if (lbl.Value=="Yes")
                {
                   
                    status.Text = "Temporary Issued Items";
                    status.ForeColor = Color.Red;
                    lk.Visible = true;
                }
                else
                {
                    status.Text = "Regular Issued Items"; status.ForeColor = Color.Black;
                    lk.Visible = false;
                }
            }
    }
}