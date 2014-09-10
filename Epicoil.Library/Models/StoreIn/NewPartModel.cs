using System.Data;

namespace Epicoil.Library.Models.StoreIn
{
    public class NewPartModel : StoreInDetail
    {
        public string SerialNo { get; set; }

        public int iRunning { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string InvoiceNum { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
            //this.InvoiceNum = (string)row["InvoiceNum"].GetString();
        }
    }
}