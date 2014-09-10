using System;
using System.Data;

namespace Epicoil.Library.Models.StoreIn
{
    public class RecieptHeadModel
    {
        public decimal TransactionID { get; set; }

        public int StoreInID { get; set; }

        public string StoreInNum { get; set; }

        public DateTime StoreInDate { get; set; }

        public int PONum { get; set; }

        public string PONumber { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public int VendorNum { get; set; }

        public void DataBind(DataRow row)
        {
            this.TransactionID = (decimal)row["TransactionID"].GetDecimal();
            this.StoreInID = (int)row["StoreInID"].GetInt();
            this.StoreInNum = (string)row["StoreInNum"].GetString();
            this.StoreInDate = (DateTime)row["StoreInDate"].GetDate();
            this.PONum = (int)row["PONum"].GetInt();
            this.PONumber = (string)row["PONumber"].GetString();
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.VendorNum = (int)row["VendorNum"].GetInt();
        }
    }
}