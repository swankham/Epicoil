using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IWorkEntryRepo
    {
        IEnumerable<MaterialModel> GetAllMaterial(string plant);

        IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlaningHeadModel model);
    }
}