using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.CentralStore
{
    public partial class ItemsInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearchImage_ServerClick(object sender, EventArgs e)
        {

        }
        protected void btnAdvancedSearchFilters_Click(object sender, EventArgs e)
        {
            panelAdvancedSearchFilters.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            panelAdvancedSearchFilters.Visible = false;
        }

    }
}