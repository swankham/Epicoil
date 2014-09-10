using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICurrenciesRepo
    {
        IEnumerable<CurrenciesModel> GetAll();

        CurrenciesModel GetByID(string code);
    }
}