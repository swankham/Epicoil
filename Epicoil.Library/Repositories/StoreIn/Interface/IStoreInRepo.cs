using Epicor.Mfg.BO;
using System.Collections.Generic;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreIn;

namespace Epicoil.Library.Repositories.StoreIn
{
    public interface IStoreInRepo
    {
        int GenerateId(string PlantID);

        StoreInHead GetStoreInPlanByID(int Id);

        string NewPart(NewPartModel model, SessionInfo epiSession, int StoreInNo, out bool IsSucces, out string msgError);

        bool NewPartCollection(NewPartModel model, Epicor.Mfg.Core.Session epiSession, out bool IsSucces, out string msgError);

        string GetSerialByFormat(int StartId);

        int RunningPart();

        void UpdateStock(string PartNum, decimal qty);

        void UpdateStoreInPlan(int StoreInPlanId);

        void UpdateStoreInPlanDetail(int LineId);

        StoreInHead InsertStoreIn(StoreInHead model, SessionInfo epiSession);

        bool InsertStoreInLine(List<StoreInDetail> model, SessionInfo epiSession, int StoreInid, decimal TransactionID, out string msg);

        IEnumerable<StoreInDetail> GetDetail(int storeInPlantId);

        IEnumerable<StoreInDetail> GetDetailArticle(int storeInPlantId, string PONum, int POLine);

        IEnumerable<StoreInDetail> GetDetailArticleITAKU(int LineID);

        StoreInDetail GetArticleListToSave(int LineId);

        bool GetNewRcv(RecieptHeadModel head, List<ReceiptDetailModel> model, Epicor.Mfg.Core.Session epiSession, out bool IsSucces, out string msgError);

        void GetNewRcvDtl(Epicor.Mfg.Core.Session epiSession, ReceiptDataSet dsReceipt);

        void GetRowsToNewRcvDetail(int storeInPlantId, SessionInfo epiSession, ReceiptDataSet dsReceipt);

        StoreInHead GetStoreInByID(int storeInID, string Plant);

        IEnumerable<NewPartModel> GetNewPartCollection(decimal TransactionID);

        void UpdateArticleToStoreIn(decimal TransactionLineID, string ArticleNo);

        RecieptHeadModel GetDataToNewReceiptPO(decimal TransactionID);

        void UpdatePORelBeforeReceipt(int PONum, int POLine);

        IEnumerable<ReceiptDetailModel> GetPODetailToReceipt(decimal TransactionID);

        IEnumerable<StoreInHeadBalance> GetStoreInBalanceAll(string Plant, int StoreInPlanId);

        IEnumerable<StoreInDetail> GetDetailArticle(int storeInPlantId, decimal TransactionID);

        void UpdateStoreInFlag(int storeInPlanId);
    }
}