using System;
using System.Data;

namespace Epicoil.Library.Models.Sales
{
    public class OrderHeadModel
    {
        public int OrderNum { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequestDate { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string EndUserCode { get; set; }

        public string EndUserName { get; set; }

        public string ShipTo { get; set; }

        public string ShipToName { get; set; }

        public string CustPO { get; set; }

        public string SOCode { get; set; }

        public string SOCodeName { get; set; }

        public string Term { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public string OrderType { get; set; }

        public string OrderName { get; set; }

        public string PIC { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalAmount { get; set; }

        public void DataBind(DataRow row)
        {
            this.OrderNum = (int)row["OrderNum"].GetInt();
            this.OrderNumber = (string)row["OrderNum"].GetString();
            this.OrderDate = (DateTime)row["OrderDate"].GetDate();
            this.RequestDate = (DateTime)row["RequestDate"].GetDate();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.EndUserCode = (string)row["EndUserCode"].GetString();
            this.EndUserName = (string)row["EndUserName"].GetString();
            this.ShipTo = (string)row["ShipTo"].GetString();
            this.ShipToName = (string)row["ShipToName"].GetString();
            this.CustPO = string.IsNullOrEmpty((string)row["PONum"].GetString()) ? "" : (string)row["PONum"].GetString();
            this.SOCode = (string)row["SOCode"].GetString();
            this.SOCodeName = (string)row["SOCodeName"].GetString();
            //this.Term = (string)row["TermsCode"].GetString();
            //this.BussinessType = (string)row["BussinessType"].GetString();
            //this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            //this.OrderType = (string)row["OrderType"].GetString();
            //this.PIC = (string)row["EntryPerson"].GetString();
            //this.TotalWeight = (decimal)row["TotalWeight"].GetDecimal();
            //this.TotalAmount = (decimal)row["TotalAmount"].GetDecimal();
        }
    }
}