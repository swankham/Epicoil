using System.Data;

namespace Epicoil.Library.Models
{
    public class BussinessTypeModel
    {
        public string BussinessCode { get; set; }

        public string BussinessName { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.BussinessCode = (string)row["Key1"];
            this.BussinessName = (string)row["Character01"];
        }
    }
}