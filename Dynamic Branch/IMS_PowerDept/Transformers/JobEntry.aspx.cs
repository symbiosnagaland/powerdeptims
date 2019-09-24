using IMS_PowerDept.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.Transformers
{
    public partial class JobEntry : System.Web.UI.Page
    {

        DataTable dtRepairFirm = new DataTable("dtRepairFirm");
      //  DataTable static dtTransformers = new DataTable();

        protected static DataTable dtTransformers = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);



            protected void Page_Load(object sender, EventArgs e)
            {
                Session["USERID"] = 1;//setting user id temp here


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

                //if (Request["challanono"] != null)
                //{
                //    int challanNo = Convert.ToInt32(Request["challanno"]);
                //    //to get the details from db using challonno
                //}

                if (!Page.IsPostBack)
                {
                    FillTransformerMasterDataRepairFirms();
                    FetchTransformers();
                    InsertItemsRows(Convert.ToInt32(tbItemsRows.Text));
                }
                //Loading rows by default 10

                _btnSave.Visible = btnReset.Visible = true;


            }
            private void InsertItemsRows(int numberofRows)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;

                //dt.Columns.Add(new DataColumn("Item", typeof(string)));
                //dt.Columns.Add(new DataColumn("Unit", typeof(string)));
                //dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                //dt.Columns.Add(new DataColumn("IHead", typeof(string)));
                //dt.Columns.Add(new DataColumn("Rate", typeof(string)));
                for (int i = 0; i < numberofRows; i++)
                {
                    dr = dt.NewRow();
                    //dr["Item"] = string.Empty;
                    //dr["Unit"] = string.Empty;
                    //dr["Quantity"] = string.Empty;
                    //dr["IHead"] = string.Empty;
                    //dr["Rate"] = string.Empty;
                    dt.Rows.Add(dr);
                }
                 ViewState["TABLE"] = dt;
                gvItems.DataSource = dt;
                gvItems.DataBind();

            }


            

            #region fill transformer master data from excel file
            public void FillTransformerMasterDataRepairFirms()
            {
               
                string path = Server.MapPath("\\AppContent") + "\\Transformers_MasterData.xlsx";               
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
                        //using different oledbcommand
                        OleDbCommand dbCommandRepairFirm = new OleDbCommand("SELECT * FROM [REPAIRFIRM$]", dbConnection);
                     
                        //Create an OleDbDataReader to store the results from 
                        //executing the select statement in the OleDbCommand
                        //1
                        //using common dbreader
                        OleDbDataReader dbReader = dbCommandRepairFirm.ExecuteReader();
                        dtRepairFirm.Load(dbReader);

                        _ddlRepairFirm.DataSource = dtRepairFirm;
                        _ddlRepairFirm.DataTextField = _ddlRepairFirm.DataValueField = "REPAIRFIRM";
                        _ddlRepairFirm.DataBind();
                        _ddlRepairFirm.Items.Insert(0, new ListItem("--Select Repair Firm--", ""));
                      
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

            #region Fetch Tranformer Entries from DB once only
            //retrive chead and divisions
            private void FetchTransformers()
            {
                string cmdText = "SELECT  ReceivedTransformerID, Voltage + '(voltage)     ' + CONVERT(varchar(50), kVA) + '(kVA)       ' + Make + '(make)     ' + Seriel + '(seriel)     ' AS ReceivedTransformer FROM  Transformer_ReceiptsDetails";
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
                adapter.Fill(data);
                dtTransformers = data;
      
            }
            #endregion        

            #region rows data populated here
            protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                //condition to let it fetch from excel only once
                //if (dtVOLTAGE == null || dtVOLTAGE.Rows.Count <= 0)
                //    FillTransformerMasterData();
                ////Check if the DataTable has data
                //else //if (dtTransformer != null)
                {
                    //Load the dropdown list with the contents of the table for seperately                
                    try
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            //Find the DropDownList in the Row
                            DropDownList ddlTransfomer = (e.Row.FindControl("ddlTransfomer") as DropDownList);
                            ddlTransfomer.DataSource = dtTransformers;
                            ddlTransfomer.DataValueField = "receivedtransformerid";
                            ddlTransfomer.DataTextField = "ReceivedTransformer";
                            ddlTransfomer.DataBind();
                            ddlTransfomer.Items.Insert(0, new ListItem("--Select Transformer--", ""));

                          
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' There was some error.')", true);
                    }
                }
            }
            #endregion

            //#region adding new rows
            //protected void ButtonAddnewRows_Click(object sender, EventArgs e)
            //{

            //    int rowIndex = 0;
            //    if (ViewState["TABLE"] != null)
            //    {
            //        DataTable dtCurrentTable = (DataTable)ViewState["TABLE"];
            //        DataRow drCurrentRow = null;
            //        if (dtCurrentTable.Rows.Count > 0)
            //        {
            //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //            {
            //                //  extract the TextBox values
            //                DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
            //                TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
            //                TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbQuantity");
            //                DropDownList ihead = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("_ddIhead");
            //                DropDownList ddlRates = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("ddlRates");
            //                drCurrentRow = dtCurrentTable.NewRow();
            //                dtCurrentTable.Rows[i - 1]["Item"] = _ddItems.Text;
            //                dtCurrentTable.Rows[i - 1]["Unit"] = _tbUnit.Text;
            //                dtCurrentTable.Rows[i - 1]["Quantity"] = _tbQuantity.Text;
            //                dtCurrentTable.Rows[i - 1]["IHead"] = ihead.Text;
            //                dtCurrentTable.Rows[i - 1]["Rate"] = ddlRates.Text;
            //                rowIndex++;
            //            }
            //            dtCurrentTable.Rows.Add(drCurrentRow);
            //            int numberOfnewRowsToAdd = 10;
            //            while (numberOfnewRowsToAdd > 1)
            //            {
            //                drCurrentRow = dtCurrentTable.NewRow();
            //                dtCurrentTable.Rows.Add(drCurrentRow);
            //                numberOfnewRowsToAdd--;
            //            }
            //            ViewState["TABLE"] = dtCurrentTable;
            //            gvItems.DataSource = dtCurrentTable;
            //            gvItems.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("ViewState is null");
            //    }



            //    //Set Previous Data on Postbacks         
            //    rowIndex = 0;
            //    if (ViewState["TABLE"] != null)
            //    {
            //        DataTable dt = (DataTable)ViewState["TABLE"];
            //        if (dt.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                DropDownList _ddItems = (DropDownList)gvItems.Rows[rowIndex].Cells[1].FindControl("_ddItems");
            //                TextBox _tbUnit = (TextBox)gvItems.Rows[rowIndex].Cells[2].FindControl("_tbUnit");
            //                TextBox _tbQuantity = (TextBox)gvItems.Rows[rowIndex].Cells[3].FindControl("_tbQuantity");
            //                DropDownList ihead = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("_ddIhead");
            //                DropDownList ddlRates = (DropDownList)gvItems.Rows[rowIndex].Cells[3].FindControl("ddlRates");
            //                _ddItems.Text = dt.Rows[i]["Item"].ToString();
            //                _tbUnit.Text = dt.Rows[i]["Unit"].ToString();
            //                _tbQuantity.Text = dt.Rows[i]["Quantity"].ToString();
            //                //not populating as list item is not there now data is not there now
            //                ihead.Text = dt.Rows[i]["IHead"].ToString();
            //                ddlRates.Text = dt.Rows[i]["Rate"].ToString();
            //                rowIndex++;

            //            }

            //            //  TextBox tbtotalAmount2 = gvItems.FooterRow.FindControl("tbtotalAmount") as TextBox;


            //            //  string totalAMount = (string) ViewState["TOTALAMOUNT"];
            //            //  tbtotalAmount2.Text = totalAmount;
            //        }

            //    }
            //}

            //#endregion

            #region SAVE
            //method for saving all the data        
            private void SAVE()
            {
                TransformerLogic jobEntry = new TransformerLogic();
                string insertStatement1; string insertStatement2;
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                try
                {
                    //insert statement for main table 
                    insertStatement1 = "Insert into Transformer_job(ChallanNo, ChallanDate, RepairFirm, ModifiedBy)values('@ChallanNo', '@ChallanDate',  '@RepairFirm', '@ModifiedBy')";
                    sb1.Append(insertStatement1.Replace("@ChallanNo", _tbChalanNo.Text.TrimEnd()).Replace("@ChallanDate", _tbChallanDate.Text).Replace("@RepairFirm", _ddlRepairFirm.SelectedItem.ToString()).Replace("@ModifiedBy", Session["USERID"].ToString()));


                    //multiple insert statements for details table
                    insertStatement2 = "INSERT INTO Transformer_JobDetails(challanNo,TransformerID, transformer, JobNo) values('@challanNo','@TransformerID','@transformer', '@JobNo')";

                    DropDownList ddlTransfomer; TextBox tbJobNo;
                    for (int i = 0; i < gvItems.Rows.Count; i++)
                    {
                        ddlTransfomer = gvItems.Rows[i].FindControl("ddlTransfomer") as DropDownList;
                        tbJobNo = gvItems.Rows[i].FindControl("tbJobNo") as TextBox;
                        //SAVE CODE
                        if (ddlTransfomer.SelectedValue.ToString() != "")
                            sb2.Append(insertStatement2.Replace("@challanNo", _tbChalanNo.Text.TrimEnd()).Replace("@TransformerID", ddlTransfomer.SelectedValue).Replace("@transformer", ddlTransfomer.SelectedItem.ToString()).Replace("@JobNo", tbJobNo.Text));
                    }
                    //now save it to db
                    //making sure sb string is not empty
                    if (sb2.ToString() != "")
                    {

                        jobEntry.SaveEntries(sb1.ToString(), sb2.ToString());
                        //using session here to
                        //Session["CHALLANNO"] = _tbChalanNo.Text;
                        //Response.Redirect(Request.Url.ToString());
                        panelError.Visible = false;
                        lblSuccess.Text = "Job Entry successfully added.";
                        panelSuccess.Visible = true;
                        ClearAllControls(Page.Controls);
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
                    if (ex.Message.Contains("duplicate key"))
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
         
                SAVE();



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



            void ClearAllControls(ControlCollection ctrls)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl is TextBox)
                        ((TextBox)ctrl).Text = string.Empty;
               
                    else  if (ctrl is DropDownList)
                        ((DropDownList)ctrl).SelectedIndex = -1;
                    ClearAllControls(ctrl.Controls);
                }
            }

        
    }
}