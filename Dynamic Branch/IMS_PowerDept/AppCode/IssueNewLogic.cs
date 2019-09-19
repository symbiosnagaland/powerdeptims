using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace IMS_PowerDept.AppCode
{
    public class IssueNewLogic
    {
        public static DataSet RetrieveAllItems()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();

            //reterive issue HEadNames
            string cmd = "select distinct ItemName,issueheadname from ItemsRateSecondary ";
            //retrive item names          
            string cmd2 = "select  itemname, CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit from items";

            string cmd3 = "select itemname,issueheadname,Rate,OrderNo,Quantity,cast((Rate*Quantity)as decimal(10,2))  AS AMT from ItemsRateSecondary IT2 where quantity>0 order by OrderNo,rate";

            string cmd4 = "select sum(quantity) TotQtyAvailable, itemname,IssueHeadName  from ItemsRateSecondary group by itemname,IssueHeadName";

            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd2, conn); 
                dst.Tables.Add("Items");
                adapter.Fill(dst.Tables["Items"]);

                adapter.SelectCommand.CommandText = cmd;
                dst.Tables.Add("itemRatesSecondery");
                adapter.Fill(dst.Tables["itemRatesSecondery"]);

                adapter.SelectCommand.CommandText = cmd3;
                dst.Tables.Add("IT2");
                adapter.Fill(dst.Tables["IT2"]);

                adapter.SelectCommand.CommandText = cmd4;
                dst.Tables.Add("IT3");
                adapter.Fill(dst.Tables["IT3"]);
                conn.Close();
            }
            catch { throw; }

            return dst;

        }

        public void SaveNewIssuedItems(NewProperties  issued, string sqlstatements)
        {
            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
      
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into DeliveryItemsChallan(DeliveryItemsChallanID, IndentReference, IndentDate, ChallanDate, IndentingDivisionName, ChargeableHeadName,IsDeliveredTemporary,ModifiedBy, totalamount, vehiclenumber, receiverdesignation,Remarks)values(@DeliveryItemsChallanID, @IndentReference, @IndentDate, @ChallanDate, @IndentingDivisionName, @ChargeableHeadName,@IsDeliveredTemporary, @ModifiedBy, @totalamount, @vehiclenumber, @receiverdesignation,@remarks)";
          
            cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.challanNO );
            cmd.Parameters.AddWithValue("@IndentReference", issued.indentNo);
            cmd.Parameters.AddWithValue("@IndentDate", issued.indentDate);
            cmd.Parameters.AddWithValue("@ChallanDate", issued.challanDate );
            cmd.Parameters.AddWithValue("@IndentingDivisionName", issued.intendingDivision );
            cmd.Parameters.AddWithValue("@ChargeableHeadName", issued.ChargeableHeadName);
            cmd.Parameters.AddWithValue("@IsDeliveredTemporary", issued.IsDeliveredTemporary );
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

        public void UpdateNewIssuedItems(NewProperties issued, string sqlstatements)
        {
            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());

            SqlCommand cmd = conn.CreateCommand();
           // cmd.CommandText = "Insert into DeliveryItemsChallan(DeliveryItemsChallanID, IndentReference, IndentDate, ChallanDate, IndentingDivisionName, ChargeableHeadName,IsDeliveredTemporary,ModifiedBy, totalamount, vehiclenumber, receiverdesignation,Remarks)values(@DeliveryItemsChallanID, @IndentReference, @IndentDate, @ChallanDate, @IndentingDivisionName, @ChargeableHeadName,@IsDeliveredTemporary, @ModifiedBy, @totalamount, @vehiclenumber, @receiverdesignation,@remarks)";

            cmd.CommandText = "Update DeliveryItemsChallan set  IndentReference=@IndentReference, IndentDate=@IndentDate,"+
                " ChallanDate= @ChallanDate, IndentingDivisionName=@IndentingDivisionName, ChargeableHeadName= @ChargeableHeadName,"+
                "IsDeliveredTemporary=@IsDeliveredTemporary,ModifiedBy= @ModifiedBy, totalamount=@totalamount, vehiclenumber= @vehiclenumber, "+
                "receiverdesignation=@receiverdesignation,Remarks=@remarks where DeliveryItemsChallanID=@DeliveryItemsChallanID ";

            

            cmd.Parameters.AddWithValue("@DeliveryItemsChallanID", issued.challanNO);
            cmd.Parameters.AddWithValue("@IndentReference", issued.indentNo);
            cmd.Parameters.AddWithValue("@IndentDate", issued.indentDate);
            cmd.Parameters.AddWithValue("@ChallanDate", issued.challanDate);
            cmd.Parameters.AddWithValue("@IndentingDivisionName", issued.intendingDivision);
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
                    if(sqlstatements !="")
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    
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
                //conn.Close();
            }
        }
    }
}