using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class TransformerLogic
    {

        public void SaveEntries(string sqlstatement1, string sqlstatements)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlstatement1;
            
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = sqlstatements;

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