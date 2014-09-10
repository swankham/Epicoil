using System.Collections.Generic;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public interface IPackingStyle
    {
        IEnumerable<PackingStyleModel> GetAll();

        IEnumerable<PackingStyleModel> GetByFilter(PackingStyleModel model);

        PackingStyleModel Get(string codeNum);
    }
}