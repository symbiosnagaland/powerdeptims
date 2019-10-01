
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
    public partial class IssuedEntryEdit : System.Web.UI.Page
    {
        
   
        string stDate, edDate;
        //
        protected static DataTable dtItems = new DataTable();
        protected static DataTable dtIssueHead = new DataTable();
        protected static DataTable dtRate = new DataTable();

        double totalAmount = 0;
        protected static DataSet dst = new DataSet();
        SqlConnection con = new SqlConnection(AppConns.GetConnectionString());
        DataTable data;
        DataTable data2;

        string str;
        SqlCommand com;
        int count;
        //

     
        protected void Page_Load(object sender, EventArgs e)
        {
            //for loading print page
            if (Session["CHALLANNO"] != null)
            {
                //temp code to display properly in local as well as in hosting environment 
                string appPath = HttpRuntime.AppDomainAppVirtualPath;
                if (appPath != "/")
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(' " + appPath + "/Print/Print.aspx?Id=" + Session["CHALLANNO"].ToString() + "');", true);
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Print/Print.aspx?Id=" + Session["CHALLANNO"].ToString() + "');", true);
                Session["CHALLANNO"] = null;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["challanid"] != null)
                {

                
                    hdnFieldChallanNotoEdit.Value = Request.QueryString["challanid"];
                    //1 based on above oteoid , get the item details to edit
                    GetIssuedItemsDetails(Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));                  
                    _retriveCheadDivsion();

                    GetIssueHeadsandItemsForDropDowns();
                   
                    InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));

                    _btnSave.Visible = _btnCancel.Visible = btnReset.Visible= true;
                }
            }
        }

        #region Get Issued Items Details from DB 



        private void GetIssuedItemsDetails(decimal challanID)  //string challanID
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT IndentReference, IndentDate, ChallanDate, IndentingDivisionName, ChargeableHeadName, TotalAmount, IsDeliveredTemporary, vehiclenumber, receiverdesignation,Remarks FROM            DeliveryItemsChallan where deliveryitemschallanID =@deliveryitemschallanID";
                cmd.Parameters.AddWithValue("@deliveryitemschallanID", challanID);
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read()) //ri.ChargeableHeadName, ri.IssueHeadName,
                    {
                      // hdnFieldChallanID.Value= _tbChalanNo.Text = challanID;
                        hdnFieldChallanID.Value = _tbChalanNo.Text = challanID.ToString();
                       
                        _tbChallanDate.Text = Convert.ToDateTime(dr["ChallanDate"]).ToString("dd-MM-yyyy");
                        _tbIndentValue.Text = dr["IndentReference"].ToString();
                        _tbIntendDate.Text = Convert.ToDateTime(dr["IndentDate"]).ToString("dd-MM-yyyy");
                        lblDivisionOld.Text = dr["IndentingDivisionName"].ToString();
                        lblChHeadOld.Text = dr["ChargeableHeadName"].ToString();
                        totalAmount = Convert.ToDouble(dr["TotalAmount"]);
                        _tbtotalAmount.Text = dr["TotalAmount"].ToString ();
                        tbVehicleNumberCaps.Text = dr["vehiclenumber"].ToString();
                        tbReceiverDesignation.Text = dr["receiverdesignation"].ToString();
                        tbremarks .Text  = dr["Remarks"].ToString();
                            string isdelivered= dr["IsDeliveredTemporary"].ToString();
                            if (isdelivered == "Yes") 
                                istemporary.Checked = true;
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


        #region retrive ch chead and divisions
        private void _retriveCheadDivsion()
        {
            //retrive CHead
            string cmdText2 = "SELECT DISTINCT ChargeableHeadName FROM ChargeableHeads;";
            data2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter(cmdText2, con);
            adapter2.Fill(data2);
            _ddCHead.DataSource = data2;
            _ddCHead.DataTextField = "ChargeableHeadName";
            _ddCHead.DataBind();
            _ddCHead.Items.Insert(0, new ListItem("--Select Chargeable Head--", "0"));

            //retrive divisions
            string cmdText = "SELECT DISTINCT divisionName FROM Divisions";
            //string cmdText = "SELECT * FROM Items;";
            data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
            adapter.Fill(data);
            _ddIntendDivisions.DataSource = data;
            _ddIntendDivisions.DataTextField = "divisionName";
            _ddIntendDivisions.DataBind();
            _ddIntendDivisions.Items.Insert(0, new ListItem("--Select Division Name--", "0"));
        }
#endregion


        #region insert empty rows for issued items
        private void InsertItemsRows(int numberofRows)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("IHead", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            for (int i = 0; i < numberofRows; i++)
            {
                dr = dt.NewRow();
                dr["Item"] = string.Empty;
                dr["Unit"] = string.Empty;
                dr["Quantity"] = string.Empty;
                dr["IHead"] = string.Empty;
                dr["Rate"] = string.Empty;
                dt.Rows.Add(dr);
            }
            // ViewState["TABLE"] = dt;
            gvItems.DataSource = dt;
            gvItems.DataBind();

        }

        #endregion


        private void GetIssueHeadsandItemsForDropDowns()
        {
           // dst = ReceivedItemsLogic.RetrieveReceivedItemsAndReceivedItemsDetails();
            //second table contains the items names                             
           // dtItems = dst.Tables[0];

            dst = IssueNewLogic.RetrieveAllItems();
            //second table contains the items names                             
            dtItems = dst.Tables["Items"];
            dtIssueHead = dst.Tables["itemRatesSecondery"];
            dtRate = dst.Tables["IT2"];

            
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
                    ddItemName.Items.Insert(0, new ListItem("--Select ItemName--", ""));

                }
       
        }
        catch( System.NullReferenceException ) //m forced to do nothing for this exception cos for first item it is giving exception
         {//do noting
        }
            catch(Exception ex)
            {
                              panelError.Visible = lblError.Visible = true;
                lblError.Text = "There was some error in loading the items: " + ex.Message;
                lblError.ToolTip = ex.ToString();
            }
        }

        #region update items
   protected void _btnSave_Click(object sender, EventArgs e)
        {
            if (_tbChalanNo.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan No./Indent number cannot be NULL.";
                panelSuccess.Visible = false;
                return;
            }
            if (_tbIndentValue.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Indent number cannot be NULL.";
                panelSuccess.Visible = false;
                return;
            }
            if (_tbChallanDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan Date cannot be NULL.";
                panelSuccess.Visible = false;

                return;
            }
            if (lblDivisionOld.Text == "--Select Division Name--")
            {
                panelError.Visible = true;
                lblError.Text = "Division Name Should Be Selected.";
                panelSuccess.Visible = false;
                return;
            }
            if (lblChHeadOld.Text == "--Select Chargeable Head--")
            {
                panelError.Visible = true;
                lblError.Text = "ChargeableHead Name Should Be Selected.";
                panelSuccess.Visible = false;
                return;
            }

            try
            {

                
                _Save();

               // double myChallanNo = Convert.ToDouble (_tbChalanNo.Text) - 1;

                Response.Redirect("IssuedItemsDetails.aspx?id=" + _tbChalanNo.Text);              

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }
        #endregion



  
        protected void btnRowsAdd_Click(object sender, EventArgs e)
        {
            InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));
            _btnSave.Visible = _btnCancel.Visible =btnReset.Visible= true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }






        #region delete command option for already inserted items
        /// <summary>
        /// //on row command of the items, mainly now 1. delete specfic row of slected item id in the secondary table
        /// 2. update sum total amount in primary table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
        protected void gvItems_Edit_RowCommand(object sender, GridViewCommandEventArgs e)
        {           
            
             if (e.CommandName != "")
            {
                double amount = Convert.ToDouble(e.CommandName);
                string[] receivedItem = e.CommandArgument.ToString().Split(new char[] { ',' });

                string DeliveryItemDetailsID = receivedItem[0].ToString();
                String ItemName = receivedItem[1].ToString();
                 string oldRate = receivedItem[2].ToString();
                 string oldIssueHeadName = receivedItem[3].ToString();
                 string oldquantity = receivedItem[4].ToString();
                 



                SqlTransaction tr = null;

                if (Request.QueryString["challanid"] != null)
                {

                }

                    //this will execute first
                    SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM DeliveryItemsDetails WHERE DeliveryItemDetailsID = @DeliveryItemDetailsID";
                cmd.Parameters.AddWithValue("@DeliveryItemDetailsID",Convert.ToDecimal (DeliveryItemDetailsID));

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "update DeliveryItemsChallan set totalamount = totalamount -'"+ amount +"' where DeliveryItemsChallanID =@DeliveryItemsChallanID";
                cmd2.Parameters.AddWithValue("@DeliveryItemsChallanID", Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));

                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "update TOP (1) ItemsRateSecondary set quantity = quantity + '" + oldquantity + "' where  ItemName= '" + ItemName + "'and issueheadname='" + oldIssueHeadName + "' and rate='" + oldRate + "' ";

                cmd3.Parameters.AddWithValue("@oldquantity", Convert.ToDecimal(oldquantity));
                cmd3.Parameters.AddWithValue("@ItemName", ItemName);
                cmd3.Parameters.AddWithValue("@oldIssueHeadName", oldIssueHeadName);
                cmd3.Parameters.AddWithValue("@oldRate", Convert.ToDecimal(oldRate));



                try
                {
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;
                    cmd3.Transaction = tr;
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    tr.Commit();
                }
                catch(Exception ex)
                {
                    tr.Rollback();
                    lblError.Text = "An error exception occured!. Please reload the page and try again!";
                    lblError.ToolTip = ex.ToString();
                    lblError.Visible = panelError.Visible = true;
                    //throw;
                }
                finally
                {
                    conn.Close();
                }
                
                GetIssuedItemsDetails(Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));
                gvItems_Edit.DataBind();

            }
        }
        #endregion

        #region on selection of an item inside the gridview
        protected void _ddlIhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 

            //Boolean ans = Convert.ToBoolean(ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "CheckRepetingItems();", true));

            // Boolean ans=Convert .ToBoolean ( Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CheckRepetingItems();", true));

            //string confirmValue = Request.Form["confirm_value"];

            foreach (GridViewRow row in gvItems.Rows)
            {
                //Finding Dropdown control  
                Control ctrl = row.FindControl("ddlIhead") as DropDownList;


                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    //   Comparing ClientID of the dropdown with sender

                    if (ddl.ClientID == ddl1.ClientID)
                    {

                        string selected = ddl.SelectedItem.Value;


                        DropDownList ddlIhead = row.FindControl("ddlIhead") as DropDownList;

                        TextBox tbRate = row.FindControl("tbRate") as TextBox;
                        TextBox tbQty = row.FindControl("tbQty") as TextBox;
                        TextBox tbOrderNO = row.FindControl("tbOrderNO") as TextBox;
                        TextBox tbAmount = row.FindControl("tbAmount") as TextBox;
                        TextBox tbMaxQtyAvail = row.FindControl("tbMaxQtyAvail") as TextBox;



                        TextBox _tbOrderQuantity = row.FindControl("_tbOrderQuantity") as TextBox;
                        TextBox _tbAmt = row.FindControl("_tbAmt") as TextBox;

                        _tbOrderQuantity.Text = "0";
                        _tbAmt.Text = "0";
                        tbRate.Text = "";
                        tbQty.Text = "";
                        tbOrderNO.Text = "";
                        tbAmount.Text = "";
                        tbMaxQtyAvail.Text = "";

                        DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;


                        HiddenField _hdnFieldItemID = row.FindControl("_hdnFieldItemID") as HiddenField;



                        DataTable dt = dst.Tables["IT2"].Clone();
                        DataRow[] rates = dst.Tables["IT2"].Select("ItemName= '" + _ddItems.SelectedItem .ToString () + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

                        DataTable dt2 = dst.Tables["IT3"].Clone();
                        DataRow[] MaxQty = dst.Tables["IT3"].Select("ItemName= '" + _ddItems.SelectedItem.ToString() + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

                        foreach (DataRow dr in MaxQty)
                        {
                            dt2.ImportRow(dr);
                        }
                        DataView v1 = new DataView(dt2);
                        DataTable myMaxQty = v1.ToTable(true, "TotQtyAvailable");

                        foreach (DataRow dr in rates)
                        {
                            dt.ImportRow(dr);
                        }

                        DataView view = new DataView(dt);
                        DataTable myRates = view.ToTable(true, "Rate", "Quantity", "OrderNo", "AMT");

                        if (myRates.Rows.Count > 0)
                        {
                            tbRate.Text = myRates.Rows[0]["Rate"].ToString();
                            tbQty.Text = myRates.Rows[0]["Quantity"].ToString();
                            tbOrderNO.Text = myRates.Rows[0]["OrderNo"].ToString();
                            tbAmount.Text = myRates.Rows[0]["AMT"].ToString();

                            tbMaxQtyAvail.Text = myMaxQty.Rows[0]["TotQtyAvailable"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);
                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                        }
                        break;
                    }
                }
            }
        }

        //end of a function

        protected void _ddItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);


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
                        //ClientID is match so find the Textbox 
                        //control bind it with  dropdown data.                      

                        //retrive item unit
                        string selected = ddl.SelectedItem.Value;
                        DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;

                        DropDownList ddlIhead = row.FindControl("ddlIhead") as DropDownList;

                        TextBox _tbUnit = row.FindControl("_tbUnit") as TextBox;

                        TextBox tbRate = row.FindControl("tbRate") as TextBox;
                        TextBox tbQty = row.FindControl("tbQty") as TextBox;
                        TextBox tbOrderNO = row.FindControl("tbOrderNO") as TextBox;
                        TextBox tbAmount = row.FindControl("tbAmount") as TextBox;

                        TextBox tbMaxQtyAvail = row.FindControl("tbMaxQtyAvail") as TextBox;



                        TextBox _tbOrderQuantity = row.FindControl("_tbOrderQuantity") as TextBox;
                        TextBox _tbAmt = row.FindControl("_tbAmt") as TextBox;


                        HiddenField _hdnFieldItemID = row.FindControl("_hdnFieldItemID") as HiddenField;






                        string[] splitresult = _ddItems.SelectedValue.Split(' ');
                        if (splitresult.Length > 1)
                        {
                            _tbUnit.Text = splitresult[1];
                            _hdnFieldItemID.Value = splitresult[0];
                            DataTable dt = dst.Tables["itemRatesSecondery"].Clone();
                            DataRow[] rates = dst.Tables["itemRatesSecondery"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "'");




                            foreach (DataRow dr in rates)
                            {
                                dt.ImportRow(dr);
                            }

                            DataView view = new DataView(dt);
                            DataTable myIssueHead = view.ToTable(true, "IssueHeadName", "issuableQuantity");

                            ddlIhead.DataSource = myIssueHead;
                            ddlIhead.DataTextField = "issuableQuantity";
                            ddlIhead.DataValueField = "IssueHeadName";
                            ddlIhead.DataBind();
                            ddlIhead.Items.Insert(0, new ListItem("--Select Issue Heads--", "0"));

                            tbRate.Text = "";
                            tbQty.Text = "";
                            tbOrderNO.Text = "";
                            tbAmount.Text = "";
                            _tbOrderQuantity.Text = "0";
                            _tbAmt.Text = "0";
                            tbMaxQtyAvail.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                        }
                        else
                        {
                            _tbUnit.Text = "";
                            _hdnFieldItemID.Value = "";

                            tbRate.Text = "";
                            tbQty.Text = "";
                            tbOrderNO.Text = "";
                            tbAmount.Text = "";
                            _tbOrderQuantity.Text = "";
                            _tbAmt.Text = "";
                            tbMaxQtyAvail.Text = "";

                            ddlIhead.Items.Clear();
                            ddlIhead.Items.Insert(0, new ListItem("--Select Issue Heads--", "0"));

                            _tbOrderQuantity.Text = "";

                            _tbAmt.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                            return;
                        }
                        break;
                    }
                }
            }
        }

        //end of Function

        protected void OnTextChanged(object sender, EventArgs e)
        {
            int a = 5;

        }
       
        #endregion


        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlIheadRateActualBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 
            //of the Grid from where the SelectedIndex change event is fired.
            foreach (GridViewRow row in gvItems.Rows)
            {
                //Finding Dropdown control  
                Control ctrl = row.FindControl("ddlIheadRateActualBalance") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddlIssueheadRateActualBalance = (DropDownList)ctrl;
                    //   Comparing ClientID of the dropdown with sender
                    if (ddl.ClientID == ddlIssueheadRateActualBalance.ClientID)
                    {  
                        DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;
                        //    DropDownList ddlRates = row.FindControl("ddlRates") as DropDownList;
                        //  Label lblRate = row.FindControl("lblRate") as Label;

                        HiddenField hdnSelectedIssueHead = row.FindControl("hdnSelectedIssueHead") as HiddenField;
                        HiddenField hdnSelectedRate = row.FindControl("hdnSelectedRate") as HiddenField;
                       // TextBox _tbQuantity = row.FindControl("_tbQuantity") as TextBox;   
                       // hdnSelectedRate.Value = ddlIssueheadRateActualBalance.SelectedValue;
                        hdnSelectedRate.Value = ddlIssueheadRateActualBalance.SelectedItem.ToString().Split(':')[1].Trim();



                        hdnSelectedIssueHead.Value = ddlIssueheadRateActualBalance.SelectedItem.ToString().Split(':')[0].Trim();

                    }
                }
            }
        }
        #endregion
        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void _btnUpdate_Click(object sender, EventArgs e)
        {
            if (_tbChalanNo.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan No./Indent number cannot be NULL.";
                panelSuccess.Visible = false;
                return;
            }
            if (_tbIndentValue.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Indent number cannot be NULL.";
                panelSuccess.Visible = false;
                return;
            }
            if (_tbChallanDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan Date cannot be NULL.";
                panelSuccess.Visible = false;

                return;
            }
            //DateTime ChallanDate = Convert.ToDateTime(_tbChallanDate.Text);
            //DateTime IndentDate = Convert.ToDateTime(_tbIntendDate.Text);

            //if (ChallanDate < IndentDate)
            //{
            //    panelError.Visible = true;
            //    lblError.Text = "Challan date Should be Greater or Equal to Indent Date.";
            //    panelSuccess.Visible = false;
            //    _tbChallanDate.Style.Add("background", "Pink");
            //    _tbChallanDate.Focus();
            //    return;
            //}
            //else
            //{
            //    _tbChallanDate.Style.Add("background", "White");
            //}


            if (lblDivisionOld.Text  == "--Select Division Name--")
            {
                panelError.Visible = true;
                lblError.Text = "Division Name Should Be Selected.";
                panelSuccess.Visible = false;
                return;
            }
            if (lblChHeadOld.Text  == "--Select Chargeable Head--")
            {
                panelError.Visible = true;
                lblError.Text = "ChargeableHead Name Should Be Selected.";
                panelSuccess.Visible = false;
                return;
            }

            try
            {

              
                    _Save();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }

        private void _Save()
        {
            NewProperties issued = new NewProperties();
            IssueNewLogic enterSave = new IssueNewLogic();
            try
            {

                issued.challanNO = Convert.ToDouble(_tbChalanNo.Text);
       
                issued.indentNo = _tbIndentValue.Text;

                // issued.challanDate = DateTime.ParseExact(, "dd-MM-yyyy", null).ToString("MM-dd-yyyy");
                // issued.indentDate = DateTime.ParseExact(_tbIntendDate.Text, "dd-MM-yyyy", null).ToString("MM-dd-yyyy");

                if(_tbChallanDate.Text.Contains("/"))
                issued.challanDate = DateTime.ParseExact(_tbChallanDate.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
               else
                    issued.challanDate = DateTime.ParseExact(_tbChallanDate.Text, @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                if (_tbIntendDate.Text.Contains("/"))
                    issued.indentDate = DateTime.ParseExact(_tbIntendDate.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                else
                issued.indentDate = DateTime.ParseExact(_tbIntendDate.Text, @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                issued.TotalAmount = Convert .ToDouble(_tbtotalAmount.Text);



                //stDate = DateTime.ParseExact(RecievedItemsOrderObject.Date, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
               // edDate = DateTime.ParseExact(RecievedItemsOrderObject.SupplyDate, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");



                if (_ddIntendDivisions.SelectedItem.ToString() != "--Select Division Name--")
                {
                    issued.intendingDivision = _ddIntendDivisions.SelectedItem.ToString();
                }
                else
                {
                    issued.intendingDivision = lblDivisionOld.Text;
                }


                if (_ddCHead.SelectedItem.ToString() != "--Select Chargeable Head--")
                {
                    issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                }
                else
                {
                    issued.ChargeableHeadName = lblChHeadOld.Text;
                }
                

                issued.IsDeliveredTemporary = istemporary.Checked ? "Yes" : "No";
                issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                issued.ReceiverDesignation = tbReceiverDesignation.Text;
                issued.VehicleNumber = tbVehicleNumberCaps.Text.ToUpper();
                issued.Remarks = tbremarks.Text ;

                StringBuilder sb = new StringBuilder();
                string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,itemid,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                string updateItemRateSecondary = "update ItemsRateSecondary set quantity=quantity-'@QUANTITY' where itemname='@itemname' and issueHeadName=  '@IssueHeadName' and OrderNO='@OrderNO'";


                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    DropDownList _ddItems = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;

                    TextBox tbQty = gvItems.Rows[i].FindControl("tbQty") as TextBox;

                    TextBox _tbOrderQuantity = gvItems.Rows[i].FindControl("_tbOrderQuantity") as TextBox;
                    DropDownList ddlIhead = gvItems.Rows[i].FindControl("ddlIhead") as DropDownList;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox _tbAmt = gvItems.Rows[i].FindControl("_tbAmt") as TextBox;
                    HiddenField _hdnFieldItemID = gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField;
                    TextBox tbOrderNO = gvItems.Rows[i].FindControl("tbOrderNO") as TextBox;





                    if (_ddItems.SelectedValue.ToString() != "")
                    {
                        //Checking the quantity is null in quantity
                        if (_tbOrderQuantity.Text == "")
                        {
                            panelError.Visible = true;
                            lblError.Text = "Error! Quantity Cannot be Blank.";
                            panelSuccess.Visible = false;
                            return;
                        }

                        if (tbRate.Text != "")
                        {

                            totalAmount += Convert.ToDouble(_tbOrderQuantity.Text) * Convert.ToDouble(tbRate.Text);

                            if (Convert.ToDouble(_tbOrderQuantity.Text) > Convert.ToDouble(tbQty.Text))
                            {
                                DataTable dt = dst.Tables["IT2"].Clone();
                                DataRow[] rates = dst.Tables["IT2"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

                                Double OrderedQty = Convert.ToDouble(_tbOrderQuantity.Text);
                                double QtyAvailableinRows = Convert.ToDouble(tbQty.Text);

                                foreach (DataRow dr in rates)
                                {
                                    dt.ImportRow(dr);
                                }
                                DataView view = new DataView(dt);
                                DataTable myRates = view.ToTable(true, "Rate", "Quantity", "OrderNo", "AMT");

                                int counter = 0;
                                Double tempQty;
                                Double tempOrderNo;
                                Double tempRate;


                                while (OrderedQty > 0)
                                {
                                    tempQty = Convert.ToDouble(myRates.Rows[counter]["Quantity"]);
                                    tempOrderNo = Convert.ToDouble(myRates.Rows[counter]["OrderNo"]);
                                    tempRate = Convert.ToDouble(myRates.Rows[counter]["Rate"]);

                                    if (OrderedQty < tempQty)
                                    {
                                        tempQty = OrderedQty;
                                    }

                                    sb.Append(updateItemRateSecondary.Replace("@itemname", _ddItems.SelectedItem.ToString()).Replace("@QUANTITY", tempQty.ToString()).Replace("@IssueHeadName", ddlIhead.SelectedValue).Replace("@OrderNO", tempOrderNo.ToString()));

                                    sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemID", _hdnFieldItemID.Value).Replace("@ItemName", Utilities.ValidSql(_ddItems.SelectedItem.ToString())).Replace("@IssueHeadName", ddlIhead.SelectedValue).Replace("@QUANTITY", tempQty.ToString()).Replace("@UNIT", itemUnit.Text).Replace("@RATE", tempRate.ToString()));

                                    OrderedQty = OrderedQty - tempQty;
                                    counter++;
                                }
                            }
                            else
                            {
                                sb.Append(updateItemRateSecondary.Replace("@itemname", _ddItems.SelectedItem.ToString()).Replace("@QUANTITY", _tbOrderQuantity.Text).Replace("@IssueHeadName", ddlIhead.SelectedValue).Replace("@OrderNO", tbOrderNO.Text));
                                sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemID", _hdnFieldItemID.Value).Replace("@ItemName", Utilities.ValidSql(_ddItems.SelectedItem.ToString())).Replace("@IssueHeadName", ddlIhead.SelectedValue).Replace("@QUANTITY", _tbOrderQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", tbRate.Text));

                            }

                        }
                        else
                        {

                            panelError.Visible = true;
                            lblError.Text = "Error! One of item's Issue Head and Rate is not selected.";
                            panelSuccess.Visible = false;
                            return;
                        }

                    }
                }
                issued.TotalAmount += totalAmount;
          
                    //call the method to save both in primary deliverychallan table and delivery details table

                    enterSave.UpdateNewIssuedItems(issued, sb.ToString());


                    lblSuccess.Text = "Challan number " + _tbChalanNo.Text + " details Updated successfully!";

                    GetIssueHeadsandItemsForDropDowns();


                   // _tbChalanNo.Text = (Convert.ToDouble(_tbChalanNo.Text) + 1).ToString();

                   // panelError.Visible = false;
                //  panelSuccess.Visible = true; 
                Response.Redirect("/Admin/IssueEntriesList.aspx");               

            }

            catch (System.Threading.ThreadAbortException)
            {
                

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! The challan ID already exists. Please use another ID";
                    panelSuccess.Visible = false;

                }
                else
                {
                    Session["ERRORMSG"] = ex.ToString();
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("Error.aspx", true);



            }
        }

        protected void _btnDelete_Click(object sender, EventArgs e)
        {
            decimal index = Convert.ToDecimal(Request.QueryString["challanid"]);             
                    SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
                    
                    SqlCommand cmd1 = conn.CreateCommand();
                    cmd1.CommandText = "Select count(*) as count from DeliveryItemsDetails where DeliveryItemsChallanID = @DeliveryItemsChallanID";
                    cmd1.Parameters.AddWithValue("@DeliveryItemsChallanID", index);

                    SqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandText = "DELETE FROM [DeliveryItemsChallan]  WHERE [DeliveryItemsChallanID] = @DeliveryItemsChallanID";
                    cmd2.Parameters.AddWithValue("@DeliveryItemsChallanID", index);

                    conn.Open();
              
                 int count  = Convert.ToInt32(cmd1.ExecuteScalar());

                 if (count < 1)
                 {
                     cmd2.ExecuteNonQuery();
                     conn.Close();
                     Response.Redirect("IssueEntriesList.aspx");
                 }
        }
    }
}