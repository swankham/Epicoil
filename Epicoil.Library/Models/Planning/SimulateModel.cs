using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateModel : BaseSerial
    {
        public string Plant { get; set; }

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

        public decimal LengthM
        {
            get
            {
                return (Length == 0) ? CalLengthMeter(UnitWeight, Width, Thick, Gravity, FrontPlate, BackPlate)
                    : Math.Round((Length / 1000), 2);
            }
        }

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

            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.TransactionLineID = (int)row["MaterialTransLineID"].GetInt();
            this.MCSSNum = (string)row["MCSSNum"].GetString();
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
    }
}