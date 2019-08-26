using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS_PowerDept.AppCode
{
    public class NewProperties
    {
        protected int ChallanNO;
        public int challanNO
        {
            get { return ChallanNO; }
            set { ChallanNO = value; }
        }

        protected string ChallanDate;
        public string challanDate
        {
            get { return ChallanDate; }
            set { ChallanDate = value; }
        }


        protected string IndentNo;
        public String indentNo
        {
            get { return IndentNo; }
            set { IndentNo = value; }
        }

        protected string IndentDate;
        public String indentDate
        {
            get { return IndentDate; }
            set { IndentDate = value; }
        }


        protected string IntendingDivision;
        public string intendingDivision
        {
            get { return IntendingDivision; }
            set { IntendingDivision = value; }
        }

        protected string chargeableHeadName;
        public String ChargeableHeadName
        {
            get { return chargeableHeadName; }
            set { chargeableHeadName = value; }
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