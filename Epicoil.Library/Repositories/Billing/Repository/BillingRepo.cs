using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Billing;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Billing
{
    public class BillingRepo : IBillingRepo
    {
        public IEnumerable<InvcHeadModel> GetInvoiceAll(string plant)
        {
            string sql = string.Format(@"SELECT ivh.Company
	                                          , ivh.Plant
                                              , ivh.CreditMemo
                                              , ivh.UnappliedCash
                                              , ivh.InvoiceNum
                                              , ivh.InvoiceType
                                              , ivh.OrderNum
                                              , cust.CustID
	                                          , cust.Name as CustomerName
	                                          , CONCAT(cust.Address1, ' ', cust.Address2, ' ', cust.Address3) as BillToAddress
                                              , ivh.PONum
                                              , ivh.EntryPerson
                                              , ivh.TermsCode
                                              , ivh.InvoiceDate
                                              , ivh.DueDate
                                              , ivh.FiscalYear
                                              , ivh.FiscalPeriod
                                              , ivh.InvoiceComment
                                              , ivh.InvoiceAmt
                                              , ivh.DocInvoiceAmt
	                                          , tax.Percentage
	                                          , tax.TaxAmount
                                              , ivh.InvoiceBal
                                              , ivh.DocInvoiceBal
                                              , ivh.UnpostedBal
                                              , ivh.DocUnpostedBal
                                              , ivh.DepositCredit
                                              , ivh.DocDepositCredit
                                              , ivh.InvoiceRef
                                              , ivh.RefCancelled
                                              , ivh.RefCancelledBy
                                              , ivh.CurrencyCode
                                              , ivh.ExchangeRate
                                              , ivh.LineType
                                              , ivh.DepGainLoss
                                              , ivh.DNComments
                                              , ivh.DNCustNbr
                                              , ivh.DebitNote
                                              , ivh.ApplyDate
                                              , ivh.ShipDate
                                              , ivh.HeadNum
                                              , ivh.DepositAmt
                                          FROM dbo.InvcHead ivh
  	                                        LEFT JOIN Customer cust ON(ivh.CustNum = cust.CustNum)
	                                        LEFT JOIN (SELECT t.Company, t.InvoiceNum, ISNULL(sum(t.TaxAmt), 0) as TaxAmount, ISNULL(t.Percent_, 0) as Percentage
				                                        FROM InvcTax t
				                                        GROUP BY t.InvoiceNum, t.Percent_, t.Company) tax
				                                        ON(ivh.InvoiceNum = tax.InvoiceNum)
                                          WHERE ivh.Posted = 0  -- Not yet post to GL.
		                                        AND ivh.OpenInvoice = 1 --Invoice is not receipt.
		                                        AND ivh.checkbox20 = 0 --Billing was printed.
		                                        AND ivh.Plant = N'{0}'", plant);

            return Repository.Instance.GetMany<InvcHeadModel>(sql);
        }
    }
}