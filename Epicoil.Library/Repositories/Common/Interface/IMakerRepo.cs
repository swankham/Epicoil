using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IMakerRepo
    {
        IEnumerable<MakerModel> GetAll();

        IEnumerable<MakerModel> GetByFilter(MakerModel model);

        MakerModel GetByID(string code);
    }
}