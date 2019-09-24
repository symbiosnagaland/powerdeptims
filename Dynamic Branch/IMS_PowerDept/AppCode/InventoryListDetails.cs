using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace IMS_PowerDept.AppCode
{
    public class InventoryListDetails
    {
        protected String _ItemsInventoryID;
        protected String _ItemID;
        protected String _ItemName;
        protected String _IssueHeadName;
        protected String _GrossBalance;
        protected String _TemporaryIssued;
        protected String _NetActualBalance;
        protected String _MinimumUnitIndicator;
        protected String _ModifiedOn;

      
        public string ItemsInventoryID
        {
            get { return _ItemsInventoryID; }
            set { _ItemsInventoryID = value; }
        }

        public string ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public string IssueHeadName
        {
            get { return _IssueHeadName; }
            set { _IssueHeadName = value; }
        }

        public string GrossBalance
        {
            get { return _GrossBalance; }
            set { _GrossBalance = value; }
        }

        public string TemporaryIssued
        {
            get { return _TemporaryIssued; }
            set { _TemporaryIssued = value; }
        }

        public string NetActualBalance
        {
            get { return _NetActualBalance; }
            set { _NetActualBalance = value; }
        }
        public string MinimumUnitIndicator
        {
            get { return _MinimumUnitIndicator; }
            set { _MinimumUnitIndicator = value; }
        } 
        public string ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
      

        //public InventoryListDetails(String pStrIssueHeadName)
        //{
        //    SqlConnection conn = null;

        //    try
        //    {
        //        conn = new SqlConnection(AppConns.GetConnectionString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    SqlCommand cmd = conn.CreateCommand();
        //    cmd.CommandText = "select a.itemsinventoryid ItemsInventoryID, a.ItemID ItemID,a.ItemName ItemName, a.IssueHeadName IssueHeadName, a.GrossBalance GrossBalance, a.TemporaryIssued TemporaryIssued, a.NetActualBalance NetActualBalance, a.MinimumUnitIndicator MinimumUnitIndicator, a.ModifiedOn ModifiedOn from [ItemsInventory] where IssueHeadName=@pStrIssueHeadName";
            
        //    cmd.Parameters.AddWithValue("@pStrIssueHeadName", pStrIssueHeadName);

        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            _ItemsInventoryID = dr["ItemsInventoryID"].ToString();
        //            _ItemID = dr["ItemID"].ToString();
        //            _ItemName = dr["ItemName"].ToString();
        //            _IssueHeadName = dr["IssueHeadName"].ToString();
        //            // _IssueHeadName = DropDownList1.SelectedValue.ToString();
        //            _GrossBalance = dr["GrossBalance"].ToString();
        //            _TemporaryIssued = dr["TemporaryIssued"].ToString();
        //            _NetActualBalance = dr["NetActualBalance"].ToString();
        //            _MinimumUnitIndicator = dr["MinimumUnitIndicator"].ToString();
        //            _ModifiedOn = dr["ModifiedOn"].ToString();
                    
        //        }
        //        dr.Close();
        //        conn.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }
}