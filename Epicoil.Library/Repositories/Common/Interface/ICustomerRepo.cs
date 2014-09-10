using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICustomerRepo
    {
        IEnumerable<CustomerModel> GetAllCustomer();

        IEnumerable<CustomerModel> GetCustomerByFilter(CustomerModel model);

        CustomerModel GetCustomerByID(string CustID);
    }
}