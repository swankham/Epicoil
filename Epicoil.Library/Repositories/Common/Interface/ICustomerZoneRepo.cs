using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{

        public interface ICustomerZoneRepo
        {
            IEnumerable<CustomerZoneModel> GetAll(string plant);
            IEnumerable<CustomerZoneModel> GetByFilet(CustomerZoneModel filter);

        }
}
