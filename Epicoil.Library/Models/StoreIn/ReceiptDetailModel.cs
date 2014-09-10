using System.Data;

namespace Epicoil.Library.Models.StoreIn
{
    public class ReceiptDetailModel
    {
        public decimal TransactionID { get; set; }

        //public decimal TransactionLineID { get; set; }

        public int PONum { get; set; }

        public int POLine { get; set; }

        public decimal Quantity { get; set; }

        public decimal Weight { get; set; }

        public void DataBind(DataRow row)
        {
            this.TransactionID = (decimal)row["TransactionID"].GetDecimal();
            //this.TransactionLineID = (decimal)row["TransactionLineID"].GetDecimal();
            this.PONum = (int)row["PONum"].GetInt();
            this.POLine = (int)row["POLine"].GetInt();
            this.Quantity = (decimal)row["Quantity"].GetDecimal();
            this.Weight = (decimal)row["Weight"].GetDecimal();
        }
    }
}