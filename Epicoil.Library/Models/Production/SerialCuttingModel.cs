using Epicoil.Library.Models.Planning;
using System;
using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class SerialCuttingModel : GeneratedSerialModel
    {
        public int ProductionID { get; set; }

        public decimal LengthActual { get; set; }

        public decimal WeightActual { get; set; }

        public bool NGFlag { get; set; }

        public int CutSeq { get; set; }

        public string MaterialSerialNo { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.LengthActual = (decimal)row["LengthActual"].GetDecimal();
            this.WeightActual = (decimal)row["WeightActual"].GetDecimal();
            this.NGFlag = Convert.ToBoolean((int)row["NGFlag"].GetInt());
            this.CutSeq = (int)row["CutSeq"].GetInt();
            this.MaterialSerialNo = (string)row["MaterialSerialNo"].GetString();
        }
    }
}