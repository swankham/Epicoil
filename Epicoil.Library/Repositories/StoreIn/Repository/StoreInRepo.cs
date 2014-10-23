using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreIn;
using Epicoil.Library.Repositories;

namespace Epicoil.Library.Repositories.StoreIn
{
    public class StoreInRepo : IStoreInRepo
    {
        private readonly IWharehouseRepo _repoWhs;

        public StoreInRepo()
        {
            this._repoWhs = new WharehouseRepo();
        }

        //private enum Month
        //{
        //    A,
        //    B,
        //    C,
        //    D,
        //    E,
        //    F,
        //    G,
        //    H,
        //    I,
        //    J,
        //    K,
        //    L
        //}

        public int GenerateId(string PlantID)
        {
            string sql = @" SELECT TOP 1 * FROM ucc_ic_StoreInHead ORDER BY StoreInId DESC";

            var id = Repository.Instance.GetOne<int>(sql, "StoreInId");

            return Convert.ToInt32(id) + 1;
        }

        public StoreInHead GetStoreInPlanByID(int Id)
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag
                                                , 0 as StoreInId, '' as StoreInNum, GETDATE() as StoreInDate , stph.TransactionType as Poscession, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup
                                                , stph.CustID, cust.Name as CustomerName, 0 as TransactionID
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                --LEFT JOIN ucc_ic_StoreInHead stin ON(stph.StoreInPlanId = stin.StoreInPlanId)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.StoreInPlanId = {0} AND stph.OpenStatus = 1", Id);

            return Repository.Instance.GetOne<StoreInHead>(sql);
        }

        public string NewPart(NewPartModel model, SessionInfo epiSession, int StoreInNo, out bool IsSucces, out string msgError)
        {
            int iRunning = RunningPart();
            string PartNum = GetSerialByFormat(iRunning);
            try
            {
                Session currSession = new Session(epiSession.UserID, epiSession.UserPassword, epiSession.AppServer, Session.LicenseType.Default);
                Part myPart = new Part(currSession.ConnectionPool);
                
                PartDataSet dsPart = new PartDataSet();
                myPart.GetNewPart(dsPart);

                DataRow drPart = dsPart.Tables[0].Rows[0];
                drPart.BeginEdit();
                drPart["PartNum"] = PartNum;
                drPart["PartDescription"] = PartNum;
                drPart["UOMClassID"] = "COUNT";
                drPart["IUM"] = "KG";
                drPart["PUM"] = "KG";
                drPart["TypeCode"] = "M";
                drPart["SalesUM"] = "KG";
                drPart["UserChar4"] = model.SupplierCode;
                drPart["UnitPrice"] = model.Amount;
                drPart["Character01"] = model.SaleContract;
                drPart["Character03"] = model.NGRemark;
                drPart["Character07"] = model.ArticleNo;
                drPart["Character07"] = model.CustID;
                drPart["Number10"] = model.Quantity;
                drPart["Date01"] = DateTime.Now;

                drPart["ShortChar01"] = string.IsNullOrEmpty(model.CommodityCode) ? "" : model.CommodityCode;
                drPart["ShortChar02"] = string.IsNullOrEmpty(model.SpecCode) ? "" : model.SpecCode;
                drPart["ShortChar04"] = model.BussinessType;

                drPart["ShortChar05"] = model.MakerCode;
                drPart["ShortChar06"] = model.MillCode;
                drPart["Character07"] = model.ArticleNo;
                drPart["ShortChar08"] = iRunning;
                drPart["ShortChar09"] = model.CoatingCode;

                drPart["Number01"] = model.Thick;
                drPart["Number02"] = model.Width;
                drPart["Number03"] = model.Length;
                drPart["Number11"] = 1;
                drPart["Number12"] = 1;
                if (model.NGStatus == 1)
                {
                    drPart["Character10"] = "N";
                }
                else
                {
                    drPart["Character10"] = "B";
                }

                drPart["NetWeight"] = model.Weight;
                drPart.EndEdit();
                myPart.Update(dsPart);

                currSession.Dispose();

                UpdateStock(PartNum, model.Quantity);
                IsSucces = true;
                msgError = "";
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
            }
            return PartNum;
        }

        public bool NewPartCollection(NewPartModel model, Session epiSession, out bool IsSucces, out string msgError)
        {
            try
            {
                Part myPart = new Part(epiSession.ConnectionPool);

                PartDataSet dsPart = new PartDataSet();
                myPart.GetNewPart(dsPart);

                DataRow drPart = dsPart.Tables[0].Rows[0];
                drPart.BeginEdit();
                drPart["PartNum"] = model.SerialNo;
                drPart["PartDescription"] = model.SerialNo; ;
                drPart["UOMClassID"] = "COUNT";
                drPart["IUM"] = "KG";
                drPart["PUM"] = "KG";
                drPart["TypeCode"] = "M";
                drPart["SalesUM"] = "KG";
                drPart["UserChar4"] = string.IsNullOrEmpty(model.SupplierCode) ? "" : model.SupplierCode;
                drPart["UnitPrice"] = model.Amount;
                drPart["Character01"] = string.IsNullOrEmpty(model.SaleContract) ? "" : model.SaleContract;
                drPart["Character03"] = string.IsNullOrEmpty(model.NGRemark) ? "-" : model.NGRemark;
                drPart["Character07"] = model.ArticleNo;
                drPart["Character07"] = model.CustID;
                drPart["Number10"] = model.Quantity;
                drPart["Date01"] = DateTime.Now;

                drPart["ShortChar01"] = string.IsNullOrEmpty(model.CommodityCode) ? "" : model.CommodityCode;
                drPart["ShortChar02"] = string.IsNullOrEmpty(model.SpecCode) ? "" : model.SpecCode;
                drPart["ShortChar04"] = string.IsNullOrEmpty(model.BussinessType) ? "" : model.BussinessType;

                drPart["ShortChar05"] = string.IsNullOrEmpty(model.MakerCode) ? "" : model.MakerCode;
                drPart["ShortChar06"] = string.IsNullOrEmpty(model.MillCode) ? "" : model.MillCode;
                drPart["Character07"] = string.IsNullOrEmpty(model.ArticleNo) ? "" : model.ArticleNo;
                drPart["Number18"] = model.iRunning;
                drPart["ShortChar09"] = string.IsNullOrEmpty(model.CoatingCode) ? "" : model.CoatingCode;

                drPart["Number01"] = model.Thick;
                drPart["Number02"] = model.Width;
                drPart["Number03"] = model.Length;
                drPart["Number11"] = 1;
                drPart["Number12"] = 1;
                if (model.NGStatus == 1)
                {
                    drPart["Character10"] = "N";
                }
                else
                {
                    drPart["Character10"] = "B";
                }

                drPart["NetWeight"] = model.Weight;
                drPart.EndEdit();
                myPart.Update(dsPart);

                IsSucces = true;
                msgError = "";
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
            }
            return IsSucces;
        }

