using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IWharehouseRepo
    {
        IEnumerable<WharehouseModel> GetAll(string Plant);

        WharehouseModel GetByID(string code);
    }
}