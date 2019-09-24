using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.UserControls
{
    public partial class ItemsListInventoryControl : System.Web.UI.UserControl
    {


        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                 SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
            }

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
           /* string appPath = HttpRuntime.AppDomainAppVirtualPath;

         
             string scriptPath =   "<script>window.open('/Print/Print_ItemsInventory.aspx?ID=1&issueheadname=@ihn&date=@date','_newtab'); </script>";
             scriptPath = scriptPath.Replace("@ihn", Utilities.QueryStringEncode(IssueHeadList.SelectedValue));

             if (tbDate.Text == "")
             {
                 scriptPath = scriptPath.Replace("@date", DateTime.Today.ToString("dd/MM/yyyy"));
             }
             else
             {
                 tbDate.Text = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                 scriptPath = scriptPath.Replace("@date", tbDate.Text);
             }
           
            if (appPath != "/")         

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                 "click","@" + scriptPath, false);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click","@" + scriptPath, false);

          //  SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
            */
        }
        private void SelectedIssueHeadNameDetails(string pStrIssueHeadName)
        {
            if (pStrIssueHeadName == "All" || pStrIssueHeadName=="")
            {
                gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetAllDetails().Tables[0];
                gvItemsInventory.DataBind();
                gvItemsInventory.Visible = true;
            }
            else
            {
                gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(pStrIssueHeadName).Tables[0];
                gvItemsInventory.DataBind();
                gvItemsInventory.Visible = true;
            }
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
          /*  string appPath = HttpRuntime.AppDomainAppVirtualPath;
            string scriptPath = "<script>window.open('/Print/Print_ItemsInventory.aspx?ID=2&issueheadname=@ihn&date=@date','_newtab'); </script>";
            scriptPath = scriptPath.Replace("@ihn", Utilities.QueryStringEncode(IssueHeadList.SelectedValue));

            string myDate = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            if (tbDate.Text == "")
            {
                scriptPath = scriptPath.Replace("@date", DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else
            {
                scriptPath = scriptPath.Replace("@date", myDate);
            }
          
            if (appPath != "/")
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),               
                     "click","@" + scriptPath, false);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                   "click","@" + scriptPath, false);
          //  SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
           * 
           */
        }

        void DisplayCurrentPage()
        {
          //  int currentPage = gvItemsInventory.PageIndex + 1;
         //   Message.Text = "Page " + currentPage.ToString() + " of " +
        //   gvItemsInventory.PageCount.ToString();
         
        }

        protected void gvItemsInventory_DataBound1(object sender, EventArgs e)
        {
         
        }

        protected void gvItemsInventory_PageIndexChanged(object sender, EventArgs e)
        {
           
           SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
           DisplayCurrentPage();
        }
      
  
        protected void _btnSelectedIssueHead_Click(object sender, EventArgs e)
        {
            SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
            DisplayCurrentPage();
        }

    

        protected void gvItemsInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
                gvItemsInventory.PageIndex = e.NewPageIndex;
                SelectedIssueHeadNameDetails(IssueHeadList.SelectedValue.ToString());
              
           
        }

        protected void btnSearchbyDates_Click(object sender, EventArgs e)
        {
            //string selectedIssueHead = IssueHeadList.SelectedValue.ToString();

            //if (selectedIssueHead == "All" || selectedIssueHead == "")
            //{
            //    gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetAllDetailsByDates(tbStartDateSearch.Text, tbEndDateSearch.Text);
            //    gvItemsInventory.DataBind();
            //    gvItemsInventory.Visible = true;
            //}
            //else
            //{
            //    gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(selectedIssueHead, tbStartDateSearch.Text, tbEndDateSearch.Text);
            //    gvItemsInventory.DataBind();
            //    gvItemsInventory.Visible = true;
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(tbDate.Text=="")
            {
                return;
            }
            string selectedIssueHead = IssueHeadList.SelectedValue.ToString();

            string myDate = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            if (selectedIssueHead == "All" || selectedIssueHead == "")
            {
                gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetAllDetailsByDate(myDate);
                gvItemsInventory.DataBind();
                gvItemsInventory.Visible = true;
            }
            else
            {
                gvItemsInventory.DataSource = SelectedIssueHeadDetails.GetSelectedIssueHeadDetails(selectedIssueHead, myDate);
                gvItemsInventory.DataBind();
                gvItemsInventory.Visible = true;
            }
        }

        protected void tbDate_TextChanged(object sender, EventArgs e)
        {

        }

     

               }
    }

    
