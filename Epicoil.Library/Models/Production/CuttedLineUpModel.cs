using System;
using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class CuttedLineUpModel
    {
        public int CutLineUpID { get; set; }

        public int ProductionID { get; set; }

        public int WorkOrderID { get; set; }

        public decimal CutSeq { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public int CompleteFlag { get; set; }

        public int MaterialTransLineID { get; set; }

        public void DataBind(DataRow row)
        {
            this.CutLineUpID = (int)row["CutLineUpID"].GetInt();
            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.CutSeq = (decimal)row["CutSeq"].GetDecimal();
            this.StartTime = (DateTime)row["StartTime"].GetDate();
            this.FinishTime = (DateTime)row["FinishTime"].GetDate();
            this.CompleteFlag = (int)row["CompleteFlag"].GetInt();
            this.MaterialTransLineID = (int)row["MaterialTransLineID"].GetInt();
        }
    }
}