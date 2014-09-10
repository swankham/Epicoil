using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        public IEnumerable<CustomerModel> GetAllCustomer()
        {
            string sql = string.Format(@"Select * from Customer Order by name asc");

            return Repository.Instance.GetMany<CustomerModel>(sql);
        }

        public IEnumerable<CustomerModel> GetCustomerByFilter(CustomerModel model)
        {
            IEnumerable<CustomerModel> query = GetAllCustomer();

            if (model.CustName != null) { query = query.Where(p => p.CustName.Contains(model.CustName.ToString())); }
            return query;
        }

        public CustomerModel GetCustomerByID(string CustID)
        {
            string sql = string.Format(@"Select * from Customer where CustID = N'{0}'", CustID);
            return Repository.Instance.GetOne<CustomerModel>(sql);
        }
    }
}