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

        StoreInPlanHead GetByID(string Id);

        int GenerateId();

        bool GetPOByPONumber(string Plant, string PONumber, StoreInPlanHead model, out string msg, out int PONum);

        bool GetPOBySaleContract(string Plant, string SaleContract, StoreInPlanHead model, out string msg);

        bool GetPOLine(string PONumber, string SaleContract, StoreInPlanHead model, int POLine, out string msg);

        decimal GetReceivedWeight(int PONum, int POLine);

        StoreInPlanDetail GetPoLineDetail(string PONumber, int POLine);

        StoreInPlanHead SaveHead(StoreInPlanHead model, SessionInfo _session);

        void SaveArticle(StoreInPlanDetail model, SessionInfo _session);

        bool CheckInvoiceExisting(string Invoice);

        bool CheckStoreInFlagExist(int StoreInPlanId);

        bool CheckArticleExisting(string Article, int LineID = 0);

        IEnumerable<StoreInPlanDetail> GetDetail(int storeInPlantId);

        IEnumerable<StoreInPlanDetail> GetDetailArticle(int storeInPlantId, int POLine);

        IEnumerable<StoreInPlanDetail> GetDetailArticleITAKU(int storeInPlantId);

        void DeleteLine(int LineId);

        GetHeader GetHeaderByPONum(string PONum);

        int GetLastSeqId(int StoreInPlanId, int PONum, int POLine);

        string[] GetTableFromExcel(string FileName);

        IEnumerable<ExternalFileModel> GetFileDetail(string FileName, string Sheet);

        decimal GetMCSSAllowance(string MCSSNo);

        bool CloseStoreInPlan(int storeInPlanId);
    }
}