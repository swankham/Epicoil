using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class MaterialModel : BaseSerial
    {
        public string MCSSNo { get; set; }

        public int TransactionLineID { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public DateTime WorkDate { get; set; }

        public int Seq { get; set; }

        public string SerialNo { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal Weight { get; set; }

        public decimal UsingWeight { get; set; }

        public decimal RemainWeight//  { get; set; }
        {
            get
            {
                return Weight - UsingWeight;
            }
        }

        public decimal UsingLengthM { get; set; }

        public decimal LengthM
        {
            get
            {
                return (Length == 0) ? CalculateLengthMeter(Weight, Width, Thick, Gravity, FrontPlate, BackPlate)
                    : Math.Round((Length / 1000), 2);
            }
        }

        public decimal RemainLengthM//  { get; set; }
        {
            get
            {
                return LengthM - UsingLengthM;
            }
        }

        public decimal QuantityPack { get; set; }

        public decimal UsingQuantity { get; set; }

        public decimal RemainQuantity 
        {
            get
            {
                return ((Length == 0) ? 1 : QuantityPack) - UsingQuantity;
            }
        }

        public bool CBSelect { get; set; }

        public bool CBalready { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public string ProductStatus { get; set; }

        public string PrdDescriptions { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public bool UsedFlag { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.MCSSNo = (string)row["PartNum"].GetString();
            this.TransactionLineID = (int)row["TransactionLineID"].GetInt();
            this.SerialNo = (string)row["LotNum"].GetString();
            this.Thick = (decimal)row["Number01"].GetDecimal();
            this.Width = (decimal)row["Number02"].GetDecimal();
            this.Length = (decimal)row["Number03"].GetDecimal();
            this.Weight = (decimal)row["Number04"].GetDecimal();
            this.UsingWeight = (decimal)row["UsingWeight"].GetDecimal();
            this.UsingLengthM = (decimal)row["UsingLM"].GetDecimal();
            this.UsingQuantity = (decimal)row["Quantity"].GetDecimal();
            this.QuantityPack = (decimal)row["QuantityPack"].GetDecimal();
            this.CBSelect = Convert.ToBoolean((int)row["CBSelect"].GetInt());
            this.CBalready = Convert.ToBoolean((int)row["CBalready"].GetInt());
            this.Status = (string)row["Status"].GetString();
            this.Note = (string)row["Note"].GetString();
            this.ProductStatus = (string)row["ProductStatus"].GetString();
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
        }

        public decimal CalculateLengthMeter(decimal weight, decimal width, decimal thick, decimal gravity, decimal frontPlate, decimal backPlate)
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

        public void CalculateUsingLength()
        {
            UsingLengthM = (Length == 0) ? CalculateLengthMeter(UsingWeight, Width, Thick, Gravity, FrontPlate, BackPlate)
                    : Math.Round((Length / 1000), 2);
        }

        public void SetUsingWeight()
        {
            UsingWeight = (UsingWeight == 0) ? Weight : UsingWeight;
        }

        public void SetUsingQuantity()
        {
            UsingQuantity = (UsingQuantity == 0) ? ((Length == 0) ? 1 : QuantityPack) : QuantityPack;
        }

        public void SetQuantityPack()
        {
            QuantityPack = (Length == 0) ? 1 : QuantityPack;
        }

        public bool ValidateToCoilBackAuto(IEnumerable<CoilBackRuleModel> coilBackRuleList, out string risk, out string msg)
        {
            bool valid = false;
            risk = string.Empty;
            msg = string.Empty;

            if (RemainWeight <= 0)
            {
                //risk = "WARNNING";
                msg = "Remain weight must greater than 0.";
                return false;
            }

            IEnumerable<CoilBackRuleModel> coilRule = coilBackRuleList;
            coilRule = coilRule.Where(i => i.ThickMin <= Thick && i.ThickMax >= Thick);
            coilRule = coilRule.Where(i => i.WidthMin <= Width && i.WidthMax >= Width);
            coilRule = coilRule.Where(i => i.Weight <= RemainWeight);

            if (coilRule.ToList().Count > 0)
            {
                var result = coilRule.First();
                //The remain weight to matched the coil back rule, and then we will create coil back.
                risk = "WARNNING";
                msg = result.Description;
                CBSelect = true;
                valid = true;
            }

            return valid;
        }
    }
}