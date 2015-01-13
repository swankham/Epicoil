using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class LevellerSimulateModel
    {
        #region Properties

        public string Plant { get; set; }

        public int WorkOrderID { get; set; }

        public int CuttingLineID { get; set; }

        public int MaterialTransLineID { get; set; }

        public int SOQuantity { get; set; }

        public int CalQuantity { get; set; }

        public decimal UsingWeight { get; set; }

        public decimal UsingLength { get; set; }

        public decimal UsingLengthM { get; set; }

        public decimal RemainWeight { get; set; }

        public decimal RemainLength { get; set; }

        public decimal RemainLengthM { get; set; }

        #endregion Properties

        #region Methods

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.MaterialTransLineID = (int)row["MaterialTransLineID"].GetInt();
            this.SOQuantity = (int)row["SOQuantity"].GetInt();
            this.CalQuantity = (int)row["CalQuantity"].GetInt();
            this.UsingWeight = (decimal)row["UsingWeight"].GetDecimal();
            this.UsingLength = (decimal)row["UsingLength"].GetDecimal();
            this.UsingLengthM = (decimal)row["UsingLengthM"].GetDecimal();
            this.RemainWeight = (decimal)row["RemainWeight"].GetDecimal();
            this.RemainLength = (decimal)row["RemainLength"].GetDecimal();
            this.RemainLengthM = (decimal)row["RemainLengthM"].GetDecimal();
        }

        #endregion Methods
    }
}