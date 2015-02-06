using Epicoil.Library.Enums;
using Epicoil.Library.Repositories.StoreInPlan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Resources;

using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class StoreInPlanHeadModel : ICloneable
    {
        #region Fields

        private IStoreInPlanRepo _repoMaster;

        #endregion Fields

        #region Constructors

        public StoreInPlanHeadModel()
        {
            _repoMaster = new StoreInPlanRepo();

            Currencies = new List<CurrencyModel>();
            ImportPorts = new List<PortModel>();
            ArivePorts = new List<PortModel>();
            StoreInPlanDetails = new List<StoreInPlanDetailModel>();
            StoreInPlanFileDetails = new List<ExternalFileModel>();
        }

        #endregion Constructors

        public int ImportFlag { get; set; }  //0=Import, 1=Domestic, 2=Itaku

        public string OrderType
        {
            get
            {
                return Enum.GetName(typeof(StoreInPlanType), ImportFlag);
            }
        }

        public bool InsertState { get; set; }
        
        public string PlantID { get; set; }

        [Display(Name = "Plant", ResourceType = typeof(ResourceLang))]
        public string PlantName { get; set; }

        public int StoreInPlanId { get; set; }

        [Display(Name = "StoreInPlanNum", ResourceType = typeof(ResourceLang))]
        public string StoreInPlanNum { get; set; }

        public string TransactionType { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please fill the Suplier.")]
        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please fill the Customer.")]
        public string CustID { get; set; }

        public string CustomerName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string MakerCode { get; set; }

        public string MakerName { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        //[RequiredIf("MyProperty2 == null && MyProperty3 == false")]
        public string MillCode { get; set; }

        public string MillName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CurrencyCode { get; set; }

        public string IMexItemNo { get; set; }

        public string InvoiceNum { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal ExchangeRate { get; set; }

        public string TisiFlag { get; set; }

        public string LoadPort { get; set; }

        public string ArivePort { get; set; }

        public DateTime ETDDate { get; set; }

        public DateTime ETADate { get; set; }

        [StringLength(5)]
        public string Vessel { get; set; }

        public string ImexConfirm { get; set; }

        public string IMexConfirmText
        {
            get
            {
                if (ImexConfirm == "0")
                {
                    return "Pending";
                }
                else if (ImexConfirm == "1")
                {
                    return "Confirmed";
                }
                else if (ImexConfirm == "2")
                {
                    return "Reject";
                }
                else
                {
                    return "Sale reply";
                }
            }
        }

        public string ImexRemark { get; set; }

        public string StoreInFlag { get; set; }

        public string UserGroup { get; set; }

        public int POLineRcv { get; set; }

        public int NumberOfRcv { get; set; }

        public decimal WeightRcv { get; set; }

        public int OpenOrder { get; set; }

        public IList<CurrencyModel> Currencies { get; set; }

        public IList<PortModel> ImportPorts { get; set; }

        public IList<PortModel> ArivePorts { get; set; }

        public IList<StoreInPlanDetailModel> StoreInPlanDetails { get; set; }

        public IList<ExternalFileModel> StoreInPlanFileDetails { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.ImportFlag = (int)row["TransactionType"].GetInt();
            this.InsertState = false;
            this.StoreInPlanId = (int)row["StoreInPlanId"].GetInt();
            this.StoreInPlanNum = (string)row["StoreInPlanNum"].GetString();
            this.TransactionType = (string)row["TransactionType"].GetString();
            this.BussinessType = (string)row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
            this.CurrencyCode = (string)row["CurrencyCode"].GetString().Trim();
            this.IMexItemNo = (string)row["IMexItemNo"].GetString();
            this.InvoiceNum = (string)row["InvoiceNum"].GetString();
            this.InvoiceDate = (DateTime)row["InvoiceDate"].GetDate();
            this.ExchangeRate = (decimal)row["PORate"].GetDecimal();
            this.TisiFlag = (string)row["TisiFlag"].GetString();
            this.LoadPort = (string)row["LoadPort"].GetString();
            this.ArivePort = (string)row["ArivePort"].GetString();
            this.ETDDate = (DateTime)row["ETDDate"].GetDate();
            this.ETADate = (DateTime)row["ETADate"].GetDate();
            this.Vessel = (string)row["Vessel"].GetString();
            this.ImexConfirm = (string)row["ImexConfirm"].GetString();
            this.ImexRemark = (string)row["ImexRemark"].GetString();
            this.StoreInFlag = (string)row["StoreInFlag"].GetString();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.OpenOrder = (int)row["OpenOrder"].GetInt();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool ValidHeader(SessionInfo _session, out string attribute, out string message)
        {
            bool result = true;
            attribute = string.Empty;
            message = string.Empty;
            _repoMaster = new StoreInPlanRepo();

            if (string.IsNullOrEmpty(InvoiceNum))
            {
                message = "Please fill the required field.";
                result = false;
            }
            else if (_repoMaster.CheckInvoiceExisting(InvoiceNum) && InsertState == true)
            {
                message = "This invoice number is duplicate.";
                result = false;
            }

            if (string.IsNullOrEmpty(SupplierCode))
            {
                message = "Please fill the required field.";
                result = false;
            }

            if (string.IsNullOrEmpty(MakerCode))
            {
                message = "Please fill the required field.";
                result = false;
            }

            if (string.IsNullOrEmpty(MillCode))
            {
                message = "Please fill the required field.";
                result = false;
            }

            if (ImportFlag == 0)
            {
                if (string.IsNullOrEmpty(Vessel))
                {
                    message = "Please fill the required field.";
                    result = false;
                }
                if (string.IsNullOrEmpty(ArivePort))
                {
                    message = "Please fill the required field.";
                    result = false;
                }
            }

            return result;
        }
    }

    public class POHeaderModel
    {
        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.CurrencyCode = (string)row["CurrencyCode"].GetString();
            this.ExchangeRate = (decimal)row["ExchangeRate"].GetDecimal();
            this.OrderDate = (DateTime)row["OrderDate"].GetDate();
        }
    }
}