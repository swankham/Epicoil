using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.StoreIn;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Repositories.Planning
{
    public class WorkEntryRepo : IWorkEntryRepo
    {
        private readonly IClassMasterRepo _repoCls;
        private readonly IResourceRepo _repoResrc;
        private readonly IUserCodeRepo _repoUcode;
        private readonly ICoilBackRuleRepo _repoRule;
        private readonly IStoreInRepo _repoIn;

        public WorkEntryRepo()
        {
            this._repoUcode = new UserCodeRepo();
            this._repoResrc = new ResourceRepo();
            this._repoCls = new ClassMasterRepo();
            this._repoRule = new CoilBackRuleRepo();
            this._repoIn = new StoreInRepo();
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

        public decimal CalWgtFromMat(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        public decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        public IEnumerable<CoilBackModel> DeleteCoilBack(SessionInfo _session, int workOrderId, int transactionLineID)
        {
            string sql = string.Format(@"DELETE FROM ucc_pln_CoilBack WHERE TransactionLineID = {0}", transactionLineID);
            Repository.Instance.ExecuteWithTransaction(sql, "Delete CoilBack");

            return GetCoilBackAll(workOrderId);
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

        public IEnumerable<CutDesignModel> GenerateCuttingLine(SessionInfo _session, PlanningHeadModel head, out string risk, out string msg)
        {
            risk = "";
            msg = "";
            if (head.Materails.ToList().Count > 0)
            {
                var cutLines = GetCuttingLines(head.WorkOrderID);
                var rowData = (from item in head.Materails
                               select item).First();

                risk = string.Empty;
                msg = string.Empty;
                decimal cutTotalWidth = cutLines.Sum(i => i.Width * i.Stand);

                if (rowData.Width > cutTotalWidth)
                {
                    var newCut = cutLines.First();
                    newCut.SONo = "";
                    newCut.SOLine = 0;
                    newCut.NORNum = "";
                    newCut.LineID = 0;
                    newCut.Status = "S";
                    newCut.Width = rowData.Width - cutTotalWidth;
                    newCut.Stand = 1;
                    newCut.CalculateRow(head);
                    if (newCut.ValidateByRow(head, out risk, out msg))
                    {
                        var result = SaveLineCutting(_session, head, newCut);
                    }
                }
            }

            return GetCuttingLines(head.WorkOrderID);
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
        /// Get rows by filtering base on the model from master data [PartLot].
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="model"></param>
        /// <returns>Rows</returns>
        public IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlanningHeadModel model)
        {
            IEnumerable<MaterialModel> query = this.GetAllMaterial(plant);

            //Verify alway.
            //if (!string.IsNullOrEmpty(model.BussinessType))
            if (!string.IsNullOrEmpty(model.BussinessType)) query = query.Where(p => p.BussinessType.Equals(model.BussinessType.GetString()));
            if (!string.IsNullOrEmpty(model.Possession)) query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            if (model.Materails.ToList().Count > 0 && model.CuttingDesign.ToList().Count == 0)
            {
                var mat = model.Materails.FirstOrDefault();
                query = query.Where(p => p.CustID.ToString().ToUpper().Equals(mat.CustID.ToString().ToUpper()));
                query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(mat.CommodityCode.ToString().ToUpper()));
                query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(mat.SpecCode.ToString().ToUpper()));
                query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(mat.CoatingCode.ToString().ToUpper()));

                if (Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.ToString().ToUpper().Equals(mat.MakerCode.ToString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.ToString().ToUpper().Equals(mat.MillCode.ToString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.ToString().ToUpper().Equals(mat.SupplierCode.ToString().ToUpper()));

                if (Convert.ToBoolean(model.CurrentClass.ThicknessReq.GetInt())) query = query.Where(p => p.Thick.Equals(mat.Thick));
                if (Convert.ToBoolean(model.CurrentClass.WidthReq.GetInt())) query = query.Where(p => p.Width.Equals(mat.Width));
                if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(mat.Length));
            }

            if (model.CuttingDesign.ToList().Count > 0)
            {
                var cut = model.CuttingDesign.FirstOrDefault();
                if (Convert.ToBoolean(model.CurrentClass.CustomerReq.GetInt())) query = query.Where(p => p.CustID.ToString().ToUpper().Equals(cut.CustID.ToString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.ComudityReq.GetInt())) query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(cut.CommodityCode.ToString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.SpecCodeReq.GetInt())) query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(cut.SpecCode.ToString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.PlateCodeReq.GetInt())) query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(cut.CoatingCode.ToString().ToUpper()));

                query = query.Where(p => p.Thick.Equals(cut.Thick));
                query = query.Where(p => p.Width >= cut.Width);
                if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(cut.Length));
            }

            query = query.Where(p => p.Thick >= model.ProcessLineDetail.ThickMin);
            query = query.Where(p => p.Thick <= model.ProcessLineDetail.ThickMax);
            query = query.Where(p => p.Width >= model.ProcessLineDetail.WidthMin);
            query = query.Where(p => p.Width <= model.ProcessLineDetail.WidthMax);
            query = query.Where(p => p.Length >= model.ProcessLineDetail.LengthMin);
            query = query.Where(p => p.Length <= model.ProcessLineDetail.LengthMax);

            //if machine is not Sliter and Leveller must be filter for "SHEET" only.
            if (model.ProcessLineDetail.LengthMin > 0 || model.ProcessLineDetail.LengthMax > 0)
                query = query.Where(p => p.Length > 0);

            //if machine is Sliter and Leveller must be filter for "COIL" only.
            if (model.ProcessLineDetail.LengthMin == 0 && model.ProcessLineDetail.LengthMax == 0)
                query = query.Where(p => p.Length.Equals(0));

            return query;
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
	                                        , mat.UsingWgt as UsingWeight, mat.RemainWgt as RemainWeight
	                                        , mat.Qty as Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, SelectCB as CBSelect
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

        public IEnumerable<CoilBackModel> GetCoilBackAll(int workOrderId)
        {
            string sql = string.Format(@"SELECT cb.LineID, cb.WorkOrderID
	                                        , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , busi.Key1 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , cb.*
                                        FROM ucc_pln_CoilBack cb
	                                        LEFT JOIN UD29 cmdt ON(cb.Cmdty = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(cb.Cmdty = spec.Key2 and cb.Spec = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(cb.Coating = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(cb.BT = busi.Key1)
                                        WHERE cb.WorkOrderID = {0}
                                        ORDER BY cb.LineID", workOrderId);

            return Repository.Instance.GetMany<CoilBackModel>(sql);
        }

        public CoilBackModel GetCoilBackByID(int transactionLineID)
        {
            string sql = string.Format(@"SELECT cb.LineID, cb.WorkOrderID
	                                        , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , busi.Key1 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , cb.*
                                        FROM ucc_pln_CoilBack cb
	                                        LEFT JOIN UD29 cmdt ON(cb.Cmdty = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(cb.Cmdty = spec.Key2 and cb.Spec = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(cb.Coating = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(cb.BT = busi.Key1)
                                        WHERE cb.transactionLineID = {0}", transactionLineID);

            return Repository.Instance.GetOne<CoilBackModel>(sql);
        }

        public CutDesignModel GetCuttingByID(int LineID)
        {
            string sql = string.Format(@"SELECT cut.*, busi.Character01 as BussinessTypeName
                                            FROM ucc_pln_CuttingDesign cut
                                                LEFT JOIN UD25 busi ON(cut.BussinessType = busi.Key1)
                                            WHERE cut.LineID = {0}", LineID);
            return Repository.Instance.GetOne<CutDesignModel>(sql);
        }

        public IEnumerable<CutDesignModel> GetCuttingLines(int workOrderID)
        {
            string sql = string.Format(@"SELECT cut.*, busi.Character01 as BussinessTypeName
                                            FROM ucc_pln_CuttingDesign cut
                                                LEFT JOIN UD25 busi ON(cut.BussinessType = busi.Key1)
                                            WHERE cut.WorkOrderID = {0} ORDER BY LineID ASC", workOrderID);
            return Repository.Instance.GetMany<CutDesignModel>(sql);
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
	                                        , mat.UsingWgt as UsingWeight, mat.RemainWgt as RemainWeight
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
        /// Get WorkOrders all that to saved.
        /// </summary>
        /// <param name="plant"></param>
        /// <returns>WorkOrder rows</returns>
        public IEnumerable<PlanningHeadModel> GetWorkAll(string plant)
        {
            string sql = string.Format(@"SELECT uf.Name as PICName, busi.Character01 as BussinessTypeName, plh.*
                                        FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
		                                    LEFT JOIN UD25 busi ON(plh.BT = busi.Key1)
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
            string sql = string.Format(@"SELECT uf.Name as PICName, busi.Character01 as BussinessTypeName, plh.*
                                            FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
                                            LEFT JOIN UD25 busi ON(plh.BT = busi.Key1)
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
                result.CuttingDesign = GetCuttingLines(result.WorkOrderID).ToList();
                result.CoilBacks = GetCoilBackAll(result.WorkOrderID).ToList();
                result.CoilBackRoles = _repoRule.GetAll().ToList();
            }

            return result;
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
                                                       ,TotalWidth
                                                       ,ClassID
                                                       ,Completed
                                                       ,SimulateFlag
                                                       ,GenSerialFlag
                                                       ,OpenFlag
                                                       ,OperationState)
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
                                                       , {24} --<ClassID, int,>
                                                       , {25} --<Completed, tinyint>
                                                       , {26} --<SimulateFlag, tinyint>
                                                       , {27} --<GenSerialFlag, tinyint>
                                                       , {28} --<OpenFlag, tinyint>
                                                       , {29} --<OperationState, int>
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
                                                      ,ClassID = {24} --<TotalWidth, decimal(20,9),>
                                                      ,Completed = {25} --<Completed, tinyint,>
                                                      ,SimulateFlag = {26} --<SimulateFlag, tinyint,>
                                                      ,GenSerialFlag = {27} --<GenSerialFlag, tinyint,>
                                                      ,OpenFlag = {28} --<OpenFlag, tinyint,>
                                                      ,OperationState = {29} --<OperationState, int>
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
                                              , model.BussinessType.GetString()
                                              , Convert.ToInt32(model.LVTrim).GetInt()        //{20}
                                              , Convert.ToInt32(model.PackingPlan).GetInt()
                                              , _session.UserID
                                              , model.TotalWidth.GetDecimal()
                                              , model.ClassID.GetInt()
                                              , model.Completed.GetInt()            //{25}
                                              , model.SimulateFlag.GetInt()
                                              , model.GenSerialFlag.GetInt()
                                              , model.OpenFlag.GetInt()
                                              , model.OperationState.GetInt()       //{29}
                                              );
            Repository.Instance.ExecuteWithTransaction(sql, "Update Planning");
            //if (model.Materails.ToList().Count != 0)
            //    var result = SaveMaterial(_session, model.Materails);

            return GetWorkById(workOrderNum, Convert.ToInt32(model.ProcessStep), _session.PlantID);
        }

        private int GetCoilBackStep(string serial)
        {
            string sql = string.Format(@"SELECT BackStep FROM ucc_pln_CoilBack (NOLOCK)
                                            WHERE Serial = N'{0}'", serial);

            var result = Repository.Instance.GetOne<int>(sql, "BackStep").GetInt();
            return result;
        }

        public IEnumerable<CoilBackModel> SaveCoilBack(SessionInfo _session, CoilBackModel data)
        {
            data.BackStep = GetCoilBackStep(data.OldSerial) + 1;
            data.Serial = data.OldSerial + Enum.GetName(typeof(CoilStep), Convert.ToInt32(data.BackStep));

            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_pln_CoilBack (NOLOCK)
										    WHERE TransactionLineID = {1}
									    )
                                        BEGIN
                                            INSERT INTO ucc_pln_CoilBack
                                                       (WorkOrderID
                                                       ,TransactionLineID
                                                       ,SeqID
                                                       ,Serial
                                                       ,Cmdty
                                                       ,Spec
                                                       ,Coating
                                                       ,Thick
                                                       ,Width
                                                       ,Length
                                                       ,Weight
                                                       ,Qty
                                                       ,LengthM
                                                       ,MCSSNo
                                                       ,OldSerial
                                                       ,BackStep
                                                       ,Status
                                                       ,BT
                                                       ,Possession
                                                       ,ProductStatus
                                                       ,Description
                                                       ,Note
                                                       ,CoilBackState
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy)
                                                 VALUES
                                                       ({0}  --<WorkOrderID, bigint,>
                                                       ,{1}  --<TransactionLineID, bigint,>
                                                       ,{2}  --<SeqID, int,>
                                                       ,N'{3}'  --<Serial, nvarchar(18),>
                                                       ,N'{4}'  --<Cmdty, nvarchar(10),>
                                                       ,N'{5}'  --<Spec, nvarchar(10),>
                                                       ,N'{6}'  --<Coating, nvarchar(10),>
                                                       ,{7}  --<Thick, decimal(20,9),>
                                                       ,{8}  --<Widht, decimal(20,9),>
                                                       ,{9}  --<Length, decimal(20,9),>
                                                       ,{10}  --<Weight, decimal(20,9),>
                                                       ,{11}  --<Qty, decimal(20,9),>
                                                       ,{12}  --<LengthM, decimal(20,9),>
                                                       ,N'{13}'  --<MCSSNo, nvarchar(18),>
                                                       ,N'{14}'  --<OldSerial, nvarchar(18),>
                                                       ,{15}  --<BackStep, int,>
                                                       ,N'{16}'  --<Status, nvarchar(10),>
                                                       ,N'{17}'  --<BT, nvarchar(10),>
                                                       ,{18}  --<Possession, int,>
                                                       ,{19}  --<ProductStatus, int,>
                                                       ,N'{20}'  --<Description, varchar(500),>
                                                       ,N'{21}'  --<Note, varchar(500),>
                                                       ,{22}  --<CoilBackState, int,>
                                                       ,GETDATE()  --<CreationDate, datetime,>
                                                       ,GETDATE()  --<LastUpdateDate, datetime,>
                                                       ,N'{23}'  --<CreatedBy, varchar(45),>
                                                       ,N'{23}'  --<UpdatedBy, varchar(45),>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_pln_CoilBack
                                                   SET WorkOrderID = {0}  --<WorkOrderID, bigint,>
                                                      ,TransactionLineID = {1}  --<TransactionLineID, bigint,>
                                                      ,SeqID = {2}  --<SeqID, int,>
                                                      ,Serial = N'{3}'  --<Serial, nvarchar(18),>
                                                      ,Cmdty = N'{4}'  --<Cmdty, nvarchar(10),>
                                                      ,Spec = N'{5}' --<Spec, nvarchar(10),>
                                                      ,Coating = N'{6}'  --<Coating, nvarchar(10),>
                                                      ,Thick = {7} --<Thick, decimal(20,9),>
                                                      ,Width = {8}  --<Widht, decimal(20,9),>
                                                      ,Length = {9}  --<Length, decimal(20,9),>
                                                      ,Weight = {10}  --<Weight, decimal(20,9),>
                                                      ,Qty = {11}  --<Qty, decimal(20,9),>
                                                      ,LengthM = {12}  --<LengthM, decimal(20,9),>
                                                      ,MCSSNo =  N'{13}' --<MCSSNo, nvarchar(18),>
                                                      ,OldSerial = N'{14}'  --<OldSerial, nvarchar(18),>
                                                      ,BackStep = {15}  --<BackStep, int,>
                                                      ,Status = N'{16}'  --<Status, nvarchar(10),>
                                                      ,BT = N'{17}'  --<BT, nvarchar(10),>
                                                      ,Possession = {18}  --<Possession, int,>
                                                      ,ProductStatus = {19}  --<ProductStatus, int,>
                                                      ,Description = N'{20}'  --<Description, varchar(500),>
                                                      ,Note = N'{21}'  --<Note, varchar(500),>
                                                      ,CoilBackState = {22}  --<CoilBackState, int,>
                                                      ,LastUpdateDate = GETDATE()  --<LastUpdateDate, datetime,>
                                                      ,UpdatedBy = N'{23}'  --<UpdatedBy, varchar(45),>
                                                 WHERE TransactionLineID = {1}
                                            END" + Environment.NewLine
                                  , data.WorkOrderID
                                  , data.TransactionLineID
                                  , data.SeqID
                                  , data.Serial
                                  , data.CommodityCode
                                  , data.SpecCode       //{5}
                                  , data.CoatingCode
                                  , data.Thick
                                  , data.Width
                                  , data.Length
                                  , data.Weight         //{10}
                                  , data.Qty
                                  , (data.Length == 0) ? data.CBLengthMeter(data.Weight, data.Width, data.Thick, data.Gravity, data.FrontPlate, data.BackPlate) : Math.Round((data.Length / 1000), 2)
                                  , data.MCSSNo
                                  , data.OldSerial
                                  , data.BackStep.GetInt()       //{15}
                                  , data.Status
                                  , data.BussinessType
                                  , data.Possession
                                  , data.ProductStatus
                                  , data.Description        //{20}
                                  , data.Note
                                  , data.CoilBackState.GetInt()
                                  , _session.UserID
                                  );
            Repository.Instance.ExecuteWithTransaction(sql, "Update Cutting");

            return GetCoilBackAll(data.WorkOrderID);
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
                                                       ,UpdatedBy, TotalLength)
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
                                                       , {35}  --<TotalLength, Decimal(20,9)>
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
                                                      ,TotalLength = {35}  --<TotalLength, decimal(20,9),>
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
                                  , data.Thick
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
                                  , data.TotalLength.GetDecimal()
                                  );
            Repository.Instance.ExecuteWithTransaction(sql, "Update Cutting");

            return GetCuttingLines(head.WorkOrderID);
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
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_pln_Material (NOLOCK)
										    WHERE TransactionLineID = {29}
									    )
                                        BEGIN
                                            INSERT INTO ucc_pln_Material
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
		                                    )
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE ucc_pln_Material
                                               SET UsingWgt = {13} --<UsingWgt, decimal(20,9),>
                                                  ,RemainWgt = {14} --<RemainWgt, decimal(20,9),>
                                                  ,Qty = {15} --<Qty, decimal(20,9),>
                                                  ,RemainQty = {16} --<RemainQty, decimal(20,9),>
                                                  ,LenghtM = {17} --<LenghtM, decimal(20,9),>
                                                  ,UsingLM = {18} --<UsingLM, decimal(20,9),>
                                                  ,SelectCB = {19} --<SelectCB, tinyint,>
                                                  ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                  ,UpdatedBy = N'{28}' --<UpdatedBy, varchar(45),>
                                             WHERE TransactionLineID = {29}
                                        END" + Environment.NewLine
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
                                              , model.TransactionLineID
                                              );

            //Update PartLot.CheckBox01 = 1 to change status has already used.
            sql += string.Format(@"UPDATE PartLot SET CheckBox01 = 1, ShortChar05 = N'{2}', Date03 = CONVERT(DATETIME, '{3}',103), Number08 = 2
                                   WHERE PartNum = N'{0}' AND LotNum = N'{1}'"
                                   , model.MCSSNo, model.SerialNo, model.WorkOrderNum, model.WorkDate.ToString("dd/MM/yyyy hh:mm:ss"));

            Repository.Instance.ExecuteWithTransaction(sql, "Add Material");
            return GetMaterial(_session.PlantID, model.MCSSNo, model.SerialNo);
        }

        public IEnumerable<SimulateModel> InsertSimulate(SessionInfo _session, PlanningHeadModel head, int cutDiv = 0)
        {
            int iRow = 0;
            foreach (var item in head.CuttingDesign.Where(i => i.Status != "S").OrderBy(i => i.LineID))
            {
                int cutDivision = (cutDiv == 0) ? item.CutDivision : cutDiv;

                for (int j = 1; j <= cutDivision; j++)
                {
                    iRow = iRow + j;
                    string sql = string.Format(@"INSERT INTO ucc_pln_Simulate
                                                   (Plant
                                                   ,WorkOrderID
                                                   ,CuttingLineID
                                                   ,MaterialTransLineID
                                                   ,SimSeq
                                                   ,Thick
                                                   ,Width
                                                   ,Length
                                                   ,LengthM
                                                   ,Status
                                                   ,Stand
                                                   ,CutDiv
                                                   ,UnitWeight
                                                   ,TotalWeight
                                                   ,CalculatedFlag
                                                   ,CreationDate
                                                   ,LastUpdateDate
                                                   ,CreatedBy
                                                   ,UpdatedBy
                                                   ,UsingLengthM)
                                             VALUES
                                                   ( N'{0}' --<Plant, nvarchar(8),>
                                                   , {1} --<WorkOrderID, bigint,>
                                                   , {2} --<CuttingLineID, bigint,>
                                                   , {3} --<MaterialTransLineID, bigint,>
                                                   , {4} --<SimSeq, int,>
                                                   , {5} --<Thick, decimal(20,9),>
                                                   , {6} --<Width, decimal(20,9),>
                                                   , {7} --<Length, decimal(20,9),>
                                                   , {8} --<LengthM, decimal(20,9),>
                                                   , N'{9}' --<Status, nvarchar(10),>
                                                   , {10} --<Stand, decimal(20,9),>
                                                   , {11} --<CutDiv, decimal(20,9),>
                                                   , {12} --<UnitWeight, decimal(20,9),>
                                                   , {13} --<TotalWeight, decimal(20,9),>
                                                   , {14} --<CalculatedFlag, tinyint,>
                                                   , GETDATE() --<CreationDate, datetime,>
                                                   , GETDATE() --<LastUpdateDate, datetime,>
                                                   , N'{15}' --<CreatedBy, nvarchar(45),>
                                                   , N'{15}' --<UpdatedBy, nvarchar(45),>
                                                   , {16}
                                                    )" + Environment.NewLine
                                                       , _session.PlantID
                                                       , head.WorkOrderID
                                                       , item.LineID
                                                       , item.TransactionLineID
                                                       , iRow
                                                       , item.Thick //{5}
                                                       , item.Width
                                                       , item.Length
                                                       , item.TotalLength
                                                       , item.Status
                                                       , item.Stand     //{10}
                                                       , j
                                                       , item.UnitWeight
                                                       , item.UnitWeight * item.Stand
                                                       , 0
                                                       , _session.UserID
                                                       , 0);

                    sql += string.Format(@"UPDATE ucc_pln_PlanHead SET SimulateFlag = 1 WHERE WorkOrderID = {0} ", item.WorkOrderID);

                    Repository.Instance.ExecuteWithTransaction(sql, "Insert Simulates");
                }
            }
            return GetSimulateAll(head.WorkOrderID);
        }

        public bool ClearSimulateLines(int workOrderID)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM ucc_pln_Simulate WHERE WorkOrderID = {0}", workOrderID);
                Repository.Instance.ExecuteWithTransaction(sql, "Delete Simulate");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<SimulateModel> GetSimulateAll(int workOrderID)
        {
            string sql = string.Format(@"SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                            , cut.NORNum, 1 as Quantity, cut.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                            , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                            , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                            , sim.*, mat.MCSSNo
                                            FROM ucc_pln_Simulate sim
	                                            LEFT JOIN ucc_pln_Material mat ON(sim.MaterialTransLineID = mat.TransactionLineID)
	                                            LEFT JOIN ucc_pln_CuttingDesign cut ON(sim.CuttingLineID = cut.LineID)
	                                            LEFT JOIN UD25 busi ON(cut.BussinessType = busi.Key1)
	                                            LEFT JOIN UD29 cmdt ON(cut.CommodityCode = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(cut.CommodityCode = spec.Key2 and cut.SpecCode = spec.Key1)
	                                            LEFT JOIN UD31 coat ON(cut.CoatingCode = coat.Key1)
                                            WHERE sim.WorkOrderID = {0}
                                            ORDER BY sim.CutDiv, sim.SimSeq ASC", workOrderID);
            return Repository.Instance.GetMany<SimulateModel>(sql);
        }

        public IEnumerable<SimulateModel> UpdateSimulateByWorkOrder(SessionInfo _session, IEnumerable<SimulateModel> model, int workComplete)
        {
            foreach (var item in model)
            {
                string sql = string.Format(@"UPDATE ucc_pln_Simulate
                                               SET Plant =  N'{0}' --<Plant, nvarchar(8),>
                                                  ,CuttingLineID =  {2} --<CuttingLineID, bigint,>
                                                  ,MaterialTransLineID =  {3} --<MaterialTransLineID, bigint,>
                                                  ,SimSeq =  {4} --<SimSeq, int,>
                                                  ,Thick =  {5} --<Thick, decimal(20,9),>
                                                  ,Width =  {6} --<Width, decimal(20,9),>
                                                  ,Length =  {7} --<Length, decimal(20,9),>
                                                  ,UsingLengthM =  {8} --<UsingLengthM, decimal(20,9),>
                                                  ,LengthM =  {9} --<LengthM, decimal(20,9),>
                                                  ,Status =  N'{10}' --<Status, nvarchar(10),>
                                                  ,Stand =  {11} --<Stand, decimal(20,9),>
                                                  ,CutDiv =  {12} --<CutDiv, decimal(20,9),>
                                                  ,UnitWeight =  {13} --<UnitWeight, decimal(20,9),>
                                                  ,TotalWeight =  {14} --<TotalWeight, decimal(20,9),>
                                                  ,CalculatedFlag =  {15} --<CalculatedFlag, tinyint,>
                                                  ,LastUpdateDate =  GETDATE() --<LastUpdateDate, datetime,>
                                                  ,UpdatedBy =  N'{16}' --<UpdatedBy, nvarchar(45),>
                                             WHERE SimLineID = {1}" + Environment.NewLine
                                                   , _session.PlantID
                                                   , item.SimLineID
                                                   , item.CuttingLineID
                                                   , item.TransactionLineID
                                                   , item.SimSeq
                                                   , item.Thick //{5}
                                                   , item.Width
                                                   , item.Length
                                                   , item.UsingLengthM
                                                   , item.LengthM
                                                   , item.Status        //{10}
                                                   , item.Stand
                                                   , item.CutDiv
                                                   , item.UnitWeight
                                                   , item.TotalWeight
                                                   , Convert.ToInt32(item.CalculatedFlag).GetInt()  //{15}
                                                   , _session.UserID);

                Repository.Instance.ExecuteWithTransaction(sql, "Insert Simulates");
            }

            var workOrderId = (from item in model
                               select item.WorkOrderID).First();

            string sql1 = string.Format(@"UPDATE ucc_pln_PlanHead SET Completed = {1} WHERE WorkOrderID = {0} ", workOrderId, workComplete);
            Repository.Instance.ExecuteWithTransaction(sql1, "Update Complete");

            return null;
        }

        public IEnumerable<CutDesignModel> UpdateCuttingByWorkOrder(SessionInfo _session, IEnumerable<SimulateModel> model, int workOrderID)
        {
            var cutLines = GetCuttingLines(workOrderID).Where(i => i.Status != "S");
            foreach (var item in cutLines)
            {
                var lineGrpSum = (from sim in model.Where(i => i.CuttingLineID.Equals(item.LineID))
                                  group sim by sim.CuttingLineID into simgrp
                                  select new
                                  {
                                      UnitWeight = (simgrp.Sum(x => x.TotalWeight) / simgrp.Max(x => x.CutDiv)) / simgrp.Max(x => x.Stand)
                                  ,
                                      TotalWeight = simgrp.Sum(x => x.TotalWeight)
                                  ,
                                      TotalLength = simgrp.Max(x => x.LengthM)
                                  ,
                                      TransactionLineID = simgrp.Max(x => x.TransactionLineID)
                                  }).FirstOrDefault();

                string sql = string.Format(@"UPDATE ucc_pln_CuttingDesign
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
                                                      ,TotalLength = {35}  --<TotalLength, decimal(20,9),>
                                                 WHERE LineID = {34}" + Environment.NewLine
                                                              , _session.CompanyID
                                                              , _session.PlantID
                                                              , item.WorkOrderID
                                                              , lineGrpSum.TransactionLineID.GetDecimal()         //Update Simulate
                                                              , item.CutSeq.GetString()
                                                              , item.SONo.GetString()           //{5}
                                                              , item.SOLine.GetInt()
                                                              , item.NORNum.GetString()
                                                              , item.CommodityCode.GetString()
                                                              , item.SpecCode.GetString()
                                                              , item.CoatingCode.GetString()    //{10}
                                                              , item.Thick
                                                              , item.Width
                                                              , item.Length
                                                              , item.Status.GetString()
                                                              , item.Stand      //{15}
                                                              , item.CutDivision
                                                              , lineGrpSum.UnitWeight.GetDecimal()       //Update Simulate
                                                              , lineGrpSum.TotalWeight.GetDecimal()       //Update Simulate
                                                              , item.CustID.GetString()
                                                              , item.EndUserCode.GetString()    //{20}
                                                              , item.DestinationCode.GetString()
                                                              , item.QtyPack
                                                              , item.Pack
                                                              , item.SOWeight
                                                              , item.SOQuantity     //{25}
                                                              , item.CalQuantity
                                                              , item.DeliveryDate.ToString("dd/MM/yyyy hh:mm:ss")
                                                              , item.BussinessType.GetString()
                                                              , item.Possession
                                                              , item.ProductStatus.GetInt()      //{30}
                                                              , item.Description.GetString()
                                                              , item.Note.GetString()
                                                              , _session.UserID
                                                              , item.LineID.GetInt()
                                                              , lineGrpSum.TotalLength.GetDecimal()       //Update Simulate
                                                              );

                Repository.Instance.ExecuteWithTransaction(sql, "Insert Simulates");
            }

            return GetCuttingLines(workOrderID);
        }

        public IEnumerable<MaterialModel> UpdateMaterialByWorkOrder(SessionInfo _session, IEnumerable<MaterialModel> model, int workOrderID)
        {
            foreach (var item in model)
            {
                string sql = string.Format(@"UPDATE ucc_pln_Material
                                               SET UsingWgt = {1} --<UsingWgt, decimal(20,9),>
                                                  ,RemainWgt = {2} --<RemainWgt, decimal(20,9),>
                                                  ,LenghtM = {3} --<LenghtM, decimal(20,9),>
                                                  ,UsingLM = {4} --<UsingLM, decimal(20,9),>
                                                  ,SelectCB = {5} --<SelectCB, tinyint,>
                                                  ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                  ,UpdatedBy = N'{6}' --<UpdatedBy, varchar(45),>
                                             WHERE TransactionLineID = {0}" + Environment.NewLine
                                                                            , item.TransactionLineID
                                                                            , item.UsingWeight
                                                                            , item.RemainWeight
                                                                            , item.LengthM
                                                                            , item.UsingLengthM
                                                                            , 0 //(item.RemainWeight > 0) ? 1 : 0
                                                                            , _session.UserID
                                                                            );

                Repository.Instance.ExecuteWithTransaction(sql, "Update Material");
            }

            return GetAllMaterial(_session.PlantID, workOrderID);
        }

        public IEnumerable<GeneratedSerialModel> GetSerialAllByWorkOrder(int workOrderID)
        {
            string sql = string.Format(@"SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                            , cut.NORNum, 1 as Quantity, cut.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                            , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                            , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                            , gsn.*, mat.MCSSNo
                                            FROM ucc_pln_SerialGenerated gsn
	                                            LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                            LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                            LEFT JOIN UD25 busi ON(cut.BussinessType = busi.Key1)
	                                            LEFT JOIN UD29 cmdt ON(cut.CommodityCode = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(cut.CommodityCode = spec.Key2 and cut.SpecCode = spec.Key1)
	                                            LEFT JOIN UD31 coat ON(cut.CoatingCode = coat.Key1)
                                            WHERE gsn.WorkOrderID = {0}", workOrderID);

            return Repository.Instance.GetMany<GeneratedSerialModel>(sql);
        }

        public IEnumerable<GeneratedSerialModel> GenerateSerial(SessionInfo _session, IEnumerable<SimulateModel> model, int workOrderID)
        {
            int iRunning = 1;
            foreach (var item in model)
            {
                for (int j = 1; j <= item.Stand; j++)
                {
                    //int iRunning = RunningLot();
                    string LotNum = item.MaterialSerialNo + '-' + iRunning.ToString();//GetSerialByFormat(iRunning);
                    string sql = string.Format(@"INSERT INTO ucc_pln_SerialGenerated
                                                       (Plant
                                                       ,SimLineID
                                                       ,WorkOrderID
                                                       ,CuttingLineID
                                                       ,MaterialTransLineID
                                                       ,Thick
                                                       ,Width
                                                       ,Length
                                                       ,LengthM
                                                       ,Status
                                                       ,UnitWeight
                                                       ,Quantity
                                                       ,TotalWeight
                                                       ,GeneratedFlag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,LotRunning
                                                       ,SerialNo)
                                                 VALUES
                                                       (N'{0}'  --<Plant, nvarchar(8),>
                                                       ,{1}  --<SimLineID, bigint,>
                                                       ,{2}  --<WorkOrderID, bigint,>
                                                       ,{3}  --<CuttingLineID, bigint,>
                                                       ,{4}  --<MaterialTransLineID, bigint,>
                                                       ,{5}  --<Thick, decimal(20,9),>
                                                       ,{6}  --<Width, decimal(20,9),>
                                                       ,{7}  --<Length, decimal(20,9),>
                                                       ,{8}  --<LengthM, decimal(20,9),>
                                                       ,N'{9}'  --<Status, nvarchar(10),>
                                                       ,{10}  --<UnitWeight, decimal(20,9),>
                                                       ,{11}  --<Quantity, decimal(20,9),>
                                                       ,{12}  --<TotalWeight, decimal(20,9),>
                                                       ,{13}  --<GeneratedFlag, tinyint,>
                                                       ,GETDATE()  --<CreationDate, datetime,>
                                                       ,GETDATE()  --<LastUpdateDate, datetime,>
                                                       ,N'{14}'  --<CreatedBy, nvarchar(45),>
                                                       ,N'{14}'  --<UpdatedBy, nvarchar(45),>
                                                       ,{15}  --<LotRunning, bigint,>
                                                       ,N'{16}'
                                                       )" + Environment.NewLine
                                                      , _session.PlantID
                                                      , item.SimLineID
                                                      , item.WorkOrderID
                                                      , item.CuttingLineID
                                                      , item.TransactionLineID
                                                      , item.Thick          //{5}
                                                      , item.Width
                                                      , item.Length
                                                      , item.LengthM
                                                      , item.Status
                                                      , item.UnitWeight     //{10}
                                                      , item.Quantity
                                                      , item.TotalWeight
                                                      , 0
                                                      , _session.UserID
                                                      , iRunning
                                                      , LotNum
                                                      );

                    Repository.Instance.ExecuteWithTransaction(sql, "Insert Simulates");
                    iRunning++;
                }
            }
            string sql1 = string.Format(@"UPDATE ucc_pln_PlanHead SET GenSerialFlag = 1 WHERE WorkOrderID = {0} ", workOrderID);
            Repository.Instance.ExecuteWithTransaction(sql1, "Update Complete");

            return GetSerialAllByWorkOrder(workOrderID);
        }

        public string GetSerialByFormat(int StartId)
        {
            return (DateTime.Now.ToString("yy") + Enum.GetName(typeof(Month), int.Parse(DateTime.Now.ToString("MM"))) + StartId.ToString("00000"));
        }

        public int RunningLot()
        {
            string sql = "SELECT TOP 1 * FROM ucc_pln_SerialGenerated ORDER BY LotRunning DESC";

            return Repository.Instance.GetOne<int>(sql, "LotRunning") + 1;
        }

        public bool UnConfirmWork(int workOrderID)
        {
            try
            {
                string sql1 = string.Format(@"UPDATE ucc_pln_PlanHead SET Completed = 0 WHERE WorkOrderID = {0} ", workOrderID);
                Repository.Instance.ExecuteWithTransaction(sql1, "Update Complete");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ImportSerialToEpicor(SessionInfo _session, PlanningHeadModel model, out string msg)
        {
            msg = string.Empty;
            //bool IsSuccess = false;
            Session currSession;
            var resultContinue = GetSerialAllByWorkOrder(model.WorkOrderID).ToList().Where(i => i.Status.Equals("C"));
            try
            {
                currSession = new Session(_session.UserID, _session.UserPassword, _session.AppServer, Session.LicenseType.Default);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            foreach (var item in resultContinue)
            {
                try
                {
                    LotSelectUpdate lotPart = new LotSelectUpdate(currSession.ConnectionPool);
                    LotSelectUpdateDataSet dsLot = new LotSelectUpdateDataSet();
                    lotPart.GetNewPartLot(dsLot, item.MCSSNo);

                    DataRow drLot = dsLot.Tables[0].Rows[0];
                    drLot.BeginEdit();

                    drLot["LotNum"] = item.SerialNo;
                    drLot["PartLotDescription"] = item.SerialNo;
                    drLot["Number01"] = item.Thick;
                    drLot["Number02"] = item.Width;
                    drLot["Number03"] = item.Length;
                    drLot["Number04"] = item.UnitWeight;
                    drLot["Number05"] = 1;
                    drLot["Number08"] = 0;
                    drLot["Character02"] = item.WorkOrderID.ToString();
                    drLot["Character02"] = item.WorkOrderID.ToString();
                    //drLot[""] = item.Thick;
                    //drLot[""] = item.Thick;
                    //drLot[""] = item.Thick;
                    //drLot[""] = item.Thick;
                    //drLot[""] = item.Thick;

                    drLot.EndEdit();
                    lotPart.Update(dsLot);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return false;
                }
            }

            return true;
        }

        public int GetStepByWorkOrder(int workOrderID)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_pln_PlanHead WHERE WorkOrderID = {0} ORDER BY ProcessStep DESC", workOrderID);

            return Repository.Instance.GetOne<int>(sql, "ProcessStep") + 1;
        }
    }
}