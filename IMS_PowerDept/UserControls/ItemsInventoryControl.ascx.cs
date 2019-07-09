using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace IMS_PowerDept.UserControls
{
    public partial class ItemsInventoryControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);  
       

        }
           protected void gvItemsInventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

      

        protected void gvItemsInventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }

        protected void gvItemsInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
  

        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlItems.SelectedIndex != 0)
            {
                SelectedItemName(ddlItems.SelectedItem.ToString());
                divprintArea.Visible = true;

               string itemnameEncoded = HttpUtility.UrlEncode(ddlItems.SelectedItem.ToString()); 
                hyperlinkPrint.NavigateUrl = "/Print/Print_ItemsInventory.aspx?item=" + itemnameEncoded ;
            }
            else
            {
                divprintArea.Visible = false;
            }

           
        }
        private void SelectedItemName(string pStrItemName)
        {

            gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetSelectedItemNameDetails(pStrItemName).Tables[0];
            gvItemsInventory.DataBind();
            gvItemsInventory.Visible = true;
        }
    }
    }
