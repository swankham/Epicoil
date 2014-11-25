using Epicoil.Library.Models.Planning;
using System;
using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class SerialCuttingModel : GeneratedSerialModel
    {
        public int SerialLineID { get; set; }

        public int ProductionID { get; set; }

        public decimal LengthActual { get; set; }

        public decimal WeightActual { get; set; }

        public bool NGFlag { get; set; }

        public decimal CutSeq { get; set; }

        public string CutSeqStr
        {
            get
            {
                return CutSeq.ToString("#,###.#");
            }
        }

        public string MaterialSerialNo { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.SerialLineID = (int)row["LineID"].GetInt();
            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.LengthActual = (decimal)row["LengthActual"].GetDecimal();
            this.WeightActual = (decimal)row["WeightActual"].GetDecimal();
            this.NGFlag = Convert.ToBoolean((int)row["NGFlag"].GetInt());
            this.CutSeq = (decimal)row["CutSeq"].GetDecimal();
            this.MaterialSerialNo = (string)row["MaterialSerialNo"].GetString();
        }

        public void SetLengthActualM()
        {
            decimal d1 = WeightActual * 1000;
            decimal d2 = Thick * Gravity;
            decimal d3 = (FrontPlate + BackPlate) / 1000;
            decimal d4 = Width / 1000;

            d2 = d2 + d3;
            d2 = d2 * d4;

            //Fix bug Infinity.
            if (d2 == 0) d2 = 1;
            decimal result = d1 / d2;

            //Convert mm to M.
            LengthActual = Math.Round(result / 1000, 2);
        }

        public void SetWeightActualKg()
        {
            WeightActual = Math.Round((UnitWeight * LengthActual) / LengthM, 2);
        }
    }
}