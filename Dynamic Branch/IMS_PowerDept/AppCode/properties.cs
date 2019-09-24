using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class properties
    {


        protected int receivedItemsOTEOID;
        public int ReceivedItemsOTEOID
        {
            get { return receivedItemsOTEOID; }
            set { receivedItemsOTEOID = value; }
        }

        protected string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }


        protected string supplyOrderRef;
        public String SupplyOderRef
        {
            get { return supplyOrderRef; }
            set { supplyOrderRef = value; }
        }

        protected string supplyDate;
        public String SupplyDate
        {
            get { return supplyDate; }
            set { supplyDate = value; }
        }


        protected string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        protected string chargeableHeadName;
        public String ChargeableHeadName
        {
            get { return chargeableHeadName; }
            set { chargeableHeadName = value; }
        }

        protected string issueHeadName;
        public string IssueHeadName
        {
            get { return issueHeadName; }
            set { issueHeadName = value; }
        }

        protected double totalAmount;
        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        protected string IsDeliveredTemp;
        public string IsDeliveredTemporary
        {
            get { return IsDeliveredTemp; }
            set { IsDeliveredTemp = value; }
        }

        protected Int16 modifiedBy;
        public Int16 ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }



        protected decimal challanid;
        public decimal ChallanID
        {
            get { return challanid; }
            set { challanid = value; }
        }

        protected string indentValue;
        public String IndentValue
        {
            get { return indentValue; }
            set { indentValue = value; }
        }

        protected string date2;
        public string Date2
        {
            get { return date2; }
            set { date2 = value; }
        }

        protected string division;
        public String Division
        {
            get { return division; }
            set { division = value; }
        }

        protected string vehicenumber;
        public string VehicleNumber
        {
            get { return vehicenumber; }
            set { vehicenumber = value; }
        }

        protected string receiverdesignation;
        public String ReceiverDesignation
        {
            get { return receiverdesignation; }
            set { receiverdesignation = value; }
        }

        protected string remarks;
        public String Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }



    }
}