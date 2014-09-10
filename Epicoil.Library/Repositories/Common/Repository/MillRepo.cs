using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class MillRepo : IMillRepo
    {
        public IEnumerable<MillModel> GetAll()
        {
            string sql = @"select * from ud14 order by key1, key2 asc";
            return Repository.Instance.GetMany<MillModel>(sql);
        }

        public IEnumerable<MillModel> GetAll(string makercode)
        {
            string sql = string.Format(@"select * from ud14 where key1 = N'{0}' order by key2 asc", makercode);
            return Repository.Instance.GetMany<MillModel>(sql);
        }

        public IEnumerable<MillModel> Get(MillModel model)
        {
            IEnumerable<MillModel> query = GetAll();

            if (model.MakerCode != null) { query = query.Where(p => p.MakerCode.Contains(model.MakerCode.ToString())); }
            if (model.MillCode != null) { query = query.Where(p => p.MillCode.Contains(model.MillCode.ToString())); }
            if (model.MillName != null) { query = query.Where(p => p.MillName.Contains(model.MillName.ToString())); }

            return query.ToList();
        }

        public MillModel GetByID(string millcode, string makercode)
        {
            string sql = string.Format(@"Select * from UD14 where Key2 = N'{0}' and Key1 = N'{1}'", millcode, makercode);
            return Repository.Instance.GetOne<MillModel>(sql);
        }
    }
}