using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IWorkEntryRepo
    {
        IEnumerable<MaterialModel> GetAllMaterial(string plant);

        IEnumerable<MaterialModel> GetAllMaterial(string plant, int workOrderId);

        MaterialModel GetMaterial(int transactionLineID);

        MaterialModel GetMaterial(string plant, string partNum, string lotNum);

        IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlanningHeadModel model);

        int GetLastStep(int workOrderID);

        int GetLastWorkOrder(string plant);

        string GenWorkOrderFixFormat(int id);

        PlanningHeadModel Save(SessionInfo _session, PlanningHeadModel model);

        IEnumerable<PlanningHeadModel> GetWorkAll(string plant);

        PlanningHeadModel GetWorkById(string workOrderNum, int processStep, string plant);

        MaterialModel SaveMaterial(SessionInfo _session, MaterialModel model);

        bool DeleteMaterail(SessionInfo _session, MaterialModel model, out string msg);

        decimal CalUnitWgt(decimal T, decimal W, decimal L, decimal Gravity, decimal FrontCoat, decimal BackCoat);

        decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack);
    }
}