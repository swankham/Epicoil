using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ISpecRepo
    {
        IEnumerable<SpecModel> GetAll();

        IEnumerable<SpecModel> GetAll(string commodity);

        IEnumerable<SpecModel> Get(SpecModel model);

        SpecModel GetByID(string SpecID, string commodity);
    }
}