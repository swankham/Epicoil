using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Production
{
    public interface IMaterialFindingRepo
    {
        IEnumerable<MaterialFindingModel> GetAllMaterailTracker(string plant);

        IEnumerable<MaterialFindingModel> GetAllMaterailTrackerByFilter(string plant, MaterialFindingModel model, IEnumerable<MaterialFindingModel> data = null);

        IEnumerable<MaterialFindingModel> SaveMaterial(SessionInfo _session, MaterialFindingModel model);
    }
}