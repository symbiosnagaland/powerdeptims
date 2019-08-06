using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.UserControls
{
    public partial class ErrorControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (Session["ERRORMSG"] != null)
                {
                    tbErrorDetails.Visible = true;
                    tbErrorDetails.Text = Session["ERRORMSG"].ToString();

                    //save error log
                    try
                    {
                        //with details
                        //bool true =true;
                        if (Session["username"] != null)
                            Utilities.WriteLog(true, Session["username"].ToString(), tbErrorDetails.Text, Request.UrlReferrer.ToString());
                        else
                            Utilities.WriteLog(true, "", tbErrorDetails.Text, Request.UrlReferrer.ToString());
                    }
                    catch
                    {
                        //further do nothing
                    }
                    tbErrorDetails.ReadOnly = true;

                }
                // }
                else//no error msg means save only previous page url
                {
                    try
                    {
                        tbErrorDetails.Visible = false;
                        Utilities.WriteLog(true, "", "", Request.UrlReferrer.ToString());
                    }
                    catch
                    {//do nothing . we are already in error page yaar.
                    }

                }


            }
        }


        //private void sendErrorMail(string pMessage)
        //{
        //    try
        //    {
        //        if (ViewState["PreviousPageUrl"] != null)
        //        {
        //            if (MailManager.ErrorMailSending(pMessage, ViewState["PreviousPageUrl"].ToString()))
        //            {
        //                successDiv.Visible = true;
        //                successLabel.Text = "Mail sent successfully";
        //            }
        //            else
        //            {
        //                errorDiv.Visible = true;
        //                errorLabel.Text = "Error! Email could not be sent.";
        //            }
        //        }
        //        else//viewstate is null
        //        {

        //            if (MailManager.ErrorMailSending(pMessage, "Error Page"))
        //            {
        //                successDiv.Visible = true;
        //                successLabel.Text = "Mail sent successfully";
        //            }
        //            else
        //            {
        //                errorDiv.Visible = true;
        //                errorLabel.Text = "Error! Email could not be sent.";
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //protected void lbsenderror_Click(object sender, EventArgs e)
        //{
        //    //implement send error mail here witht the error details including the page it got redirected from      
        //    try
        //    {
        //        if (Session["ERRORMSG"] != null)
        //        {

        //            string message = Session["ERRORMSG"].ToString();
        //            sendErrorMail(message);

        //        }
        //        else
        //        {
        //            errorDiv.Visible = true;
        //            errorLabel.Text = "Error details could not be sent. Please mail us your error details in the specified support email.";
        //        }
        //    }

        //    catch
        //    {
        //        throw;

        //    }

        //}
    }
}