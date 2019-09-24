using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace IMS_PowerDept.UserControls
{
    public partial class Dash : System.Web.UI.UserControl
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ConnectionString);
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                retrivedivsions();
                retriveissuehead();
                retriveChHead();
                totalitems();
                totalissueditems();
                totalReveiveditem();               
                totalIssueAmount();
            }
            catch
            { throw; }
        }

        private void retrivedivsions()
        {
            try
            {
                con.Open();
                str = " select count (divisionName) as Division from Divisions";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    div.Text = reader["Division"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }

        }

        //issuehead

        private void retriveissuehead()
        {
            try
            {
                con.Open();
                str = " select count (IssueHeadName) as Ihead from IssueHeads";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    headI.Text = reader["Ihead"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }

        }

        //chhead

        private void retriveChHead()
        {
            try
            {
                con.Open();
                str = " select count (ChargeableHeadName) as ChHead from ChargeableHeads";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    chhead.Text = reader["ChHead"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }

        }

        //totalItems

        private void totalitems()
        {
            try
            {
                con.Open();
                str = " select count (itemname) as Titems from Items";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    item.Text = reader["Titems"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }
        }

        //total items issued
        //select count (ItemName) from [DeliveryItemsDetails]

        

        private void totalissueditems()
        {
            try
            {
                con.Open();
                str = "select count (DeliveryItemsChallanID) as ttItems from DeliveryItemsChallan";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    issueditems.Text = reader["ttItems"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }
        }


        //total received item
        private void totalReveiveditem()
        {
            try
            {
                con.Open();
                str = "select count (ReceivedItemsOTEOID) as ReceivedItemsDetails from ReceivedItemsOTEO";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    ttrcitm.Text = reader["ReceivedItemsDetails"].ToString();
                    reader.Close();
                    con.Close();
                }
            }
            catch
            { throw; }
        }


        //total issue amount

        private void totalIssueAmount()
        {
            try
            {
                //    con.Open();
                //    str = "SELECT SUM(Rate) AS Sum FROM DeliveryItemsDetails";
                //    com = new SqlCommand(str, con);
                //    SqlDataReader reader = com.ExecuteReader();
                //    if (reader.Read())
                //    {

                //        issamount.Text = reader["Sum"].ToString();
                //        reader.Close();
                //        con.Close();
                //    }
            }
            catch
            { throw; }
        }

    }
}