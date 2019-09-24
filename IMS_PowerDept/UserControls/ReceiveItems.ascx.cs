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
        string CheckExistingItem;
       
        protected static DataTable dtIssueHead = new DataTable();
        protected static DataTable dtRate = new DataTable();

        double totalAmount = 0;
        protected static DataSet dst = new DataSet();
        SqlConnection con = new SqlConnection(AppConns.GetConnectionString());
        DataTable data;
        DataTable data2;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            /*for the purpose of opening new print page if saved */ 
         /*   if (Session["OTEONUMBER"] != null)            
            {              
                //temp code to display properly in local as well as in hosting environment 
                string appPath = HttpRuntime.AppDomainAppVirtualPath;
                if(appPath !="/")                
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(' "+appPath + "/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
                 else                
                 ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Print/ePrint.aspx?Id=" + Session["OTEONUMBER"].ToString() + "');", true);
           Session["OTEONUMBER"] = null;
            }*/
            /*for the purpose of opening new print page if saved */ 
             


           
            if (!Page.IsPostBack)
            {
                //InsertEmptyRow();
                GetIssueHeadsandItemsForDropDowns();
                InsertEmptyRow(10);
                _btnSave.Visible = Button1.Visible = true;


                ddlChargeableHead.Enabled = false;
                gvItems.Enabled = false;
                _RetriveOTEOID();
                _RetriveIssueHeads();
              
                

            }
            
        }

        private void _RetriveIssueHeads()
        {
            //retrive CHead
            string cmdText2 = "select IssueHeadId,IssueHeadName from issueheads order by IssueHeadName;";
            data2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter(cmdText2, con);
            adapter2.Fill(data2);
            ddlIssueHead.DataSource = data2;
            ddlIssueHead.DataTextField = "IssueHeadName";
            ddlIssueHead.DataValueField = "IssueHeadId";
            ddlIssueHead.DataBind();
            ddlIssueHead.Items.Insert(0, new ListItem("--Select Issue Head--", ""));
       
            con.Close();
        }
        

        private void _RetriveOTEOID()
        {
            string str = "select max(ReceivedItemsOTEOID) from ReceivedItemsOTEO";
            SqlCommand com = new SqlCommand(str, con);
            con.Open();
            int count;
            if (com.ExecuteScalar() != DBNull.Value)
            {
                count = Convert.ToInt32(com.ExecuteScalar()) + 1;
            }
            else
            {
                count = 1;
            }
            tbOtEONumber.Text = count.ToString();
            con.Close();
        }
     
  
         private void GetIssueHeadsandItemsForDropDowns()
         {

             dst = ReceiveItemsNewLogic.RetrieveAllItems();
             //second table contains the items names                             
             dtItems = dst.Tables["Items"];
            // dtIssueHead = dst.Tables["itemRatesSecondery"];
            // dtRate = dst.Tables["IT2"];
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


                ddlChargeableHead.DataSource = ReceiveItemsNewLogic.RetrieveCorrespondingActiveChargeableHeads(Convert.ToInt32(ddlIssueHead.SelectedValue));
                ddlChargeableHead.DataValueField = "CHARGEABLEHEADID";
                ddlChargeableHead.DataTextField = "CHARGEABLEHEADNAME";
                ddlChargeableHead.DataBind();
                ddlChargeableHead.Items.Insert(0, new ListItem("--Select Chargeable Head--", ""));

                ReceivedItemsLogic.RetrieveActiveIssueHeadsAndActiveItemsSeperatelyOnIssueItemChanged(ddlIssueHead.SelectedValue);

            }
            else
            {
                ddlChargeableHead.Enabled = false ;
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
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error in loading the items.')", true);
            }
        }


         private void AddNewRowsToGrid()
        {
            int rowIndex = 0;

          TextBox tbtotalAmount= gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;       
          string totalAmount = tbtotalAmount.Text;

          //first work being done is to   extract the gridview control items values(if any)
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count ; i++)
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
                      drCurrentRow=  dtCurrentTable.NewRow();
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

                if (tbOtEONumber.Text == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "OTEO Number Cannot be Blank.";
                    panelSuccess.Visible = false;
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);
                }



                RecievedItemsOrderObject.Date = DateTime.ParseExact(tbOTEODate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");


                if (tbSupplyOrderReference.Text == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Supply Order Reference Cannot be Blank.";
                    panelSuccess.Visible = false;
                    tbSupplyOrderReference.Focus();
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
                }



                RecievedItemsOrderObject.SupplyDate = DateTime.ParseExact(tbSupplyDate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");



                if (tbSupplierName.Text == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Supplier Name  Cannot be Blank.";
                    panelSuccess.Visible = false;
                    tbSupplierName.Focus();
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
                }


                if (ddlIssueHead.SelectedValue == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Should Select Issue Heads.";
                    panelSuccess.Visible = false;
                    return;

                }
                else
                {
                    RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                }

                if (ddlChargeableHead.SelectedItem.ToString() == "--Select Chargeable Head--")
                {
                    panelError.Visible = true;
                    lblError.Text = "Should Select Chargeable Heads.";
                    panelSuccess.Visible = false;
                    return;

                }
                else
                {
                    RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                }








                TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                //setting user id temp here
                // Session["USERID"] = 1;
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control

                StringBuilder sb = new StringBuilder();


                //  sb.Append("<root>");

                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRateMaster = "INSERT into ItemsRateMaster (itemname,MaxOrderNO,IssueHeadName) values('@ITEMNAME','@odNo','@issueHead')";
                string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@odNo' where itemname='@ITEMNAME' AND IssueHeadName='@issueHead'";


                string insertRateSecondary = "INSERT into ItemsRateSecondary (ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";
                //string updateRateSecondary = "update ItemsRateSecondary set ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";


                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    // TextBox tbItemID = gvItems.Rows[i].FindControl("tbItemID") as TextBox;

                    HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;

                    TextBox tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;

                    TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES
                    CheckExistingItem = tbOrderNo.Text;
                    //SAVE CODE
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
                            sb.Append(insertRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        }
                        else
                        {
                            sb.Append(UpdateRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        }
                    }
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    //ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString(),sb1.ToString (),sb2.ToString(),sb3.ToString ());
                 
                    ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString());
                   

                    Response.Redirect("ReceivedItemsDetails.aspx?id="+tbOtEONumber.Text );
          
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

                if (tbOtEONumber.Text=="")
                {
                    panelError.Visible = true;
                    lblError.Text = "OTEO Number Cannot be Blank.";
                    panelSuccess.Visible = false;
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.ReceivedItemsOTEOID = Convert.ToInt32(tbOtEONumber.Text);
                }

                

                RecievedItemsOrderObject.Date  = DateTime.ParseExact(tbOTEODate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");


                if (tbSupplyOrderReference.Text == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Supply Order Reference Cannot be Blank.";
                    panelSuccess.Visible = false;
                    tbSupplyOrderReference.Focus();
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.SupplyOderRef = tbSupplyOrderReference.Text;
                }
                
                

                RecievedItemsOrderObject.SupplyDate = DateTime.ParseExact(tbSupplyDate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");



                if (tbSupplierName.Text == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Supplier Name  Cannot be Blank.";
                    panelSuccess.Visible = false;
                    tbSupplierName.Focus();
                    return;
                }
                else
                {
                    RecievedItemsOrderObject.Supplier = tbSupplierName.Text;
                }


                if (ddlIssueHead.SelectedValue == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Should Select Issue Heads.";
                    panelSuccess.Visible = false;
                    return;

                }
                else
                {
                    RecievedItemsOrderObject.IssueHeadName = ddlIssueHead.SelectedItem.ToString();
                }

                if (ddlChargeableHead.SelectedItem.ToString() == "--Select Chargeable Head--")
                {
                    panelError.Visible = true;
                    lblError.Text = "Should Select Chargeable Heads.";
                    panelSuccess.Visible = false;
                    return;

                }
                else
                {
                    RecievedItemsOrderObject.ChargeableHeadName = ddlChargeableHead.SelectedItem.ToString();
                }


                
                
               



                TextBox tbtotalAmount = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;
                RecievedItemsOrderObject.TotalAmount = Convert.ToDouble(tbtotalAmount.Text);
                //setting user id temp here
               // Session["USERID"] = 1;
                RecievedItemsOrderObject.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                //getting all the values of controls values inside gridview control

                StringBuilder sb = new StringBuilder();
                

                //  sb.Append("<root>");

                string insertStatement = "INSERT INTO ReceivedItemsDetails(RECEIVEDITEMSOTEOID,ITEMID, ITEMNAME,QUANTITY,UNIT,RATE, AMOUNT) values('@RECEIVEDITEMSOTEOID','@ITEMID', '@ITEMNAME', '@QUANTITY', '@UNIT', '@RATE', '@AMOUNT')";

                string insertRateMaster = "INSERT into ItemsRateMaster (itemname,MaxOrderNO,IssueHeadName) values('@ITEMNAME','@odNo','@issueHead')";
                string UpdateRateMaster = "update ItemsRateMaster set MaxOrderNO='@odNo' where itemname='@ITEMNAME' AND IssueHeadName='@issueHead'";


                string insertRateSecondary = "INSERT into ItemsRateSecondary (ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";
                //string updateRateSecondary = "update ItemsRateSecondary set ITEMNAME,Rate,OrderNO,Quantity,IssueHeadName) values('@ITEMNAME','@RATE','@odNo','@QUANTITY','@issueHead')";


                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    // TextBox tbItemID = gvItems.Rows[i].FindControl("tbItemID") as TextBox;

                    HiddenField hdnFieldItemID = gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField;
                    DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                    TextBox _tbUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;
                    // Label lblUnit = gvItems.Rows[i].FindControl("lblUnit") as Label;

                    TextBox tbOrderNo = gvItems.Rows[i].FindControl("_tbOrderNo") as TextBox;

                    TextBox tbQuantity = gvItems.Rows[i].FindControl("_tbQuantity") as TextBox;
                    TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                    TextBox tbAmount = gvItems.Rows[i].FindControl("tbAmount") as TextBox;
                    // Label lblAmount = gvItems.Rows[i].FindControl("lblAmount") as Label;

                    //CODE TO CHECK NULL VALUES
                    CheckExistingItem = tbOrderNo.Text;
                    //SAVE CODE
                    if (itemName.SelectedValue.ToString() != "0")
                    {
                       

                            double result;
                            if  (!double.TryParse(tbQuantity.Text, out result))
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
                            sb.Append(insertRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        }
                        else
                        {
                            sb.Append(UpdateRateMaster.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@odNo", tbOrderNo.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                            sb.Append(insertRateSecondary.Replace("@ITEMNAME", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@RATE", tbRate.Text).Replace("@odNo", tbOrderNo.Text).Replace("@QUANTITY", tbQuantity.Text).Replace("@issueHead", ddlIssueHead.SelectedItem.ToString()));
                        } 
                   }
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb.ToString() != "")
                {
                    
                        ameh.SaveReceivedItemsDetails(RecievedItemsOrderObject, sb.ToString());
                     
                    Session["OTEONUMBER"] = tbOtEONumber.Text;
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
                            DataRow[] myOrderNo = dst.Tables["IT2"].Select("Itemname= '" + _ddItems.SelectedItem.ToString() + "' and issueheadname='" + ddlIssueHead.SelectedItem.Text  + "'");

                            foreach (DataRow dr in myOrderNo)
                           {
                                dt.ImportRow(dr);
                           }
                           
                            DataView view = new DataView(dt);
                            DataTable myRates = view.ToTable(true, "MaxOrderNo");

                            if (myRates.Rows.Count > 0)
                            {

                                 int myOrder =Convert.ToInt32 ( myRates.Rows[0]["MaxOrderNo"].ToString());
                                 myOrder = myOrder + 1;
                                tbOrderNO.Text = myOrder.ToString() ;

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