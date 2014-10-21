using System.Data;
using Epicoil.Library.Models.StoreInPlan;

namespace Epicoil.Library.Models.StoreIn
{
    public class StoreInDetail : StoreInPlanDetail
    {
        public int StoreInID { get; set; }

        public string StoreInNum { get; set; }

        public decimal TransactionID { get; set; }

        public decimal TransactionLineID { get; set; }

        public int VendorNum { get; set; }

        public string ShipVia { get; set; }

        public string StockNo { get; set; }

        public string Location { get; set; }

        public int NGStatus { get; set; }

        public string NGRemark { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);
            this.TransactionID = (decimal)row["TransactionID"].GetDecimal();
            this.TransactionLineID = (decimal)row["TransactionLineID"].GetDecimal();
            this.StoreInID = (int)row["StoreInId"].GetInt();
            this.StockNo = row["StockNo"].GetString();
            this.Location = row["Location"].GetString();
            this.PONum = (int)row["PONum"].GetInt();
            this.VendorNum = (int)row["VendorNum"].GetInt();
            this.StoreInNum = (string)row["StoreInNum"].GetString();
            this.NGStatus = (int)row["NGFlag"].GetInt();
            this.NGRemark = (string)row["NGRemark"].GetString();
        }
    }
}