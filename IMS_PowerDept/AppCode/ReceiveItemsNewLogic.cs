using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace IMS_PowerDept.AppCode
{
    public class ReceiveItemsNewLogic
    {
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
            finally
            {
                conn.Close();
            }

            return dt;

        }

        public static DataSet RetrieveAllItems()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dst = new DataSet();

            //reterive issue HEadNames
            string cmd = "select distinct ItemName,issueheadname from ItemsRateSecondary  order by itemname";
            //retrive item names          
            string cmd2 = "select  itemname, CONVERT(VARCHAR(10), itemid) + ' ' + unit as itemid_unit from items";

            string cmd3 = "select itemname,issueheadname,rate, max(OrderNo) as MaxOrderNo from ItemsRateSecondary IT2 group by itemname,issueheadname,rate  order by MaxOrderNo desc";

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
    }
}