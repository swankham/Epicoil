using System.Data;

namespace Epicoil.Library.Models
{
    public class CustomerModel
    {
        public string Plant { get; set; }

        public string CustId { get; set; }

        public string CustName { get; set; }

        public string Address { get; set; }

        public virtual void DataBind(DataRow row)
        {
            //this.Plant = (string)row["Plant"];
            this.CustId = (string)row["CustID"];
            this.CustName = (string)row["Name"];
            this.Address = (string)row["Address1"];
        }
    }
}