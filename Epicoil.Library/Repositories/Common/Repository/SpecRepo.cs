using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class SpecRepo : ISpecRepo
    {
        public IEnumerable<SpecModel> GetAll()
        {
            string sql = @"select * from ud30 order by key1, key2, character01, shortchar02 asc";
            return Repository.Instance.GetMany<SpecModel>(sql);
        }

        public IEnumerable<SpecModel> GetAll(string commodity)
        {
            string sql = string.Format(@"select * from ud30 where key2 = N'{0}' order by key1, key2, character01, shortchar02 asc", commodity);
            return Repository.Instance.GetMany<SpecModel>(sql);
        }

        public IEnumerable<SpecModel> Get(SpecModel model)
        {
            IEnumerable<SpecModel> query = GetAll();

            if (model.SpecID != null) { query = query.Where(p => p.SpecID.Contains(model.SpecID.ToString())); }
            if (model.SpecName != null) { query = query.Where(p => p.SpecName.Contains(model.SpecName.ToString())); }
            if (model.Commodity != null) { query = query.Where(p => p.Commodity.Contains(model.Commodity.ToString())); }

            return query.ToList();
        }

        public SpecModel GetByID(string SpecID, string commodity)
        {
            string sql = string.Format(@"Select * from UD30 where Key1 = N'{0}' and Key2 = N'{1}'", SpecID, commodity);
            return Repository.Instance.GetOne<SpecModel>(sql);
        }
    }
}