﻿using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Planning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories.Planning
{
    public class WorkEntryRepo : IWorkEntryRepo
    {
        private readonly IUserCodeRepo _repoUcode;
        private readonly IResourceRepo _repoResrc;

        public WorkEntryRepo()
        {
            this._repoUcode = new UserCodeRepo();
            this._repoResrc = new ResourceRepo();
        }

        public IEnumerable<MaterialModel> GetAllMaterial(string plant)
        {
            string sql = string.Format(@"SELECT pl.PartNum
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , 0 as UsingWeight, oh.Quantity * pl.Number10 as RemainWeight, (pl.Number03 / 100) as LengthM
	                                        , oh.Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName
                                        FROM PartLot pl
	                                        INNER JOIN Part p ON(pl.PartNum = p.PartNum)
                                            INNER JOIN PartPlant pln ON(p.PartNum = pln.PartNum)
	                                        LEFT JOIN UD29 cmdt ON(p.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(p.ShortChar01 = spec.Key2 and p.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(p.ShortChar09 = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(pl.Character02 = busi.Key1)
	                                        INNER JOIN (SELECT PartNum, LotNum, sum(OnhandQty) as Quantity, DimCode FROM PartBin
				                                        GROUP BY PartNum, LotNum, DimCode) oh
				                                        ON(p.PartNum = oh.PartNum and pl.LotNum = oh.LotNum)
	                                        LEFT JOIN Vendor ven ON(pl.ShortChar03 = ven.VendorID)
	                                        LEFT JOIN UD19 maker ON(pl.ShortChar01 = maker.Key1)
	                                        LEFT JOIN UD14 mill ON(pl.ShortChar01 = mill.Key2 and pl.ShortChar02 = mill.Key1)
	                                        LEFT JOIN Customer cust ON(p.Character08 = cust.CustID)
                                        WHERE pln.Plant = N'{0}' AND pl.Number05 = 1 AND pl.Number08 IN (0, 1)", plant);

            return Repository.Instance.GetMany<MaterialModel>(sql);
        }

        public IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlaningHeadModel model)
        {
            IEnumerable<MaterialModel> query = this.GetAllMaterial(plant);

            if (!string.IsNullOrEmpty(model.MaterialPattern.CustID) && Convert.ToBoolean(model.CurrentClass.CustomerReq.GetInt())) query = query.Where(p => p.CustID.ToString().ToUpper().Equals(model.MaterialPattern.CustID.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.CommodityCode) && Convert.ToBoolean(model.CurrentClass.ComudityReq.GetInt())) query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(model.MaterialPattern.CommodityCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.SpecCode) && Convert.ToBoolean(model.CurrentClass.SpecCodeReq.GetInt())) query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(model.MaterialPattern.SpecCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.CoatingCode) && Convert.ToBoolean(model.CurrentClass.PlateCodeReq.GetInt())) query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(model.MaterialPattern.CoatingCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.MakerCode) && Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.ToString().ToUpper().Equals(model.MaterialPattern.MakerCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.MillCode) && Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.ToString().ToUpper().Equals(model.MaterialPattern.MillCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.SupplierCode) && Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.ToString().ToUpper().Equals(model.MaterialPattern.SupplierCode.ToString().ToUpper()));

            query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            if (Convert.ToBoolean(model.CurrentClass.ThicknessReq.GetInt())) query = query.Where(p => p.Thick.Equals(model.MaterialPattern.Thick));
            if (Convert.ToBoolean(model.CurrentClass.WidthReq.GetInt())) query = query.Where(p => p.Width.Equals(model.MaterialPattern.Width));
            if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(model.MaterialPattern.Length));

            return query;
        }

        public int GetLastStep(int workOrderID)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_pln_PlanHead
                                            WHERE WorkOrderID = {0}
                                            ORDER BY WorkOrderID DESC", workOrderID);

            int id = Repository.Instance.GetOne<int>(sql, "ProcessStep").GetInt();
            return Convert.ToInt32(id) + 1;
        }

        public int GetLastWorkOrder(string plant)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_pln_PlanHead
                                            WHERE Plant = N'{0}'
                                            ORDER BY WorkNumber DESC", plant);

            var id = Repository.Instance.GetOne<Int64>(sql, "WorkNumber");
            return Convert.ToInt32(id) + 1;
        }

        public string GenWorkOrderFixFormat(int id)
        {
            return ("K" + DateTime.Now.ToString("yy") + Enum.GetName(typeof(Month), int.Parse(DateTime.Now.ToString("MM"))) + id.ToString("0000"));
        }

        public PlaningHeadModel Save(Models.SessionInfo _session, PlaningHeadModel model)
        {
            int id = GetLastWorkOrder(_session.PlantID);
            string workOrderNum = GenWorkOrderFixFormat(id);

            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_pln_PlanHead (NOLOCK)
										    WHERE WorkNumber = {3} AND Plant = N'{1}'
									    )
                                        BEGIN
                                            INSERT INTO ucc_pln_PlanHead
                                                       (Company
                                                       ,Plant
                                                       ,WorkOrderNum
                                                       ,WorkNumber
                                                       ,ProcessStep
                                                       ,ProcessStepLocked
                                                       ,ProcessLine
                                                       ,OrderType
                                                       ,PIC
                                                       ,Possession
                                                       ,IssueDate
                                                       ,DueDate
                                                       ,UsingWgt
                                                       ,InputWgt
                                                       ,RewindWgt
                                                       ,OutputWgt
                                                       ,LossWgt
                                                       ,Yield
                                                       ,TotalMatAmount
                                                       ,BT
                                                       ,LVTrim
                                                       ,PackingPlan
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,TotalWidth)
                                                 VALUES
                                                       ( N'{0}' --<Company, nvarchar(8),>
                                                       , N'{1}' --<Plant, nvarchar(8),>
                                                       , N'{2}' --<WorkOrderNum, nvarchar(10),>
                                                       , {3} --<WorkNumber, bigint,>
                                                       , {4} --<ProcessStep, int,>
                                                       , {5} --<ProcessStepLocked, tinyint,>
                                                       , N'{6}' --<ProcessLine, nvarchar(20),>
                                                       , N'{7}' --<OrderType, nvarchar(20),>
                                                       , N'{8}' --<PIC, nvarchar(20),>
                                                       , N'{9}' --<Possession, nvarchar(20),>
                                                       , CONVERT(DATETIME, '{10}',103)  --<IssueDate, datetime,>
                                                       , CONVERT(DATETIME, '{11}',103)  --<DueDate, datetime,>
                                                       , {12} --<UsingWgt, decimal(20,9),>
                                                       , {13} --<InputWgt, decimal(20,9),>
                                                       , {14} --<RewindWgt, decimal(20,9),>
                                                       , {15} --<OutputWgt, decimal(20,9),>
                                                       , {16} --<LossWgt, decimal(20,9),>
                                                       , {17} --<Yield, decimal(20,9),>
                                                       , {18} --<TotalMatAmount, decimal(20,9),>
                                                       , N'{19}' --<BT, nvarchar(20),>
                                                       , {20} --<LVTrim, tinyint,>
                                                       , {21} --<PackingPlan, tinyint,>
                                                       , GETDATE() --<CreationDate, datetime,>
                                                       , GETDATE() --<LastUpdateDate, datetime,>
                                                       , N'{22}' --<CreatedBy, varchar(45),>
                                                       , N'{22}' --<UpdatedBy, varchar(45),>
                                                       , {23} --<TotalWidth, decimal(20,9),>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_pln_PlanHead
                                                   SET Company = N'{0}' --<Company, nvarchar(8),>
                                                      ,Plant = N'{1}' --<Plant, nvarchar(8),>
                                                      ,WorkOrderNum = N'{2}' --<WorkOrderNum, nvarchar(10),>
                                                      ,WorkNumber = {3} --<WorkNumber, bigint,>
                                                      ,ProcessStep = {4} --<ProcessStep, int,>
                                                      ,ProcessStepLocked = {5} --<ProcessStepLocked, tinyint,>
                                                      ,ProcessLine = N'{6}' --<ProcessLine, nvarchar(20),>
                                                      ,OrderType = N'{7}' --<OrderType, nvarchar(20),>
                                                      ,PIC = N'{8}' --<PIC, nvarchar(20),>
                                                      ,Possession = N'{9}' --<Possession, nvarchar(20),>
                                                      ,IssueDate = CONVERT(DATETIME, '{10}',103) --<IssueDate, datetime,>
                                                      ,DueDate = CONVERT(DATETIME, '{11}',103) --<DueDate, datetime,>
                                                      ,UsingWgt = {12} --<UsingWgt, decimal(20,9),>
                                                      ,InputWgt = {13} --<InputWgt, decimal(20,9),>
                                                      ,RewindWgt = {14} --<RewindWgt, decimal(20,9),>
                                                      ,OutputWgt = {15} --<OutputWgt, decimal(20,9),>
                                                      ,LossWgt = {16} --<LossWgt, decimal(20,9),>
                                                      ,Yield = {17} --<Yield, decimal(20,9),>
                                                      ,TotalMatAmount = {18} --<TotalMatAmount, decimal(20,9),>
                                                      ,BT = N'{19}' --<BT, nvarchar(20),>
                                                      ,LVTrim = {20} --<LVTrim, tinyint,>
                                                      ,PackingPlan = {21} --<PackingPlan, tinyint,>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,UpdatedBy = N'{22}' --<UpdatedBy, varchar(45),>
                                                      ,TotalWidth = {23} --<TotalWidth, decimal(20,9),>
                                                 WHERE WorkNumber = {3} AND Plant = N'{1}'
                                            END" + Environment.NewLine
                                              , _session.CompanyID
                                              , _session.PlantID
                                              , workOrderNum
                                              , id
                                              , model.ProcessStep
                                              , 0       //{5}
                                              , model.ProcessLineSpec.ResourceID
                                              , model.OrderType
                                              , model.PIC
                                              , model.Possession
                                              , model.IssueDate.ToString("dd/MM/yyyy hh:mm:ss")     //{10}
                                              , model.DueDate.ToString("dd/MM/yyyy hh:mm:ss")
                                              , model.UsingWeight.GetDecimal()
                                              , model.InputWeight.GetDecimal()
                                              , model.RewindWeight.GetDecimal()
                                              , model.OutputWeight.GetDecimal()      //{15}
                                              , model.LossWeight.GetDecimal()
                                              , model.Yield.GetDecimal()
                                              , model.TotalMaterialAmount.GetDecimal()
                                              , model.BT.GetString()
                                              , Convert.ToInt32(model.LVTrim).GetInt()        //{20}
                                              , Convert.ToInt32(model.PackingPlan).GetInt()
                                              , _session.UserID
                                              , model.TotalWeight.GetDecimal()
                                              );
            Repository.Instance.ExecuteWithTransaction(sql, "Update Planning");

            return GetWorkById(workOrderNum, _session.PlantID);
        }

        public IEnumerable<PlaningHeadModel> GetWorkAll(string plant)
        {
            throw new NotImplementedException();
        }

        public PlaningHeadModel GetWorkById(string workOrderNum, string plant)
        {
            string sql = string.Format(@"SELECT uf.Name as PICName, plh.*
                                            FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
                                            WHERE plh.WorkOrderNum = '{0}' AND plh.Plant = N'{1}'", workOrderNum, plant);

            var result = Repository.Instance.GetOne<PlaningHeadModel>(sql);

            if (result != null)
            {
                result.ResourceList = _repoResrc.GetAll(plant);
                result.OrderTypeList = _repoUcode.GetAll("OrderType");
                result.PossessionList = _repoUcode.GetAll("Pocessed");
                result.ProcessLineSpec = _repoResrc.GetByID(plant, result.ProcessLineSpec.ResourceID);
            }

            return result;
        }
    }
}