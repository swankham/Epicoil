using System;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateModel : BaseSerial
    {
        public string Plant { get; set; }

        public int SimLineID { get; set; }
        public int WorkOrderID { get; set; }

        public int CuttingLineID { get; set; }

        public int TransactionLineID { get; set; }

        public string MCSSNum { get; set; }

        public string MaterialSerialNo { get; set; }

        public int SimSeq { get; set; }

        public string SONo { get; set; }

        public int SOLine { get; set; }

        public string NORNum { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal UsingLengthM { get; set; }

        public decimal LengthM { get; set; }

        //{
        //    get
        //    {
        //        return (Length == 0) ? CalLengthMeter(UnitWeight, Width, Thick, Gravity, FrontPlate, BackPlate) : Math.Round((Length / 1000), 2);
        //    }
        //}

        public string Status { get; set; }

        public int Stand { get; set; }

        public int CutDiv { get; set; }

        public decimal UnitWeight { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal Quantity { get; set; }

        public bool CalculatedFlag { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.SimLineID = (int)row["SimLineID"].GetInt();
            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.TransactionLineID = (int)row["MaterialTransLineID"].GetInt();
            this.MCSSNum = (string)row["MCSSNo"].GetString();
            this.MaterialSerialNo = (string)row["MaterialSerialNo"].GetString();
            this.SimSeq = (int)row["SimSeq"].GetInt();
            this.SONo = (string)row["SONo"].GetString();
            this.SOLine = (int)row["SOLine"].GetInt();
            this.NORNum = (string)row["NORNum"].GetString();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.Status = (string)row["Status"].GetString();
            this.Stand = (int)row["Stand"].GetInt();
            this.CutDiv = (int)row["CutDiv"].GetInt();
            this.UnitWeight = (decimal)row["UnitWeight"].GetDecimal();
            this.TotalWeight = (decimal)row["TotalWeight"].GetDecimal();
            this.Quantity = (decimal)row["Quantity"].GetDecimal();
            this.CalculatedFlag = Convert.ToBoolean((int)row["CalculatedFlag"].GetInt());
            this.LengthM = (decimal)row["LengthM"].GetDecimal();
        }

        public decimal CalLengthMeter(decimal weight, decimal width, decimal thick, decimal gravity, decimal frontPlate, decimal backPlate)
        {
            decimal d1 = weight * 1000;
            decimal d2 = thick * gravity;
            decimal d3 = (frontPlate + backPlate) / 1000;
            decimal d4 = width / 1000;

            d2 = d2 + d3;
            d2 = d2 * d4;

            //Fix bug Infinity.
            if (d2 == 0) d2 = 1;
            decimal result = d1 / d2;

            //Convert mm to M.
            return Math.Round(result / 1000, 2);
        }

        public void CalculateRow(SimulateActionModel head, MaterialModel mat)
        {
            //Fix bug in case Materials is null.
            //if (head.SimulateOption == 0)
            //{
            decimal widthMat = 0;
            decimal cutMax = 0;
            widthMat = mat.Width;
            cutMax = head.Cuttings.Max(i => i.CutDiv);
            UnitWeight = Math.Round(CalUnitWgtByUsingWgt(head.Expected, widthMat, Width), 2);
            TotalWeight = UnitWeight * Stand;
            LengthM = Math.Round((Length == 0) ? CalLengthMeter(UnitWeight, Width, Thick, Gravity, FrontPlate, BackPlate) : Math.Round((Length / 1000), 1), 0);
            //}
            //else if (head.SimulateOption == 1)
            //{
            //    LengthM = mat.UsingLengthM;
            //    UnitWeight = (mat.Weight * LengthM) / mat.LengthM;
            //}

            CalculatedFlag = true;
            MCSSNum = mat.MCSSNo;
            TransactionLineID = mat.TransactionLineID;
            MaterialSerialNo = mat.SerialNo;
        }

        public decimal CalculateUnitWeightOptionLM(MaterialModel mat)
        {
            return (mat.Weight * LengthM) / mat.LengthM;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="UsingWgt">Unit is Kg.</param>
        /// <param name="WidthMaterial">Unit is MM.</param>
        /// <param name="WidthFG">Unit is MM.</param>
        /// <returns></returns>
        private decimal CalUnitWgtByUsingWgt(decimal UsingWgt, decimal WidthMaterial, decimal WidthFG)
        {
            decimal CalWeightFG = 0.0M;
            if (UsingWgt > 0 && WidthMaterial > 0 && WidthFG > 0)
            {
                CalWeightFG = Math.Round((UsingWgt / WidthMaterial * WidthFG), 2);
            }
            return CalWeightFG;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="MaterialLengthM">Unit is Meter</param>
        /// <param name="MaterialWeight">Unit is Kg.</param>
        /// <param name="MaterialUsingWeight">Unit is Kg.</param>
        /// <param name="CutDiv">Value of cut</param>
        /// <returns></returns>
        private static decimal CalUsingLength(decimal MaterialLengthM, decimal MaterialWeight, decimal MaterialUsingWeight, decimal CutDiv)
        {
            decimal ActualLength = 0.0M;
            if (MaterialWeight > 0 && MaterialUsingWeight > 0 && MaterialLengthM > 0 && CutDiv > 0)
            {
                ActualLength = MaterialUsingWeight * MaterialLengthM / MaterialWeight;
            }
            return ActualLength;
        }
    }
}