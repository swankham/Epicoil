using System.Data;

namespace Epicoil.Library.Models
{
    public class MillModel
    {
        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string MakerCode { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.MillCode = (string)row["Key2"];
            this.MillName = (string)row["Character01"];
            this.MakerCode = (string)row["Key1"];
        }
    }
}