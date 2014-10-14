using Epicoil.Library.Models.Billing;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Billing
{
    public interface IBillingRepo
    {
        IEnumerable<InvcHeadModel> GetInvoiceAll(string plant);
    }
}