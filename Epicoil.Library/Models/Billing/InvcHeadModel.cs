using System;
using System.Data;

namespace Epicoil.Library.Models.Billing
{
    public class InvcHeadModel
    {
        public string Company { get; set; }

        public string Plant { get; set; }

        public int CreditMemo { get; set; }

        public int UnappliedCash { get; set; }

        public int InvoiceNum { get; set; }

        public string InvoiceType { get; set; }

        public int OrderNum { get; set; }

        public string OrderNumber { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string BillToAddress { get; set; }

        public string PONum { get; set; }

        public string EntryPerson { get; set; }

        public string TermsCode { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public int FiscalYear { get; set; }

        public int FiscalPeriod { get; set; }

        public string InvoiceComment { get; set; }

        public decimal InvoiceAmt { get; set; }

        public decimal DocInvoiceAmt { get; set; }

        public decimal Percentage { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal InvoiceBal { get; set; }

        public decimal DocInvoiceBal { get; set; }

        public decimal UnpostedBal { get; set; }

        public decimal DocUnpostedBal { get; set; }

        public decimal DepositCredit { get; set; }

        public decimal DocDepositCredit { get; set; }

        public int InvoiceRef { get; set; }

        public int RefCancelled { get; set; }

        public int RefCancelledBy { get; set; }

        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public string LineType { get; set; }

        public decimal DepGainLoss { get; set; }

        public string DNComments { get; set; }

        public string DNCustNbr { get; set; }

        public int DebitNote { get; set; }

        public DateTime ApplyDate { get; set; }

        public DateTime ShipDate { get; set; }

        public decimal DepositAmt { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.Company = (string)row["Company"].GetString();
            this.Plant = (string)row["Plant"].GetString();
            this.CreditMemo = (int)row["CreditMemo"].GetInt();
            this.UnappliedCash = (int)row["UnappliedCash"].GetInt();
            this.InvoiceNum = (int)row["InvoiceNum"].GetInt();
            this.InvoiceType = (string)row["InvoiceType"].GetString();
            this.OrderNum = (int)row["OrderNum"].GetInt();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.BillToAddress = (string)row["BillToAddress"].GetString();
            this.PONum = (string)row["PONum"].GetString();
            this.EntryPerson = (string)row["EntryPerson"].GetString();
            this.TermsCode = (string)row["TermsCode"].GetString();
            this.InvoiceDate = (DateTime)row["InvoiceDate"].GetDate();
            this.DueDate = (DateTime)row["DueDate"].GetDate();
            this.FiscalYear = (int)row["FiscalYear"].GetInt();
            this.FiscalPeriod = (int)row["FiscalPeriod"].GetInt();
            this.InvoiceComment = (string)row["InvoiceComment"].GetString();
            this.InvoiceAmt = (decimal)row["InvoiceAmt"].GetDecimal();
            this.DocInvoiceAmt = (decimal)row["DocInvoiceAmt"].GetDecimal();
            this.Percentage = (decimal)row["Percentage"].GetDecimal();
            this.TaxAmount = (decimal)row["TaxAmount"].GetDecimal();
            this.InvoiceBal = (decimal)row["InvoiceBal"].GetDecimal();
            this.DocInvoiceBal = (decimal)row["DocInvoiceBal"].GetDecimal();
            this.UnpostedBal = (decimal)row["UnpostedBal"].GetDecimal();
            this.DocUnpostedBal = (decimal)row["DocUnpostedBal"].GetDecimal();
            this.DepositCredit = (decimal)row["DepositCredit"].GetDecimal();
            this.DocDepositCredit = (decimal)row["DocDepositCredit"].GetDecimal();
            this.InvoiceRef = (int)row["InvoiceRef"].GetInt();
            this.RefCancelled = (int)row["RefCancelled"].GetInt();
            this.RefCancelledBy = (int)row["RefCancelledBy"].GetInt();
            this.CurrencyCode = (string)row["CurrencyCode"].GetString();
            this.ExchangeRate = (decimal)row["ExchangeRate"].GetDecimal();
            this.LineType = (string)row["LineType"].GetString();
            this.DepGainLoss = (decimal)row["DepGainLoss"].GetDecimal();
            this.DNComments = (string)row["DNComments"].GetString();
            this.DNCustNbr = (string)row["DNCustNbr"].GetString();
            this.DebitNote = (int)row["DebitNote"].GetInt();
            this.ApplyDate = (DateTime)row["ApplyDate"].GetDate();
            this.ShipDate = (DateTime)row["ShipDate"].GetDate();
            this.DepositAmt = (decimal)row["DepositAmt"].GetDecimal();
        }
    }
}