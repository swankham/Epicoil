using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateReshearModel
    {
        public int WorkOrderID { get; set; }

        public int LineID { get; set; }

        public int MaterialTransLineID { get; set; }

        public int CuttingLineID { get; set; }

        public int OptionNum { get; set; }

        public decimal WidthSuggsQty { get; set; }

        public decimal WidthActualQty { get; set; }

        public decimal WidthSuggsRemain { get; set; }

        public decimal WidthActualRemain { get; set; }

        public decimal LengthSuggsQty { get; set; }

        public decimal LengthActualQty { get; set; }

        public decimal LengthSuggsRemain { get; set; }

        public decimal LengthActualRemain { get; set; }

        public decimal Quantity
        {
            get
            {
                return WidthActualQty * LengthActualQty;
            }
        }

        public int SelectedFlag { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public void DataBind(DataRow row)
        {
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.LineID = (int)row["LineID"].GetInt();
            this.MaterialTransLineID = (int)row["MaterialTransLineID"].GetInt();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.OptionNum = (int)row["OptionNum"].GetInt();
            this.WidthSuggsQty = (decimal)row["WidthSuggsQty"].GetDecimal();
            this.WidthActualQty = (decimal)row["WidthActualQty"].GetDecimal();
            this.WidthSuggsRemain = (decimal)row["WidthSuggsRemain"].GetDecimal();
            this.WidthActualRemain = (decimal)row["WidthActualRemain"].GetDecimal();
            this.LengthSuggsQty = (decimal)row["LengthSuggsQty"].GetDecimal();
            this.LengthActualQty = (decimal)row["LengthActualQty"].GetDecimal();
            this.LengthSuggsRemain = (decimal)row["LengthSuggsRemain"].GetDecimal();
            this.LengthActualRemain = (decimal)row["LengthActualRemain"].GetDecimal();
            this.SelectedFlag = (int)row["SelectedFlag"].GetInt();
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
        }
    }
}