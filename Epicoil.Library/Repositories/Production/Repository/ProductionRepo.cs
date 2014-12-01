using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories.Production
{
    public class ProductionRepo : IProductionRepo
    {
        private readonly IWorkEntryRepo _repoPln;
        private readonly IResourceRepo _repoRes;
        private readonly IUserCodeRepo _repoUcd;

        public ProductionRepo()
        {
            this._repoPln = new WorkEntryRepo();
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
        }

        public IEnumerable<ReasonModel> GetAllReasonAll()
        {
            string sql = @"SELECT * FROM ucc_prd_Reason ORDER BY StopCode ASC";
            return Repository.Instance.GetMany<ReasonModel>(sql);
        }

        public IEnumerable<SerialCuttingModel> GetAllSerialByProduction(int workOrderID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SerialCuttingModel> GetAllSerialByWorkOrder(int workOrderID)
        {
            string sql = string.Format(@"SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                                , cut.NORNum, 1 as Quantity, mat.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                                , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                                , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                                , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate	    
		                                            , mat.MCSSNo, pln.WorkOrderNum, 0 as ProductionID, 0 as LengthActual, 0 as WeightActual, 0 as NGFlag
		                                            , gsn.*
                                                FROM ucc_pln_SerialGenerated gsn
	                                                LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                                LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                                LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                                LEFT JOIN UD25 busi ON(mat.BT = busi.Key1)
	                                                LEFT JOIN UD29 cmdt ON(mat.Cmdty = cmdt.Key1)
	                                                LEFT JOIN UD30 spec ON(mat.Cmdty = spec.Key2 and mat.Spec = spec.Key1)
	                                                LEFT JOIN UD31 coat ON(mat.Coating = coat.Key1)
                                                WHERE ProductionUsedFlag = 0 AND gsn.WorkOrderID = {0}
                                            UNION ALL
                                            SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                                , cut.NORNum, 1 as Quantity, mat.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                                , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                                , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                                , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                                , mat.MCSSNo, pln.WorkOrderNum, gsn.ProductionID, gsn.ActualLengthM as LengthActual, gsn.ActualWeight as WeightActual, gsn.NGFlag, gsn.Plant
		                                            , gsn.FinLineID as LineID, gsn.SerialNo as SerialID, 0 as SimLineID, gsn.WorkOrderID, gsn.CuttingLineID, gsn.MaterialTransLineID, gsn.Thick, gsn.Width, gsn.Length
		                                            , gsn.PlanLengthM as LengthM, gsn.Status, gsn.CutSeq, gsn.PlanWeight as UnitWeight, gsn.Quantity, gsn.TotalWeight, 0 as GeneratedFlag, gsn.CreationDate, gsn.LastUpdateDate
		                                            , gsn.CreatedBy, gsn.UpdatedBy, 0 as LotRunning, 0 as ProductionUsedFlag
                                                FROM ucc_prd_FinishJobBySerial gsn
	                                                LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                                LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                                LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                                LEFT JOIN UD25 busi ON(mat.BT = busi.Key1)
	                                                LEFT JOIN UD29 cmdt ON(mat.Cmdty = cmdt.Key1)
	                                                LEFT JOIN UD30 spec ON(mat.Cmdty = spec.Key2 and mat.Spec = spec.Key1)
	                                                LEFT JOIN UD31 coat ON(mat.Coating = coat.Key1)
                                                WHERE gsn.WorkOrderID = {0}
                                            ORDER BY MaterialTransLineID , CutSeq ASC", workOrderID);

            return Repository.Instance.GetMany<SerialCuttingModel>(sql);
        }

        public IEnumerable<CuttedLineUpModel> GetCutLineUpAll(int workOrderId)
        {
            string sql = string.Format(@"SELECT * FROM ucc_prd_CuttingLineUp
                                            WHERE WorkOrderID = {0}", workOrderId);

            return Repository.Instance.GetMany<CuttedLineUpModel>(sql);
        }

        public ProductionHeadModel GetProdHead(int workOrderId)
        {
            string sql = string.Format(@"SELECT prd.*, pln.WorkOrderNum, pln.OperationState
                                            FROM ucc_prd_ProductionHead prd
                                            INNER JOIN ucc_pln_PlanHead pln ON(prd.WorkOrderId = pln.WorkOrderId)
                                            WHERE prd.WorkOrderID = {0}", workOrderId);

            var result = Repository.Instance.GetOne<ProductionHeadModel>(sql);

            result.ProcessLines = _repoRes.GetAll(result.Plant).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S")).ToList();
            result.ProcessLineID = result.ProcessLineID;
            result.SerialLines = GetAllSerialByWorkOrder(workOrderId).ToList();
            result.Materials = _repoPln.GetAllMaterial(result.Plant, workOrderId).ToList();
            result.Reasons = GetAllReasonAll().ToList();
            result.Cutteds = GetCutLineUpAll(workOrderId).ToList();
            return result;
        }

        public IEnumerable<CuttedLineUpModel> SaveCuttedLineUp(SessionInfo _session, CuttedLineUpModel model)
        {
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_prd_CuttingLineUp (NOLOCK)
										    WHERE WorkOrderID = {1} AND CutSeq = {2}
									    )
                                        BEGIN
                                            INSERT INTO ucc_prd_CuttingLineUp
                                                       (ProductionID
                                                       ,WorkOrderID
                                                       ,CutSeq
                                                       ,StartTime
                                                       ,FinishTime
                                                       ,CompleteFlag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,MaterialTransLineID)
                                                 VALUES
                                                       ({0} --<ProductionID, bigint,>
                                                       ,{1} --<WorkOrderID, bigint,>
                                                       ,{2} --<CutSeq, decimal(5,1),>
                                                       ,GETDATE() --<StartTime, datetime,>
                                                       ,GETDATE() --<FinishTime, datetime,>
                                                       ,{3} --<CompleteFlag, int,>
                                                       ,GETDATE() --<CreationDate, datetime,>
                                                       ,GETDATE() --<LastUpdateDate, datetime,>
                                                       ,N'{4}' --<CreatedBy, varchar(45),>
                                                       ,N'{4}' --<UpdatedBy, varchar(45),>
                                                       ,{5} --<MaterialTransLineID, bigint,>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_prd_CuttingLineUp
                                                   SET ProductionID = {0} --<ProductionID, bigint,>
                                                      ,WorkOrderID = {1} --<WorkOrderID, bigint,>
                                                      ,CutSeq = {2} --<CutSeq, decimal(5,1),>
                                                      ,StartTime = GETDATE() --<StartTime, datetime,>
                                                      ,FinishTime = GETDATE() --<FinishTime, datetime,>
                                                      ,CompleteFlag = {3} --<CompleteFlag, int,>
                                                      ,CreationDate = GETDATE() --<CreationDate, datetime,>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,CreatedBy = N'{4}' --<CreatedBy, varchar(45),>
                                                      ,UpdatedBy = N'{4}' --<UpdatedBy, varchar(45),>
                                                      ,MaterialTransLineID = {5} --<MaterialTransLineID, bigint,>
                                                 WHERE WorkOrderID = {1} AND CutSeq = {2}
                                            END" + Environment.NewLine
                                              , model.ProductionID
                                              , model.WorkOrderID
                                              , model.CutSeq
                                              , model.CompleteFlag
                                              , _session.UserID
                                              , model.MaterialTransLineID);

            //Update PartLot.CheckBox01 = 1 to change status has already used.
            sql += string.Format(@"UPDATE ucc_prd_CuttingLineUp SET CompleteFlag = 1 WHERE WorkOrderID = {0} AND CutSeq = {1}", model.WorkOrderID, Math.Round(model.CutSeq, 0) - 1);

            Repository.Instance.ExecuteWithTransaction(sql, "Save Line up");

            return GetCutLineUpAll(model.WorkOrderID);
        }

        public ProductionHeadModel SaveHead(SessionInfo _session, ProductionHeadModel model)
        {
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_prd_ProductionHead (NOLOCK)
										    WHERE WorkOrderID = {1} AND Plant = N'{0}'
									    )
                                        BEGIN
                                            INSERT INTO ucc_prd_ProductionHead
                                                       (WorkOrderID
                                                       ,ProductionDate
                                                       ,StartTime
                                                       ,FinishTime
                                                       ,PuaseTime
                                                       ,ProcessLineID
                                                       ,CompleteFlag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,Plant)
                                                 VALUES
                                                       ({1} --<WorkOrderID, bigint,>
                                                       ,CONVERT(DATETIME, '{2}',103) --<ProductionDate, datetime,>
                                                       ,CONVERT(DATETIME, '{3}',103) --<StartTime, datetime,>
                                                       ,CONVERT(DATETIME, '{4}',103) --<FinishTime, datetime,>
                                                       ,{5} --<PuaseTime, int,>
                                                       ,N'{6}' --<ProcessLineID, nvarchar(45),>
                                                       ,{7} --<CompleteFlag, int,>
                                                       ,GETDATE() --<CreationDate, datetime,>
                                                       ,GETDATE() --<LastUpdateDate, datetime,>
                                                       ,N'{8}' --<CreatedBy, varchar(45),>
                                                       ,N'{8}' --<UpdatedBy, varchar(45),>
                                                       ,N'{0}' --<Plant, varchar(45),>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE dbo.ucc_prd_ProductionHead
                                                   SET WorkOrderID = {1} --<WorkOrderID, bigint,>
                                                      --,ProductionDate = CONVERT(DATETIME, '{2}',103) --<ProductionDate, datetime,>
                                                      --,StartTime = CONVERT(DATETIME, '{3}',103) --<StartTime, datetime,>
                                                      ,FinishTime = CONVERT(DATETIME, '{4}',103) --<FinishTime, datetime,>
                                                      ,PuaseTime = {5} --<PuaseTime, int,>
                                                      ,ProcessLineID = N'{6}' --<ProcessLineID, nvarchar(45),>
                                                      ,CompleteFlag = {7} --<CompleteFlag, int,>
                                                      ,CreationDate = GETDATE() --<CreationDate, datetime,>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,CreatedBy = N'{8}' --<CreatedBy, varchar(45),>
                                                      ,UpdatedBy = N'{8}' --<UpdatedBy, varchar(45),>
                                                      ,Plant = N'{0}' --<Plant, varchar(45),>
                                                 WHERE WorkOrderID = {1} AND Plant = N'{0}'
                                            END" + Environment.NewLine
                                              , _session.PlantID
                                              , model.WorkOrderID
                                              , model.ProductionDate.ToString("dd/MM/yyyy hh:mm:ss:mmm tt")
                                              , model.StartTime.ToString("dd/MM/yyyy hh:mm:ss:mmm tt")
                                              , model.FinishTime.ToString("dd/MM/yyyy hh:mm:ss:mmm tt")
                                              , model.PuaseTime
                                              , model.ProcessLineID
                                              , model.CompleteFlag
                                              , _session.UserID);

            //Update PartLot.CheckBox01 = 1 to change status has already used.
            sql += string.Format(@"UPDATE ucc_pln_PlanHead SET OperationState = {1} WHERE WorkOrderID = {0}", model.WorkOrderID, model.OperationState);

            Repository.Instance.ExecuteWithTransaction(sql, "Save Production");

            return GetProdHead(model.WorkOrderID);
        }

        public bool SaveLineStop(SessionInfo _session, LineStopModel model)
        {
            string sql = string.Format(@"INSERT INTO ucc_prd_LineStop
                                                    (ProductionID
                                                    ,WorkOrderID
                                                    ,StopCode
                                                    ,Description
                                                    ,DurationTime
                                                    ,CreationDate
                                                    ,LastUpdateDate
                                                    ,CreatedBy
                                                    ,UpdatedBy
                                                    ,CutSeq)
                                                VALUES
                                                    ({0} --<ProductionID, bigint,>
                                                    ,{1} --<WorkOrderID, bigint,>
                                                    ,N'{2}' --<StopCode, nvarchar(45),>
                                                    ,N'{3}' --<Description, nvarchar(max),>
                                                    ,N'{4}' --<DurationTime, nvarchar(45),>
                                                    ,GETDATE() --<CreationDate, datetime,>
                                                    ,GETDATE() --<LastUpdateDate, datetime,>
                                                    ,N'{5}' --<CreatedBy, varchar(45),>
                                                    ,N'{5}' --<UpdatedBy, varchar(45),>
                                                    ,{6} --<CutSeq, int,>
		                                            )"
                /*
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_prd_LineStop
                                                   SET ProductionID = {0} --<ProductionID, bigint,>
                                                      ,WorkOrderID = {1} --<WorkOrderID, bigint,>
                                                      ,StopCode = N'{2}' --<StopCode, nvarchar(45),>
                                                      ,Description = N'{3}' --<Description, nvarchar(max),>
                                                      ,DurationTime = N'{5}' --<DurationTime, nvarchar(45),>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,UpdatedBy = N'{5}' --<UpdatedBy, varchar(45),>
                                                 WHERE WorkOrderID = {1} AND ProductionID = {0}
                                            END"
                 */
                                            + Environment.NewLine
                                              , model.ProductionID
                                              , model.WorkOrderID
                                              , model.StopCode
                                              , model.Description
                                              , model.DurationTime
                                              , _session.UserID
                                              , model.CutSeq);

            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Save Line Stop");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveSerialCutting(SessionInfo _session, ProductionHeadModel model)
        {
            string sql = string.Empty;
            foreach (var item in model.SerialLines.Where(i => i.CutSeq == model.CutSeq))
            {
                #region Queries statement

                sql += string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_prd_FinishJobBySerial (NOLOCK)
										    WHERE ProductionID = {1} AND WorkOrderID = {2} AND SerialNo = N'{3}' AND CutSeq = {4}
									    )
                                        BEGIN
                                            INSERT INTO ucc_prd_FinishJobBySerial
                                                       (Plant
                                                       ,ProductionID
                                                       ,WorkOrderID
                                                       ,SerialNo
                                                       ,CutSeq
                                                       ,MaterialTransLineID
                                                       ,MaterialSerialNo
                                                       ,Thick
                                                       ,Width
                                                       ,Length
                                                       ,ActualLengthM
                                                       ,PlanLengthM
                                                       ,Status
                                                       ,ActualWeight
                                                       ,PlanWeight
                                                       ,Quantity
                                                       ,TotalWeight
                                                       ,Flag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy
                                                       ,NGFlag
                                                       ,CuttingLineID)
                                                 VALUES
                                                       (N'{0}' --<Plant, nvarchar(8),>
                                                       ,{1} --<ProductionID, bigint,>
                                                       ,{2} --<WorkOrderID, bigint,>
                                                       ,N'{3}' --<SerialNo, nvarchar(40),>
                                                       ,{4} --<CutSeq, decimal(5,1),>
                                                       ,{5} --<MaterialTransLineID, bigint,>
                                                       ,N'{6}' --<MaterialSerialNo, nvarchar(40),>
                                                       ,{7} --<Thick, decimal(20,9),>
                                                       ,{8} --<Width, decimal(20,9),>
                                                       ,{9} --<Length, decimal(20,9),>
                                                       ,{10} --<ActualLengthM, decimal(20,9),>
                                                       ,{11} --<PlanLengthM, decimal(20,9),>
                                                       ,N'{12}' --<Status, nvarchar(10),>
                                                       ,{13} --<ActualWeight, decimal(20,9),>
                                                       ,{14} --<PlanWeight, decimal(20,9),>
                                                       ,{15} --<Quantity, decimal(20,9),>
                                                       ,{16} --<TotalWeight, decimal(20,9),>
                                                       ,{17} --<Flag, int,>
                                                       ,GETDATE() --<CreationDate, datetime,>
                                                       ,GETDATE() --<LastUpdateDate, datetime,>
                                                       ,N'{18}' --<CreatedBy, nvarchar(45),>
                                                       ,N'{18}' --<UpdatedBy, nvarchar(45),>
                                                       ,{19} --<NGFlag, int,>
                                                       ,{20} --<CuttingLineID, bigint,>
		                                               )
                                            END
                                        ELSE
                                            BEGIN
                                                UPDATE ucc_prd_FinishJobBySerial
                                                   SET Plant = N'{0}' --<Plant, nvarchar(8),>
                                                      ,ProductionID = {1} --<ProductionID, bigint,>
                                                      ,WorkOrderID = {2} --<WorkOrderID, bigint,>
                                                      ,SerialNo = N'{3}' --<SerialNo, nvarchar(40),>
                                                      ,CutSeq = {4} --<CutSeq, decimal(5,1),>
                                                      ,MaterialTransLineID = {5} --<MaterialTransLineID, bigint,>
                                                      ,MaterialSerialNo = N'{6}' --<MaterialSerialNo, nvarchar(40),>
                                                      ,Thick = {7} --<Thick, decimal(20,9),>
                                                      ,Width = {8} --<Width, decimal(20,9),>
                                                      ,Length = {9} --<Length, decimal(20,9),>
                                                      ,ActualLengthM = {10} --<ActualLengthM, decimal(20,9),>
                                                      ,PlanLengthM = {11} --<PlanLengthM, decimal(20,9),>
                                                      ,Status = N'{12}' --<Status, nvarchar(10),>
                                                      ,ActualWeight = {13} --<ActualWeight, decimal(20,9),>
                                                      ,PlanWeight = {14} --<PlanWeight, decimal(20,9),>
                                                      ,Quantity = {15} --<Quantity, decimal(20,9),>
                                                      ,TotalWeight = {16} --<TotalWeight, decimal(20,9),>
                                                      ,Flag = {17} --<Flag, int,>
                                                      ,LastUpdateDate = GETDATE() --<LastUpdateDate, datetime,>
                                                      ,UpdatedBy = N'{18}' --<UpdatedBy, nvarchar(45),>
                                                      ,NGFlag = {19} --<NGFlag, int,>
                                                      ,CuttingLineID = {20} --<CuttingLineID, bigint,>
                                                 WHERE ProductionID = {1} AND WorkOrderID = {2} AND SerialNo = N'{3}' AND CutSeq = {4}
                                            END" + Environment.NewLine
                                                  , _session.PlantID
                                                  , model.ProductionID
                                                  , model.WorkOrderID
                                                  , item.SerialNo
                                                  , model.CutSeq
                                                  , item.MaterialTransLineID    //{5}
                                                  , item.MaterialSerialNo
                                                  , item.Thick
                                                  , item.Width
                                                  , item.Length
                                                  , item.LengthActual   //{10}
                                                  , item.LengthM
                                                  , item.Status
                                                  , item.WeightActual
                                                  , item.UnitWeight
                                                  , item.Quantity       //{15}
                                                  , item.TotalWeight
                                                  , model.CompleteFlag
                                                  , _session.UserID
                                                  , Convert.ToInt32(item.NGFlag)
                                                  , item.CuttingLineID);

                //Update PartLot.CheckBox01 = 1 to change status has already used.
                sql += string.Format(@"UPDATE ucc_pln_SerialGenerated SET ProductionUsedFlag = 1 WHERE WorkOrderID = {0} AND SerialNo = N'{1}'", model.WorkOrderID, item.SerialNo);

                #endregion Queries statement
            }

            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Save Serial");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeteteCutFromPlan(int workOrderId, decimal cutSeq)
        {
            string sql = string.Format(@"UPDATE ucc_pln_SerialGenerated SET ProductionUsedFlag = 1 WHERE WorkOrderID = {0} AND CutSeq = {1}", workOrderId, cutSeq);

            try
            {
                Repository.Instance.ExecuteWithTransaction(sql, "Delete Line Stop");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}