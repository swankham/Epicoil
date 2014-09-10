using System.Data;

namespace Epicoil.Library.Models
{
    public class MakerModel
    {
        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public int MillChild { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.MakerCode = (string)row["Key1"];
            this.MakerName = (string)row["Character01"];
            //this.MillChild = (int)row["MillChild"].GetInt();
        }
    }
}