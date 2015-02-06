using System;
using System.Data;

namespace Epicoil.Library.Models.Purchase
{
    public class POHeaderModel
    {
        #region Properties

        public int PONum { get; set; }

        public int Revision { get; set; }

        public int OpenOrder { get; set; }

        public int VoidOrder { get; set; }

        public string EntryPerson { get; set; }

        public DateTime OrderDate { get; set; }

        public string TermsCode { get; set; }

        public int VendorNum { get; set; }

        public string SupplierName { get; set; }

        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string ApprovedBy { get; set; }

        public int Approve { get; set; }

        public string ApprovalStatus { get; set; }

        public decimal ApprovedAmount { get; set; }

        public string CustId { get; set; }

        public string CustomerName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string SaleContractNum { get; set; }

        public DateTime SaleContractDate { get; set; }

        public string ImportCode { get; set; }

        public string CalculateCode { get; set; }

        public string SectionCode { get; set; }

        #endregion Properties

        #region Methods

        public virtual void DataBind(DataRow row)
        {
            this.PONum = (int)row["PONum"].GetInt();
            this.Revision = (int)row["Revision"].GetInt();
            this.OpenOrder = (int)row["OpenOrder"].GetInt();
            this.VoidOrder = (int)row["VoidOrder"].GetInt();
            this.EntryPerson = (string)row["EntryPerson"].GetString();
            this.OrderDate = (DateTime)row["OrderDate"].GetDate();
            this.TermsCode = (string)row["TermsCode"].GetString();
            this.VendorNum = (int)row["VendorNum"].GetInt();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.CurrencyCode = (string)row["CurrencyCode"].GetString();
            this.ExchangeRate = (int)row["ExchangeRate"].GetInt();
            this.ApprovedDate = (DateTime)row["ApprovedDate"].GetDate();
            this.ApprovedBy = (string)row["ApprovedBy"].GetString();
            this.Approve = (int)row["Approve"].GetInt();
            this.ApprovalStatus = (string)row["ApprovalStatus"].GetString();
            this.ApprovedAmount = (int)row["ApprovedAmount"].GetInt();
            this.CustId = (string)row["CustId"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
            this.SaleContractNum = (string)row["SaleContractNum"].GetString();
            this.SaleContractDate = (DateTime)row["SaleContractDate"].GetDate();
            this.ImportCode = (string)row["ImportCode"].GetString();
            this.CalculateCode = (string)row["CalculateCode"].GetString();
            this.SectionCode = (string)row["SectionCode"].GetString();
        }

        #endregion Methods
    }
}