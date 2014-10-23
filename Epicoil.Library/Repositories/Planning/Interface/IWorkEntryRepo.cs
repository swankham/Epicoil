using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IWorkEntryRepo
    {
        IEnumerable<MaterialModel> GetAllMaterial(string plant);

        IEnumerable<MaterialModel> GetAllMaterial(string plant, int workOrderId);

        MaterialModel GetMaterial(string plant, string partNum, string lotNum);

        IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlaningHeadModel model);

        int GetLastStep(int workOrderID);

        int GetLastWorkOrder(string plant);

        string GenWorkOrderFixFormat(int id);

        PlaningHeadModel Save(SessionInfo _session, PlaningHeadModel model);

        IEnumerable<PlaningHeadModel> GetWorkAll(string plant);

        PlaningHeadModel GetWorkById(string workOrderNum, string plant);

        MaterialModel SaveMaterail(SessionInfo _session, MaterialModel model);
    }
}