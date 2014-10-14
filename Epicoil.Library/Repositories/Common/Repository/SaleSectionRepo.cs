using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories
{
    public class SaleSectionRepo : ISaleSectionRepo
    {

        public IEnumerable<SaleSectionModel> GetAll(string plant)
        {
            string sql = string.Format(@"select * from ud35 WHERE ShortChar20 = '{0}'", plant);
            return Repository.Instance.GetMany<SaleSectionModel>(sql);
        }


        public IEnumerable<SaleSectionModel> GetByFilet(SaleSectionModel filter)
        {
            IEnumerable<SaleSectionModel> query = this.GetAll(filter.Plant);

            if (!string.IsNullOrEmpty(filter.SaleSectCode.ToString().ToUpper())) query = query.Where(p => p.SaleSectCode.ToString().ToUpper().Equals(filter.SaleSectCode.ToString().ToUpper()));

            return query;
        }
    }
}
