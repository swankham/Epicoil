using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
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
        private readonly IClassMasterRepo _repoCls;

        public WorkEntryRepo()
        {
            this._repoUcode = new UserCodeRepo();
            this._repoResrc = new ResourceRepo();
            this._repoCls = new ClassMasterRepo();
        }

        /// <summary>
        /// Get Material when new WorkOrder from master data [PartLot].
        /// Fix pl.CheckBox01 = 0 for material is not used in WorkOrder.
        /// </summary>
        /// <param name="plant"></param>
        /// <returns>Rows</returns>
        public IEnumerable<MaterialModel> GetAllMaterial(string plant)
        {
            string sql = string.Format(@"SELECT pl.PartNum, 0 as TransactionLineID
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , pl.Number04 as UsingWeight, pl.Number04 as RemainWeight
	                                        , oh.Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName, 0 as CBalready, 0 as UsingLM
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
                                        WHERE pln.Plant = N'{0}' AND pl.Number05 = 1 AND pl.Number08 IN (0, 1) AND pl.CheckBox01 = 0", plant);

            return Repository.Instance.GetMany<MaterialModel>(sql);
        }

        /// <summary>
        /// Get Material by WorkOrder from transaction data [WorkOrder => ucc_pln_Material].
        /// Fix pl.CheckBox01 = 1 for material is used in the [workOrderId] param.
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="workOrderId"></param>
        /// <returns>Rows</returns>
        public IEnumerable<MaterialModel> GetAllMaterial(string plant, int workOrderId)
        {
            string sql = string.Format(@"SELECT pl.PartNum, mat.TransactionLineID
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , pl.Number04 as UsingWeight, pl.Number04 as RemainWeight
	                                        , mat.Qty as Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName, mat.CBalready, mat.UsingLM
                                        FROM ucc_pln_Material mat
		                                    INNER JOIN PartLot pl ON(mat.MCSSNo = pl.PartNum AND mat.Serial = pl.LotNum)
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
                                        WHERE pln.Plant = N'{0}' AND pl.Number05 = 1 AND pl.Number08 IN (2) AND pl.CheckBox01 = 1
                                              AND mat.WorkOrderID = {1}", plant, workOrderId);

            return Repository.Instance.GetMany<MaterialModel>(sql);
        }

        /// <summary>
        /// Get one row from selecting material from transaction data [WorkOrder => ucc_pln_Material].
        /// </summary>
        /// <param name="transactionLineID">Type of transaction running unique id from ucc_pln_Material table</param>
        /// <returns>A row</returns>
        public MaterialModel GetMaterial(int transactionLineID)
        {
            string sql = string.Format(@"SELECT pl.PartNum, mat.TransactionLineID
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , pl.Number04 as UsingWeight, pl.Number04 as RemainWeight
	                                        , mat.Qty as Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName, mat.CBalready, mat.UsingLM
                                        FROM ucc_pln_Material mat
		                                    INNER JOIN PartLot pl ON(mat.MCSSNo = pl.PartNum AND mat.Serial = pl.LotNum)
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
                                        WHERE mat.TransactionLineID ={0} AND pl.Number05 = 1 AND pl.Number08 IN (2)", transactionLineID);

            return Repository.Instance.GetOne<MaterialModel>(sql);
        }

        /// <summary>
        /// Get one row from selecting material from master data [PartLot].
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="partNum"></param>
        /// <param name="lotNum"></param>
        /// <returns>A row</returns>
        public MaterialModel GetMaterial(string plant, string partNum, string lotNum)
        {
            string sql = string.Format(@"SELECT pl.PartNum, 0 as TransactionLineID
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , pl.Number04 as UsingWeight, pl.Number04 as RemainWeight
	                                        , oh.Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName, 0 as CBalready, 0 as UsingLM
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
                                        WHERE pln.Plant = N'{0}' AND pl.Number05 = 1 AND pl.Number08 IN (0, 1) --AND pl.CheckBox01 = 0
                                              AND pl.PartNum = N'{1}' AND pl.LotNum = N'{2}'", plant, partNum, lotNum);

            return Repository.Instance.GetOne<MaterialModel>(sql);
        }

        /// <summary>
        /// Get rows by filtering base on the model from master data [PartLot].
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="model"></param>
        /// <returns>Rows</returns>
        public IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlanningHeadModel model)
        {
            IEnumerable<MaterialModel> query = this.GetAllMaterial(plant);
            if (model.CurrentClass != null)
            {
                if (!string.IsNullOrEmpty(model.MaterialPattern.CustID) && Convert.ToBoolean(model.CurrentClass.CustomerReq.GetInt())) query = query.Where(p => p.CustID.ToString().ToUpper().Equals(model.MaterialPattern.CustID.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.CommodityCode) && Convert.ToBoolean(model.CurrentClass.ComudityReq.GetInt())) query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(model.MaterialPattern.CommodityCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.SpecCode) && Convert.ToBoolean(model.CurrentClass.SpecCodeReq.GetInt())) query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(model.MaterialPattern.SpecCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.CoatingCode) && Convert.ToBoolean(model.CurrentClass.PlateCodeReq.GetInt())) query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(model.MaterialPattern.CoatingCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.MakerCode) && Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.ToString().ToUpper().Equals(model.MaterialPattern.MakerCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.MillCode) && Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.ToString().ToUpper().Equals(model.MaterialPattern.MillCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.SupplierCode) && Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.ToString().ToUpper().Equals(model.MaterialPattern.SupplierCode.ToString().ToUpper()));

                if (Convert.ToBoolean(model.CurrentClass.ThicknessReq.GetInt())) query = query.Where(p => p.Thick.Equals(model.MaterialPattern.Thick));
                if (Convert.ToBoolean(model.CurrentClass.WidthReq.GetInt())) query = query.Where(p => p.Width.Equals(model.MaterialPattern.Width));
                if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(model.MaterialPattern.Length));
            }

            if (!string.IsNullOrEmpty(model.Possession)) query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            //if (model.ProcessLineDetail.ThickMin != 0)
            query = query.Where(p => p.Thick >= model.ProcessLineDetail.ThickMin);
            //if (model.ProcessLineDetail.ThickMax != 0)
            query = query.Where(p => p.Thick <= model.ProcessLineDetail.ThickMax);
            //if (model.ProcessLineDetail.WidthMin != 0)
            query = query.Where(p => p.Width >= model.ProcessLineDetail.WidthMin);
            //if (model.ProcessLineDetail.WidthMax != 0)
            query = query.Where(p => p.Width <= model.ProcessLineDetail.WidthMax);
            //if (model.ProcessLineDetail.LengthMin != 0)
            query = query.Where(p => p.Length >= model.ProcessLineDetail.LengthMin);
            //if (model.ProcessLineDetail.LengthMax != 0)
            query = query.Where(p => p.Length <= model.ProcessLineDetail.LengthMax);

            //if machine is not Sliter and Leveller must be filter for sheet only.
            if (model.ProcessLineDetail.LengthMin > 0 || model.ProcessLineDetail.LengthMax > 0)
                query = query.Where(p => p.Length > 0);

            //if machine is Sliter and Leveller must be filter for coil only.
            if (model.ProcessLineDetail.LengthMin == 0 && model.ProcessLineDetail.LengthMax == 0)
                query = query.Where(p => p.Length.Equals(0));

            return query;
        }

        /// <summary>
        /// Get last WorkOrder step for each WorkOrder by WorkOrderID.
        /// </summary>
        /// <param name="workOrderID"></param>
        /// <returns>int</returns>
        public int GetLastStep(int workOrderID)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_pln_PlanHead
                                            WHERE WorkOrderID = {0}
                                            ORDER BY WorkOrderID DESC", workOrderID);

            int id = Repository.Instance.GetOne<int>(sql, "ProcessStep").GetInt();
            return Convert.ToInt32(id) + 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        public int GetLastWorkOrder(string plant)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_pln_PlanHead
                                            WHERE Plant = N'{0}'
                                            ORDER BY WorkNumber DESC", plant);

            var id = Repository.Instance.GetOne<Int64>(sql, "WorkNumber");
            return Convert.ToInt32(id) + 1;
        }

        /// <summary>
        /// Formatting WorkOrder Number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string : WorkOrderNumber</returns>
        public string GenWorkOrderFixFormat(int id)
        {
            return ("K" + DateTime.Now.ToString("yy") + Enum.GetName(typeof(Month), int.Parse(DateTime.Now.ToString("MM"))) + id.ToString("0000"));
        }

        /// <summary>
        /// Save transaction header.
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="model"></param>
        /// <returns>PlaningHeadModel : Transaction to saved</returns>
        public PlanningHeadModel Save(Models.SessionInfo _session, PlanningHeadModel model)
        {
            int id = 0;
            string workOrderNum = "";

            if (string.IsNullOrEmpty(model.WorkOrderNum))
            {
                id = GetLastWorkOrder(_session.PlantID);
                workOrderNum = GenWorkOrderFixFormat(id);
            }
            else
            {
                id = model.WorkOrderID;
                workOrderNum = model.WorkOrderNum;
            }

            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_pln_PlanHead (NOLOCK)
										    WHERE WorkOrderID = {3} AND ProcessStep = {4} AND Plant = N'{1}'
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
                                                 WHERE WorkOrderID = {3} AND ProcessStep = {4} AND Plant = N'{1}'
                                            END" + Environment.NewLine
                                              , _session.CompanyID
                                              , _session.PlantID
                                              , workOrderNum
                                              , id
                                              , model.ProcessStep
                                              , 0       //{5}
                                              , model.ProcessLineId
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

            return GetWorkById(workOrderNum, Convert.ToInt32(model.ProcessStep), _session.PlantID);
        }

        /// <summary>
        /// Get WorkOrders all that to saved.
        /// </summary>
        /// <param name="plant"></param>
        /// <returns>WorkOrder rows</returns>
        public IEnumerable<PlanningHeadModel> GetWorkAll(string plant)
        {
            string sql = string.Format(@"SELECT uf.Name as PICName, plh.*
                                            FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
                                            WHERE plh.Plant = N'{0}'", plant);

            var result = Repository.Instance.GetMany<PlanningHeadModel>(sql);
            return result;
        }

        /// <summary>
        /// Get WorkOrder that to saved by WorkOrderNumber.
        /// </summary>
        /// <param name="workOrderNum"></param>
        /// <param name="plant"></param>
        /// <returns>a row by workOrderNum</returns>
        public PlanningHeadModel GetWorkById(string workOrderNum, int processStep, string plant)
        {
            string sql = string.Format(@"SELECT uf.Name as PICName, plh.*
                                            FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
                                            WHERE plh.WorkOrderNum = '{0}' AND plh.Plant = N'{1}' AND ProcessStep = {2}", workOrderNum, plant, processStep);

            var result = Repository.Instance.GetOne<PlanningHeadModel>(sql);

            if (result != null)
            {
                result.ResourceList = _repoResrc.GetAll(plant).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S"));
                result.OrderTypeList = _repoUcode.GetAll("OrderType");
                result.PossessionList = _repoUcode.GetAll("Pocessed");
                result.ProcessLineDetail = _repoResrc.GetByID(plant, result.ProcessLineId);
                result.Materails = GetAllMaterial(plant, result.WorkOrderID).ToList();
                result.CurrentClass = _repoCls.GetByID(plant, result.ClassID);
            }

            return result;
        }

        /// <summary>
        /// Save a material for each WorkOrder.
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaterialModel SaveMaterial(Models.SessionInfo _session, MaterialModel model)
        {
            //int id = 0;
            string sql = string.Format(@"INSERT INTO ucc_pln_Material
                                               (Company
                                               ,Plant
                                               ,WorkOrderID
                                               ,MatSeq
                                               ,Serial
                                               ,Cmdty
                                               ,Spec
                                               ,Coating
                                               ,Category
                                               ,Thick
                                               ,Width
                                               ,Length
                                               ,Weight
                                               ,UsingWgt
                                               ,RemainWgt
                                               ,Qty
                                               ,RemainQty
                                               ,LenghtM
                                               ,UsingLM
                                               ,SelectCB
                                               ,CBalready
                                               ,MCSSNo
                                               ,Status
                                               ,BT
                                               ,Possession
                                               ,ProductSts
                                               ,Descriptions
                                               ,Note
                                               ,CreationDate
                                               ,LastUpdateDate
                                               ,CreatedBy
                                               ,UpdatedBy)
                                         VALUES
                                               ( N'{0}' --<Company, nvarchar(8),>
                                               , N'{1}' --<Plant, nvarchar(8),>
                                               , {2} --<WorkOrderID, bigint,>
                                               , {3} --<MatSeq, int,>
                                               , N'{4}' --<Serial, nvarchar(18),>
                                               , N'{5}' --<Cmdty, nvarchar(10),>
                                               , N'{6}' --<Spec, nvarchar(10),>
                                               , N'{7}' --<Coating, nvarchar(10),>
                                               , N'{8}' --<Category, nvarchar(10),>
                                               , {9} --<Thick, decimal(20,9),>
                                               , {10} --<Width, decimal(20,9),>
                                               , {11} --<Length, decimal(20,9),>
                                               , {12} --<Weight, decimal(20,9),>
                                               , {13} --<UsingWgt, decimal(20,9),>
                                               , {14} --<RemainWgt, decimal(20,9),>
                                               , {15} --<Qty, decimal(20,9),>
                                               , {16} --<RemainQty, decimal(20,9),>
                                               , {17} --<LenghtM, decimal(20,9),>
                                               , {18} --<UsingLM, decimal(20,9),>
                                               , {19} --<SelectCB, tinyint,>
                                               , {20} --<CBalready, tinyint,>
                                               , N'{21}' --<MCSSNo, nvarchar(18),>
                                               , N'{22}' --<Status, nvarchar(10),>
                                               , N'{23}' --<BT, nvarchar(10),>
                                               , {24} --<Possession, int,>
                                               , N'{25}' --<ProductSts, nvarchar(10),>
                                               , N'{26}' --<Descriptions, varchar(500),>
                                               , N'{27}' --<Note, varchar(500),>
                                               , GETDATE() --<CreationDate, datetime,>
                                               , GETDATE() --<LastUpdateDate, datetime,>
                                               , N'{28}' --<CreatedBy, varchar(45),>
                                               , N'{28}' --<UpdatedBy, varchar(45),>
		                                    )" + Environment.NewLine
                                              , _session.CompanyID
                                              , _session.PlantID
                                              , model.WorkOrderID
                                              , 1 //model.Seq
                                              , model.SerialNo
                                              , model.CommodityCode     //{5}
                                              , model.SpecCode
                                              , model.CoatingCode
                                              , ""
                                              , model.Thick
                                              , model.Width         //{10}
                                              , model.Length
                                              , model.Weight
                                              , model.UsingWeight
                                              , model.RemainWeight
                                              , model.UsingQuantity      //{15}
                                              , model.RemainQuantity
                                              , model.LengthM
                                              , model.UsingLengthM
                                              , Convert.ToInt32(model.CBSelect.GetBoolean())
                                              , Convert.ToInt32(model.CBalready.GetBoolean())   //{20}
                                              , model.MCSSNo
                                              , model.Status
                                              , model.BussinessType
                                              , model.Possession
                                              , model.ProductStatus         //{25}
                                              , model.PrdDescriptions
                                              , model.Note
                                              , _session.UserID
                                              );

            sql += string.Format(@"UPDATE PartLot SET CheckBox01 = 1, ShortChar05 = N'{2}', Date03 = CONVERT(DATETIME, '{3}',103), Number08 = 2
                                   WHERE PartNum = N'{0}' AND LotNum = N'{1}'"
                                   , model.MCSSNo, model.SerialNo, model.WorkOrderNum, model.WorkDate.ToString("dd/MM/yyyy hh:mm:ss"));

            Repository.Instance.ExecuteWithTransaction(sql, "Add Material");
            return GetMaterial(_session.PlantID, model.MCSSNo, model.SerialNo);
        }

        /// <summary>
        /// Must be call PlanningHeadModel.ValidateToDelMaterial
        /// </summary>
        /// <param name="_session">Type of current session login</param>
        /// <param name="model">Type of material selected to delete</param>
        /// <param name="msg">Out put result messege from this</param>
        /// <returns>true = success/false = unsuccess</returns>
        public bool DeleteMaterail(Models.SessionInfo _session, MaterialModel model, out string msg)
        {
            msg = "";
            try
            {
                string sql = string.Format(@"DELETE FROM ucc_pln_Material WHERE TransactionLineID = {0}", model.TransactionLineID);

                sql += string.Format(@"UPDATE PartLot SET CheckBox01 = 0, ShortChar05 = '', Date03 = null , Number08 = 1
                                        WHERE PartNum = N'{0}' AND LotNum = N'{1}'"
                                           , model.MCSSNo, model.SerialNo);

                Repository.Instance.ExecuteWithTransaction(sql, "Delete Material");
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public decimal CalUnitWgt(decimal T, decimal W, decimal L, decimal Gravity, decimal FrontCoat, decimal BackCoat)
        {//If Coil length must be LengthM*1000
            decimal Ma = 0.0M;
            decimal Mb = 0.0M;
            decimal Mc = 0.0M;
            decimal WgtPerUnit = 0;

            Ma = (T * Gravity) + ((FrontCoat + BackCoat) / 1000);
            Mb = (W / 1000) * (L);
            Mc = (Ma * Mb);
            WgtPerUnit = Math.Round(Mc, 2);
            return WgtPerUnit;
        }

        public decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        public decimal CalWgtFromMat(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        public CutDesignModel GetCuttingByID(int LineID)
        {
            string sql = string.Format(@"SELECT cut.* FROM ucc_pln_CuttingDesign cut WHERE cut.LineID = {0}", LineID);
            return Repository.Instance.GetOne<CutDesignModel>(sql);
        }

        public IEnumerable<CutDesignModel> GetCuttingLines(int workOrderID)
        {
            string sql = string.Format(@"SELECT cut.* FROM ucc_pln_CuttingDesign cut WHERE cut.WorkOrderID = {0}", workOrderID);
            return Repository.Instance.GetMany<CutDesignModel>(sql);
        }

        public IEnumerable<CutDesignModel> SaveLineCutting(SessionInfo _session, PlanningHeadModel head, CutDesignModel data)
        {
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_pln_CuttingDesign (NOLOCK)
										    WHERE LineID = {34}
									    )
                                        BEGIN
                                            INSERT INTO ucc_pln_CuttingDesign
                                                       (Company
                                                       ,Plant
                                                       ,WorkOrderID
                                                       ,TransactionLineID
                                                       ,CutSeq
                                                       ,SONo
                                                       ,SOLine
                                                       ,NORNum
                                                       ,CommodityCode
                                                       ,SpecCode
                                                       ,CoatingCode
                                                       ,Thick
                                                       ,Width
                                                       ,Length
                                                       ,Status
                                                       ,Stand
                                                       ,CutDivision
                                                       ,UnitWeight
                                                       ,TotalWeight
                                                       ,CustID
                                                       ,EndUserCode
                                                       ,DestinationCode
                                                       ,QtyPack
                                                       ,Pack
                                                       ,SOWeight
                                                       ,SOQuantity
                                                       ,CalQuantity
                                                       ,DeliveryDate
                                                       ,BussinessType
                                                       ,Possession
                                                       ,ProductStatus
                                                       ,Description
                                                       ,Note
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy)
                                                 VALUES
                                                       ( N'{0}' --<Company, nvarchar(8),>
                                                       , N'{1}' --<Plant, nvarchar(8),>
                                                       , {2} --<WorkOrderID, bigint,>
                                                       , {3} --<TransactionLineID, bigint,>
                                                       , {4} --<CutSeq, int,>
                                                       , N'{5}' --<SONo, nvarchar(30),>
                                                       , {6} --<SOLine, int,>
                                                       , N'{7}' --<NORNum, nvarchar(100),>
                                                       , N'{8}' --<CommodityCode, nvarchar(30),>
                                                       , N'{9}' --<SpecCode, nvarchar(30),>
                                                       , N'{10}' --<CoatingCode, nvarchar(30),>
                                                       , {11} --<Thick, decimal(20,9),>
                                                       , {12} --<Width, decimal(20,9),>
                                                       , {13} --<Length, decimal(20,9),>
                                                       , N'{14}' --<Status, nvarchar(20),>
                                                       , {15} --<Stand, int,>
                                                       , {16} --<CutDivision, int,>
                                                       , {17} --<UnitWeight, decimal(20,9),>
                                                       , {18} --<TotalWeight, decimal(20,9),>
                                                       , N'{19}' --<CustID, nvarchar(20),>
                                                       , N'{20}' --<EndUserCode, nvarchar(20),>
                                                       , N'{21}' --<DestinationCode, nvarchar(20),>
                                                       , {22} --<QtyPack, decimal(20,9),>
                                                       , {23} --<Pack, decimal(20,9),>
                                                       , {24} --<SOWeight, decimal(20,9),>
                                                       , {25} --<SOQuantity, decimal(20,9),>
                                                       , {26} --<CalQuantity, decimal(20,9),>
                                                       , CONVERT(DATETIME, '{27}',103) --<DeliveryDate, datetime,>
                                                       , N'{28}' --<BussinessType, nvarchar(50),>
                                                       , {29} --<Procession, int,>
                                                       , {30} --<ProductStatus, int,>
                                                       , N'{31}' --<Description, varchar(max),>
                                                       , N'{32}' --<Note, varchar(max),>
                                                       , GETDATE() --<CreationDate, datetime,>
                                                       , GETDATE() --<LastUpdateDate, datetime,>
                                                       , N'{33}' --<CreatedBy, varchar(45),>
                                                       , N'{33}' --<UpdatedBy, varchar(45),>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_pln_CuttingDesign
                                                   SET Company = N'{0}'  --<Company, nvarchar(8),>
                                                      ,Plant = N'{1}'  --<Plant, nvarchar(8),>
                                                      ,WorkOrderID = {2}  --<WorkOrderID, bigint,>
                                                      ,TransactionLineID = {3}  --<TransactionLineID, bigint,>
                                                      ,CutSeq = {4}  --<CutSeq, int,>
                                                      ,SONo = N'{5}'  --<SONo, nvarchar(30),>
                                                      ,SOLine = {6}  --<SOLine, int,>
                                                      ,NORNum = N'{7}'  --<NORNum, nvarchar(100),>
                                                      ,CommodityCode = N'{8}'  --<CommodityCode, nvarchar(30),>
                                                      ,SpecCode = N'{9}'  --<SpecCode, nvarchar(30),>
                                                      ,CoatingCode = N'{10}'  --<CoatingCode, nvarchar(30),>
                                                      ,Thick = {11}  --<Thick, decimal(20,9),>
                                                      ,Width = {12}  --<Width, decimal(20,9),>
                                                      ,Length = {13}  --<Length, decimal(20,9),>
                                                      ,Status = N'{14}'  --<Status, nvarchar(20),>
                                                      ,Stand = {15}  --<Stand, int,>
                                                      ,CutDivision = {16}  --<CutDivision, int,>
                                                      ,UnitWeight = {17}  --<UnitWeight, decimal(20,9),>
                                                      ,TotalWeight = {18}  --<TotalWeight, decimal(20,9),>
                                                      ,CustID = N'{19}'  --<CustID, nvarchar(20),>
                                                      ,EndUserCode = N'{20}'  --<EndUserCode, nvarchar(20),>
                                                      ,DestinationCode = N'{21}'  --<DestinationCode, nvarchar(20),>
                                                      ,QtyPack = {22}  --<QtyPack, decimal(20,9),>
                                                      ,Pack = {23}  --<Pack, decimal(20,9),>
                                                      ,SOWeight = {24}  --<SOWeight, decimal(20,9),>
                                                      ,SOQuantity = {25}  --<SOQuantity, decimal(20,9),>
                                                      ,CalQuantity = {26}  --<CalQuantity, decimal(20,9),>
                                                      ,DeliveryDate = CONVERT(DATETIME, '{27}',103)  --<DeliveryDate, datetime,>
                                                      ,BussinessType = N'{28}'  --<BussinessType, nvarchar(50),>
                                                      ,Possession = {29}  --<Procession, int,>
                                                      ,ProductStatus = {30}  --<ProductStatus, int,>
                                                      ,Description = N'{31}'  --<Description, varchar(max),>
                                                      ,Note = N'{32}'  --<Note, varchar(max),>
                                                      ,LastUpdateDate = GETDATE()  --<LastUpdateDate, datetime,>
                                                      ,CreatedBy = N'{33}'  --<CreatedBy, varchar(45),>
                                                      ,UpdatedBy = N'{33}'  --<UpdatedBy, varchar(45),>
                                                 WHERE LineID = {34}
                                            END" + Environment.NewLine
                                  , _session.CompanyID
                                  , _session.PlantID
                                  , head.WorkOrderID
                                  , 0 //Defualt TransactionLineID => Material Line
                                  , data.CutSeq.GetString()
                                  , data.SONo.GetString()           //{5}
                                  , data.SOLine.GetInt()
                                  , data.NORNum.GetString()
                                  , data.CommodityCode.GetString()
                                  , data.SpecCode.GetString()
                                  , data.CoatingCode.GetString()    //{10}
                                  , data.Thick.GetDecimal()
                                  , data.Width
                                  , data.Length
                                  , data.Status.GetString()
                                  , data.Stand      //{15}
                                  , data.CutDivision
                                  , data.UnitWeight
                                  , data.TotalWeight
                                  , data.CustID.GetString()
                                  , data.EndUserCode.GetString()    //{20}
                                  , data.DestinationCode.GetString()
                                  , data.QtyPack
                                  , data.Pack
                                  , data.SOWeight
                                  , data.SOQuantity     //{25}
                                  , data.CalQuantity
                                  , data.DeliveryDate.ToString("dd/MM/yyyy hh:mm:ss")
                                  , data.BussinessType.GetString()
                                  , data.Possession
                                  , data.ProductStatus.GetInt()      //{30}
                                  , data.Description.GetString()
                                  , data.Note.GetString()
                                  , _session.UserID
                                  , data.LineID.GetInt()
                                  );
            Repository.Instance.ExecuteWithTransaction(sql, "Update Cutting");

            return GetCuttingLines(head.WorkOrderID);
        }

        public bool DeleteCutting(SessionInfo _session, CutDesignModel model, out string msg)
        {
            msg = "";
            try
            {
                string sql = string.Format(@"DELETE FROM ucc_pln_CuttingDesign WHERE LineID = {0}", model.LineID);
                Repository.Instance.ExecuteWithTransaction(sql, "Delete Cutting");
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}