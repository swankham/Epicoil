using System.Data;
namespace Epicoil.Library.Models.Planning
{
    public class LevellerSimulateModel
    {
        public string Plant { get; set; }

        public int WorkOrderID { get; set; }

        public int CuttingLineID { get; set; }

        public int MaterialTransLineID { get; set; }

        public int SOQuantity { get; set; }

        public int Quantity { get; set; }

        public decimal Weight { get; set; }

        public decimal LengthM { get; set; }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.MaterialTransLineID = (int)row["MaterialTransLineID"].GetInt();
            this.SOQuantity = (int)row["SOQuantity"].GetInt();
            this.Quantity = (int)row["Quantity"].GetInt();
            this.Weight = (decimal)row["Weight"].GetDecimal();
            this.LengthM = (decimal)row["LengthM"].GetDecimal();
        }
    }
}