using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class LineStopModel
    {
        public int ProductionID { get; set; }

        public int WorkOrderID { get; set; }

        public string StopCode { get; set; }

        public string Description { get; set; }

        public string DurationTime { get; set; }

        public decimal CutSeq { get; set; }

        public void DataBind(DataRow row)
        {
            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.StopCode = (string)row["StopCode"].GetString();
            this.Description = (string)row["Description"].GetString();
            this.DurationTime = (string)row["DurationTime"].GetString();
            this.CutSeq = (decimal)row["CutSeq"].GetDecimal();
        }
    }
}