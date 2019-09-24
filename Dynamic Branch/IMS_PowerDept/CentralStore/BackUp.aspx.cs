using IMS_PowerDept.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IMS_PowerDept.CentralStore
{
    public partial class BackUp : System.Web.UI.Page
    {
        string _DatabaseName = "PowerDeptNagalandIMS"; 
        string backupFolderPath =  HttpContext.Current.Request.ApplicationPath + "\\DBBackup\\";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayBackupFileNames();                
            }
        }

        protected void btnTakeBackup_Click(object sender, EventArgs e)
        {

            TakeDBBackup();
            DisplayBackupFileNames();
        }

        #region take backup action
        private void TakeDBBackup()
        {
            try
            {
                string _DatabaseName = "PowerDeptNagalandIMS";
                string _BackupName = _DatabaseName + "_" + DateTime.Now.ToString("ddMMMMyyyy_hh_mm_tt") + ".bak";
            //    string _BackupName = _DatabaseName + ".bak";

                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = AppConns.GetConnectionString();              
                string backupFolderPath = Server.MapPath(HttpContext.Current.Request.ApplicationPath +"\\DBBackup\\");
               string sqlQuery = "Backup database [" + _DatabaseName + "] to disk='" + backupFolderPath + _BackupName +"'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlConnection.Open();
                int iRows = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                lblmessage.Text = "Back up created successfully.";
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion


        #region restore backup
        /// <summary>
        /// restore db to selected file but first please supply the backupfilename
        /// </summary>
        /// <param name="backupDBName">i need the backup file name</param>
        private void RestoreDBBackup(string backupDBName)
        {
            try
            {
                            
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = AppConns.GetMasterConnectionString();
                // string backupFolderPath = Server.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\DBBackup\\";                   //"DROP DATABASE " + _DatabaseName + 
                string backupFolderPath = Server.MapPath(HttpContext.Current.Request.ApplicationPath +"\\DBBackup\\");
                string sqlQuery = " Alter Database " +_DatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;  RESTORE DATABASE " + _DatabaseName + " FROM DISK = '" + backupFolderPath + backupDBName + "';  alter database " + _DatabaseName + " set multi_user";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlConnection.Open();
                int iRows = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion


        #region display backed up file names in a list
        private void DisplayBackupFileNames()
        {
            //will clear the listbox first
            listBoxBackupFiles.Items.Clear();

            backupFolderPath = Server.MapPath(HttpContext.Current.Request.ApplicationPath + "\\DBBackup\\");
                DirectoryInfo d = new DirectoryInfo(@"" + backupFolderPath);
                FileInfo[] Files = d.GetFiles("*.bak").OrderByDescending(p => p.CreationTime).ToArray();//only bak files   and order by time           
	           foreach (FileInfo file in Files)
	            {
	               listBoxBackupFiles.Items.Add(file.Name);
	            }      
           }
        
        #endregion

        #region restore button click event
        protected void btnRestore_Click(object sender, EventArgs e)
        {
             if (listBoxBackupFiles.SelectedValue != "")
            {
              //1 before restoreing first take the latest backup
                TakeDBBackup();
                 // 2 then only restore
            RestoreDBBackup(listBoxBackupFiles.SelectedItem.ToString());
            lblmessage.Text = "Database Restored Successfully";
                 //3 also display latest filenames
            DisplayBackupFileNames();
            }
             else
             {

                 lblmessage.Text = "<font color='Red'> Error! Please select backup name to restore first.</font>";
             }
        }
        #endregion

        #region delete backup 
        /// <summary>
/// 
/// </summary>
/// <param name="backupNamewithPath"></param>
/// <returns></returns>
        private bool DeleteBackUp(string backupNamewithPath)
        {
            try
            {
                // A.
                // Try to delete the file.
                File.Delete(backupNamewithPath);
                return true;
            }
            catch (IOException)
            {
                // B.
                // We could not delete the file.
                return false;
            }
        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxBackupFiles.SelectedValue != "")
            {
                backupFolderPath = Server.MapPath(backupFolderPath);
                DeleteBackUp(backupFolderPath + listBoxBackupFiles.SelectedItem.ToString());
                lblmessage.Text = "Selected Backup Deleted Successfully";
                DisplayBackupFileNames();
            }
            else
            {
                
                lblmessage.Text = "<font color='Red'> Error! Please select backup name to delete first.</font>";
            }

        }

   
    }
}