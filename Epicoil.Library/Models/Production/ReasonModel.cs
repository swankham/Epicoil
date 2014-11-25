using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class ReasonModel
    {
        public int ID { get; set; }

        public string ReasonCode { get; set; }

        public string ReasonDescription { get; set; }

        public void DataBind(DataRow row)
        {
            this.ID = (int)row["ID"].GetInt();
            this.ReasonCode = (string)row["StopCode"].GetString();
            this.ReasonDescription = (string)row["ReasonDescription"].GetString();
        }
    }
}