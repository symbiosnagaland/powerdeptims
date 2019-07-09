using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Print
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvitems.DataBind();
            gvitems.UseAccessibleHeader = true;
            gvitems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i = 1;
            int tempcounter = i + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

}