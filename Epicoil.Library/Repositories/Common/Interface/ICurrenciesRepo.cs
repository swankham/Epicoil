using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICurrenciesRepo
    {
        IEnumerable<CurrencyModel> GetAll();

        CurrencyModel GetByID(string code);
    }
}