        public string GetSerialByFormat(int StartId)
        {
            return (DateTime.Now.ToString("yy") + Enum.GetName(typeof(Month), int.Parse(DateTime.Now.ToString("MM"))) + StartId.ToString("00000"));
        }

        public int RunningPart()
        {
            decimal num;
            string sql = "SELECT TOP 1 * FROM Part ORDER BY Number18 DESC";

            num = Repository.Instance.GetOne<decimal>(sql, "Number18");

            return Convert.ToInt32(num) + 1;
        }

        public void UpdateStock(string PartNum, decimal qty)
        {
            string sql = string.Format(@"Update PartWhse set OnHandQty = {1}  where PartNum = N'{0}'", PartNum, qty);
            Repository.Instance.ExecuteWithTransaction(sql, "Update Stock");
        }

        public StoreInHead InsertStoreIn(StoreInHead model, SessionInfo epiSession)
        {
            int id = 0;
            id = GenerateId(epiSession.PlantID);
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_ic_StoreInHead (NOLOCK)
										    WHERE TransactionID = {13}
									    )
                                        BEGIN
                                            INSERT INTO ucc_ic_StoreInHead
                                                       (Company
                                                       ,Plant
                                                       ,TransactionType
                                                       ,StoreInId
                                                       ,StoreInNum
                                                       ,StoreInPlanId
                                                       ,StoreInDate
                                                       ,PORateType
                                                       ,PORate
                                                       ,DutyRate
                                                       ,Poscession
                                                       ,ActiveFlag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,PONum
                                                       ,PONumber)
                                                 VALUES
                                                       (N'{0}'     --<Company, nvarchar(8),>
                                                       ,N'{1}'     --<Plant, nvarchar(8),>
                                                       ,N'{2}'     --<TransactionType, nvarchar(15),>
                                                       ,{3}     --<StoreInId, int,>
                                                       ,N'{4}'     --<StoreInNum, nvarchar(10),>
                                                       ,{5}     --<StoreInPlanId, int,>
                                                       ,CONVERT(DATETIME, '{6}',103)     --<StoreInDate, datetime,>
                                                       ,N'{7}'     --<PORateType, nvarchar(50),>
                                                       ,{8}     --<PORate, decimal(18,2),>
                                                       ,{9}     --<DutyRate, decimal(18,2),>
                                                       ,{10}     --<Poscession, nvarchar(50),>
                                                       ,{11}     --<ActiveFlag, tinyint,>
                                                       ,GETDATE()     --<CreationDate, datetime,>
                                                       ,GETDATE()     --<LastUpdateDate, datetime,>
                                                       ,N'{12}'     --<CreatedBy, varchar(45),>
                                                       ,N'{12}'     --<UpdatedBy, varchar(45),>
                                                       ,N'{14}'     --<CreatedBy, varchar(45),>
                                                       ,N'{15}'     --<UpdatedBy, varchar(45),>
                                                )
                                            END" + Environment.NewLine
                                              , epiSession.CompanyID  //{0}
                                              , epiSession.PlantID
                                              , model.PossessionPromt.ToUpper()
                                              , id
                                              , "IN" + id.ToString("00000")
                                              , model.StoreInPlanId  //{5}
                                              , model.StoreInDate.ToString("dd/MM/yyyy hh:mm:ss")
                                              , ""  //Fix PORateType to string.Empty
                                              , model.ExchangeRate
                                              , model.ExchangeRate
                                              , model.Possession  //{10}
                                              , 1
                                              , epiSession.UserID
                                              , model.TransactionID
                                              , model.PONum
                                              , model.PONumber);
            Repository.Instance.ExecuteWithTransaction(sql, "Update Stock");

            return GetStoreInByID(id, epiSession.PlantID);
        }

        public bool InsertStoreInLine(List<StoreInDetail> model, SessionInfo epiSession, int StoreInid, decimal TransactionID, out string msg)
        {
            foreach (var item in model)
            {
                //int id = GenerateId();
                string sql = string.Format(@"INSERT INTO ucc_ic_StoreInDetail
                                                   (Company
                                                   ,Plant
                                                   ,TransactionID
                                                   ,StoreInId
                                                   ,StoreInPlanId
                                                   ,SeqId
                                                   ,POLine
                                                   ,ArticleNo
                                                   ,Quantity
                                                   ,Weight
                                                   ,Place
                                                   ,Note
                                                   ,Location
                                                   ,NGFlag
                                                   ,NGRemark
                                                   ,StockNo
                                                   ,CreationDate
                                                   ,LastUpdateDate
                                                   ,CreatedBy
                                                   ,UpdatedBy
                                                   ,StoreInPlanLineId)
                                             VALUES
                                                   (N'{0}' --<Company, nvarchar(15),>
                                                   ,N'{1}' --<Plant, nvarchar(15),>
                                                   ,{2} --<TransactionID, decimal(15,0),>
                                                   ,{3} --<StoreInId, int,>
                                                   ,{4} --<StoreInPlanId, int,>
                                                   ,{5} --<SeqId, int,>
                                                   ,{6} --<POLine, int,>
                                                   ,N'{7}' --<ArticleNo, nvarchar(50),>
                                                   ,{8} --<Quantity, decimal(15,2),>
                                                   ,{9} --<Weight, decimal(15,2),>
                                                   ,N'{10}' --<Place, nvarchar(30),>
                                                   ,N'{11}' --<Note, nvarchar(max),>
                                                   ,N'{12}' --<Location, nvarchar(max),>
                                                   ,{13} --<NGFlag, tinyint,>
                                                   ,N'{14}' --<NGRemark, nvarchar(max),>
                                                   ,N'{15}' --<StockNo, nvarchar(30),>
                                                   ,GETDATE() --<CreationDate, datetime,>
                                                   ,GETDATE() --<LastUpdateDate, datetime,>
                                                   ,N'{16}' --<CreatedBy, varchar(45),>
                                                   ,N'{16}' --<UpdatedBy, varchar(45),>
                                                   ,{17} --<StoreInPlanLineId, int,>
                                                   )" + Environment.NewLine
                                                      , epiSession.CompanyID
                                                      , epiSession.PlantID
                                                      , TransactionID
                                                      , item.SeqId
                                                      , StoreInid
                                                      , item.SeqId  //{5}
                                                      , item.POLine
                                                      , item.ArticleNo
                                                      , item.Quantity
                                                      , item.Weight
                                                      , item.Place  //{10}
                                                      , item.Note
                                                      , item.Location
                                                      , item.NGStatus
                                                      , item.NGRemark
                                                      , item.StockNo  //{15}
                                                      , epiSession.UserID
                                                      , item.LineID
                                                      );

                Repository.Instance.ExecuteWithTransaction(sql, "Insert StoreIn Line");
                UpdateStoreInPlanDetail(item.LineID);
                //UpdatePORelBeforeReceipt(item.PONum, item.POLine);
                this.UpdateStoreInFlag(item.StoreInPlanId);
            }
            msg = "";
            return true;
        }

        public void UpdateStoreInPlan(int StoreInPlanId)
        {
            string sql = string.Format(@"Update ucc_ic_StoreInPlanHead set StoreInFlag = 1  where StoreInPlanId = {0}", StoreInPlanId);
            Repository.Instance.ExecuteWithTransaction(sql, "Update Store In Plan");
        }

        public void UpdateStoreInPlanDetail(int LineId)
        {
            string sql = string.Format(@"Update ucc_ic_StoreInPlanDtl set StoreInFlag = 1  where LineId = {0}", LineId);
            Repository.Instance.ExecuteWithTransaction(sql, "Update Store In Plan Detail");
        }

        public IEnumerable<StoreInDetail> GetDetail(int storeInPlantId)
        {
            string sql = string.Format(@"SELECT dtl.StoreInPlanId, max(dtl.LineId) as LineId,ISNULL(poh.PONum, 0) as PONum, ISNULL(poh.ShortChar10,'N/A') as PONumber, ISNULL(pod.POLine,dtl.SeqId) as POLine, ISNULL(poh.ShortChar02, max(dtl.SContract)) as SaleContract
	                                        , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01,dtl.CommodityCode) as CommodityCode, ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                        , ISNULL(pod.Number01,dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, max(dtl.ArticleNo) as ArticleNo
	                                        , sum(dtl.Quantity) as Quantity, sum(dtl.Weight) as Weight,max(dtl.Place) as Place, dtl.Note, GETDATE() as ReceiptDate
	                                        , 0 as TaxPaid, max(dtl.PackingNo) as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                            , null as StoreInId, '' as StockNo , '' as Location, poh.VendorNum, '' as StoreInNum
                                            , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                            , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, max(dtl.SeqId) as SeqId
                                            , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, pod.PartNum
                                            , max(dtl.Category) as Category, max(dtl.CoatingCode) as CoatingCode, max(dtl.StoreInFlag) as StoreInFlag
                                            , max(cmdt.Character01) as CommodityName, max(spec.Character01) as SpecName, max(coat.Character01) as CoatingName
                                            , 0 as TransactionID, 0 as TransactionLineID
                                            , 0 as NGFlag, '' as NGRemark
                                        FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                        LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                        LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                        --LEFT JOIN ucc_ic_StoreInHead stin ON(dtl.StoreInPlanId = stin.StoreInPlanId)
										LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                        LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                        LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01,dtl.CommodityCode) = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar02,dtl.SpecCode) = spec.Key2 and pod.Character01 = spec.Key1)
	                                    LEFT JOIN UD31 coat ON(dtl.CoatingCode = coat.Key1)
                                        WHERE dtl.StoreInPlanId = {0} and dtl.StoreInFlag = 0
                                        GROUP BY dtl.StoreInPlanId--, stin.StoreInId, stin.StoreInNum
                                            , poh.PONum, poh.ShortChar10, pod.POLine, poh.ShortChar02
	                                        , pod.ShortChar01, pod.ShortChar02, eusr.Name, actusr.Name
	                                        , pod.Number01, pod.Number02, pod.Number03, poh.VendorNum
                                            , pod.Number18, pod.DocUnitCost, pod.PartNum
	                                        , dtl.Note, pod.Character02, pod.Character03, pod.ShortChar04, busi.Character01
                                            , dtl.CommodityCode, dtl.SpecCode, dtl.Thick, dtl.Width, dtl.Length, ISNULL(pod.POLine,dtl.SeqId)
	                                        , pod.Number05,pod.XOrderQty", storeInPlantId);

            return Repository.Instance.GetMany<StoreInDetail>(sql);
        }

        public IEnumerable<StoreInDetail> GetDetailByStoreIn(int storeInId)
        {
            string sql = string.Format(@"SELECT dtl.StoreInPlanId, max(dtl.LineId) as LineId,ISNULL(poh.PONum, 0) as PONum, ISNULL(poh.ShortChar10,'N/A') as PONumber, ISNULL(pod.POLine,dtl.SeqId) as POLine, ISNULL(poh.ShortChar02, max(dtl.SContract)) as SaleContract
	                                        , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01,dtl.CommodityCode) as CommodityCode, ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                        , ISNULL(pod.Number01,dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, max(dtl.ArticleNo) as ArticleNo
	                                        , sum(dtl.Quantity) as Quantity, sum(dtl.Weight) as Weight,max(dtl.Place) as Place, dtl.Note, GETDATE() as ReceiptDate
	                                        , 0 as TaxPaid, max(dtl.PackingNo) as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                            , null as StoreInId, '' as StockNo , '' as Location, poh.VendorNum, '' as StoreInNum
                                            , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                            , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, max(dtl.SeqId) as SeqId
                                            , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, pod.PartNum
                                            , max(dtl.Category) as Category, max(dtl.CoatingCode) as CoatingCode, max(dtl.StoreInFlag) as StoreInFlag
                                            , max(cmdt.Character01) as CommodityName, max(spec.Character01) as SpecName, max(coat.Character01) as CoatingName
                                            , sth.TransactionID, 0 as TransactionLineID
                                            , 0 as NGFlag, '' as NGRemark
                                        FROM ucc_ic_StoreInHead sth
										LEFT JOIN ucc_ic_StoreInDetail std ON(sth.TransactionID = std.TransactionID)
										LEFT JOIN ucc_ic_StoreInPlanDtl dtl ON(std.StoreInPlanLineId = dtl.LineId)
                                        LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                        LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
										LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                        LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                        LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01,dtl.CommodityCode) = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar02,dtl.SpecCode) = spec.Key2 and pod.Character01 = spec.Key1)
	                                    LEFT JOIN UD31 coat ON(dtl.CoatingCode = coat.Key1)
                                        WHERE sth.StoreInId = {0}
                                        GROUP BY dtl.StoreInPlanId, sth.TransactionID
                                            , poh.PONum, poh.ShortChar10, pod.POLine, poh.ShortChar02
	                                        , pod.ShortChar01, pod.ShortChar02, eusr.Name, actusr.Name
	                                        , pod.Number01, pod.Number02, pod.Number03, poh.VendorNum
                                            , pod.Number18, pod.DocUnitCost, pod.PartNum
	                                        , dtl.Note, pod.Character02, pod.Character03, pod.ShortChar04, busi.Character01
                                            , dtl.CommodityCode, dtl.SpecCode, dtl.Thick, dtl.Width, dtl.Length, ISNULL(pod.POLine,dtl.SeqId)
	                                        , pod.Number05,pod.XOrderQty", storeInId);

            return Repository.Instance.GetMany<StoreInDetail>(sql);
        }

        public IEnumerable<StoreInDetail> GetDetailArticle(int storeInPlantId, string PONum, int POLine)
        {
            IEnumerable<StoreInDetail> result;
            string sql = string.Format(@"SELECT dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                            , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, dtl.ArticleNo
	                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
	                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                                , pod.Character02 as Enduser, pod.Character03 as ActualEndUser, pod.PartNum
                                                , sti.StoreInId, sti.StockNo, sti.location as Location, poh.VendorNum, '' as StoreInNum
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, dtl.SeqId
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, dtl.CoatingCode, dtl.StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                                , sti.TransactionID, sti.TransactionLineID
                                                , sti.NGFlag, sti.NGRemark
                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                            LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                            LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(pod.ShortChar01 = spec.Key2 and pod.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(dtl.CoatingCode = coat.Key1)
                                            WHERE dtl.StoreInPlanId = {0} AND pod.POLine = {1}  and dtl.StoreInFlag = 0", storeInPlantId, POLine);

            result = Repository.Instance.GetMany<StoreInDetail>(sql);
            return result;
        }

        public IEnumerable<StoreInDetail> GetDetailArticleITAKU(int LineID)
        {
            IEnumerable<StoreInDetail> result;
            string sql = string.Format(@"SELECT dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01,dtl.CommodityCode) as CommodityCode, ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                            , ISNULL(pod.Number01,dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, dtl.ArticleNo
	                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
	                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                                , pod.Character02 as Enduser, pod.Character03 as ActualEndUser, '' as PartNum
                                                , sti.StoreInId, sti.StockNo, sti.location as Location, poh.VendorNum, '' as StoreInNum
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, dtl.SeqId
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, dtl.CoatingCode, dtl.StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                                , sti.TransactionID, sti.TransactionLineID
                                                , sti.NGFlag, sti.NGRemark
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
                                            WHERE dtl.LineId = {0} and dtl.StoreInFlag = 0", LineID);

            result = Repository.Instance.GetMany<StoreInDetail>(sql);

            return result;
        }

        /// <summary>
        /// Get line detail for...
        /// - UpdateStoreInPlanDetail() in External
        /// - GetNewPart() in Epicor.
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns>NewPartModel => StoreInDetail => StoreInPlanDetail</returns>
        public StoreInDetail GetArticleListToSave(int LineId)
        {
            string sql = string.Format(@"SELECT dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                            , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, dtl.ArticleNo
	                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
	                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                                , pod.Character02 as Enduser, pod.Character03 as ActualEndUser, pod.PartNum
                                                , sti.StoreInId, sti.StockNo, sti.location as Location, poh.VendorNum, '' as StoreInNum
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName, dtl.SeqId
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, dtl.CoatingCode, dtl.StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                                , sti.TransactionID, sti.TransactionLineID
                                                , sti.NGFlag, sti.NGRemark
                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                            LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                            LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(pod.ShortChar01 = spec.Key2 and pod.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(dtl.CoatingCode = coat.Key1)
                                            WHERE dtl.LineId = {0}", LineId);
            return Repository.Instance.GetOne<StoreInDetail>(sql);
        }

        /// <summary>
        /// ******************Important!*****************
        /// </summary>
        /// <param name="model"></param>
        /// <param name="epiSession"></param>
        /// <param name="IsSucces"></param>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public bool GetNewRcv(RecieptHeadModel head, List<ReceiptDetailModel> model, Epicor.Mfg.Core.Session epiSession, out bool IsSucces, out string msgError)
        {
            msgError = "";
            ReceiptDataSet dsReceipt = new ReceiptDataSet();
            try
            {
                Receipt myReceipt = new Receipt(epiSession.ConnectionPool);
                myReceipt.ValidateMRPONum(head.PONum, head.VendorNum, out msgError);

                #region PO no error.

                if (string.IsNullOrEmpty(msgError))
                {
                    /*This OK*/
                    myReceipt.GetNewRcvHead(dsReceipt, head.VendorNum, "");

                    /*Fix for testing
                    myReceipt.GetNewRcvHead(dsReceipt, 5, "");
                    */

                    DataRow drReceipt = dsReceipt.Tables[0].Rows[0];
                    drReceipt.BeginEdit();
                    string PackSlip = head.StoreInNum + "-RCV";

                    /* This OK */
                    drReceipt["VendorNum"] = head.VendorNum;
                    drReceipt["PackSlip"] = PackSlip;
                    drReceipt["ReceiptDate"] = DateTime.Now;
                    drReceipt["ReceivePerson"] = epiSession.UserID;
                    drReceipt["ShipViaCode"] = "UC02";
                    drReceipt["PONum"] = head.PONum;

                    /*Fix for testing
                    drReceipt["VendorNum"] = 5;
                    drReceipt["PackSlip"] = "IN00002-5";
                    drReceipt["ReceiptDate"] = DateTime.Now;
                    drReceipt["ReceivePerson"] = "manager";
                    drReceipt["ShipViaCode"] = "UC02";
                    drReceipt["PONum"] = 272;
                    */

                    drReceipt.EndEdit();
                    myReceipt.Update(dsReceipt);

                    //myReceipt.CreateMassReceipts(model.VendorNum, "", model.StoreInNum, 0, model.PONum.ToString(), dsReceipt);
                    myReceipt.CreateMassReceipts(head.VendorNum, "", PackSlip, 0, head.PONum.ToString(), dsReceipt);

                    myReceipt.ReceiveAll(dsReceipt);
                    int i = 0;
                    foreach (DataRow dr in dsReceipt.Tables["RcvDtl"].Rows)
                    {
                        //myReceipt.GetNewRcvDtl(dsReceipt, model.VendorNum, "", model.StoreInNum);
                        //DataRow drDtl = dsReceipt.Tables["RcvDtl"].Rows[i];
                        //myReceipt.GetDtlQtyInfo(dsReceipt, model.VendorNum, "", model.StoreInNum, dr["PackLine"].GetInt(), 1, "KG", "", out msgError);
                        ReceiptDetailModel result = model.Where<ReceiptDetailModel>(p => p.PONum.ToString().Equals(dr["PONum"].ToString())
                                                                && p.POLine.ToString().Equals(dr["POLine"].ToString())).Single<ReceiptDetailModel>();
                        dr.BeginEdit();
                        dr["OurQty"] = result.Weight;
                        dr["OurUnitCost"] = 1;
                        //dr["VendorQty"] = result.Weight;
                        dr["ReceivedComplete"] = false;
                        dr["DocUnitCost"] = 20;  //ถ้าเป็น PO Sample จะเป็น 0 (Else result.UnitPrice)
                        dr["POTransValue"] = 20;
                        dr["ExtTransValue"] = 20;
                        dr["InputOurQty"] = result.Weight;
                        dr["Number01"] = result.Weight;   //Weight
                        dr["Received"] = true;
                        dr.EndEdit();

                        i++;
                    }
                    myReceipt.Update(dsReceipt);
                    myReceipt.ReceiveAllLines(true, dsReceipt);

                    //myReceipt.CommitRcvDtl(model.VendorNum, "", model.StoreInNum, dsReceipt);
                    IsSucces = true;
                    msgError = "";
                    return IsSucces;
                }

                #endregion PO no error.

                IsSucces = false;
                return IsSucces;
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
                return IsSucces;
            }
        }

        public void GetNewRcvDtl(Epicor.Mfg.Core.Session epiSession, ReceiptDataSet dsReceipt)
        {
            try
            {
                Receipt myReceipt = new Receipt(epiSession.ConnectionPool);

                //ReceiptDataSet dsReceipt = new ReceiptDataSet();
                //myReceipt.GetNewRcvDtl(dsReceipt, model.VendorNum, "", model.StoreInNum);
                myReceipt.GetNewRcvDtl(dsReceipt, 5, "", "IN00002-3");

                DataRow drReceipt = dsReceipt.Tables[0].Rows[0];
                drReceipt.BeginEdit();
                drReceipt["VendorNum"] = 5; // model.VendorNum;
                drReceipt["PackSlip"] = "IN00002-3";//model.StoreInNum + "-2";
                //drReceipt["PackLine"] = 1;
                //drReceipt["PartNum"] = DateTime.Now;
                drReceipt["WareHouseCode"] = "600";
                drReceipt["OurQty"] = 10; // model.Quantity;
                drReceipt["PONum"] = 272; // model.PONum;
                drReceipt["POLine"] = 1; // model.POLine;
                drReceipt["PORelNum"] = 1; // model.POLine;
                drReceipt.EndEdit();

                myReceipt.Update(dsReceipt);

                //IsSucces = true;
                //msgError = "";
            }
            catch (Exception ex)
            {
                //IsSucces = false;
                //msgError = ex.Message;
            }
        }

        public void GetRowsToNewRcvDetail(int storeInPlantId, SessionInfo epiSession, ReceiptDataSet dsReceipt)
        {
            IEnumerable<StoreInDetail> result;
            bool IsSucces;
            string msgError;

            string sql = string.Format(@"SELECT dtl.StoreInPlanId, max(dtl.LineId) as LineId,poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                        , 0 as WeightBalnce, 0 as RemainingWeight, pod.ShortChar01 as CommodityCode, pod.ShortChar02 as SpecCode
	                                        , pod.Number01 as Thick, pod.Number02 as Width, pod.Number03 as Length, max(dtl.ArticleNo) as ArticleNo
	                                        , sum(dtl.Quantity) as Quantity, sum(dtl.Weight) as Weight,max(dtl.Place) as Place, dtl.Note, GETDATE() as ReceiptDate
	                                        , 0 as TaxPaid, dtl.Charactor02 as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                            , stin.StoreInId, '' as StockNo , '' as Location, poh.VendorNum
                                            , pod.Character02 as Enduser, pod.Character03 as ActualEndUser, stin.StoreInNum
                                        FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                        LEFT JOIN POHeader poh ON(dtl.PONum = poh.ShortChar10)
                                        INNER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                        LEFT JOIN ucc_ic_StoreInHead stin ON(dtl.StoreInPlanId = stin.StoreInPlanId)
                                        WHERE dtl.StoreInPlanId = {0}
                                        GROUP BY dtl.StoreInPlanId, stin.StoreInId, stin.StoreInNum
                                            , poh.PONum, poh.ShortChar10, pod.POLine, poh.ShortChar02
	                                        , pod.ShortChar01, pod.Character01
	                                        , pod.Number01, pod.Number02, pod.Number03, poh.VendorNum
	                                        , dtl.Note, pod.Character02, pod.Character03
	                                        , dtl.Charactor02, pod.Number05,pod.XOrderQty", storeInPlantId);

            result = Repository.Instance.GetMany<StoreInDetail>(sql);
            //foreach (var t in result)
            //{
            //    this.GetNewRcvDtl(t, epiSession, dsReceipt, out IsSucces, out msgError);
            //}
        }

        public StoreInHead GetStoreInByID(int storeInID, string Plant)
        {
            string sql = string.Format(@"SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag
                                                , stin.StoreInId, stin.StoreInNum, stin.StoreInDate , stin.Poscession, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup
                                                , stph.CustID, cust.Name as CustomerName, stin.TransactionID
                                            FROM ucc_ic_StoreInHead stin
		                                        LEFT JOIN ucc_ic_StoreInPlanHead stph ON(stin.StoreInPlanId = stph.StoreInPlanId and stin.Plant = stph.Plant)
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                            WHERE stin.StoreInId = {0} AND stin.Plant = N'{1}' AND stph.OpenStatus = 1", storeInID, Plant);

            return Repository.Instance.GetOne<StoreInHead>(sql);
        }

        public IEnumerable<NewPartModel> GetNewPartCollection(decimal TransactionID)
        {
            string sql = string.Format(@"SELECT sth.StoreInPlanId, sti.storeinplanlineid as LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                            , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01, stpl.CommodityCode) as CommodityCode, ISNULL(pod.ShortChar02,stpl.SpecCode) as SpecCode
	                                            , ISNULL(pod.Number01, stpl.Thick) as Thick, ISNULL(pod.Number02, stpl.Width) as Width, ISNULL(pod.Number03, stpl.Length) as Length, sti.ArticleNo
	                                            , sti.Quantity as Quantity, sti.Weight, sti.Place, sti.Note, GETDATE() as ReceiptDate
	                                            , 0 as TaxPaid, ISNULL(sti.Charactor01, stpl.PackingNo) as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance
                                                , pod.Character02 as Enduser, pod.Character03 as ActualEndUser, pod.PartNum
                                                , sti.StoreInId, sti.StockNo, sti.location as Location, poh.VendorNum, '' as StoreInNum
                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                , ISNULL(pod.ShortChar04, stph.BType) as BussinessType, busi.Character01 as BussinessTypeName, sti.SeqId
                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, '' as Category, poh.ShortChar02 as SContract
												, ISNULL(pod.ShortChar03, stpl.CoatingCode) as CoatingCode, 1 as StoreInFlag
                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
                                                , sti.TransactionID, sti.TransactionLineID, ISNULL(poh.ShortChar01, stph.CustID) as CustID, cust.Name as CustomerName
												, ISNULL(ven.VendorId, stph.SupplierCode) as SupplierCode, ISNULL(ven.Name, ven1.Name) as SupplierName
												, ISNULL(maker.Key1, stph.MakerCode) as MakerCode, maker.Character01 as MakerName, ISNULL(poh.ShortChar04, stph.MillCode) as MillCode, mill.Character01 as MillName
                                            FROM ucc_ic_StoreInDetail sti
											LEFT JOIN ucc_ic_StoreInHead sth ON(sti.TransactionID = sth.TransactionID)

											LEFT JOIN ucc_ic_StoreInPlanDtl stpl ON(sth.StoreInPlanId = stpl.StoreInPlanId and stpl.LineId = sti.storeinplanlineid)
											LEFT JOIN ucc_ic_StoreInPlanHead stph ON(sth.StoreInPlanId = stph.StoreInPlanId)

                                            LEFT OUTER JOIN POHeader poh ON(sth.PONum = poh.PONum)
                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and sti.POLine = pod.POLine)
											LEFT JOIN Customer cust ON(ISNULL(poh.ShortChar01, stph.CustID)= cust.CustID)
											LEFT JOIN Vendor ven ON(poh.VendorNum = ven.VendorNum)
											LEFT JOIN Vendor ven1 ON(stph.SupplierCode = ven1.VendorId)
										    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                            LEFT JOIN UD25 busi ON(ISNULL(pod.ShortChar04, stph.BType) = busi.Key1)
                                            LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01, stpl.CommodityCode) = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar01, stpl.CommodityCode)= spec.Key2 and ISNULL(pod.ShortChar02,stpl.SpecCode) = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(ISNULL(pod.ShortChar03, stpl.CoatingCode) = coat.Key1)
											LEFT JOIN UD19 maker ON(ISNULL(poh.ShortChar03, stph.MakerCode) = maker.Key1)
											LEFT JOIN UD14 mill ON(ISNULL(poh.ShortChar03, stph.MakerCode) = mill.Key1 and  ISNULL(poh.ShortChar04, stph.MillCode) = mill.Key2)
                                            WHERE sti.TransactionID = {0}", TransactionID);

            return Repository.Instance.GetMany<NewPartModel>(sql);
        }

        public void UpdateArticleToStoreIn(decimal TransactionLineID, string ArticleNo)
        {
            string sql = string.Format(@"UPDATE ucc_ic_StoreInDetail SET StockNo = N'{1}' WHERE TransactionLineID = {0}", TransactionLineID, ArticleNo);
            Repository.Instance.ExecuteWithTransaction(sql, "Update Article No.");
        }

        public RecieptHeadModel GetDataToNewReceiptPO(decimal TransactionID)
        {
            string sql = string.Format(@"SELECT sti.TransactionID, sth.StoreInId, sth.StoreInNum, sth.StoreInDate
	                                            , poh.PONum, poh.ShortChar10 as PONumber, ven.VendorNum
	                                            , ven.VendorId as SupplierCode, ven.Name as SupplierName
                                            FROM ucc_ic_StoreInDetail sti
	                                            LEFT JOIN ucc_ic_StoreInHead sth ON(sti.TransactionID = sth.TransactionID)
	                                            LEFT OUTER JOIN POHeader poh ON(sth.PONum = poh.PONum)
	                                            LEFT JOIN Vendor ven ON(poh.VendorNum = ven.VendorNum)
                                            WHERE sti.TransactionID = {0}
                                            GROUP BY sti.TransactionID, sth.StoreInId, sth.StoreInNum, sth.StoreInDate
	                                            , poh.PONum, poh.ShortChar10
	                                            , ven.VendorId, ven.Name, ven.VendorNum", TransactionID);

            return Repository.Instance.GetOne<RecieptHeadModel>(sql);
        }

        public void UpdatePORelBeforeReceipt(int PONum, int POLine)
        {
            string sql = string.Format(@"SELECT OrderQty FROM PODetail WHERE PONum = {0} AND POLine = {1}", PONum, POLine);
            decimal Quantity = Repository.Instance.GetOne<decimal>(sql, "OrderQty");

            sql = string.Format(@"UPDATE PORel
                                        SET XRelQty = {2}
	                                      , RelQty = {2}
	                                      , ReceivedQty = {2}
	                                      , BaseQty = {2}
                                        WHERE PONum = {0} AND POLine = {1}", PONum, POLine, Quantity);

            Repository.Instance.ExecuteWithTransaction(sql, "Update PORel");
        }

        public IEnumerable<ReceiptDetailModel> GetPODetailToReceipt(decimal TransactionID)
        {
            string sql = string.Format(@"SELECT sth.PONum,sti.POLine, sti.TransactionID, sum(sti.Quantity) as Quantity, sum(sti.Weight) as Weight
                                            FROM ucc_ic_StoreInDetail sti
                                            LEFT JOIN ucc_ic_StoreInHead sth ON(sti.TransactionID = sth.TransactionID)
                                            WHERE sti.TransactionID = {0}
                                            GROUP BY sth.PONum,sti.POLine, sti.TransactionID", TransactionID);

            return Repository.Instance.GetMany<ReceiptDetailModel>(sql);
        }

        public IEnumerable<StoreInHeadBalance> GetStoreInBalanceAll(string Plant, int StoreInPlanId)
        {
            string sql = string.Format(@"--Section First get Store In Plan list was Store In.
                                        SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag
                                                , GETDATE() as StoreInDate , stph.TransactionType as Poscession, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup
                                                , stph.CustID, cust.Name as CustomerName, stin.TransactionID
												, stin.StoreInId, stin.StoreInNum, stph.storeinflag StatusFlag
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                INNER JOIN ucc_ic_StoreInHead stin ON(stph.StoreInPlanId = stin.StoreInPlanId)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.Plant = '{0}' AND stph.StoreInPlanId = {1} AND stph.OpenStatus = 1
                                        UNION ALL
                                        --Section second get Store In Plan list are not Store In.
                                        SELECT stph.Plant, stph.StoreInPlanId, stph.StoreInPlanNum, stph.TransactionType
		                                        , stph.BType as BussinessType, busi.Character01 as BussinessTypeName
		                                        , stph.SupplierCode, ven.Name as SupplierName, ISNULL(stph.CurrencyCode,'THB') as CurrencyCode
		                                        , stph.MakerCode, maker.Character01 as MakerName
		                                        , stph.MillCode, mill.Character01 as MillName
		                                        , stph.IMexItemNo, stph.InvoiceNum, stph.InvoiceDate, stph.PORate
		                                        , stph.DutyRate, stph.TisiFlag, stph.LoadPort, stph.ArivePort, stph.ETDDate, stph.ETADate
		                                        , stph.Vessel, stph.IMEXChecked, stph.StoreInFlag
                                                , GETDATE() as StoreInDate , stph.TransactionType as Poscession, stph.ImexConfirm, stph.ImexRemark
                                                , stph.LastUpdateDate, stph.UpdatedBy, stph.UserGroup
                                                , stph.CustID, cust.Name as CustomerName, 0 as TransactionID
												, 0 as StoreInId, '-' as StoreInNum, stph.storeinflag StatusFlag
                                          FROM ucc_ic_StoreInPlanHead stph
	                                            LEFT JOIN Vendor ven ON(stph.SupplierCode = ven.VendorID)
	                                            LEFT JOIN UD19 maker ON(stph.MakerCode = maker.Key1)
	                                            LEFT JOIN UD14 mill ON(stph.MakerCode = mill.Key1 and stph.MillCode = mill.Key2)
	                                            LEFT JOIN UD25 busi ON(stph.BType = busi.Key1)
                                                LEFT JOIN Customer cust ON(stph.CustID = cust.CustID)
                                          WHERE stph.Plant = '{0}' AND stph.StoreInPlanId = {1} AND stph.StoreInFlag = 0 AND stph.OpenStatus = 1", Plant, StoreInPlanId);

            return Repository.Instance.GetMany<StoreInHeadBalance>(sql);
        }

        public IEnumerable<StoreInDetail> GetDetailArticle(int storeInPlantId, decimal TransactionID)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT dtl.SeqId, dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                                            , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01, dtl.CommodityCode) as CommodityCode,  ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                                            , ISNULL(pod.Number01, dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, dtl.ArticleNo
	                                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
                                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName
	                                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance, sti.StockNo, sti.Location
                                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, ISNULL(pod.ShortChar03, dtl.CoatingCode) as CoatingCode, pod.PartNum, dtl.StoreInFlag
                                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
												                , sti.TransactionID, sti.TransactionLineID, sti.StoreInId, 0 as VendorNum, '' as StoreInNum
                                                                , sti.NGFlag, sti.NGRemark
                                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                                            LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										                    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										                    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                                            LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01, dtl.CommodityCode) = cmdt.Key1)
	                                                        LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar01, dtl.CommodityCode) = spec.Key2 and ISNULL(pod.ShortChar02,dtl.SpecCode) = spec.Key1)
	                                                        LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
                                                            WHERE dtl.StoreInPlanId = {0}
                                                        UNION ALL
                                                        SELECT dtl.SeqId, dtl.StoreInPlanId, dtl.LineId, poh.PONum, poh.ShortChar10 as PONumber, pod.POLine, poh.ShortChar02 as SaleContract
	                                                            , 0 as WeightBalnce, 0 as RemainingWeight, ISNULL(pod.ShortChar01, dtl.CommodityCode) as CommodityCode,  ISNULL(pod.ShortChar02,dtl.SpecCode) as SpecCode
	                                                            , ISNULL(pod.Number01, dtl.Thick) as Thick, ISNULL(pod.Number02, dtl.Width) as Width, ISNULL(pod.Number03, dtl.Length) as Length, dtl.ArticleNo
	                                                            , dtl.Quantity as Quantity, dtl.Weight, dtl.Place, dtl.Note, GETDATE() as ReceiptDate
                                                                , pod.Character02, pod.Character03, eusr.Name as EndUserName, actusr.Name as ActlEndUserName
                                                                , pod.ShortChar04 as BussinessType, busi.Character01 as BussinessTypeName
	                                                            , 0 as TaxPaid, dtl.PackingNo as PackingNumber, pod.Number05 as DutyRate,pod.XOrderQty as OpenBalance, '' as StockNo, dtl.Place as Location
                                                                , pod.Number18 as Amount, pod.DocUnitCost as UnitPrice, dtl.Category, dtl.SContract, ISNULL(pod.ShortChar03, dtl.CoatingCode) as CoatingCode, pod.PartNum, 0 as StoreInFlag
                                                                , cmdt.Character01 as CommodityName, spec.Character01 as SpecName, coat.Character01 as CoatingName
												                , 0 as TransactionID, 0 as TransactionLineID, 0 as StoreInId, 0 as VendorNum, '' as StoreInNum
                                                                , 0 as NGFlag, '' as NGRemark
                                                            FROM dbo.ucc_ic_StoreInPlanDtl dtl
                                                            LEFT OUTER JOIN POHeader poh ON(dtl.PONum = poh.PONum)
                                                            LEFT OUTER JOIN PODetail pod ON(poh.PONum = pod.PONum and dtl.POLine = pod.POLine)
                                                            --LEFT JOIN ucc_ic_StoreInDetail sti ON(dtl.ArticleNo = sti.ArticleNo)
										                    LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
										                    LEFT JOIN Customer actusr ON(pod.Character03 = actusr.CustID)
                                                            LEFT JOIN UD25 busi ON(pod.ShortChar04 = busi.Key1)
                                                            LEFT JOIN UD29 cmdt ON(ISNULL(pod.ShortChar01, dtl.CommodityCode) = cmdt.Key1)
	                                                        LEFT JOIN UD30 spec ON(ISNULL(pod.ShortChar01, dtl.CommodityCode) = spec.Key2 and ISNULL(pod.ShortChar02,dtl.SpecCode) = spec.Key1)
	                                                        LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
                                                            WHERE dtl.StoreInPlanId = {0}  AND dtl.StoreInFlag = 0) as T WHERE T.TransactionID = {1} ", storeInPlantId, TransactionID);

            return Repository.Instance.GetMany<StoreInDetail>(sql);
        }

        public void UpdateStoreInFlag(int storeInPlanId)
        {
            string sql = string.Format(@"SELECT COUNT(*) as CountRows
	                                        FROM ucc_ic_storeinplandtl
	                                        WHERE StoreInFlag = 0 AND StoreInPlanId = {0}" + Environment.NewLine, storeInPlanId);

            int rows = Repository.Instance.GetOne<int>(sql, "CountRows");
            string subSql = "";
            if (rows == 0)
            {
                subSql = string.Format(@"UPDATE ucc_ic_StoreInPlanHead SET StoreInFlag = 1, OpenStatus = 0 WHERE StoreInPlanId = {0}" + Environment.NewLine, storeInPlanId);
            }
            else
            {
                subSql = string.Format(@"UPDATE ucc_ic_StoreInPlanHead SET StoreInFlag = 0 WHERE StoreInPlanId = {0}" + Environment.NewLine, storeInPlanId);
            }
           
            Repository.Instance.ExecuteWithTransaction(subSql, "Update acknowledgement");
        }

        public bool UpdatePOReleaseQty(Session epiSession, string poNum, out string msgError)
        {
            msgError = "";
            bool result = false;

            if (epiSession.IsValidSession(epiSession.SessionID, epiSession.UserID))
            {
                try
                {
                    PO myPO = new PO(epiSession.ConnectionPool);

                    bool morePages = false;
                    PODataSet dsPO = new PODataSet();
                    dsPO = myPO.GetRows("PONum = " + poNum, "", "", "", "", "", "", "", "", 0, 1, out morePages);

                    DataRow drPO = dsPO.Tables["POHeader"].Select().Single();
                    string cal = drPO["ShortChar06"].ToString();

                    DataTable POLine = dsPO.Tables["PODetail"];
                    int i = 0;

                    foreach (DataRow list in dsPO.Tables["PORel"].Rows)
                    {
                        var item = POLine.Rows[i].ItemArray.ToArray();
                        decimal qty = 0;

                        if (cal == "1" || cal == "3" || cal == "4") { qty = Convert.ToDecimal(item[67].ToString()); }       //67=Number11, 
                        else if (cal == "2") { qty = Convert.ToDecimal(item[76].ToString()); }      //76=Number20

                        list.BeginEdit();
                        list["XRelQty"] = qty;
                        list["RelQty"] = qty;
                        list["BaseQty"] = qty;
                        list.EndEdit();
                        i++;
                    }

                    myPO.Update(dsPO);
                    result = true;
                    epiSession.Dispose();
                }
                catch (Exception ex)
                {
                    msgError = "Error : " + ex;                
                }
            }
            return result;
        }
    }
}