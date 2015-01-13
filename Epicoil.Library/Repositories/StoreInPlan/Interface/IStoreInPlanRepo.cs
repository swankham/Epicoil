using System;
using System.Collections.Generic;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;

namespace Epicoil.Library.Repositories.StoreInPlan
{
    public interface IStoreInPlanRepo
    {
        IEnumerable<StoreInPlanDialogModel> GetAll();

        IEnumerable<StoreInPlanDialogModel> GetAll(int Status);

        IEnumerable<StoreInPlanDialogModel> GetAll(int Status, string TransactionType);

        IEnumerable<ImexCheckModel> GetAllIMEX();

        IEnumerable<ImexCheckModel> GetAllIMEXByFilter(ImexCheckModel model, DateTime dateFrom, DateTime dateTo);

        IEnumerable<StoreInPlanDialogModel> GetByFilter(StoreInPlanDialogModel model, int FilterType);

        StoreInPlanHeadModel GetByID(string Id);

        int GenerateId();

        bool GetPOByPONumber(string Plant, string PONumber, StoreInPlanHeadModel model, out string msg, out int PONum);

        bool GetPOBySaleContract(string Plant, string SaleContract, StoreInPlanHeadModel model, out string msg);

        bool GetPOLine(string PONumber, string SaleContract, StoreInPlanHeadModel model, int POLine, out string msg);

        decimal GetReceivedWeight(int PONum, int POLine);

        StoreInPlanDetailModel GetPoLineDetail(string PONumber, int POLine);

        StoreInPlanHeadModel SaveHead(StoreInPlanHeadModel model, SessionInfo _session);

        void SaveArticle(StoreInPlanDetailModel model, SessionInfo _session);

        bool CheckInvoiceExisting(string Invoice);

        bool CheckStoreInFlagExist(int StoreInPlanId);

        bool CheckArticleExisting(string Article, int LineID = 0);

        IEnumerable<StoreInPlanDetailModel> GetDetail(int storeInPlantId);

        IEnumerable<StoreInPlanDetailModel> GetDetailArticle(int storeInPlantId, int POLine);

        IEnumerable<StoreInPlanDetailModel> GetDetailArticleITAKU(int storeInPlantId);

        void DeleteLine(int LineId);

        POHeaderModel GetHeaderByPONum(string PONum);

        int GetLastSeqId(int StoreInPlanId, int PONum, int POLine);

        string[] GetTableFromExcel(string FileName);

        IEnumerable<ExternalFileModel> GetFileDetail(string FileName, string Sheet);

        decimal GetMCSSAllowance(string MCSSNo);

        bool CloseStoreInPlan(int storeInPlanId);
    }
}