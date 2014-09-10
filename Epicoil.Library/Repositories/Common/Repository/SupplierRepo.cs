using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class SupplierRepo : ISupplierRepo
    {
        public IEnumerable<SupplierModel> GetAll()
        {
            string sql = @"select * from Vendor where Inactive = 0 order by VendorID asc";

            return Repository.Instance.GetMany<SupplierModel>(sql);
        }

        public IEnumerable<SupplierModel> Get(SupplierModel model)
        {
            IEnumerable<SupplierModel> query = GetAll();

            if (model.VendorID != null) { query = query.Where(p => p.VendorID.Contains(model.VendorID.ToString())); }
            if (model.VendorName != null) { query = query.Where(p => p.VendorName.Contains(model.VendorName.ToString())); }

            return query.ToList();
        }

        public SupplierModel GetByID(string VendorName)
        {
            string sql = string.Format(@"select * from Vendor where VendorID = N'{0}'", VendorName);
            return Repository.Instance.GetOne<SupplierModel>(sql);
        }
    }
}