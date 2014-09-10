using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ISupplierRepo
    {
        IEnumerable<SupplierModel> GetAll();

        IEnumerable<SupplierModel> Get(SupplierModel model);

        SupplierModel GetByID(string VendorName);
    }
}