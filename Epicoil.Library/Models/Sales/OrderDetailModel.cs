using System;
using System.Data;

namespace Epicoil.Library.Models.Sales
{
    public class OrderDetailModel
    {
        public string OrderNum { get; set; }

        public int OrderLine { get; set; }

        public string NORNo { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

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

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal SOWeight { get; set; }

        public decimal SOQuantity { get; set; }

        public int QtyPack { get; set; }

        public decimal Pack { get; set; }

        public decimal Price { get; set; }

        public int ClassID { get; set; }

        public decimal SOAmount { get; set; }

        public ClassMasterModel ClassCheck = new ClassMasterModel();

        public ClassMasterModel ClassDetail
        {
            get { return this.ClassCheck; }
            set { this.ClassCheck = value; }
        }

        public void DataBind(DataRow row)
        {
            this.OrderNum = (string)row["OrderNum"].GetString();
            this.OrderLine = (int)row["OrderLine"].GetInt();
            this.NORNo = (string)row["PartNum"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["PartNum"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.CoatingCode = (string)row["CoatingCode"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
            this.Possession = (int)row["Possession"].GetInt();
            this.BussinessType = string.IsNullOrEmpty(row["BussinessType"].GetString()) ? "" : row["BussinessType"].GetString();
            this.BussinessTypeName = string.IsNullOrEmpty(row["BussinessTypeName"].GetString()) ? "" : row["BussinessTypeName"].GetString();
            //this.SupplierCode = (string)row[""].GetString();
            //this.SupplierName = (string)row[""].GetString();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            //this.MakerCode = (string)row[""].GetString();
            //this.MakerName = (string)row[""].GetString();
            //this.MillCode = (string)row[""].GetString();
            //this.MillName = (string)row[""].GetString();
            this.Thick = (decimal)row["Number01"].GetDecimal();
            this.Width = (decimal)row["Number02"].GetDecimal();
            this.Length = (decimal)row["Number03"].GetDecimal();
            this.SOWeight = (decimal)row["Number07"].GetDecimal();
            this.SOQuantity = (decimal)row["SellingQuantity"].GetDecimal();
            this.Pack = (decimal)row["Number04"].GetDecimal();
            this.Price = (decimal)row["DocUnitPrice"].GetDecimal();
            this.SOAmount = (decimal)row["Number06"].GetDecimal();
            this.ClassID = Convert.ToInt32(row["Number17"].GetDecimal());
            this.QtyPack = Convert.ToInt32(row["Number10"].GetDecimal());
        }
    }
}