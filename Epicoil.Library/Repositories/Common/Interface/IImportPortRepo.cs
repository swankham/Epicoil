using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IImportPortRepo
    {
        IEnumerable<PortModel> GetAll();

        IEnumerable<PortModel> GetByFilter(PortModel model);

        PortModel GetByID(string PortCode);
    }
}