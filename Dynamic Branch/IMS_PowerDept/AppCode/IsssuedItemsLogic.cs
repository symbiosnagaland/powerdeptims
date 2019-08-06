using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class IsssuedItemsLogic
    {
        /// <summary>
        /// save in both tables
        /// </summary>
        /// <param name="issued">object of class containing the data for deliverychallan table(main table)</param>
        /// <param name="sqlstatements">multiple sql statmenets insert to save in details secondary table</param>  
        /// 
        

        public void SaveIssuedItems(properties issued, string sqlstatements)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

           // string stDate, edDate;
           // stDate = DateTime.ParseExact(issued.Date, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
           // edDate = DateTime.ParseExact(issued.Date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");



            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into DeliveryItemsChallan(DeliveryItemsChallanID, IndentReference, IndentDate, ChallanDate, IndentingDivisionName, ChargeableHeadName,IsDeliveredTemporary,ModifiedBy, totalamount, vehiclenumber, receiverdesignation,Remarks)values(@DeliveryItemsChallanID, @IndentReference, @IndentDate, @ChallanDate, @IndentingDivisionName, @ChargeableHeadName,@IsDeliveredTemporary, @ModifiedBy, @totalamount, @vehiclenumber, @receiverdesignation,@remarks)";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.ChallanID);
            cmd.Parameters.AddWithValue("@IndentReference", issued.IndentValue);
            cmd.Parameters.AddWithValue("@IndentDate", issued.Date   );
            cmd.Parameters.AddWithValue("@ChallanDate", issued.Date2  );
            cmd.Parameters.AddWithValue("@IndentingDivisionName", issued.Division);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", issued.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IsDeliveredTemporary", issued.IsDeliveredTemporary);
            cmd.Parameters.AddWithValue("@ModifiedBy", issued.ModifiedBy);
            cmd.Parameters.AddWithValue("@totalamount", issued.TotalAmount);
            cmd.Parameters.AddWithValue("@vehiclenumber", issued.VehicleNumber);
            cmd.Parameters.AddWithValue("@receiverdesignation", issued.ReceiverDesignation);
            cmd.Parameters.AddWithValue("@remarks", issued.Remarks);
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

        #region update issued items

        public void UpdateIssuedItems(properties issued, decimal orginalChallanID, string sqlstatements )
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Update DeliveryItemsChallan set DeliveryItemsChallanID =@DeliveryItemsChallanID, IndentReference=@IndentReference, IndentDate=@IndentDate, ChallanDate=@ChallanDate, IndentingDivisionName=@IndentingDivisionName, ChargeableHeadName=@ChargeableHeadName,IsDeliveredTemporary=@IsDeliveredTemporary,ModifiedBy=@ModifiedBy, vehiclenumber=@vehiclenumber, receiverdesignation=@receiverdesignation,Remarks=@Remarks where DeliveryItemsChallanID =@orginalChallanID";           
            cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.ChallanID);
            cmd.Parameters.AddWithValue("@IndentReference", issued.IndentValue);
            cmd.Parameters.AddWithValue("@IndentDate", issued.Date2);
            cmd.Parameters.AddWithValue("@ChallanDate", issued.Date);
            cmd.Parameters.AddWithValue("@IndentingDivisionName", issued.Division);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", issued.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IsDeliveredTemporary", issued.IsDeliveredTemporary);
            cmd.Parameters.AddWithValue("@ModifiedBy", issued.ModifiedBy);
            cmd.Parameters.AddWithValue("@orginalChallanID", orginalChallanID);
            cmd.Parameters.AddWithValue("@vehiclenumber", issued.VehicleNumber);
            cmd.Parameters.AddWithValue("@receiverdesignation", issued.ReceiverDesignation);
            cmd.Parameters.AddWithValue("@Remarks", issued.Remarks);
           
    
         //2
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = sqlstatements;

            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = "update DeliveryItemsDetails set DeliveryItemsChallanID=@DeliveryItemsChallanID where DeliveryItemsChallanID =@orginalChallanID";
            cmd3.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.ChallanID);
            cmd3.Parameters.AddWithValue("@orginalChallanID", orginalChallanID);

            try
            {
              
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd2.Transaction = tr;
                    cmd3.Transaction = tr;

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
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


        #region update issued items

        public void UpdateIssuedItems(properties issued, decimal orginalChallanID)
        {

            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            //this will execute first

            string stDate, edDate;
            stDate = DateTime.ParseExact(issued.Date , "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            edDate = DateTime.ParseExact(issued.Date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Update DeliveryItemsChallan set DeliveryItemsChallanID =@DeliveryItemsChallanID, IndentReference=@IndentReference, IndentDate=@IndentDate, ChallanDate=@ChallanDate, IndentingDivisionName=@IndentingDivisionName, ChargeableHeadName=@ChargeableHeadName,IsDeliveredTemporary=@IsDeliveredTemporary,ModifiedBy=@ModifiedBy, vehiclenumber=@vehiclenumber, receiverdesignation=@receiverdesignation,Remarks=@Remarks1  where DeliveryItemsChallanID =@orginalChallanID";
            cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.ChallanID);
            cmd.Parameters.AddWithValue("@IndentReference", issued.IndentValue);
            cmd.Parameters.AddWithValue("@IndentDate", edDate);
            cmd.Parameters.AddWithValue("@ChallanDate", stDate);
            cmd.Parameters.AddWithValue("@IndentingDivisionName", issued.Division);
            cmd.Parameters.AddWithValue("@ChargeableHeadName", issued.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IsDeliveredTemporary", issued.IsDeliveredTemporary);
            cmd.Parameters.AddWithValue("@ModifiedBy", issued.ModifiedBy);
            cmd.Parameters.AddWithValue("@orginalChallanID", orginalChallanID);
            cmd.Parameters.AddWithValue("@vehiclenumber", issued.VehicleNumber );
            cmd.Parameters.AddWithValue("@receiverdesignation", issued.ReceiverDesignation);
            cmd.Parameters.AddWithValue("@Remarks1", issued.Remarks );
      

            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = "update DeliveryItemsDetails set DeliveryItemsChallanID=@DeliveryItemsChallanID where DeliveryItemsChallanID =@orginalChallanID";
            cmd3.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.ChallanID);
            cmd3.Parameters.AddWithValue("@orginalChallanID", orginalChallanID);

            try
            {
              
                    conn.Open();
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;               
                    cmd3.Transaction = tr;
                    cmd.ExecuteNonQuery();                 
                    cmd3.ExecuteNonQuery();
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

        



        #region get issue heads, items and chargeable heads for edit pirpose
        public static DataSet RetrieveActive_CHHeads_Items_Divisions()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();
            //  DataTable dt = new DataTable();
            string cmd = "SELECT DISTINCT ChargeableHeadName FROM ChargeableHeads where status='A';";
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

    }
}