using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IMillRepo
    {
        IEnumerable<MillModel> GetAll();

        IEnumerable<MillModel> GetAll(string makercode);

        IEnumerable<MillModel> Get(MillModel model);

        MillModel GetByID(string millcode, string makercode);
    }
}