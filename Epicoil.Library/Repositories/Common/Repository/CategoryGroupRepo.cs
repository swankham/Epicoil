using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class CategoryGroupRepo : ICategoryGroupRepo
    {
        public IEnumerable<CategoryGroupModel> GetAll()
        {
            string sql = string.Format(@"Select * from UD22 Order by Key1 asc");

            return Repository.Instance.GetMany<CategoryGroupModel>(sql);
        }

        public IEnumerable<CategoryGroupModel> Get(CategoryGroupModel model)
        {
            IEnumerable<CategoryGroupModel> query = GetAll();

            if (model.CategoryGroupName != null) { query = query.Where(p => p.CategoryGroupName.Contains(model.CategoryGroupName.ToString())); }

            return query;
        }

        public CategoryGroupModel GetByID(string code)
        {
            string sql = string.Format(@"Select * from UD22 where Key1 = N'{0}'", code);
            return Repository.Instance.GetOne<CategoryGroupModel>(sql);
        }
    }
}