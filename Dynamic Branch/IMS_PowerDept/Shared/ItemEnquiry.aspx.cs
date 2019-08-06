using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Shared
{

     public partial class ItemEnquiry : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["item"] != null)
            {
                lblItemName.Text =HttpUtility.UrlDecode(Request.QueryString["item"].ToString());
                gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetSelectedItemNameDetails(lblItemName.Text).Tables[0];
                gvItemsInventory.DataBind();
                gvItemsInventory.Visible = true;


            }
        }     

     
    }
}