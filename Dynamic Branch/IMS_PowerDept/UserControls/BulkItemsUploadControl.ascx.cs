using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Data;
using IMS_PowerDept.AppCode;

namespace IMS_PowerDept.UserControls
{
    public partial class BulkItemsUploadControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// taking the data from uploaded excel file and saving to temp table using sqlbulk copy
        /// then copying all valid data from temp table to the main table
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="uploadedFilePathAndName"></param>
        protected void saveexcel_bulkcopy(string fileExtension, string uploadedFilePathAndName)
        {
            try
            {
                string strCSVConnString = "";

                if (fileExtension == ".xlsx")
                {   //read a 2007 file  

                    strCSVConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + uploadedFilePathAndName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";//DAL
                }
                else if (fileExtension == ".xls")
                {
                    //read a 97-2003 file  
                    strCSVConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + uploadedFilePathAndName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                }
                
                using (OleDbConnection con = new OleDbConnection(strCSVConnString))
                {
                    con.Open();
                    OleDbCommand com = new OleDbCommand("Select * from [sheet1$]", con);
                    OleDbDataReader dr = com.ExecuteReader();
                    SqlTransaction tr = null;
                    SqlConnection conn = new SqlConnection(AppCode.AppConns.GetConnectionString());

                    //this will execute first

                    SqlCommand cmdtruncate = conn.CreateCommand();
                    cmdtruncate.CommandText = "truncate table ItemsBulkUploadTemp";
                    //this will execute last after bulk copy to temp
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "sp_InsertItemsFromBulkTempTable";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (conn)
                    {
                        conn.Open();

                        tr = conn.BeginTransaction();
                        cmd.Transaction = tr;
                        cmdtruncate.Transaction = tr;
                        cmdtruncate.ExecuteNonQuery();

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, tr))
                        {

                            bulkCopy.ColumnMappings.Add("itemid", "itemid");
                            bulkCopy.ColumnMappings.Add("itemname", "itemname");
                            bulkCopy.ColumnMappings.Add("unit", "unit");

                            bulkCopy.DestinationTableName = "ItemsBulkUploadTemp";
                            bulkCopy.WriteToServer(dr);
                        }
                        con.Close();
                        dr.Close();
                        dr.Dispose();



                        //execute stored proc here 
                        cmd.ExecuteNonQuery();

                        tr.Commit();
                        conn.Close();

                    }

                }


                panelSuccess.Visible = true;
                panelError.Visible = false;

            }


            catch (System.Data.SqlClient.SqlException ex)
            {


                if (ex.Message.Contains("empty_column_value"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Some Columns in the excel file are empty. Please correct them first";

                }
                else if ((ex.Number == 2627) || (ex.Message.Contains("Duplicate")))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! You cannot add same itemid again. Please check your excel file and remove duplicate itemids";
                }
                //note:the above error mssges are specically placed to display the correct order



                else
                {
                    throw ex;
                }

            }

            catch (System.InvalidOperationException ex)
            {
                if (ex.Message.Contains("DBNull.Value"))
                {
                    panelError.Visible = true;
                    lblError.Text = "Error! Some Columns in the excel file are empty. Please correct them first";

                }
            }

                
            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("Error.aspx");
            }

        }

        
        protected void ExcelUploadAndSavetoDB()
        {
            try
            {
                //Check file is available in File upload Control
                if (FileUpload1.HasFile)
                {
                    string excelname = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    if (Extension.ToLower().Equals(".xlsx") || (Extension.ToLower().Equals(".xls")))
                    {
                        string uploadFolderPath = Server.MapPath(AppConns.GetUploadFolderPath());
                        //saving the exccel file in a foldeer on srver
                        FileUpload1.SaveAs(uploadFolderPath + excelname);
                        string fileUploadedName = Filename.Text = uploadFolderPath + excelname;
                        //calling the method to do the work
                        saveexcel_bulkcopy(Extension, fileUploadedName);



                    }
                    else //wrong file extension
                    {
                        panelError.Visible = true;
                        lblError.Text = "The file you are trying to upload does not seem to be an excel file.";
                    }
                }

                else
                {
                    panelError.Visible = true;
                    lblError.Text = "No excel file to upload had been selected.";

                }
            }

            catch (Exception ex)
            {
                Session["ERRORMSG"] = ex.ToString();
                Response.Redirect("Error.aspx");
            }

            finally
            {
                string path = Filename.Text;
                //Delete temporary Excel file from the Server path
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

        }

        /// <summary>
        /// button click event here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            ExcelUploadAndSavetoDB();
        }
    }
}