using System.Data;

namespace Epicoil.Library.Models
{
    public class BaseEntity
    {
        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string PlantID { get; set; }

        public string PlantName { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CompanyID = (string)row["Company"];
            this.CompanyName = (string)row["CompanyName"];
            this.PlantID = (string)row["Plant"];
            this.PlantName = (string)row["PlantName"];
        }
    }
}