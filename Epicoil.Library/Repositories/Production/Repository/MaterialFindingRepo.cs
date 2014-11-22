using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Production;
using System;
using System.Linq;
using System.Collections.Generic;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories.Production
{
    public class MaterialFindingRepo : IMaterialFindingRepo
    {
        public IEnumerable<MaterialFindingModel> GetAllMaterailTracker(string plant)
        {
            string sql = string.Format(@"SELECT pl.PartNum, mat.TransactionLineID
	                                        , pl.LotNum, plnh.WorkOrderID, plnh.WorkOrderNum, plnh.ProcessLine as ProcessLineCode, res.Description as ProcessLineName
	                                        , mtrk.FoundFlag, mtrk.UnPackFlag, mtrk.ProcessFlag
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , mat.UsingWgt as UsingWeight, mat.RemainWgt as RemainWeight
	                                        , mat.Qty as Quantity, pl.Number06 as QuantityPack, SelectCB as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName, mat.CBalready, mat.UsingLM
                                        FROM ucc_pln_Material mat
	                                        LEFT JOIN ucc_prd_MaterailTracker mtrk ON(mat.TransactionLineID = mtrk.MaterialTransLineID)
	                                        INNER JOIN ucc_pln_PlanHead plnh ON(mat.WorkOrderID = plnh.WorkOrderID)
	                                        LEFT JOIN Resource_ res ON(plnh.ProcessLine = res.ResourceID)
	                                        INNER JOIN PartLot pl ON(mat.MCSSNo = pl.PartNum AND mat.Serial = pl.LotNum)
	                                        INNER JOIN Part p ON(pl.PartNum = p.PartNum)
                                            INNER JOIN PartPlant pln ON(p.PartNum = pln.PartNum)
	                                        LEFT JOIN UD29 cmdt ON(p.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(p.ShortChar01 = spec.Key2 and p.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(p.ShortChar09 = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(pl.Character02 = busi.Key1)
	                                        LEFT JOIN Vendor ven ON(pl.ShortChar03 = ven.VendorID)
	                                        LEFT JOIN UD19 maker ON(pl.ShortChar01 = maker.Key1)
	                                        LEFT JOIN UD14 mill ON(pl.ShortChar01 = mill.Key2 and pl.ShortChar02 = mill.Key1)
	                                        LEFT JOIN Customer cust ON(p.Character08 = cust.CustID)
                                        WHERE pln.Plant = N'{0}' 
	                                        AND pl.Number05 = 1 -- N (Normal)
	                                        AND pl.Number08 IN (2) -- 2= Processing Plan
	                                        AND pl.CheckBox01 = 1 -- Using Flag", plant);

            return Repository.Instance.GetMany<MaterialFindingModel>(sql);
        }


        public IEnumerable<MaterialFindingModel> GetAllMaterailTrackerByFilter(string plant, MaterialFindingModel model, IEnumerable<MaterialFindingModel> data = null)
        {
            IEnumerable<MaterialFindingModel> query = data;
            if (query == null) query = this.GetAllMaterailTracker(plant);

            if (!string.IsNullOrEmpty(model.WorkOrderNum)) query = query.Where(p => p.WorkOrderNum.Equals(model.WorkOrderNum.GetString()));
            if (!string.IsNullOrEmpty(model.ProcessLineCode)) query = query.Where(p => p.ProcessLineCode.Equals(model.ProcessLineCode.GetString()));
            //if (!string.IsNullOrEmpty(model.PossessionName)) query = query.Where(p => p.PossessionName.Equals(model.PossessionName.GetString()));
            if (!string.IsNullOrEmpty(model.SpecCode)) query = query.Where(p => p.SpecCode.Equals(model.SpecCode.GetString()));
            if (!string.IsNullOrEmpty(model.CommodityCode)) query = query.Where(p => p.CommodityCode.Equals(model.CommodityCode.GetString()));
            if (!string.IsNullOrEmpty(model.CoatingCode)) query = query.Where(p => p.CoatingCode.Equals(model.CoatingCode.GetString()));

            query = query.Where(p => p.FoundFlag == model.FoundFlag);
            query = query.Where(p => p.UnPackFlag == model.UnPackFlag);
            query = query.Where(p => p.ProcessFlag == model.ProcessFlag);

            return query;
        }

        public IEnumerable<MaterialFindingModel> SaveMaterial(SessionInfo _session, MaterialFindingModel model)
        {
            //int id = 0;
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_prd_MaterailTracker (NOLOCK)
										    WHERE MaterialTransLineID = {1}
									    )
                                        BEGIN
                                            INSERT INTO ucc_prd_MaterailTracker
                                                       (WorkOrderID
                                                       ,MaterialTransLineID
                                                       ,FoundFlag
                                                       ,UnPackFlag
                                                       ,ProcessFlag
                                                       ,CreationDate
                                                       ,LastUpdateDate
                                                       ,CreatedBy
                                                       ,UpdatedBy)
                                                 VALUES
                                                       ({0}  --<WorkOrderID, bigint,>
                                                       ,{1}  --<MaterialTransLineID, bigint,>
                                                       ,{2}  --<FoundFlag, int,>
                                                       ,{3}  --<UnPackFlag, int,>
                                                       ,{4}  --<ProcessFlag, int,>
                                                       ,GETDATE()  --<CreationDate, datetime,>
                                                       ,GETDATE()  --<LastUpdateDate, datetime,>
                                                       ,N'{5}'  --<CreatedBy, varchar(45),>
                                                       ,N'{5}' --<UpdatedBy, varchar(45),>
		                                    )
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE ucc_prd_MaterailTracker
                                               SET WorkOrderID = {0}  --<WorkOrderID, bigint,>
                                                  ,FoundFlag = {2}  --<FoundFlag, int,>
                                                  ,UnPackFlag = {3}  --<UnPackFlag, int,>
                                                  ,ProcessFlag = {4}  --<ProcessFlag, int,>
                                                  ,LastUpdateDate = GETDATE()  --<LastUpdateDate, datetime,>
                                                  ,UpdatedBy =  N'{5}' --<UpdatedBy, varchar(45),>
                                             WHERE  MaterialTransLineID = {1} --<Search Conditions,,>
                                        END" + Environment.NewLine
                                              , model.WorkOrderID
                                              , model.TransactionLineID
                                              , Convert.ToInt32(model.FoundFlag.GetBoolean())
                                              , Convert.ToInt32(model.UnPackFlag.GetBoolean())
                                              , Convert.ToInt32(model.ProcessFlag.GetBoolean())
                                              , _session.UserID
                                              );

            //Update PartLot.CheckBox01 = 1 to change status has already used.
//            sql += string.Format(@"UPDATE PartLot SET CheckBox01 = 1, ShortChar05 = N'{2}', Date03 = CONVERT(DATETIME, '{3}',103), Number08 = 2
//                                   WHERE PartNum = N'{0}' AND LotNum = N'{1}'"
//                                   , model.MCSSNo, model.SerialNo, model.WorkOrderNum, model.WorkDate.ToString("dd/MM/yyyy hh:mm:ss"));

            Repository.Instance.ExecuteWithTransaction(sql, "Save Material");
            return GetAllMaterailTracker(_session.PlantID);
        }
    }
}