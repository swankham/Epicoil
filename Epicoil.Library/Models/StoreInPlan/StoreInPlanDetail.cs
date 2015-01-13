using System;
using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class StoreInPlanDetailModel
    {
        public string PlantID { get; set; }

        public int StoreInPlanId { get; set; }

        public int LineID { get; set; }

        public int SeqId { get; set; }

        public string PONumber { get; set; }

        public string PartNum { get; set; }

        public int PONum { get; set; }

        public int POLine { get; set; }

        public string SaleContract { get; set; }

        public decimal OpenBalance { get; set; }

        public decimal WeightBalnce { get; set; }

        public decimal RemainingWeight { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public string ArticleNo { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Weight { get; set; }

        public string Place { get; set; }

        public string Note { get; set; }

        public string EndUserID { get; set; }

        public string EndUserName { get; set; }

        public string ActlEndUserID { get; set; }

        public string ActlEndUserName { get; set; }

        public DateTime ReceiptDate { get; set; }

        public bool TaxPaid { get; set; }

        public decimal DutyRate { get; set; }

        public string PackingNumber { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public string Category { get; set; }

        //public string SContract { get; set; }
        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public decimal POAllowance { get; set; }

        public int StoreInFlag { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.LineID = (int)row["LineId"];
            this.SeqId = (int)row["SeqId"];
            this.StoreInPlanId = (int)row["StoreInPlanId"];
            this.PONum = (int)row["PONum"].GetInt();
            this.PONumber = (string)row["PONumber"].GetString();
            this.POLine = (int)row["POLine"].GetInt();
            this.PartNum = (string)row["PartNum"].GetString();
            this.DutyRate = (decimal)row["DutyRate"].GetDecimal();
            this.SaleContract = (string)row["SaleContract"].GetString();
            this.WeightBalnce = (decimal)row["WeightBalnce"].GetDecimal();
            this.RemainingWeight = (decimal)row["RemainingWeight"].GetDecimal();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.ArticleNo = (string)row["ArticleNo"].GetString();
            this.Quantity = (decimal)row["Quantity"].GetDecimal();
            this.Weight = (decimal)row["Weight"].GetDecimal();
            this.UnitPrice = (decimal)row["UnitPrice"].GetDecimal();
            this.Amount = (decimal)row["Amount"].GetDecimal();
            this.Place = (string)row["Place"].GetString();
            this.Note = (string)row["Note"].GetString();
            this.EndUserID = string.IsNullOrEmpty((string)row["Character02"].GetString()) ? "" : (string)row["Character02"];
            this.EndUserName = string.IsNullOrEmpty((string)row["EndUserName"].GetString()) ? "" : (string)row["EndUserName"];
            this.ActlEndUserID = string.IsNullOrEmpty((string)row["Character03"].GetString()) ? "" : (string)row["Character03"];
            this.ActlEndUserName = string.IsNullOrEmpty((string)row["ActlEndUserName"].GetString()) ? "" : (string)row["ActlEndUserName"];
            this.ReceiptDate = (DateTime)row["ReceiptDate"].GetDate();
            this.TaxPaid = Convert.ToBoolean(row["TaxPaid"].GetInt());
            this.PackingNumber = (string)row["PackingNumber"].GetString();
            this.OpenBalance = (decimal)row["OpenBalance"].GetDecimal();
            this.BussinessType = string.IsNullOrEmpty((string)row["BussinessType"].GetString()) ? "" : (string)row["BussinessType"];
            this.BussinessTypeName = string.IsNullOrEmpty((string)row["BussinessTypeName"].GetString()) ? "" : (string)row["BussinessTypeName"];
            this.Category = (string)row["Category"].GetString();
            this.CoatingCode = (string)row["CoatingCode"].GetString();
            this.StoreInFlag = (int)row["StoreInFlag"].GetInt();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
        }
    }
}