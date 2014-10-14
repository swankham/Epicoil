using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class MakerRepo : IMakerRepo
    {
        public IEnumerable<MakerModel> GetAll()
        {
                string sql = string.Format(@"select * from UD19 order by key1 asc");
                var resut = Repository.Instance.GetMany<MakerModel>(sql);
                return resut;
        }

        public IEnumerable<MakerModel> GetByFilter(MakerModel model)
        {
            IEnumerable<MakerModel> query = GetAll();

            if (model.MakerCode != null) { query = query.Where(p => p.MakerCode.Contains(model.MakerCode.ToString())); }
            if (model.MakerName != null) { query = query.Where(p => p.MakerName.Contains(model.MakerName.ToString())); }

            return query;
        }

        public MakerModel GetByID(string code)
        {
            string sql = string.Format(@"Select * from UD19 where Key1 = N'{0}'", code);
            return Repository.Instance.GetOne<MakerModel>(sql);
        }
    }
}