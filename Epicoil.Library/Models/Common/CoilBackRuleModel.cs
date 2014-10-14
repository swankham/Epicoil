using System.Data;

namespace Epicoil.Library.Models
{
    public class CoilBackRuleModel
    {
        public string Plant { get; set; }

        public string RuleID { get; set; }

        public decimal ThickMin { get; set; }

        public decimal ThickMax { get; set; }

        public string Thick
        {
            get
            {
                return ThickMin.ToString("#,##0.00") + " - " + ThickMax.ToString("#,##0.00");
            }
        }

        public decimal WidthMin { get; set; }

        public decimal WidthMax { get; set; }

        public string Width
        {
            get
            {
                return WidthMin.ToString("#,##0.00") + " - " + WidthMax.ToString("#,##0.00");
            }
        }

        public decimal Weight { get; set; }

        public string Description { get; set; }

        public void DataBind(DataRow row)
        {
            Plant = string.IsNullOrEmpty(row["Key5"].GetString()) ? "" : row["Key5"].GetString();
            RuleID = (string)row["Key1"].GetString();
            ThickMin = (decimal)row["Number01"].GetDecimal();
            ThickMax = (decimal)row["Number02"].GetDecimal();
            WidthMin = (decimal)row["Number03"].GetDecimal();
            WidthMax = (decimal)row["Number04"].GetDecimal();
            Description = string.IsNullOrEmpty(row["Character01"].GetString()) ? "" : row["Character01"].GetString();
        }
    }
}