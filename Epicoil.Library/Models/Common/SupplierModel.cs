using System.Data;

namespace Epicoil.Library.Models
{
    public class SupplierModel
    {
        public string Plant { get; set; }

        public string VendorID { get; set; }

        public string VendorName { get; set; }

        public string Address { get; set; }

        public virtual void DataBind(DataRow row)
        {
            //this.Plant = (string)row["Plant"];
            this.VendorID = (string)row["VendorID"];
            this.VendorName = (string)row["Name"];
            this.Address = (string)row["Address1"];
        }
    }
}
