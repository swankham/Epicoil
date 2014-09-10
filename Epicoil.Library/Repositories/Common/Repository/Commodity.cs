using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class Commodity : ICommodity
    {
        public IEnumerable<CommodityModel> GetAll()
        {
            string sql = string.Format(@"select * from UD29 order by key1 asc");

            return Repository.Instance.GetMany<CommodityModel>(sql);
        }

        public IEnumerable<CommodityModel> GetByFilter(CommodityModel model)
        {
            IEnumerable<CommodityModel> query = GetAll();

            if (model.CommodityCode != null) { query = query.Where(p => p.CommodityCode.Contains(model.CommodityCode.ToString())); }
            if (model.CommodityName != null) { query = query.Where(p => p.CommodityName.Contains(model.CommodityName.ToString())); }

            return query;
        }

        public CommodityModel GetByID(string CmdtCode)
        {
            string sql = string.Format(@"Select * from UD29 where Key1 = N'{0}'", CmdtCode);
            return Repository.Instance.GetOne<CommodityModel>(sql);
        }
    }
}