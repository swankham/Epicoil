using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IPackingOrderRepo
    {
        IEnumerable<PlanningHeadModel> GetWorkAll(PlanningHeadModel model);

        PackingOrderModel GetPackOrderByID(int workOrderId);

        bool PackOrderExist(int workOrderId);

        IEnumerable<PackStyleOrderModel> GetPackStyleByWorkOrder(int workOrderId, int headerId = 0);

        IEnumerable<PackStyleOrderModel> GetPackStyleByPackOrder(int workOrderId);

        bool SavePackOrder(SessionInfo _session, PackingOrderModel model, out PackingOrderModel resultRow);

        bool SavePackStyles(SessionInfo _session, IEnumerable<PackStyleOrderModel> packStyles = null, PackStyleOrderModel model = null);

        bool SaveSerialByStyle(SessionInfo _session, IEnumerable<SerialsPackingModel> models);

        IEnumerable<SerialsPackingModel> GetSerialForFirstDefault(PackStyleOrderModel model);

        IEnumerable<SerialsPackingModel> GetSerialByStyleID(int headerId, int styleLineId = 0);

        IEnumerable<SkidPackingModel> GetSkidPackingsByWork(int headLineId);

        IEnumerable<SkidPackingModel> AddSkidPacking(SessionInfo _session, SkidPackingModel pack);

        IEnumerable<SkidPackingModel> DeleteSkidPacking(SessionInfo _session, int headLineId, int lineId, int serialLineId = 0);

        IEnumerable<SkidPackingModel> AddSerialToSkid(SessionInfo _session, int headId, int packid, string serials);
    }
}