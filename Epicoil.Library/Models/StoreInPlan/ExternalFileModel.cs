using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class ExternalFileModel
    {
        public int LineID { get; set; }

        public int SeqId { get; set; }

        public string ArticleNo { get; set; }

        public string Commodity { get; set; }

        public string Specification { get; set; }

        public string Coating { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal Quantity { get; set; }

        public decimal Weight { get; set; }

        public string Place { get; set; }

        public string PackingNo { get; set; }

        public string Category { get; set; }

        public string MakerSaleContract { get; set; }

        public string Note { get; set; }

        public string MakerCode { get; set; }

        public string CustID { get; set; }

        public string Vessel { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.LineID = (int)row["LineId"];

            this.SeqId = (int)row["SeqId"];

            this.ArticleNo = (string)row["MakerNo"].GetString();

            this.Commodity = (string)row["Commodity"].GetString();

            this.Specification = (string)row["Spec"].GetString();

            //this.Coating = (string)row["Coating"].GetString();

            this.Thick = (decimal)row["Thick"].GetDecimal();

            this.Width = (decimal)row["Width"].GetDecimal();

            this.Length = (decimal)row["Length"].GetDecimal();

            this.Quantity = (decimal)row["Qty"].GetDecimal();

            this.Weight = (decimal)row["Weight"].GetDecimal();

            this.Place = (string)row["Location"].GetString();

            //this.PackingNo = (string)row["PackingNo"].GetString();

            //this.Category = (string)row["Category"].GetString();

            this.MakerSaleContract = (string)row["CC"].GetString();

            this.Note = (string)row["Remark"].GetString();

            this.MakerCode = (string)row["Maker"].GetString();
            this.CustID = (string)row["Cust"].GetString();
            this.Vessel = (string)row["Vessel"].GetString();
        }
    }
}