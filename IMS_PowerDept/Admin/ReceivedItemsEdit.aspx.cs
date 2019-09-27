using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Admin
{
    public partial class ReceivedItemsEdit : System.Web.UI.Page
    {

        protected static DataTable dtItems = new DataTable();
        //  protected static DataTable gridviewItemsDataTable = new DataTable();
        protected static string OTEOID;

        protected static DataSet dst = new DataSet();

        string CheckExistingItem;

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (Request.QueryString["oteoid"] != null)
                {
                    OTEOID = Request.QueryString["oteoid"];
                    //based on above oteoid , get the item details to edit
                    GetRecievedItemsDetails(OTEOID);
                    GetIssueHeadsandItemsForDropDowns();

                    //first get data from db
                    RetrieveActive_IssueHeads_Items_ChargeableHeads();
                    //second, when empty rows are created, in gridview row databound itself the items are populated retrieved above
                    InsertEmptyRow(Convert.ToInt32(tbItemsRows.Text));

                    _btnSave.Visible = _btnCancel.Visible = true;
                }
            }


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

        }

        private void GetIssueHeadsandItemsForDropDowns()
        {

            dst = ReceiveItemsNewLogic.RetrieveAllItems();
            //second table contains the items names                             
            dtItems = dst.Tables["Items"];
            // dtIssueHead = dst.Tables["itemRatesSecondery"];
            // dtRate = dst.Tables["IT2"];
        }


        //populating data for edit purpose
        private void RetrieveActive_IssueHeads_Items_ChargeableHeads()
        {

            DataSet dst = new DataSet();
            dst =
                 ReceivedItemsLogic.RetrieveActive_IssueHeads_Items_ChargeableHeads();
            ddlIssueHead.DataSource = dst.Tables[0]; //first table contains the issue heads
            ddlIssueHead.DataValueField = "ISSUEHEADID";
            ddlIssueHead.DataTextField = "ISSUEHEADNAME";
            ddlIssueHead.DataBind();
            ddlIssueHead.Items.Insert(0, new ListItem("--Select Issue Head--", ""));

            //second table contains the items names
            dtItems = dst.Tables[1];

            //3rd table contains the ch heads
            //ddlChargeableHead.DataSource = dst.Tables[2];
            //ddlChargeableHead.DataValueField = "CHARGEABLEHEADID";
            //ddlChargeableHead.DataTextField = "CHARGEABLEHEADNAME";
            //ddlChargeableHead.DataBind();
            //ddlChargeableHead.Items.Insert(0, new ListItem("--Select ChargeableHead Head--", ""));



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
            for (int i = 0; i < numberofRows; i++)
            {
                dr = dt.NewRow();

                dr["Item"] = string.Empty;
                dr["Unit"] = string.Empty;
                dr["Quantity"] = string.Empty;

                dr["Rate"] = string.Empty;
                dr["Amount"] = string.Empty;
                dt.Rows.Add(dr);
            }

            //Store the DataTable in ViewState
            // ViewState["CurrentTable"] = dt;

            gvItems.DataSource = dt;
            gvItems.DataBind();
        }


        protected void ddlIssueHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueHead.SelectedValue != "")
            {
                ddlChargeableHead.Enabled = true;
                ddlChargeableHead.DataSource = ReceivedItemsLogic.RetrieveCorrespondingActiveChargeableHeads(Convert.ToInt32(ddlIssueHead.SelectedValue));
                ddlChargeableHead.DataValueField = "CHARGEABLEHEADID";
                ddlChargeableHead.DataTextField = "CHARGEABLEHEADNAME";
                ddlChargeableHead.DataBind();
                ddlChargeableHead.Items.Insert(0, new ListItem("--Select ChargeableHead Head--", ""));

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
                //if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    TextBox tbtotalAmount = e.Row.FindControl("tbtotalAmount") as TextBox;

                //    tbtotalAmount.Text = tbtotalAmountAddedItems.Text;

                //}
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error in loading the items.')", true);
            }
        }


        #region update items
        /// <summary>
        /// save to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        protected void _btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReceivedItemsLogic ameh = new ReceivedItemsLogic();
                properties RecievedItemsOrderObject = new properties();
                int originalOTEOID = Convert.ToInt32(hdnFieldOTEOID.Value);
                //int ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);//new 
                RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);
                RecievedItemsOrderObject.Date = tbOTEODate.Text;
                RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
                RecievedItemsOrderObject.SupplyDate = tbSupplyDate.Text;
                RecievedItemsOrderObject.Supplier = tbSupplierName.Text;

                // RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                // RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                if (ddlChargeableHead.SelectedIndex > 0)
                {
                    RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                }
                else
                {
                    RecievedItemsOrderObject.ChargeableHeadName = lblChargeableHead.Text; //same old value populated
                }

                if (ddlIssueHead.SelectedIndex > 0)
                {
                    RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                }
                else
                {
                    RecievedItemsOrderObject.IssueHeadName = lblIssueHeadOld.Text; //same old value populated
                }



                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);

                StringBuilder sb = new StringBuilder();

                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRateMaster = "INSERT into ItemsRateMaster (itemname,MaxOrderNO,IssueHeadName) values('@ITEMNAME','@odNo','@issueHead')";
                string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@odNo' where itemname='@ITEMNAME' AND IssueHeadName='@issueHead'";
                string insertRateSecondary = "INSERT into ItemsRateSecondary (ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";

                for (int i = 0; i < gvItems.Rows.Count; i++)
                {

                    HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;
                    TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                    TextBox tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;

                    CheckExistingItem = tbOrderNo.Text;
                    if (itemName.SelectedValue.ToString() != "0")
                    {
                        double result;
                        if (!double.TryParse(tbQuantity.Text, out result))
                        {

                            panelError.Visible = true;
                            lblError.Text = "Should Be Numeric";
                            panelSuccess.Visible = false;
                            tbQuantity.Focus();
                            tbQuantity.Style.Add("background", "Pink");
                            return;
                        }
                        else
                        {
                            tbQuantity.Style.Add("background", "White");
                        }

                        double result1;
                        if (!double.TryParse(tbRate.Text, out result1))
                        {

                            panelError.Visible = true;
                            lblError.Text = "Should Be Numeric";
                            panelSuccess.Visible = false;
                            tbRate.Focus();
                            tbRate.Style.Add("background", "Pink");
                            return;
                        }
                        else
                        {
                            tbRate.Style.Add("background", "White");
                        }

                        sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));

                        if (CheckExistingItem == "1")
                        {
                            sb.Append(insertRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                        }
                        else
                        {
                            sb.Append(UpdateRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                        }

                    }
                }
                //now save it to db
                if (sb.ToString() != "")
                {

                    TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID, sb.ToString());
                    Response.Redirect("ReceivedItemsDetails.aspx?id=" + tbOtEONumber.Text);
                }
                else
                {
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmountAddedItems.Text);
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID);  //originalOTEOID 
                    Response.Redirect("ReceivedItemsDetails.aspx?id=" + tbOtEONumber.Text);

                }

                Response.Redirect("~/Admin/ReceivedEntriesList.aspx");
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
       
        #endregion



        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());

        }

        protected void btnRowsAdd_Click(object sender, EventArgs e)
        {
            InsertEmptyRow(Convert.ToInt32(tbItemsRows.Text));
            _btnSave.Visible = btnReset1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }




        #region Get Received Items Details DB
        private void GetRecievedItemsDetails(string oteoID)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select ri.ReceivedItemOTEODate,ri.SupplyOrderReference,ri.SupplyOrderDate, ri.Supplier,  ri.TotalAmount, ch.ChargeableHeadID, ch.IssueHeadID from ReceivedItemsOTEO ri, ChargeableHeads ch where ri.ChargeableHeadName= ch.ChargeableHeadName and ri.ReceivedItemsOTEOID=@ReceivedItemsOTEOID";
                cmd.CommandText = "select ri.ReceivedItemOTEODate,ri.SupplyOrderReference,ri.SupplyOrderDate,ChargeableHeadName, IssueHeadName ,ri.Supplier,  ri.TotalAmount from ReceivedItemsOTEO ri where ri.ReceivedItemsOTEOID=@ReceivedItemsOTEOID";
                cmd.Parameters.AddWithValue("@ReceivedItemsOTEOID", oteoID);
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read()) //ri.ChargeableHeadName, ri.IssueHeadName,
                    {
                        hdnFieldOTEOID.Value = tbOtEONumber.Text = oteoID;
                        tbOTEODate.Text = Convert.ToDateTime(dr["ReceivedItemOTEODate"]).ToString("dd-MM-yyyy");
                        tbSupplyOrderReference.Text = dr["SupplyOrderReference"].ToString();
                        tbSupplyDate.Text = Convert.ToDateTime(dr["SupplyOrderDate"]).ToString("dd-MM-yyyy");
                        tbSupplierName.Text = dr["Supplier"].ToString();

                        // ddlChargeableHead.SelectedValue = 
                        // ddlIssueHead.SelectedValue = dr["IssueHeadID"].ToString();
                        lblIssueHeadOld.Text = dr["IssueHeadName"].ToString();
                        lblChargeableHead.Text = dr["ChargeableHeadName"].ToString();

                        tbtotalAmountAddedItems.Text = dr["totalamount"].ToString();


                    }
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                dr.Close();
                conn.Close();

            }


        }
        #endregion


        /// <summary>
        /// //on row command of the items, mainly now 1. delete specfic row of slected item id in the secondary table
        /// 2. update sum total amount in primary table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItems_Edit_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //if (e.CommandName == "Delete")
            //instead of using command name =delete, using amount value
            if (e.CommandName != "")
            {
                //Label lblAmount = (Label) gvItems_Edit.FindControl(e.
                double amount = Convert.ToDouble(e.CommandName);
                string[] MyArraryOfCommand = e.CommandArgument.ToString().Split(new char[] { ',' });

                string receivedItemID = MyArraryOfCommand[0];
                string ItemID = MyArraryOfCommand[1];
                string itemname = MyArraryOfCommand[2];
                string rate=MyArraryOfCommand[3];
                string issueheadname = MyArraryOfCommand[4];
                string quantity = MyArraryOfCommand[5];

                SqlTransaction tr = null;
                SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
                //this will execute first
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [ReceivedItemsDetails] WHERE [ReceivedItemID] = @ReceivedItemID";
                cmd.Parameters.AddWithValue("@ReceivedItemID", receivedItemID);

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "update ReceivedItemsOTEO set totalamount = totalamount-" + amount + " where receiveditemsoteoid =@receiveditemsoteoid";
                //cmd2.Parameters.AddWithValue("@amount",);
                cmd2.Parameters.AddWithValue("@receiveditemsoteoid", OTEOID);


                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "SELECT COUNT (*) FROM ReceivedItemsDetails INNER JOIN ReceivedItemsOTEO ON ReceivedItemsDetails.ReceivedItemsOTEOID = ReceivedItemsOTEO.ReceivedItemsOTEOID WHERE (ReceivedItemsDetails.ReceivedItemsOTEOID = @ReceivedItemsOTEOID)";
                cmd3.Parameters.AddWithValue("@ReceivedItemsOTEOID", OTEOID);

                SqlCommand cmd4 = conn.CreateCommand();
                //cmd4.CommandText = "DELETE FROM [Itemsratesecondary] WHERE [id] = @id ";

                cmd4.CommandText = "update top(1) [Itemsratesecondary] set quantity=quantity-@quantity WHERE [itemname] = @itemname and "+
                    "[rate] = @rate and [issueheadname] = @issueheadname";

                cmd4.Parameters.AddWithValue("@ItemID", ItemID);
                cmd4.Parameters.AddWithValue("@quantity", quantity);
                cmd4.Parameters.AddWithValue("@itemname", itemname);
                cmd4.Parameters.AddWithValue("@rate", rate);
                cmd4.Parameters.AddWithValue("@issueheadname", issueheadname);
                 cmd4.Parameters.AddWithValue("@receiveditemsoteoid", OTEOID);



                try
                {
                    using (conn)
                    {
                        conn.Open();
                        tr = conn.BeginTransaction();
                        cmd.Transaction = tr;
                        cmd2.Transaction = tr;
                        cmd4.Transaction = tr;

                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        int rowsAffected =cmd4.ExecuteNonQuery();
                        if(rowsAffected ==0)
                        {
                            tr.Rollback();
                        }
                        else
                        {
                            tr.Commit();
                        }
                       
                        int count = Convert.ToInt32(cmd3.ExecuteScalar());
                        if (count < 1)
                        {
                            _btnDelete.Visible = true;
                        }
                    }
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                GetRecievedItemsDetails(OTEOID);
                gvItems_Edit.DataBind();

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ReceivedItemsLogic ameh = new ReceivedItemsLogic();
            properties RecievedItemsOrderObject = new properties();
            int originalOTEOID = Convert.ToInt32(hdnFieldOTEOID.Value);
            //int ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);//new 
            RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);
            RecievedItemsOrderObject.Date = tbOTEODate.Text;
            RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
            RecievedItemsOrderObject.SupplyDate = tbSupplyDate.Text;
            RecievedItemsOrderObject.Supplier = tbSupplierName.Text;

            // RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
            // RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
            if (ddlChargeableHead.SelectedIndex > 0)
            {
                RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
            }
            else
            {
                RecievedItemsOrderObject.ChargeableHeadName = lblChargeableHead.Text; //same old value populated
            }

            if (ddlIssueHead.SelectedIndex > 0)
            {
                RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
            }
            else
            {
                RecievedItemsOrderObject.IssueHeadName = lblIssueHeadOld.Text; //same old value populated
            }



            RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
           
            StringBuilder sb = new StringBuilder();
           
            string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

            string insertRateMaster = "INSERT into ItemsRateMaster (itemname,MaxOrderNO,IssueHeadName) values('@ITEMNAME','@odNo','@issueHead')";
            string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@odNo' where itemname='@ITEMNAME' AND IssueHeadName='@issueHead'";
            string insertRateSecondary = "INSERT into ItemsRateSecondary (ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";
            
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {

                HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField;
                DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;
                TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                TextBox tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;

                CheckExistingItem = tbOrderNo.Text;
                if (itemName.SelectedValue.ToString() != "0")
                {
                    double result;
                    if (!double.TryParse(tbQuantity.Text, out result))
                    {

                        panelError.Visible = true;
                        lblError.Text = "Should Be Numeric";
                        panelSuccess.Visible = false;
                        tbQuantity.Focus();
                        tbQuantity.Style.Add("background", "Pink");
                        return;
                    }
                    else
                    {
                        tbQuantity.Style.Add("background", "White");
                    }

                    double result1;
                    if (!double.TryParse(tbRate.Text, out result1))
                    {

                        panelError.Visible = true;
                        lblError.Text = "Should Be Numeric";
                        panelSuccess.Visible = false;
                        tbRate.Focus();
                        tbRate.Style.Add("background", "Pink");
                        return;
                    }
                    else
                    {
                        tbRate.Style.Add("background", "White");
                    }

                    sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));

                    if (CheckExistingItem == "1")
                    {
                        sb.Append(insertRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                        sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                    }
                    else
                    {
                        sb.Append(UpdateRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                        sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", RecievedItemsOrderObject.IssueHeadName));
                    }

                }
            }
                //now save it to db
            if (sb.ToString() != "")
            {
                   
                TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID, sb.ToString());
            }
            else 
            {
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmountAddedItems.Text);
                ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID);  //originalOTEOID 

            }

                Response.Redirect("~/Admin/ReceivedEntriesList.aspx");
            

        }

        protected void _btnDelete_Click(object sender, EventArgs e)
        {

            int index = Convert.ToInt32(hdnFieldOTEOID.Value);
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());

            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = "SELECT COUNT (*) FROM ReceivedItemsDetails INNER JOIN ReceivedItemsOTEO ON ReceivedItemsDetails.ReceivedItemsOTEOID = ReceivedItemsOTEO.ReceivedItemsOTEOID WHERE (ReceivedItemsDetails.ReceivedItemsOTEOID = @ReceivedItemsOTEOID)";
            cmd1.Parameters.AddWithValue("@ReceivedItemsOTEOID", index);
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = "DELETE FROM [ReceivedItemsOTEO]  WHERE [ReceivedItemsOTEOID]  = @ReceivedItemsOTEOID";
            cmd2.Parameters.AddWithValue("@ReceivedItemsOTEOID", index);
            conn.Open();

            int count = Convert.ToInt32(cmd1.ExecuteScalar());

            if (count < 1)
            {
                cmd2.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("ReceivedEntriesList.aspx");

            }
        }

        protected void gvItems_Edit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void _ddItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);


            foreach (GridViewRow row in gvItems.Rows)
            {
                //Finding Dropdown control  
                Control ctrl = row.FindControl("_ddItems") as DropDownList;

                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    //   Comparing ClientID of the dropdown with sender
                    if (ddl.ClientID == ddl1.ClientID)
                    {

                        string selected = ddl.SelectedItem.Value;
                        DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;



                        TextBox _tbUnit = row.FindControl("_tbUnit") as TextBox;



                        TextBox tbOrderNO = row.FindControl("_tbOrderNo") as TextBox;



                        HiddenField _hdnFieldItemID = row.FindControl("_hdnFieldItemID") as HiddenField;






                        string[] splitresult = _ddItems.SelectedValue.Split(' ');
                        if (splitresult.Length > 1)
                        {
                            _tbUnit.Text = splitresult[1];
                            _hdnFieldItemID.Value = splitresult[0];
                            DataTable dt = dst.Tables["IT2"].Clone();
                            string myIssueHeandName;
                            if (ddlIssueHead.SelectedIndex > 0)
                                myIssueHeandName = ddlIssueHead.SelectedItem.ToString();
                            else
                                myIssueHeandName = lblIssueHeadOld.Text;

                            DataRow[] myOrderNo = dst.Tables["IT2"].Select("Itemname= '" + _ddItems.SelectedItem.ToString() + "' and issueheadname='" + myIssueHeandName + "'");

                            foreach (DataRow dr in myOrderNo)
                            {
                                dt.ImportRow(dr);
                            }

                            DataView view = new DataView(dt);
                            DataTable myRates = view.ToTable(true, "MaxOrderNo");

                            if (myRates.Rows.Count > 0)
                            {

                                int myOrder = Convert.ToInt32(myRates.Rows[0]["MaxOrderNo"].ToString());
                                myOrder = myOrder + 1;
                                tbOrderNO.Text = myOrder.ToString();

                            }
                            else
                            {
                                tbOrderNO.Text = "1";
                            }
                        }
                        else
                        {
                            _tbUnit.Text = "";
                            _hdnFieldItemID.Value = "";

                            return;
                        }




                        break;
                    }
                }
            }
        }
    }
}
