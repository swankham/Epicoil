using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class MaterialModel
    {
        public string MCSSNo { get; set; }

        public int TransactionLineID { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public DateTime WorkDate { get; set; }

        public int Seq { get; set; }

        public string SerialNo { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public decimal Gravity { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public decimal FrontPlate { get; set; }

        public decimal BackPlate { get; set; }

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

        public decimal LengthM
        {
            get
            {
                return (Length == 0) ? CalculateLengthMeter(Weight, Width, Thick, Gravity, FrontPlate, BackPlate)
                    : Math.Round((Length / 1000), 2);
            }
        }

        public decimal UsingLengthM { get; set; }

        public decimal QuantityPack { get; set; }

        public decimal UsingQuantity { get; set; }

        public decimal RemainQuantity //{ get; set; }
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

        public int Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Possession);
            }
        }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

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

        public virtual void DataBind(DataRow row)
        {
            this.MCSSNo = (string)row["PartNum"].GetString();
            this.TransactionLineID = (int)row["TransactionLineID"].GetInt();
            this.SerialNo = (string)row["LotNum"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.Gravity = (decimal)row["Gravity"].GetDecimal();
            this.CoatingCode = string.IsNullOrEmpty(row["CoatingCode"].GetString()) ? "" : row["CoatingCode"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
            this.FrontPlate = (decimal)row["FrontPlate"].GetDecimal();
            this.BackPlate = (decimal)row["BackPlate"].GetDecimal();
            this.Thick = (decimal)row["Number01"].GetDecimal();
            this.Width = (decimal)row["Number02"].GetDecimal();
            this.Length = (decimal)row["Number03"].GetDecimal();
            this.Weight = (decimal)row["Number04"].GetDecimal();
            this.UsingWeight = (decimal)row["UsingWeight"].GetDecimal();
            //this.RemainWeight = (decimal)row["RemainWeight"].GetDecimal();
            this.UsingLengthM = (decimal)row["UsingLM"].GetDecimal();
            this.UsingQuantity = (decimal)row["Quantity"].GetDecimal();
            //this.RemainQuantity = (decimal)row["RemainQty"].GetDecimal();
            this.QuantityPack = (decimal)row["QuantityPack"].GetDecimal();
            this.CBSelect = Convert.ToBoolean((int)row["CBSelect"].GetInt());
            this.CBalready = Convert.ToBoolean((int)row["CBalready"].GetInt());
            this.Status = (string)row["Status"].GetString();
            this.Note = (string)row["Note"].GetString();
            this.Possession = (int)row["Possession"].GetInt();
            this.BussinessType = string.IsNullOrEmpty(row["BussinessType"].GetString()) ? "" : row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
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
    }
}