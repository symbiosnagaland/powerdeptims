using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class TransformerReceiptLogic
    {

        public void SaveTransformerReceiptEntry(string sqlstatement1, string sqlstatements)
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


           public static string GetDivisionNameofReceivedTransformer(int receivedtransformerid)
        {
               string divisionname ="";
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
      
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select division from transformer_receipts where challanno=(select challanno from transformer_receiptsdetails where receivedtransformerid = @receivedtransformerid)";
            cmd.Parameters.AddWithValue("@receivedtransformerid", receivedtransformerid);
            try
            {             
                conn.Open();
                if (cmd.ExecuteScalar() != DBNull.Value)
                    divisionname = cmd.ExecuteScalar().ToString();
             
            }
            catch { throw; }
            finally { conn.Close(); }

            return divisionname;

        }

        
    }
}