using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS_PowerDept.AppCode;
using System.Web;
//using IMS_PowerDept.Dataset_Report;

namespace IMS_PowerDept.UserControls
{

    public partial class IssueEntry : System.Web.UI.UserControl
    {
        protected static DataTable dtItems = new DataTable();
        static double totalAmount = 0;
        protected static DataSet dst = new DataSet();
        SqlConnection con = new SqlConnection(AppConns.GetConnectionString());
        DataTable data;
        DataTable data2;

        //for ChallanID
        string str;
        SqlCommand com;
        int count;
        // DataSet_All ReceivedItemsTables = new DataSet_All();
        protected void Page_Load(object sender, EventArgs e)
        {
            //_tbChallanDate.Text = System.DateTime.Today.ToShortDateString();
            //_tbIntendDate.Text = System.DateTime.Today.ToShortDateString();

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

            if (!Page.IsPostBack)
            {
                _RetriveDelliveryChallanID();
                _retriveCheadDivsion();
                //first keep the items and ihead for gridview read by fetching only once
                GetIssueHeadsandItemsForDropDowns();
                //then when rows are created below , in rowdata bound the above fetched values can be used to populate it
                InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));

            }
        }

        //auto increament Challn No - taking refrence from database
        private void _RetriveDelliveryChallanID()
        {

            str = "select max(deliveryitemschallanid) from DeliveryItemsChallan";
            com = new SqlCommand(str, con);
            con.Open();
            if (com.ExecuteScalar() != DBNull.Value)
                count = Convert.ToInt32(com.ExecuteScalar()) + 1;
            else
                count = 1;
            _tbChalanNo.Text =count.ToString();
            con.Close();
        }



        //insert row in gridview 
        private void InsertEmptyRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("IHead", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            for (int i = 0; i < 10; i++)
            {
                dr = dt.NewRow();
                dr["Item"] = string.Empty;
                dr["Unit"] = string.Empty;
                dr["Quantity"] = string.Empty;
                dr["IHead"] = string.Empty;
                dr["Rate"] = string.Empty;
                dt.Rows.Add(dr);
            }
            ViewState["TABLE"] = dt;
            gvItems.DataSource = dt;
            gvItems.DataBind();

            _btnSave.Visible = btnReset.Visible = true;
        }

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

        private void addnewrow()
        {
            int rowIndex = 0;
            if (ViewState["TABLE"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TABLE"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //  extract the TextBox values
                        DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
                        TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
                        TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbQuantity");
                        DropDownList ihead = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("_ddIhead");
                        DropDownList ddlRates = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("ddlRates");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Item"] = _ddItems.Text;
                        dtCurrentTable.Rows[i - 1]["Unit"] = _tbUnit.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = _tbQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["IHead"] = ihead.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = ddlRates.Text;
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
                    ViewState["TABLE"] = dtCurrentTable;
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
            if (ViewState["TABLE"] != null)
            {
                DataTable dt = (DataTable)ViewState["TABLE"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
                        TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
                        TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbQuantity");
                        DropDownList ihead = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("_ddIhead");
                        DropDownList ddlRates = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("ddlRates");
                        _ddItems.Text = dt.Rows[i]["Item"].ToString();
                        _tbUnit.Text = dt.Rows[i]["Unit"].ToString();
                        _tbQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        //not populating as list item is not there now data is not there now
                        ihead.Text = dt.Rows[i]["IHead"].ToString();
                        ddlRates.Text = dt.Rows[i]["Rate"].ToString();
                        rowIndex++;

                    }

                    //  TextBox tbtotalAmount2 = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;


                    //  string totalAMount = (string) ViewState["TOTALAMOUNT"];
                    //  tbtotalAmount2.Text = totalAmount;
                }

            }
        }

        //retrive chead and divisions
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

        //method for saving all the data

        private void _SaveDeliveryItemDetails()
        {
            try
            {

                IsssuedItemsLogic akivi = new IsssuedItemsLogic();
                properties issued = new properties();

                issued.ChallanID = Convert.ToDecimal(_tbChalanNo.Text);
                issued.Date = _tbChallanDate.Text;
                issued.IndentValue = _tbIndentValue.Text;
                issued.Date2 = _tbIntendDate.Text;
                issued.Division = _ddIntendDivisions.Text;
                issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                issued.IsDeliveredTemporary = istemporary.Checked ? "Yes" : "No";
                // Session["USERID"] = 1;//setting user id temp here
                issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //  TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                // issued.TotalAmount = Convert.ToDouble(tbtotalAmount.Text); 
                // issued.TotalAmount = Convert.ToDouble(_tbtotalAmount.Text); 

                //if data is saved in challan table , save into item table
                StringBuilder sb = new StringBuilder();
                //  sb.Append("<root>");
                string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    //TextBox itemID = gvItems.Rows[i].FindControl("_tbItemID") as TextBox;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    TextBox itemQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    DropDownList itemIHead = gvItems.Rows[i].FindControl("_ddIhead") as DropDownList;
                    DropDownList ddlRates = gvItems.Rows[i].FindControl("ddlRates") as DropDownList;
                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "")
                    {
                        sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", itemIHead.SelectedItem.ToString()).Replace("@QUANTITY", itemQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", ddlRates.Text));
                        //saving total amount
                        totalAmount += Convert.ToDouble(itemQuantity.Text) * Convert.ToDouble(ddlRates.SelectedValue);
                    }
                }
                issued.TotalAmount = totalAmount;
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    //call the method to save both in primary deliverychallan table and delivery details table
                    akivi.SaveIssuedItems(issued, sb.ToString());
                    Session["CHALLANNO"] = _tbChalanNo.Text;
                    Response.Redirect(Request.Url.ToString());


                }


                else
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Select atleast one item to add.";
                    panelSuccess.Visible = false;

                }

            }

            catch (System.Threading.ThreadAbortException)
            {
                //do nothing

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! The challan ID already exists. Please use another ID";
                    panelSuccess.Visible = false;
                    // tbOtEONumber.BorderColor = System.Drawing.Color.Red;
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

        //save as temp items

        private void _saveitemastemporary()
        {
            try
            {

            }
            catch (System.Threading.ThreadAbortException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }
        //get max Challan ID
        private void _MaxChallanID()
        {
        }
        private void GetIssueHeadsandItemsForDropDowns()
        {
            // DataSet dst = new DataSet();
            dst = ReceivedItemsLogic.RetrieveReceivedItemsAndReceivedItemsDetails();
            //second table contains the items names                             
            dtItems = dst.Tables[1];
        }

        #region on selection of an item inside the gridview
        protected void _ddItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 
            //of the Grid from where the SelectedIndex change event is fired.

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
                        DropDownList _ddIhead = row.FindControl("_ddIhead") as DropDownList;
                        TextBox _tbUnit = row.FindControl("_tbUnit") as TextBox;
                        DataTable dt = dst.Tables["ReceivedItemsDetails"].Clone();
                        //putting dynamic item enquiry link
                        HyperLink hlItemEnquiry = row.FindControl("hlinkItemenquiry") as HyperLink;
                        string urlEncodedItemName = HttpUtility.UrlEncode(_ddItems.SelectedItem.ToString());
                        hlItemEnquiry.NavigateUrl = "/Shared/ItemEnquiry.aspx?item=" + urlEncodedItemName;
                        hlItemEnquiry.Visible = true;
                        hlItemEnquiry.Text = "item enquiry";
                        //populate new destination table                   
                        DataRow[] iheads = dst.Tables["ReceivedItemsDetails"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "'");
                        foreach (DataRow dr in iheads)
                        {
                            // if(iheads.Length !=0)
                            dt.ImportRow(dr);
                        }
                        DataView view = new DataView(dt);
                        DataTable distinctValues = view.ToTable(true, "ISSUEHEADNAME");

                        _ddIhead.DataSource = distinctValues;
                        _ddIhead.DataTextField = "ISSUEHEADNAME";
                        _ddIhead.DataBind();
                        _ddIhead.Items.Insert(0, new ListItem("--Select Issue Head--", "0"));
                        //when found the right row, then get out of it

                        string[] splitresult = _ddItems.SelectedValue.Split(' ');
                        _tbUnit.Text = splitresult[1];
                        break;
                    }
                }
            }
        }
        #endregion


        #region on selection of ihead inside the gridview
        /// <summary>
        /// when ihead is selected from a row of data grid then this event is fired and the rates specific to it are populated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _ddIhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Casting sender to Dropdown
            DropDownList ddl = sender as DropDownList;
            //Looping through each Gridview row to find exact Row 
            //of the Grid from where the SelectedIndex change event is fired.
            foreach (GridViewRow row in gvItems.Rows)
            {
                //Finding Dropdown control  
                Control ctrl = row.FindControl("_ddIhead") as DropDownList;

                if (ctrl != null)
                {
                    DropDownList ddlIssuehead = (DropDownList)ctrl;
                    //   Comparing ClientID of the dropdown with sender
                    if (ddl.ClientID == ddlIssuehead.ClientID)
                    {
                        //ClientID is match so find the Textbox 
                        //control bind it with  dropdown data.
                        // TextBox txt = row.FindControl("_tbUnit") as TextBox;

                        //retrive item unit

                        DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;
                        DropDownList ddlRates = row.FindControl("ddlRates") as DropDownList;


                        DataTable dt = dst.Tables["ReceivedItemsDetails"].Clone();
                        //populate new destination table 
                        DataRow[] rates = dst.Tables["ReceivedItemsDetails"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "' and issueheadname= '" + ddlIssuehead.SelectedItem.ToString() + "'");
                        foreach (DataRow dr in rates)
                        {
                            dt.ImportRow(dr);
                        }
                        DataView view = new DataView(dt);
                        DataTable distinctValues = view.ToTable(true, "Rate");

                        ddlRates.DataSource = distinctValues;
                        ddlRates.DataTextField = "Rate";
                        ddlRates.DataBind();
                        ddlRates.Items.Insert(0, new ListItem("--Select Rate--", "0"));
                        //when found the right row, then get out of it
                        //break;
                    }
                }
            }
        }
        #endregion


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
                    ddItemName.Items.Insert(0, new ListItem("--Select Item Name--", ""));
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error.')", true);
            }

        }

        protected void ButtonAddnewRows_Click(object sender, EventArgs e)
        {
            addnewrow();
        }

        protected void _btnSave_Click(object sender, EventArgs e)
        {
            //checking in there is any null value b4 saving 2 challan table
            if (_tbChalanNo.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan No./Indent number cannot be NULL.";
                return;
            }
            if (_tbIndentValue.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Indent number cannot be NULL.";
                return;
            }
            //_tbChallanDate _ddIntendDivisions _ddCHead
            if (_tbChallanDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan Date cannot be NULL.";
                return;
            }
            if (_ddIntendDivisions.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Division Name cannot be NULL.";
                return;
            }
            if (_ddCHead.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "ChargeableHead Name cannot be NULL.";
                return;
            }

            try
            {

                con.Open();
                SqlCommand cmdd = new SqlCommand("select * from DeliveryItemsChallan where DeliveryItemsChallanID = @DeliveryItemsChallanID", con);
                SqlParameter param = new SqlParameter();
                //SqlParameter param1 = new SqlParameter();
                param.ParameterName = "@DeliveryItemsChallanID";
                param.Value = _tbChalanNo.Text;
                cmdd.Parameters.Add(param);
                //cmdd.Parameters.Add(param1);
                SqlDataReader reader = cmdd.ExecuteReader();
                if (reader.HasRows)
                {
                    panelError.Visible = true;
                    lblError.Text = "This Delivery ItemsChallanID already exists.Please choose another ID.";

                    reader.Close();
                    con.Close();
                    return;
                }
                else
                {
                    reader.Close();
                    con.Close();
                    _SaveDeliveryItemDetails();


                }


            }
            catch (System.Threading.ThreadAbortException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }

        protected void btnRowsAdd_Click(object sender, EventArgs e)
        {
            InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));


            //not enabling for now below cos it creates a new css which removes the default css. fix this and will use below line of code 
            //btnRowsAdd.Enabled = false;
        }

        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Save_Click(object sender, EventArgs e)
        {

            if (_tbChalanNo.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan No./Indent number cannot be NULL.";
                return;
            }
            if (_tbIndentValue.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Indent number cannot be NULL.";
                return;
            }
            if (_tbChallanDate.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan Date cannot be NULL.";
                return;
            }
            if (_ddIntendDivisions.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Division Name cannot be NULL.";
                return;
            }
            if (_ddCHead.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "ChargeableHead Name cannot be NULL.";
                return;
            }

            try
            {

                con.Open();
                SqlCommand cmdd = new SqlCommand("select * from DeliveryItemsChallan where DeliveryItemsChallanID = @DeliveryItemsChallanID", con);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@DeliveryItemsChallanID";
                param.Value = _tbChalanNo.Text;
                cmdd.Parameters.Add(param);
                SqlDataReader reader = cmdd.ExecuteReader();
                if (reader.HasRows)
                {
                    panelError.Visible = true;
                    lblError.Text = "This Delivery ItemsChallanID already exists.Please choose another ID.";

                    reader.Close();
                    con.Close();
                    return;
                }
                else
                {
                    reader.Close();
                    con.Close();
                    _Save();


                }


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
            try
            {

                IsssuedItemsLogic akivi = new IsssuedItemsLogic();
                properties issued = new properties();

                issued.ChallanID = Convert.ToDecimal(_tbChalanNo.Text);
                issued.Date = _tbChallanDate.Text;
                issued.IndentValue = _tbIndentValue.Text;
                issued.Date2 = _tbIntendDate.Text;
                issued.Division = _ddIntendDivisions.Text;
                issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                issued.IsDeliveredTemporary = istemporary.Checked ? "Yes" : "No";
                issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);

                StringBuilder sb = new StringBuilder();
                string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    TextBox itemQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    DropDownList itemIHead = gvItems.Rows[i].FindControl("_ddIhead") as DropDownList;
                    DropDownList ddlRates = gvItems.Rows[i].FindControl("ddlRates") as DropDownList;

                    if (itemName.SelectedValue.ToString() != "")
                    {
                        sb.Append(insertStatement.Replace("@DeliveryItemsChallanID",_tbChalanNo.Text).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", itemIHead.SelectedItem.ToString()).Replace("@QUANTITY", itemQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", ddlRates.Text));
                        totalAmount += Convert.ToDouble(itemQuantity.Text) * Convert.ToDouble(ddlRates.SelectedValue);
                    }
                }
                issued.TotalAmount = totalAmount;
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    // //call the method to save both in primary deliverychallan table and delivery details table
                    akivi.SaveIssuedItems(issued, sb.ToString());
                    //Session["CHALLANNO"] = _tbChalanNo.Text;
                    _tbChalanNo.Text = (Convert.ToDouble(_tbChalanNo.Text) + 1).ToString();
                    //to continue with same
                 //   Response.Redirect("IssueEntriesList.aspx");


                }


                else
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Select atleast one item to add.";
                    panelSuccess.Visible = false;

                }

            }

            catch (System.Threading.ThreadAbortException)
            {
                //do nothing

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! The challan ID already exists. Please use another ID";
                    panelSuccess.Visible = false;
                    // tbOtEONumber.BorderColor = System.Drawing.Color.Red;
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

    }
}