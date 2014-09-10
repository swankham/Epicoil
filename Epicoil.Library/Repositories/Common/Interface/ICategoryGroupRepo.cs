using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICategoryGroupRepo
    {
        IEnumerable<CategoryGroupModel> GetAll();

        IEnumerable<CategoryGroupModel> Get(CategoryGroupModel model);

        CategoryGroupModel GetByID(string code);
    }
}