using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace IMS_PowerDept.UserControls
{
    public partial class ReceiveItems : System.Web.UI.UserControl
    {
        protected static DataTable dtItems = new DataTable();
        protected static DataTable gridviewItemsDataTable = new DataTable();
        string myTempData;


        protected void Page_Load(object sender, EventArgs e)
        {

            /*for the purpose of opening new print page if saved */
            if (Session["OTEONUMBER"] != null)
            {
                //temp code to display properly in local as well as in hosting environment 
                string appPath = HttpRuntime.AppDomainAppVirtualPath;
                if (appPath != "/")
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(' " + appPath + "/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
                Session["OTEONUMBER"] = null;
            }
            /*for the purpose of opening new print page if saved */




            if (!Page.IsPostBack)
            {
                //InsertEmptyRow();
                GetIssueHeadsandItemsForDropDowns();
                InsertEmptyRow(10);
                _btnSave.Visible = Button1.Visible = true;


                ddlChargeableHead.Enabled = false;
                gvItems.Enabled = false;


            }

        }


        //trying to call a code befhind function using java script
        [WebMethod]
        public static string ProcessIT(string issueHead, string itemId)
        {
            int a = 5;
            int b = 7;
            int c = a + b;

            return "hello world " + c;
        }


        // do not delete till final deployment 
        private void GetIssueHeadsandItemsForDropDowns()
        {
            int maxOTEOID;
            DataSet dst = new DataSet();
            dst = ReceivedItemsLogic.RetrieveActiveIssueHeadsAndActiveItemsSeperately(out maxOTEOID);//using the out parameter to receive the max +1 oteoid from db
            ddlIssueHead.DataSource = dst.Tables[0]; //first table contains the issue heads
            ddlIssueHead.DataValueField = "ISSUEHEADID";
            ddlIssueHead.DataTextField = "ISSUEHEADNAME";
            ddlIssueHead.DataBind();
            ddlIssueHead.Items.Insert(0, new ListItem("--Select Issue Head--", ""));
            //second table contains the items names
            dtItems = dst.Tables[1];

            //auto populating the max +1 oteoid
            tbOtEONumber.Text = maxOTEOID.ToString();

        }

        //insert row in gridview 
        private void InsertEmptyRow(int numberofRows)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));

            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Order No", typeof(string)));
            for (int i = 0; i < numberofRows; i++)
            {
                dr = dt.NewRow();

                dr["Item"] = string.Empty;
                dr["Unit"] = string.Empty;
                dr["Quantity"] = string.Empty;

                dr["Rate"] = string.Empty;
                dr["Amount"] = string.Empty;
                dr["Order NO"] = string.Empty;

                dt.Rows.Add(dr);
            }

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvItems.DataSource = dt;
            gvItems.DataBind();
        }


        protected void ddlIssueHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueHead.SelectedValue != "")
            {
                ddlChargeableHead.Enabled = true;
                gvItems.Enabled = true;


                ddlChargeableHead.DataSource = ReceivedItemsLogic.RetrieveCorrespondingActiveChargeableHeads(Convert.ToInt32(ddlIssueHead.SelectedValue));
                ddlChargeableHead.DataValueField = "CHARGEABLEHEADID";
                ddlChargeableHead.DataTextField = "CHARGEABLEHEADNAME";
                ddlChargeableHead.DataBind();
                ddlChargeableHead.Items.Insert(0, new ListItem("--Select ChargeableHead Head--", ""));

                ReceivedItemsLogic.RetrieveActiveIssueHeadsAndActiveItemsSeperatelyOnIssueItemChanged(ddlIssueHead.SelectedValue);

            }
            else
            {
                ddlChargeableHead.Enabled = false;
                gvItems.Enabled = false;

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the DropDownList in the Row
                    DropDownList ddItemName = (e.Row.FindControl("_ddItems") as DropDownList);
                    ddItemName.DataSource = dtItems;
                    ddItemName.DataTextField = "itemname";

                    ddItemName.DataValueField = "itemid_unit";

                    ddItemName.DataBind();
                    ddItemName.Items.Insert(0, new ListItem("--Select ItemName--", "0"));



                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error in loading the items.')", true);
            }
        }


        private void AddNewRowsToGrid()
        {
            int rowIndex = 0;

            TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
            string totalAmount = tbtotalAmount.Text;

            //first work being done is to   extract the gridview control items values(if any)
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //  extract the TextBox values
                        DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
                        TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
                        TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbQuantity");
                        TextBox tbRate = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("tbRate");
                        TextBox tbAmount = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("tbAmount");
                        TextBox tbOrderNo = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbOrderNo");

                        drCurrentRow = dtCurrentTable.NewRow();
                        //   drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Item"] = _ddItems.Text;
                        dtCurrentTable.Rows[i - 1]["Unit"] = _tbUnit.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = _tbQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = tbRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = tbAmount.Text;
                        dtCurrentTable.Rows[i - 1]["OrderNO"] = tbOrderNo.Text;



                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    int numberOfnewRowsToAdd = 10;
                    while (numberOfnewRowsToAdd > 1)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        numberOfnewRowsToAdd--;
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvItems.DataSource = dtCurrentTable;
                    gvItems.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }



            //Set Previous Data on Postbacks         
            rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
                        TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
                        TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbQuantity");
                        TextBox tbRate = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("tbRate");
                        TextBox tbAmount = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("tbAmount");
                        //Label lblAmount = (Label)gvItems.Rows[rowIndex].Cells[3].FindControl("lblAmount");
                        _ddItems.Text = dt.Rows[i]["Item"].ToString();
                        _tbUnit.Text = dt.Rows[i]["Unit"].ToString();
                        _tbQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        tbRate.Text = dt.Rows[i]["Rate"].ToString();
                        tbAmount.Text = dt.Rows[i]["Amount"].ToString();

                        rowIndex++;

                    }

                    TextBox tbtotalAmount2 = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;


                    //  string totalAMount = (string) ViewState["TOTALAMOUNT"];
                    tbtotalAmount2.Text = totalAmount;


                }
            }

        }
        /// <summary>
        /// save to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReceivedItemsLogic ameh = new ReceivedItemsLogic();
                properties RecievedItemsOrderObject = new properties();

                RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);

                RecievedItemsOrderObject.Date = DateTime.ParseExact(tbOTEODate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                //RecievedItemsOrderObject.Date = tbOTEODate.Text;

                RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;

                RecievedItemsOrderObject.SupplyDate = DateTime.ParseExact(tbSupplyDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //RecievedItemsOrderObject.SupplyDate = tbSupplyDate.Text;

                RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
                RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();



                TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                //setting user id temp here
                Session["USERID"] = 1;
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();

                //  sb.Append("<root>");
                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRateMaster = "INSERT into ItemsRateMaster (itemid,Rate,MaxOrderNO,Quantity,IssueHeadName) values('@ITEMID','@RATE','@odNo','@QUANTITY','@issueHead')";

                string updateRateMaster = "Update  ItemsRateMaster set MaxOrderNO= '@odNo' where itemid= '@ITEMID'";

                string insertRateMasterSecondary = "INSERT into ItemsRateSecondary (itemid,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMID','@RATE','@odNo','@QUANTITY','@issueHead')";



                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    // TextBox tbItemID = gvItems.Rows[i].FindControl("tbItemID") as TextBox;
                    HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("hdnFieldItemID") as HiddenField;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;
                    TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                    TextBox _tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES

                    //SAVE CODE
                    myTempData = _tbOrderNo.Text;
                    if (itemName.SelectedValue.ToString() != "0")
                    {
                        sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));
                        sb1.Append(insertRateMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@RATE", tbRate.Text).Replace("@odNo", _tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));

                        sb2.Append(insertRateMasterSecondary.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@RATE", tbRate.Text).Replace("@odNo", _tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));

                        sb3.Append(updateRateMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@odNo", _tbOrderNo.Text));

                    }
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    //ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(),sb1.ToString (),sb2.ToString(),sb3.ToString ());

                    //
                    if (sb1.ToString() != "" && sb2.ToString() != "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb1.ToString(), sb2.ToString(), sb3.ToString());
                    }
                    else if (sb1.ToString() != "" && sb2.ToString() == "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb1.ToString());
                    }

                    else if (sb1.ToString() == "" && sb2.ToString() != "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb2.ToString(), sb3.ToString());
                    }
                    //

                    Response.Redirect("ReceivedItemsDetails.aspx?id=" + tbOtEONumber.Text);

                }


                else
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Select atleast one item to add.";
                    panelSuccess.Visible = false;

                }

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! The OTEO ID already exists. Please use another ID";
                    panelSuccess.Visible = false;
                    tbOtEONumber.BorderColor = System.Drawing.Color.Red;
                }
                else
                {
                    Session["ERRORMSG"] = ex.ToString();
                    Response.Redirect("Error.aspx");
                }
            }

            catch (System.Threading.ThreadAbortException)
            {
                //do nothing 
            }

            catch (Exception ex)
            {

                panelError.Visible = true;
                lblError.Text = ex.Message;
                panelSuccess.Visible = false;
            }
        }

        protected void ButtonAddnewRows_Click(object sender, EventArgs e)
        {
            AddNewRowsToGrid();
        }
        /// <summary>
        /// on selection of chargeable head
        /// populate the issue head dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());

        }

        protected void btnRowsAdd_Click(object sender, EventArgs e)
        {
            InsertEmptyRow(Convert.ToInt32(tbItemsRows.Text));
            _btnSave.Visible = Button1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void _save_Click(object sender, EventArgs e)
        {
            try
            {
                ReceivedItemsLogic ameh = new ReceivedItemsLogic();
                properties RecievedItemsOrderObject = new properties();

                RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);

                RecievedItemsOrderObject.Date = DateTime.ParseExact(tbOTEODate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                // RecievedItemsOrderObject.Date = tbOTEODate.Text;
                RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;

                RecievedItemsOrderObject.SupplyDate = DateTime.ParseExact(tbSupplyDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //RecievedItemsOrderObject.SupplyDate = tbSupplyDate.Text;

                RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
                RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();



                TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                //setting user id temp here
                Session["USERID"] = 1;
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control

                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();

                //  sb.Append("<root>");

                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRateMaster = "INSERT into ItemsRateMaster (itemid,Rate,MaxOrderNO,Quantity,IssueHeadName) values('@ITEMID','@RATE','@odNo','@QUANTITY','@issueHead')";
                string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@odNo' where itemid='@ITEMID'";


                string insertRateSecondary = "INSERT into ItemsRateSecondary (itemid,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMID','@RATE','@odNo','@QUANTITY','@issueHead')";


                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    // TextBox tbItemID = gvItems.Rows[i].FindControl("tbItemID") as TextBox;

                    HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("hdnFieldItemID") as HiddenField;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;

                    TextBox tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;

                    TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES
                    myTempData = tbOrderNo.Text;
                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "0")
                    {

                        sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));

                        if (myTempData == "1")//no such item there in master
                        {
                            sb1.Append(insertRateMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        }
                        else
                        {
                            sb3.Append(UpdateRateMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@odNo", tbOrderNo.Text));
                            sb2.Append(insertRateSecondary.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        }
                    }
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    if (sb1.ToString() != "" && sb2.ToString() != "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb1.ToString(), sb2.ToString(), sb3.ToString());
                    }
                    else if (sb1.ToString() != "" && sb2.ToString() == "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb1.ToString());
                    }

                    else if (sb1.ToString() == "" && sb2.ToString() != "")
                    {
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(), sb2.ToString(), sb3.ToString());
                    }


                    //Session["OTEONUMBER"] = tbOtEONumber.Text;
                    Response.Redirect("ReceivedEntriesList.aspx");
                }

                else
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Select atleast one item to add.";
                    panelSuccess.Visible = false;

                }

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! The OTEO ID already exists. Please use another ID";
                    panelSuccess.Visible = false;
                    tbOtEONumber.BorderColor = System.Drawing.Color.Red;
                }
                else
                {
                    Session["ERRORMSG"] = ex.ToString();
                    Response.Redirect("Error.aspx");
                }
            }

            catch (System.Threading.ThreadAbortException)
            {
                //do nothing 
            }

            catch (Exception ex)
            {

                panelError.Visible = true;
                lblError.Text = ex.Message;
                panelSuccess.Visible = false;
            }
        }

        protected void tbOTEODate_TextChanged(object sender, EventArgs e)
        {


        }

        protected void tbOTEODate_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}