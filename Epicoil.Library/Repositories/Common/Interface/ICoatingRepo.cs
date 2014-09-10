using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICoatingRepo
    {
        IEnumerable<CoatingModel> GetAll();

        IEnumerable<CoatingModel> Get(CoatingModel model);

        CoatingModel GetByID(string Coating);
    }
}