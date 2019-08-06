using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS_PowerDept.AppCode
{
    public class Utilities
    {

        #region Write Log with userid

        public static void WriteLog(bool isError, String pStrEmpID, String pStrEvent, string previousPageURL)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(AppConns.GetConnectionString());
            }
            catch
            {
                throw;
            }

            SqlCommand cmdlog = conn.CreateCommand();

            //insert into table
            cmdlog.CommandText = "insert LOGS (ISERROR, EVENT,PREVIOUSPAGEURL, SYSTEMUSERID,MODON) values (@ISERROR, @EVENT,@PREVIOUSPAGEURL,@SYSTEMUSERID,@MODON)";
            cmdlog.Parameters.AddWithValue("@USERID", isError);
            cmdlog.Parameters.AddWithValue("@EVENT", pStrEvent);
            cmdlog.Parameters.AddWithValue("@PREVIOUSPAGEURL", previousPageURL);
            cmdlog.Parameters.AddWithValue("@SYSTEMUSERID", pStrEmpID);
            cmdlog.Parameters.AddWithValue("@MODON", System.DateTime.Now);


            try
            {
                conn.Open();
                cmdlog.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        #endregion

    public static void ClearAllControls(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;

                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).SelectedIndex = -1;
                ClearAllControls(ctrl.Controls);
            }
        }


    public static String ValidSql(String s)
    {
        
        try
        {
            if (s.Length > 0)
            {
                //for (int i = 0; i < s.Length; i++)
                //{

                //    if (s.Substring(i, 1) == "'")
                //    {
                //        temp = temp + "'";
                //    }

                //    temp = temp + s.Substring(i, 1);
                //}

                s = s.Replace("'", "''");

                return (s.Trim());
            }
        }
        catch
        {
            throw;
        }

        return (s);
    }
    //Function to encode the string
    static public string TamperProofStringEncode(string value, string key)
    {
        System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
        return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
    }

    //Function to decode the string
    //Throws an exception if the data is corrupt
    static public string TamperProofStringDecode(string value, string key)
    {
        String dataValue = "";
        String calcHash = "";
        String storedHash = "";

        System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

        try
        {
            dataValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[0]));
            storedHash = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[1]));
            calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

            if (storedHash != calcHash)
            {
                //Data was corrupted
                throw new ArgumentException("Hash value does not match");
                //This error is immediately caught below
            }

        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid TamperProofString");
        }
        return dataValue;
    }

    static public string QueryStringEncode(string value)
    {
        return System.Web.HttpUtility.UrlEncode(TamperProofStringEncode(value, "jafsdg3938lg"));
    }

    static public string QueryStringDecode(string value)
    {
        return TamperProofStringDecode(value,"jafsdg3938lg");
    }

          public static void ExecuteSQLQueries(string sqlstatement1, string sqlstatement2)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlstatement1;
            
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = sqlstatement2;

            try
            {
                using (conn)
                {
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    tr.Commit();
                }
            }
            catch
            {
                //  tr.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    }
