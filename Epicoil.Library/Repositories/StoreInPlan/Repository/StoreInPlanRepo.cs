using System;
using System.Collections.Generic;
using System.Linq;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;

namespace Epicoil.Library.Repositories.StoreInPlan
{
    public class StoreInPlanRepo : IStoreInPlanRepo
    {
        private readonly ICurrenciesRepo _repoCurr;
        private readonly IImportPortRepo _repoPort;

        public StoreInPlanRepo()
        {
            this._repoCurr = new CurrenciesRepo();
            this._repoPort = new ImportPortRepo();
        }

        public IEnumerable<StoreInPlanDialogModel> GetAll()
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , 0 as PONum, '' as PONumber, stph.CustID, cust.Name as CustomerName
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          ORDER BY stph.StoreInPlanId DESC");

            return Repository.Instance.GetMany<StoreInPlanDialogModel>(sql);
        }

        public IEnumerable<StoreInPlanDialogModel> GetAll(int Status)
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup, stpd.PONum, ISNULL(stpd.PONumber,'') as PONumber
                                                , stph.CustID, cust.Name as CustomerName
                                          FROM ucc_ic_StoreInPlanHead stph
												LEFT JOIN ucc_ic_StoreInPlanDtl stpd ON(stph.StoreInPlanId = stpd.StoreInPlanId)
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.StoreInFlag = {0} AND stph.ImexConfirm = 1 AND stph.TransactionType = '2'
										  GROUP BY stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType, busi.Character01
		                                        , stph.SupplierCode, ven.Name, ISNULL(stph.CurrencyCode,'THB')
		                                        , stph.MakerCode, maker.Character01
		                                        , stph.MillCode, mill.Character01, stpd.PONum, ISNULL(stpd.PONumber,'')
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup, stph.CustID, cust.Name
                                          ORDER BY stph.StoreInPlanId DESC", Status);

