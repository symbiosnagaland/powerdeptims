
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
        
        protected static DataTable dtItems = new DataTable();
        protected static DataSet dst = new DataSet();

        //protected static string ChallanID;
       // protected  decimal ChallanID;
        DataTable data;
        DataTable data2;
        string stDate, edDate;

       SqlConnection con = new SqlConnection(AppConns.GetConnectionString());
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

                   // ChallanID = Request.QueryString["challanid"];

                  //  ChallanID = Convert.ToDecimal  (Request.QueryString["challanid"]);
                    hdnFieldChallanNotoEdit.Value = Request.QueryString["challanid"];
                    //1 based on above oteoid , get the item details to edit
                    GetIssuedItemsDetails(Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));

                    //first get data from db
                    _retriveCheadDivsion();

                    //the items inserted before in save is displayed in gridview using datasource


                    //first keep the items and ihead for gridview read by fetching only once
                    GetIssueHeadsandItemsForDropDowns();
                    //then when rows are created below , in rowdata bound the above fetched values can be used to populate it
                    InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));

                    _btnSave.Visible = _btnCancel.Visible = btnReset.Visible= true;
                }
            }
        }

        #region Get Issued Items Details from DB 
        //1 primary table content
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
                       
                        _tbChallanDate.Text = Convert.ToDateTime(dr["ChallanDate"]).ToString("dd/MM/yyyy");
                        _tbIndentValue.Text = dr["IndentReference"].ToString();
                        _tbIntendDate.Text = Convert.ToDateTime(dr["IndentDate"]).ToString("dd/MM/yyyy");
                       
                       // _ddIntendDivisions.SelectedValue = dr["ChargeableHeadID"].ToString(); instead of using ddl using label to display old data and giving option ddl seperately to change if user wants to
                        lblDivisionOld.Text = dr["IndentingDivisionName"].ToString();
                        
                        lblChHeadOld.Text = dr["ChargeableHeadName"].ToString();
                        _tbtotalAmount.Text = dr["TotalAmount"].ToString();

                        tbVehicleNumberCaps.Text = dr["vehiclenumber"].ToString();
                        tbReceiverDesignation.Text = dr["receiverdesignation"].ToString();
                        tbremarks .Text  = dr["Remarks"].ToString();
                         
                            string isdelivered= dr["IsDeliveredTemporary"].ToString();
                            if (isdelivered == "Yes") //1 is for true
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
            dst = ReceivedItemsLogic.RetrieveReceivedItemsAndReceivedItemsDetails();
            //second table contains the items names                             
            dtItems = dst.Tables[1];
            
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
            try   
            {

                IsssuedItemsLogic akivi = new IsssuedItemsLogic();
                properties issued = new properties();

                decimal orginalChallanID = Convert.ToDecimal(hdnFieldChallanID.Value);

                issued.ChallanID = Convert.ToDecimal(_tbChalanNo.Text);

               // issued.Date = _tbChallanDate.Text;
                issued.Date = DateTime.ParseExact(_tbChallanDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                stDate = DateTime.ParseExact(_tbChallanDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                issued.IndentValue = _tbIndentValue.Text;
                // issued.Date2 = _tbIntendDate.Text;

                issued.Date2 = DateTime.ParseExact(_tbIntendDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                edDate = DateTime.ParseExact(_tbIntendDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //using 2 controls for displaying old value and for givng option to check
                if (_ddIntendDivisions.SelectedIndex > 0)
                    issued.Division = _ddIntendDivisions.Text;
                else
                    issued.Division = lblDivisionOld.Text; //same old value populated

               //same wiht chargeable head value
                if (_ddCHead.SelectedIndex > 0)
                    issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                else
                    issued.ChargeableHeadName = lblChHeadOld.Text;

                issued.IsDeliveredTemporary = istemporary.Checked ? "Yes" : "No";               
                issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);               
                //if data is saved in challan table , save into item table
                StringBuilder sb = new StringBuilder();
                //  sb.Append("<root>");
                issued.VehicleNumber = tbVehicleNumberCaps.Text;
                issued.ReceiverDesignation = tbReceiverDesignation.Text ;
                issued.Remarks = tbremarks.Text;

                string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    //TextBox itemID = gvItems.Rows[i].FindControl("_tbItemID") as TextBox;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    TextBox itemQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    //DropDownList itemIHead = gvItems.Rows[i].FindControl("_ddIhead") as DropDownList;
                    //DropDownList ddlRates = gvItems.Rows[i].FindControl("ddlRates") as DropDownList;

                    HiddenField hdnSelectedIssueHead = gvItems.Rows[i].FindControl("hdnSelectedIssueHead") as HiddenField;
                    HiddenField hdnSelectedRate = gvItems.Rows[i].FindControl("hdnSelectedRate") as HiddenField;     
                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "")
                        sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", hdnSelectedIssueHead.Value).Replace("@QUANTITY", itemQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", hdnSelectedRate.Value));
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    //call the method to save both in primary deliverychallan table and delivery details table
                    akivi.UpdateIssuedItems(issued, orginalChallanID, sb.ToString());
                }
                    //what if user doesn't require to add items but to change primary table details only



                else //else new item(s) not inserted...save only primary table
                {

                    akivi.UpdateIssuedItems(issued, orginalChallanID);
                   // panelError.Visible = true;
                   // lblError.Text = "Error! Select atleast one item to add.";
                   // panelSuccess.Visible = false;

                }
                Session["CHALLANNO"] = _tbChalanNo.Text;
               // Response.Redirect(Request.Url.ToString());
               Response.Redirect("~/Admin/IssuedEntryEdit.aspx?challanid=" + _tbChalanNo.Text);

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
            //if (e.CommandName == "edit")
            //{
            //    GridViewRow gRow;
            //    gRow = (GridViewRow)e.CommandSource;
            //    //TextBox txtAbc= (TextBox)gRow.FindControl("txtAbc");
            //    LinkButton lbtnEdit =(LinkButton)gRow.FindControl("lbtnEdit");
            //    double amount =Convert.ToDouble(lbtnEdit.ToolTip);              
            //}
            //instead of using command name =delete, using amount value
            //else



             if (e.CommandName != "")
            {
                double amount = Convert.ToDouble(e.CommandName);
                string receivedItemID = e.CommandArgument.ToString();
                SqlTransaction tr = null;

                if (Request.QueryString["challanid"] != null)
                {

                }

                    //this will execute first
                    SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM DeliveryItemsDetails WHERE DeliveryItemDetailsID = @DeliveryItemDetailsID";
                cmd.Parameters.AddWithValue("@DeliveryItemDetailsID", receivedItemID);

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "update DeliveryItemsChallan set totalamount = totalamount-" + amount + " where DeliveryItemsChallanID =@DeliveryItemsChallanID";
                cmd2.Parameters.AddWithValue("@DeliveryItemsChallanID", Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));
                try
                {
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
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
                //SqlConnection conn2 = new SqlConnection(AppConns.GetConnectionString());

                //SqlCommand cmd3 = conn2.CreateCommand();
                //cmd3.CommandText = "Select count(*) as count from DeliveryItemsDetails where DeliveryItemsChallanID = @DeliveryItemsChallanID";
                //cmd3.Parameters.AddWithValue("@DeliveryItemsChallanID", ChallanID);
                //    try
                //    {

                //        conn2.Open();
                //        if (cmd3.ExecuteScalar() != DBNull.Value)
                //        {
                //            int count = Convert.ToInt32(cmd3.ExecuteScalar());
                //            if (count < 1)
                //            {
                //                _btnDelete.Visible = true;
                //            }
                //        }
                //        else
                //        {
                //            _btnDelete.Visible = true;
                //        }
                //    }
                //        catch
                //    {
                //            throw;
                //        }
                //    finally
                //    {
                //        conn2.Close();
                //    }
                //}
                GetIssuedItemsDetails(Convert.ToDecimal(hdnFieldChallanNotoEdit.Value));
                gvItems_Edit.DataBind();

            }
        }
        #endregion

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
                        //DropDownList _ddIhead = row.FindControl("_ddIhead") as DropDownList;

                        DropDownList ddlIheadRateActualBalance = row.FindControl("ddlIheadRateActualBalance") as DropDownList;
                        TextBox _tbUnit = row.FindControl("_tbUnit") as TextBox;

                        //   DataTable dt = dst.Tables["ReceivedItemsDetails"].Clone();
                        DataTable dt = dst.Tables["ItemsEnquiryListTable"].Clone();

                        //putting dynamic item enquiry link v2. not needed now
                        //HyperLink hlItemEnquiry = row.FindControl("hlinkItemenquiry") as HyperLink;
                        //string urlEncodedItemName = HttpUtility.UrlEncode(_ddItems.SelectedItem.ToString());
                        //hlItemEnquiry.NavigateUrl = "/Shared/ItemEnquiry.aspx?item=" + urlEncodedItemName;
                        //hlItemEnquiry.Visible = true;
                        //hlItemEnquiry.Text = "item enquiry";
                        //populate new destination table                   
                        //   DataRow[] iheads = dst.Tables["ReceivedItemsDetails"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "'");
                        DataRow[] iheads = dst.Tables["ItemsEnquiryListTable"].Select("itemname= '" + _ddItems.SelectedItem.ToString() + "'");

                        foreach (DataRow dr in iheads)
                        {
                            dt.ImportRow(dr);
                        }
                        DataView view = new DataView(dt);
                        DataTable dtIssueheadRateNetActualBalance = view.ToTable(false, "IssueheadRateNetActualBalance", "Rate");

                        //_ddIhead.DataSource = distinctValues;
                        //_ddIhead.DataTextField = "ISSUEHEADNAME";

                        ddlIheadRateActualBalance.DataSource = dtIssueheadRateNetActualBalance;
                        ddlIheadRateActualBalance.DataTextField = "IssueheadRateNetActualBalance";
                        ddlIheadRateActualBalance.DataValueField = "Rate";
                        ddlIheadRateActualBalance.DataBind();
                        ddlIheadRateActualBalance.Items.Insert(0, new ListItem("Issue Head : Rate : Net Balance", "0"));
                        //when found the right row, then get out of it
                        string[] splitresult = _ddItems.SelectedValue.Split(' ');
                        _tbUnit.Text = splitresult[1];
                        break;
                    }
                }
            }
        }
        #endregion

        /*
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
        */

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
                        hdnSelectedRate.Value = ddlIssueheadRateActualBalance.SelectedValue;
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
            try
            {
                IsssuedItemsLogic akivi = new IsssuedItemsLogic();
                properties issued = new properties();

                decimal orginalChallanID = Convert.ToDecimal(hdnFieldChallanID.Value);

                issued.ChallanID = Convert.ToDecimal(_tbChalanNo.Text);
               // issued.Date = _tbChallanDate.Text;
                issued.Date=DateTime.ParseExact(_tbChallanDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                stDate = DateTime.ParseExact(_tbChallanDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                issued.IndentValue = _tbIndentValue.Text;
               // issued.Date2 = _tbIntendDate.Text;

                issued.Date2=DateTime.ParseExact(_tbIntendDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                edDate = DateTime.ParseExact(_tbIntendDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
              
                if (_ddIntendDivisions.SelectedIndex > 0)
                    issued.Division = _ddIntendDivisions.Text;
                else
                    issued.Division = lblDivisionOld.Text; 

                             if (_ddCHead.SelectedIndex > 0)
                    issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                else
                    issued.ChargeableHeadName = lblChHeadOld.Text;

                issued.IsDeliveredTemporary = istemporary.Checked ? "Yes" : "No";
                issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                issued.ReceiverDesignation = tbReceiverDesignation.Text;
                issued.VehicleNumber = tbVehicleNumberCaps.Text.ToUpper();
                issued.Remarks = tbremarks.Text;

               
                StringBuilder sb = new StringBuilder();
                            string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    TextBox itemQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;

                    //DropDownList itemIHead = gvItems.Rows[i].FindControl("_ddIhead") as DropDownList;
                    //DropDownList ddlRates = gvItems.Rows[i].FindControl("ddlRates") as DropDownList;

                    HiddenField hdnSelectedIssueHead = gvItems.Rows[i].FindControl("hdnSelectedIssueHead") as HiddenField;
                    HiddenField hdnSelectedRate = gvItems.Rows[i].FindControl("hdnSelectedRate") as HiddenField;     


                    if (itemName.SelectedValue.ToString() != "")
                        sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", hdnSelectedIssueHead.Value).Replace("@QUANTITY", itemQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", hdnSelectedRate.Value));
                }
                               if (sb.ToString() != "")
                {
                    akivi.UpdateIssuedItems(issued, orginalChallanID,sb.ToString());
                }
               else 
                {
                    akivi.UpdateIssuedItems(issued, orginalChallanID);                   
                }
            
                Response.Redirect("~/Admin/IssueEntriesList.aspx");

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