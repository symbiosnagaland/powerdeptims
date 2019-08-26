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



        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (Request.QueryString["oteoid"] != null)
                {
                    OTEOID = Request.QueryString["oteoid"];
                    //based on above oteoid , get the item details to edit
                    GetRecievedItemsDetails(OTEOID);

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
                int ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);//new 
                RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);
                RecievedItemsOrderObject.Date  = tbOTEODate.Text;
                RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
                RecievedItemsOrderObject.Date2  = tbSupplyDate.Text;
                RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
               
                
               // RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
               // RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                if (ddlChargeableHead.SelectedIndex > 0)
                    RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                else
                    RecievedItemsOrderObject.ChargeableHeadName = lblChargeableHead.Text; //same old value populated

                if (ddlIssueHead.SelectedIndex > 0)
                    RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                else
                    RecievedItemsOrderObject.IssueHeadName = lblIssueHeadOld.Text; //same old value populated

              
               
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control
                StringBuilder sb = new StringBuilder();
                //  sb.Append("<root>");
                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

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
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES

                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "0")
                        sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));
                }
                //now save it to db
                if (sb.ToString() != "")
                {
                    //getting total amount from footer
                    TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);   
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID,sb.ToString()); 
                }
                else //no new items received so, can save primary table only
                {
                    //getting total amount from single label
                    
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmountAddedItems.Text);
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, ReceivedItemsOTEOID);  //originalOTEOID 
                 
                }
                Session["OTEONUMBER"] = tbOtEONumber.Text;
              // Response.Redirect(Request.Url.ToString());
                 Response.Redirect("~/Admin/ReceivedItemsEdit.aspx?Id="+tbOtEONumber.Text);
                
                //temp code to display properly in local as well as in hosting environment 
                //string appPath = HttpRuntime.AppDomainAppVirtualPath;
                //if (appPath != "/")
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(' " + appPath + "/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
                //else
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
                //Session["OTEONUMBER"] = null;

               


               

            }
            //catch (System.Threading.ThreadAbortException)
            //{
            //    //do nothing 
            //}
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
                        hdnFieldOTEOID.Value=  tbOtEONumber.Text = oteoID;                       
                        tbOTEODate.Text = Convert.ToDateTime(dr["ReceivedItemOTEODate"]).ToString("dd/MM/yyyy");
                        tbSupplyOrderReference.Text = dr["SupplyOrderReference"].ToString();
                        tbSupplyDate.Text = Convert.ToDateTime(dr["SupplyOrderDate"]).ToString("dd/MM/yyyy");
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
            if(e.CommandName !="")
            {                
                //Label lblAmount = (Label) gvItems_Edit.FindControl(e.
                double amount = Convert.ToDouble(e.CommandName);
            string[] MyArraryOfCommand= e.CommandArgument.ToString().Split(new char[] { ',' });

            string receivedItemID = MyArraryOfCommand[0];
            string ItemID = MyArraryOfCommand[1];

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM [ReceivedItemsDetails] WHERE [ReceivedItemID] = @ReceivedItemID";
            cmd.Parameters.AddWithValue("@ReceivedItemID", receivedItemID);
            
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = "update ReceivedItemsOTEO set totalamount = totalamount-"+amount + " where receiveditemsoteoid =@receiveditemsoteoid";
                //cmd2.Parameters.AddWithValue("@amount",);
            cmd2.Parameters.AddWithValue("@receiveditemsoteoid", OTEOID);


            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = "SELECT COUNT (*) FROM ReceivedItemsDetails INNER JOIN ReceivedItemsOTEO ON ReceivedItemsDetails.ReceivedItemsOTEOID = ReceivedItemsOTEO.ReceivedItemsOTEOID WHERE (ReceivedItemsDetails.ReceivedItemsOTEOID = @ReceivedItemsOTEOID)";
            cmd3.Parameters.AddWithValue("@ReceivedItemsOTEOID", OTEOID);

            SqlCommand cmd4 = conn.CreateCommand();
            cmd4.CommandText = "DELETE FROM [Itemsratesecondary] WHERE [ItemID] = @ItemID and OTEO=@receiveditemsoteoid";
            cmd4.Parameters.AddWithValue("@ItemID", ItemID);
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
                    cmd4.ExecuteNonQuery();
                    tr.Commit();
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
            string OTEONO = tbOtEONumber.Text;
            RecievedItemsOrderObject.Date  = tbOTEODate.Text;
            RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
            RecievedItemsOrderObject.SupplyDate = tbSupplyDate.Text;
            RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
            string IssueHeadName;
                 
               // RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
               // RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                if (ddlChargeableHead.SelectedIndex > 0)
                    RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                else
                    RecievedItemsOrderObject.ChargeableHeadName = lblChargeableHead.Text; //same old value populated

                if (ddlIssueHead.SelectedIndex > 0)
                {
                    RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                    IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                }
                else
                { 
                    RecievedItemsOrderObject.IssueHeadName = lblIssueHeadOld.Text; //same old value populated
                    IssueHeadName = lblIssueHeadOld.Text; 
                }

              
               
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control
                StringBuilder sb = new StringBuilder();
                //  sb.Append("<root>");
                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRATESecondary = "INSERT INTO ItemsRateSecondary(ItemId,IssueHeadName, Rate,OrderNo,Quantity,OTEO) values('@ITEMID','@ISSUEHEAD', '@RATE','@ORDERno','@QUANTITY','@OTEO')";

                string insertRATEMaster = "INSERT INTO ItemsRateMaster(ItemId,IssueHeadName, Rate,MaxOrderNo,Quantity,OTEO) values('@ITEMID','@ISSUEHEAD', '@RATE','@ORDERno','@QUANTITY','@OTEO')";

                string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@ORDERno' where itemid='@ITEMID'";


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
                    TextBox tbOrderNO = gvItems.Rows[i].FindControl("tbOrderNO") as TextBox;
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES

                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "0")
                    {
                        sb.Append(insertStatement.Replace("@RECEIVEDITEMSOTEOID", tbOtEONumber.Text).Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tbQuantity.Text).Replace("@UNIT", _tbUnit.Text).Replace("@RATE", tbRate.Text).Replace("@AMOUNT", tbAmount.Text));
                        sb.Append(insertRATESecondary.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ISSUEHEAD", IssueHeadName).Replace("@RATE", tbRate.Text).Replace("@ORDERno", tbOrderNO.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@OTEO", tbOtEONumber.Text));

                        if(tbOrderNO.Text =="1")
                        {
                            sb.Append(insertRATEMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ISSUEHEAD", IssueHeadName).Replace("@RATE", tbRate.Text).Replace("@ORDERno", tbOrderNO.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@OTEO", tbOtEONumber.Text));
                        }
                        else
                        {
                            sb.Append(UpdateRateMaster.Replace("@ITEMID", hdnFieldItemID.Value).Replace("@ORDERno", tbOrderNO.Text));
                         }                        
                    }
                }
                //now save it to db
                if (sb.ToString() != "")
                {
                    //getting total amount from footer
                    TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);   
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID,sb.ToString()); 
                }
                else //no new items received so, can save primary table only
                {
                    //getting total amount from single label
                    
                    RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmountAddedItems.Text);
                    ameh.UpdateReceivedItemsDetails(RecievedItemsOrderObject, originalOTEOID);  //originalOTEOID 
                 
                }
               // Session["OTEONUMBER"] = tbOtEONumber.Text;
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
}
}
