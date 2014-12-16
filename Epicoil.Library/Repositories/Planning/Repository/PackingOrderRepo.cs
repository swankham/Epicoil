using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Production;
using Epicoil.Library.Repositories.StoreIn;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories.Planning
{
    public class PackingOrderRepo : IPackingOrderRepo
    {
        #region Fields

        private readonly IClassMasterRepo _repoCls;
        private readonly IStoreInRepo _repoIn;
        private readonly IResourceRepo _repoResrc;
        private readonly ICoilBackRuleRepo _repoRule;
        private readonly IUserCodeRepo _repoUcode;
        private readonly IWorkEntryRepo _repoWork;
        private readonly IProductionRepo _repoProd;

        #endregion Fields

        #region Constructors

        public PackingOrderRepo()
        {
            this._repoUcode = new UserCodeRepo();
            this._repoResrc = new ResourceRepo();
            this._repoCls = new ClassMasterRepo();
            this._repoRule = new CoilBackRuleRepo();
            this._repoIn = new StoreInRepo();
            this._repoWork = new WorkEntryRepo();
            this._repoProd = new ProductionRepo();
        }

        #endregion Constructors

        #region Methods

        #region WorkOrders from Planning

        public IEnumerable<PlanningHeadModel> GetWorkAll(PlanningHeadModel model)
        {
            string whereCluase = string.Empty;
            if (model.Completed != 0) whereCluase = string.Format(@"AND Completed = {0}", model.Completed);
            if (model.GenSerialFlag != 0) whereCluase += string.Format(@"AND GenSerialFlag = {0}", model.GenSerialFlag);
            if (model.OperationState != 0) whereCluase += string.Format(@"AND OperationState = {0}", model.OperationState);

            string sql = string.Format(@"SELECT uf.Name as PICName, busi.Character01 as BussinessTypeName, plh.*
                                        FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
		                                    LEFT JOIN UD25 busi ON(plh.BT = busi.Key1)
                                            WHERE plh.Plant = N'{0}' AND PackingOrderFlag = 0 {1}", model.Plant, whereCluase);

            var result = Repository.Instance.GetMany<PlanningHeadModel>(sql);
            return result;
        }

        #endregion WorkOrders from Planning

        #region Packing Order header

        public PackingOrderModel GetPackOrderByID(int workOrderId)
        {
            string sql = string.Format(@"SELECT pkh.*, pln.WorkOrderNum, pln.DueDate, pln.IssueDate
                                            FROM ucc_pkg_Header pkh
                                            INNER JOIN ucc_pln_PlanHead pln ON(pkh.WorkOrderID = pln.WorkOrderID)
                                            WHERE pkh.WorkOrderID = {0}", workOrderId);

            var result = Repository.Instance.GetOne<PackingOrderModel>(sql);

            if (result != null)
            {
                result.SerialLines = GetSerialByStyleID(result.Id).ToList();
                result.PackStyles = GetPackStyleByPackOrder(result.WorkOrderId).ToList();
                result.SkidPacks = GetSkidPackingsByWork(result.Id).ToList();
            }
            return result;
        }

        public bool PackOrderExist(int workOrderId)
        {
            string sql = string.Format(@"SELECT pkh.*, pln.WorkOrderNum, pln.DueDate, pln.IssueDate
                                            FROM ucc_pkg_Header pkh
                                            INNER JOIN ucc_pln_PlanHead pln ON(pkh.WorkOrderID = pln.WorkOrderID)
                                            WHERE pkh.WorkOrderID = {0}", workOrderId);

            var result = Repository.Instance.GetMany<PackingOrderModel>(sql);
            return (result.ToList().Count > 0);
        }

        public bool SavePackOrder(SessionInfo _session, PackingOrderModel model, out PackingOrderModel resultRow)
        {
            bool isSuccess = false;
            resultRow = null;
            string sql = string.Format(@"IF NOT EXISTS
                                        (
	                                        SELECT * FROM ucc_pkg_Header (NOLOCK)
	                                        WHERE WorkOrderID = {2}
                                        )
	                                        BEGIN
		                                        INSERT INTO ucc_pkg_Header
				                                           (Plant
				                                           ,PackOrderNum
				                                           ,WorkOrderID
				                                           ,Remarks
				                                           ,CompleteFlag
				                                           ,CreationDate
				                                           ,LastUpdateDate
				                                           ,CreatedBy
				                                           ,UpdatedBy)
			                                         VALUES
				                                           (N'{0}' --<Plant, nvarchar(8),>
				                                           ,N'{1}' --<PackOrderNum, nvarchar(45),>
				                                           ,{2} --<WorkOrderID, bigint,>
				                                           ,N'{3}' --<Remarks, nvarchar(max),>
				                                           ,{4} --<CompleteFlag, int,>
				                                           ,GETDATE() --<CreationDate, datetime,>
				                                           ,GETDATE() --<LastUpdateDate, datetime,>
				                                           ,N'{5}' --<CreatedBy, nvarchar(45),>
				                                           ,N'{5}' --<UpdatedBy, nvarchar(45),>)
				                                           )
	                                        END
                                        ELSE
	                                        BEGIN
		                                        UPDATE ucc_pkg_Header
		                                           SET Remarks = N'{3}' --<Remarks, nvarchar(max),>
			                                          ,CompleteFlag = {4} --<CompleteFlag, int,>
			                                          ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
			                                          ,UpdatedBy = N'{5}' --<UpdatedBy, nvarchar(45),>
		                                         WHERE WorkOrderID = {2}
	                                         END
                                        " + Environment.NewLine
                                         , _session.PlantID
                                         , model.PackOrderNum
                                         , model.WorkOrderId
                                         , model.Remark
                                         , model.CompleteFlag
                                         , _session.UserID);
            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Update Planning");
                isSuccess = true;
                resultRow = GetPackOrderByID(model.WorkOrderId);
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        #endregion Packing Order header

        #region Packing Style

        public IEnumerable<PackStyleOrderModel> GetPackStyleByWorkOrder(int workOrderId, int headerId = 0)
        {
            string sql = string.Format(@"SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                        , cut.NORNum, 1 as Quantity, mat.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                        , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , odt.Character01 as CustId, cust.Name as CustomerName, nor.ShortChar11 as StyleCode, Count(gsn.LineID) as TotalQuantity
                                            , {1} as HeadLineID, min(gsn.Thick) as ThickMin, max(gsn.Thick) as ThickMax, min(gsn.Width) as WidthMin, max(gsn.Width) as WidthMax, '' as Remarks, '' as StyleName, 0 as LineID
                                            , nor.Number06 as CoilWeigthPackMin, nor.Number07 as CoilWeigthPackMax, nor.Number08 as CoilPerPackMin, nor.Number09 as CoilPerPackMax
                                        FROM ucc_pln_SerialGenerated gsn
	                                        LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                        LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                        LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                        LEFT JOIN UD25 busi ON(mat.BT = busi.Key1)
	                                        LEFT JOIN UD29 cmdt ON(mat.Cmdty = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(mat.Cmdty = spec.Key2 and mat.Spec = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(mat.Coating = coat.Key1)
	                                        LEFT JOIN OrderDtl odt ON(cast(cut.SONo as int) = odt.OrderNum AND odt.OrderLine = cut.SOLine)
	                                        LEFT JOIN Customer cust ON(odt.Character01 = cust.CustID)
	                                        LEFT JOIN UD12 nor ON(cut.NORNum = nor.Key1)
                                        WHERE ProductionUsedFlag = 0
	                                        AND gsn.WorkOrderID = {0}
	                                        AND gsn.Status = 'F'
                                        GROUP BY
	                                        mat.MCSSNo, mat.Serial, cut.SONo, cut.SOLine
	                                        , cut.NORNum, mat.Possession, busi.Key1, busi.Character01
	                                        , cmdt.Key1, cmdt.Character01
	                                        , spec.Key1, spec.Character01, spec.Number01
	                                        , coat.Key1, ISNULL(coat.Character01, '') , ISNULL(coat.Number01, 0.00), ISNULL(coat.Number02, 0.00)
	                                        , odt.Character01, cust.Name, nor.ShortChar11, nor.Number06, nor.Number07, nor.Number08, nor.Number09", workOrderId, headerId);

            var result = Repository.Instance.GetMany<PackStyleOrderModel>(sql);
            return result;
        }

        public bool SavePackStyles(SessionInfo _session, IEnumerable<PackStyleOrderModel> packStyles = null, PackStyleOrderModel model = null)
        {
            bool isSuccess = false;
            string sql = string.Empty;
            //resultRow = null;
            if (packStyles != null && model == null)
            {
                foreach (var item in packStyles)
                {
                    sql += string.Format(@"IF NOT EXISTS
                                        (
	                                        SELECT * FROM ucc_pkg_PackStyleList (NOLOCK)
	                                        WHERE HeadLineID = {1} AND StyleCode = N'{3}'
                                        )
	                                        BEGIN
		                                        INSERT INTO ucc_pkg_PackStyleList
				                                            (Plant
				                                            ,HeadLineID
				                                            ,CustID
				                                            ,StyleCode
				                                            ,TotalQuantity
				                                            ,ThickMin
				                                            ,ThickMax
				                                            ,WidthMin
				                                            ,WidthMax
				                                            ,Remarks
				                                            ,CompleteFlag
				                                            ,CreationDate
				                                            ,LastUpdateDate
				                                            ,CreatedBy
				                                            ,UpdatedBy
                                                            ,CoilWeigthPackMin
                                                            ,CoilWeigthPackMax
                                                            ,CoilPerPackMin
                                                            ,CoilPerPackMax)
			                                            VALUES
				                                            (N'{0}' --<Plant, nvarchar(8),>
				                                            ,{1} --<HeadLineID, bigint,>
				                                            ,N'{2}' --<CustID, nvarchar(25),>
				                                            ,N'{3}' --<StyleCode, nvarchar(45),>
				                                            ,{4} --<TotalQuantity, int,>
				                                            ,{5} --<ThickMin, decimal(15,9),>
				                                            ,{6} --<ThickMax, decimal(15,9),>
				                                            ,{7} --<WidthMin, decimal(15,9),>
				                                            ,{8} --<WidthMax, decimal(15,9),>
				                                            ,N'{9}' --<Remarks, nvarchar(max),>
				                                            ,{10} --<CompleteFlag, int,>
				                                            ,GETDATE() --<CreationDate, datetime,>
				                                            ,GETDATE() --<LastUpdateDate, datetime,>
				                                            ,N'{11}' --<CreatedBy, nvarchar(45),>
				                                            ,N'{11}' --<UpdatedBy, nvarchar(45),>
                                                            ,{12}
                                                            ,{13}
                                                            ,{14}
                                                            ,{15}
				                                            )
	                                        END
                                        ELSE
	                                        BEGIN
		                                        UPDATE ucc_pkg_PackStyleList
			                                        SET Remarks = N'{9}' --<Remarks, nvarchar(max),>
                                                       ,CompleteFlag = {10} --<CompleteFlag, int,>
				                                       ,HeadLineID = {1} --<HeadLineID, bigint,>
				                                       ,CustID = N'{2}' --<CustID, nvarchar(25),>
				                                       ,StyleCode = N'{3}' --<StyleCode, nvarchar(45),>
				                                       ,TotalQuantity = {4} --<TotalQuantity, int,>
				                                       ,ThickMin = {5} --<ThickMin, decimal(15,9),>
				                                       ,ThickMax = {6} --<ThickMax, decimal(15,9),>
				                                       ,WidthMin = {7} --<WidthMin, decimal(15,9),>
				                                       ,WidthMax = {8} --<WidthMax, decimal(15,9),>				                                       				                                        
				                                       ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
				                                       ,UpdatedBy = N'{11}' --<UpdatedBy, nvarchar(45),>
				                                       ,CoilWeigthPackMin = {12} --<CoilWeigthPackMin, decimal(15,9),>
				                                       ,CoilWeigthPackMax = {13} --<CoilWeigthPackMax, decimal(15,9),>
				                                       ,CoilPerPackMin = {14} --<CoilPerPackMin, decimal(15,9),>
				                                       ,CoilPerPackMax = {15} --<CoilPerPackMax, decimal(15,9),>
			                                        WHERE HeadLineID = {1} AND StyleCode = N'{3}'
	                                        END
                                            " + Environment.NewLine
                                                     , _session.PlantID
                                                     , item.HeadLineID
                                                     , item.CustId
                                                     , item.StyleCode
                                                     , item.TotalQuantity
                                                     , item.ThickMin
                                                     , item.ThickMax
                                                     , item.WidthMin
                                                     , item.WidthMax
                                                     , item.Remarks
                                                     , 0
                                                     , _session.UserID
                                                     , item.CoilWeigthPackMin
                                                     , item.CoilWeigthPackMax
                                                     , item.CoilPerPackMin
                                                     , item.CoilPerPackMax);
                }
            }
            else if (packStyles == null && model != null)
            {
                sql = string.Format(@"UPDATE ucc_pkg_PackStyleList
			                            SET Remarks = N'{2}' --<Remarks, nvarchar(max),>
                                            ,CompleteFlag = {3} --<CompleteFlag, int,>
				                            ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
				                            ,UpdatedBy = N'{4}' --<UpdatedBy, nvarchar(45),>
			                            WHERE HeadLineID = {0} AND StyleCode = N'{1}'" + Environment.NewLine
                                        , model.HeadLineID
                                        , model.StyleCode
                                        , model.Remarks
                                        , 0
                                        , _session.UserID);
            }

            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Update Planning");
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public IEnumerable<PackStyleOrderModel> GetPackStyleByPackOrder(int workOrderId)
        {
            string sql = string.Format(@"SELECT cust.Name as CustomerName, pks.CodeNum as StyleName
	                                         , stl.*
                                          FROM ucc_pkg_PackStyleList stl
                                          INNER JOIN ucc_pkg_Header pkh ON(pkh.LineID = stl.HeadLineID)
                                          LEFT JOIN Customer cust ON(stl.CustID = cust.CustID)
                                          LEFT JOIN ucc_tqa_PackingStyle pks ON(stl.StyleCode = pks.CodeNum)
                                          WHERE pkh.WorkOrderID = {0}", workOrderId);

            return Repository.Instance.GetMany<PackStyleOrderModel>(sql);
        }

        #endregion Packing Style

        #region Serial Packing

        public IEnumerable<SerialsPackingModel> GetSerialForFirstDefault(PackStyleOrderModel model)
        {
            string sql = string.Format(@"SELECT mat.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                            , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                            , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
                                                , pkg.LineID as HeadLineID, gsn.UnitWeight
	                                            , gsn.LineID as SerialLineID, gsn.SerialNo, gsn.Thick, gsn.Width, gsn.Length
	                                            , 0 as LineID, {3} as PackStyleLineID, N'{2}' as PackingStyleCode, 0 as PackingDesignId, '' as SkidNumber, 0 as CompleteFlag
                                            FROM ucc_pln_SerialGenerated gsn
	                                            INNER JOIN ucc_pkg_Header pkg ON(gsn.WorkOrderID = pkg.WorkOrderID)
	                                            LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                            LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                            LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                            LEFT JOIN UD25 busi ON(mat.BT = busi.Key1)
	                                            LEFT JOIN UD29 cmdt ON(mat.Cmdty = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(mat.Cmdty = spec.Key2 and mat.Spec = spec.Key1)
	                                            LEFT JOIN UD31 coat ON(mat.Coating = coat.Key1)
	                                            LEFT JOIN OrderDtl odt ON(cast(cut.SONo as int) = odt.OrderNum AND odt.OrderLine = cut.SOLine)
	                                            LEFT JOIN UD12 nor ON(cut.NORNum = nor.Key1)
                                            WHERE ProductionUsedFlag = 0
	                                            AND gsn.Status = 'F'
	                                            AND pkg.LineID = {0}	--{0} //HeadLineId
	                                            AND odt.Character01 = N'{1}'	--N'{1}'	//CustId
	                                            AND nor.ShortChar11	= N'{2}'	--N'{2}'	//StyleCode  CS-0002,CU-0001", model.HeadLineID, model.CustId, model.StyleCode, model.Id);

            return Repository.Instance.GetMany<SerialsPackingModel>(sql);
        }

        public bool SaveSerialByStyle(SessionInfo _session, IEnumerable<SerialsPackingModel> models)
        {
            bool isSuccess = false;
            string sql = string.Empty;
            foreach (var item in models)
            {
                sql += string.Format(@"IF NOT EXISTS
                                        (
	                                        SELECT * FROM ucc_pkg_SerialsPacking (NOLOCK)
	                                        WHERE HeadLineID = {1} AND PackStyleLineID = {2} AND SerialLineID = {3}
                                        )
	                                        BEGIN
                                                INSERT INTO ucc_pkg_SerialsPacking
                                                           (Plant
                                                           ,HeadLineID
                                                           ,PackStyleLineID
                                                           ,SerialLineID
                                                           ,PackingDesignId
                                                           ,SkidNumber
                                                           ,CompleteFlag
                                                           ,CreationDate
                                                           ,LastUpdateDate
                                                           ,CreatedBy
                                                           ,UpdatedBy)
                                                     VALUES
                                                           (N'{0}' --<Plant, nvarchar(8),>
                                                           ,{1} --<HeadLineID, bigint,>
                                                           ,{2} --<PackStyleLineID, bigint,>
                                                           ,{3} --<SerialLineID, int,>
                                                           ,{4} --<PackingDesignId, int,>
                                                           ,N'{5}' --<SkidNumber, nvarchar(45),>
                                                           ,{6} --<CompleteFlag, int,>
                                                           ,GETDATE() --<CreationDate, datetime,>
                                                           ,GETDATE() --<LastUpdateDate, datetime,>
                                                           ,N'{7}' --<CreatedBy, nvarchar(45),>
                                                           ,N'{7}' --<UpdatedBy, nvarchar(45),>
				                                           )
	                                        END
                                        ELSE
	                                        BEGIN
                                                UPDATE ucc_pkg_SerialsPacking
                                                   SET Plant = N'{0}' --<Plant, nvarchar(8),>
                                                      ,HeadLineID = {1} --<HeadLineID, bigint,>
                                                      ,PackStyleLineID = {2} --<PackStyleLineID, bigint,>
                                                      ,SerialLineID = {3} --<SerialLineID, int,>
                                                      ,PackingDesignId = {4} --<PackingDesignId, int,>
                                                      ,SkidNumber = N'{5}' --<SkidNumber, nvarchar(45),>
                                                      ,CompleteFlag = {6} --<CompleteFlag, int,>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,UpdatedBy = N'{7}' --<UpdatedBy, nvarchar(45),>
                                                 WHERE HeadLineID = {1} AND PackStyleLineID = {2} AND SerialLineID = {3}
	                                         END
                                        " + Environment.NewLine
                             , _session.PlantID
                             , item.HeadLineId
                             , item.PackStyleId
                             , item.SerialLineId
                             , item.PackingDesignId    //Skid line id
                             , item.SkidNumber
                             , item.CompleteFlag
                             , _session.UserID);
            }

            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Save Serial Packing");
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public IEnumerable<SerialsPackingModel> GetSerialByStyleID(int headerId, int styleLineId = 0)
        {
            string wherecluase = string.Empty;

            if (styleLineId != 0) wherecluase = string.Format(@" AND snpk.PackStyleLineID = {0}", styleLineId);
            string sql = string.Format(@"SELECT mat.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                            , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                            , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
                                                , pkg.LineID as HeadLineID, gsn.UnitWeight
	                                            , gsn.LineID as SerialLineID, gsn.SerialNo, gsn.Thick, gsn.Width, gsn.Length
	                                            , snpk.LineID, snpk.PackStyleLineID, pks.StyleCode as PackingStyleCode, snpk.PackingDesignId, snpk.SkidNumber, snpk.CompleteFlag
                                            FROM ucc_pkg_SerialsPacking snpk
		                                        INNER JOIN ucc_pln_SerialGenerated gsn ON(snpk.SerialLineID = gsn.LineID)
		                                        INNER JOIN ucc_pkg_PackStyleList pks ON(snpk.PackStyleLineID = pks.LineID)
	                                            INNER JOIN ucc_pkg_Header pkg ON(gsn.WorkOrderID = pkg.WorkOrderID)
	                                            LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                            LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                            LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                            LEFT JOIN UD25 busi ON(mat.BT = busi.Key1)
	                                            LEFT JOIN UD29 cmdt ON(mat.Cmdty = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(mat.Cmdty = spec.Key2 and mat.Spec = spec.Key1)
	                                            LEFT JOIN UD31 coat ON(mat.Coating = coat.Key1)
	                                            LEFT JOIN OrderDtl odt ON(cast(cut.SONo as int) = odt.OrderNum AND odt.OrderLine = cut.SOLine)
	                                            LEFT JOIN UD12 nor ON(cut.NORNum = nor.Key1)
                                            WHERE pkg.LineID = {0} {1}", headerId, wherecluase);

            return Repository.Instance.GetMany<SerialsPackingModel>(sql);
        }

        #endregion Serial Packing

        #region Skid Packing

        public IEnumerable<SkidPackingModel> GetSkidPackingsByWork(int headLineId)
        {
            string sql = string.Format(@"SELECT * FROM ucc_pkg_SkidDesignPacking
                                            WHERE HeadLineID = {0}", headLineId);

            return Repository.Instance.GetMany<SkidPackingModel>(sql);
        }

        public IEnumerable<SkidPackingModel> AddSkidPacking(SessionInfo _session, SkidPackingModel pack)
        {
            string sql = string.Empty;
            sql += string.Format(@"INSERT INTO ucc_pkg_SkidDesignPacking
                                                   (Plant
                                                   ,HeadLineID
                                                   ,PackStyleLineID
                                                   ,Seq
                                                   ,SkidNumber
                                                   ,Description
                                                   ,CompleteFlag
                                                   ,CreationDate
                                                   ,LastUpdateDate
                                                   ,CreatedBy
                                                   ,UpdatedBy)
                                             VALUES
                                                   (N'{0}' --<Plant, nvarchar(8),>
                                                   ,{1} --<HeadLineID, int,>
                                                   ,{2} --<PackStyleLineID, int,>
                                                   ,{3} --<Seq, int,>
                                                   ,N'{4}' --<SkidNumber, nvarchar(45),>
                                                   ,N'{5}' --<Description, nvarchar(100),>
                                                   ,{6} --<CompleteFlag, int,>
                                                   ,GETDATE() --<CreationDate, datetime,>
                                                   ,GETDATE() --<LastUpdateDate, datetime,>
                                                   ,N'{7}' --<CreatedBy, nvarchar(45),>
                                                   ,N'{7}' --<UpdatedBy, nvarchar(45),>
				                             )" + Environment.NewLine
                                             , _session.PlantID
                                             , pack.HeadLineID
                                             , pack.PackStyleId
                                             , pack.Seq
                                             , pack.SkidNumber
                                             , pack.Description
                                             , 0
                                             , _session.UserID);

            Repository.Instance.ExecuteWithTransaction(sql, "Add Skid");
            return GetSkidPackingsByWork(pack.HeadLineID);
        }

        public IEnumerable<SkidPackingModel> DeleteSkidPacking(SessionInfo _session, int headLineId, int lineId, int serialLineId = 0)
        {
            string sql = string.Empty;
            if (serialLineId == 0)
            {
                sql = string.Format(@"DELETE FROM ucc_pkg_SkidDesignPacking
                                   WHERE LineID = {0}" + Environment.NewLine, lineId);

                sql += string.Format(@"UPDATE ucc_pkg_SerialsPacking
                                           SET PackingDesignId = 0
                                              ,LastUpdateDate = GETDATE()
                                              ,UpdatedBy = N'{0}'
                                         WHERE PackingDesignId = {1}", _session.UserID, lineId);
            }
            else if (serialLineId > 0)
            {
                sql = string.Format(@"UPDATE ucc_pkg_SerialsPacking
                                           SET PackingDesignId = 0
                                              ,LastUpdateDate = GETDATE()
                                              ,UpdatedBy = N'{0}'
                                         WHERE SerialLineID = {1}", _session.UserID, serialLineId);
            }

            Repository.Instance.ExecuteWithTransaction(sql, "Delete Skid Pack");
            return GetSkidPackingsByWork(headLineId);
        }

        public IEnumerable<SkidPackingModel> AddSerialToSkid(SessionInfo _session, int headId, int packid, string serials)
        {
             string sql = string.Format(@"UPDATE ucc_pkg_SerialsPacking
                                           SET PackingDesignId = {2}
                                              ,LastUpdateDate = GETDATE()
                                              ,UpdatedBy = N'{0}'
                                         WHERE SerialLineID IN ({1})", _session.UserID, serials, packid);

            Repository.Instance.ExecuteWithTransaction(sql, "Add Skid Pack");
            return GetSkidPackingsByWork(headId);
        }

        #endregion Skid Packing

        #endregion Methods
    }
}