using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class BussinessTypeRepo : IBussinessTypeRepo
    {
        public IEnumerable<BussinessTypeModel> GetAll()
        {
            string sql = string.Format(@"select * from UD25 order by key1 asc");

            return Repository.Instance.GetMany<BussinessTypeModel>(sql);
        }

        public IEnumerable<BussinessTypeModel> GetByFilter(BussinessTypeModel model)
        {
            IEnumerable<BussinessTypeModel> query = GetAll();

            if (model.BussinessCode != null) { query = query.Where(p => p.BussinessCode.Contains(model.BussinessCode.ToString())); }
            if (model.BussinessName != null) { query = query.Where(p => p.BussinessName.Contains(model.BussinessName.ToString())); }

            return query;
        }

        public BussinessTypeModel GetByID(string code)
        {
            string sql = string.Format(@"Select * from UD25 where Key1 = N'{0}'", code);
            return Repository.Instance.GetOne<BussinessTypeModel>(sql);
        }
    }
}