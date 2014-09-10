using System.Collections.Generic;
using System.Linq;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public class PackingStyle : IPackingStyle
    {
        public IEnumerable<PackingStyleModel> GetAll()
        {
            string sql;
            sql = "SELECT * FROM ucc_tqa_PackingStyle (NOLOCK)";

            return Repository.Instance.GetMany<PackingStyleModel>(sql);
        }

        public PackingStyleModel Get(string codeNum)
        {
            string sql;
            sql = "SELECT * FROM ucc_tqa_PackingStyle (NOLOCK)";
            sql += string.Format(" WHERE CodeNum = N'{0}'", codeNum);

            return Repository.Instance.GetOne<PackingStyleModel>(sql);
        }

        public IEnumerable<PackingStyleModel> GetByFilter(PackingStyleModel model)
        {
            IEnumerable<PackingStyleModel> query = GetAll();

            if (model.CodeNum != null) { query = query.Where(p => p.CodeNum.Contains(model.CodeNum.ToString())); }
            if (model.StyleType > 0)
            {
                query = query.Where(p => p.StyleType.Equals(model.StyleType));
            }

            return query;
        }
    }
}