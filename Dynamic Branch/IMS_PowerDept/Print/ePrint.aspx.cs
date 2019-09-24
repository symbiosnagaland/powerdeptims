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
    public partial class ePrint : System.Web.UI.Page
    {
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}
        protected void Page_Load(object sender, EventArgs e)
        {                       
            GridView1.DataBind();  
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i = 1;
              int tempcounter=i + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
      
 }
 
}