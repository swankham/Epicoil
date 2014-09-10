using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Epicoil.Library.Models;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class StoreInPlanHead : ICloneable
    {
        public int ImportFlag { get; set; }  //0=Import, 1=Domestic, 2=Itaku

        public bool InsertState { get; set; }

        public string PlantID { get; set; }

        public string PlantName { get; set; }

        public int StoreInPlanId { get; set; }

        public string StoreInPlanNum { get; set; }

        public string TransactionType { get; set; }

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

        public IEnumerable<CurrenciesModel> CurrenciesList = new List<CurrenciesModel>();
        public IEnumerable<PortModel> PortList = new List<PortModel>();

        public List<CurrenciesModel> Currencies
        {
            get { return this.CurrenciesList.ToList(); }
            set { this.CurrenciesList = value; }
        }

        public List<PortModel> ImportPorts
        {
            get { return this.PortList.ToList(); }
            set { this.PortList = value; }
        }

        public IEnumerable<StoreInPlanDetail> StoreInPlanDetails { get; set; }

        public IEnumerable<ExternalFileModel> StoreInPlanFileDetails { get; set; }

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
            this.InvoiceDate = (DateTime)row["InvoiceDate"].GetDate(); ;
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
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class GetHeader
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
        }
    }
}