            return Repository.Instance.GetMany<StoreInPlanDialogModel>(sql);
        }

        public IEnumerable<StoreInPlanDialogModel> GetAll(int Status, string TransactionType)
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup, stpd.PONum, ISNULL(stpd.PONumber,'') as PONumber
                                                , stph.CustID, cust.Name as CustomerName
                                          FROM ucc_ic_StoreInPlanHead stph
												LEFT JOIN ucc_ic_StoreInPlanDtl stpd ON(stph.StoreInPlanId = stpd.StoreInPlanId)
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.StoreInFlag = 0 AND stph.ImexConfirm = 1 AND stph.TransactionType IN ('0','1')
										  GROUP BY stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType, busi.Character01
		                                        , stph.SupplierCode, ven.Name, ISNULL(stph.CurrencyCode,'THB')
		                                        , stph.MakerCode, maker.Character01
		                                        , stph.MillCode, mill.Character01, stpd.PONum, ISNULL(stpd.PONumber,'')
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup, stph.CustID, cust.Name
                                          ORDER BY stph.StoreInPlanId DESC", Status);

            return Repository.Instance.GetMany<StoreInPlanDialogModel>(sql);
        }

        public IEnumerable<ImexCheckModel> GetAllIMEX()
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup, stph.CustID, cust.Name as CustomerName
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
	                                      WHERE stph.TransactionType = 0 and stph.StoreInFlag = 0
                                          ORDER BY stph.StoreInPlanId, stph.LastUpdateDate DESC");

            return Repository.Instance.GetMany<ImexCheckModel>(sql);
        }

        public IEnumerable<ImexCheckModel> GetAllIMEXByFilter(ImexCheckModel model, DateTime dateFrom, DateTime dateTo)
        {
            IEnumerable<ImexCheckModel> query = new List<ImexCheckModel>();

            query = this.GetAllIMEX();
            query = query.Where(p => p.InvoiceDate.Date >= dateFrom.Date.Date && p.InvoiceDate.Date <= dateTo.Date.Date);

            if (!string.IsNullOrEmpty(model.StoreInPlanNum)) { query = query.Where(p => p.StoreInPlanNum.Contains(model.StoreInPlanNum)); }
            if (!string.IsNullOrEmpty(model.InvoiceNum)) { query = query.Where(p => p.InvoiceNum.Contains(model.InvoiceNum)); }

            return query.ToList();
        }

        public StoreInPlanHead GetByID(string Id)
        {
            StoreInPlanHead result;
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag, stph.ImexConfirm, stph.ImexRemark
                                                , stph.CustID, cust.Name as CustomerName
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.StoreInPlanNum = N'{0}' ", Id);

            result = Repository.Instance.GetOne<StoreInPlanHead>(sql);
            if (result != null)
            {
                result.CurrenciesList = this._repoCurr.GetAll();
                result.PortList = this._repoPort.GetAll();
            }

            return result;
        }

        public IEnumerable<StoreInPlanDialogModel> GetByFilter(StoreInPlanDialogModel model, int FilterType)
        {
            IEnumerable<StoreInPlanDialogModel> query = new List<StoreInPlanDialogModel>();

            if (FilterType == 0)
            {
                query = GetAll();
            }
            else if (FilterType == 1)
            {
                query = GetAll(model.ImportFlag);
            }
            else
            {
                query = GetAll(model.ImportFlag, model.TransactionType);
            }

            if (!string.IsNullOrEmpty(model.StoreInPlanNum)) { query = query.Where(p => p.StoreInPlanNum.Contains(model.StoreInPlanNum)); }
            if (!string.IsNullOrEmpty(model.InvoiceNum)) { query = query.Where(p => p.InvoiceNum.Contains(model.InvoiceNum)); }
            if (!string.IsNullOrEmpty(model.PONumber)) { query = query.Where(p => p.PONumber.GetString().Contains(model.PONumber.GetString())); }

            return query.ToList();
        }

        public int GenerateId()
        {
            string sql = @" SELECT TOP 1 * FROM ucc_ic_StoreInPlanHead ORDER BY StoreInPlanId DESC";

            var id = Repository.Instance.GetOne<int>(sql, "StoreInPlanId");

            return Convert.ToInt32(id) + 1;
        }

        public bool GetPOByPONumber(string Plant, string PONumber, StoreInPlanHead model, out string msg, out int PONum)
        {
            bool result = false;
            string sql = string.Format(@"SELECT * FROM POHeader WHERE ShortChar10 = N'{0}' and ShortChar03 = N'{1}' and ShortChar04 = N'{2}' and Approve = 1"
                                        , PONumber, model.MakerCode, model.MillCode);

            msg = Repository.Instance.GetOne<string>(sql, "ShortChar02");
            PONum = Repository.Instance.GetOne<int>(sql, "PONUM");

            if (msg != null)
            { result = true; }
            return result;
        }

        public bool GetPOBySaleContract(string Plant, string SaleContract, StoreInPlanHead model, out string msg)
        {
            bool result = false;
            string sql = string.Format(@"SELECT * FROM POHeader WHERE ShortChar02 = N'{0}' and ShortChar03 = N'{1}' and ShortChar04 = N'{2}'  and Approve = 1"
                                        , SaleContract, model.MakerCode, model.MillCode);

            msg = Repository.Instance.GetOne<string>(sql, "ShortChar10");
            if (msg != null)
            { result = true; }
            return result;
        }

        public bool GetPOLine(string PONumber, string SaleContract, StoreInPlanHead model, int POLine, out string msg)
        {
            bool result = false;
            string sql = string.Format(@"SELECT pod.* FROM PODetail pod
                                         INNER JOIN POHeader poh ON(pod.PONUM = poh.PONum)
                                         WHERE poh.ShortChar10 = N'{0}' AND poh.ShortChar02 = N'{1}' AND pod.POLine = {2}
                                         AND poh.ShortChar03 = N'{3}' and poh.ShortChar04 = N'{4}'"
                                        , PONumber, SaleContract, POLine, model.MakerCode, model.MillCode);

            msg = Repository.Instance.GetOne<string>(sql, "ShortChar10");
            if (msg != null)
            { result = true; }
            return result;
        }

        public decimal GetReceivedWeight(int PONum, int POLine)
        {
            decimal num = new decimal(0);
            string sql = string.Format(@"SELECT ISNULL(sum(Weight),0) as Weight FROM ucc_ic_StoreInPlanDtl
                                            WHERE PONum = {0} AND POLine = {1}", PONum, POLine);
            return Repository.Instance.GetOne<decimal>(sql, "Weight");
        }

        public StoreInPlanDetail GetPoLineDetail(string PONumber, int POLine)
        {
            string sql = string.Format(@"SELECT 0 as SeqId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                        , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                        , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, '' as ArticleNo
	                                        , 0 as Quantity, 0 as Weight, '' as Place, '' as Note, pod.Character02 as Enduser, pod.Character03 as ActualEndUser, GETDATE() as ReceiptDate
	                                        , 0 as TaxPaid, '' as PackingNumber, pod.XOrderQty as OpenBalance, 0 as LineId, 0 as StoreInPlanId, 0 as DutyRate
                                            , '' as StockNo, '' as Location, pod.Number18 as Amount, pod.DocUnitCost as UnitPrice
                                            , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                            , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName
                                            , '' as Category, '' as SContract, pod.ShortChar03 as CoatingCode, pod.PartNum, 0 as StoreInFlag
                                            , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                        FROM PODetail pod
                                        INNER JOIN POHeader poh ON(pod.PONUM = poh.PONum)
                                        LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
										LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                        LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(pod.ShortChar01 = spec.Key2 and pod.ShortChar02 = spec.Key1)
	                                    LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
                                        WHERE poh.ShortChar10 = N'{0}' AND pod.POLine = {1}"
                                        , PONumber, POLine);

            return Repository.Instance.GetOne<StoreInPlanDetail>(sql);
        }

        public StoreInPlanHead SaveHead(StoreInPlanHead model, SessionInfo _session)
        {
            ///TODO not yet fix GETDATE()
            string sql = "";
            if (model.InsertState)
            {
                model.StoreInPlanId = GenerateId();
                model.StoreInPlanNum = model.StoreInPlanId.ToString("000000");
            }

            if (model.ImportFlag != 0)
            {
                model.ImexConfirm = "1";
            }

            sql += string.Format(@"IF NOT EXISTS
									(
										SELECT * FROM ucc_ic_StoreInPlanHead (NOLOCK)
										WHERE StoreInPlanNum = N'{3}'
									)
									BEGIN
                                        INSERT INTO ucc_ic_StoreInPlanHead
                                                   (Company
                                                   ,Plant
                                                   ,StoreInPlanId
                                                   ,StoreInPlanNum
                                                   ,TransactionType
                                                   ,BType
                                                   ,SupplierCode
                                                   ,IMexItemNo
                                                   ,InvoiceNum
                                                   ,InvoiceDate
                                                   ,PORate
                                                   ,DutyRate
                                                   ,TisiFlag
                                                   ,LoadPort
                                                   ,ArivePort
                                                   ,ETDDate
                                                   ,ETADate
                                                   ,Vessel
                                                   ,IMEXChecked
                                                   ,MillCode
                                                   ,MakerCode
                                                   ,StoreInFlag
                                                   ,CreationDate
                                                   ,LastUpdateDate
                                                   ,CreatedBy
                                                   ,UpdatedBy
                                                   ,CurrencyCode
                                                   ,ImexConfirm
                                                   ,UserGroup
                                                   ,CustID)
                                             VALUES
                                                   (N'{0}'    --<Company, nvarchar(8),>
                                                   , N'{1}'   --<Plant, nvarchar(8),>
                                                   , {2}   --<StoreInPlanId, int,>
                                                   , N'{3}'   --<StoreInPlanNum, nvarchar(10),>
                                                   , N'{4}'   --<TransactionType, nvarchar(15),>
                                                   , N'{5}'   --<BType, nchar(10),>
                                                   , N'{6}'   --<SupplierCode, nvarchar(50),>
                                                   , N'{7}'   --<IMexItemNo, nvarchar(30),>
                                                   , N'{8}'   --<InvoiceNum, nvarchar(15),>
                                                   , GETDATE()   --<InvoiceDate, datetime,>
                                                   , {9}   --<PORate, int,>
                                                   , {10}   --<DutyRate, int,>
                                                   , {11}   --<TisiFlag, tinyint,>
                                                   , N'{12}'   --<LoadPort, nvarchar(30),>
                                                   , N'{13}'   --<ArivePort, nvarchar(30),>
                                                   , GETDATE()   --<ETDDate, datetime,>
                                                   , GETDATE()   --<ETADate, datetime,>
                                                   , N'{14}'   --<Vessel, nvarchar(30),>
                                                   , {15}   --<IMEXChecked, int,>
                                                   , N'{16}'   --<MillCode, nvarchar(50),>
                                                   , N'{17}'   --<MakerCode, nvarchar(50),>
                                                   , {18}  --<StoreInFlag, tinyint,>
                                                   , GETDATE()   --<CreationDate, datetime,>
                                                   , GETDATE()   --<LastUpdateDate, datetime,>
                                                   , N'{19}'   --<CreatedBy, varchar(45),>
                                                   , N'{19}'   --<UpdatedBy, varchar(45),>
                                                   , N'{20}'   --<CurrencyCode, nchar(10),>
                                                   , N'{15}'   --<IMEXChecked, nchar(10),>
                                                   , N'{22}'   --<IMEXChecked, nchar(10),>
                                                   , N'{23}'  --CustID
                                             )
									END
                                    ELSE
                                    BEGIN
                                        UPDATE ucc_ic_StoreInPlanHead
                                           SET Company = N'{0}'     --<Company, nvarchar(8),>
                                              ,Plant = N'{1}'    --<Plant, nvarchar(8),>
                                              ,StoreInPlanId = {2}    --<StoreInPlanId, int,>
                                              ,StoreInPlanNum = N'{3}'    --<StoreInPlanNum, nvarchar(10),>
                                              ,TransactionType = N'{4}'    --<TransactionType, nvarchar(15),>
                                              ,BType = N'{5}'    --<BType, nchar(10),>
                                              ,SupplierCode = N'{6}'    --<SupplierCode, nvarchar(50),>
                                              ,IMexItemNo = N'{7}'    --<IMexItemNo, nvarchar(30),>
                                              ,InvoiceNum = N'{8}'    --<InvoiceNum, nvarchar(15),>
                                              ,InvoiceDate = GETDATE()    --<InvoiceDate, datetime,>
                                              ,PORate = {9}    --<PORate, int,>
                                              ,DutyRate = {10}    --<DutyRate, int,>
                                              ,TisiFlag = {11}    --<TisiFlag, tinyint,>
                                              ,LoadPort = N'{12}'    --<LoadPort, nvarchar(30),>
                                              ,ArivePort = N'{13}'    --<ArivePort, nvarchar(30),>
                                              ,ETDDate = GETDATE()    --<ETDDate, datetime,>
                                              ,ETADate = GETDATE()    --<ETADate, datetime,>
                                              ,Vessel = N'{14}'    --<Vessel, nvarchar(30),>
                                              ,IMEXChecked = {15}    --<IMEXChecked, int,>
                                              ,MillCode = N'{16}'    --<MillCode, nvarchar(50),>
                                              ,MakerCode = N'{17}'    --<MakerCode, nvarchar(50),>
                                              ,StoreInFlag = {18}    --<StoreInFlag, tinyint,>
                                              ,CreationDate = GETDATE()    --<CreationDate, datetime,>
                                              ,LastUpdateDate =  GETDATE()   --<LastUpdateDate, datetime,>
                                              ,CreatedBy = N'{19}'    --<CreatedBy, varchar(45),>
                                              ,UpdatedBy = N'{19}'    --<UpdatedBy, varchar(45),>
                                              ,CurrencyCode = N'{20}'    --<CurrencyCode, nchar(10),>
                                              ,ImexConfirm = N'{15}'    --<IMEXChecked, int,>
                                              ,ImexRemark = N'{21}'
                                              ,UserGroup = N'{22}'
                                              ,CustID = N'{23}' 
                                         WHERE StoreInPlanNum = N'{3}'
                                    END" + Environment.NewLine
                                     , _session.CompanyID
                                     , _session.PlantID
                                     , model.StoreInPlanId
                                     , model.StoreInPlanNum
                                     , model.ImportFlag
                                     , model.BussinessType
                                     , model.SupplierCode
                                     , model.IMexItemNo
                                     , model.InvoiceNum
                                     , model.ExchangeRate
                                     , 0
                                     , Convert.ToInt32(string.IsNullOrEmpty(model.TisiFlag) ? "0" : model.TisiFlag)
                                     , model.LoadPort
                                     , model.ArivePort
                                     , model.Vessel
                                     , model.ImexConfirm
                                     , model.MillCode
                                     , model.MakerCode
                                     , 0
                                     , _session.UserID
                                     , model.CurrencyCode
                                     , model.ImexRemark
                                     , model.UserGroup
                                     , model.CustID);

            Repository.Instance.ExecuteWithTransaction(sql, "Store In Plan Header");
            return GetByID(model.StoreInPlanNum);
        }

        public bool CheckInvoiceExisting(string Invoice)
        {
            bool result = false;
            string sql = string.Format(@"SELECT * FROM ucc_ic_StoreInPlanHead WHERE InvoiceNum = N'{0}'", Invoice);

            if (!string.IsNullOrEmpty(Repository.Instance.GetOne<string>(sql, "InvoiceNum")))
            {
                result = true;
            }
            return result;
        }

        public bool CheckArticleExisting(string Article, int LineID = 0)
        {
            bool result = false;
            string sql = string.Format(@"SELECT * FROM ucc_ic_StoreInPlanDtl WHERE ArticleNo = N'{0}' AND LineId != {1}", Article, LineID);

            if (!string.IsNullOrEmpty(Repository.Instance.GetOne<string>(sql, "ArticleNo")))
            {
                result = true;
            }
            return result;
        }

        public void SaveArticle(StoreInPlanDetail model, SessionInfo _session)
        {
            ///TODO not yet fix GETDATE()
            string sql = "";
            sql += string.Format(@"IF NOT EXISTS
									(
										SELECT * FROM ucc_ic_StoreInPlanDtl (NOLOCK)
										WHERE LineId = {17}
									)
									BEGIN
                                    INSERT INTO ucc_ic_StoreInPlanDtl
                                               (Company
                                               ,Plant
                                               ,StoreInPlanId
                                               ,PONumber
                                               ,PONum
                                               ,POLine
                                               ,SpecName
                                               ,Thick
                                               ,ArticleNo
                                               ,Quantity
                                               ,Weight
                                               ,Place
                                               ,Note
                                               ,CreationDate
                                               ,LastUpdateDate
                                               ,CreatedBy
                                               ,UpdatedBy
                                               ,Width
                                               ,Length
                                               ,SeqId
                                               ,StoreInFlag
                                               ,SpecCode       ------------
                                               ,CommodityCode
                                               ,CoatingCode
                                               ,PackingNo
                                               ,Category
	                                           ,SContract)
                                             VALUES
                                               (N'{0}'    --<Company, nvarchar(15),>
                                               ,N'{1}'    --<Plant, nvarchar(15),>
                                               ,{2}    --<StoreInPlanId, int,>
                                               ,N'{13}'    --<PONumber, nvarchar(20),>
                                               ,{3}    --<PONum, int,>
                                               ,{4}    --<POLine, int,>
                                               ,N'{5}'    --<SpecName, nvarchar(50),>
                                               ,{6}    --<Thick, decimal(15,2),>
                                               ,N'{7}'    --<ArticleNo, nvarchar(50),>
                                               ,{8}    --<Quantity, decimal(15,2),>
                                               ,{9}    --<Weight, decimal(15,2),>
                                               ,N'{10}'    --<Place, nvarchar(30),>
                                               ,N'{11}'    --<Note, nvarchar(max),>
                                               ,GETDATE()    --<CreationDate, datetime,>
                                               ,GETDATE()    --<LastUpdateDate, datetime,>
                                               ,N'{12}'    --<CreatedBy, varchar(45),>
                                               ,N'{12}'    --<UpdatedBy, varchar(45),>)
                                               ,{14}    --<Width, decimal(15,2),>
                                               ,{15}    --<Length, decimal(15,2),>
                                               ,{16}
                                               ,{18}
                                               ,N'{19}'    ------------
                                               ,N'{20}'
                                               ,N'{21}'
                                               ,N'{22}'
                                               ,N'{23}'
                                               ,N'{24}'
                                             )
                                    END
                                    ELSE
                                    BEGIN
                                        UPDATE ucc_ic_StoreInPlanDtl
                                           SET ArticleNo = N'{7}' --<ArticleNo, nvarchar(50),>
                                              ,Quantity = {8} --<Quantity, decimal(15,2),>
                                              ,Weight = {9} --<Weight, decimal(15,2),>
                                              ,Place = N'{10}' --<Place, nvarchar(30),>
                                              ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                              ,UpdatedBy = N'{12}' --<UpdatedBy, varchar(45),>
                                         WHERE LineId = {17}
                                    END
                                            " + Environment.NewLine
                                                 , _session.CompanyID
                                                 , _session.PlantID
                                                 , model.StoreInPlanId
                                                 , model.PONum
                                                 , model.POLine
                                                 , model.SpecCode
                                                 , model.Thick
                                                 , model.ArticleNo
                                                 , model.Quantity
                                                 , model.Weight
                                                 , model.Place
                                                 , model.Note
                                                 , _session.UserID
                                                 , model.PONumber
                                                 , model.Width
                                                 , model.Length
                                                 , model.SeqId
                                                 , model.LineID
                                                 , 0
                                                 , model.SpecCode
                                                 , model.CommodityCode
                                                 , model.CoatingCode
                                                 , model.PackingNumber
                                                 , model.Category
                                                 , model.SaleContract);

            Repository.Instance.ExecuteWithTransaction(sql, "Store In Plan Detail");
        }

        public IEnumerable<StoreInPlanDetail> GetDetail(int storeInPlantId)
        {
            string sql = string.Format(@"SELECT  0 as SeqId, dtl.StoreInPlanId, max(dtl.LineId) as LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                        , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                        , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, max(dtl.ArticleNo) as ArticleNo
	                                        , sum(dtl.Quantity) as Quantity, sum(dtl.Weight) as Weight,max(dtl.Place) as Place, dtl.Note, GETDATE() as ReceiptDate
                                            , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
	                                        , 0 as TaxPaid, max(dtl.PackingNo) as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                            , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, pod.Number18 as Amount, pod.DocUnitCost as UnitPrice
                                            , max(dtl.Category) as Category, max(dtl.CoatingCode) as CoatingCode, pod.PartNum, max(dtl.StoreInFlag) as StoreInFlag
                                            , max(cmdt.Character01) as CommodityName, max(spec.Character01) as SpecName, max(coat.Character01) as CoatingName
                                        FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                        LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                        LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
										LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                        LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                        LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(pod.ShortChar01 = spec.Key2 and pod.ShortChar02 = spec.Key1)
	                                    LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
                                        WHERE dtl.StoreInPlanId = {0}
                                        GROUP BY dtl.StoreInPlanId,
                                        poh.PONum, poh.ShortChar10, pod.POLine, poh.ShortChar02
	                                        , pod.ShortChar01, pod.ShortChar02, pod.Number18, pod.DocUnitCost
	                                        , pod.Number01, pod.Number02, pod.Number03, eusr.Name, actusr.Name
	                                        , dtl.Note, pod.Character02, pod.Character03
                                            , pod.ShortChar04, busi.Character01, pod.PartNum
	                                        , pod.Number05,pod.XOrderQty ", storeInPlantId);

            return Repository.Instance.GetMany<StoreInPlanDetail>(sql);
        }

        public IEnumerable<StoreInPlanDetail> GetDetailArticle(int storeInPlantId, int POLine)
        {
            string sql = string.Format(@"SELECT dtl.SeqId, dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                            , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, dtl.ArticleNo
	                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance, sti.StockNo, sti.Location
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, dtl.CoatingCode, pod.PartNum, dtl.StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                            LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                            LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(pod.ShortChar01 = spec.Key2 and pod.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
                                            WHERE dtl.StoreInPlanId = {0} AND pod.POLine = {1} ORDER BY dtl.SeqId ASC", storeInPlantId, POLine);

            return Repository.Instance.GetMany<StoreInPlanDetail>(sql);
        }

        public IEnumerable<StoreInPlanDetail> GetDetailArticleITAKU(int storeInPlantId)
        {
            string sql = string.Format(@"SELECT dtl.SeqId, dtl.StoreInPlanId, dtl.LineId, ISNULL(poh.PONum, 0) as PONum, ISNULL(poh.ShortChar10,'N/A') as PONumber, ISNULL(pod.POLine, dtl.SeqId) as POLine, ISNULL(poh.ShortChar02, dtl.SContract) as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01,dtl.CommodityCode) as CommodityCode, ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                            , ISNULL(pod.Number01,dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, dtl.ArticleNo
	                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance, sti.StockNo, sti.Location
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, dtl.CoatingCode, '' as PartNum, dtl.StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                            LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                            LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01,dtl.CommodityCode) = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar01,dtl.CommodityCode) = spec.Key2 and ISNULL(pod.ShortChar02,dtl.SpecCode) = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(dtl.CoatingCode = coat.Key1)
                                            WHERE dtl.StoreInPlanId = {0} ORDER BY dtl.SeqId ASC", storeInPlantId);

            return Repository.Instance.GetMany<StoreInPlanDetail>(sql);
        }

        public void DeleteLine(int LineId)
        {
            string sql = "";
            sql += string.Format(@"DELETE FROM ucc_ic_StoreInPlanDtl WHERE LineId = {0}" + Environment.NewLine, LineId);

            Repository.Instance.ExecuteWithTransaction(sql, "Store In Plan Header");
        }

        public GetHeader GetHeaderByPONum(string PONum)
        {
            string sql = string.Format(@"select ven.VendorId as SupplierCode, ven.Name as SupplierName, maker.Key1 as MakerCode, maker.Character01 as MakerName
                                            , poh.ShortChar04 as MillCode, mill.Character01 as MillName, poh.CurrencyCode, poh.ExchangeRate
                                            , poh.ShortChar01 as CustID, cust.Name as CustomerName
                                            from POHeader poh
	                                            LEFT JOIN Vendor ven ON(poh.VendorNum = ven.VendorNum)
	                                            LEFT JOIN UD19 maker ON(poh.ShortChar03 = maker.Key1)
                                                LEFT JOIN Customer cust ON(poh.ShortChar01 = cust.CustID)
	                                            LEFT JOIN UD14 mill ON(poh.ShortChar03 = mill.Key1 and poh.ShortChar04 = mill.Key2)
                                          WHERE poh.ShortChar10 = N'{0}'  and poh.Approve = 1 and poh.ApprovalStatus = 'A' and poh.ShortChar02 != ''", PONum);

            return Repository.Instance.GetOne<GetHeader>(sql);
        }

        public int GetLastSeqId(int StoreInPlanId, int PONum, int POLine)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_ic_StoreInPlanDtl
                                            WHERE StoreInPlanId = {0} and PONum = {1} and POLine = {2}
                                            ORDER BY SeqId DESC", StoreInPlanId, PONum, POLine);

            var id = Repository.Instance.GetOne<int>(sql, "SeqId");

            return Convert.ToInt32(id) + 1;
        }

        public string[] GetTableFromExcel(string FileName)
        {
            DBConnection dbHelper = new DBConnection();
            string[] strTableList;

            if (dbHelper.FileConnecting(FileName) == true)
            {
                strTableList = dbHelper.FileGetTableList();
            }
            else
            {
                strTableList = null;
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();

            return strTableList;
        }

        public IEnumerable<ExternalFileModel> GetFileDetail(string FileName, string Sheet)
        {
            string sql = string.Format(@"SELECT 0 as LineID, 0 as SeqId, ex.* FROM [{0}$] ex", Sheet);
            //string sql = string.Format(@"SELECT ex.* FROM [{0}$] ex", Sheet);

            return Repository.Instance.GetManyForExcel<ExternalFileModel>(sql, FileName);
        }

        public decimal GetMCSSAllowance(string MCSSNo)
        {
            decimal num = new decimal(0);
            string sql = string.Format(@"SELECT * FROM ud15 WHERE Key1 = N'{0}'", MCSSNo);
            return Repository.Instance.GetOne<decimal>(sql, "Number06");
        }
    }
}