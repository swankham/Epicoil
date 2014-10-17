using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories
{
    public class SaleSectionRepo : ICustomerZoneRepo
    {

        public IEnumerable<CustomerZoneModel> GetAll(string plant)
        {
            string sql = string.Format(@"select * from ud35 WHERE ShortChar20 = '{0}'", plant);
            return Repository.Instance.GetMany<CustomerZoneModel>(sql);
        }


        public IEnumerable<CustomerZoneModel> GetByFilet(CustomerZoneModel filter)
        {
            IEnumerable<CustomerZoneModel> query = this.GetAll(filter.Plant);

            if (!string.IsNullOrEmpty(filter.SaleSectCode.ToString().ToUpper())) query = query.Where(p => p.SaleSectCode.ToString().ToUpper().Equals(filter.SaleSectCode.ToString().ToUpper()));

            return query;
        }
    }
}
