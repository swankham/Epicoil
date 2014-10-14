using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories
{
    public class CoilBackRuleRepo : ICoilBackRuleRepo
    {
        public IEnumerable<CoilBackRuleModel> GetAll()
        {
            string sql = string.Format(@"SELECT * FROM UD16");
            return Repository.Instance.GetMany<CoilBackRuleModel>(sql);
        }


        public IEnumerable<CoilBackRuleModel> GetByFilter(CoilBackRuleModel filter)
        {
            IEnumerable<CoilBackRuleModel> query = this.GetAll();

            if (!string.IsNullOrEmpty(filter.RuleID)) query = query.Where(p => p.RuleID.ToString().ToUpper().Equals(filter.RuleID.ToString().ToUpper()));

            return query;
        }
    }
}
