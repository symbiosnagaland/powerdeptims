using IMS_PowerDept.AppCode;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IMS_PowerDept.UserControls
{

    public partial class TransformerReceiptEntry : System.Web.UI.UserControl
    {
        DataTable dtVOLTAGE = new DataTable("dtVOLTAGE");
        DataTable dtKVA = new DataTable("dtKVA");
        DataTable dtMAKE = new DataTable("dtMAKE");
       protected static DataSet dstTransformer = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);       
    
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
          //  Session["USERID"] = 1;//setting user id temp here


            //if (Session["CHALLANNO"] != null)
            //{
            //    //code to display properly in local as well as in hosting environment 
            //    string appPath = HttpRuntime.AppDomainAppVirtualPath;
            //    if (appPath != "/") //if / is not the root path
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(' " + appPath + "/Print/Print.aspx?Id=" + Session["CHALLANNO"].ToString() + "');", true);
            //    else
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Print/Print.aspx?Id=" + Session["CHALLANNO"].ToString() + "');", true);
            //    Session["CHALLANNO"] = null;
            //}

            if (Request["challanono"] != null)
            {
                int challanNo = Convert.ToInt32(Request["challanno"]);
                //to get the details from db using challonno
            }

            if (!Page.IsPostBack)
            {             
                PopulateDivsions();
                InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));
            }
            //Loading rows by default 10
            
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


        #region populate divisions
        //retrive chead and divisions
        private void PopulateDivsions()
        {
            //retrive divisions
            string cmdText = "SELECT DISTINCT divisionName FROM Divisions";           
           DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
            adapter.Fill(data);
            _ddIntendDivisions.DataSource = data;
            _ddIntendDivisions.DataTextField = "divisionName";
            _ddIntendDivisions.DataBind();
            _ddIntendDivisions.Items.Insert(0, new ListItem("--Select Division Name--", "0"));
        }
        #endregion        

        #region fill transformer master data from excel file
        public void FillTransformerMasterData()
        {
            //Retrieve the spreadsheet directory path from the web config file
            // string path = ConfigurationManager.AppSettings["ExcelPath"];
            string path = Server.MapPath("\\AppContent") + "\\Transformers_MasterData.xlsx";
            //Append the name of the Excel Spreadsheet to the path
            //  path += "\\

            //Create a DataTable to be returned by this method
           

            //Create an OLEDB connection string to access the Excel spreadsheet
            //string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            //conn += path;
            //conn += ";Extended Properties=\"Excel 8.0;HDR=Yes;\"";

            //read a 2007 file  
            string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";//DAL



            //Check if the file exists
            if (File.Exists(path))
            {
                //Instantiate an OleDBConnection using the conn variable just created
                OleDbConnection dbConnection = new OleDbConnection(@conn);

                //If the OleDBConnection is not open, then open it
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }

                //Declare a try-catch-finally statement
                try
                {
                    //Create a temporary DataTable using the opened OleDBConnection
                    //The GetOleDbSchemaTable method return the first worksheet in 
                    //the Excel spreadsheet in the form of a table
                    DataTable dtTableSchema = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    //If no data was returned from the OleDBConnection throw an error
                    if (dtTableSchema == null || dtTableSchema.Rows.Count < 1)
                    {
                        throw new Exception("Error: Could not determine the name of the first worksheet.");
                    }

                    //The name of the first worksheet in the Excel spreadsheet is contained
                    //in the name of the DataTable
                    // string firstSheetName = dtTableSchema.Rows[0]["TABLE_NAME"].ToString();

                    //Create an OleDbCommand using a select statement and the connection to retrieve 
                    //the data from the Excel spreadsheet
                    // OleDbCommand dbCommand = new OleDbCommand("SELECT * FROM [" + firstSheetName + "]", dbConnection);

                    //using different oledbcommand
                    OleDbCommand dbCommandVoltage = new OleDbCommand("SELECT * FROM [VOLTAGE$]", dbConnection);
                    OleDbCommand dbCommandKVA = new OleDbCommand("SELECT * FROM [KVA$]", dbConnection);
                    OleDbCommand dbCommandMake = new OleDbCommand("SELECT * FROM [MAKE$]", dbConnection);

                    //Create an OleDbDataReader to store the results from 
                    //executing the select statement in the OleDbCommand
                    //1
                    //using common dbreader
                    OleDbDataReader dbReader = dbCommandVoltage.ExecuteReader();
                    dtVOLTAGE.Load(dbReader);
                    //2
                    dbReader = dbCommandKVA.ExecuteReader();
                    dtKVA.Load(dbReader);
                    //3

                    dbReader = dbCommandMake.ExecuteReader();
                    dtMAKE.Load(dbReader);
                }
                finally
                {
                    //Close and dispose the connection
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();
                        dbConnection.Dispose();
                    }
                }
                //Return the DataTable
                // return dtTransformer;
            }
            //  return null;
        }
        #endregion

        #region rows data populated here
        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //condition to let it fetch from excel only once
              if (dtVOLTAGE == null || dtVOLTAGE.Rows.Count <= 0)
                     FillTransformerMasterData();
            //Check if the DataTable has data
            else //if (dtTransformer != null)
            {
                //Load the dropdown list with the contents of the table for seperately                
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Find the DropDownList in the Row
                        DropDownList ddlVoltage = (e.Row.FindControl("ddlVoltage") as DropDownList);
                        ddlVoltage.DataSource = dtVOLTAGE;
                        ddlVoltage.DataTextField = "VOLTAGE";
                        ddlVoltage.DataBind();
                        ddlVoltage.Items.Insert(0, new ListItem("--Select Voltage--", ""));

                        //Find the DropDownList in the Row
                        DropDownList ddlKVA = (e.Row.FindControl("ddlKVA") as DropDownList);
                        ddlKVA.DataSource = dtKVA;
                        ddlKVA.DataTextField = "KVA";
                        ddlKVA.DataBind();
                        ddlKVA.Items.Insert(0, new ListItem("--Select kVA--", ""));

                        //Find the DropDownList in the Row
                        DropDownList ddlMake = (e.Row.FindControl("ddlMake") as DropDownList);
                        ddlMake.DataSource = dtMAKE;
                        ddlMake.DataTextField = "MAKE";
                        ddlMake.DataBind();
                        ddlMake.Items.Insert(0, new ListItem("--Select Make of Transformer--", ""));
                        
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error.')", true);
                }
            }
        }
        #endregion

        #region adding new rows
        protected void ButtonAddnewRows_Click(object sender, EventArgs e)
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
        
        #endregion

        #region SAVE
        //method for saving all the data        
        private void _SaveDeliveryItemDetails()
        {
            TransformerReceiptLogic baby = new TransformerReceiptLogic();
            string insertStatement1; string insertStatement2;            
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            try
            {
                //insert statement for main table 
                insertStatement1 = "Insert into Transformer_Receipts(ChallanNo, ChallanDate, Division, ReceiptDate,ProbableCause,ModifiedBy)values('@ChallanNo', '@ChallanDate',  '@Division', '@ReceiptDate', '@ProbableCause','@ModifiedBy')";
                sb1.Append(insertStatement1.Replace("@ChallanNo", _tbChalanNo.Text.TrimEnd()).Replace("@ChallanDate", _tbChallanDate.Text).Replace("@Division", _ddIntendDivisions.SelectedItem.ToString()).Replace("@ReceiptDate", _tbReceiptDate.Text).Replace("@ProbableCause", tbProbableCause.Text).Replace("@ModifiedBy", Session["USERID"].ToString()));


                //multiple insert statements for details table
                insertStatement2 = "INSERT INTO Transformer_ReceiptsDetails(ChallanNo,Voltage, kVA,Make,Seriel,Oil) values('@ChallanNo','@Voltage', '@kVA', '@Make', '@Seriel', '@Oil')";

                DropDownList ddlVoltage, ddlKVA, ddlMake; TextBox tbSeriel, tbOil;
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    ddlVoltage = gvItems.Rows[i].FindControl("ddlVoltage") as DropDownList;
                    ddlKVA = gvItems.Rows[i].FindControl("ddlKVA") as DropDownList;
                    ddlMake = gvItems.Rows[i].FindControl("ddlMake") as DropDownList;

                    tbSeriel = gvItems.Rows[i].FindControl("tbSeriel") as TextBox;
                    tbOil = gvItems.Rows[i].FindControl("tbOil") as TextBox;
                    //SAVE CODE
                    if (ddlVoltage.SelectedValue.ToString() != "")
                        sb2.Append(insertStatement2.Replace("@ChallanNo", _tbChalanNo.Text.TrimEnd()).Replace("@Voltage", ddlVoltage.SelectedValue).Replace("@kVA", ddlKVA.SelectedItem.ToString()).Replace("@Make", ddlMake.SelectedValue).Replace("@Seriel", tbSeriel.Text).Replace("@Oil", tbOil.Text));
                }
                //now save it to db
                //making sure sb string is not empty
                if (sb2.ToString() != "")
                {
                    //call the method to save both in primary deliverychallan table and delivery details table
                    baby.SaveTransformerReceiptEntry(sb1.ToString(), sb2.ToString());
                    //using session here to
                    //Session["CHALLANNO"] = _tbChalanNo.Text;
                    //Response.Redirect(Request.Url.ToString());
                    panelError.Visible = false;
                    lblSuccess.Text = "Receipt Entry successfully added.";
                    panelSuccess.Visible = true;
                    Utilities.ClearAllControls(Page.Controls);
                }
                else
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Select atleast one item to add.";
                    panelSuccess.Visible = false;
                }

            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("duplicate key"))
                {
                     panelError.Visible = true;
                    lblError.Text = "Error! Challan number already exists. ";
                    panelSuccess.Visible = false;
                    return;
                }
            }

            catch (System.Threading.ThreadAbortException)
            {
                //do nothing

            }
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("~/Error.aspx", true);



            }
        }
        #endregion

        #region save click event 

        protected void _btnSave_Click(object sender, EventArgs e)
        {
            //checking in there is any null value b4 saving 2 challan table
            if (_tbChalanNo.Text.Trim() == "")
            {
                panelError.Visible = true;
                lblError.Text = "Challan No. cannot be NULL.";
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
                     _SaveDeliveryItemDetails();
                        
            
            
        }
        #endregion

        protected void btnRowsAdd_Click(object sender, EventArgs e)
        {
            InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));
            _btnSave.Visible = btnReset.Visible = true;
         
        }

        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }


       

       
    }
}