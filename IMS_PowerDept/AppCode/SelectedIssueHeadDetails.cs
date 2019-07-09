using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace IMS_PowerDept.AppCode
{
    public class SelectedIssueHeadDetails
    {
        public static DataSet GetSelectedIssueHeadDetails(String pStrIssueHeadName)
        {
      
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());      
            DataSet dt = new DataSet();
            try
            {
                SqlDataAdapter dad = new SqlDataAdapter("sp_IssueHeadWiseDetailedInventory '" + pStrIssueHeadName + "'", conn);
            dad.Fill(dt);        
          }
          catch (Exception ex)
          {
              ex.ToString();

          }
          finally
          {
              conn.Close();
          }
          return (dt);
        }
        public static DataSet GetSelectedItemNameDetails(String pStrItemName)
        {

            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dt = new DataSet();
            try
            {
                SqlDataAdapter dad = new SqlDataAdapter("sp_GetDetailedInventoryWithRateByItemName'" + pStrItemName + "'", conn);
                dad.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                conn.Close();
            }
            return (dt);
        }

        public static DataSet GetDetailsOfSelectedItemName(String pStrItemName)
        {

            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataSet dt = new DataSet();
            try
            {
                SqlDataAdapter dad = new SqlDataAdapter("sp_GetDetailsByItemName '" + pStrItemName + "'", conn);
                dad.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                conn.Close();
            }
            return (dt);
        }
      
        public static DataSet GetAllDetails()
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());        
           DataSet dt = new DataSet();
           try
           {
           SqlDataAdapter dad = new SqlDataAdapter("sp_GetDetailedInventory", conn);
            dad.Fill(dt);         
          }
          catch (Exception ex)
          {
              ex.ToString();

          }
          finally
          {
              conn.Close();
          }
          return (dt);
        }

        public static DataTable GetAllDetailsByDate(string pDate)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataTable dt = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetDetailedInventoryByDate @date ";
            //  cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@date", pDate);
           // cmd.Parameters.AddWithValue("@todate", @pToDate);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr); dr.Close();

            }
            catch (Exception ex)
            {
                // ex.ToString();
                throw;

            }
            finally
            {
                conn.Close();
            }
            return (dt);
        }


        //public static DataTable GetAllDetailsByDates(string pFromDate, string pToDate)
        //{
        //    SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
        //    DataTable dt = new DataTable();
        //     SqlCommand cmd = conn.CreateCommand();
        //     cmd.CommandText = "sp_GetDetailedInventoryByDates @startdate, @todate ";
        //  //  cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@startdate", @pFromDate);
        //    cmd.Parameters.AddWithValue("@todate", @pToDate);
           
        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dt.Load(dr);                dr.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //       // ex.ToString();
        //        throw;

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return (dt);
        //}


        public static DataTable GetSelectedIssueHeadDetails(String pStrIssueHeadName, string pDate)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataTable dt = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetDetailedInventoryByIssueHeadandDate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IssueHeadName1", pStrIssueHeadName);

            cmd.Parameters.AddWithValue("@date", pDate );
          

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr); dr.Close();

            }
            catch (Exception ex)
            {
                // ex.ToString();
                throw;

            }
            finally
            {
                conn.Close();
            }
            return (dt);
        }

        public static DataTable GetSelectedIssueHeadDetails(String pStrIssueHeadName, string pFromDate, string pToDate)
        {
            SqlConnection conn = new SqlConnection(AppConns.GetConnectionString());
            DataTable dt = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetDetailedInventoryByIssueHeadandDates";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IssueHeadName1", pStrIssueHeadName);

            cmd.Parameters.AddWithValue("@startdate", @pFromDate);
            cmd.Parameters.AddWithValue("@todate", @pToDate);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr); dr.Close();

            }
            catch (Exception ex)
            {
                // ex.ToString();
                throw;

            }
            finally
            {
                conn.Close();
            }
            return (dt);
        }
    }
}
