using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class MaterialModel
    {
        public string MCSSNo { get; set; }

        public int Seq { get; set; }

        public string SerialNo { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal Weight { get; set; }

        public decimal UsingWeight { get; set; }

        public decimal RemainWeight { get; set; }

        public decimal LengthM { get; set; }

        public decimal Quantity { get; set; }

        public decimal RemainQty { get; set; }

        public decimal QuantityPack { get; set; }

        public bool CBSelect { get; set; }

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

        public void DataBind(DataRow row)
        {
            this.MCSSNo = (string)row["PartNum"].GetString();
            this.SerialNo = (string)row["LotNum"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.CoatingCode = string.IsNullOrEmpty(row["CoatingCode"].GetString()) ? "" : row["CoatingCode"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
            this.Thick = (decimal)row["Number01"].GetDecimal();
            this.Width = (decimal)row["Number02"].GetDecimal();
            this.Length = (decimal)row["Number03"].GetDecimal();
            this.Weight = (decimal)row["Number04"].GetDecimal();
            this.UsingWeight = (decimal)row["UsingWeight"].GetDecimal();
            this.RemainWeight = (decimal)row["RemainWeight"].GetDecimal();
            this.LengthM = (decimal)row["LengthM"].GetDecimal();
            this.Quantity = (decimal)row["Quantity"].GetDecimal();
            this.RemainQty = (decimal)row["RemainQty"].GetDecimal();
            this.QuantityPack = (decimal)row["QuantityPack"].GetDecimal();
            this.CBSelect = Convert.ToBoolean((int)row["CBSelect"].GetInt());
            this.Status = (string)row["Status"].GetString();
            this.Note = (string)row["Note"].GetString();
            this.Possession = (int)row["Possession"].GetInt();
            this.BussinessType = string.IsNullOrEmpty(row["BussinessType"].GetString()) ? "" : row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.ProductStatus = (string)row["ProductStatus"].GetString();
        }
    }
}