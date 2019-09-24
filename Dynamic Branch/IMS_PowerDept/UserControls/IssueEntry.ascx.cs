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

        protected void Page_Load(object sender, EventArgs e)
        {
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

            private void _RetriveDelliveryChallanID()
            {

                str = "select max(deliveryitemschallanid) from DeliveryItemsChallan";
                com = new SqlCommand(str, con);
                con.Open();
                if (com.ExecuteScalar() != DBNull.Value)
                {
                    count = Convert.ToInt32(com.ExecuteScalar()) + 1;
                }
                else
                {
                    count = 1;
                }
                _tbChalanNo.Text =count.ToString();
                con.Close();
            }

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
              
                data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
                adapter.Fill(data);
                _ddIntendDivisions.DataSource = data;
                _ddIntendDivisions.DataTextField = "divisionName";
                _ddIntendDivisions.DataBind();
                _ddIntendDivisions.Items.Insert(0, new ListItem("--Select Division Name--", "0"));
                con.Close();
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
               
                gvItems.DataSource = dt;
                gvItems.DataBind();


            }

            protected void btnRowsAdd_Click(object sender, EventArgs e)
            {
                InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));
            }

            protected void _btnCancel_Click(object sender, EventArgs e)
            {
                Response.Redirect(Request.Url.ToString());
            }


            private void GetIssueHeadsandItemsForDropDowns()
            {

                dst = IssueNewLogic.RetrieveAllItems();
                //second table contains the items names                             
                dtItems = dst.Tables["Items"];
             //   dtIssueHead = dst.Tables["itemRatesSecondery"];
              //  dtRate = dst.Tables["IT2"];
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
                        ddItemName.Items.Insert(0, new ListItem("--Select Item Name--", ""));
                       // ddItemName.Attributes.Add("onchange", "calculateQtySum();");
                    }
                }
                catch
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error.')", true);
                }

            }


        
         protected void OnTextChanged(object sender, EventArgs e)
         {
             int a = 5;

         }


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
                            DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;


                            TextBox tbRate = row.FindControl("tbRate") as TextBox ;
                            TextBox tbQty = row.FindControl("tbQty") as TextBox;
                            TextBox tbOrderNO = row.FindControl("tbOrderNO") as TextBox ;
                            TextBox  tbAmount = row.FindControl("tbAmount") as TextBox ;
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

                            
                            HiddenField  _hdnFieldItemID = row.FindControl("_hdnFieldItemID") as HiddenField ;



                            DataTable dt = dst.Tables["IT2"].Clone();
                            DataRow[] rates = dst.Tables["IT2"].Select("ItemName= '" + _ddItems.SelectedItem.ToString () + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

                            DataTable dt2 = dst.Tables["IT3"].Clone();
                            DataRow[] MaxQty = dst.Tables["IT3"].Select("ItemName= '" + _ddItems.SelectedItem.ToString() + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

                            foreach (DataRow dr in MaxQty )
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
                            DataTable myRates = view.ToTable(true, "Rate","Quantity","OrderNo","AMT");

                            if(myRates .Rows .Count >0)
                            {
                                tbRate.Text = myRates.Rows[0]["Rate"].ToString();
                                tbQty.Text = myRates.Rows[0]["Quantity"].ToString();
                                tbOrderNO.Text = myRates.Rows[0]["OrderNo"].ToString();
                                tbAmount.Text = myRates.Rows[0]["AMT"].ToString();

                                tbMaxQtyAvail.Text = myMaxQty.Rows[0]["TotQtyAvailable"].ToString();

                                

                                
                              //  ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);


                            }
                            else
                            {
                              //  ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                            }
                         
                          
                            
                           
                            break;
                        }
                    }
                }
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
                            //ClientID is match so find the Textbox 
                            //control bind it with  dropdown data.                      

                            //retrive item unit
                            string selected = ddl.SelectedItem.Value;
                            DropDownList _ddItems = row.FindControl("_ddItems") as DropDownList;

                            DropDownList ddlIhead = row.FindControl("ddlIhead") as DropDownList;

                            TextBox _tbUnit = row.FindControl("_tbUnit") as TextBox;

                            TextBox  tbRate = row.FindControl("tbRate") as TextBox ;
                            TextBox tbQty = row.FindControl("tbQty") as TextBox ;
                            TextBox  tbOrderNO = row.FindControl("tbOrderNO") as TextBox ;
                            TextBox  tbAmount = row.FindControl("tbAmount") as TextBox ;

                            TextBox tbMaxQtyAvail = row.FindControl("tbMaxQtyAvail") as TextBox;

                            

                            TextBox _tbOrderQuantity = row.FindControl("_tbOrderQuantity") as TextBox;
                            TextBox _tbAmt = row.FindControl("_tbAmt") as TextBox; 

                            
                            HiddenField  _hdnFieldItemID = row.FindControl("_hdnFieldItemID") as HiddenField ;

                            


                            
                          
                            string[] splitresult = _ddItems.SelectedValue.Split(' ');
                            if(splitresult .Length > 1)
                            { 
                                _tbUnit.Text = splitresult[1];
                                _hdnFieldItemID.Value    = splitresult[0];
                                DataTable dt = dst.Tables["itemRatesSecondery"].Clone();
                                DataRow[] rates = dst.Tables["itemRatesSecondery"].Select("Itemname= '" + _ddItems.SelectedItem.ToString () + "'");




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
                                _tbOrderQuantity.Text  = "0";
                                _tbAmt.Text = "0";
                                tbMaxQtyAvail.Text = "0";
                               // ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                            }
                            else
                            {
                                _tbUnit.Text = "";
                                _hdnFieldItemID.Value  = "";

                                tbRate.Text = "";
                                tbQty.Text = "";
                                tbOrderNO.Text = "";
                                tbAmount.Text = "";
                                _tbOrderQuantity.Text = "";
                                _tbAmt.Text = "";
                                tbMaxQtyAvail.Text = "";

                                ddlIhead.Items.Clear();
                                ddlIhead.Items.Insert(0, new ListItem("--Select Issue Heads--", "0"));

                                 _tbOrderQuantity.Text  = "";

                                _tbAmt.Text  = "";
                                //ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "calculateQtySum();", true);

                                return;
                            }
                            



                           break;
                        }
                    }
                }
            }

            protected void Save_Click(object sender, EventArgs e)
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

                if (_tbIntendDate.Text.Trim() == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Indent Date cannot be NULL.";
                    panelSuccess.Visible = false;

                    return;
                }

                // converting stirng to date and compare indent date and challan date

                DateTime ChallanDate = Convert.ToDateTime(_tbChallanDate.Text);
                DateTime IndentDate = Convert.ToDateTime(_tbIntendDate.Text);

                if (ChallanDate < IndentDate)
                {
                    panelError.Visible = true;
                    lblError.Text = "Challan date Should be Greater or Equal to Indent Date.";
                    panelSuccess.Visible = false;
                    _tbChallanDate.Style.Add("background", "Pink");
                    _tbChallanDate.Focus();
                    return;
                }
                else
                {
                    _tbChallanDate.Style.Add("background", "White");
                }



                if (_ddIntendDivisions.SelectedItem.ToString() == "--Select Division Name--")
                {
                    panelError.Visible = true;
                    lblError.Text = "Division Name Should Be Selected.";
                    panelSuccess.Visible = false;
                    return;
                }
                if (_ddCHead.SelectedItem.ToString()  == "--Select Chargeable Head--")
                {
                    panelError.Visible = true;
                    lblError.Text = "ChargeableHead Name Should Be Selected.";
                    panelSuccess.Visible = false;
                    return;
                }

                try
                {

                    SqlCommand cmdd = new SqlCommand("select * from DeliveryItemsChallan where DeliveryItemsChallanID = @DeliveryItemsChallanID", con);
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@DeliveryItemsChallanID";
                    param.Value = _tbChalanNo.Text;
                    cmdd.Parameters.Add(param);

                    con.Open();
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

                if (_tbIntendDate.Text.Trim() == "")
                {
                    panelError.Visible = true;
                    lblError.Text = "Indent Date cannot be NULL.";
                    panelSuccess.Visible = false;

                    return;
                }

                // converting stirng to date and compare indent date and challan date

                DateTime ChallanDate = Convert.ToDateTime(_tbChallanDate.Text);
                DateTime IndentDate = Convert.ToDateTime(_tbIntendDate.Text);

                if (ChallanDate < IndentDate)
                {
                    panelError.Visible = true;
                    lblError.Text = "Challan date Should be Greater or Equal to Indent Date.";
                    panelSuccess.Visible = false;
                    _tbChallanDate.Style.Add("background", "Pink");
                    _tbChallanDate.Focus();
                    return;
                }
                else
                {
                    _tbChallanDate.Style.Add("background", "White");
                }



                if (_ddIntendDivisions.SelectedItem.ToString() == "--Select Division Name--")
                {
                    panelError.Visible = true;
                    lblError.Text = "Division Name Should Be Selected.";
                    panelSuccess.Visible = false;
                    return;
                }
                if (_ddCHead.SelectedItem.ToString() == "--Select Chargeable Head--")
                {
                    panelError.Visible = true;
                    lblError.Text = "ChargeableHead Name Should Be Selected.";
                    panelSuccess.Visible = false;
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
                        Boolean Reply= _Save();
                        if(Reply==true)
                        {
                            double myId = Convert.ToDouble(_tbChalanNo.Text) - 1;
                            Response.Redirect("IssuedItemsDetails.aspx?id=" + myId);
                        }
                       
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



            private Boolean  _Save()
            {
                 NewProperties issued = new NewProperties();
                 IssueNewLogic enterSave=new IssueNewLogic ();                   
                try
                {                   

                    issued.challanNO  = Convert.ToDouble  (_tbChalanNo.Text);
                    issued.challanDate  = DateTime.ParseExact(_tbChallanDate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                    issued.indentNo  = _tbIndentValue.Text;
                    issued.indentDate  = DateTime.ParseExact(_tbIntendDate.Text, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                  //  issued.TotalAmount = 900;
                    issued.intendingDivision  = _ddIntendDivisions.SelectedItem .ToString ();
                    issued.ChargeableHeadName = _ddCHead.SelectedItem.ToString();
                    issued.IsDeliveredTemporary  = istemporary.Checked ? "Yes" : "No";
                    issued.ModifiedBy = Convert.ToInt16(Session["USERID"]);
                    issued.ReceiverDesignation = tbReceiverDesignation.Text;
                    issued.VehicleNumber = tbVehicleNumberCaps.Text.ToUpper();
                    issued.Remarks = tbRemarks.Text;

                    StringBuilder sb = new StringBuilder();
                    string insertStatement = "INSERT INTO DeliveryItemsDetails(DeliveryItemsChallanID,itemid,ItemName, IssueHeadName,QUANTITY,UNIT,RATE) values('@DeliveryItemsChallanID','@ItemID','@ItemName', '@IssueHeadName', '@QUANTITY', '@UNIT', '@RATE')";
                    string updateItemRateSecondary = "update ItemsRateSecondary set quantity=quantity-'@QUANTITY' where itemname='@ItemName' and issueHeadName='@IssueHeadName' and OrderNO='@OrderNO'";


                    for (int i = 0; i < gvItems.Rows.Count; i++)
                    {
                        DropDownList itemName = gvItems.Rows[i].FindControl("_ddItems") as DropDownList;
                        TextBox itemUnit = gvItems.Rows[i].FindControl("_tbUnit") as TextBox;

                        TextBox tbQty = gvItems.Rows[i].FindControl("tbQty") as TextBox; 

                        TextBox _tbOrderQuantity = gvItems.Rows[i].FindControl("_tbOrderQuantity") as TextBox;                       
                        DropDownList ddlIhead = gvItems.Rows[i].FindControl("ddlIhead") as DropDownList;
                        TextBox tbRate = gvItems.Rows[i].FindControl("tbRate") as TextBox;
                        TextBox _tbAmt = gvItems.Rows[i].FindControl("_tbAmt") as TextBox;
                        HiddenField _hdnFieldItemID=gvItems.Rows[i].FindControl("_hdnFieldItemID") as HiddenField ;
                        TextBox  tbOrderNO=gvItems.Rows[i].FindControl("tbOrderNO") as TextBox  ;
                        




                        if (itemName.SelectedValue.ToString() != "")
                        {
                            //Checking the quantity is null in quantity
                            double result;
                            if (!double.TryParse(_tbOrderQuantity.Text, out result))
                            {
                                panelError.Visible = true;
                                lblError.Text = "Error! Quantity Should Be Numeric.";
                                panelSuccess.Visible = false;
                                _tbOrderQuantity.Style.Add("background", "Pink");
                                return false;
                            }

                            
                            Double result1 = Convert.ToDouble(_tbOrderQuantity.Text);
                            if (result1 <=0)
                            {
                                panelError.Visible = true;
                                lblError.Text = "Error! Quantity Should be Greater than Zero.";
                                panelSuccess.Visible = false;
                                _tbOrderQuantity.Style.Add("background", "Pink");
                                return false;
                            }
                            else
                            {
                                _tbOrderQuantity.Style.Add("background", "White");
                            }

                            if (tbRate.Text  != "")
                            {

                                totalAmount += Convert.ToDouble(_tbOrderQuantity.Text) * Convert.ToDouble(tbRate.Text);

                                if (Convert.ToDouble(_tbOrderQuantity.Text) > Convert.ToDouble(tbQty.Text))
                                {
                                    DataTable dt = dst.Tables["IT2"].Clone();
                                    DataRow[] rates = dst.Tables["IT2"].Select("itemName= '" + itemName.SelectedItem.ToString() + "' and IssueHeadName='" + ddlIhead.SelectedValue.ToString() + "'");

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
                                    Double  tempOrderNo;
                                    Double tempRate;


                                    while (OrderedQty>0)
                                    {
                                        tempQty =Convert .ToDouble ( myRates.Rows[counter]["Quantity"]);
                                        tempOrderNo =Convert .ToDouble ( myRates.Rows[counter]["OrderNo"]);
                                        tempRate = Convert.ToDouble(myRates.Rows[counter]["Rate"]);

                                        if(OrderedQty < tempQty)
                                        {
                                            tempQty = OrderedQty;
                                        }

                                        sb.Append(updateItemRateSecondary.Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", tempQty.ToString()).Replace("@IssueHeadName", ddlIhead.SelectedValue).Replace("@OrderNO", tempOrderNo.ToString()));

                                        sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemID", _hdnFieldItemID.Value).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", ddlIhead.SelectedValue ).Replace("@QUANTITY", tempQty.ToString()).Replace("@UNIT", itemUnit.Text).Replace("@RATE", tempRate.ToString ()));
                                        
                                        OrderedQty = OrderedQty - tempQty;
                                        counter++;
                                    }                                    
                                }
                                else
                                {
                                    sb.Append(updateItemRateSecondary.Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@QUANTITY", _tbOrderQuantity.Text).Replace("@IssueHeadName", ddlIhead.SelectedValue ).Replace("@OrderNO", tbOrderNO.Text));
                                    sb.Append(insertStatement.Replace("@DeliveryItemsChallanID", _tbChalanNo.Text).Replace("@ItemID", _hdnFieldItemID.Value).Replace("@ItemName", Utilities.ValidSql(itemName.SelectedItem.ToString())).Replace("@IssueHeadName", ddlIhead.SelectedValue ).Replace("@QUANTITY", _tbOrderQuantity.Text).Replace("@UNIT", itemUnit.Text).Replace("@RATE", tbRate.Text));

                                }
                               
                            }
                            else
                            {

                                panelError.Visible = true;
                                lblError.Text = "Error! One of item's Issue Head and Rate is not selected.";
                                panelSuccess.Visible = false;
                                return false;
                            }
                      
                        }
                    }
                    issued.TotalAmount = totalAmount;
                    //now save it to db
                    //making sure sb string is not empty
                    if (sb.ToString() != "")
                    {
                        // //call the method to save both in primary deliverychallan table and delivery details table
                       
                         enterSave.SaveNewIssuedItems (issued ,sb.ToString ());
                     

                        lblSuccess.Text = "Challan number " + _tbChalanNo.Text + " details entered successfully!";

                        GetIssueHeadsandItemsForDropDowns();


                        _tbChalanNo.Text = (Convert.ToDouble(_tbChalanNo.Text) + 1).ToString();

                        panelError.Visible = false;
                        panelSuccess.Visible = true;

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
                return true;
            }
    }
}