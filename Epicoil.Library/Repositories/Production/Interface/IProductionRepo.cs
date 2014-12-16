using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Production
{
    public interface IProductionRepo
    {
        IEnumerable<ReasonModel> GetAllReasonAll();

        IEnumerable<SerialCuttingModel> GetAllSerialByProduction(int workOrderID);

        IEnumerable<SerialCuttingModel> GetAllSerialByWorkOrder(int workOrderID);

        IEnumerable<CuttedLineUpModel> GetCutLineUpAll(int workOrderId);

        ProductionHeadModel GetProdHead(int workOrderId);

        IEnumerable<CuttedLineUpModel> SaveCuttedLineUp(SessionInfo _session, CuttedLineUpModel model);

        ProductionHeadModel SaveHead(SessionInfo _session, ProductionHeadModel model);

        bool SaveLineStop(SessionInfo _session, LineStopModel model);

        bool SaveSerialCutting(SessionInfo _session, ProductionHeadModel model);

        bool DeteteCutFromPlan(int workOrderId, decimal cutSeq);

        bool ClearSerialInEpicor(SessionInfo _session, ProductionHeadModel model, out string msg);
    }
}