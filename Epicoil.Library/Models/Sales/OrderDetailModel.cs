using System;

namespace Epicoil.Library.Models.Sales
{
    public class OrderDetailModel
    {
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

        public int Pack { get; set; }

        public decimal Price { get; set; }

        public decimal SOAmount { get; set; }

        public ClassMasterModel ClassCheck = new ClassMasterModel();

        public ClassMasterModel ClassDetail
        {
            get { return this.ClassCheck; }
            set { this.ClassCheck = value; }
        }
    }
}