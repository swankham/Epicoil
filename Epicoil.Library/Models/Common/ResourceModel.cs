using System.Data;

namespace Epicoil.Library.Models
{
    public class ResourceModel
    {
        public string Plant { get; set; }

        public string ResourceGrpID { get; set; }

        public string GropDescription { get; set; }

        public string ResourceID { get; set; }

        public string ResourceDescription { get; set; }

        public decimal MaxKnife { get; set; }

        public decimal ThickMin { get; set; }

        public decimal ThickMax { get; set; }

        public decimal WidthMin { get; set; }

        public decimal WidthMax { get; set; }

        public decimal LengthMin { get; set; }

        public decimal LengthMax { get; set; }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.ResourceGrpID = (string)row["ResourceGrpID"].GetString();
            this.GropDescription = (string)row["GropDescription"].GetString();
            this.ResourceID = (string)row["ResourceID"].GetString();
            this.ResourceDescription = (string)row["ResourceDescription"].GetString();
            this.MaxKnife = (decimal)row["Number07"].GetDecimal();
            this.ThickMin = (decimal)row["Number01"].GetDecimal();
            this.ThickMax = (decimal)row["Number02"].GetDecimal();
            this.WidthMin = (decimal)row["Number03"].GetDecimal();
            this.WidthMax = (decimal)row["Number04"].GetDecimal();
            this.LengthMin = (decimal)row["Number05"].GetDecimal();
            this.LengthMax = (decimal)row["Number06"].GetDecimal();
        }
    }
}