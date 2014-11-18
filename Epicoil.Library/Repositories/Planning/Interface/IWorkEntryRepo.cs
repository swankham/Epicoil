﻿using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IWorkEntryRepo
    {
        decimal CalUnitWgt(decimal T, decimal W, decimal L, decimal Gravity, decimal FrontCoat, decimal BackCoat);

        decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack);

        IEnumerable<CoilBackModel> DeleteCoilBack(SessionInfo _session, int workOrderId, int transactionLineID);

        bool DeleteCutting(SessionInfo _session, CutDesignModel model, out string msg);

        bool DeleteMaterail(SessionInfo _session, MaterialModel model, out string msg);

        IEnumerable<CutDesignModel> GenerateCuttingLine(SessionInfo _session, PlanningHeadModel head, out string risk, out string msg);

        IEnumerable<CutDesignModel> GenerateCuttingLineForLeveller(SessionInfo _session, PlanningHeadModel head, out string risk, out string msg);

        string GenWorkOrderFixFormat(int id);

        IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlanningHeadModel model);

        IEnumerable<MaterialModel> GetAllMaterial(string plant);

        IEnumerable<MaterialModel> GetAllMaterial(string plant, int workOrderId);

        IEnumerable<CoilBackModel> GetCoilBackAll(int workOrderId);

        CoilBackModel GetCoilBackByID(int transactionLineID);

        CutDesignModel GetCuttingByID(int LineID);

        IEnumerable<CutDesignModel> GetCuttingLines(int workOrderID);

        int GetLastStep(int workOrderID);

        int GetLastWorkOrder(string plant);

        MaterialModel GetMaterial(int transactionLineID);

        MaterialModel GetMaterial(string plant, string partNum, string lotNum);

        IEnumerable<PlanningHeadModel> GetWorkAll(string plant);

        PlanningHeadModel GetWorkById(string workOrderNum, int processStep, string plant);

        PlanningHeadModel Save(SessionInfo _session, PlanningHeadModel model);

        IEnumerable<CoilBackModel> SaveCoilBack(SessionInfo _session, CoilBackModel data);

        IEnumerable<CutDesignModel> SaveLineCutting(SessionInfo _session, PlanningHeadModel head, CutDesignModel data);

        MaterialModel SaveMaterial(SessionInfo _session, MaterialModel model);

        IEnumerable<SimulateModel> InsertSimulate(SessionInfo _session, PlanningHeadModel head, int cutDiv = 0);

        bool ClearSimulateLines(int workOrderID);

        IEnumerable<SimulateModel> GetSimulateAll(int workOrderID);

        IEnumerable<SimulateModel> GetSimulateLeveller(int workOrderID);

        IEnumerable<SimulateModel> UpdateSimulateByWorkOrder(SessionInfo _session, IEnumerable<SimulateModel> model, int workComplete);

        IEnumerable<CutDesignModel> UpdateCuttingByWorkOrder(SessionInfo _session, IEnumerable<SimulateModel> model, int workOrderID);

        IEnumerable<MaterialModel> UpdateMaterialByWorkOrder(SessionInfo _session, IEnumerable<MaterialModel> model, int workOrderID);

        IEnumerable<GeneratedSerialModel> GetSerialAllByWorkOrder(int workOrderID);

        IEnumerable<GeneratedSerialModel> GenerateSerial(SessionInfo _session, IEnumerable<SimulateModel> model, int workOrderID);

        string GetSerialByFormat(int StartId);

        int RunningLot();

        bool UnConfirmWork(int workOrderID);

        bool ImportSerialToEpicor(SessionInfo _session, PlanningHeadModel model, out string msg);

        bool ClearSerialInEpicor(SessionInfo _session, PlanningHeadModel model, out string msg);

        int GetStepByWorkOrder(int workOrderID);

        IEnumerable<LevellerSimulateModel> SaveLevellerSimulate(SessionInfo _session, LevellerSimulateModel model);

        IEnumerable<LevellerSimulateModel> GetLevellerSimAll(int workOrderID);

        bool ClearSimulateLeveller(int workOrderID);

        bool UnlockHold(int workOrderID);

        bool ClearSerialInEpicor(int workOrderID);

        IEnumerable<SimulateReshearModel> GetReshearSimulation(int workOrderID);

        IEnumerable<SimulateReshearModel> SaveReshearSimulation(SessionInfo _session, SimulateReshearModel model);
    }
}