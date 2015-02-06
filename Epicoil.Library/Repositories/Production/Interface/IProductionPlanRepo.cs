using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Production
{
    public interface IProductionPlanRepo
    {
        ProductionPlanModel Get(SessionInfo _session);

        IEnumerable<WorkOrderPlanModel> GetWorksPlan(SessionInfo _session);
    }
}