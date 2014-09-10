using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class CoatingRepo : ICoatingRepo
    {
        public IEnumerable<CoatingModel> GetAll()
        {
            string sql = @"select * from ud31 order by key1, character01, character02 asc";

            return Repository.Instance.GetMany<CoatingModel>(sql);
        }

        public IEnumerable<CoatingModel> Get(CoatingModel model)
        {
            IEnumerable<CoatingModel> query = GetAll();

            if (model.CoatingPlate != null) { query = query.Where(p => p.CoatingPlate.Contains(model.CoatingPlate.ToString())); }
            if (model.CoatingName != null) { query = query.Where(p => p.CoatingName.Contains(model.CoatingName.ToString())); }

            return query.ToList();
        }

        public CoatingModel GetByID(string Coating)
        {
            string sql = string.Format(@"Select * from UD31 where Key1 = N'{0}'", Coating);
            return Repository.Instance.GetOne<CoatingModel>(sql);
        }
    }
}