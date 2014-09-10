using System.Data;

namespace Epicoil.Library.Models
{
    public class POLineModel
    {
        public int PONum { get; set; }

        public int POLine { get; set; }

        public string LineDesc { get; set; }

        public decimal POQuantity { get; set; }

        public decimal POWeight { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public string EndUserID { get; set; }

        public string EndUserName { get; set; }

        public string ActlEndUserID { get; set; }

        public string ActlEndUserName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal BalanceWeight { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.PONum = (int)row["PONum"];
            this.POLine = (int)row["POLine"];
            this.LineDesc = (string)row["LineDesc"];
            this.POWeight = (decimal)row["Number11"].GetDecimal();
            this.CommodityName = string.IsNullOrEmpty((string)row["ShortChar01"].GetString()) ? "" : (string)row["ShortChar01"];
            this.SpecCode = string.IsNullOrEmpty((string)row["ShortChar02"].GetString()) ? "" : (string)row["ShortChar02"];
            this.SpecName = string.IsNullOrEmpty((string)row["SpecName"].GetString()) ? "" : (string)row["SpecName"];
            this.CoatingCode = string.IsNullOrEmpty((string)row["ShortChar03"].GetString()) ? "" : (string)row["ShortChar03"];
            this.CoatingName = string.IsNullOrEmpty((string)row["CoatingName"].GetString()) ? "" : (string)row["CoatingName"];
            this.EndUserID = string.IsNullOrEmpty((string)row["Character02"].GetString()) ? "" : (string)row["Character02"];
            this.EndUserName = string.IsNullOrEmpty((string)row["EndUserName"].GetString()) ? "" : (string)row["EndUserName"];
            this.ActlEndUserID = string.IsNullOrEmpty((string)row["Character03"].GetString()) ? "" : (string)row["Character03"];
            this.ActlEndUserName = string.IsNullOrEmpty((string)row["ActlEndUserName"].GetString()) ? "" : (string)row["ActlEndUserName"];
            //this.POQuantity = (decimal)row[""].GetDecimal();
            this.Thick = (decimal)row["Number01"];
            this.Width = (decimal)row["Number02"];
            this.Length = (decimal)row["Number03"];
        }
    }
}