using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class ReceivedItemsLogic
    {



        //public static DataTable RetrieveActiveIssueHeads()
        //{
        //    SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
        //    DataTable dt = new DataTable();
        //    string cmd = "SELECT issueheadid, issueheadname FROM issueheads where status='A';";
        //    try
        //    {

        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);
        //        adapter.Fill(dt);
        //    }
        //    catch { throw; }

        //    return dt;

   

        //    }
        /// <summary>
        /// retrieving the named values seperately
        /// </summary>
        /// <returns></returns>
        ///  public static void RetrieveActiveIssueHeadsAndActiveItemsSeperately(out int maxOTEOID)
        public static DataSet RetrieveActiveIssueHeadsAndActiveItemsSeperately(out int maxOTEOID)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();
            //  DataTable dt = new DataTable();
            string cmd = "SELECT issueheadid, issueheadname FROM issueheads where status='A';";
            //retrive item names
            string cmd2 = "SELECT CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit,itemname FROM Items where status='A'";

            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = "select max(ReceivedItemsOTEOID) from receiveditemsoteo";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);
                //fill 
                dst.Tables.Add("IssueHeads"); dst.Tables.Add("Items");

                adapter.Fill(dst.Tables[0]);
                adapter = new SqlDataAdapter(cmd2, conn);
                adapter.Fill(dst.Tables[1]);
                conn.Open();
                if (cmd3.ExecuteScalar() != DBNull.Value)
                    maxOTEOID = Convert.ToInt32(cmd3.ExecuteScalar()) + 1;
                else
                    maxOTEOID = 1;
            }
            catch { throw; }
            finally { conn.Close(); }

            return dst;

        }

        #region get issue heads, items and chargeable heads for edit pirpose
        public static DataSet   RetrieveActive_IssueHeads_Items_ChargeableHeads()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();
            //  DataTable dt = new DataTable();
            string cmd = "SELECT issueheadid, issueheadname FROM issueheads where status='A';";
            //retrive item names
            string cmd2 = "SELECT CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit,itemname FROM Items where status='A'";

            
            string cmd3 = "SELECT chargeableheadid, chargeableheadname FROM chargeableheads where status='A'";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);
                //fill 
                dst.Tables.Add("IssueHeads"); dst.Tables.Add("Items"); dst.Tables.Add("ChargeableHeads");

                adapter.Fill(dst.Tables[0]);
                //2nd table
                adapter = new SqlDataAdapter(cmd2, conn);
                adapter.Fill(dst.Tables[1]);
                //3rd table
                adapter = new SqlDataAdapter(cmd3, conn);
                adapter.Fill(dst.Tables[2]);

               
            }
            catch { throw; }
            finally { conn.Close(); }

            return dst;

        }
        #endregion


        /// <summary>
        /// for receieved entry page, get active chargeable heads , corresponding issue head and active items seperately
        /// </summary>
        /// <returns></returns>
        ///  public static void RetrieveActiveIssueHeadsAndActiveItemsSeperately(out int maxOTEOID)
        public static DataSet RetrieveActiveChargeableHeadsAndActiveItemsSeperately(out int maxOTEOID)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();
            //  DataTable dt = new DataTable();
           // string cmd = "SELECT ChargeableHeadName, (CONVERT(VARCHAR(10), chargeableheadid) + '*' + issueheadname + '*' + CONVERT(VARCHAR(10), issueheadid)) as chheadid_issueheadname_id  FROM ChargeableHeads where status='A';";

            string cmd = "SELECT ChargeableHeadName, chargeableheadid  FROM ChargeableHeads where status='A';";
 
            //retrive item names
            string cmd2 = "SELECT CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit,itemname FROM Items where status='A'";

            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = "select max(ReceivedItemsOTEOID) from receiveditemsoteo";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);
                //fill 
                dst.Tables.Add("ChargeableHeads"); dst.Tables.Add("Items");

                adapter.Fill(dst.Tables[0]);
                adapter = new SqlDataAdapter(cmd2, conn);
                adapter.Fill(dst.Tables[1]);
                conn.Open();
                if (cmd3.ExecuteScalar() != DBNull.Value)
                    maxOTEOID = Convert.ToInt32(cmd3.ExecuteScalar()) + 1;
                else
                    maxOTEOID = 1;
            }
            catch { throw; }
            finally { conn.Close(); }

            return dst;

        }

        /// <summary>
        /// get all items once received in received entry and not from master data items
                /// </summary>
        /// <returns></returns>
        public static DataSet RetrieveReceivedItemsAndReceivedItemsDetails()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();    
            
            //
            //string cmd = "SELECT ItemID, ItemName, unit, Quantity, Rate, amount, IssueHeadName FROM  view_ReceivedItems_Tables";//v1 
            //v.2 - bringing itemname - rate - netactual balance
            string cmd = "exec sp_ItemsEnquiryList";
            //retrive item names          
            string cmd2 = "select distinct itemname, CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit from ReceivedItemsDetails";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, conn);
                //fill 
               //v1-  dst.Tables.Add("ReceivedItemsDetails"); 
                dst.Tables.Add("ItemsEnquiryListTable"); 
                dst.Tables.Add("Items");

                adapter.Fill(dst.Tables[0]);
                adapter = new SqlDataAdapter(cmd2, conn);
                adapter.Fill(dst.Tables[1]);
            }
            catch { throw; }

            return dst;

        }

        public static DataTable RetrieveCorrespondingActiveChargeableHeads(int intIssueHead)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataTable dt = new DataTable();
            string cmdText = "SELECT chargeableheadid, chargeableheadname FROM chargeableheads where  issueheadid=@issueheadid and status='A'";
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
                adapter.SelectCommand.Parameters.Add("@issueheadid", SqlDbType.SmallInt).Value = intIssueHead;
                adapter.Fill(dt);
            }
            catch { throw; }

            return dt;

        }

        #region save to db received item oteo and received items details
        /// <summary>
        /// Save Received item oteo and items table data
        /// </summary>
        /// <param name="RecievedItemsOrderObject"></param>
        /// <param name="sqlstatements"></param>
        public void SaveReceivedItemsDetails(properties RecievedItemsOrderObject, string sqlstatements)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_InsertReceivedItemsAfterValidation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReceivedItemsOTEOID", RecievedItemsOrderObject.ReceivedItemsOTEOID);
            cmd.Parameters.AddWithValue("@ReceivedItemOTEODate", RecievedItemsOrderObject.Date);
            cmd.Parameters.AddWithValue("@SupplyOrderReference", RecievedItemsOrderObject.SupplyOderRef);
            cmd.Parameters.AddWithValue("@SupplyOrderDate", RecievedItemsOrderObject.SupplyDate);
            cmd.Parameters.AddWithValue("@Supplier", RecievedItemsOrderObject.Supplier);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", RecievedItemsOrderObject.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IssueHeadName", RecievedItemsOrderObject.IssueHeadName);
            cmd.Parameters.AddWithValue("@TotalAmount", RecievedItemsOrderObject.TotalAmount);
            cmd.Parameters.AddWithValue("@ModifiedBy", RecievedItemsOrderObject.ModifiedBy);

            //    cmd.Parameters.AddWithValue("@XMLItems", XMLItems);


            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = sqlstatements;

            try
            {
              
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    tr.Commit();




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
        }
        #endregion


        #region update to db received item oteo and received items details
        /// <summary>
        /// Save Received item oteo and items table data
        /// </summary>
        /// <param name="RecievedItemsOrderObject"></param>
        /// <param name="sqlstatements"></param>
        public void UpdateReceivedItemsDetails(properties RecievedItemsOrderObject,  int originaloteoid, string sqlstatements)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_UpdateReceivedItemsOTEO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReceivedItemsOTEOID", RecievedItemsOrderObject.ReceivedItemsOTEOID);
            cmd.Parameters.AddWithValue("@ReceivedItemOTEODate", RecievedItemsOrderObject.Date);
            cmd.Parameters.AddWithValue("@SupplyOrderReference", RecievedItemsOrderObject.SupplyOderRef);
            cmd.Parameters.AddWithValue("@SupplyOrderDate", RecievedItemsOrderObject.SupplyDate);
            cmd.Parameters.AddWithValue("@Supplier", RecievedItemsOrderObject.Supplier);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", RecievedItemsOrderObject.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IssueHeadName", RecievedItemsOrderObject.IssueHeadName);
            cmd.Parameters.AddWithValue("@TotalAmount", RecievedItemsOrderObject.TotalAmount);
            cmd.Parameters.AddWithValue("@ModifiedBy", RecievedItemsOrderObject.ModifiedBy);
            cmd.Parameters.AddWithValue("@OriginalReceivedItemsOTEOID", originaloteoid);
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = sqlstatements;

            try
            {
                 conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    tr.Commit();
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
        }
        #endregion

        #region update to db received item oteo and received items details
        /// <summary>
        /// Save Received item oteo and items table data
        /// </summary>
        /// <param name="RecievedItemsOrderObject"></param>
        /// <param name="sqlstatements"></param>
        public void UpdateReceivedItemsDetails(properties RecievedItemsOrderObject, int originaloteoid)
        {

            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_UpdateReceivedItemsOTEO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReceivedItemsOTEOID", RecievedItemsOrderObject.ReceivedItemsOTEOID);
            cmd.Parameters.AddWithValue("@ReceivedItemOTEODate", RecievedItemsOrderObject.Date);
            cmd.Parameters.AddWithValue("@SupplyOrderReference", RecievedItemsOrderObject.SupplyOderRef);
            cmd.Parameters.AddWithValue("@SupplyOrderDate", RecievedItemsOrderObject.SupplyDate);
            cmd.Parameters.AddWithValue("@Supplier", RecievedItemsOrderObject.Supplier);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", RecievedItemsOrderObject.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IssueHeadName", RecievedItemsOrderObject.IssueHeadName);
            cmd.Parameters.AddWithValue("@TotalAmount", RecievedItemsOrderObject.TotalAmount);
            cmd.Parameters.AddWithValue("@ModifiedBy", RecievedItemsOrderObject.ModifiedBy);
            cmd.Parameters.AddWithValue("@OriginalReceivedItemsOTEOID", originaloteoid);

            try
            {
                 conn.Open();
                    cmd.ExecuteNonQuery(); 
                
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


    }
}